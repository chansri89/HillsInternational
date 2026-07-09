using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Resources;

public partial class ProjectSectorGroupMaster : System.Web.UI.Page
{ 
    #region Declaration
    ProcessBus Bus = new ProcessBus();
    List<SectorMsg> SeList = new List<SectorMsg>();
    public static int DeleteIndex = 0;
    public static int UpdateIndex = 0;
    UserAccess user = new UserAccess();
    public static string ProgramName = string.Empty;
    BaseClass BaseMsg = new BaseClass();
    #endregion
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        ProgramName = System.IO.Path.GetFileName(Request.PhysicalPath);
        if (!Page.IsPostBack)
        {
            LoadGrdSectorGrp();
            pnlAddClass.Visible = false;
            pnlClass.Visible = false;
            //if (StateList.Count == 0)
            //{
            //    ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.MsgForGrdnotLoad + "');", true);
            //    Pnlgv.Visible = false;
            //    pnlAdd.Visible = true;

            //}
        }        
       
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
            
        if (!user.HasPermission(ProgramName, UserPermission.CanCreate.ToString()))
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.CreatePermissionRestricted + "');", true);
        }
        else
        {
            if (IsValidSave() == 0)
            {
                SectorGrpSave();
            }
        }
    }
    protected void btnSaveClass_Click(object sender, EventArgs e)
    {
        if (IsValidSaveClass() == 0)
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
    protected void btnBack_Click(object sender, EventArgs e)
    {
        pnlAddClass.Visible = false;
        pnlClass.Visible = false;
        pnlAdd.Visible = true;
        Pnlgv.Visible = true;
    }
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        LoadGridSectorClass();
    }
    #endregion
    #region Methods
    private void LoadGrdSectorGrp()
    {
        SectorMsg st = new SectorMsg();
        st.Flag = "R";
        SeList = Bus.MasProjectSectorGroupInsertandUpdate(st);
        GrdSectorGrp.DataSource = "";
        GrdSectorGrp.DataSource = SeList;
        GrdSectorGrp.DataBind();
        if (!user.HasPermission(ProgramName, UserPermission.CanEdit.ToString()))
        {
            GrdSectorGrp.Columns[GrdSectorGrp.Columns.Count - 2].Visible = false;
        }
        if (!user.HasPermission(ProgramName, UserPermission.CanDelete.ToString()))
        {
            GrdSectorGrp.Columns[GrdSectorGrp.Columns.Count - 1].Visible = false;
        }
        ddlProjectSectorGroup.DataTextField = "ProjectSectorGroupName";
        ddlProjectSectorGroup.DataValueField = "ProjectSectorGroupId";
        ddlProjectSectorGroup.DataSource = SeList;
        ddlProjectSectorGroup.DataBind();
        ddlProjectSectorGroup.Items.Insert(0, "-- Select Please --");
    }
    private void LoadGridSectorClass()//To load data into grid.
    {
        List<SectorMsg> SecList = new List<SectorMsg>();
        SectorMsg Cmp = new SectorMsg();
        Cmp.Flag = "R";
        SecList = Bus.MasProjectSectorClassInsertandUpdate(Cmp);
        SectorFilter(SecList);
        //GrdSectorClass.DataSource = "";
        //GrdSectorClass.DataSource = SecList;
        //GrdSectorClass.DataBind();
    }
    private void SectorClassSave()
    {
        List<SectorMsg> seList = new List<SectorMsg>();
       
        SectorMsg Cmp = new SectorMsg();
        if (btnSaveClass.Text.ToUpper() == "SAVE CLASS")
        {
            Cmp.Flag = "I";
            Cmp.ProjectSectorSubGroupId = 0;
        }
        //else
        //{
        //    Cmp.Flag = "U";
        //Cmp.IsActive = chkIsActive.Checked;
        //Cmp.ProjectSectorSubGroupId = Convert.ToInt32(txtProjectSectorSubgroupId.Text);
        //}
        Cmp.ProjectSectorGroupId =  Convert.ToInt32(ddlProjectSectorGroup.SelectedValue);;
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

    private void SectorGrpSave()
    {
        SectorMsg Se = new SectorMsg();
        Se.Flag = "I";

        Se.ProjectSectorGroupName = txtProjectSectorGroupName.Text.Trim();
        //Se.StateShortName = txtStateShortName.Text.Trim();
        //State.CreatedBy = BaseMsg.EmployeeCode;
        SeList = Bus.MasProjectSectorGroupInsertandUpdate(Se);
        //Output Dispay
        foreach (SectorMsg StateSave in SeList)
        {
            if (StateSave.Result == "0")
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.SuccessFullySaved + "');", true);
                AllClear();
                break;

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + StateSave.Result + "');", true);
                break;
            }
        }
    }
    private void SectorGrpUpdate()
    {
        GridViewRow row = GrdSectorGrp.Rows[UpdateIndex];
        SectorMsg SE = new SectorMsg();
        SE.Flag = "U";

        TextBox txtSecGrpId = (TextBox)row.FindControl("txtProjectSectorGroupId");
        TextBox txtSecGrpname = (TextBox)row.FindControl("txtProjectSectorGroupName");
       // CheckBox chkActive = (CheckBox)row.FindControl("chkIsActive");

        SE.ProjectSectorGroupId = Convert.ToInt32(txtSecGrpId.Text.Trim());
        SE.ProjectSectorGroupName = txtSecGrpname.Text.Trim();
       
        SeList = Bus.MasProjectSectorGroupInsertandUpdate(SE);
        GrdSectorGrp.EditIndex = -1;

        foreach (SectorMsg SecUpdate in SeList)
        {
            if (SecUpdate.Result == "0")
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Successfully UpDated" + "');", true);

                AllClear();
                break;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + SecUpdate.Result + "');", true);
                break;
            }
        }
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
        //Pnlgv.Visible = true;
        //pnlAdd.Visible = true;

    }
    #endregion
    #region Clear
    public void AllClear()
    {

        txtProjectSectorGroupName.Text = "";
        txtProjectSectorClass.Text = "";
        txtProjectSectorSubgroupId.Text = "";
        LoadGrdSectorGrp();
        pnlAddClass.Visible = false;
        pnlClass.Visible = false;
        pnlAdd.Visible = true;
        Pnlgv.Visible = true;

    }
    #endregion
    #region Validation
    private int IsValidSave()
    {

        int Error = 0;
         string DisplayError = "";
         if ((txtProjectSectorGroupName.Text.Trim() == "" || txtProjectSectorGroupName.Text.Trim().Length.ToString() == "0"))
        {
            DisplayError = DisplayError + AgilerMail.Errorstatename;
            Error = 1;
        }
        
        if (Error == 1)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + DisplayError + "');", true);
        }
        return Error;
    }

    private int IsValidGridSave(string StateName,string StateShortName)
    {

        int Error = 0;
        string DisplayError = "";
        if ((StateName.Trim() == "" || StateName.Trim().Length.ToString() == "0"))
        {
            DisplayError = DisplayError + AgilerMail.Errorstatename;
            Error = 1;
        }
        if (StateShortName.Trim() == "" || StateShortName.Trim().Length.ToString() == "0")
        {
            DisplayError = DisplayError + "--" + AgilerMail.ErrStateShortName;
            Error = 1;
        }
        if (Error == 1)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + DisplayError + "');", true);
        }
        return Error;
    }
    private int IsValidSaveClass()
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
    #region GrdEdit
    protected void GrdSectorGrp_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToUpper() == "ADDCLASS")
        {
            Int32 WProjectSectorGroupId = Convert.ToInt32((e.CommandArgument).ToString());
            LoadGridSectorClass();
            pnlAddClass.Visible = true;
            pnlClass.Visible = true;
            pnlAdd.Visible = false;
            Pnlgv.Visible = false;
            ddlProjectSectorGroup.SelectedValue = WProjectSectorGroupId.ToString();
        }
        
    }
    protected void GrdSectorGrp_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        UpdateIndex = e.RowIndex;
        GridViewRow row = GrdSectorGrp.Rows[UpdateIndex];
        TextBox txtProjectSectorName = (TextBox)row.FindControl("txtProjectSectorGroupName");
        if (txtProjectSectorName.Text == "" || txtProjectSectorName.Text.Length == 0)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Sector Name cannot be Blank" + "');", true);
        }
        else
        {
            SectorGrpUpdate();
            GrdSectorGrp.Columns[2].Visible = true;
        }
    }
    
    protected void GrdSectorGrp_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GrdSectorGrp.EditIndex = -1;
        LoadGrdSectorGrp();
        GrdSectorGrp.Columns[2].Visible = true;
    }
    protected void GrdSectorGrp_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GrdSectorGrp.Columns[2].Visible = false;
        GrdSectorGrp.EditIndex = e.NewEditIndex;
        LoadGrdSectorGrp();
        GridViewRow row = GrdSectorGrp.Rows[GrdSectorGrp.EditIndex];
        TextBox ProjSectorName = (TextBox)row.FindControl("txtProjectSectorGroupName");
        ProjSectorName.Focus();
    }

    #endregion
}