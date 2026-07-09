using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Resources;
using System.Data;
using Microsoft.Reporting.WebForms;

//using Ganini.Lib;
//using System.Data.SqlClient;
//using System.Configuration;


public partial class rptTenderIOWMappingCost : System.Web.UI.Page
{ 
    #region Declaration
    ProcessBus Bus = new ProcessBus(); LibFunctions Lib = new LibFunctions();
    private static List<ProjectMsg> PMList = new List<ProjectMsg>();
    private static List<CompanyMessage> CmpList = new List<CompanyMessage>();
    public static List<ClientMsg> ClList = new List<ClientMsg>();
    private static List<ProjectMsg> projList = new List<ProjectMsg>();
    List<RateYearMsg> RYMList = new List<RateYearMsg>();
    private Int32 WCompanyId = 0;
    UserAccess user = new UserAccess();
    public static string ProgramName = string.Empty;
    BaseClass BaseMsg = new BaseClass();
    #endregion
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        ProgramName = System.IO.Path.GetFileName(Request.PhysicalPath);
        if (!Page.IsPostBack)
        {
            LoadCompany();
            //grdIOWSelectedddlForYearMonth.Enabled = false;
        }        
       
    }
    //protected void ddlCompanyChanged(object sender, EventArgs e)
    //{
    //    if (ddlCompany.SelectedIndex > 0)
    //    {
    //        WCompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
    //        LoadForYearMonth();
    //        LoadProject();
    //    }
    //    else
    //    {
    //        ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Location Not Selected.." + "');", true);
           
    //    }
    //}
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
        LoadRegion();
        LoadClient(WCompanyId);
        LoadForYearMonth();
    }
    protected void ddlClientChanged(object sender, EventArgs e)
    {
        if (ddlClient.SelectedIndex > 0 && ddlCompany.SelectedIndex > 0)
        {
            Int32 WCompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
            string WClientCode = ddlClient.SelectedValue;
            LoadProject(WCompanyId, WClientCode);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Company and Client are to be Selected" + "');", true);
        }
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        if (!user.HasPermission(ProgramName, UserPermission.CanCreate.ToString()))
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.CreatePermissionRestricted + "');", true);
        }
        else
        {
            if (IsValidSave() == 0)
            {
                LoadReport();
                
               
            }
        }
    }
    #endregion
    #region Methods
    private void LoadCompany()
    {
        //CmpList = Bus.CompanyMasterSelect();
        //if (BaseMsg.IsAdmin == false)
        //{
        //    CmpList = (from cmp in CmpList where cmp.CompanyId == BaseMsg.CompanyId select cmp).ToList();
        //}
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
                ddlCompany.Enabled = true;
                WCompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                LoadForYearMonth();
               // LoadProject();
                loadddlCompChanged(WCompanyId);
            }
            else
            {
                ddlCompany.Enabled = true;
            }

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.ErrCompanyData + "');", true);
            // ddlCompany.Items.Insert(0, "--Select Please--");
        }
       
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


    private void LoadClient(Int32 WCompanyId)
    {
        //ClientMsg Cmp = new ClientMsg();
        //Cmp.Flag = "R";
        //Cmp.CreatedBy = BaseMsg.EmployeeCode;
        //Cmp.CompanyId = WCompanyId;
        //ClList = Bus.MasClientInsertUpdateandDelete(Cmp);
        //ClList = (from Cl in ClList where Cl.ClientType.Trim().ToUpper() == "CLIENT" select Cl).ToList();
        ClList = Lib.LoadClient(WCompanyId);
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
            ddlClient.Items.Clear();
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Pls Create Clients for the selected Company" + "');", true);
        }

    }
    private void LoadProject(Int32 WCompanyId, string WClientCode)//To load data into grid.
    {
        projList = Bus.MasClientProjectSelect(WCompanyId);
        var proj = (from pj in projList where pj.ClientCode == WClientCode select new { pj.ClientProjectId, pj.ProjectName }).Distinct().ToList();
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

    //    var proj = (from pm in PMList where pm.CompanyId == CompId select new { pm.ClientProjectId, pm.ProjectName }).Distinct().ToList();
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
    private void LoadReport()
    {
        Int32 WcompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
        Int64 ClientprojectId = Convert.ToInt64(ddlProject.SelectedValue);
        //string IOWCode = "ALL";
        Int64 FromYearMonth = Convert.ToInt64(ddlFromYearMonth.SelectedValue);
        Int64 ToYearMonth = Convert.ToInt64(ddlToYearMonth.SelectedValue);
        string WRegion = ddlRegion.SelectedItem.ToString();
        string ReportTitle = ddlCompany.SelectedItem.ToString();
        //string ReportTitle1 = "";
        string WProject = "";// " Project: " + ddlProject.SelectedItem.ToString();
        ReportTitle = ReportTitle + WProject;   
        string RepPeriod = " - FM: " + ddlFromYearMonth.SelectedItem.ToString() + " - TO: " + ddlToYearMonth.SelectedItem.ToString() ;
        string RunDate = "RunDate :" + System.DateTime.UtcNow.AddMinutes(330).ToString("dd-MM-yyyy HH:mm");

        DataTable dt = null; DataTable dtOrig = null;
        if (rdbtnSummary.SelectedValue == "1")
        {
            dt = Bus.rptTenderCRAMIOWCostDetail(ClientprojectId, FromYearMonth, ToYearMonth, WRegion); // IOWMapping Cost Detail radio button 
        }
        else
        {
            dt = Bus.rptTenderIOWCostDetailListing(ClientprojectId, FromYearMonth, ToYearMonth, WRegion);
        }
       
        //string Error = "";
        ////Error = dtOrig.Rows[0][0].ToString();
        //if (Error != "Error")
        //{
        //    if (ddlRegion.SelectedIndex > 0)
        //    {
        //        IEnumerable<DataRow> filteredRows = dtOrig.AsEnumerable()
        //           .Where(row => row.Field<string>("Region") == ddlRegion.SelectedItem.ToString());

        //        dt = filteredRows.CopyToDataTable();
        //    }
        //    else
        //    {
        //        dt = dtOrig;
        //    }
        //}
        //else
        //{
        //    Error = Error + ".. OR";
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
            string ProjCode = "";
            string RepType = "";
            if (rdbtnSummary.SelectedValue == "1")
            {
                rep.ReportPath = reppath + "rptTenderCRAMIOWCostDetail.rdlc";
                RepType = "_Dtl_";
                ProjCode = dt.Rows[0][1].ToString();
            }
            else
            {
                rep.ReportPath = reppath + "rptTenderIOWCostDetailListing.rdlc";
                RepType = "_Sum_";
                ProjCode = dt.Rows[0][3].ToString();
            }

           

          //  this.ReportViewer1.LocalReport.DisplayName = " Tender Cost Listing for- " + ddlProject.SelectedItem.Text + "_" + System.DateTime.UtcNow.AddMinutes(330).ToString("yyyyMMdd");
            this.ReportViewer1.LocalReport.DisplayName = " Tender Cost Listing for- " + ProjCode + RepType + System.DateTime.UtcNow.AddMinutes(330).ToString("yyyyMMdd");
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

               
                //ReportParameter rp4 = new ReportParameter();
                //rp4.Name = "ReportTitle1";
                //rp4.Values.Add(ReportTitle1);
                //ReportViewer1.LocalReport.SetParameters(rp4);
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
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('"  + " Data not Available... May be Mapping Not done.." + "');", true);
            ReportViewer1.Visible = false;
        }

    }

    private void LoadForYearMonth()
    {
        if (ddlCompany.SelectedIndex > 0)
        {
            Int32 Wcompanyid = Convert.ToInt32(ddlCompany.SelectedValue);
            DataTable dt = new DataTable();
            DataTable dtAct = new DataTable();
            dtAct.Columns.Add("ForYearMonth", typeof(System.String));
            dt = Bus.CRAMForYearMonthSelect(Wcompanyid);
            Int64 WMaxYYYYmm = 0; // get the maximum year month from database
            //foreach (DataRow row in dt.Rows)
            //{
            //    Int64 Wyyyymm = Convert.ToInt64(row.Field<Int64>("ForYearMonth"));
            //    if (Wyyyymm > WMaxYYYYmm)
            //    {
            //        WMaxYYYYmm = Wyyyymm;
            //    }
            //}
            //DataRow dr = null;
            //Int64 NextYearMonth = 0;
            ////for (int i = 1; i < 2; i++)
            ////{


            //Int64 Wmm = WMaxYYYYmm % 100;
            //Int64 Wyyyy = WMaxYYYYmm / 100;
            //if (Wmm + 1 > 12)
            //{
            //    Wyyyy = Wyyyy + 1;
            //    NextYearMonth = Wyyyy * 100 + 1;
            //}
            //else
            //{
            //    NextYearMonth = Wyyyy * 100 + Wmm + 1;
            //}

            ////}
            //dr = dtAct.NewRow(); // have new row on each iteration
            //dr["ForYearMonth"] = NextYearMonth.ToString();
            //dtAct.Rows.Add(dr);

            //dr = dtAct.NewRow();
            //dr["ForYearMonth"] = WMaxYYYYmm.ToString();
            //dtAct.Rows.Add(dr);
            dtAct = dt;
            ddlFromYearMonth.DataSource = dtAct;
            ddlFromYearMonth.DataBind();
            ddlFromYearMonth.Items.Insert(0, "Select Pls");
            //if (dt.Rows.Count == 1)
            //{
            //    ddlFromYearMonth.SelectedIndex = 1;
            //}
            //else
            //{
                ddlFromYearMonth.SelectedIndex = 0;
            //}
            //------------
            ddlToYearMonth.DataSource = dtAct;
            ddlToYearMonth.DataBind();
            ddlToYearMonth.Items.Insert(0, "Select Pls");
            //if (dt.Rows.Count == 1)
            //{
            //    ddlToYearMonth.SelectedIndex = 1;
            //}
            //else
            //{
                ddlToYearMonth.SelectedIndex = 0;
            //}
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + " Pls Select Company " + "');", true);
        }
    }
    #endregion
    #region Clear
    private void AllClear()
    {

        ddlFromYearMonth.SelectedIndex = 0;
        ddlToYearMonth.SelectedIndex = 0;
        ddlCompany.SelectedIndex = 0;

    }
    #endregion
    #region Validation
    private int IsValidSave()
    {
        int Error = 0;
         string DisplayError = "";
         if (ddlCompany.SelectedIndex == 0 || ddlFromYearMonth.SelectedIndex == 0 || ddlToYearMonth.SelectedIndex == 0)
        {
            DisplayError=DisplayError+"Company and From and To Year Month are Mandatory..Pls Select..";
            Error = 1;
        }
         if (ddlClient.Items.Count == 0)
         {
             DisplayError = DisplayError + "For Selected Company No Client";
             Error = 1;
         }
         if (ddlFromYearMonth.SelectedIndex > 0 || ddlToYearMonth.SelectedIndex > 0)
         {
             try
             {
                 Int64 FmYYMM = Convert.ToInt64(ddlFromYearMonth.SelectedValue);
                 Int64 ToYYMM = Convert.ToInt64(ddlToYearMonth.SelectedValue);
                 if (FmYYMM > ToYYMM)
                 {
                     DisplayError = DisplayError + "From Year Month cannot be Greatedr Than To Year Month ..)";
                     Error = 1;
                 }
             }
             catch
             {
                 DisplayError = DisplayError + " Pls Select the Year Months ..)";
                 Error = 1;
             }
         }

         if (ddlProject.SelectedIndex == 0 )
         {
             DisplayError = DisplayError + "Pls Select the Project for Updation...";
             Error = 1;
         }
        if (Error == 1)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + DisplayError + "');", true);
        }
        return Error;
    }

 
    #endregion

}