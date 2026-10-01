using System;
using System.Web.UI;
using Ganini.Lib;

/// <summary>Sign-in layout (no navigation, no session gate).</summary>
public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblVersion.Text = Server.HtmlEncode(Config.GetAppsetting("VERSION"));
    }
}
