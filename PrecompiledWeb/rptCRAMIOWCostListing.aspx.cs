using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Resources;
using Ganini.Lib;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.Reporting.WebForms; //required for creating local report scs 231213

public partial class rptCRAMIOWCostListing : System.Web.UI.Page
{
    ProcessBus Bus = new ProcessBus(); LibFunctions Lib = new LibFunctions();
    // CrystalDecisions.CrystalReports.Engine.ReportDocument oReport = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    BaseClass BaseMsg = new BaseClass();
    Validation Isvalid = new Validation();
    private static List<EmployeeMasterMsg> EmpList = new List<EmployeeMasterMsg>();
    //private static List<PFEStablishmentMsg> PFEList = new List<PFEStablishmentMsg>();/
    public static List<GroupMsg> GList = new List<GroupMsg>();
    public static List<SubGroupMsg> SGList = new List<SubGroupMsg>();
    public static List<IOWHeadMsg> IOWHList = new List<IOWHeadMsg>();
    DateTime frmDate;
    DateTime toDate;
    private static List<CompanyMessage> CmpList = new List<CompanyMessage>();
    public static string ProgramName = string.Empty;
    private static string TranType = "";
    public static string CreatePermission = "";
    public static string ParameterFlag = "";
   
    #region Events

    protected void Page_Load(object sender, EventArgs e)
    {
        ProgramName = System.IO.Path.GetFileName(Request.PhysicalPath);
        if (!Page.IsPostBack)
        {
            //lblExceptionList.Text = "Item Rate Report";
            LoadCompany();
            
            
        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        if (IsView() == 0)
        {
            System.Globalization.DateTimeFormatInfo dateinfo = new System.Globalization.DateTimeFormatInfo();
            dateinfo.ShortDatePattern = "dd-MM-yyyy";
            ReportMsg RepMsg = new ReportMsg();
            
            RepMsg.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue); // 
            if (ddlGroup.SelectedIndex == 0)
            {
                RepMsg.GroupCode = "0";
            }
            else
            {
                RepMsg.GroupCode = (ddlGroup.SelectedValue); // 
            }
            if (ddlSubGroup.SelectedIndex == 0)
            {
                RepMsg.SubGroupCode = "0";
            }
            else
            {
                RepMsg.SubGroupCode = (ddlSubGroup.SelectedValue); // 
            }
            if (ddlIOWHead.SelectedIndex == 0)
            {
                RepMsg.IOWHeadCode = "0";
            }
            else
            {
                RepMsg.IOWHeadCode = (ddlIOWHead.SelectedValue); // 
            }
            if (ddlRegion.SelectedIndex == 0)
            {
                RepMsg.Region = "ALL";
            }
            else
            {
                RepMsg.Region = (ddlRegion.SelectedValue); // 
            }
            RepMsg.TransType = rbtType.SelectedValue;
            RepMsg.ForYearMonth = Convert.ToInt64(ddlForYearMonth.SelectedValue);
            LoadReport(RepMsg);
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        pnlExList.Visible = false;
    }
    protected void ddlCompanyChanged(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedIndex > 0)
        {
            LoadGroup();
            LoadSubGroup();
            LoadIOWHead();
            LoadForYearMonth();
            LoadRegion();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Pls Select Company" + "');", true);
            
        }
    }
    protected void ddlGroupChanged(object sender, EventArgs e)
    { 
        List<SubGroupMsg> SGLst = new List<SubGroupMsg>();
        List<IOWHeadMsg> IOWHlst = new List<IOWHeadMsg>();
        if (ddlCompany.SelectedIndex > 0 && ddlGroup.SelectedIndex >0 )
        {
           
            SGLst = (from sg in SGList where sg.GroupCode == ddlGroup.SelectedValue select sg).ToList();
            ddlLoadSubGroup(SGLst);
            IOWHlst = (from ih in IOWHList where ih.GroupCode == ddlGroup.SelectedValue select ih).ToList();
            ddlLoadIOWHead(IOWHlst);
        }
        else if (ddlCompany.SelectedIndex > 0 && ddlGroup.SelectedIndex == 0 )
        {
            SGLst = SGList.ToList();
            IOWHlst = IOWHList.ToList();
            ddlLoadSubGroup(SGLst);
            ddlLoadIOWHead(IOWHlst);
        }      
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Pls Select Company" + "');", true);

        }
    }
    private void LoadCompany()
    {
        CmpList = Lib.LoadCompany();

        if (CmpList != null && CmpList.Count > 0)
        {
            ddlCompany.DataSource = CmpList;
            ddlCompany.DataBind();
            ddlCompany.Items.Insert(0, "--Select Please--");
            ddlCompany.SelectedIndex = 0;
            if (CmpList.Count == 1)
            {
                ddlCompany.SelectedIndex = 1;
                LoadGroup();
                LoadSubGroup();
                LoadIOWHead();
                LoadForYearMonth();
                LoadRegion();
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Company Data not Found" + "');", true);
            // ddlCompany.Items.Insert(0, "--Select Please--");
        }
    }
    private void LoadGroup()
    {
        GroupMsg pkmsg = new GroupMsg();
        pkmsg.Flag = "R";
        pkmsg.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
        GList = Bus.MasGroupInsertUpdate(pkmsg);
        if (GList.Count > 0)
        {
            ddlGroup.DataSource = GList;
            ddlGroup.DataBind();
            ddlGroup.Items.Insert(0, "ALL");
            ddlGroup.SelectedIndex = 0;
            ddlGroup.Enabled = true;
            ddlGroup.Focus();
        }
        else
        {
            ddlGroup.Items.Clear();
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Group Data Not found " + "');", true);
        }
    }
    private void LoadSubGroup()
    {
        SubGroupMsg pkmsg = new SubGroupMsg();
        pkmsg.Flag = "R";
        pkmsg.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
        SGList = Bus.MasSubGroupInsertUpdate(pkmsg);
        if (SGList.Count > 0)
        {
            ddlLoadSubGroup(SGList);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Sub Group Data Not found " + "');", true);
        }
    }
    private void ddlLoadSubGroup(List<SubGroupMsg> sglst)
    {
        ddlSubGroup.DataSource = sglst;
        ddlSubGroup.DataBind();
        ddlSubGroup.Items.Insert(0, "ALL");
        ddlSubGroup.SelectedIndex = 0;
        ddlSubGroup.Enabled = true;
        ddlSubGroup.Focus();
    }
    private void LoadRegion() //scs241221 hills wants region to be part of calculation
    {
        Int32 Wcompanyid = Convert.ToInt32(ddlCompany.SelectedValue);
        DataTable dt = new DataTable();
        dt = Bus.ItemRateYearMonthSelect(Wcompanyid);
        if (dt.Rows.Count > 0)
        {
            ddlRegion.DataSource = dt;
            ddlRegion.DataBind();
            ddlRegion.Items.Insert(0, "ALL");
            ddlRegion.SelectedIndex = 0;
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Region data in Item Rate master is not Found" + "');", true);
        }
    }
 

    private void LoadIOWHead()
    {
        IOWHeadMsg pkmsg = new IOWHeadMsg();
        pkmsg.Flag = "R";
        pkmsg.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
        IOWHList = Bus.MasIOWHeadInsertUpdate(pkmsg);
        if (IOWHList.Count > 0)
        {
            ddlIOWHead.DataSource = IOWHList;
            ddlIOWHead.DataBind();
            ddlIOWHead.Items.Insert(0, "ALL");
            ddlIOWHead.SelectedIndex = 0;
            ddlIOWHead.Enabled = true;
            ddlIOWHead.Focus();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "IOW Head Data Not found " + "');", true);
        }
    }
    private void ddlLoadIOWHead(List<IOWHeadMsg> IHLst)
    {
        ddlIOWHead.DataSource = IHLst;
        ddlIOWHead.DataBind();
        ddlIOWHead.Items.Insert(0, "ALL");
        ddlIOWHead.SelectedIndex = 0;
        ddlIOWHead.Enabled = true;
        ddlIOWHead.Focus();
    }
    private void LoadForYearMonth()
    {
        if (ddlCompany.SelectedIndex > 0)
        {
            Int32 Wcompanyid = Convert.ToInt32(ddlCompany.SelectedValue);
            DataTable dt = new DataTable();
            dt = Bus.CRAMForYearMonthSelect(Wcompanyid);
            ddlForYearMonth.DataSource = dt;
            ddlForYearMonth.DataBind();
            ddlForYearMonth.Items.Insert(0, "Select Pls");
            if (dt.Rows.Count == 1)
            {
                ddlForYearMonth.SelectedIndex = 1;
            }
            else
            {
                ddlForYearMonth.SelectedIndex = 0;
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + " Pls Select Company " + "');", true);
        }
    }
    public void LoadReport(ReportMsg reportMsg)
    {

        string ReportTitle = ddlCompany.SelectedItem.ToString();
        string WGroup = " Group: " + ddlGroup.SelectedItem.ToString() ;
        string WSubGroup = " - SubGroup: " + ddlSubGroup.SelectedItem.ToString() ;
        string WIOWHead =  " - IOWHead: " + ddlIOWHead.SelectedItem.ToString() ;
        string RepPeriod = "  " ;
        string RunDate = "RunAt:" + System.DateTime.UtcNow.AddMinutes(330).ToString("dd-MM-yyyy HH:mm");
        WGroup = WGroup + WSubGroup + WIOWHead + " - For Year Month: " + ddlForYearMonth.SelectedValue;
        DataTable dtAp = new DataTable();
        DataTable dt = new DataTable();
        dt = Bus.rptCRAMIOWCostListing(reportMsg); //
       // dt = Bus.rptTenderQuoteComparision(reportMsg); //rptTenderQuoteComparision
        //if (dt != null)
        //{
            //dtAp = dt;  // Passed reion filter in sql hence commenting below 250415
        //}
        //if (ddlRegion.SelectedIndex > 0)
        //{
        //    IEnumerable<DataRow> filteredRows = dt.AsEnumerable()
        //      .Where(row => row.Field<string>("Region") == ddlRegion.SelectedItem.ToString());

        //    dtAp = filteredRows.CopyToDataTable();
        //}
        if ((dt != null && dt.Rows.Count > 0))
        {
            Microsoft.Reporting.WebForms.ReportDataSource rptDatasource = new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", dt);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rptDatasource);
            LocalReport rep = ReportViewer1.LocalReport;
            String reppath = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["RptPath"]);//.GetSection("RptPath"));
            string Wreppath = "\\" + "CRAM" + "\\"; // scs 201007 added for generating new reports area
            reppath = reppath + Wreppath;
            if (rbtType.SelectedValue == "2")
            {
                rep.ReportPath = reppath + "rptIOWCostListing.rdlc";
            }
            else
            {
                rep.ReportPath = reppath + "rptIOWCostSummaryListing.rdlc";
                WGroup = "Summary: " + WGroup;
            }

            this.ReportViewer1.LocalReport.DisplayName = " IOW Cost Listing - " + WGroup +"_" + System.DateTime.UtcNow.AddMinutes(330).ToString("yyyyMMdd");
            try
            {
                ReportParameter rp1 = new ReportParameter();
                rp1.Name = "ReportTitle";
                rp1.Values.Add(ReportTitle);
                ReportViewer1.LocalReport.SetParameters(rp1);

                ReportParameter rp2 = new ReportParameter();
                rp2.Name = "RepPeriod";
                rp2.Values.Add(RepPeriod);
                ReportViewer1.LocalReport.SetParameters(rp2);

                ReportParameter rp3 = new ReportParameter();
                rp3.Name = "RunDate";
                rp3.Values.Add(RunDate);
                ReportViewer1.LocalReport.SetParameters(rp3);

                ReportParameter rp4 = new ReportParameter();
                rp4.Name = "ReportTitle1";
                rp4.Values.Add(WGroup);
                ReportViewer1.LocalReport.SetParameters(rp4);
            }
            catch (Exception ex)
            {
                string a = ex.ToString(); // dummy
            }


            ReportViewer1.Visible = true;
            ReportViewer1.LocalReport.Refresh();
            pnlExList.Visible = true;
        }
        else
        {
            //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "No Data Available for the Selected Period." + "');", true);
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Data not Available." + "');", true);
            ReportViewer1.Visible = false;
        }
    }
    #endregion
    #region Validation
    private int IsView()
    {

        System.Globalization.DateTimeFormatInfo dateinfo = new System.Globalization.DateTimeFormatInfo();
        dateinfo.ShortDatePattern = "dd/MM/yyyy";
        int Error = 0;
        string DisplayError = "";
        if (ddlCompany.SelectedIndex == 0 )
        {
            Error = 1;
            DisplayError = "Company must be selected ";
        }
        if (ddlForYearMonth.SelectedIndex == 0)
        {
            Error = 1;
            DisplayError = "Year Month must be selected ";
        }
        if (ddlGroup.Items.Count == 0)
        {
            Error = 1;
            DisplayError = "Check for the selected Company there is No group ";
        }
        if (Error == 1)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + DisplayError + "');", true);
        }
        return Error;
    }
    #endregion



}