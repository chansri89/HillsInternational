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

public partial class ClientProject : System.Web.UI.Page
{
    #region Declaration
    ProcessBus Bus = new ProcessBus(); LibFunctions Lib = new LibFunctions();
    List<EmployeeMasterMsg> EmpList = new List<EmployeeMasterMsg>();
    private static List<ClientMsg> ClientList = new List<ClientMsg>();
    List<EnterpriseMasterMsg> EnterpriseList = new List<EnterpriseMasterMsg>(); //scs 2704
    List<StateMasterMsg> StateList = new List<StateMasterMsg>();
    //private static List<ClientTypeMsg> GCusTypLst = new List<ClientTypeMsg>();
    private static List<CompanyMessage> CmpList = new List<CompanyMessage>();
    List<ProjectMsg> PMList = new List<ProjectMsg>();
    public static List<SectorMsg> SectList = new List<SectorMsg>();
    List<SectorMsg> SectGrpList = new List<SectorMsg>();
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
            Pnlgv.Visible = false;
            pnlAdd.Visible = false;
            LoadCompany();
            LoadState();
            LoadSector();
            LoadddlSector(SectList);
            LoadddlSectorGroup(SectList);
            
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        
        AllClear();
       // Pnlgv.Visible = false;
        pnlAdd.Visible = false;
    }
    protected void ddlCompanyChanged(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedIndex > 0)
        {
            WCompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
            LoadClient(WCompanyId);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Pls Select Company" + "');", true);
            Pnlgv.Visible = false;
            pnlAdd.Visible = false;
        }
    }
    protected void ddlSectorGroupChanged(object sender, EventArgs e)
    {
        LoadddlSector(SectList);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text.ToUpper() == "SAVE")
        {
            if (!user.HasPermission(ProgramName, UserPermission.CanCreate.ToString()))
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.CreatePermissionRestricted + "');", true);
            }
            else
            {

                if (IsValidSave() == 0)
                {
                    ClientSave();
                }
            }
        }
        else if (btnSave.Text.ToUpper() == "UPDATE")
        {
            if (!user.HasPermission(ProgramName, UserPermission.CanEdit.ToString()))
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.EditPermissionRestricted + "');", true);
                AllClear();
            }
            else
            {

                if (IsValidSave() == 0)
                {
                    ClientSave();
                }
            }
        }
        else
        {
        }
    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        if (ddlClient.SelectedIndex > 0 && ddlCompany.SelectedIndex > 0)
        {
            Int32 WCompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
            string WClientCode = (ddlClient.SelectedValue);
            LoadGridClient(WCompanyId, WClientCode);
            pnlAdd.Visible = true;
            Pnlgv.Visible = true;
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Pls Select Company And the Client" + "');", true);
            Pnlgv.Visible = false;
            pnlAdd.Visible = false;
        }
    }
    #region GridEditing
    protected void GrdClient_RowCommand(object sender, GridViewCommandEventArgs e)
    {
     
        string WProjectCode = (e.CommandArgument).ToString();
        foreach(GridViewRow row in GrdProjMaster.Rows)
        {
            if (((Label)row.FindControl("lblProjectCode")).Text == WProjectCode)
            {
                txtClientProjectId.Text = ((Label)row.FindControl("lblClientProjectId")).Text;
                txtProjectCode.Text = ((Label)row.FindControl("lblProjectCode")).Text;
                txtProjectCode.Enabled = false; //Project code cannot be allowed to change scs 260323
                txtProjectName.Text = ((Label)row.FindControl("lblProjectName")).Text;
                txtProjectLocation.Text = ((Label)row.FindControl("lblProjectLocation")).Text;

                txtProjectCity.Text = ((Label)row.FindControl("lblProjectCity")).Text;
                txtStartDate.Text = ((Label)row.FindControl("lblStartDate")).Text;
                txtEndDate.Text = ((Label)row.FindControl("lblEndDate")).Text;
                txtTenderPeriod.Text = ((Label)row.FindControl("lblTenderDate")).Text;
                txtConstructionEndDate.Text = ((Label)row.FindControl("lblConstructionCompleted")).Text;
                txtConstructionStart.Text = ((Label)row.FindControl("lblConstructionStart")).Text;

                txtDeviationMonths.Text = ((Label)row.FindControl("lblDeviation")).Text;
                string WState = ((Label)row.FindControl("lblStateName")).Text;
                Int32 WStateId = Convert.ToInt32(((Label)row.FindControl("lblStateId")).Text);
                string WSectgrpName = (((Label)row.FindControl("lblProjectSectorGroupName")).Text);
                Int32 WSectId = Convert.ToInt32(((Label)row.FindControl("lblProjectSectorSubGroupId")).Text);
                LoadState();
                foreach (StateMasterMsg gs in StateList)
                {
                    if (gs.StateName == WState)
                    {
                        ddlState.SelectedValue = gs.StateId.ToString();
                        break;
                    }
                }
                LoadSector();
                LoadddlSectorGroup(SectList);
                foreach (SectorMsg st in SectList)
                {
                    if (st.ProjectSectorGroupName == WSectgrpName)
                    {
                        ddlSectorGroup.SelectedValue = st.ProjectSectorGroupId.ToString();
                        break;
                    }
                }
                LoadddlSector(SectList);
                foreach (SectorMsg st in SectList)
                {
                    if (st.ProjectSectorSubGroupId == WSectId)
                    {
                        ddlSectorClass.SelectedValue = st.ProjectSectorSubGroupId.ToString();
                        break;
                    }
                }
               
                chkIsActive.Checked = ((CheckBox)row.FindControl("chkActive")).Checked;
                chkIsActive.Visible = true;
                btnSave.Text = "Update";
                pnlAdd.GroupingText = "Update Client";
                Pnlgv.Visible = false;
                pnlAdd.Visible = true;
                break;
            }
            
        }

        

    }


    #endregion
    #endregion
    #region Methods
    private void LoadCompany()
    {
        //CmpList = Bus.CompanyMasterSelect();
        CmpList = Lib.LoadCompany();
        //if (BaseMsg.IsAdmin == false)
        //{
        //    CmpList = (from cmp in CmpList where cmp.CompanyId == BaseMsg.CompanyId select cmp).ToList();
        //}
        if (CmpList != null && CmpList.Count > 0)
        {
            ddlCompany.DataSource = CmpList;
            ddlCompany.DataBind();
            ddlCompany.Items.Insert(0, "--Select Please--");
            ddlCompany.SelectedIndex = 0;
            if (CmpList.Count == 1)
            {
                ddlCompany.SelectedIndex = 1;
               // ddlCompany.Enabled = false;
                WCompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                LoadClient(WCompanyId);
            }
            else
            {
                ddlCompany.Enabled = true;
            }

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.ErrCompanyData + "');", true);
            // ddlCompany.Items.Insert(0, "--Select Please--");
        }

    }
    private void LoadClient(Int32 CompanyId)//To load data into grid.
    {
        ClientMsg Cmp = new ClientMsg();
        Cmp.Flag = "R";
        Cmp.CreatedBy = BaseMsg.EmployeeCode;
        Cmp.CompanyId = WCompanyId;
        ClientList = Bus.MasClientInsertUpdateandDelete(Cmp);
        ClientList = (from Cl in ClientList where Cl.ClientType.Trim().ToUpper() == "CLIENT" select Cl).ToList();
        if (ClientList.Count > 0)
        {
            ddlClient.DataSource = ClientList;
            ddlClient.DataBind();
            ddlClient.Items.Insert(0, "-- Select Please --");
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Pls Create Clients for the selected Company" + "');", true);
        }
    }

    private void LoadState()
    {
        EmployeeMasterMsg EmpMsg = new EmployeeMasterMsg();
        StateList = Bus.StateMasterSelect(EmpMsg);

        ddlState.DataTextField = "StateName";
        ddlState.DataValueField = "StateId";
        ddlState.DataSource = StateList;
        ddlState.DataBind();
        ddlState.Items.Insert(0, "-- Select Please --");
    }
    private void LoadGridClient(Int32 CompanyId, string WClientCode)//To load data into grid.
    {
        List<ProjectMsg> projList = new List<ProjectMsg>();
        ProjectMsg Cmp = new ProjectMsg();
        Cmp.Flag = "R";
        Cmp.CreatedBy = BaseMsg.EmployeeCode;
        Cmp.CompanyId = WCompanyId;
        Cmp.ClientCode = WClientCode;
        projList = Bus.MasClientProjectMasterInsertandUpdate(Cmp);
        if (projList.Count > 0)
        {
            GrdProjMaster.DataSource = projList;
            GrdProjMaster.DataBind();
            Pnlgv.Visible = true;
        }
        else
        {
        }
    }
    private void LoadSector()
    {
        SectorMsg StMsg = new SectorMsg();
        StMsg.Flag = "R";
        SectList = Bus.MasProjectSectorClassInsertandUpdate(StMsg);
        
    }
    private void LoadddlSectorGroup(List<SectorMsg> SecList)
    {
        var SGrpList = (from sgrp in SecList select new { sgrp.ProjectSectorGroupId, sgrp.ProjectSectorGroupName }).Distinct().ToList();
        ddlSectorGroup.DataSource = SGrpList;
        ddlSectorGroup.DataBind();
        ddlSectorGroup.Items.Insert(0, "--Select Please--");
        ddlSectorGroup.SelectedIndex = 0;
    }

    private void LoadddlSector(List<SectorMsg> SecList)
    {
        if (SecList != null && SecList.Count > 0)
        {
            if (ddlSectorGroup.SelectedIndex <= 0)
            {
                ddlSectorClass.DataSource = SecList;
                ddlSectorClass.DataBind();
                ddlSectorClass.Items.Insert(0, "--Select Please--");
                ddlSectorClass.SelectedIndex = 0;
            }
            else
            {
                List<SectorMsg> seclst = new List<SectorMsg>();
                seclst = (from sec in SecList where sec.ProjectSectorGroupId == Convert.ToInt32(ddlSectorGroup.SelectedValue) select sec).ToList();
                ddlSectorClass.DataSource = seclst;
                ddlSectorClass.DataBind();
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.ErrCompanyData + "');", true);
            // ddlCompany.Items.Insert(0, "--Select Please--");
        }

    }
 
    private void ClientSave()
    {
        System.Globalization.DateTimeFormatInfo dateinfo = new System.Globalization.DateTimeFormatInfo();
        dateinfo.ShortDatePattern = "dd-MM-yyyy";
        WCompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
        List<ProjectMsg> WProjList = new List<ProjectMsg>();
        ProjectMsg Cmp = new ProjectMsg();
        if (btnSave.Text.ToUpper() == "SAVE")
        {
            Cmp.Flag = "I";
           Cmp.ClientProjectId = 0;
        }
        else
        {
            Cmp.Flag = "U";
            Cmp.IsActive = chkIsActive.Checked;
            Cmp.ClientProjectId = Convert.ToInt32(txtClientProjectId.Text);
        }
        Cmp.CompanyId = WCompanyId;
        Cmp.ClientCode = ddlClient.SelectedValue.Trim();
        Cmp.ProjectCode = txtProjectCode.Text.Trim();
        Cmp.ProjectName = txtProjectName.Text.Trim();
        Cmp.ProjectLocation = txtProjectLocation.Text.Trim();
        Cmp.ProjectCity = txtProjectCity.Text.Trim();
        Cmp.StartDate = Convert.ToDateTime(txtStartDate.Text.Trim(),dateinfo);
        Cmp.EndDate = Convert.ToDateTime(txtEndDate.Text.Trim(), dateinfo);
        Cmp.ConstructionStart = Convert.ToDateTime(txtConstructionStart.Text.Trim(), dateinfo);
        Cmp.ConstructionCompleted = Convert.ToDateTime(txtConstructionEndDate.Text.Trim(), dateinfo);
        Cmp.TenderPeriod = Convert.ToDateTime(txtTenderPeriod.Text.Trim(), dateinfo);
        try
        {
            Cmp.DeviationMonths = Convert.ToInt32(txtDeviationMonths.Text.Trim());
        }
        catch
        {
            Cmp.DeviationMonths = 0;
        }
        Cmp.StateName = ddlState.SelectedItem.ToString(); //txtState.Text.Trim();
        Cmp.StateId = Convert.ToInt32(ddlState.SelectedValue);
              
        Cmp.IsActive = chkIsActive.Checked; //scs 230221
        Cmp.CreatedBy = BaseMsg.EmployeeCode;
        Cmp.ProjectSectorSubGroupId = Convert.ToInt32(ddlSectorClass.SelectedValue);
        WProjList = Bus.MasClientProjectMasterInsertandUpdate(Cmp);
        //Output Dispay
        foreach (ProjectMsg cmp in WProjList)
        {
            if (cmp.ProjectResult == "0")
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.SuccessFullySaved + "');", true);
                AllClear();
                //pnlPendind.Enabled = false;
                btnSave.Text = "Save";
                txtProjectCode.Enabled = true; //Project code cannot be allowed to change scs 260323
                break;

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + cmp.ProjectResult + "');", true);
                break;
            }
        }
    }
 
 
    #region Validation

    private int IsValidSave()
    {
        int Error = 0;
        string DisplayError = "";
         System.Globalization.DateTimeFormatInfo dateinfo = new System.Globalization.DateTimeFormatInfo();
        dateinfo.ShortDatePattern = "dd-MM-yyyy";

        if (ddlCompany.SelectedIndex == 0 && ddlClient.SelectedIndex ==0)
        {
            DisplayError = DisplayError + " Company and Client are to be selected .";
            //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.ErrorClientCode + "');", true);
            Error = 1;
        }
        if (txtProjectCode.Text.Trim() == "" || Convert.ToInt32(txtProjectCode.Text.Trim().Length) == 0)
        {
            DisplayError = DisplayError + "--" + " Project Code is Mandatory . ";
            // ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.ErrorClientShortName + "');", true);
            Error = 1;
        }
        if (txtProjectName.Text.Trim() == "" || Convert.ToInt32(txtProjectName.Text.Trim().Length) == 0)
        {
            DisplayError = DisplayError + "--" + " Project Name is Mandatory . ";
            // ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.ErrorClientName + "');", true);
            Error = 1;
        }
        if (ddlSectorClass.SelectedIndex == 0)
        {
            DisplayError = DisplayError + "--" + " Sector Class has to be selected . ";
            Error = 1;
        }
        if (ddlState.SelectedIndex ==0)
        {
            DisplayError = DisplayError + "--" + " State has to be selected . ";
            Error = 1;
        }
      
        try
        {
            DateTime StDate = Convert.ToDateTime(txtStartDate.Text, dateinfo);
            DateTime EndDate = Convert.ToDateTime(txtEndDate.Text, dateinfo);
            DateTime ConstStDate = Convert.ToDateTime(txtConstructionStart.Text, dateinfo);
            DateTime ConstEndDate = Convert.ToDateTime(txtConstructionEndDate.Text, dateinfo);
            DateTime TendPer = Convert.ToDateTime(txtTenderPeriod.Text, dateinfo);
            if (EndDate < StDate) 
            {
                DisplayError = DisplayError + "--" + " End Date Cannot be Less Than Start Date ";
                Error = 1;
            }
            if (TendPer < StDate || TendPer > EndDate)
            {
                DisplayError = DisplayError + "--" + " Tender Period Should be more than Start date and Less than End Date ";
                Error = 1;
            }

            if (ConstEndDate < ConstStDate)
            {
                DisplayError = DisplayError + "--" + " Construction End Date Cannot be Less Than Construction Start Date ";
                Error = 1;
            }
        }
        catch
        {
            DisplayError = DisplayError + "--" + " Kindly Check Date Format in Date Fields ";
            // ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.ErrorClientShortName + "');", true);
            Error = 1;
        }
        if (Error == 1)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + DisplayError + "');", true);
        }
        return Error;
    }

 

    #endregion
    #region Clear
    public void AllClear()
    {
        txtClientProjectId.Text = "";
        txtProjectName.Text = "";
        txtProjectCode.Text = "";
        txtProjectLocation.Text = "";
        txtProjectCity.Text = "";
        txtStartDate.Text = "";
        txtEndDate.Text = "";
        ddlState.SelectedIndex = 0;
        
        txtConstructionStart.Text = "";
        txtConstructionEndDate.Text = "";
        txtTenderPeriod.Text = "";

        Pnlgv.Visible = true;
        pnlAdd.Visible = true;
        pnlPendind.Enabled = true;
        
        LoadGridClient(Convert.ToInt32(ddlCompany.SelectedValue), ddlClient.SelectedValue); //SCS 210301
        pnlPendind.Enabled = true;
        chkIsActive.Visible = false;
        pnlAdd.GroupingText = "Add Client";

    }
    #endregion
    #endregion        
    }