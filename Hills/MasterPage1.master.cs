using System;

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
    }

    protected void lnkLogOut_Click(object sender, EventArgs e)
    {
        SignOut();
    }
}
