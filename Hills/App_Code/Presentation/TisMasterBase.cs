using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using Ganini.Lib;

/// <summary>One page link in the sidebar navigation.</summary>
public class TisNavItem
{
    public string Text { get; set; }
    public string Url { get; set; }
    public string Section { get; set; }
    public bool StartsSection { get; set; }
    public bool IsActive { get; set; }

    public string TextHtml { get { return HttpUtility.HtmlEncode(Text); } }
    public string SectionHtml { get { return HttpUtility.HtmlEncode(Section ?? string.Empty); } }
    public string UrlAttr { get { return HttpUtility.HtmlAttributeEncode(Url); } }
    public string LinkClass { get { return IsActive ? "tis-nav__link is-active" : "tis-nav__link"; } }
    public string AriaCurrent { get { return IsActive ? "page" : string.Empty; } }
    public string SectionClass { get { return StartsSection ? "tis-nav__section" : "tis-hidden"; } }
    public string ModuleSectionClass { get { return StartsSection ? "tis-module__section" : "tis-hidden"; } }
}

/// <summary>A top-level menu group (MainMenu) with its pages, optionally split into SubMenu sections.</summary>
public class TisNavGroup
{
    public TisNavGroup()
    {
        Items = new List<TisNavItem>();
    }

    public string Key { get; set; }
    public string Text { get; set; }
    public string Icon { get; set; }
    public List<TisNavItem> Items { get; private set; }

    public bool HasActive { get { return Items.Any(i => i.IsActive); } }
    public string TextHtml { get { return HttpUtility.HtmlEncode(Text); } }
    public string GroupClass { get { return HasActive ? "tis-nav__group has-active" : "tis-nav__group"; } }
    public string CountText { get { return Items.Count == 1 ? "1 page" : Items.Count.ToString(CultureInfo.InvariantCulture) + " pages"; } }
}

/// <summary>
/// Shared behaviour for the authenticated app shell (MasterPage1, and MasterPage3 which nests inside it):
/// session gate, role-based menu from AdmUserAccessProgramsSelect, breadcrumb, page title and sign-out.
/// </summary>
public abstract class TisMasterBase : MasterPage
{
    private const string AppName = "Hills TIS";
    private static readonly string SessionFlag = "1";

    private readonly ProcessBus bus = new ProcessBus();
    private readonly BaseClass access = new BaseClass();

    public List<TisNavGroup> NavGroups { get; private set; }
    public TisNavGroup CurrentGroup { get; private set; }
    public TisNavItem CurrentItem { get; private set; }
    public bool IsHome { get; private set; }

    public string UserName { get { return access.EmployeeName; } }
    public string Version { get { return Config.GetAppsetting("VERSION"); } }
    public string Today { get { return DateTime.Now.ToString("dd MMM yyyy", CultureInfo.InvariantCulture); } }

    public string UserInitials
    {
        get
        {
            string name = (UserName ?? string.Empty).Trim();
            if (name.Length == 0) return "?";
            string[] parts = name.Split(new[] { ' ', '.', '_' }, StringSplitOptions.RemoveEmptyEntries);
            string initials = parts.Length > 1
                ? parts[0].Substring(0, 1) + parts[parts.Length - 1].Substring(0, 1)
                : parts[0].Substring(0, Math.Min(2, parts[0].Length));
            return initials.ToUpperInvariant();
        }
    }

    /// <summary>Breadcrumb list items for the top bar (already HTML-encoded).</summary>
    public string BreadcrumbHtml
    {
        get
        {
            var crumbs = new List<string>();
            if (IsHome)
            {
                crumbs.Add("Home");
            }
            else if (CurrentItem != null)
            {
                crumbs.Add(CurrentGroup.Text);
                if (!string.IsNullOrEmpty(CurrentItem.Section)) crumbs.Add(CurrentItem.Section);
                crumbs.Add(CurrentItem.Text);
            }
            else
            {
                crumbs.Add(PageTitle);
            }

            var sb = new StringBuilder();
            foreach (string crumb in crumbs)
            {
                sb.Append("<li>").Append(HttpUtility.HtmlEncode(crumb)).Append("</li>");
            }
            return sb.ToString();
        }
    }

    /// <summary>Human title for the current page: the menu entry, else the page's own Title, else its file name.</summary>
    public string PageTitle
    {
        get
        {
            if (IsHome) return "Home";
            if (CurrentItem != null) return CurrentItem.Text;
            if (!string.IsNullOrEmpty(Page.Title)) return Page.Title;
            return Humanize(System.IO.Path.GetFileNameWithoutExtension(Request.AppRelativeCurrentExecutionFilePath));
        }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        if (access.EmployeeName == string.Empty)
        {
            Response.Redirect(string.Format("~/Login.aspx?IsSessionTimeOutFlag={0}", "Y"));
            return;
        }

        List<ProgramMsg> programs = access.ProgramMsgList;
        if (!Page.IsPostBack || programs == null)
        {
            Session["Sess1curVal"] = "";
            var emp = new EmployeeMasterMsg();
            emp.EmployeeCode = access.EmployeeCode;
            programs = bus.AdmUserAccessProgramsSelect(emp);
            access.ProgramMsgList = programs;
        }

        if (access.SessionVar1 != "")
        {
            Session["Sess1curVal"] = SessionFlag;
            Session["Sess3curVal"] = SessionFlag;
            access.SessionVar1 = SessionFlag;
        }

        BuildNavigation(programs ?? new List<ProgramMsg>());
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        if (Page.Header != null)
        {
            Page.Title = PageTitle + " - " + AppName;
        }
    }

