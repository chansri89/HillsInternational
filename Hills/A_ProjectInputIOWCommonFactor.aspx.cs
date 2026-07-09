using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Ganini.Lib;
using Resources;

public partial class A_ProjectInputIOWCommonFactor : System.Web.UI.Page
{
    #region Declaration
    ProcessBus Bus = new ProcessBus(); LibFunctions Lib = new LibFunctions();
    //List<EmployeeMasterMsg> EmpList = new List<EmployeeMasterMsg>();
    private static List<ProjectInputCRAMCommonFactorMsg> CommFactList = new List<ProjectInputCRAMCommonFactorMsg>();
    private static List<CompanyMessage> CmpList = new List<CompanyMessage>();
    public static List<ClientMsg> ClList = new List<ClientMsg>();
    private static List<ProjectMsg> projList = new List<ProjectMsg>();
    //public static int DeleteIndex = 0;
    //public static int UpdateIndex = 0;
    UserAccess user = new UserAccess();
    public static string ProgramName = string.Empty;
    BaseClass BaseMsg = new BaseClass();
    private Int32 WCompanyId = 0;
    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        ProgramName = System.IO.Path.GetFileName(Request.PhysicalPath);
        if (!Page.IsPostBack)
        {
            Pnlgv.Visible = false;
            pnlAdd.Visible = false;
            LoadCompany();
            Enableddls();
            
        }
    }
 
    protected void ddlCompanyChanged(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedIndex > 0)
        {
            WCompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
            //Pnlgv.Visible = true;
            //pnlAdd.Visible = true;
            //LoadItemCategory();
            LoadClient(WCompanyId);
            LoadForYearMonth();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Pls Select Company" + "');", true);
            Pnlgv.Visible = false;
            pnlAdd.Visible = false;
        }
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
    protected void btnView_Click(object sender, EventArgs e)
    {
        if (IsView() == 0)
        {
            Disableddls();
            WCompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
            LoadGridCRAMCommonFactor(WCompanyId);
            Pnlgv.Visible = true;
            pnlAdd.Visible = true;
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Pnlgv.Visible = false;
        pnlAdd.Visible = false;
        pnlPendind.Visible = true;
        Enableddls();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text.ToUpper() == "SAVE")
        {
            if (!user.HasPermission(ProgramName, UserPermission.CanCreate.ToString()))
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.CreatePermissionRestricted + "');", true);
            }
            else
            {

                if (IsValidSave() == 0)
                {
                    CRAMCommonFactorSave();
                }
            }
        }
        else if (btnSave.Text.ToUpper() == "UPDATE")
        {
            if (!user.HasPermission(ProgramName, UserPermission.CanEdit.ToString()))
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.EditPermissionRestricted + "');", true);
                AllClear();
            }
            else
            {

                if (IsValidSave() == 0)
                {
                    CRAMCommonFactorSave();
                }
            }
        }
        else
        {
        }
    }
  
    //protected void btnFilter_Click(object sender, EventArgs e)
    //{
    //   // CRAMCommonFactorFilter();
    //    Int32 WCompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
    //    LoadGridCRAMCommonFactor(WCompanyId);
    //}
    #region GridEditing
    protected void GrdIOWCommonFactor_RowCommand(object sender, GridViewCommandEventArgs e)
    {
     
        int WCRAMCommonFactorId = Convert.ToInt32(e.CommandArgument.ToString());
        foreach (GridViewRow row in GrdIOWCommonFactor.Rows)
        {
            if (((Label)row.FindControl("lblIOWCommonFactorId")).Text == WCRAMCommonFactorId.ToString())
            {
                txtIOWCommonFactorId.Text = ((Label)row.FindControl("lblIOWCommonFactorId")).Text;
                txtIOWCommonFactor.Text = ((Label)row.FindControl("lblIOWCommonFactor")).Text;
                txtSequenceNumber.Text = ((Label)row.FindControl("lblSequenceNumber")).Text;
                txtFactorPercentage.Text = ((Label)row.FindControl("lblFactorPercentage")).Text;
                txtEffectivePercentage.Text = ((Label)row.FindControl("lblEffectivePercentage")).Text;
                chkIsActive.Checked = ((CheckBox)row.FindControl("chkActive")).Checked;
               // chkIsActive.Visible = true;
                btnSave.Text = "Update";
                Pnlgv.Visible = false;
                pnlAdd.Visible = true;
                pnlAdd.GroupingText = "Update Common Factor";
                break;
            }
            
        }

        

    }

    #endregion
    #endregion
    #region Methods
    private void LoadCompany()
    {
        //CmpList = Bus.CompanyMasterSelect();
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
    private void loadddlCompChanged(Int32 WCompanyId)
    {
        LoadClient(WCompanyId);

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

     private void LoadGridCRAMCommonFactor(Int32 CompanyId)//To load data into grid.
    {
        ProjectInputCRAMCommonFactorMsg Cmp = new ProjectInputCRAMCommonFactorMsg();
        Cmp.Flag = "R";
        Cmp.CreatedBy = BaseMsg.EmployeeCode;
        Cmp.CompanyId = CompanyId;
         Cmp.ClientCode = ddlClient.SelectedValue;
         Cmp.ProjectCode = ddlProject.SelectedValue;
         Cmp.ForYearMonth = Convert.ToInt64(ddlForYearMonth.SelectedValue);

        //Cmp.CRAMCommonFactorId =
        CommFactList = Bus.ProjectInputIOWCommonFactorInsertUpdate(Cmp);
        if (CommFactList.Count > 0)
        {
            GrdIOWCommonFactor.DataSource = "";
            GrdIOWCommonFactor.DataSource = CommFactList;
            GrdIOWCommonFactor.DataBind();
            Pnlgv.Visible = true;
            pnlAdd.Visible = true;
            Disableddls();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Check if Common Factor Data is Available.." + "');", true);
        }
       
    }

    private void CRAMCommonFactorSave()
    {
        WCompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
        ProjectInputCRAMCommonFactorMsg Cmp = new ProjectInputCRAMCommonFactorMsg();
        if (btnSave.Text.ToUpper() == "SAVE")
        {
            Cmp.Flag = "I";
            Cmp.IOWCommonFactorId = 0;
            Cmp.IsActive = true;
        }
        else
        {
            Cmp.Flag = "U";
            Cmp.IsActive = chkIsActive.Checked;
            Cmp.IOWCommonFactorId = Convert.ToInt32(txtIOWCommonFactorId.Text);
        }
        Cmp.CompanyId = WCompanyId;
        Cmp.ClientCode = ddlClient.SelectedValue;
        Cmp.ProjectCode = ddlProject.SelectedValue;
        Cmp.ForYearMonth = Convert.ToInt64(ddlForYearMonth.SelectedValue);
        Cmp.IOWCommonFactor = txtIOWCommonFactor.Text.Trim();
        Cmp.SequenceNumber = Convert.ToInt32(txtSequenceNumber.Text.Trim());
        Cmp.FactorPercentage = Convert.ToDouble(txtFactorPercentage.Text.Trim());
        Cmp.EffectivePercentage = Convert.ToDouble(txtEffectivePercentage.Text.Trim());
        Cmp.CreatedBy = BaseMsg.EmployeeCode;
        CommFactList = Bus.ProjectInputIOWCommonFactorInsertUpdate(Cmp);
        //Output Dispay
        foreach (ProjectInputCRAMCommonFactorMsg cmp in CommFactList)
        {
            if (cmp.Result == "0")
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.SuccessFullySaved + "');", true);
                AllClear();
                btnSave.Text = "Save";
                break;

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + cmp.Result + "');", true);
                break;
            }
        }
    }
    private void Disableddls()
    {
        ddlCompany.Enabled = false;
        ddlClient.Enabled = false;
        ddlProject.Enabled = false;
        ddlForYearMonth.Enabled = false;
    }
    private void Enableddls()
    {
        ddlCompany.Enabled = true;
        ddlClient.Enabled = true;
        ddlProject.Enabled = true;
        ddlForYearMonth.Enabled = true;
    }
    #endregion
    #region Validation

    private int IsValidSave()
    {
        int Error = 0;
        string DisplayError = "";
        if (txtIOWCommonFactor.Text.Trim() == "" )
        {
            DisplayError = DisplayError + " IOW Common Factor is Mandatory .";
            Error = 1;
        }
        try
        {
            double commFact = Convert.ToDouble(txtFactorPercentage.Text);
            double EffFact = Convert.ToDouble(txtEffectivePercentage.Text);
            if (commFact> EffFact)
            {
                DisplayError = DisplayError + " Effective Percentage should be equal to or morethan factor Percentage  .";
                Error = 1;
            }
        }
        catch
        {
            DisplayError = DisplayError + " Factor Percentage and Effective Percentage should be numeric .";
            Error = 1;
        }

        try
        {
            
            int seq = Convert.ToInt32(txtSequenceNumber.Text);
            if (txtSequenceNumber.Text.Length != 3)
            {
                DisplayError = DisplayError + " Sequence Number should be Minimum and Maximum 3 digits .";
                Error = 1;
            }
            else
            {
                foreach (GridViewRow gvr in GrdIOWCommonFactor.Rows)
                {
                    string wCommfactId = ((Label)gvr.FindControl("lblIOWCommonFactorId")).Text;
                    if (txtSequenceNumber.Text == ((Label)gvr.FindControl("lblSequenceNumber")).Text && txtIOWCommonFactorId.Text != wCommfactId)
                    {
                        DisplayError = DisplayError + " Duplicate Sequence Number...";
                        Error = 1;
                        break;
                    }
                }
            }
        }
        catch
        {
            DisplayError = DisplayError + " Sequence Number should be Integer .";
            Error = 1;
        }
        //if (ddlItemCategory.SelectedIndex ==0)
        //{
        //    DisplayError = DisplayError + "--" + " Item Category Has to be selected . ";
        //    Error = 1;
        //}
        
        if (Error == 1)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + DisplayError + "');", true);
        }
        return Error;
    }
 
    private int IsView()
    {

        System.Globalization.DateTimeFormatInfo dateinfo = new System.Globalization.DateTimeFormatInfo();
        dateinfo.ShortDatePattern = "dd/MM/yyyy";
        int Error = 0;
        string DisplayError = "";
        if (ddlCompany.SelectedIndex == 0)
        {
            Error = 1;
            DisplayError = "Company must be selected ";
        }
        if (ddlClient.SelectedIndex == 0)
        {
            Error = 1;
            DisplayError = "Client must be selected ";
        }
        if (ddlProject.SelectedIndex == 0)
        {
            Error = 1;
            DisplayError = "Client Project must be selected ";
        }
        if (ddlForYearMonth.SelectedIndex == 0)
        {
            Error = 1;
            DisplayError = "Year Month must be selected ";
        }

        if (Error == 1)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + DisplayError + "');", true);
        }
        return Error;
    }
    
 

    #endregion
    #region Clear
    public void AllClear()
    {
        txtIOWCommonFactorId.Text = "";
        txtIOWCommonFactor.Text = "";
        //ddlItemCategory.SelectedIndex = 0;
        txtEffectivePercentage.Text = "";
        txtFactorPercentage.Text = "";
        txtSequenceNumber.Text = "";
        Pnlgv.Visible = true;
        pnlAdd.Visible = true;
        pnlPendind.Enabled = true;
        LoadGridCRAMCommonFactor(Convert.ToInt32(ddlCompany.SelectedValue)); //SCS 210301
        pnlPendind.Enabled = true;
        chkIsActive.Visible = false;
        pnlAdd.GroupingText = "Add Common Factor";
        Enableddls();
    }
    #endregion
       
    }