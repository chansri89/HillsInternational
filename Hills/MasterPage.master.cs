using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Ganini.Lib;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string lblVersionNumber= Config.GetAppsetting("VERSION");
        lblHeading.Text = "Tender Insight System - Version: " + lblVersionNumber; //scs 230501 vj wants version in the top
    }
}
