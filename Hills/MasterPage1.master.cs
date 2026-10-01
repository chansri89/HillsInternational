using System;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Authenticated app shell. Session gate, menu, breadcrumb and sign-out live in TisMasterBase.
/// </summary>
public partial class MasterPage1 : TisMasterBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lnkNavHome.Attributes["class"] = IsHome ? "tis-nav__link is-active" : "tis-nav__link";
        if (IsHome) lnkNavHome.Attributes["aria-current"] = "page";

        rptNavGroups.DataSource = NavGroups;
        rptNavGroups.DataBind();

        litBreadcrumb.Text = BreadcrumbHtml;
        litInitials.Text = Server.HtmlEncode(UserInitials);
        litInitialsTop.Text = litInitials.Text;
        lbluname.Text = Server.HtmlEncode(UserName);
        lblUserMenuName.Text = lbluname.Text;
        lblVersionNumber.Text = Server.HtmlEncode(Version);
        lblDate.Text = Today;

        RegisterUploadPostBackControls(ContentPlaceHolder1);
    }

    private void RegisterUploadPostBackControls(Control root)
    {
        ScriptManager scriptManager = ScriptManager.GetCurrent(Page);
        if (scriptManager == null || !ContainsFileUpload(root)) return;

        RegisterPostBackButtons(root, scriptManager);
    }

    private bool ContainsFileUpload(Control root)
    {
        if (root is FileUpload) return true;

        foreach (Control child in root.Controls)
            if (ContainsFileUpload(child)) return true;

        return false;
    }

    private void RegisterPostBackButtons(Control root, ScriptManager scriptManager)
    {
        if (root is IButtonControl) scriptManager.RegisterPostBackControl(root);

        foreach (Control child in root.Controls)
            RegisterPostBackButtons(child, scriptManager);
    }

    protected void lnkLogOut_Click(object sender, EventArgs e)
    {
        SignOut();
    }
}
