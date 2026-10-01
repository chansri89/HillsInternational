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
using System.Collections;

public partial class AssignUserRoles : System.Web.UI.Page
{
    #region Declaration
    ProcessBus Bus = new ProcessBus(); LibFunctions Lib = new LibFunctions();
    List<EmployeeMasterMsg> EmployeeMasterMsgList = new List<EmployeeMasterMsg>();
    List<RoleMsg> AvailableRoleMsgList = new List<RoleMsg>();
    List<RoleMsg> AssignedRoleMsgList = new List<RoleMsg>();
    ArrayList List = new ArrayList();
    BaseClass BaseMsg = new BaseClass();
    UserAccess user = new UserAccess();
    public static string ProgramName = string.Empty;
    #endregion
    #region Events

    protected void Page_Load(object sender, EventArgs e)
    {
        ProgramName = System.IO.Path.GetFileName(Request.PhysicalPath);
        if (!Page.IsPostBack)
        {
            LoadUserName();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!user.HasPermission(ProgramName, UserPermission.CanCreate.ToString()))
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.CreatePermissionRestricted + "');", true);
        }
        else
        {
            ArrayList AryRoleId = new ArrayList();
            string RoleId = null;

            if (lstbxAssignedRole.Items.Count != 0)
            {
                foreach (ListItem LstTemp in lstbxAssignedRole.Items)
                {
                    RoleId = LstTemp.Value;
                    AryRoleId.Add(RoleId);
                }
                int Result = Bus.AdmUserRolesInsert(ddlUserName.SelectedValue, AryRoleId);
                if (Result == 0)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.SuccessFullySaved + "');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.RoleNotSaved + "');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.SelectRole + "');", true);
            }
        }
    }
    protected void btnMove_Click(object sender, EventArgs e)
    {
        if (lstbxAvailableRole.Items.Count != 0)
        {
            foreach (ListItem LstItem in lstbxAvailableRole.Items)
            {
                if (LstItem.Selected)
                {
                    lstbxAssignedRole.Items.Add(LstItem);
                    List.Add(LstItem);
                }
            }
        }
        foreach (ListItem ListValue in List)
        {
            lstbxAvailableRole.Items.Remove(ListValue);
        }
        lstbxAssignedRole.SelectedIndex = 0;
    }
    protected void btnMoveFull_Click(object sender, EventArgs e)
    {
        ArrayList ArTemp = new ArrayList();
        foreach (ListItem LstTemp in lstbxAvailableRole.Items)
        {
            ArTemp.Add(LstTemp);
        }
        lstbxAvailableRole.Items.Clear();
        if (null != ArTemp)
        {
            foreach (ListItem ListValue in ArTemp)
            {
                lstbxAssignedRole.Items.Add(ListValue);
            }
        }
        lstbxAssignedRole.SelectedIndex = 0;
    }
    protected void btnRemove_Click(object sender, EventArgs e)
    {
        if (lstbxAssignedRole.Items.Count != 0)
        {
            foreach (ListItem LstItem in lstbxAssignedRole.Items)
            {
                if (LstItem.Selected)
                {
                    lstbxAvailableRole.Items.Add(LstItem);
                    List.Add(LstItem);
                }
            }
        }
        foreach (ListItem ListValue in List)
        {
            lstbxAssignedRole.Items.Remove(ListValue);
        }
        lstbxAvailableRole.SelectedIndex = 0;
    }
    protected void btnRemoveFull_Click(object sender, EventArgs e)
    {
        ArrayList ArTemp = new ArrayList();
        foreach (ListItem LstTemp in lstbxAssignedRole.Items)
        {
            ArTemp.Add(LstTemp);
        }
        lstbxAssignedRole.Items.Clear();
        if (null != ArTemp)
        {
            foreach (ListItem ListValue in ArTemp)
            {
                lstbxAvailableRole.Items.Add(ListValue);
            }
        }
        lstbxAvailableRole.SelectedIndex = 0;
    }
    protected void btnExit_Click(object sender, EventArgs e)
    {

    }
    protected void ddlUserName_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<RoleMsg> RoleMsgList = new List<RoleMsg>();
        if (ddlUserName.SelectedIndex != 0)
        {
            EmployeeMasterMsg emp = new EmployeeMasterMsg();
            emp.EmployeeCode = ddlUserName.SelectedValue;
            RoleMsgList = Bus.AdmAvailandAssignerRolesSelect(emp);
            AvailableRoleMsgList = (from roleMsg in RoleMsgList
                                    where roleMsg.AsgRoleId.Equals(0)
                                    select roleMsg).ToList();
            LoadAvailableRoles(AvailableRoleMsgList);
            AssignedRoleMsgList = (from roleMsg in RoleMsgList
                                   where roleMsg.AsgRoleId > 0
                                   select roleMsg).ToList();
            LoadAssignedRoles(AssignedRoleMsgList);
            pnlAssignRoles.Visible = true;
        }
        else
        {
            pnlAssignRoles.Visible = false;
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('Select Role.');", true);
        }

    }

    #endregion
    #region Methods

    public void LoadUserName()
    {
        EmployeeMasterMsg emp = new EmployeeMasterMsg();
        emp.LoginEmployeeCode = BaseMsg.EmployeeCode;//added by sai 280714
        EmployeeMasterMsgList = Bus.EmployeeMasterSelect(emp);
        List<EmployeeMasterMsg> EmpList = Lib.LoadEmployeesOnUserRight(EmployeeMasterMsgList);
        if (EmpList != null && EmpList.Count > 0)
        {
            ddlUserName.DataSource = EmpList;
            ddlUserName.DataBind();
            ddlUserName.Items.Insert(0, "--Select Please--");
            ddlUserName.SelectedIndex = 0;
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('There is no Employee.');", true);
        }
    }

    public void LoadAvailableRoles(List<RoleMsg> RoleMsgList)
    {
        lstbxAvailableRole.DataSource = RoleMsgList;
        lstbxAvailableRole.DataBind();
    }

    public void LoadAssignedRoles(List<RoleMsg> RoleMsgList)
    {
        lstbxAssignedRole.DataSource = RoleMsgList;
        lstbxAssignedRole.DataBind();
    }

    #endregion
    #region Validation

    private int IsValidSave()
    {
        int Error = 0;
        return Error;
    }
    #endregion
    #region Clear
    public void AllClear()
    {
    }
    #endregion
  
}