using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    BaseClass AccessBaseClass = new BaseClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        //lblWelcomeCompany.Text = AccessBaseClass.CompanyCode;
        //lblWelcomeCompany.Text = "Welcome to " + lblWelcomeCompany.Text;
    }
}