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
public partial class ForgotPassword : System.Web.UI.Page
{
    #region Declaration 
    ProcessBus Bus = new ProcessBus();
    List<EmployeeMasterMsg> EmpList = new List<EmployeeMasterMsg>();
    EmployeeMasterMsg emp = new EmployeeMasterMsg();
    BaseClass BaseInfoMsg = new BaseClass();
    #endregion
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {        
        if (!Page.IsPostBack)
        {
            txtUsername.Focus();
        }
    }
    #endregion
    #region Validation
    public int LogValidation()
    {
        int Error = 0;
        if (txtUsername.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.ErrUserName + "');", true);
            txtUsername.Focus();
            Error = 1;
        }
      
        return Error;
    }
    #endregion
    #region Methods
    public void ForgotPwd()
        {
            string Result;
            emp.EmployeeCode = txtUsername.Text;
            emp.LoginEmployeeCode = BaseInfoMsg.EmployeeCode;
            Result = Bus.GetForgotPassword(emp);
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + Result + "');", true);
        }
    #endregion
    protected void btnGo_Click(object sender, EventArgs e)
    {
        if (LogValidation() == 0)
        {
            ForgotPwd();
        }
        
    }
}