using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Resources;
using Ganini.Security;

public partial class ProjectClosure : System.Web.UI.Page
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


}