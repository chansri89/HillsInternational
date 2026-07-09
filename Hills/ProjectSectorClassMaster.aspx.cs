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

public partial class ProjectSectorClassMaster : System.Web.UI.Page
{
    #region Declaration
    ProcessBus Bus = new ProcessBus(); LibFunctions Lib = new LibFunctions();
   // List<EmployeeMasterMsg> EmpList = new List<EmployeeMasterMsg>();
    private static List<SectorMsg> SecList = new List<SectorMsg>();
    //List<EnterpriseMasterMsg> EnterpriseList = new List<EnterpriseMasterMsg>(); //scs 2704
    //List<StateMasterMsg> StateList = new List<StateMasterMsg>();
    //private static List<ClientTypeMsg> GCusTypLst = new List<ClientTypeMsg>();
    //private static List<CompanyMessage> CmpList = new List<CompanyMessage>();
    //private static List<GSTINState> GSTSt = new List<GSTINState>();
    //List<ClientTypeMasterMsg> ClientTypeList = new List<ClientTypeMasterMsg>();
    //public static List<ClientinStateMasterMsg> ClientstateList = new List<ClientinStateMasterMsg>();
    public static int DeleteIndex = 0;
    public static int UpdateIndex = 0;
    UserAccess user = new UserAccess();
    public static string ProgramName = string.Empty;
    BaseClass BaseMsg = new BaseClass();
    private Int32 WSecGrpId = 0;
    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        ProgramName = System.IO.Path.GetFileName(Request.PhysicalPath);
        if (!Page.IsPostBack)
        {
            //Pnlgv.Visible = false;
            //pnlAdd.Visible = false;
            LoadProjectSectorGroup();
            LoadGridSectorClass();
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        
        AllClear();
       // Pnlgv.Visible = false;
        pnlAdd.Visible = false;
    }
    //protected void ddlProjectSectorGroupChanged(object sender, EventArgs e)
    //{
    //    if (ddlProjectSectorGroup.SelectedIndex > 0)
    //    {
    //        WSecGrpId = Convert.ToInt32(ddlProjectSectorGroup.SelectedValue);
    //        Pnlgv.Visible = true;
    //        pnlAdd.Visible = true;
    //        LoadGridSectorClass(WSecGrpId);
    //    }
    //    else
    //    {
    //        ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Location Data Not Available" + "');", true);
    //        Pnlgv.Visible = false;
    //        pnlAdd.Visible = false;
    //    }
    //}
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (IsValidSave() == 0)
        {
            if (!user.HasPermission(ProgramName, UserPermission.CanCreate.ToString()) && btnSave.Text.ToUpper() == "SAVE")
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.CreatePermissionRestricted + "');", true);
            }
            else if (!user.HasPermission(ProgramName, UserPermission.CanEdit.ToString()) && btnSave.Text.ToUpper() == "UPDATE")
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.EditPermissionRestricted + "');", true);
                AllClear();
            }
            else
            {
                SectorClassSave();
            }
        }
    }
 
    protected void btnFilter_Click(object sender, EventArgs e)
    {
       LoadGridSectorClass();
    }
    #region GridEditing
    protected void GrdSectorClass_RowCommand(object sender, GridViewCommandEventArgs e)
    {
     
        Int32 WSecProjsubgrpId = Convert.ToInt32((e.CommandArgument).ToString());
        foreach (GridViewRow row in GrdSectorClass.Rows)
        {
            if (((Label)row.FindControl("lblProjectSectorSubGroupId")).Text == WSecProjsubgrpId.ToString())
            {
                txtProjectSectorClass.Text = ((LinkButton)row.FindControl("lnkSectorClass")).Text;
                string WProjSecGrp = ((Label)row.FindControl("lblProjectSectorGroupName")).Text;
                txtProjectSectorSubgroupId.Text = ((Label)row.FindControl("lblProjectSectorSubGroupId")).Text;
                LoadProjectSectorGroup();
                foreach (SectorMsg gs in SecList)
                {
                    if (gs.ProjectSectorGroupName == WProjSecGrp)
                    {
                        ddlProjectSectorGroup.SelectedValue = gs.ProjectSectorGroupId.ToString();
                        break;
                    }
                }
           
                chkIsActive.Checked = ((CheckBox)row.FindControl("chkActive")).Checked;
                chkIsActive.Visible = true;
                btnSave.Text = "Update";
                pnlAdd.GroupingText = "Update Sector Class";
                Pnlgv.Visible = false;
                pnlAdd.Visible = true;
                break;
            }
            
        }

        

    }


    #endregion
    #endregion
    #region Methods
    private void LoadProjectSectorGroup()
    {
        SectorMsg Sec = new SectorMsg();
        Sec.Flag = "R";
        SecList = Bus.MasProjectSectorGroupInsertandUpdate(Sec);

        if (SecList != null && SecList.Count > 0)
        {
            ddlProjectSectorGroup.DataSource = SecList;
            ddlProjectSectorGroup.DataBind();
            ddlProjectSectorGroup.Items.Insert(0, "--Select Please--");
            ddlProjectSectorGroup.SelectedIndex = 0;
            if (SecList.Count == 1)
            {
                ddlProjectSectorGroup.SelectedIndex = 1;
                ddlProjectSectorGroup.Enabled = false;
               // LoadGridSectorClass();
            }
            else
            {
                ddlProjectSectorGroup.Enabled = true;
            }
            Pnlgv.Visible = true;
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.ErrCompanyData + "');", true);
            // ddlCompany.Items.Insert(0, "--Select Please--");
        }

    }
 
  
    private void LoadGridSectorClass()//To load data into grid.
    {
        SectorMsg Cmp = new SectorMsg();
        Cmp.Flag = "R";
        SecList = Bus.MasProjectSectorClassInsertandUpdate(Cmp);
        SectorFilter(SecList);
       
    }
    private void SectorFilter(List<SectorMsg> cli)
    {

        List<SectorMsg> FSecList = new List<SectorMsg>();
        if (txtFilter.Text.Trim().Length == 0)
        {
            FSecList = cli.ToList();
        }
        else
        {
            foreach (SectorMsg cm in cli)
            {
                if (cm.ProjectSectorClass.ToUpper().Contains(txtFilter.Text.Trim().ToUpper()))
                {
                    FSecList.Add(cm);
                }
            }

        }
        if (FSecList.Count == 0)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "No Client for the Filter Found .." + "');", true);
            FSecList = cli.ToList();
        }
        GrdSectorClass.DataSource = "";
        GrdSectorClass.DataSource = FSecList;
        GrdSectorClass.DataBind();
        Pnlgv.Visible = true;
        pnlAdd.Visible = true;

    }
    private void SectorClassSave()
    {
        List<SectorMsg> seList = new List<SectorMsg>();
        WSecGrpId = Convert.ToInt32(ddlProjectSectorGroup.SelectedValue);
        SectorMsg Cmp = new SectorMsg();
        if (btnSave.Text.ToUpper() == "SAVE")
        {
            Cmp.Flag = "I";
            Cmp.ProjectSectorSubGroupId = 0;
        }
        else
        {
            Cmp.Flag = "U";
            Cmp.IsActive = chkIsActive.Checked;
            Cmp.ProjectSectorSubGroupId = Convert.ToInt32(txtProjectSectorSubgroupId.Text);
        }
        Cmp.ProjectSectorGroupId = WSecGrpId;
        Cmp.ProjectSectorGroupName = ddlProjectSectorGroup.SelectedItem.ToString();
        Cmp.ProjectSectorClass = txtProjectSectorClass.Text.Trim();
            
        Cmp.IsActive = chkIsActive.Checked; //scs 230221
       // Cmp.CreatedBy = BaseMsg.EmployeeCode;

        seList = Bus.MasProjectSectorClassInsertandUpdate(Cmp);
        //Output Dispay
        foreach (SectorMsg cmp in seList)
        {
            if (cmp.Result == "0")
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.SuccessFullySaved + "');", true);
                AllClear();
                btnSave.Text = "Save";
                break;

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + cmp.Result + "');", true);
                break;
            }
        }
    }
 
 
    #region Validation

    private int IsValidSave()
    {
        int Error = 0;
        string DisplayError = "";
        if (txtProjectSectorClass.Text.Trim() == "" || txtProjectSectorClass.Text.Length == 0)
        {
            DisplayError = DisplayError + " Sector Class is Mandatory .";
            Error = 1;
        }
        if (ddlProjectSectorGroup.SelectedIndex == 0)
        {
            DisplayError = DisplayError + "--" + " Sectro Group is Mandatory.. Pls Select . ";
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
        txtProjectSectorClass.Text = "";
        ddlProjectSectorGroup.SelectedIndex = 0; 
        Pnlgv.Visible = true;
        pnlAdd.Visible = true;
        pnlPendind.Enabled = true;
        LoadGridSectorClass(); //SCS 210301
        pnlPendind.Enabled = true;
        chkIsActive.Visible = false;
        pnlAdd.GroupingText = "Add Sector Class";

    }
    #endregion
    #endregion        
    }