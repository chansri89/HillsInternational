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

public partial class IOWCommonFactorMaster : System.Web.UI.Page
{
    #region Declaration
    ProcessBus Bus = new ProcessBus(); LibFunctions Lib = new LibFunctions();
    //List<EmployeeMasterMsg> EmpList = new List<EmployeeMasterMsg>();
    private static List<CRAMCommonFactorMsg> CommFactList = new List<CRAMCommonFactorMsg>();
    private static List<CompanyMessage> CmpList = new List<CompanyMessage>();
   
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
            
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        
        AllClear();
       // Pnlgv.Visible = false;
       // pnlAdd.Visible = false;
       // LoadGridCRAMCommonFactor(WCompanyId);
    }
    protected void ddlCompanyChanged(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedIndex > 0)
        {
            WCompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
            Pnlgv.Visible = true;
            pnlAdd.Visible = true;
            //LoadItemCategory();
            LoadGridCRAMCommonFactor(WCompanyId);
           
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Pls Select Company" + "');", true);
            Pnlgv.Visible = false;
            pnlAdd.Visible = false;
        }
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
        CmpList = Lib.LoadCompany(); //scs260325 modified the Lib function to do company select along with User company based on admin
        //CmpList = Lib.LoadCompanyOnUserRight(CmpList);
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
                //ddlCompany.Enabled = false;
                WCompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                LoadGridCRAMCommonFactor(WCompanyId);
                //LoadItemCategory();
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
     private void LoadGridCRAMCommonFactor(Int32 CompanyId)//To load data into grid.
    {
        CRAMCommonFactorMsg Cmp = new CRAMCommonFactorMsg();
        Cmp.Flag = "R";
        Cmp.CreatedBy = BaseMsg.EmployeeCode;
        Cmp.CompanyId = CompanyId;
        //Cmp.CRAMCommonFactorId =
        CommFactList = Bus.MasIOWCommonFactorInsertUpdate(Cmp);
        GrdIOWCommonFactor.DataSource = "";
        GrdIOWCommonFactor.DataSource = CommFactList;
        GrdIOWCommonFactor.DataBind();
        Pnlgv.Visible = true;
        pnlAdd.Visible = true;
       
    }

    private void CRAMCommonFactorSave()
    {
        WCompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
        CRAMCommonFactorMsg Cmp = new CRAMCommonFactorMsg();
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
        Cmp.IOWCommonFactor = txtIOWCommonFactor.Text.Trim();
        Cmp.SequenceNumber = Convert.ToInt32(txtSequenceNumber.Text.Trim());
        Cmp.FactorPercentage = Convert.ToDouble(txtFactorPercentage.Text.Trim());
        Cmp.EffectivePercentage = Convert.ToDouble(txtEffectivePercentage.Text.Trim());
        Cmp.CreatedBy = BaseMsg.EmployeeCode;
        CommFactList = Bus.MasIOWCommonFactorInsertUpdate(Cmp);
        //Output Dispay
        foreach (CRAMCommonFactorMsg cmp in CommFactList)
        {
            if (cmp.Result == "0")
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.SuccessFullySaved + "');", true);
                AllClear();
                //pnlPendind.Enabled = false;
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

    }
    #endregion
       
    }