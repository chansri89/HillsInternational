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
using Ganini.Security;


public partial class EmployeeMaster : System.Web.UI.Page
{
    #region Declaration
    ProcessBus Bus = new ProcessBus();
    List<EmployeeMasterMsg> EmpList = new List<EmployeeMasterMsg>();
    List<CompanyMessage> CompList = new List<CompanyMessage>();
    public static int DeleteIndex = 0;
    public static int UpdateIndex = 0;
    UserAccess user = new UserAccess();
    public static string ProgramName = string.Empty;
    BaseClass BaseMsg = new BaseClass();
    public static string pass;
    KeyGen KeyGen = new KeyGen();
    #endregion
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        ProgramName = System.IO.Path.GetFileName(Request.PhysicalPath);
        if (!Page.IsPostBack)
       {
           LoadGridEmployees();
           LoadCompanyName();
           LoadManagerName();
           if (EmpList.Count == 0)
           {
               ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.MsgForGrdnotLoad + "');", true);
               Pnlgv.Visible = false;
               pnlAdd.Visible = true;

           }
        
       }
       
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
       // For Deleting a Record from Grid-Starts
        if (HidDeleteCount.Value == "1")
        {
            EmployeeDelete();
            HidDeleteCount.Value = "0";
            return;
        }

        ///For Deleting a Record from Grid-Ends
        ///For Updating a Record from Grid-Starts
        if (HidUpdateCount.Value == "1")
        {
            EmployeeUpdate();
            HidUpdateCount.Value = "0";
            return;
        }
        ///For Deleting a Record from Grid-Ends
        if (!user.HasPermission(ProgramName, UserPermission.CanCreate.ToString()))
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.CreatePermissionRestricted + "');", true);
        }
        else
        {
            if (IsValidSave() == 0)
            {
                EmployeeSave();
            }
        }
        
    }
    protected void GrdEmployeeMaster_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GrdEmployeeMaster.EditIndex = e.NewEditIndex;
        GridViewRow row = GrdEmployeeMaster.Rows[GrdEmployeeMaster.EditIndex];
        Label lblCompname = (Label)row.FindControl("lblCompanyName");
        Label lblManagername = (Label)row.FindControl("lblManagerName");
        EmployeeMasterMsg emp = new EmployeeMasterMsg();
        LoadGridEmployees();
        foreach (GridViewRow gvr in GrdEmployeeMaster.Rows)
        {
            if (gvr.RowIndex == GrdEmployeeMaster.EditIndex)
            {
                DropDownList ddlcmpname = (DropDownList)gvr.FindControl("ddlCompanyName");
                // ddlcmpname.Items.Insert(0, new ListItem("--Parent--"));
                foreach (EmployeeMasterMsg Emp in EmpList)
                {
                    if (Emp.CompanyName.Trim() == lblCompname.Text.Trim())
                    {
                        ddlcmpname.SelectedValue = Emp.CompanyCode;
                        ddlcmpname.Focus();
                        break;
                    }
                    ddlcmpname.SelectedIndex = 0;
                }
                DropDownList ddlmanagername = (DropDownList)gvr.FindControl("ddlManagerName");

               // EmpList = Bus.EmployeeMasterSelect(emp); scs 270714
                foreach (EmployeeMasterMsg EmpEdit in EmpList)
                {
                    if (EmpEdit.ManagerName.Trim() == lblManagername.Text.Trim())
                    {
                        ddlmanagername.SelectedValue = EmpEdit.ManagerCode.ToString();
                        break;
                    }
                }
                if (lblManagername.Text.Trim() == string.Empty)
                {
                    ddlmanagername.Items.Insert(0, new ListItem("--Select--", "0"));
                    ddlmanagername.SelectedIndex = 0;
                }
            }
        }
    }
    protected void GrdEmployeeMaster_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GrdEmployeeMaster.EditIndex = -1;
        LoadGridEmployees();
    }
    protected void GrdEmployeeMaster_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DeleteIndex = e.RowIndex;
        EmployeeDelete();
    }
    protected void GrdEmployeeMaster_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        UpdateIndex = e.RowIndex;
        GridViewRow row = GrdEmployeeMaster.Rows[UpdateIndex];
        string EmailId = ((TextBox)row.FindControl("txtEmailId")).Text.Trim();
        string ActivityDesg = ((TextBox)row.FindControl("txtEmployeeDesignation")).Text.Trim();
        string EmployeeCode = ((TextBox)row.FindControl("txtEmployeeCode")).Text.Trim();
        string MangerCode = ((DropDownList)row.FindControl("ddlManagerName")).Text.Trim();
        
        if (IsValidGrid(EmailId.Trim(), ActivityDesg.Trim(),EmployeeCode.Trim(),MangerCode.Trim()) == 0)
        {
            EmployeeUpdate();
        }
    }
    #endregion
    #region Methods
    public void LoadGridEmployees() //To load data into grid.
    {
        EmployeeMasterMsg Emp = new EmployeeMasterMsg();
        Emp.LoginEmployeeCode = BaseMsg.EmployeeCode; //scs 270714
        Emp.Flag = "R";
        EmpList = Bus.MasEmployeeInsertUpdateandDelete(Emp);
        LoadGrdEmp(EmpList);
    }
    public void LoadGrdEmp(List<EmployeeMasterMsg> EmpLst)
    {
        EmpLst = (from emplst in EmpLst where emplst.IsAuditor == false select emplst).ToList();
        GrdEmployeeMaster.DataSource = "";
        GrdEmployeeMaster.DataSource = EmpLst;
        GrdEmployeeMaster.DataBind();
        if (!user.HasPermission(ProgramName, UserPermission.CanEdit.ToString()))
        {
            GrdEmployeeMaster.Columns[GrdEmployeeMaster.Columns.Count - 2].Visible = false;
        }
        if (!user.HasPermission(ProgramName, UserPermission.CanDelete.ToString()))
        {
            GrdEmployeeMaster.Columns[GrdEmployeeMaster.Columns.Count - 1].Visible = false;
        }
    }
   
    
    private void LoadCompanyName()
    {
        EmployeeMasterMsg EmpMsg = new EmployeeMasterMsg();
        EmpMsg.EmployeeCode = BaseMsg.EmployeeCode;
        ddlCompanyName.DataSource = Bus.CompanyMasterSelect(EmpMsg);
        ddlCompanyName.DataTextField = "CompanyName";
        ddlCompanyName.DataValueField = "CompanyCode";
        ddlCompanyName.DataBind();
        ddlCompanyName.Items.Insert(0, "-- Select Please --");
    }
    private void LoadManagerName()
    {
        EmployeeMasterMsg EmpMsg = new EmployeeMasterMsg();
        ddlManagerName.DataSource = EmpList;
        //if (EmpMsg.IsAuditor == false)
        //{
        //    var ddlManagerNameList = (from Manager in EmpList
        //                              where Manager.IsAuditor == false
        //                              select new { Manager.EmployeeCode, Manager.EmployeeName }).ToList();
        //    ddlManagerName.DataSource = ddlManagerNameList;
        //}
        ddlManagerName.DataTextField = "EmployeeName";
        ddlManagerName.DataValueField = "EmployeeCode";
        ddlManagerName.DataBind();
        ddlManagerName.Items.Insert(0, "-- Select Please --");
    }
    public List<CompanyMessage> getCompanyName()
    {
        EmployeeMasterMsg Emp = new EmployeeMasterMsg();
        Emp.EmployeeCode = BaseMsg.EmployeeCode;
        List<CompanyMessage> CompList = new List<CompanyMessage>();
        CompList = Bus.CompanyMasterSelect(Emp);
        return CompList;

        //EmployeeMasterMsg Emp = new EmployeeMasterMsg();
        //Emp.EmployeeCode = BaseMsg.EmployeeCode;
        //CompList = Bus.CompanyMasterSelect(Emp);
        //return CompList;

    }
    public List<EmployeeMasterMsg> getManagerName()
    {
       
        return EmpList;

    }
   
    private void EmployeeSave()
    {
        EmployeeMasterMsg Emp = new EmployeeMasterMsg();
        Emp.Flag = "I";
        Emp.EmployeeCode = txtEmployeeCode.Text.Trim();
        Emp.LoginEmployeeCode =  BaseMsg.EmployeeCode; //scs to check this properly 270714
        Emp.EmployeeName = txtEmpname.Text.Trim();
        Emp.EmailId = txtEmailId.Text.Trim();
        Emp.Password = txtPassword.Text.Trim();
        Emp.Password = KeyGen.EncryptPwd(txtPassword.Text.Trim());// Encypt the password --scs150916
        Emp.CompanyCode = ddlCompanyName.SelectedItem.Value;
        Emp.EmployeeDesignation = txtEmployeeDesignation.Text.Trim();
        Emp.IsActive = chkIsActive.Checked;
        Emp.IsCompanyAdmin = ChkIsCompanyAdmin.Checked;
        Emp.IsAuditor = false; // scs 300914 auditor master provided ChkIsAuditor.Checked;  // sai 210714
        if (Emp.IsAuditor == true)
        {
            lblManagerName.Visible = false;
            ddlManagerName.Visible = false;
            Emp.ManagerCode = Emp.EmployeeCode;
            //Emp.ManagerName = Emp.EmployeeName;

        }
        else
        {
            lblManagerName.Visible = true;
            ddlManagerName.Visible = true;
            Emp.ManagerCode = ddlManagerName.SelectedItem.Value;
        }
        //Emp.CreatedBy = "Admin";
        Emp.CreatedBy = BaseMsg.EmployeeCode;  // sai 210714
        EmpList = Bus.MasEmployeeInsertUpdateandDelete(Emp);
        //Output Dispay
        foreach (EmployeeMasterMsg EmpMsg in EmpList)
        {
            if (EmpMsg.EmployeeResult == "0")
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.SuccessFullySaved + "');", true);
                //LoadGridEmployees();
                ClearAll();
                if (EmpList.Count > 0)
                {
                    Pnlgv.Visible = true;
                    pnlAdd.Visible = true;
                }

                break;

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + EmpMsg.EmployeeResult + "');", true);
                break;
            }
        }
    }
    public void EmployeeUpdate()
    {
        GridViewRow row = GrdEmployeeMaster.Rows[UpdateIndex];
        EmployeeMasterMsg Emp = new EmployeeMasterMsg();
        Emp.Flag = "U";
        TextBox txtEmpCode = (TextBox)row.FindControl("txtEmployeeCode");
        TextBox txtEmpname = (TextBox)row.FindControl("txtEmpname");
        TextBox txtEmailId = (TextBox)row.FindControl("txtEmailId");
        DropDownList ddlcmpname = (DropDownList)row.FindControl("ddlCompanyName");
        DropDownList ddlmanagername = (DropDownList)row.FindControl("ddlManagerName");
        TextBox txtActivityDesg = (TextBox)row.FindControl("txtEmployeeDesignation");
        CheckBox chkActive = (CheckBox)row.FindControl("chkIsActive");
        CheckBox chkCompanyAdmin = (CheckBox)row.FindControl("chkCompanyAdmin"); //scs270714
        //CheckBox chkIsAuditor = (CheckBox)row.FindControl("chkIsAuditor");

        Emp.EmployeeCode = txtEmpCode.Text.Trim();
        Emp.LoginEmployeeCode = BaseMsg.EmployeeCode; //scs check 270714
        Emp.EmployeeName = txtEmpname.Text.Trim();
        Emp.EmailId = txtEmailId.Text.Trim();
        Emp.CompanyCode = ddlcmpname.SelectedItem.Value;
        Emp.ManagerCode = ddlmanagername.SelectedItem.Value;
        Emp.EmployeeDesignation = txtActivityDesg.Text.Trim();
        Emp.IsActive = chkActive.Checked;
        Emp.IsCompanyAdmin = chkCompanyAdmin.Checked;
        Emp.IsAuditor = false; //scs300914 separet auditor master given chkIsAuditor.Checked;  // sai 210714
        //Emp.CreatedBy = "Admin";
        Emp.CreatedBy = BaseMsg.EmployeeCode;  // sai 210714
        Emp.Password = string.Empty;
        EmpList = Bus.MasEmployeeInsertUpdateandDelete(Emp);
        GrdEmployeeMaster.EditIndex = -1;

        foreach (EmployeeMasterMsg EmpUpdate in EmpList)
        {
            if (EmpUpdate.EmployeeResult == "0")
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.UpdatedSuccessfully + "');", true);
                ClearAll();
                break;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + EmpUpdate.EmployeeResult + "');", true);
                break;
            }
        }

    }
    public void EmployeeDelete()
    {
        EmployeeMasterMsg Emp = new EmployeeMasterMsg();
        Emp.Flag = "D";
        GridViewRow gvr = GrdEmployeeMaster.Rows[DeleteIndex];
        Label lblEmpCode = (Label)gvr.FindControl("lblEmployeeCode");
        Label lblEmpName = (Label)gvr.FindControl("lblEmployeeName");
        Label lblEmaiId = (Label)gvr.FindControl("lblEmailid");
        Label lblCompname = (Label)gvr.FindControl("lblCompanyName");
        Label lblManagername = (Label)gvr.FindControl("lblManagerName");
        Label lblAcitivityDesg = (Label)gvr.FindControl("lblEmployeeDesignation");

        Emp.EmployeeCode = lblEmpCode.Text.Trim();
        Emp.LoginEmployeeCode = BaseMsg.EmployeeCode; //scs check 270714
        Emp.EmployeeName = lblEmpName.Text.Trim();
        Emp.EmailId = lblEmaiId.Text.Trim();
        //Emp.CompanyCode = lblCompname.SelectedItem.Value;
        //Emp.ManagerCode = lblManagername.SelectedItem.Value;
        Emp.CompanyCode = "0";
        Emp.ManagerCode = "0";
        Emp.EmployeeDesignation = txtEmployeeDesignation.Text.Trim();
        //Emp.CreatedBy = "Admin";
        Emp.CreatedBy = BaseMsg.EmployeeCode;  // sai 210714
        Emp.Password = string.Empty;
        EmpList = Bus.MasEmployeeInsertUpdateandDelete(Emp);
        foreach (EmployeeMasterMsg EmpUpdate in EmpList)
        {
            if (EmpUpdate.EmployeeResult == "0")
            {
                //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.EmployeeDeletedSuccessfully + "');", true);
                ClearAll();
                break;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + EmpUpdate.EmployeeResult + "');", true);
                break;
            }
        }
    }
    #endregion
    #region Clear
    public void ClearAll()
    {
        txtEmployeeCode.Text = "";
        txtEmpname.Text = "";
        txtPassword.Text = "";
        txtEmailId.Text = "";
        txtEmployeeDesignation.Text = "";
        chkIsActive.Checked = false;
        ChkIsCompanyAdmin.Checked = false;
        ChkIsAuditor.Checked = false;
        LoadGrdEmp(EmpList); 
        //GrdEmployeeMaster.DataSource = "";
        //GrdEmployeeMaster.DataSource = EmpList;
        //GrdEmployeeMaster.DataBind();
        //if (!user.HasPermission(ProgramName, UserPermission.CanEdit.ToString()))
        //{
        //    GrdEmployeeMaster.Columns[GrdEmployeeMaster.Columns.Count - 2].Visible = false;
        //}
        //if (!user.HasPermission(ProgramName, UserPermission.CanDelete.ToString()))
        //{
        //    GrdEmployeeMaster.Columns[GrdEmployeeMaster.Columns.Count - 1].Visible = false;
        //}
        ////LoadGridEmployees(); SCS 270714
        LoadManagerName();
        LoadCompanyName();
    }
    #endregion
    #region Validation

    private int IsValidSave()
    {
        string DisplayErorr = "";
        int Error = 0;
        if (txtEmployeeCode.Text.Trim() == "" || Convert.ToInt32(txtEmployeeCode.Text.Length.ToString().Trim()) == 0)
        {
            DisplayErorr = DisplayErorr + AgilerMail.ErrEmployeeCode;
            //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.ErrEmployeeCode + "');", true);
            Error = 1;
        }
        if (txtEmpname.Text.Trim() == "" || Convert.ToInt32(txtEmpname.Text.Length.ToString().Trim()) == 0)
        {
            DisplayErorr = DisplayErorr + "-" + AgilerMail.ErrEmployeeName;
            //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.ErrEmployeeName + "');", true);
            Error = 1;
        }
        if (txtEmailId.Text.Trim() == "" || Convert.ToInt32(txtEmailId.Text.Length.ToString().Trim()) == 0)
        {
            DisplayErorr = DisplayErorr + "-" + AgilerMail.ErrEmailId;
            //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.ErrEmailId + "');", true);
            Error = 1;
        }
        //if (txtPassword.Text.Trim() == "" || Convert.ToInt32(txtPassword.Text.Length.ToString().Trim()) == 0)
        //{
        //    DisplayErorr = DisplayErorr + "-" + AgilerMail.ErrEmployeePassword;
        //    //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.ErrEmployeePassword + "');", true);
        //    Error = 1;
        //}
        if (ddlCompanyName.SelectedIndex==0)
        {
            DisplayErorr = DisplayErorr + "-" + AgilerMail.selectCompanyName;
            //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.ErrorCompanyName+ "');", true);
            Error = 1;
        }

        if (txtEmployeeDesignation.Text.Trim() == "" || Convert.ToInt32(txtEmployeeDesignation.Text.Length.ToString().Trim()) == 0)
        {
            DisplayErorr = DisplayErorr + "-" + AgilerMail.ErrEmployeeDesignation;
            //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.ErrEmployeeDesignation + "');", true);
            Error = 1;
        }
        if (txtPassword.Text.Trim() == "" || Convert.ToInt32(txtPassword.Text.Length.ToString().Trim()) == 0)
        {
            DisplayErorr = DisplayErorr + "-" + AgilerMail.ErrPassword;
            //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.ErrEmployeeDesignation + "');", true);
            Error = 1;
        }
        if (Error == 1)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + DisplayErorr + "');", true);
        }
        return Error;
    }
    private int IsValidGrid(string EmailId, string ActivityDesg,string EmployeeCode,string MangerCode)
    {

        int Error = 0;
        string DisplayError = "";
        //GridViewRow row = GrdEmployeeMaster.Rows[UpdateIndex];
        //CheckBox chkIsAuditor = (CheckBox)row.FindControl("chkIsAuditor");
        //bool IsAuditor = chkIsAuditor.Checked;

        if (EmailId.Trim() == "" || Convert.ToInt32(EmailId.Trim().ToString().Length) == 0)
        {
            DisplayError = DisplayError + AgilerMail.ErrEmailId;
            Error = 1;
        }

        //if (ActivityDesg.Trim() == "" || Convert.ToInt32(ActivityDesg.Trim().ToString().Length) == 0)
        //{
        //    DisplayError = DisplayError +"-"+ AgilerMail.ErrEmployeeDesignation;
        //    Error = 1;
        //}
        //if ((IsAuditor == true) && ((EmployeeCode.Trim().ToString()) != (MangerCode.Trim().ToString())))
        //{
        //    DisplayError = DisplayError + "-" + AgilerMail.ErrIsAuditorTrue;
        //    Error = 1;
        //}
        //if ((IsAuditor == false) && ((EmployeeCode.Trim().ToString()) == (MangerCode.Trim().ToString())))
        //{
        //    DisplayError = DisplayError + "-" + AgilerMail.ErrIsAuditorFalse;
        //    Error = 1;
        //}

        if (Error == 1)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + DisplayError + "');", true);
        }
        return Error;
    }
    #endregion
    protected void ChkIsAuditor_CheckedChanged(object sender, EventArgs e)
    {
        //pass = txtPassword.Text.Trim();
        Audtior();
        //txtPassword.Text = pass;
    }
    private void Audtior()
    {
        if (ChkIsAuditor.Checked == true)
        {
            lblManagerName.Visible = false;
            ddlManagerName.Visible = false;

        }
        else
        {
            lblManagerName.Visible = true;
            ddlManagerName.Visible = true;
        }
    }
}