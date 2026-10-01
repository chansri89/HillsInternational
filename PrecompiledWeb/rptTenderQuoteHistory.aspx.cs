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

public partial class rptTenderQuoteHistory : System.Web.UI.Page
{
    ProcessBus Bus = new ProcessBus(); LibFunctions Lib = new LibFunctions();
    // CrystalDecisions.CrystalReports.Engine.ReportDocument oReport = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    BaseClass BaseMsg = new BaseClass();
    Validation Isvalid = new Validation();
    private static List<EmployeeMasterMsg> EmpList = new List<EmployeeMasterMsg>();
    public static List<ClientMsg> ClList = new List<ClientMsg>();
    private static List<ProjectMsg> projList = new List<ProjectMsg>();
    public static List<ProjectMsg> PMList = new List<ProjectMsg>();
    //DateTime frmDate;
    //DateTime toDate;
    private static List<CompanyMessage> CmpList = new List<CompanyMessage>();
    public static string ProgramName = string.Empty;
   // private static string TranType = "";
    public static string CreatePermission = "";
    public static string ParameterFlag = "";
   
    #region Events

    protected void Page_Load(object sender, EventArgs e)
    {
        ProgramName = System.IO.Path.GetFileName(Request.PhysicalPath);
        if (!Page.IsPostBack)
        {
            lblExceptionList.Text = "Project Tender Package Quote History";
            LoadCompany();
        }
    }
    protected void ddlCompanyChanged(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedIndex > 0)
        {
            Int32 WCompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
            loadddlCompChanged(WCompanyId);
            //AllClear();
            //LoadClient(WCompanyId);
            //LoadForYearMonth();
            // LoadProject();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Location Not Selected" + "');", true);
            //Pnlgv.Visible = false;
            // pnlAdd.Visible = false;
        }
    }
    private void loadddlCompChanged(Int32 WCompanyId)
    {
        LoadClient(WCompanyId);

    }
    protected void ddlClientChanged(object sender, EventArgs e)
    {
        if (ddlClient.SelectedIndex > 0 && ddlCompany.SelectedIndex > 0)
        {
            Int32 WCompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
            string WClientCode = ddlClient.SelectedValue;
            LoadProject(WCompanyId, WClientCode);
            pnlExList.Visible = false;
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Company and Client are to be Selected" + "');", true);
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
            RepMsg.ProjectCode  = (ddlProject.SelectedValue); // 
            LoadReport(RepMsg);
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        pnlExList.Visible = false;
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
                //LoadProject();
                Int32 WCompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                loadddlCompChanged(WCompanyId);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Company Data not Found" + "');", true);
            // ddlCompany.Items.Insert(0, "--Select Please--");
        }
    }
    private void LoadClient(Int32 WCompanyId)
    {
        ClientMsg Cmp = new ClientMsg();
        Cmp.Flag = "R";
        Cmp.CreatedBy = BaseMsg.EmployeeCode;
        Cmp.CompanyId = WCompanyId;
        ClList = Bus.MasClientInsertUpdateandDelete(Cmp);
        ClList = (from Cl in ClList where Cl.ClientType.Trim().ToUpper() == "CLIENT" select Cl).ToList();
        ddlClient.DataSource = ClList;
        ddlClient.DataBind();
        ddlClient.Items.Insert(0, "-- Select Please --");
        ddlClient.Enabled = true;
        if (ClList.Count > 1)
        {
            //just Leave
        }
        else if (ClList.Count == 1)
        {
            ddlClient.SelectedIndex = 1;
            string WClientCode = ddlClient.SelectedValue;
            LoadProject(WCompanyId, WClientCode);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Pls Create Clients for the selected Company" + "');", true);
        }

    }
    private void LoadProject(Int32 WCompanyId, string WClientCode)//To load data into grid.
    {
        projList = Bus.MasClientProjectSelect(WCompanyId);
        var proj = (from pj in projList where pj.ClientCode == WClientCode select new { pj.ProjectCode, pj.ProjectName }).Distinct().ToList();
        ddlProject.DataSource = proj;
        ddlProject.DataBind();
        ddlProject.Items.Insert(0, "-- Select Please --");

    }
    //private void LoadProject()
    //{
    //    int varcount = 0;
    //    Int32 CompId = Convert.ToInt32(ddlCompany.SelectedValue);
    //    //string EmpCode = BaseMsg.EmployeeCode;
    //    //string uploadFlag = "C";
    //    PMList = Bus.MasClientProjectSelect(CompId);

    //    var proj = (from pm in PMList where pm.CompanyId == CompId select new { pm.ProjectCode, pm.ProjectName }).Distinct().ToList();
    //    if (proj != null)
    //    {
    //        ddlProject.DataSource = proj;
    //        ddlProject.DataBind();
    //        ddlProject.Items.Insert(0, "--Select Please--");
    //        ddlProject.SelectedIndex = 0;
    //        ddlProject.Enabled = true;
    //        ddlProject.Focus();
    //        foreach (var c in proj)
    //        {
    //            varcount = varcount + 1;
    //        }
    //        if (varcount == 1)
    //        {
    //            ddlProject.SelectedIndex = 1;
    //        }
    //    }
    //    else
    //    {
    //        ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Project Data Not found for the Client" + "');", true);
    //    }
    //}

    public void LoadReport(ReportMsg reportMsg)
    {

        string ReportTitle = ddlCompany.SelectedItem.ToString();
        string ReportTitle1 = "Tender Cost History: " + ddlProject.SelectedItem.ToString();
        string RepPeriod = " From " ;
        string RunDate = "RunDate :" + System.DateTime.UtcNow.AddMinutes(330).ToString("dd-MM-yyyy HH:mm");

        DataTable dtAp = new DataTable();
        DataTable dt = new DataTable();
        dt = Bus.rptTenderQuoteHistory(reportMsg); //
        dtAp = dt;
       // dt = Bus.rptTenderQuoteComparision(reportMsg); //rptTenderQuoteComparision
        //if (dt != null)
        //{
        //    if (rbtType.SelectedValue == "2")
        //    {
        //        dtAp = dt;
        //        //ReportTitle = ReportTitle + "  Tender Details for ";
        //    }
        //    else if (rbtType.SelectedValue == "1")
        //    {
        //        //ReportTitle = ReportTitle + "   Tender Cost for ";
        //        //IEnumerable<DataRow> dr = (from dt1 in dt.AsEnumerable() where dt1.Field<string>("UOM").Length > 0 && dt1.Field<string>("SrlNo") !="SrlNo" select dt1);
        //        IEnumerable<DataRow> dr = (from dt1 in dt.AsEnumerable()
        //                                   where
        //                                       (dt1.Field<string>("SrlNo") != "SrlNo" && (dt1.Field<decimal>("SupplyAmount")) > 0 || (dt1.Field<decimal>("InstallAmount")) > 0)
        //                                   select dt1);
        //       // if (dr.Count() > 0)
        //        if (dr.Count() > 0)
        //        {
        //            dtAp = dr.CopyToDataTable<DataRow>();
        //            dtAp = dt;
        //        }
        //    }
        //    else
        //    {
        //    }
        //}
        if ((dtAp != null && dtAp.Rows.Count > 0))
        {
            Microsoft.Reporting.WebForms.ReportDataSource rptDatasource = new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", dtAp);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rptDatasource);
            LocalReport rep = ReportViewer1.LocalReport;
            String reppath = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["RptPath"]);//.GetSection("RptPath"));
            string Wreppath = "\\" + "TenderReports" + "\\"; // scs 201007 added for generating new reports area
            reppath = reppath + Wreppath;
            rep.ReportPath = reppath + "rptTenderQuoteHistory.rdlc";
           // rep.ReportPath = reppath + "rptTenderComparision.rdlc";                
            //ReportTitle1 =" ";
            this.ReportViewer1.LocalReport.DisplayName = " Project- " + ReportTitle1+" History_" + System.DateTime.UtcNow.AddMinutes(330).ToString("yyyyMMddHHmm");
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
                rp4.Values.Add(ReportTitle1);
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
        if (ddlCompany.SelectedIndex == 0 || ddlProject.SelectedIndex == 0)
        {
            Error = 1;
            DisplayError = "Company and Project must be selected ";
        }
        

        if (Error == 1)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + DisplayError + "');", true);
        }
        return Error;
    }
    #endregion



}