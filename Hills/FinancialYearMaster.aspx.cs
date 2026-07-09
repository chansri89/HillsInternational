using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Resources;
using Ganini.Lib;

public partial class FinancialYearMaster : System.Web.UI.Page
{ 
    #region Declaration
    ProcessBus Bus = new ProcessBus();
    List<FinancialYearMasterMsg> FinancialYrList = new List<FinancialYearMasterMsg>();
    public static int DeleteIndex = 0;
    public static int UpdateIndex = 0;
    UserAccess user = new UserAccess();
    Validation valid = new Validation();
    BaseClass BaseClassInfo = new BaseClass();
    public static string ProgramName = string.Empty;
    public static int JScriptFlag = 0;
    #endregion
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        ProgramName = System.IO.Path.GetFileName(Request.PhysicalPath);
        if (!Page.IsPostBack)
        {
            txtFromDate.Text = System.DateTime.Now.Date.ToString("dd/MM/yyyy");
            txtToDate.Text = System.DateTime.Now.Date.ToString("dd/MM/yyyy");
            LoadGrdFinancialYearMaster();
            if (FinancialYrList.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.MsgForGrdnotLoad + "');", true);
                Pnlgv.Visible = false;
                pnlAdd.Visible = true;

            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        System.Globalization.DateTimeFormatInfo dateinfo = new System.Globalization.DateTimeFormatInfo();
        dateinfo.ShortDatePattern = "dd/MM/yyyy";

        if (!user.HasPermission(ProgramName, UserPermission.CanCreate.ToString()))
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.CreatePermissionRestricted + "');", true);
        }
        else
        {
            if (IsValidSave() == 0)
            {
                int Frmdate = Convert.ToDateTime(txtFromDate.Text, dateinfo).DayOfYear;
                int TDate = Convert.ToDateTime(txtToDate.Text, dateinfo).DayOfYear;
                int Frmtodate = Frmdate + TDate;
                if (Frmtodate >= 365 || Frmtodate <= 365)
                {
                    if (JScriptFlag == 0 && HidCount.Value == "0")
                    {
                        JScriptFlag = 1;
                        String jsScript = "";
                        //Asking a alert Message to Delete or not
                        jsScript += "var answer=confirm(\'" + "Financial year have less than or more than 12 Months is it ok ?" + "\');\n";
                        jsScript += "if (answer){\n";
                        //If answer is OK then Updating the HiddenField(control Available) HidDeleteCount as '1' and calling the btnSave_Click to call the Method CompanyDelete
                        jsScript += "document.getElementById(\"ctl00_ContentPlaceHolder1_HidCount\").value='" + "1" + "';\n";
                        jsScript += "document.getElementById(\"ctl00_ContentPlaceHolder1_btnSave\").click();\n";
                        jsScript += "}\n";
                        jsScript += "else{\n";
                        //If answer is CANCEL then Updating the HiddenField(control Available) HidDeleteCount as '0' 
                        jsScript += "document.getElementById(\"ctl00_ContentPlaceHolder1_HidCount\").value='" + "0" + "';\n";
                        jsScript += "}\n";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", jsScript, true);
                    }
                }
                if (HidCount.Value == "1")
                {
                    FinancialYrSave();
                    HidCount.Value = "0";
                    JScriptFlag = 0;
                    return;
                }
                else
                {
                    //AllClear();
                    JScriptFlag = 0;
                    return;
                }
                
            }
        }
       
    }
    #endregion
    #region Methods
    private void LoadGrdFinancialYearMaster()
    {

        FinancialYearMasterMsg Financial = new FinancialYearMasterMsg();
        Financial.Flag = "R";
        FinancialYrList = Bus.MasFinancialYearInsertUpdate(Financial);
        GrdFinancialYearMaster.DataSource = "";
        GrdFinancialYearMaster.DataSource = FinancialYrList;
        GrdFinancialYearMaster.DataBind();
        if (!user.HasPermission(ProgramName, UserPermission.CanEdit.ToString()))
        {
            GrdFinancialYearMaster.Columns[GrdFinancialYearMaster.Columns.Count - 1].Visible = false;
        }
    }

    private void FinancialYrSave()
    {

        System.Globalization.DateTimeFormatInfo dateinfo = new System.Globalization.DateTimeFormatInfo();
        dateinfo.ShortDatePattern = "dd/MM/yyyy";

        FinancialYearMasterMsg Financial = new FinancialYearMasterMsg();
        Financial.Flag = "I";
        Financial.FromDate = Convert.ToDateTime(txtFromDate.Text, dateinfo);
        Financial.ToDate = Convert.ToDateTime(txtToDate.Text, dateinfo);
        Financial.FiscalYear = Convert.ToDateTime(txtFromDate.Text, dateinfo).Year;
        Financial.CreatedBy = BaseClassInfo.EmployeeCode;
        Financial.IsActive = true;
        FinancialYrList = Bus.MasFinancialYearInsertUpdate(Financial);
        
        //Output Dispay
        foreach (FinancialYearMasterMsg FinancialYrSave in FinancialYrList)
        {
            if (FinancialYrSave.FinancialYearResult == "0")
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.SuccessFullySaved + "');", true);
                LoadGrdFinancialYearMaster();
                AllClear();
                if (FinancialYrList.Count >0)
                    {
                        Pnlgv.Visible = true;
                        pnlAdd.Visible = true;
                    }
                break;

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + FinancialYrSave.FinancialYearResult + "');", true);
                break;
            }
        }
    }
    public void FinancialYrUpdate()
    {
        System.Globalization.DateTimeFormatInfo dateinfo = new System.Globalization.DateTimeFormatInfo();
        dateinfo.ShortDatePattern = "dd/MM/yyyy";
        GridViewRow row = GrdFinancialYearMaster.Rows[UpdateIndex];
        FinancialYearMasterMsg Financial = new FinancialYearMasterMsg();
        Financial.Flag = "U";
        Label lblFinancialYearId = (Label)row.FindControl("lblFinancialYearId");
        TextBox txtFromDate = (TextBox)row.FindControl("txtFromDate");
        TextBox txtToDate = (TextBox)row.FindControl("txtToDate");
        TextBox FiscalYear = (TextBox)row.FindControl("txtFromDate");

        Financial.FinancialYearId = Convert.ToInt32(lblFinancialYearId.Text);
        Financial.FromDate = Convert.ToDateTime(txtFromDate.Text, dateinfo);
        Financial.ToDate = Convert.ToDateTime(txtToDate.Text, dateinfo);
        Financial.FiscalYear = Convert.ToDateTime(txtFromDate.Text, dateinfo).Year;
        Financial.CreatedBy = BaseClassInfo.EmployeeCode;
        FinancialYrList = Bus.MasFinancialYearInsertUpdate(Financial);
        GrdFinancialYearMaster.EditIndex = -1;

        foreach (FinancialYearMasterMsg FinancialYrUpdate in FinancialYrList)
        {
            if (FinancialYrUpdate.FinancialYearResult == "0")
            {
                AllClear();
                break;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + FinancialYrUpdate.FinancialYearResult + "');", true);
                break;
            }
        }
    }  
    #endregion
    #region Clear
    public void AllClear()
    {


        txtFromDate.Text = System.DateTime.Now.Date.ToString("dd/MM/yyyy");
        txtToDate.Text = System.DateTime.Now.Date.ToString("dd/MM/yyyy");
        chkIsActive.Checked = false;
        LoadGrdFinancialYearMaster();

    }
    #endregion
    #region Validation
    private int IsValidSave()
    {

        System.Globalization.DateTimeFormatInfo dateinfo = new System.Globalization.DateTimeFormatInfo();
        dateinfo.ShortDatePattern = "dd/MM/yyyy";

        int Error = 0;
        string DisplayError = "";

        if ((txtFromDate.Text.Trim() == "" || txtFromDate.Text.Length.ToString().Trim() == "0"))
        {
            DisplayError = DisplayError + "--" + AgilerMail.ErrFromDate;
            Error = 1;
        }

        if (txtToDate.Text.Trim() == "" || txtToDate.Text.Length.ToString().Trim() == "0")
        {
            DisplayError = DisplayError + "--" + AgilerMail.ErrToDate;
            Error = 1;
        }
        if (Convert.ToDateTime(txtFromDate.Text, dateinfo) > Convert.ToDateTime(txtToDate.Text, dateinfo))
        {
            DisplayError = DisplayError + "--" + AgilerMail.ErrFromToDate;
            Error = 1;
        }
        if (Error == 1)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + DisplayError + "');", true);
        }
        return Error;
    }
    private int IsValidGrid(string FromDate, string ToDate)
    {

        System.Globalization.DateTimeFormatInfo dateinfo = new System.Globalization.DateTimeFormatInfo();
        dateinfo.ShortDatePattern = "dd/MM/yyyy";

        int Error = 0;
        string DisplayError = "";

        if (Convert.ToDateTime(FromDate, dateinfo) > Convert.ToDateTime(ToDate, dateinfo))
        {
            DisplayError = DisplayError + "--" + AgilerMail.ErrFromToDate;
            Error = 1;
        }
        else
        {
            int FromMonth = Convert.ToDateTime(FromDate).Month;
            int Todate = Convert.ToDateTime(ToDate).Month;
            int Totmonth = FromMonth + Todate;
            if (Totmonth > 12)
            {
                DisplayError = DisplayError + "--" + AgilerMail.ErrFinancialYear;
                Error = 1;
            }
        }
        if (Error == 1)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + DisplayError + "');", true);
        }
        return Error;
    }
    #endregion
    #region GrdEdit

    protected void GrdFinancialYearMaster_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        System.Globalization.DateTimeFormatInfo dateinfo = new System.Globalization.DateTimeFormatInfo();
        dateinfo.ShortDatePattern = "dd/MM/yyyy";

        if (!user.HasPermission(ProgramName, UserPermission.CanCreate.ToString()))
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.CreatePermissionRestricted + "');", true);
        }
        else
        {

            UpdateIndex = e.RowIndex;
            GridViewRow row = GrdFinancialYearMaster.Rows[UpdateIndex];
            //string FromDate = Convert.ToDateTime(((TextBox)row.FindControl("txtFromDate")).Text).ToString();
            //string ToDate = Convert.ToDateTime(((TextBox)row.FindControl("txtToDate")).Text).ToString();
            FinancialYrUpdate();
        }
    }  
    protected void GrdFinancialYearMaster_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GrdFinancialYearMaster.EditIndex = -1;
        LoadGrdFinancialYearMaster();
    }
    protected void GrdFinancialYearMaster_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GrdFinancialYearMaster.EditIndex = e.NewEditIndex;
        GridViewRow row = GrdFinancialYearMaster.Rows[GrdFinancialYearMaster.EditIndex];
        LoadGrdFinancialYearMaster();
        foreach (GridViewRow gvr in GrdFinancialYearMaster.Rows)
        {
            if (gvr.RowIndex == GrdFinancialYearMaster.EditIndex)
            {
                TextBox Fromdate = ((TextBox)gvr.FindControl("txtFromDate"));
                TextBox Todate = ((TextBox)gvr.FindControl("txtToDate"));
            }
        }
    }

    #endregion
}