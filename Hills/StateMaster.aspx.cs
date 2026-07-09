using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Resources;

public partial class StateMaster : System.Web.UI.Page
{ 
    #region Declaration
    ProcessBus Bus = new ProcessBus();
    List<StateMasterMsg> StateList = new List<StateMasterMsg>();
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
            LoadGrdStateMaster();
            if (StateList.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.MsgForGrdnotLoad + "');", true);
                Pnlgv.Visible = false;
                pnlAdd.Visible = true;

            }
        }        
       
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
       
        //if (HidUpdateCount.Value == "1")
        //{
        //    StateUpdate();
        //    HidUpdateCount.Value = "0";
        //    return;
        //}
        
        if (!user.HasPermission(ProgramName, UserPermission.CanCreate.ToString()))
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.CreatePermissionRestricted + "');", true);
        }
        else
        {
            if (IsValidSave() == 0)
            {
                StateSave();
            }
        }
    }
    #endregion
    #region Methods
    private void LoadGrdStateMaster()
    {
        StateMasterMsg State = new StateMasterMsg();
        State.Flag = "R";
        StateList = Bus.MasStateInsertUpdateandDelete(State);
        GrdStateMaster.DataSource = "";
        GrdStateMaster.DataSource = StateList;
        GrdStateMaster.DataBind();
        if (!user.HasPermission(ProgramName, UserPermission.CanEdit.ToString()))
        {
            GrdStateMaster.Columns[GrdStateMaster.Columns.Count - 2].Visible = false;
        }
        if (!user.HasPermission(ProgramName, UserPermission.CanDelete.ToString()))
        {
            GrdStateMaster.Columns[GrdStateMaster.Columns.Count - 1].Visible = false;
        }
    }

    private void StateSave()
    {
        StateMasterMsg State = new StateMasterMsg();
        State.Flag = "I";

        State.StateName = txtStatetName.Text.Trim();
        State.StateShortName = txtStateShortName.Text.Trim();
        //State.CreatedBy = BaseMsg.EmployeeCode;
        StateList = Bus.MasStateInsertUpdateandDelete(State);
        //Output Dispay
        foreach (StateMasterMsg StateSave in StateList)
        {
            if (StateSave.StateResult == "0")
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.SuccessFullySaved + "');", true);
                AllClear();
                break;

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + StateSave.StateResult + "');", true);
                break;
            }
        }
    }
    public void StateUpdate()
    {
        GridViewRow row = GrdStateMaster.Rows[UpdateIndex];
        StateMasterMsg State = new StateMasterMsg();
        State.Flag = "U";

        TextBox txtStateId = (TextBox)row.FindControl("txtStateId");
        TextBox txtStatename = (TextBox)row.FindControl("txtStateName");
        TextBox txtStateshname = (TextBox)row.FindControl("txtStatehName");
       // CheckBox chkActive = (CheckBox)row.FindControl("chkIsActive");

        State.StateId = Convert.ToInt32(txtStateId.Text.Trim());
        State.StateName = txtStatename.Text.Trim();
        State.StateShortName = txtStateshname.Text.Trim();        
        StateList = Bus.MasStateInsertUpdateandDelete(State);
        GrdStateMaster.EditIndex = -1;

        foreach (StateMasterMsg StateUpdate in StateList)
        {
            if (StateUpdate.StateResult == "0")
            {
                //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + StackResource.StateUpdatedSuccessfully + "');", true);

                AllClear();
                break;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + StateUpdate.StateResult + "');", true);
                break;
            }
        }
    }
  
    #endregion
    #region Clear
    public void AllClear()
    {

        txtStatetName.Text = "";
        txtStateShortName.Text = "";
        LoadGrdStateMaster();

    }
    #endregion
    #region Validation
    private int IsValidSave()
    {

        int Error = 0;
         string DisplayError = "";
         if ((txtStatetName.Text.Trim() == "" || txtStatetName.Text.Trim().Length.ToString() == "0"))
        {
            DisplayError = DisplayError + AgilerMail.Errorstatename;
            Error = 1;
        }
         if (txtStateShortName.Text.Trim() == "" || txtStateShortName.Text.Trim().Length.ToString() == "0")
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
    #endregion
    #region GrdEdit

    protected void GrdStateMaster_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        UpdateIndex = e.RowIndex;
        GridViewRow row = GrdStateMaster.Rows[UpdateIndex];
        TextBox txtStatename = (TextBox)row.FindControl("txtStateName");
        TextBox txtStateshname = (TextBox)row.FindControl("txtStatehName");
        if (IsValidGridSave(txtStatename.Text.Trim(), txtStateshname.Text.Trim()) == 0)
        {
            StateUpdate();
        }
    }
    
    protected void GrdStateMaster_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GrdStateMaster.EditIndex = -1;
        LoadGrdStateMaster();
    }
    protected void GrdStateMaster_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GrdStateMaster.EditIndex = e.NewEditIndex;
        LoadGrdStateMaster();
        GridViewRow row = GrdStateMaster.Rows[GrdStateMaster.EditIndex];
        TextBox Statename = (TextBox)row.FindControl("txtStateName");
        Statename.Focus();
    }

    #endregion
}