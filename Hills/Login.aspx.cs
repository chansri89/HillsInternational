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
using System.Text;
using System.Net.Mail;
using Ganini.Security;

public partial class Login : System.Web.UI.Page
{
    ProcessBus Bus = new ProcessBus();
    EmployeeMasterMsg emp = new EmployeeMasterMsg();
    List<ProgramMsg> ProgramMsgList = new List<ProgramMsg>();
   // LoginInfoMsg LoginInfo = new LoginInfoMsg();
    BaseClass BaseInfoMsg = new BaseClass();
    string _Ip = Config.GetAppsetting("IP");
    int _Port = Convert.ToInt32(Config.GetAppsetting("_Port"));
    KeyGen KeyGen = new KeyGen();
    //private static string mytok = "{6D19E6F5-AC05-42ac-8E57-DB71C8FDE672}";//This is Key for decrypt the Password

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["IsSessionTimeOutFlag"] != null && Request.QueryString["IsSessionTimeOutFlag"].ToString() == "Y")
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Due to Session time out, your login was expired. Please re-login." + "');", true);
            txtUsername.Focus();
        }
        if (!Page.IsPostBack)
        {
            txtUsername.Focus();
        }
    }
    protected void lnkForgot_Click(object sender, EventArgs e)
    {
        if (ForgotValidation() == 0)
        {
            ForgotPwd();
        }
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        LoginInfoMsg LoginInfo = new LoginInfoMsg();
        if (LogValidation() == 0)
        {
            System.Net.Dns.GetHostName(); //Get HostName Ex: Sys-4
            System.Net.IPAddress[] MachineIP = System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName());
            for (int IP = 0; IP < MachineIP.Length; IP++)
            {
                LoginInfo.MachineIP = MachineIP[IP].ToString(); //Get MAchineIP Address
            }
            LoginInfo.UserName = txtUsername.Text.Trim();
            //LoginInfo.Password = txtPwd.Text.Trim();
            LoginInfo.Password = KeyGen.EncryptPwd(txtPwd.Text.Trim());// Encypt the password

            LoginInfo = Bus.CheckLogin(LoginInfo);
            if (LoginInfo.Result == "0" || LoginInfo.Result == "1")//added by abinayaa for db size checking --230513 "|| LoginInfo.Result == "1" "
            {
                BaseInfoMsg.EmployeeName = LoginInfo.EmployeeName;
                BaseInfoMsg.EmployeeCode = LoginInfo.EmployeeCode; //txtUsername.Text; --SCS 210224
                BaseInfoMsg.IsAdmin = LoginInfo.IsAdmin; //txtUsername.Text; --SCS 210224
                BaseInfoMsg.IsSuperUser = LoginInfo.IsSuperUser; //--SCS 210806
                BaseInfoMsg.CompanyName = LoginInfo.CompanyName;
                BaseInfoMsg.CompanyCode = LoginInfo.CompanyCode;
                BaseInfoMsg.CompanyId = Convert.ToInt32(LoginInfo.CompanyId);//scs 210224 converted to int
                BaseInfoMsg.DepartmentId = LoginInfo.DepartmentId.ToString(); //scs 210224
                BaseInfoMsg.UserSessionId = LoginInfo.UserSessionId;
                BaseInfoMsg.LoginResult = LoginInfo.Result;//added by abinayaa for db size checking --230513
                Response.Redirect("Default.aspx");//Abinayaa 200812 Added for First Screen
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + LoginInfo.Result + "');", true);

                txtUsername.Text = string.Empty;
                txtPwd.Text = string.Empty;
                txtUsername.Focus(); //scs 201003
            }
        }
    }

    public int LogValidation()
    {
        int Error = 0;
        if (txtUsername.Text.Trim() == "")
        {
            //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.ErrUserName+ "');", true);
            //txtUsername.Focus();
            Error = 1;
        }
        if (txtPwd.Text.Trim() == "")
        {
            //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.ErrEmployeePassword+ "');", true);
            //txtPwd.Focus();
            Error = 1;
        }
        if (Error == 1)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.ErrUserName +" and "+ AgilerMail.ErrEmployeePassword + "');", true); 
            txtUsername.Focus();
        }
        return Error;
    }
    public int ForgotValidation()
    {
        int Error = 0;
        if (txtUsername.Text.Trim() == "" || Convert.ToInt32(txtUsername.Text.Length.ToString().Trim()) == 0)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.ErrForgotPwd + "');", true);
            Error = 1;
        }

        return Error;
    }
    #region Methods
    public void ForgotPwd()
    {
        //commeted by abinayaa 130713
        EmployeeMasterMsg Employee = new EmployeeMasterMsg();
        emp.EmployeeCode = txtUsername.Text;
        //Employee = Bus.GetForgotPassword(emp);
        MailMessage mail = new MailMessage();
        if (Employee.EmployeeResult == "0")
        {
            if (SendMail(Employee.EmployeeName, Employee.Password, Employee.EmailId, Employee.EmployeeCode))
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Your Password is sent to your Email Id" + "');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Mail Not Sent. Please Contact you Admin." + "');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Invalid User Name" + "');", true);
        }
        //commeted by abinayaa 130713

        string Result;
        emp.EmployeeCode = txtUsername.Text;
        Result = Bus.GetForgotPassword(emp);
        ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + Result + "');", true);
    }

    public bool SendMail(string EmployeeName, string Password, string EmailId, string EmployeeCode)
    {
        bool IsSuccess = false;
        MailMessage mail = new MailMessage(Config.GetAppsetting("FromMail"), EmailId);
        try
        {
            SmtpClient Client = new SmtpClient(_Ip, _Port);
            mail.Subject = Config.GetAppsetting("Subject");
            StringBuilder sb = new StringBuilder();
            sb.Append("<p> Dear " + EmployeeName + ",<p>");
            sb.Append("<br/>");
            sb.Append("<style type=" + '"' + "text/css" + '"' + ">           table {font-size: 75%;}           </style><p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + Config.GetAppsetting("BodyMessage") + "<p>");
            sb.Append("<table border=" + '"' + 1 + '"' + ">");
            sb.Append("<tr style=" + '"' + "color:Blue" + '"' + "> <td width=" + '"' + "90px" + '"' + "> User Name : </td> <td width=" + '"' + "90px" + '"' + ">" + EmployeeCode + " </td> </tr>");
            sb.Append("<tr style=" + '"' + "color:Blue" + '"' + "> <td width=" + '"' + "90px" + '"' + "> Password : </td> <td width=" + '"' + "90px" + '"' + ">" + Password + " </td> </tr>");
            sb.Append("</table>");
            sb.Append("<p> Please note: Your username & password are both case sensitive.<p>");
            sb.Append("<br/>");
            sb.Append("<br/>");
            sb.Append("<p>" + Config.GetAppsetting("BodyMessage2") + "<p>");
            mail.Body = sb.ToString();
            mail.IsBodyHtml = true;
            Client.UseDefaultCredentials = true;
            Client.Send(mail);
            IsSuccess = true;
        }
        catch (Exception ex)
        {
            IsSuccess = false;
            ExceptionHandling eh = new ExceptionHandling();
            eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.UI);
        }
        return IsSuccess;
    }
    #endregion
}