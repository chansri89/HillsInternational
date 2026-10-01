using System;
using System.Web;
using System.Web.UI;

/// <summary>
/// Three-step progress indicator for the Excel upload pages.
/// Set Current explicitly, or set SheetPanelId to the panel that the page shows once a
/// workbook has been read (step 2); when that panel is hidden the upload is on step 1.
/// </summary>
public partial class Controls_UploadSteps : System.Web.UI.UserControl
{
    public Controls_UploadSteps()
    {
        Step1 = "Choose company and file";
        Step2 = "Select sheets";
        Step3 = "Save to staging";
        Current = 1;
    }

    public string Step1 { get; set; }
    public string Step2 { get; set; }
    public string Step3 { get; set; }
    public int Current { get; set; }
    public string SheetPanelId { get; set; }

    protected string Step1Html { get { return HttpUtility.HtmlEncode(Step1); } }
    protected string Step2Html { get { return HttpUtility.HtmlEncode(Step2); } }
    protected string Step3Html { get { return HttpUtility.HtmlEncode(Step3); } }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        if (string.IsNullOrEmpty(SheetPanelId) || NamingContainer == null) return;
        Control panel = NamingContainer.FindControl(SheetPanelId);
        if (panel != null) Current = panel.Visible ? 2 : 1;
    }

    protected string StepClass(int step)
    {
        if (step < Current) return "is-done";
        return step == Current ? "is-current" : string.Empty;
    }
}
