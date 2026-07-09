using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Ganini.Lib;
using Resources;

public partial class ControlProcessingForUpload : System.Web.UI.Page
{
    #region Declaration
    ProcessBus Bus = new ProcessBus();
    List<EmployeeMasterMsg> EmpList = new List<EmployeeMasterMsg>();

    private static List<ControlProcessMsg> CList = new List<ControlProcessMsg>();
    //public static List<CustomerinStateMasterMsg> CustomerstateList = new List<CustomerinStateMasterMsg>();
    public static int DeleteIndex = 0;
    public static int UpdateIndex = 0;
    UserAccess user = new UserAccess();
    public static string ProgramName = string.Empty;
    BaseClass BaseMsg = new BaseClass();
    private Int32 WCompanyId = 0;
    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        ProgramName = System.IO.Path.GetFileName(Request.PhysicalPath);
        if (!Page.IsPostBack)
        {
            LoadCompanyName();
                      
        }
    }

    protected void ddlCompanyChanged(object sender, EventArgs e)
    {
        if (ddlCompanyName.SelectedIndex > 0)
        {
            LoadGridControl();

        }
        else
        {
            GrdControlProcess.Visible = false;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        System.Globalization.DateTimeFormatInfo dateinfo = new System.Globalization.DateTimeFormatInfo();
        dateinfo.ShortDatePattern = "dd-MM-yyyy";
        int error = 0; Int32 Y =0;
        try
        {
            foreach(GridViewRow gvr in GrdControlProcess.Rows)
            {
                DateTime x = Convert.ToDateTime(((Label)gvr.FindControl("lblPFTo")).Text, dateinfo);
                Int32 WMaxDays = x.Day;
                
                Y = Convert.ToInt32(((TextBox)gvr.FindControl("txtWorkingDays")).Text);


                if (Y > WMaxDays)
                //if (Convert.ToInt64(x.Year.ToString() + x.Month.ToString()) != Convert.ToInt64(WToday.Year.ToString() + WToday.Month.ToString())) 
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Working Days Cannot Exeed Max day of the Month" + "');", true);
                    error = 1;
                }
                else
                {
                    
                    //dummy
                }
            }
            if (error == 0)
            {
                SaveControlProcess();
            }
        }
        catch
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Working Days Format is not proper.." + "');", true);
        }
    }

    #endregion
    #region Methods

    private void LoadCompanyName()
    {
        EmployeeMasterMsg EmpMsg = new EmployeeMasterMsg();
        EmpMsg.EmployeeCode = BaseMsg.EmployeeCode;
        List<CompanyMessage> CompanyList = new List<CompanyMessage>();
        CompanyList = Bus.CompanyMasterSelect(EmpMsg);
        List<CompanyMessage> cmlst = new List<CompanyMessage>();
        if (BaseMsg.IsAdmin == true)
        {
            cmlst = CompanyList.ToList();
        }
        else
        {
            cmlst = (from cm in CompanyList where cm.CompanyCode == BaseMsg.CompanyCode select cm).ToList();
        }

        ddlCompanyName.DataSource = cmlst;
        // ddlCompanyName.DataSource = Bus.CompanyMasterSelect(EmpMsg);
        ddlCompanyName.DataTextField = "CompanyName";
        ddlCompanyName.DataValueField = "CompanyId";
        ddlCompanyName.DataBind();
        ddlCompanyName.Items.Insert(0, "-- Select Please --");
        ddlCompanyName.Focus();
        if (cmlst.Count == 1)
        {
            ddlCompanyName.SelectedIndex = 1;
            LoadGridControl();
           
        }
    }
    private void LoadGridControl()//To load data into grid.
    {
        Int32 CompanyId = Convert.ToInt32(ddlCompanyName.SelectedValue);
        //System.Globalization.DateTimeFormatInfo dateinfo = new System.Globalization.DateTimeFormatInfo();
        //dateinfo.ShortDatePattern = "dd-MM-yyyy";
        string CreatedBy = BaseMsg.EmployeeCode;
        CList = Bus.SelectControlProcessing(CompanyId, CreatedBy);
        if (CList.Count > 0)
        {
            GrdControlProcess.DataSource = "";
            GrdControlProcess.DataSource = CList;
            GrdControlProcess.DataBind();
            Pnlgv.Visible = true;
            GrdControlProcess.Visible = true;
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "No Processing Data Ask Admin to Create.." + "');", true);
            GrdControlProcess.Visible = false;
        }
        //foreach (GridViewRow gvr in GrdControlProcess.Rows)
        //{
        //    txtMonthYear.Text = ((Label)gvr.FindControl("lblYYYYMM")).Text;
        //    break;
        //}

    }
    private void SaveControlProcess()
    {
        System.Globalization.DateTimeFormatInfo dateinfo = new System.Globalization.DateTimeFormatInfo();
        dateinfo.ShortDatePattern = "dd-MM-yyyy";
       // List<ControlProcessMsg> CLst = new List<ControlProcessMsg>();
        ControlProcessMsg cpm = new ControlProcessMsg();
        foreach (GridViewRow gvr in GrdControlProcess.Rows)
        {
            cpm.ControlProcessingId = Convert.ToInt32(((Label)gvr.FindControl("lblControlProcessingId")).Text);
            cpm.WorkingDays =  Convert.ToInt32(((TextBox)gvr.FindControl("txtWorkingDays")).Text);
            //cpm.CompanyId = Convert.ToInt32(((Label)gvr.FindControl("lblCompanyId")).Text);
            //cpm.YYYYMM = (((Label)gvr.FindControl("lblYYYYMM")).Text);
            //cpm.ClosingDate = Convert.ToDateTime(((TextBox)gvr.FindControl("txtClosingDate")).Text,dateinfo);
            cpm.CreatedBy = BaseMsg.EmployeeCode;
            //CLst.Add(cpm);
        }
        string Result = Bus.InsertControlProcessing(cpm);
        if (Result == "S")
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Successfully Saved.." + "');", true);
            LoadGridControl();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Error While Saving ...." + "');", true);
        }
    }
    #endregion

    #region Validation

    private int IsValidSave()
    {
        int Error = 0;
        //string DisplayError = "";
        //if (txtCustomerCode.Text.Trim() == "" || Convert.ToInt32(txtCustomerCode.Text.Trim().Length) == 0)
        //{
        //    DisplayError = DisplayError + " CustomerCode is Mandatory .";
        //    //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.ErrorCustomerCode + "');", true);
        //    Error = 1;
        //}
        
        return Error;
    }

   
    #endregion

 
    }