using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Resources;

public partial class RoleNameChange : System.Web.UI.Page
{ 
    #region Declaration
    ProcessBus Bus = new ProcessBus();
    List<RoleNameChangeMsg> RoleList = new List<RoleNameChangeMsg>();
    List<RoleMsg> RoleMsgList = new List<RoleMsg>();
    BaseClass BaseMsg = new BaseClass();
    public static int DeleteIndex = 0;
    public static int UpdateIndex = 0;
    UserAccess user = new UserAccess();
    public static string ProgramName = string.Empty;
    #endregion
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        ProgramName = System.IO.Path.GetFileName(Request.PhysicalPath);
        if (!Page.IsPostBack)
        {
            LoadGrdRole();
            if (RoleMsgList.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.MsgForGrdnotLoad + "');", true);
                Pnlgv.Visible = false;
               
            }
        }
       
       
       
    }
  
    #endregion
    #region Methods
    private void LoadGrdRole()
    {
        RoleMsg Role = new RoleMsg();
        RoleMsgList = Bus.AdmRolesSelect();
        GrdRole.DataSource = "";
        GrdRole.DataSource = RoleMsgList;
        GrdRole.DataBind();
        if (!user.HasPermission(ProgramName, UserPermission.CanEdit.ToString()))
        {
            GrdRole.Columns[GrdRole.Columns.Count - 2].Visible = false;
        }
        if (!user.HasPermission(ProgramName, UserPermission.CanDelete.ToString()))
        {
            GrdRole.Columns[GrdRole.Columns.Count - 1].Visible = false;
        }
    }

   
    public void RoleUpdate()
    {
        GridViewRow row = GrdRole.Rows[UpdateIndex];
        RoleNameChangeMsg Role = new RoleNameChangeMsg();
        Role.Flag = "U";

        TextBox txtRoleId = (TextBox)row.FindControl("txtRoleId");
        TextBox txtRoleName = (TextBox)row.FindControl("txtRoleName");
       

        Role.RoleId = Convert.ToInt32(txtRoleId.Text.Trim());
        Role.RoleName = txtRoleName.Text.Trim();
        Role.CreatedBy = BaseMsg.EmployeeCode;
        string Result = Bus.AdmRoleUpdateSp(Role);
        GrdRole.EditIndex = -1;

        //foreach (RoleNameChangeMsg RoleUpdate in RoleList)
        //{
            if (Result == "0")
            {
                LoadGrdRole();
                return;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + Result + "');", true);
                return;
            }
        
    }
    public void RoleDelete()
    {
        RoleNameChangeMsg Role = new RoleNameChangeMsg();
        Role.Flag = "D";
        GridViewRow gvr = GrdRole.Rows[DeleteIndex];
        Label lblRoleId = (Label)gvr.FindControl("lblRoleId");
        Label lblRoleName = (Label)gvr.FindControl("lblRoleName");
       
        Role.RoleId = Convert.ToInt32(lblRoleId.Text.Trim());
        Role.RoleName = lblRoleName.Text.Trim();     
        Role.CreatedBy = BaseMsg.EmployeeCode;
        string Result = Bus.AdmRoleUpdateSp(Role);
        //foreach (SeriesMasterMsg SeriesDelete in SeriesList)
        //{
        if (Result == "0")
            {
                //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.SeriesDeletedSuccessfully + "');", true);
                LoadGrdRole();
                return;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + Result + "');", true);
                return;
            }
       
    }
    #endregion
    
    #region Validation
    
    private int IsValidGrid(string RoleName)
    {

        int Error = 0;
        string DisplayError = "";
        if ((RoleName.Trim() == "" || RoleName.Trim().ToString().Length == 0))
        {
            DisplayError = DisplayError + AgilerMail.ErrRolename;
            Error = 1;
        }
       
        if (Error == 1)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + DisplayError + "');", true);
        }
        return Error;
    }
    #endregion
    #region GrdEdit

    protected void GrdRole_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        UpdateIndex = e.RowIndex;
        GridViewRow row = GrdRole.Rows[UpdateIndex];
        string RoleName = ((TextBox)row.FindControl("txtRoleName")).Text.Trim();
        if (IsValidGrid(RoleName) == 0)
        {
            RoleUpdate();
        }
    }
   
    protected void GrdRole_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GrdRole.EditIndex = -1;
        LoadGrdRole();
    }
    protected void GrdRole_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GrdRole.EditIndex = e.NewEditIndex;
        LoadGrdRole();
        GridViewRow row = GrdRole.Rows[GrdRole.EditIndex];
        TextBox RoleName = (TextBox)row.FindControl("txtRoleName");
        RoleName.Focus();
    }

    #endregion
    protected void GrdRole_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DeleteIndex = e.RowIndex;
        RoleDelete();
    }
}