using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    BaseClass AccessBaseClass = new BaseClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        lblToday.Text = DateTime.Now.ToString("dddd, d MMMM yyyy", CultureInfo.InvariantCulture);

        string name = (AccessBaseClass.EmployeeName ?? string.Empty).Trim();
        if (name.Length > 0)
        {
            string first = name.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0];
            lblWelcomeCompany.Text = "Welcome back, " + Server.HtmlEncode(first);
        }

        List<TisNavGroup> groups = Master.NavGroups ?? new List<TisNavGroup>();
        rptModules.DataSource = groups;
        rptModules.DataBind();
        pnlNoModules.Visible = groups.Count == 0;
    }
}
