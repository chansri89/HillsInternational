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

public partial class CompanyMaster : System.Web.UI.Page
{
    #region Declaration
    ProcessBus Bus = new ProcessBus(); LibFunctions Lib = new LibFunctions();
    List<EmployeeMasterMsg> EmpList = new List<EmployeeMasterMsg>();
    List<CompanyMessage> CompanyList = new List<CompanyMessage>();
    List<EnterpriseMasterMsg> EnterpriseList = new List<EnterpriseMasterMsg>(); //scs 2704
    List<StateMasterMsg> StateList = new List<StateMasterMsg>();
    List<LocationMsg> LocationList = new List<LocationMsg>();
    
    //List<LocationTypeMasterMsg> LocationTypeList = new List<LocationTypeMasterMsg>();
    //public static List<LocationinStateMasterMsg> LocationstateList = new List<LocationinStateMasterMsg>();
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
            LoadGridCompany();
            LoadParentCompanyName();
            LoadState();
            LoadEnterprise();
            //LoadLoccationstate();
            LoadddlCompanyType();
            if (CompanyList.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.MsgForGrdnotLoad + "');", true);
                Pnlgv.Visible = false;
                pnlAdd.Visible = true;

            }
            else
            {
                Pnlgv.Visible = true;
                pnlAdd.Visible = true;
            }
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
                CompanySave();
            }
        }
    }
    #region GridEditing
    protected void GrdCompanyMaster_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GrdCompanyMaster.EditIndex = e.NewEditIndex;
        GridViewRow row = GrdCompanyMaster.Rows[GrdCompanyMaster.EditIndex];
        Label lblCmpType = (Label)row.FindControl("lblCompnyType");
        Label lblEntName = (Label)row.FindControl("lblEntName"); //scs 270714
        Label lblParentCmpName = (Label)row.FindControl("lblParentCompanyName");
        Label lblStateShortName = (Label)row.FindControl("lblStateShortName");
        Label lblLocationName = (Label)row.FindControl("lblLocationName");
        EmployeeMasterMsg emp = new EmployeeMasterMsg();
        LoadGridCompany();
     
        foreach (GridViewRow gvr in GrdCompanyMaster.Rows)
        {
            if (gvr.RowIndex == GrdCompanyMaster.EditIndex)
            {
                DropDownList ddlParentCmpName = (DropDownList)gvr.FindControl("ddlParentCompanyName");
                //ddlParentCmpName.Items.Insert(0, new ListItem("--Parent--"));
                ddlParentCmpName.Focus();
                foreach (CompanyMessage cmp in CompanyList)
                {
                    if (cmp.CompanyName.Trim() == lblParentCmpName.Text.Trim())
                    {
                        ddlParentCmpName.SelectedValue = cmp.CompanyCode;
                        break;
                    }
                    ddlParentCmpName.SelectedIndex = 0;
                }

                DropDownList ddlEntName = (DropDownList)gvr.FindControl("ddlEntName");
                //ddlParentCmpName.Items.Insert(0, new ListItem("--Parent--"));
                ddlEntName.Focus();
                foreach (EnterpriseMasterMsg ent in EnterpriseList)
                {
                    if (ent.EnterpriseName.Trim() == lblEntName.Text.Trim())
                    {
                        ddlEntName.SelectedValue = ent.EnterpriseName;
                        break;
                    }
                    ddlEntName.SelectedIndex = 0;
                }

                DropDownList ddlStateShName = (DropDownList)gvr.FindControl("ddlStateShortName");
                StateList = Bus.StateMasterSelect(emp);
                foreach (StateMasterMsg State in StateList)
                {
                    if (State.StateShortName.Trim() == lblStateShortName.Text.Trim())
                    {
                        ddlStateShName.SelectedValue = State.StateId.ToString();
                        break;
                    }
                }

                DropDownList ddlCmpType = (DropDownList)gvr.FindControl("ddlCompnyType");
                DataTable dt = LoadCompanyType();

                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["CompanyTypeName"].ToString().Trim() == lblCmpType.Text.Trim())
                    {
                        ddlCmpType.SelectedValue = dr["CompanyTypeId"].ToString();
                        break;
                    }
                }


               

            }
        }


    }
    protected void GrdCompanyMaster_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GrdCompanyMaster.EditIndex = -1;
        LoadGridCompany();
    }
    protected void GrdCompanyMaster_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DeleteIndex = e.RowIndex;
        CompanyDelete();
    }
    protected void GrdCompanyMaster_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        UpdateIndex = e.RowIndex;
        GridViewRow row = GrdCompanyMaster.Rows[UpdateIndex];
        TextBox txtCmpnCode = (TextBox)row.FindControl("txtCompanyCode");
        TextBox txtcmpname = (TextBox)row.FindControl("txtCompanyName");
        TextBox txtcmpshname = (TextBox)row.FindControl("txtCompanyShortName");
        if (IsValidGridSave(txtCmpnCode.Text.Trim(), txtcmpname.Text.Trim(), txtcmpshname.Text.Trim()) == 0)
        {
            CompanyUpdate();
        }
    }
    #endregion
    #endregion
    #region Methods
    public List<EnterpriseMasterMsg> getEnterprise()
    {
        EmployeeMasterMsg EmpMsg = new EmployeeMasterMsg();
        List<EnterpriseMasterMsg> EnterpriseMsg = new List<EnterpriseMasterMsg>();
        EnterpriseMsg = Bus.EnterpriseMasterSelect(EmpMsg);
        return EnterpriseMsg;
    }
    private void LoadGridCompany()//To load data into grid.
    {
        EmployeeMasterMsg EmpMsg = new EmployeeMasterMsg();
        List<CompanyMessage> CmpLst = new List<CompanyMessage>();
        CompanyMessage Cmp = new CompanyMessage();
        Cmp.Flag = "R";
        EmpMsg.EmployeeCode = BaseMsg.EmployeeCode;
        CompanyList = Bus.MasCompanyInsertUpdateandDelete(Cmp, EmpMsg);
        //if (BaseMsg.IsAdmin == true)
        //{
        //    CmpLst = CompanyList.ToList();
        //}
        //else
        //{
        //    CmpLst = (from c in CompanyList where c.CompanyCode == BaseMsg.CompanyCode select c).ToList();
        //}
        CmpLst = Lib.LoadCompanyOnUserRight(CompanyList);

        GrdCompanyMaster.DataSource = "";
        GrdCompanyMaster.DataSource = CmpLst;
        GrdCompanyMaster.DataBind();
        if (!user.HasPermission(ProgramName, UserPermission.CanEdit.ToString()))
        {
            GrdCompanyMaster.Columns[GrdCompanyMaster.Columns.Count - 2].Visible = false;
        }
        if (!user.HasPermission(ProgramName, UserPermission.CanDelete.ToString()))
        {
            GrdCompanyMaster.Columns[GrdCompanyMaster.Columns.Count - 1].Visible = false;
        }
    }
  
    private void LoadParentCompanyName()
    {
        string CompanyType1 = System.Configuration.ConfigurationManager.AppSettings["CompanyType1"].ToString();
        string CompanyType2 = System.Configuration.ConfigurationManager.AppSettings["CompanyType2"].ToString();
        List<CompanyMessage> ParentCmpnyList = new List<CompanyMessage>();
        ParentCmpnyList = (from Company in CompanyList
                           where (Company.CompanyFlag.ToUpper() != CompanyType1 && Company.CompanyFlag.ToUpper() != CompanyType2)
                           select Company).Distinct().ToList();
        ddlParentCompanyName.Items.Clear();
        ddlParentCompanyName.DataTextField = "CompanyName";
        ddlParentCompanyName.DataValueField = "CompanyCode";
        ddlParentCompanyName.DataSource = ParentCmpnyList;
        ddlParentCompanyName.DataBind();
        ddlParentCompanyName.Items.Insert(0, "-- Select Please --");
    }

    private void LoadState()
    {
        EmployeeMasterMsg EmpMsg = new EmployeeMasterMsg();
        StateList = Bus.StateMasterSelect(EmpMsg);

        ddlStateShortName.DataTextField = "StateShortName";
        ddlStateShortName.DataValueField = "StateId";
        ddlStateShortName.DataSource = StateList;
        ddlStateShortName.DataBind();
        ddlStateShortName.Items.Insert(0, "-- Select Please --");
    }
    private void LoadEnterprise()
    {
        EmployeeMasterMsg EmpMsg = new EmployeeMasterMsg();
        EnterpriseList = Bus.EnterpriseMasterSelect(EmpMsg);

        ddlEnterpriseName.DataTextField = "EnterpriseName";
        ddlEnterpriseName.DataValueField = "EnterpriseId";
        ddlEnterpriseName.DataSource = EnterpriseList;
        ddlEnterpriseName.DataBind();
        ddlEnterpriseName.Items.Insert(0, "-- Select Please --");
    }
    //private void LoadLoccationtype()
    //{
    //    LocationTypeMasterMsg LocationType = new LocationTypeMasterMsg();
    //    LocationType.Flag = "R";
    //    LocationTypeList = Bus.MasLocationTypeInsertUpdateandDelete(LocationType);
    //    var ddlLocationTypeList = (from Location in LocationTypeList
    //                               where Location.IsActive == true
    //                               select new { Location.LocationTypeId, Location.LocationTypeShortName }).ToList();
    //    ddlCompanyType.DataTextField = "LocationTypeShortName";
    //    ddlCompanyType.DataValueField = "LocationTypeId";
    //    ddlCompanyType.DataSource = ddlLocationTypeList;
    //    ddlCompanyType.DataBind();
    //    ddlCompanyType.Items.Insert(0, "-- Select Please --");
    //}
    private void CompanySave()
    {
        EmployeeMasterMsg EmpMsg = new EmployeeMasterMsg();
        CompanyMessage Cmp = new CompanyMessage();
        Cmp.Flag = "I";
        Cmp.CompanyCode = txtCompanyCode.Text.Trim();
        Cmp.CompanyName = txtCompanyName.Text.Trim();
        Cmp.CompanyShortName = txtCompanyShortName.Text.Trim();
        //Cmp.LocationTypeID = Convert.ToInt32(ddlCompanyType.SelectedValue);
        Cmp.CompanyFlag = ddlCompanyType.SelectedItem.Text.Trim();
        Cmp.EnterpriseId = Convert.ToInt32(ddlEnterpriseName.SelectedValue.ToString());
        //starts-Commented by abinayaa 020913-- if parent company is null will take entered company name is default
        //Cmp.ParentCompanyCode = ddlParentCompanyName.SelectedItem.Value;
        //Cmp.StateID = Convert.ToInt32(ddlStateShortName.SelectedItem.Value);
        //Cmp.LocationID = Convert.ToInt32(ddlLocation.SelectedItem.Value);
        if (ddlParentCompanyName.SelectedIndex == 0)
        {
            Cmp.ParentCompanyCode = Cmp.CompanyCode;
        }
        else
        {
            Cmp.ParentCompanyCode = ddlParentCompanyName.SelectedValue;
        }
        //Ends-Commented by abinayaa 020913-- if parent company is null will take entered company name is default
        Cmp.StateID = Convert.ToInt32(ddlStateShortName.SelectedValue);
        //Cmp.LocationID = Convert.ToInt32(ddlLocation.SelectedValue);
        Cmp.CreatedBy = BaseMsg.EmployeeCode;
        EmpMsg.EmployeeCode = BaseMsg.EmployeeCode;
        CompanyList = Bus.MasCompanyInsertUpdateandDelete(Cmp, EmpMsg);
        //Output Dispay
        foreach (CompanyMessage cmp in CompanyList)
        {
            if (cmp.CompanyResult == "0")
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.SuccessFullySaved + "');", true);
                AllClear();
                break;

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + cmp.CompanyResult + "');", true);
                break;
            }
        }
    }
    public List<CompanyMessage> getParentCompanyName()
    {
        string CompanyType1 = System.Configuration.ConfigurationManager.AppSettings["CompanyType1"].ToString();
        string CompanyType2 = System.Configuration.ConfigurationManager.AppSettings["CompanyType2"].ToString();
        List<CompanyMessage> ParentCmpnyList = new List<CompanyMessage>();
        ParentCmpnyList = (from Company in CompanyList
                           where (Company.CompanyFlag.ToUpper() != CompanyType1 && Company.CompanyFlag.ToUpper() != CompanyType2)
                           select Company).Distinct().ToList();
        return ParentCmpnyList;
    }
    public List<StateMasterMsg> getState()
    {
        EmployeeMasterMsg EmpMsg = new EmployeeMasterMsg();
        List<StateMasterMsg> stateMsg = new List<StateMasterMsg>();
        stateMsg = Bus.StateMasterSelect(EmpMsg);
        return stateMsg;
    }
    public DataTable LoadCompanyType()
    {
        DataTable dtCompanyType = new DataTable();
        dtCompanyType.Columns.Add("CompanyTypeName", typeof(string));
        dtCompanyType.Columns.Add("CompanyTypeId", typeof(string));
        DataRow dr1 = dtCompanyType.NewRow();
        dr1["CompanyTypeName"] = System.Configuration.ConfigurationManager.AppSettings["Corporate"].ToString();
        dr1["CompanyTypeId"] = System.Configuration.ConfigurationManager.AppSettings["Corporate"].ToString();
        dtCompanyType.Rows.Add(dr1);
        DataRow dr2 = dtCompanyType.NewRow();
        dr2["CompanyTypeName"] = System.Configuration.ConfigurationManager.AppSettings["HeadOffice"].ToString();
        dr2["CompanyTypeId"] = System.Configuration.ConfigurationManager.AppSettings["HeadOffice"].ToString();
        dtCompanyType.Rows.Add(dr2);
        DataRow dr3 = dtCompanyType.NewRow();
        dr3["CompanyTypeName"] = System.Configuration.ConfigurationManager.AppSettings["Branch"].ToString();
        dr3["CompanyTypeId"] = System.Configuration.ConfigurationManager.AppSettings["Branch"].ToString();
        dtCompanyType.Rows.Add(dr3);
        //DataRow dr4 = dtCompanyType.NewRow();
        //dr4["CompanyTypeName"] = System.Configuration.ConfigurationManager.AppSettings["TraninigCenter"].ToString();
        //dr4["CompanyTypeId"] = System.Configuration.ConfigurationManager.AppSettings["TraninigCenter"].ToString();
        //dtCompanyType.Rows.Add(dr4);
        return dtCompanyType;
    }
    public void LoadddlCompanyType()
    {
        DataTable dt = new DataTable();
        dt = LoadCompanyType();
        ddlCompanyType.DataSource = dt;// LoadCompanyType();
        ddlCompanyType.DataBind();
       // ddlCompanyType.Items.Insert(0, new ListItem("-- Select Please --", "0"));
        ddlCompanyType.Items.Insert(0, "-- Select Please --");
    }
    //private void LoadLoccationstate()
    //{
    //    LocationinStateMasterMsg Locationstate = new LocationinStateMasterMsg();
    //    Locationstate.Flag = "R";
    //    LocationstateList = Bus.MasLocationinStateInsertUpdateandDelete(Locationstate);
    //    var ddlLocationstateList = (from Locstate in LocationstateList
    //                                where Locstate.IsActive == true
    //                                select new { Locstate.LocationinState, Locstate.LocationinStateId }).ToList();
    //    ddlLocation.DataTextField = "LocationinState";
    //    ddlLocation.DataValueField = "LocationinStateId";
    //    ddlLocation.DataSource = ddlLocationstateList;
    //    ddlLocation.DataBind();
    //    ddlLocation.Items.Insert(0, "-- Select Please --");
    //}
    //public List<LocationinStateMasterMsg> getLocationstate()
    //{
    //    return LocationstateList;
    //}
    //public List<LocationTypeMasterMsg> getLocationType()
    //{
    //    return LocationTypeList;
    //}
    public void CompanyUpdate()
    {
        GridViewRow row = GrdCompanyMaster.Rows[UpdateIndex];
        EmployeeMasterMsg EmpMsg = new EmployeeMasterMsg();
        CompanyMessage cmp = new CompanyMessage();
        cmp.Flag = "U";
        TextBox txtCmpnCode = (TextBox)row.FindControl("txtCompanyCode");
        TextBox txtcmpname = (TextBox)row.FindControl("txtCompanyName");
        TextBox txtcmpshname = (TextBox)row.FindControl("txtCompanyShortName");
        //TextBox txtLocationTypeId = (TextBox)row.FindControl("txtLocationTypeId");Commented By Abinayaa 250113.LocationTypeid not taken from txtbox.its only taken from ddlcmpType selectedValue
        DropDownList ddlCompnyType = (DropDownList)row.FindControl("ddlCompnyType");
        DropDownList ddlEntName = (DropDownList)row.FindControl("ddlEntName");
        DropDownList ddlparentcmpname = (DropDownList)row.FindControl("ddlParentCompanyName");
        DropDownList ddlState = (DropDownList)row.FindControl("ddlStateShortName");
        DropDownList ddlLocation = (DropDownList)row.FindControl("ddlLocationName");
        CheckBox chkActive = (CheckBox)row.FindControl("chkIsActive");

        cmp.CompanyCode = txtCmpnCode.Text.Trim();
        cmp.CompanyName = txtcmpname.Text.Trim();
        cmp.CompanyShortName = txtcmpshname.Text.Trim();
        cmp.CompanyFlag = ddlCompnyType.SelectedItem.Text.Trim();
        //starts-Commented by abinayaa 020913-- if parent company is null will take entered company name is default
        // In update mode the ddl will not be in select pls as default abi/170913 hence commented below
        //if (ddlParentCompanyName.SelectedIndex == 0)
        //{
        //    cmp.ParentCompanyCode = cmp.CompanyCode;
        //}
        //else
        //{
        //    cmp.ParentCompanyCode = ddlparentcmpname.SelectedValue;
        //}
        //Ends --Commented by abinayaa 020913-- if parent company is null wil take entered company name is default
        cmp.ParentCompanyCode = ddlparentcmpname.SelectedValue;
        cmp.StateID = Convert.ToInt32(ddlState.SelectedValue);
        cmp.EnterpriseId = Convert.ToInt32(ddlEntName.SelectedValue);
        cmp.IsActive = chkActive.Checked;
        cmp.CreatedBy = BaseMsg.EmployeeCode;
        EmpMsg.EmployeeCode = BaseMsg.EmployeeCode;
        CompanyList = Bus.MasCompanyInsertUpdateandDelete(cmp, EmpMsg);
        GrdCompanyMaster.EditIndex = -1;

        foreach (CompanyMessage CmpUpdate in CompanyList)
        {
            if (CmpUpdate.CompanyResult == "0")
            {
                //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.CompanyUpdatedSuccessfully + "');", true);

                AllClear();
                break;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + CmpUpdate.CompanyResult + "');", true);
                break;
            }
        }
    }

    public void CompanyDelete()
    {
        EmployeeMasterMsg EmpMsg = new EmployeeMasterMsg();
        CompanyMessage Cmp = new CompanyMessage();
        Cmp.Flag = "D";
        GridViewRow gvr = GrdCompanyMaster.Rows[DeleteIndex];
        Label lblCompCode = (Label)gvr.FindControl("lblCompanyCode");
        Label lblCompName = (Label)gvr.FindControl("lblCompanyName");
        Label lblCompShortName = (Label)gvr.FindControl("lblCompanyShortName");
        //Label lblLocationTypeId = (Label)gvr.FindControl("lblLocationTypeId");
        Label lblCompType = (Label)gvr.FindControl("lblCompnyType");
        Label lblEntName = (Label)gvr.FindControl("lblEntName"); //SCS 270714
        Label lblParentCompName = (Label)gvr.FindControl("lblParentCompanyName");

        DropDownList ddlCompnyType = (DropDownList)gvr.FindControl("ddlCompnyType");

       
        Cmp.CompanyCode = lblCompCode.Text.Trim();
        Cmp.CompanyName = lblCompName.Text.Trim();
        Cmp.CompanyShortName = lblCompShortName.Text.Trim();
       // Cmp.CompanyFlag = ddlCompnyType.SelectedItem.Text.Trim();
        
        Cmp.CompanyFlag = lblCompType.Text.Trim();
        
        Cmp.ParentCompanyCode = string.Empty;
        EmpMsg.EmployeeCode = BaseMsg.EmployeeCode;
        Cmp.CreatedBy = BaseMsg.EmployeeCode;
        Cmp.StateID = 0;
        CompanyList = Bus.MasCompanyInsertUpdateandDelete(Cmp, EmpMsg);
        foreach (CompanyMessage cmp in CompanyList)
        {
            if (cmp.CompanyResult == "0")
            {
                //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.CompanyDeletedSuccessfully + "');", true);
                AllClear();
                break;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + cmp.CompanyResult + "');", true);
                break;
            }
        }
    }
    #region Validation

    private int IsValidSave()
    {
        int Error = 0;
        string DisplayError = "";
        if (txtCompanyCode.Text.Trim() == "" || Convert.ToInt32(txtCompanyCode.Text.Trim().Length.ToString()) == 0)
        {
            DisplayError = DisplayError + AgilerMail.ErrorCompanyCode;
            //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.ErrorCompanyCode + "');", true);
            Error = 1;
        }
        if (txtCompanyName.Text.Trim() == "" || Convert.ToInt32(txtCompanyName.Text.Trim().Length.ToString()) == 0)
        {
            DisplayError = DisplayError + "--" + AgilerMail.ErrorCompanyName;
            // ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.ErrorCompanyName + "');", true);
            Error = 1;
        }
        if (txtCompanyShortName.Text.Trim() == "" || Convert.ToInt32(txtCompanyShortName.Text.Trim().Length.ToString()) == 0)
        {
            DisplayError = DisplayError + "--" + AgilerMail.ErrorCompanyShortName;
            // ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.ErrorCompanyShortName + "');", true);
            Error = 1;
        }

        if (ddlCompanyType.SelectedIndex == 0)
        {
            DisplayError = DisplayError + "--" + AgilerMail.ErrorCompanyFlag;
            //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.ErrStateName + "');", true);
            Error = 1;
        }
        if (ddlParentCompanyName.SelectedIndex == 0)
        {
            DisplayError = DisplayError + "-" + AgilerMail.ErrParentCompanyName;
            Error = 1;
        }
        if (ddlEnterpriseName.SelectedIndex == 0)
        {
            DisplayError = DisplayError + "-" + AgilerMail.ErrEnterpriseName;
            Error = 1;
        }

        if (ddlStateShortName.SelectedIndex == 0)
        {
            DisplayError = DisplayError + "--" + AgilerMail.ErrStateName;
            //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.ErrStateName + "');", true);
            Error = 1;
        }

        if (Error == 1)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + DisplayError + "');", true);
        }
        return Error;
    }

    private int IsValidGridSave(string CompanyCode, string CompanyName, string CompanyShortName)
    {
        int Error = 0;
        string DisplayError = "";
        if (CompanyCode.Trim() == "" || Convert.ToInt32(CompanyCode.Trim().Length.ToString()) == 0)
        {
            DisplayError = DisplayError + AgilerMail.ErrorCompanyCode;
            Error = 1;
        }
        if (CompanyName.Trim() == "" || Convert.ToInt32(CompanyName.Trim().Length.ToString()) == 0)
        {
            DisplayError = DisplayError + "--" + AgilerMail.ErrorCompanyName;
            Error = 1;
        }
        if (CompanyShortName.Trim() == "" || Convert.ToInt32(CompanyShortName.Trim().Length.ToString()) == 0)
        {
            DisplayError = DisplayError + "--" + AgilerMail.ErrorCompanyShortName;
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
        txtCompanyCode.Text = "";
        txtCompanyName.Text = "";
        txtCompanyShortName.Text = "";
        ddlCompanyType.SelectedIndex = 0;
        ddlStateShortName.SelectedIndex = 0;
        //ddlLocation.SelectedIndex = 0;
        GrdCompanyMaster.DataSource = "";
        GrdCompanyMaster.DataSource = CompanyList;
        GrdCompanyMaster.DataBind();
        if (!user.HasPermission(ProgramName, UserPermission.CanEdit.ToString()))
        {
            GrdCompanyMaster.Columns[GrdCompanyMaster.Columns.Count - 2].Visible = false;
        }
        if (!user.HasPermission(ProgramName, UserPermission.CanDelete.ToString()))
        {
            GrdCompanyMaster.Columns[GrdCompanyMaster.Columns.Count - 1].Visible = false;
        }
       // LoadGridCompany();       
        LoadParentCompanyName();
    }
    #endregion
    #endregion        
    }