using System;
using System.Web;

/// <summary>Empty-state block for grids (use inside a GridView EmptyDataTemplate).</summary>
public partial class Controls_EmptyState : System.Web.UI.UserControl
{
    public Controls_EmptyState()
    {
        Icon = "search";
        Title = "Nothing to show yet";
        Text = string.Empty;
    }

    public string Icon { get; set; }
    public string Title { get; set; }
    public string Text { get; set; }

    protected string IconHref { get { return ResolveUrl("~/Images/icons.svg") + "#i-" + HttpUtility.HtmlAttributeEncode(Icon); } }
    protected string TitleHtml { get { return HttpUtility.HtmlEncode(Title); } }
    protected string TextHtml { get { return HttpUtility.HtmlEncode(Text); } }
}
