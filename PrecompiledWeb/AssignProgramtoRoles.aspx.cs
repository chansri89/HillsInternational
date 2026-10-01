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

public partial class AssignProgramtoRoles : System.Web.UI.Page
{
    #region Declaration
    ProcessBus Bus = new ProcessBus();
    int RoleID = 0;
    public static int DeleteIndex = 0;
    public static int UpdateIndex = 0;
   public static List<RoleProgramsMsg> GRoleProgramsList = new List<RoleProgramsMsg>();
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
           LoadAssignProgramGrid(RoleID);
           lblMainmenu.Visible = false;
           ddlMainMenu.Visible = false;
       }
   }
    protected void rdbtnRole_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdbtnRole.SelectedIndex == 0) //New Role
        {
            //LoadAvailablePrograms(); //Load Available Programs into ListBox
            tdRoleName.Visible = true;
            tdRoleFields.Visible = true;
            txtNewRoleName.Visible = true;
            ddlRoleName.Visible = false;
            LoadAssignProgramGrid(0);
            GrdAssignProgam.Visible = true;
            btnSave.Visible = true;
            txtNewRoleName.Text = "";
            btnSave.Visible = true;
        }
        else if (rdbtnRole.SelectedIndex == 1) //Update Role
        {
            LoadRolesDropDown(); //Load Roles into DropDownListBox
            tdRoleName.Visible = true;
            tdRoleFields.Visible = true;
            txtNewRoleName.Visible = false;
            ddlRoleName.Visible = true;
            GrdAssignProgam.Visible = false;
            btnSave.Visible = false;
            GrdAssignProgam.HeaderRow.FindControl("ShowAllAccess").Visible = false;
            btnSave.Visible = false;
           
        }
    }
    protected void ddlRoleName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlRoleName.SelectedIndex != 0)
        {
            RoleID = Convert.ToInt32(ddlRoleName.SelectedValue);
            LoadAssignProgramGrid(RoleID);
            lblMainmenu.Visible = true;
            ddlMainMenu.Visible = true;
            LoadddlMainMenu(); // scs 230208 as requested by VJ for filter in Mainmenu
        }
        else
        {
            GrdAssignProgam.Visible = false;
            lblMainmenu.Visible = false;
            ddlMainMenu.Visible = false;
        }
        btnSave.Visible = false;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (rdbtnRole.SelectedIndex == 1)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.ErrEditMode + "');", true);
            return;
        }
        if (!user.HasPermission(ProgramName, UserPermission.CanCreate.ToString()))
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.CreatePermissionRestricted + "');", true);
        }
        else
        {
            if (IsValidSave() == 0)
            {
                ProgramAssignSave();
            }
        }
        ///For Deleting a Record from Grid-Ends
    }
    protected void ddlMainMenu_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadGridOnMainMenuSelectedIndexChanged();
    }
    #endregion
    #region GridEditing
    protected void GrdAssignProgam_RowEditing(object sender, GridViewEditEventArgs e)
    {
        if (!user.HasPermission(ProgramName, UserPermission.CanEdit.ToString()))
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.EditPermissionRestricted + "');", true);
        }
        else
        {
            GrdAssignProgam.EditIndex = e.NewEditIndex;
            LoadAssignProgramGrid(Convert.ToInt32(ddlRoleName.SelectedValue));
            //GrdAssignProgam.Columns[7].Visible = true;
            GridViewRow row = GrdAssignProgam.Rows[GrdAssignProgam.EditIndex];
            CheckBox EditAccess = (CheckBox)row.FindControl("chkEditAccess");           
            EditAccess.Focus();
        }
        btnSave.Visible = false;
    }
    protected void GrdAssignProgam_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GrdAssignProgam.EditIndex = -1;
        LoadAssignProgramGrid(Convert.ToInt32(ddlRoleName.SelectedValue));
        //GrdAssignProgam.Columns[7].Visible = false;
        btnSave.Visible = false;
    }
    protected void GrdAssignProgam_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (!user.HasPermission(ProgramName, UserPermission.CanDelete.ToString()))
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.DeletePermissionRestricted + "');", true);
        }
        else
        {
            DeleteIndex = e.RowIndex;
            ProgramAssignDelete();
        }
    }
    protected void GrdAssignProgam_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        UpdateIndex = e.RowIndex;
        ProgramAssignUpdate();
        GrdAssignProgam.EditIndex = -1;
        btnSave.Visible = false;
    }
    #endregion
    #region Methods
    private void LoadAssignProgramGrid(int RoleId)
    {
        //List<RoleProgramsMsg> RoleProgramsList = new List<RoleProgramsMsg>();
        GRoleProgramsList = Bus.AdmRoleProgramsSelect(RoleId);
        //LoadddlMainMenu(); 
        //LoadGrid(GRoleProgramsList); //SCS vj 230208
        LoadGridMenu(GRoleProgramsList);
    }
    private void LoadGridMenu(List<RoleProgramsMsg> RoleProgramsList)
    {
        
        if (ddlMainMenu.SelectedIndex <= 0)
        {
            LoadGrid(RoleProgramsList);
        }
        else
        {
            List<RoleProgramsMsg> RpList = new List<RoleProgramsMsg>();
            RpList = (from rp in RoleProgramsList where rp.MainMenu == ddlMainMenu.SelectedValue select rp).ToList();
            LoadGrid(RpList);
        }
    }
    public void LoadGrid(List<RoleProgramsMsg> RoleProgramsList)
    {
        if (RoleProgramsList != null && RoleProgramsList.Count > 0)
        {
            btnSave.Visible = true;
            GrdAssignProgam.DataSource = "";
            GrdAssignProgam.DataSource = RoleProgramsList;
            GrdAssignProgam.DataBind();
            GrdAssignProgam.Visible = true;
            GrdAssignProgam.Enabled = true;
            if (rdbtnRole.SelectedIndex == 0)
            {
                GrdAssignProgam.Columns[7].Visible = true;
                GrdAssignProgam.Columns[8].Visible = false;
                GrdAssignProgam.HeaderRow.FindControl("ShowAllAccess").Visible = true;
                GrdAssignProgam.HeaderRow.FindControl("lblAccess").Visible = false;
                GrdAssignProgam.HeaderRow.FindControl("ShowAllCreate").Visible = true;
                GrdAssignProgam.HeaderRow.FindControl("lblCreate").Visible = false;
                GrdAssignProgam.HeaderRow.FindControl("ShowAllEdit").Visible = true;
                GrdAssignProgam.HeaderRow.FindControl("lblEdit").Visible = false;
                GrdAssignProgam.HeaderRow.FindControl("ShowAllDelete").Visible = true;
                GrdAssignProgam.HeaderRow.FindControl("lblDelete").Visible = false;
            }
            else
            {
                GrdAssignProgam.HeaderRow.FindControl("ShowAllAccess").Visible = false;
                GrdAssignProgam.HeaderRow.FindControl("lblAccess").Visible = true;
                GrdAssignProgam.HeaderRow.FindControl("ShowAllCreate").Visible = false;
                GrdAssignProgam.HeaderRow.FindControl("lblCreate").Visible = true;
                GrdAssignProgam.HeaderRow.FindControl("ShowAllEdit").Visible = false;
                GrdAssignProgam.HeaderRow.FindControl("lblEdit").Visible = true;
                GrdAssignProgam.HeaderRow.FindControl("ShowAllDelete").Visible = false;
                GrdAssignProgam.HeaderRow.FindControl("lblDelete").Visible = true;
                GrdAssignProgam.Columns[7].Visible = false;
                GrdAssignProgam.Columns[8].Visible = true;
                if (!user.HasPermission(ProgramName, UserPermission.CanEdit.ToString()))
                {
                    GrdAssignProgam.Columns[GrdAssignProgam.Columns.Count - 1].Visible = false;
                    GrdAssignProgam.Enabled = false;
                }
            }
        }
    }

    private void LoadddlMainMenu()
    {
        var MainMenuList = (from prl in GRoleProgramsList select new { prl.MainMenu }).Distinct().ToList();
        ddlMainMenu.DataSource = MainMenuList;
        ddlMainMenu.DataBind();
        ddlMainMenu.Items.Insert(0, "ALL");
        ddlMainMenu.SelectedIndex = 0;
        txtMainMenu.Text = ddlMainMenu.SelectedValue;
    }
    private void LoadGridOnMainMenuSelectedIndexChanged()
    {
        txtMainMenu.Text = ddlMainMenu.SelectedValue;  
        if (ddlRoleName.SelectedIndex > 0)
        {
            LoadGridMenu(GRoleProgramsList);
            
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Role Needs To Be Selected For Grid display" + "');", true);
        }
    }
 
    
    public void LoadRolesDropDown()
    {
        List<RoleMsg> RolesList = new List<RoleMsg>();
        RolesList = Bus.AdmRolesSelect();
        if (RolesList!=null && RolesList.Count>0)
        {
            ddlRoleName.DataSource = RolesList;
            ddlRoleName.DataBind();
            ddlRoleName.Items.Insert(0, "--Select Please--");
            ddlRoleName.SelectedIndex = 0;
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.ErrRoles + "');", true);
            ddlRoleName.Items.Insert(0, "--Select Please--");
            ddlRoleName.SelectedIndex = 0;
            rdbtnRole.SelectedValue="1";
            tdRoleName.Visible = true;
            tdRoleFields.Visible = true;
            txtNewRoleName.Visible = true;
            ddlRoleName.Visible = false;
        }
    }


    public void ProgramAssignSave()
    {
        List<RoleProgramsMsg> RoleProgramMsgList = new List<RoleProgramsMsg>();
        
        foreach (GridViewRow gvr in GrdAssignProgam.Rows)
        {
            RoleProgramsMsg roleProgramMsg = new RoleProgramsMsg();
            roleProgramMsg.Flag = "I";
            roleProgramMsg.RoleId = 0;
            roleProgramMsg.RoleName = txtNewRoleName.Text;
            Label lblProgramID = (Label)gvr.FindControl("lblProgramId");
            roleProgramMsg.ProgramId = Convert.ToInt32(lblProgramID.Text);
            CheckBox chkAccessCnt = (CheckBox)gvr.FindControl("chkAccess");
            CheckBox chkCreateCnt = (CheckBox)gvr.FindControl("chkCreate");
            CheckBox chkEditCnt = (CheckBox)gvr.FindControl("chkEdit");
            CheckBox chkDeleteCnt = (CheckBox)gvr.FindControl("chkDelete");
            CheckBox chkPrintCnt = (CheckBox)gvr.FindControl("chkPrint");
            roleProgramMsg.CanAccess = chkAccessCnt.Checked;
            roleProgramMsg.CanCreate = chkCreateCnt.Checked;
            roleProgramMsg.CanEdit = chkEditCnt.Checked;
            roleProgramMsg.CanDelete = chkDeleteCnt.Checked;
            roleProgramMsg.CanPrint = chkPrintCnt.Checked;
            roleProgramMsg.CreatedBy = BaseMsg.EmployeeCode;
            RoleProgramMsgList.Add(roleProgramMsg);
        }
        GRoleProgramsList = Bus.AdmRoleProgramsInsertUpdateandDelete(RoleProgramMsgList);
       
        foreach (RoleProgramsMsg roles in GRoleProgramsList)
        {
            if (roles.Result == "0")
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.SuccessFullySaved + "');", true);

                AllClear();
               // LoadGrid(RoleProgramsList);
                LoadGridMenu(GRoleProgramsList);
                
                break;

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + roles.Result + "');", true);
                break;
            }
        }
    }
    public void ProgramAssignUpdate()
    {
        GridViewRow gvr = GrdAssignProgam.Rows[UpdateIndex];
        List<RoleProgramsMsg> RoleProgramMsgList = new List<RoleProgramsMsg>();
        RoleProgramsMsg roleProgramMsg = new RoleProgramsMsg();
        roleProgramMsg.Flag = "U";
        roleProgramMsg.RoleName = ddlRoleName.Text;
        roleProgramMsg.RoleId = Convert.ToInt32(ddlRoleName.SelectedValue);
        Label lblProgramID = (Label)gvr.FindControl("lblProgramId");
        roleProgramMsg.ProgramId = Convert.ToInt32(lblProgramID.Text);
        CheckBox chkAccessCnt = (CheckBox)gvr.FindControl("chkEditAccess");
        CheckBox chkCreateCnt = (CheckBox)gvr.FindControl("chkEditCreate");
        CheckBox chkEditCnt = (CheckBox)gvr.FindControl("chkModEdit");
        CheckBox chkDeleteCnt = (CheckBox)gvr.FindControl("chkEditDelete");
        CheckBox chkPrintCnt = (CheckBox)gvr.FindControl("chkEditPrint");
        roleProgramMsg.CanAccess = chkAccessCnt.Checked;
        roleProgramMsg.CanCreate = chkCreateCnt.Checked;
        roleProgramMsg.CanEdit = chkEditCnt.Checked;
        roleProgramMsg.CanDelete = chkDeleteCnt.Checked;
        roleProgramMsg.CanPrint = chkPrintCnt.Checked;
        roleProgramMsg.CreatedBy = BaseMsg.EmployeeCode;
        RoleProgramMsgList = Bus.AdmRoleProgramsInsertUpdateandDelete(roleProgramMsg);
        GRoleProgramsList = RoleProgramMsgList.ToList(); //scs 230208
        GrdAssignProgam.EditIndex = -1;
        foreach (RoleProgramsMsg roleProgram in RoleProgramMsgList)
        {
            if (roleProgram.Result == "0")
            {
                AllClear();
                //LoadGrid(RoleProgramMsgList);
                LoadGridMenu(GRoleProgramsList);
                GrdAssignProgam.EditIndex = -1;
                break;

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + roleProgram.Result + "');", true);
                break;
            }
        }
    }

 

    #endregion
    #region Delete function not used

    public void ProgramAssignDelete()
    {
        GridViewRow gvr = GrdAssignProgam.Rows[DeleteIndex];
        List<RoleProgramsMsg> RoleProgramMsgList = new List<RoleProgramsMsg>();
        RoleProgramsMsg roleProgramMsg = new RoleProgramsMsg();
        roleProgramMsg.Flag = "U";
        roleProgramMsg.RoleName = ddlRoleName.Text;
        roleProgramMsg.RoleId = Convert.ToInt32(ddlRoleName.SelectedValue);
        Label lblProgramID = (Label)gvr.FindControl("lblProgramId");
        roleProgramMsg.ProgramId = Convert.ToInt32(lblProgramID.Text);
        CheckBox chkAccessCnt = (CheckBox)gvr.FindControl("chkAccess");
        CheckBox chkCreateCnt = (CheckBox)gvr.FindControl("chkCreate");
        CheckBox chkEditCnt = (CheckBox)gvr.FindControl("chkEdit");
        CheckBox chkDeleteCnt = (CheckBox)gvr.FindControl("chkDelete");
        CheckBox chkPrintCnt = (CheckBox)gvr.FindControl("chkPrint");
        roleProgramMsg.CanAccess = chkAccessCnt.Checked;
        roleProgramMsg.CanCreate = chkCreateCnt.Checked;
        roleProgramMsg.CanEdit = chkEditCnt.Checked;
        roleProgramMsg.CanDelete = chkDeleteCnt.Checked;
        roleProgramMsg.CanPrint = chkPrintCnt.Checked;
        roleProgramMsg.CreatedBy = BaseMsg.EmployeeCode;
        RoleProgramMsgList = Bus.AdmRoleProgramsInsertUpdateandDelete(roleProgramMsg);
        GRoleProgramsList = RoleProgramMsgList.ToList();
        foreach (RoleProgramsMsg roleProgram in RoleProgramMsgList)
        {
            if (roleProgram.Result == "0")
            {
                AllClear();
                // LoadGrid(RoleProgramMsgList);
                LoadGridMenu(RoleProgramMsgList);
                break;

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + roleProgram.Result + "');", true);
                break;
            }
        }
    }

    #endregion
    
    #region Validation

    private int IsValidSave()
    {
        int Error = 0;
        if (txtNewRoleName.Text.Trim() == "" || Convert.ToInt32(txtNewRoleName.Text.Length.ToString().Trim()) == 0)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('Please Enter RoleName');", true);
            Error = 1;
        }
        return Error;
    }
    #endregion

    #region Clear
    public void AllClear()
    {
        txtNewRoleName.Text = "";
    }
    #endregion

    protected void GrdAssignProgam_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        CheckBox chkAccessCnt = new CheckBox();
        CheckBox chkCreateCnt = new CheckBox();
        CheckBox chkEditCnt = new CheckBox();
        CheckBox chkDeleteCnt = new CheckBox();
        CheckBox chkPrintCnt = new CheckBox();
        if (e.CommandName == "SelectAll")
        {
            int WRowIndex = Convert.ToInt32(e.CommandArgument.ToString());
            GridViewRow gvr = GrdAssignProgam.Rows[WRowIndex];
            if (rdbtnRole.SelectedIndex == 0)
            {
                chkAccessCnt = (CheckBox)gvr.FindControl("chkAccess");
                chkCreateCnt = (CheckBox)gvr.FindControl("chkCreate");
                chkEditCnt = (CheckBox)gvr.FindControl("chkEdit");
                chkDeleteCnt = (CheckBox)gvr.FindControl("chkDelete");
                chkPrintCnt = (CheckBox)gvr.FindControl("chkPrint");
            }
            else
            {
                chkAccessCnt = (CheckBox)gvr.FindControl("chkEditAccess");
                chkCreateCnt = (CheckBox)gvr.FindControl("chkEditCreate");
                chkEditCnt = (CheckBox)gvr.FindControl("chkModEdit");
                chkDeleteCnt = (CheckBox)gvr.FindControl("chkEditDelete");
                chkPrintCnt = (CheckBox)gvr.FindControl("chkEditPrint");
            }
            chkAccessCnt.Checked = true;
            chkCreateCnt.Checked = true;
            chkEditCnt.Checked = true;
            chkDeleteCnt.Checked = true;
            chkPrintCnt.Checked = true;
            CheckBox Access = (CheckBox)gvr.FindControl("chkAccess");
            Access.Focus();
        }
    }
    protected void GrdAssignProgam_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (rdbtnRole.SelectedIndex == 1)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowState != DataControlRowState.Edit)
            {
                if (((CheckBox)e.Row.FindControl("chkAccess")) != null)
                {

                    ((CheckBox)e.Row.FindControl("chkAccess")).Enabled = false;
                    ((CheckBox)e.Row.FindControl("chkCreate")).Enabled = false;
                    ((CheckBox)e.Row.FindControl("chkEdit")).Enabled = false;
                    ((CheckBox)e.Row.FindControl("chkDelete")).Enabled = false;
                    ((CheckBox)e.Row.FindControl("chkPrint")).Enabled = false;
                }
            }
        }
    }
}