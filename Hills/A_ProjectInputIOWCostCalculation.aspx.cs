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

public partial class A_ProjectInputIOWCostCalculation : System.Web.UI.Page
{
    ProcessBus Bus = new ProcessBus(); LibFunctions Lib = new LibFunctions();
    // CrystalDecisions.CrystalReports.Engine.ReportDocument oReport = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    BaseClass BaseMsg = new BaseClass();
    Validation Isvalid = new Validation();
    private static List<EmployeeMasterMsg> EmpList = new List<EmployeeMasterMsg>();
    //private static List<PFEStablishmentMsg> PFEList = new List<PFEStablishmentMsg>();/
    //public static List<GroupMsg> GList = new List<GroupMsg>();
    //public static List<SubGroupMsg> SGList = new List<SubGroupMsg>();
    //public static List<IOWHeadMsg> IOWHList = new List<IOWHeadMsg>();
    //DateTime frmDate;
    //DateTime toDate;
    private static List<CompanyMessage> CmpList = new List<CompanyMessage>();
    public static List<ClientMsg> ClList = new List<ClientMsg>();
    private static List<ProjectMsg> projList = new List<ProjectMsg>();
    Int32 WCompanyId;
    public static string ProgramName = string.Empty;
    //private static string TranType = "";
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

    protected void btnCalc_Click(object sender, EventArgs e)
    {
        if (IsView() == 0)
        {
            //System.Globalization.DateTimeFormatInfo dateinfo = new System.Globalization.DateTimeFormatInfo();
            //dateinfo.ShortDatePattern = "dd-MM-yyyy";
            ReportMsg rpt = new ReportMsg();
            rpt.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
            rpt.ForYearMonth = Convert.ToInt64(ddlForYearMonth.SelectedValue);
            rpt.ClientCode = ddlClient.SelectedValue;
            rpt.ProjectCode = ddlProject.SelectedValue;
            rpt.CreatedBy = BaseMsg.EmployeeCode;
            //rpt.Region = ddlRegion.SelectedValue; 
            string Result = Bus.A_ProjectInputIOWRateInsert(rpt);
            if(Result == "0")
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Calculation Done .." + "');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + Result + ".. Issue .." + "');", true);
            }
            
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
            WCompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
            LoadClient(WCompanyId);
            LoadForYearMonth();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Pls Select Company" + "');", true);
           
        }
    }
    protected void ddlClientChanged(object sender, EventArgs e)
    {
        if (ddlClient.SelectedIndex > 0 && ddlCompany.SelectedIndex > 0)
        {
            WCompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
            string WClientCode = ddlClient.SelectedValue;
            LoadProject(WCompanyId, WClientCode);

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Company and Client are to be Selected" + "');", true);
        }
    }
#endregion
    #region Methods
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
                // LoadProject();
                Int32 WCompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                LoadClient(WCompanyId);
                LoadForYearMonth();
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Company Data not Found" + "');", true);
            // ddlCompany.Items.Insert(0, "--Select Please--");
        }
    }
  
    //private void LoadCompany()
    //{
    //    CmpList = Bus.CompanyMasterSelect();

    //    if (CmpList != null && CmpList.Count > 0)
    //    {
    //        ddlCompany.DataSource = CmpList;
    //        ddlCompany.DataBind();
    //        ddlCompany.Items.Insert(0, "--Select Please--");
    //        ddlCompany.SelectedIndex = 0;
    //        if (CmpList.Count == 1)
    //        {
    //            ddlCompany.SelectedIndex = 1;

    //            //LoadRegion();
    //        }
    //    }
    //    else
    //    {
    //        ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Company Data not Found" + "');", true);
    //    }
    //}


    //private void LoadRegion() //scs241221 hills wants region to be part of calculation
    //{
    //        Int32 Wcompanyid = Convert.ToInt32(ddlCompany.SelectedValue);
    //        DataTable dt = new DataTable();
    //        dt = Bus.ItemRateYearMonthSelect(Wcompanyid);
    //        if (dt.Rows.Count > 0)
    //        {
    //            ddlRegion.DataSource = dt;
    //            ddlRegion.DataBind();
    //            ddlRegion.Items.Insert(0, "--Select Please--");
    //            ddlRegion.SelectedIndex = 0;
    //        }
    //        else
    //        {
    //            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Region data in Item Rate master is not Found" + "');", true);
    //        }
    //}
 
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
        //if (ddlRegion.SelectedIndex == 0)
        //{
        //    Error = 1;
        //    DisplayError = "Region must be selected ";
        //}
        //if (ddlMonth.SelectedIndex == 0)
        //{
        //    Error = 1;
        //    DisplayError = " Month must be selected.. ";
        //}
        //try
        //{
        //    Int32 wYear = Convert.ToInt32(txtYear.Text);
        //    if (wYear <= 2010)
        //    {
        //        Error = 1;
        //        DisplayError = " Year Should not be less than 2010 and must be a Positive integer.. ";
        //    }
        //}
        //catch
        //{
        //    Error = 1;
        //    DisplayError = " Year Data is in wrong formt and must be Integer .. ";
        //}

        if (Error == 1)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + DisplayError + "');", true);
        }
        return Error;
    }
    #endregion



}