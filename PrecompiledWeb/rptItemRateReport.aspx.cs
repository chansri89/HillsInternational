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

public partial class rptItemRateReport : System.Web.UI.Page
{
    ProcessBus Bus = new ProcessBus(); LibFunctions Lib = new LibFunctions();
    // CrystalDecisions.CrystalReports.Engine.ReportDocument oReport = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    BaseClass BaseMsg = new BaseClass();
    Validation Isvalid = new Validation();
    private static List<EmployeeMasterMsg> EmpList = new List<EmployeeMasterMsg>();
    //private static List<PFEStablishmentMsg> PFEList = new List<PFEStablishmentMsg>();/
    public static List<ItemCategoryMsg> PMList = new List<ItemCategoryMsg>();
    public static List<ItemSubCategoryMsg> SubCatList = new List<ItemSubCategoryMsg>();
    List<ItemRateMsg> ItRatelst = new List<ItemRateMsg>();
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
            ddlItemSubCategory.Enabled = false;
            ddlRegion.Enabled = false;
            //lblSubCategory.Visible = false;
            
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
            if (ddlItemCategory.SelectedIndex == 0)
            {
                RepMsg.ItemCategoryId = 0;
                RepMsg.ItemSubCategoryId = 0;
            }
            else
            {
                RepMsg.ItemCategoryId = Convert.ToInt32(ddlItemCategory.SelectedValue); // 
            }
            if (ddlItemSubCategory.SelectedIndex == 0)
            {
                RepMsg.ItemSubCategoryId = 0;
            }
            else
            {
                RepMsg.ItemSubCategoryId = Convert.ToInt32(ddlItemSubCategory.SelectedValue); // 
            }
            if (ddlRegion.SelectedIndex == 0)
            {
                RepMsg.Region = "All";
            }
            else
            {
                RepMsg.Region = (ddlRegion.SelectedValue); // 
            }
            
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
            LoadItemCategory();
            LoadItemSubCategory();
            LoadRegion();
            ddlItemSubCategory.Enabled = false;
            ddlRegion.Enabled = false;
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Pls Select Company" + "');", true);
            
        }
    }
    protected void ddlItemCategoryChanged(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedIndex > 0)
        {
            LoadItemSubCategory();
            LoadRegion();
            ddlItemSubCategory.Enabled = true;
            ddlRegion.Enabled = true;
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Pls Select Company" + "');", true);

        }
    }
    protected void ddlItemSubCategoryChanged(object sender, EventArgs e)
    {
        LoadRegion();
        ddlRegion.Enabled = true;
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
                LoadItemCategory();
                LoadItemSubCategory();
                LoadRegion();
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Company Data not Found" + "');", true);
            // ddlCompany.Items.Insert(0, "--Select Please--");
        }
    }
    private void LoadItemCategory()
    {
        ItemCategoryMsg pkmsg = new ItemCategoryMsg();
        pkmsg.Flag = "R";
        pkmsg.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
        PMList = Bus.MasItemCategoryInsertUpdate(pkmsg);
        if (PMList.Count >0)
        {
            ddlItemCategory.DataSource = PMList;
            ddlItemCategory.DataBind();
            ddlItemCategory.Items.Insert(0, "ALL");
            ddlItemCategory.SelectedIndex = 0;
            ddlItemCategory.Enabled = true;
            ddlItemCategory.Focus();

        }
        else
        {
            ddlItemCategory.Items.Clear();
           // ddlRegion.Items.Insert(0, "");
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Item Category Data Not found " + "');", true);
        }
    }
    private void LoadItemSubCategory()
    {
        ItemSubCategoryMsg subMsg = new ItemSubCategoryMsg();
        List<ItemSubCategoryMsg> sublst = new List<ItemSubCategoryMsg>();
        subMsg.Flag = "R";
        subMsg.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
        sublst = Bus.MasItemSubCategoryInsertUpdate(subMsg);
        if (ddlItemCategory.SelectedIndex > 0)
        {
            SubCatList = (from sub in sublst where sub.ItemCategoryId == Convert.ToInt32(ddlItemCategory.SelectedValue) select sub).ToList();
        }
        else
        {
            SubCatList = sublst.ToList();
        }
  
        if (SubCatList.Count > 0)
        {
            ddlItemSubCategory.DataSource = SubCatList;
            ddlItemSubCategory.DataBind();
            ddlItemSubCategory.Items.Insert(0, "ALL");
            ddlItemSubCategory.SelectedIndex = 0;
            //ddlItemSubCategory.Enabled = true;
            ddlItemSubCategory.Focus();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Item sub Category Data Not found " + "');", true);
        }
    }
    private void LoadRegion()
    {
        ItemRateMsg tig = new ItemRateMsg();
        List<RegionMsg> RegLst = new List<RegionMsg>();
        List<string> regl = new List<string>();
        tig.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
        tig.Flag = "R";
        ItRatelst = Bus.MasItemRateInsertUpdateandDelete(tig);
        if (ddlItemCategory.SelectedIndex == 0 && ddlItemSubCategory.SelectedIndex == 0)
        {
            regl = (from reg in ItRatelst select reg.Region).Distinct().ToList(); 
          
        }
        else if (ddlItemCategory.SelectedIndex > 0 && ddlItemSubCategory.SelectedIndex == 0)
        {
            regl = (from reg in ItRatelst where reg.ItemCategoryName == Convert.ToString(ddlItemCategory.SelectedItem) select reg.Region).Distinct().ToList();
        }
        else if (ddlItemCategory.SelectedIndex > 0 && ddlItemSubCategory.SelectedIndex > 0)
        {
            regl = (from reg in ItRatelst
                    where reg.ItemCategoryName == Convert.ToString(ddlItemCategory.SelectedItem) && reg.ItemSubCategoryName == Convert.ToString(ddlItemSubCategory.SelectedItem)
                       select reg.Region).Distinct().ToList();
        }
        else
        {
            regl = (from reg in ItRatelst
                    where reg.ItemCategoryName == Convert.ToString(ddlItemCategory.SelectedItem)
                       select reg.Region).Distinct().ToList();
        }
        List<RegionMsg> RegList = ConvertStringListToListMessage(regl);
        if (RegList.Count > 0) //for var type length gives count
        {
            ddlRegion.DataSource = RegList;
            ddlRegion.DataBind();
            ddlRegion.Items.Insert(0, "ALL");
            ddlRegion.SelectedIndex = 0;
            //ddlRegion.Enabled = true;
            ddlRegion.Focus();
        }
        else
        {
            ddlRegion.Items.Clear();
            ddlRegion.Items.Insert(0, "");
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Item sub Category Data Not found " + "');", true);
        }            
                       
    }
    private List<RegionMsg> ConvertStringListToListMessage(List<string> stringLst)
    {
        List<RegionMsg> RegLst = new List<RegionMsg>();
       
        foreach (string s in stringLst)
        {
            RegionMsg reg = new RegionMsg();
            reg.Region = s.ToString();
            RegLst.Add(reg);
        }

        return RegLst;
    }
    public void LoadReport(ReportMsg reportMsg)
    {

        string ReportTitle = ddlCompany.SelectedItem.ToString();
        string ReportTitle1 = ""; string Category = " Category : ALL "; string subCategory = " SubCategory : ALL "; string Region = " Region: ALL ";
        if (ddlItemCategory.SelectedIndex > 0)
        {
            Category = "-Category: " + ddlItemCategory.SelectedItem.ToString();
        }
        if (ddlItemSubCategory.SelectedIndex > 0)
        {
            subCategory = "-SubCategory: " + ddlItemSubCategory.SelectedItem.ToString();
        }
        if (ddlRegion.SelectedIndex > 0)
        {
            Region = "-Region: "+ ddlRegion.SelectedItem.ToString();
        }

          ReportTitle1 = ReportTitle1+  Category + subCategory + Region ;
        string RepPeriod = " From " ;
        string RunDate = "RunDate :" + System.DateTime.UtcNow.AddMinutes(330).ToString("dd-MM-yyyy HH:mm");

        DataTable dtAp = new DataTable();
        DataTable dt = new DataTable();
        dt = Bus.rptItemRateSelect(reportMsg); //
       // dt = Bus.rptTenderQuoteComparision(reportMsg); //rptTenderQuoteComparision
        if (dt != null)
        {
            dtAp = dt;
        }
        if ((dtAp != null && dtAp.Rows.Count > 0))
        {
            Microsoft.Reporting.WebForms.ReportDataSource rptDatasource = new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", dtAp);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rptDatasource);
            LocalReport rep = ReportViewer1.LocalReport;
            String reppath = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["RptPath"]);//.GetSection("RptPath"));
            string Wreppath = "\\" + "CRAM" + "\\"; // scs 201007 added for generating new reports area
            reppath = reppath + Wreppath;
            rep.ReportPath = reppath + "rptItemRateReport.rdlc";

            this.ReportViewer1.LocalReport.DisplayName = " Package- " + ReportTitle1+"_" + System.DateTime.UtcNow.AddMinutes(330).ToString("yyyyMMdd");
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
        if (ddlCompany.SelectedIndex == 0 )
        {
            Error = 1;
            DisplayError = "Company must be selected ";
        }
        if (ddlItemCategory.Items.Count == 0)
        {
            Error = 1;
            DisplayError = "ItemCategory is Empty .. may be for selected Company No data avbl .. ";
        }

        if (Error == 1)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + DisplayError + "');", true);
        }
        return Error;
    }
    #endregion



}