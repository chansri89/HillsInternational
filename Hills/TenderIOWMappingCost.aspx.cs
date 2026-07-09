using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Resources;
using System.Data;

public partial class TenderIOWMappingCost : System.Web.UI.Page
{ 
    #region Declaration
    ProcessBus Bus = new ProcessBus(); LibFunctions Lib = new LibFunctions();
    private static List<ProjectMsg> PMList = new List<ProjectMsg>();
    private static List<CompanyMessage> CmpList = new List<CompanyMessage>();
    private static List<ClientMsg> ClientList = new List<ClientMsg>();
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
    protected void ddlCompanyChanged(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedIndex > 0)
        {
            WCompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
            LoadForYearMonth();
           // LoadProject();
            WCompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
            LoadClient(WCompanyId);

            LoadRegion();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Location Not Selected.." + "');", true);
           
        }
    }
    protected void ddlClientChanged(object sender, EventArgs e)
    {
        if (ddlClient.SelectedIndex > 0 && ddlCompany.SelectedIndex > 0)
        {
           
            LoadProject();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Company and Client are to be Selected" + "');", true);
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (!user.HasPermission(ProgramName, UserPermission.CanCreate.ToString()))
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.CreatePermissionRestricted + "');", true);
        }
        else
        {
            if (IsValidSave() == 0)
            {
                Int32 WcompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                Int64 ClientprojectId = Convert.ToInt64(ddlProject.SelectedValue);
                //string IOWCode = "ALL";
                Int64 YearMonth = Convert.ToInt64(ddlForYearMonth.SelectedValue);
                string Result = Bus.TenderIOWMappingCost( WcompanyId, ClientprojectId, YearMonth, ddlRegion.SelectedValue);
                if (Result == "0")
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Tender Cost Computed .." + "');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + Result + "');", true);
                }
            }
        }
    }
    #endregion
    #region Methods
   private void LoadProject()//To load data into grid.
    {
        Int32 WCompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
        string WClientCode = ddlClient.SelectedValue;
       
        projList = Bus.MasClientProjectSelect(WCompanyId);
        var proj = (from pj in projList where pj.ClientCode == WClientCode select new { pj.ClientProjectId, pj.ProjectName }).Distinct().ToList();
        ddlProject.DataSource = proj;
        ddlProject.DataBind();
        ddlProject.Items.Insert(0, "-- Select Please --");
    }
 
    private void LoadClient(Int32 WCompanyId)
    {
        ClientMsg Cmp = new ClientMsg();
        Cmp.Flag = "R";
        Cmp.CreatedBy = BaseMsg.EmployeeCode;
        Cmp.CompanyId = WCompanyId;
        ClientList = Bus.MasClientInsertUpdateandDelete(Cmp);
        ClientList = (from Cl in ClientList where Cl.ClientType.Trim().ToUpper() == "CLIENT" select Cl).ToList();
        ddlClient.DataSource = ClientList;
        ddlClient.DataBind();
        ddlClient.Items.Insert(0, "-- Select Please --");
        ddlClient.Enabled = true;
        if (ClientList.Count > 1)
        {
            //just Leave
        }
        else if (ClientList.Count == 1)
        {
            ddlClient.SelectedIndex = 1;
            string WClientCode = ddlClient.SelectedValue;
            LoadProject();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Pls Create Clients for the selected Company" + "');", true);
        }

    }

    private void LoadCompany()
    {
        //CmpList = Bus.CompanyMasterSelect();
        CmpList = Lib.LoadCompany();
        //if (BaseMsg.IsAdmin == false)
        //{
        //    CmpList = (from cmp in CmpList where cmp.CompanyId == BaseMsg.CompanyId select cmp).ToList();
        //}
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
                LoadClient(WCompanyId);
               // LoadProject();
                LoadRegion();
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
    private void LoadRegion() //scs241221 hills wants region to be part of calculation
    {
        Int32 Wcompanyid = Convert.ToInt32(ddlCompany.SelectedValue);
        DataTable dt = new DataTable();
        dt = Bus.ItemRateYearMonthSelect(Wcompanyid);
        if (dt.Rows.Count > 0)
        {
            ddlRegion.DataSource = dt;
            ddlRegion.DataBind();
            ddlRegion.Items.Insert(0, "--Select Please--");
            ddlRegion.SelectedIndex = 0;
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Region data in Item Rate master is not Found" + "');", true);
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
            foreach (DataRow row in dt.Rows)
            {
                Int64 Wyyyymm = Convert.ToInt64(row.Field<Int64>("ForYearMonth"));
                if (Wyyyymm > WMaxYYYYmm)
                {
                    WMaxYYYYmm = Wyyyymm;
                }
            }
            DataRow dr = null;
            Int64 NextYearMonth = 0;
            //for (int i = 1; i < 2; i++)
            //{


            Int64 Wmm = WMaxYYYYmm % 100;
            Int64 Wyyyy = WMaxYYYYmm / 100;
            if (Wmm + 1 > 12)
            {
                Wyyyy = Wyyyy + 1;
                NextYearMonth = Wyyyy * 100 + 1;
            }
            else
            {
                NextYearMonth = Wyyyy * 100 + Wmm + 1;
            }

            //}
            dr = dtAct.NewRow(); // have new row on each iteration
            dr["ForYearMonth"] = NextYearMonth.ToString();
            dtAct.Rows.Add(dr);

            dr = dtAct.NewRow();
            dr["ForYearMonth"] = WMaxYYYYmm.ToString();
            dtAct.Rows.Add(dr);
            // load current active year month and next month
            ddlForYearMonth.DataSource = dtAct;
            ddlForYearMonth.DataBind();
            ddlForYearMonth.Items.Insert(0, "Select Pls");
            //if (dt.Rows.Count == 1)
            //{
            //    ddlForYearMonth.SelectedIndex = 1;
            //}
            //else
            //{
                ddlForYearMonth.SelectedIndex = 0;
           // }
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

        ddlForYearMonth.SelectedIndex = 0;
        ddlCompany.SelectedIndex = 0;

    }
    #endregion
    #region Validation
    private int IsValidSave()
    {
        int Error = 0;
         string DisplayError = "";
         if (ddlCompany.SelectedIndex==0 || ddlForYearMonth.SelectedIndex==0)
        {
            DisplayError=DisplayError+"Company and Rate Year Month are Mandatory..Pls Select..";
            Error = 1;
        }
         if (ddlProject.SelectedIndex == 0 )
         {
             DisplayError = DisplayError + "Pls Select the Project for Updation...";
             Error = 1;
         }
         if (ddlRegion.SelectedIndex == 0)
         {
             Error = 1;
             DisplayError = "Region must be selected ";
         }
        if (Error == 1)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + DisplayError + "');", true);
        }
        return Error;
    }

 
    #endregion

}