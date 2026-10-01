using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Resources;
using Ganini.Security;

public partial class ChangePassword : System.Web.UI.Page
{
    ProcessBus Bus = new ProcessBus();
    BaseClass AccessClass = new BaseClass();
    List<ChangePasswordMsg> ChangePwdList = new List<ChangePasswordMsg>();
    KeyGen KeyGen = new KeyGen();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (IsValidValues() == 0)
        {
            ChangePwdSave();
        }
    }

    public int IsValidValues()
    {
        int Error = 0;
        string ErrorMessage = string.Empty;
        if (txtOldPassword.Text.Trim() == string.Empty)
        {
            ErrorMessage = ErrorMessage + "Old Password should not be Empty." + "\r\n";
            Error = 1;
        }
        if (txtNewPassword.Text.Trim() == string.Empty)
        {
            ErrorMessage = ErrorMessage + "New Password should not be Empty." + "\r\n";
            Error = 1;
        }
        if (txtConfirmPassword.Text.Trim() == string.Empty)
        {
            ErrorMessage = ErrorMessage + "Confirm Password should not be Empty." + "\r\n";
            Error = 1;
        }
        else if (txtConfirmPassword.Text.Trim() != txtNewPassword.Text.Trim())
        {
            ErrorMessage = ErrorMessage + "New Password and Confirm Password must be same.";
            Error = 1;
        }
        if (Error == 1)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + ErrorMessage + "');", true);
        }
        return Error;
    }
    public void ChangePwdSave()
    {
        ChangePasswordMsg ChangePwd = new ChangePasswordMsg();
        ChangePwd.NewPassword = txtNewPassword.Text;
        ChangePwd.NewPassword = KeyGen.EncryptPwd(txtNewPassword.Text.Trim());// Encypt the password --scs150916
        ChangePwd.OldPassword = txtOldPassword.Text;
        ChangePwd.OldPassword = KeyGen.EncryptPwd(txtOldPassword.Text.Trim());// Encypt the password  --scs150916
        // ChangePwd.ConfirmPassword = txtConfirmPassword.Text;
        ChangePwd.EmployeeCode = AccessClass.EmployeeCode;
        ChangePwdList = Bus.AdmChangePaswordUpdateSp(ChangePwd);
        foreach (ChangePasswordMsg changepwd in ChangePwdList)
        {
            if (changepwd.ChangePwdResult == "0")
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.SuccessFullySaved + "');", true);
                ClearALL();
                break;

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + changepwd.ChangePwdResult + "');", true);
                break;
            }
        }
    }
    #region Clear
    public void ClearALL()
    {
        txtNewPassword.Text = "";
        txtOldPassword.Text = "";
        txtConfirmPassword.Text = "";
    }
    #endregion
}