    /// <summary>Records the sign-out and clears the session (same behaviour as the original masters).</summary>
    protected void SignOut()
    {
        var login = new LoginInfoMsg();
        login.UserName = access.EmployeeCode;
        login.UserSessionId = access.UserSessionId;
        bus.UpdateUserLogoffInfo(login);
        Session.Clear();
        Session.RemoveAll();
        Response.Redirect("~/Login.aspx");
    }

    private void BuildNavigation(IEnumerable<ProgramMsg> programs)
    {
        string current = FileNameOf(Request.AppRelativeCurrentExecutionFilePath);
        IsHome = string.Equals(current, "default.aspx", StringComparison.OrdinalIgnoreCase);

        var groups = new List<TisNavGroup>();
        var byName = new Dictionary<string, TisNavGroup>(StringComparer.OrdinalIgnoreCase);
        var seenUrls = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        foreach (ProgramMsg program in programs)
        {
            string main = Clean(program.MainMenu);
            string path = Clean(program.ProgramAccessPath);
            if (main.Length == 0 || path.Length == 0) continue;

            TisNavGroup group;
            if (!byName.TryGetValue(main, out group))
            {
                group = new TisNavGroup { Text = main, Key = Slug(main), Icon = IconFor(main) };
                byName.Add(main, group);
                groups.Add(group);
            }

            if (!seenUrls.Add(main + "|" + path)) continue;

            string section = Clean(program.SubMenu);
            string previousSection = group.Items.Count > 0 ? group.Items[group.Items.Count - 1].Section : null;
            var item = new TisNavItem
            {
                Text = Clean(program.ChildMenu).Length > 0 ? Clean(program.ChildMenu) : Humanize(System.IO.Path.GetFileNameWithoutExtension(FileNameOf(path))),
                Url = ResolveClientUrl(path),
                Section = section,
                StartsSection = section.Length > 0 && !string.Equals(section, previousSection, StringComparison.OrdinalIgnoreCase),
                IsActive = !IsHome && string.Equals(FileNameOf(path), current, StringComparison.OrdinalIgnoreCase)
            };
            group.Items.Add(item);

            if (item.IsActive && CurrentItem == null)
            {
                CurrentItem = item;
                CurrentGroup = group;
            }
        }

        NavGroups = groups;
    }

    private static string Clean(string value)
    {
        return (value ?? string.Empty).Trim();
    }

    private static string FileNameOf(string path)
    {
        string p = (path ?? string.Empty);
        int q = p.IndexOfAny(new[] { '?', '#' });
        if (q >= 0) p = p.Substring(0, q);
        p = p.Replace('\\', '/');
        int slash = p.LastIndexOf('/');
        return slash >= 0 ? p.Substring(slash + 1) : p;
    }

    private static string Slug(string text)
    {
        var sb = new StringBuilder();
        foreach (char c in text.ToLowerInvariant())
        {
            if (char.IsLetterOrDigit(c)) sb.Append(c);
            else if (sb.Length > 0 && sb[sb.Length - 1] != '-') sb.Append('-');
        }
        return sb.ToString().Trim('-');
    }

    private static string Humanize(string name)
    {
        if (string.IsNullOrEmpty(name)) return AppName;
        string n = name;
        if (n.StartsWith("rpt", StringComparison.Ordinal)) n = n.Substring(3);
        if (n.StartsWith("A_", StringComparison.Ordinal)) n = n.Substring(2);
        var sb = new StringBuilder();
        for (int i = 0; i < n.Length; i++)
        {
            char c = n[i];
            if (c == '_') { sb.Append(' '); continue; }
            if (i > 0 && char.IsUpper(c) && (char.IsLower(n[i - 1]) || (i + 1 < n.Length && char.IsLower(n[i + 1]) && char.IsUpper(n[i - 1]))))
            {
                sb.Append(' ');
            }
            sb.Append(c);
        }
        return sb.ToString().Trim();
    }

    /// <summary>Picks a sidebar icon for a menu group from keywords in its name.</summary>
    public static string IconFor(string groupName)
    {
        string g = (groupName ?? string.Empty).ToLowerInvariant();
        if (g.Contains("report")) return "chart";
        if (g.Contains("upload") || g.Contains("import") || g.Contains("excel")) return "upload";
        if (g.Contains("admin") || g.Contains("role") || g.Contains("user") || g.Contains("secur") || g.Contains("access")) return "shield";
        if (g.Contains("tender") || g.Contains("mapping")) return "map";
        if (g.Contains("cram") || g.Contains("iow") || g.Contains("cost") || g.Contains("estimat")) return "layers";
        if (g.Contains("project")) return "briefcase";
        if (g.Contains("item") || g.Contains("material")) return "box";
        if (g.Contains("client") || g.Contains("customer")) return "users";
        if (g.Contains("company") || g.Contains("enterprise") || g.Contains("organi")) return "building";
        if (g.Contains("master")) return "database";
        if (g.Contains("setting") || g.Contains("config") || g.Contains("utilit") || g.Contains("tool")) return "settings";
        if (g.Contains("rate") || g.Contains("price")) return "rupee";
        return "folder";
    }
}
