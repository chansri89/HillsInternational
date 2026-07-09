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


public partial class LoginUser : System.Web.UI.Page
{
    #region Declaration
    ProcessBus Bus = new ProcessBus(); LibFunctions lib = new LibFunctions();
    List<EmployeeMasterMsg> EmpList = new List<EmployeeMasterMsg>();
    private static List<CompanyMessage> CompList = new List<CompanyMessage>();
    public static List<LoginUserMsg> LUList = new List<LoginUserMsg>();
    public static List<DepartmentMsg> DepList = new List<DepartmentMsg>();
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
           LoadGridLoginUsers();
           ddlDepartName.Enabled = false;
           chkIsActive.Visible = true;
           LoadCompanyName();
           pnlAdd.GroupingText = "Add User";
           //LoadDepartment();
          //LoadDepartment();
           if (LUList.Count == 0)
           {
               ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.MsgForGrdnotLoad + "');", true);
               Pnlgv.Visible = false;
               pnlAdd.Visible = true;

           }
        
       }
       
    }
    protected void ddlCompanyChanged(object sender, EventArgs e)
    {
    
        CompanyChanged(); //scs single company to show on top
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
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
    protected void GrdLoginUser_RowCommand(object sender, GridViewCommandEventArgs e)  //edited
    {
        string InvoiceNo = e.CommandName;
        string WLoginUserId = (e.CommandArgument).ToString();
        foreach (LoginUserMsg log in LUList)
        {
            if (log.LoginUserId == Convert.ToInt32(WLoginUserId))
            {
                txtUserName.Text = log.UserName;
                txtUserPassword.Text = log.Password;
                txtUserPassword.Visible = false; //cannot update password after creating the user. It will be user right. scs 211224
                lblUserPassword.Visible = false;
                txtEmailId.Text = log.EmailId;
                ChkIsCompanyAdmin.Checked = log.IsAdmin;
                ChkIssuperUser.Checked = log.IsSuperUser;
                chkIsActive.Checked = log.IsActive;
                chkIsActive.Visible = true;
                ddlCompanyName.SelectedValue = log.CompanyId.ToString();
                LoadDepartment();
                //loadDept(DepList, log.CompanyId);
                ddlDepartName.SelectedValue = log.DepartmentId.ToString();
                btnSave.Text = "Update";
                txtLoginUserId.Text = (WLoginUserId);
                Pnlgv.Visible = false;
                pnlAdd.Visible = true;
                pnlAdd.GroupingText="Edit User";
            }
        }
    }
 
    #endregion
    #region Methods
    public void LoadGridLoginUsers() //To load data into grid.
    {
        LoginUserMsg Emp = new LoginUserMsg();
        Emp.CreatedBy = BaseMsg.EmployeeCode; 
        Emp.Flag = "R";
        LUList = Bus.MasLoginUserInsertUpdate(Emp);
        if (BaseMsg.IsAdmin == true)
        {
            //leave it as is
        }
        else
        {
            LUList= (from usr in LUList where usr.CompanyCode==BaseMsg.CompanyCode select usr).ToList();
        }
        LoadGrdEmp(LUList);
    }
    public void LoadGrdEmp(List<LoginUserMsg> LList)
    {
        //LList = (from emplst in EmpLst where emplst.IsAuditor == false select emplst).ToList();
        GrdEmployeeMaster.DataSource = "";
        GrdEmployeeMaster.DataSource = LList;
        GrdEmployeeMaster.DataBind();
        if (!user.HasPermission(ProgramName, UserPermission.CanEdit.ToString()))
        {
            GrdEmployeeMaster.Columns[GrdEmployeeMaster.Columns.Count - 1].Visible = false;
        }
        //if (!user.HasPermission(ProgramName, UserPermission.CanDelete.ToString()))
        //{
        //    GrdEmployeeMaster.Columns[GrdEmployeeMaster.Columns.Count - 1].Visible = false;
        //}
    }
   
    
    private void LoadCompanyName()
    {
        EmployeeMasterMsg Emp = new EmployeeMasterMsg();
        Emp.EmployeeCode = BaseMsg.EmployeeCode;
        CompList  = Bus.CompanyMasterSelect(Emp);
        CompList = lib.LoadCompanyOnUserRight(CompList); //if admin allow all companies locations
        ddlCompanyName.DataSource =CompList;
        ddlCompanyName.DataTextField = "CompanyName";
        ddlCompanyName.DataValueField = "CompanyId";
        ddlCompanyName.DataBind();
        ddlCompanyName.Items.Insert(0, "-- Select Please --");
        if (CompList.Count == 1)
        {
            ddlCompanyName.SelectedIndex = 1;
            CompanyChanged();
        }
    }
    private void CompanyChanged()
    {
        if (ddlCompanyName.SelectedIndex > 0)
        {
            Int32 CompId = Convert.ToInt32(ddlCompanyName.SelectedValue);
            LoadDepartment();
            //loadDept(DepList, CompId);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Select Company " + "');", true);

        }
    }

    private void LoadDepartment()
    {
        string Employee = BaseMsg.EmployeeCode;
        DepList = Bus.DepartmentMasterSelect(Employee);
        loadDept(DepList,Convert.ToInt32(ddlCompanyName.SelectedValue));
    }
    private void loadDept(List<DepartmentMsg> dept,Int32 CompId)
    {
        List<DepartmentMsg> depl = new List<DepartmentMsg>();
       
            depl = (from dep in DepList where dep.CompanyId == CompId select dep).ToList();
        if (depl.Count>0)
        {
            ddlDepartName.DataSource = depl;
            ddlDepartName.DataTextField = "DepartmentName";
            ddlDepartName.DataValueField = "DepartmentId";
            ddlDepartName.DataBind();
            ddlDepartName.Items.Insert(0, "-- Select Please --");
            ddlDepartName.Enabled= true;
            if (depl.Count == 1)
            {
                ddlDepartName.SelectedIndex = 1;
            }

        }
        else
        {
            pnlAdd.Visible = false;
            Pnlgv.Visible = true;
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "No department for company " + "');", true);
        }

    }

    
 
    private void EmployeeSave()
    {
        LoginUserMsg Emp = new LoginUserMsg();
        if (btnSave.Text == "Save")
        {
            Emp.Flag = "I";
            Emp.LoginUserId = 0; // since New user
            Emp.Password = KeyGen.EncryptPwd(txtUserPassword.Text.Trim());// Encypt the password --scs150916
           
        }
        else
        {
            Emp.Flag = "U";
            Emp.LoginUserId = Convert.ToInt32(txtLoginUserId.Text);
            Emp.Password = string.Empty;
            
        }
            Emp.UserName = txtUserName.Text.Trim(); //scs to check this properly 270714
            Emp.EmailId = txtEmailId.Text.Trim();
            Emp.CompanyId = Convert.ToInt32(ddlCompanyName.SelectedValue);
            Emp.DepartmentId = Convert.ToInt32(ddlDepartName.SelectedValue);
            Emp.EmailId = txtEmailId.Text;
            Emp.IsAdmin = ChkIsCompanyAdmin.Checked;
            Emp.IsActive = chkIsActive.Checked;
            Emp.IsSuperUser = ChkIssuperUser.Checked;
            Emp.CreatedBy = BaseMsg.EmployeeCode;  // sai 210714


        LUList = Bus.MasLoginUserInsertUpdate(Emp);

        //Output Dispay
        foreach (LoginUserMsg EmpMsg in LUList)
        {
            if (EmpMsg.LoginResult == "0")
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.SuccessFullySaved + "');", true);
                ClearAll();
                
                if (LUList.Count > 0)
                {
                    Pnlgv.Visible = true;
                    pnlAdd.Visible = true;
                    txtUserPassword.Visible = true; //cannot update password after creating the user. It will be user right. scs 211224. after uodate enable it
                    lblUserPassword.Visible = true;
                }

                break;

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + EmpMsg.LoginResult + "');", true);
                break;
            }
        }
    }
 
  
    #endregion
    #region Clear
    public void ClearAll()
    {
        txtUserName.Text = "";
        txtUserPassword.Text = "";
        txtEmailId.Text = "";
        txtLoginUserId .Text = "0";
        chkIsActive.Checked = false;
        ChkIsCompanyAdmin.Checked = false;
        ChkIssuperUser.Checked = false;
        LoadGridLoginUsers();
        LoadCompanyName();
        //LoadDepartment();
        btnSave.Text = "Save";
        pnlAdd.GroupingText = "Add User";
       
    }
    #endregion
    #region Validation

    private int IsValidSave()
    {
        string DisplayErorr = "";
        int Error = 0;
        if (ddlCompanyName.SelectedIndex == 0)
        {
            DisplayErorr = DisplayErorr + "-" + AgilerMail.selectCompanyName;
            Error = 1;
        }
        if (ddlDepartName.SelectedIndex == 0)
        {
            DisplayErorr = DisplayErorr + "-" + "Department Name not Provided";
            Error = 1;
        }
        if (txtUserName.Text.Trim() == "" || Convert.ToInt32(txtUserName.Text.Length.ToString().Trim()) == 0)
        {
            DisplayErorr = DisplayErorr + " User Name is Not provided..";
            //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.ErrEmployeeCode + "');", true);
            Error = 1;
        }
        if ((txtUserPassword.Text.Trim() == "" || Convert.ToInt32(txtUserPassword.Text.Length.ToString().Trim()) == 0) && btnSave.Text =="Save")
        {
            DisplayErorr = DisplayErorr + "-" + AgilerMail.ErrEmployeePassword;
            Error = 1;
        }
        if (txtEmailId.Text.Trim() == "" || Convert.ToInt32(txtEmailId.Text.Length.ToString().Trim()) == 0)
        {
            DisplayErorr = DisplayErorr + "-" + AgilerMail.ErrEmailId;
            Error = 1;
        }
        if (Error == 1)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + DisplayErorr + "');", true);
        }
        return Error;
    }
    //private int IsValidGrid(string UserName, string EmailId,Int32 CompanyId,Int32 DepartmentId)
    //{

    //    int Error = 0;
    //    string DisplayError = "";

    //    if (UserName.Trim() == "")
    //    {
    //        DisplayError = DisplayError + "User Name not Provided";
    //        Error = 1;
    //    }
    //    if (EmailId.Trim() == "" || Convert.ToInt32(EmailId.Trim().ToString().Length) == 0)
    //    {
    //        DisplayError = DisplayError + AgilerMail.ErrEmailId;
    //        Error = 1;
    //    }
    //    if (CompanyId == 0 )
    //    {
    //        DisplayError = DisplayError + AgilerMail.selectCompanyName;
    //        Error = 1;
    //    }
    //    if (DepartmentId == 0 )
    //    {
    //        DisplayError = DisplayError + "Department Name not provided";
    //        Error = 1;
    //    }

   

    //    if (Error == 1)
    //    {
    //        ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + DisplayError + "');", true);
    //    }
    //    return Error;
    //}
    #endregion
 
  
}