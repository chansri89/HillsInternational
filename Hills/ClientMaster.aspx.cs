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

public partial class ClientMaster : System.Web.UI.Page
{
    #region Declaration
    ProcessBus Bus = new ProcessBus(); LibFunctions Lib = new LibFunctions();
    List<EmployeeMasterMsg> EmpList = new List<EmployeeMasterMsg>();
    private static List<ClientMsg> ClientList = new List<ClientMsg>();
    List<EnterpriseMasterMsg> EnterpriseList = new List<EnterpriseMasterMsg>(); //scs 2704
    List<StateMasterMsg> StateList = new List<StateMasterMsg>();
    //private static List<ClientTypeMsg> GCusTypLst = new List<ClientTypeMsg>();
    private static List<CompanyMessage> CmpList = new List<CompanyMessage>();
    private static List<GSTINState> GSTSt = new List<GSTINState>();
    //List<ClientTypeMasterMsg> ClientTypeList = new List<ClientTypeMasterMsg>();
    //public static List<ClientinStateMasterMsg> ClientstateList = new List<ClientinStateMasterMsg>();
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
            List<ClientTypeMsg> cltypeList = LoadClientType();
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
            Pnlgv.Visible = true;
            pnlAdd.Visible = true;
            LoadGridClient(WCompanyId);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Location Data Not Available" + "');", true);
            Pnlgv.Visible = false;
            pnlAdd.Visible = false;
        }
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
    protected void ddlStateChanged(object sender, EventArgs e)
    {
        ddlState.Focus();
        if (ddlState.SelectedIndex > 0)
        {
            txtAddr2.Focus();
        }
    }
    protected void btnFilter_Click(object sender, EventArgs e)
    {
       // ClientFilter();
        Int32 WCompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
        LoadGridClient(WCompanyId);
    }
    #region GridEditing
    protected void GrdClient_RowCommand(object sender, GridViewCommandEventArgs e)
    {
     
        string WClientCode = (e.CommandArgument).ToString();
        foreach(GridViewRow row in GrdClientMaster.Rows)
        {
            if (((Label)row.FindControl("lblClientCode")).Text == WClientCode)
            {
                //txtClientId.Text = ((Label)row.FindControl("lblClientId")).Text;
                txtClientCode.Text = ((Label)row.FindControl("lblClientCode")).Text; 
                txtClientCode.Enabled = false; //client code cannot be allowed to change scs 260323
                txtClientName.Text = ((Label)row.FindControl("lblClientName")).Text;
                txtClientShortName.Text = ((Label)row.FindControl("lblClientShortName")).Text;

                txtAddr1.Text = ((Label)row.FindControl("lblClientAddress1")).Text;
                txtAddr2.Text = ((Label)row.FindControl("lblClientAddress2")).Text;
                txtBuildingName.Text = ((Label)row.FindControl("lblBuildingName")).Text;

                txtCity.Text = ((Label)row.FindControl("lblCity")).Text;
                //txtState.Text = ((Label)row.FindControl("lblState")).Text;
                string WState = ((Label)row.FindControl("lblStateName")).Text;
                Int32 WStateId = Convert.ToInt32(((Label)row.FindControl("lblStateId")).Text);
                string WCustype = ((Label)row.FindControl("lblClientType")).Text.Trim(); //scs 230217 introduced supplierType
                // LoadGSTINState();
                LoadState();
                foreach (StateMasterMsg gs in StateList)
                {
                    if (gs.StateName == WState)
                    {
                        ddlState.SelectedValue = gs.StateId.ToString();
                        break;
                    }
                }
                List<ClientTypeMsg> cltypeList = LoadClientType();
                foreach (ClientTypeMsg clm in cltypeList)
                {
                    if (clm.ClientType == WCustype)
                    {
                        ddlClientType.SelectedValue = WCustype;
                        break;
                    }
                }


                txtPinCode.Text = ((Label)row.FindControl("lblPinCode")).Text;

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
        //if (BaseMsg.IsAdmin == false)
        //{
        //    CmpList = (from cmp in CmpList where cmp.CompanyId == BaseMsg.CompanyId select cmp).ToList();
        //}
        CmpList = Lib.LoadCompany();
        if (CmpList != null && CmpList.Count > 0)
        {
            ddlCompany.DataSource = CmpList;
            ddlCompany.DataBind();
            ddlCompany.Items.Insert(0, "--Select Please--");
            ddlCompany.SelectedIndex = 0;
            if (CmpList.Count == 1)
            {
                ddlCompany.SelectedIndex = 1;
                ddlCompany.Enabled = false;
                WCompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                LoadGridClient(WCompanyId);
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
    private List<ClientTypeMsg> LoadClientType()
    {
        List<ClientTypeMsg> cltypeList = new List<ClientTypeMsg>();
        ClientTypeMsg clty1 = new ClientTypeMsg();
        clty1.ClientType = "Client";
        cltypeList.Add(clty1);
        ClientTypeMsg clty2 = new ClientTypeMsg();
        clty2.ClientType = "Tenderer";
        cltypeList.Add(clty2);

        ddlClientType.DataSource = cltypeList;
        ddlClientType.DataBind();
        ddlClientType.Items.Insert(0, "--Select Please--");

             return cltypeList;
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
    private void LoadGridClient(Int32 CompanyId)//To load data into grid.
    {
        ClientMsg Cmp = new ClientMsg();
        Cmp.Flag = "R";
        Cmp.CreatedBy = BaseMsg.EmployeeCode;
        Cmp.CompanyId = WCompanyId;
        //Cmp.ClientId =
        ClientList = Bus.MasClientInsertUpdateandDelete(Cmp);
        ClientFilter(ClientList);
       
    }
    private void ClientFilter(List<ClientMsg> cli)
    {

        List<ClientMsg> FCustList = new List<ClientMsg>();
        if (txtFilter.Text.Trim().Length == 0)
        {
            FCustList = cli.ToList();
        }
        else
        {
            foreach (ClientMsg cm in cli)
            {
                if (cm.ClientName.ToUpper().Contains(txtFilter.Text.Trim().ToUpper()))
                {
                    FCustList.Add(cm);
                }
            }

        }
        if (FCustList.Count == 0)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "No Client for the Filter Found .." + "');", true);
            FCustList = cli.ToList();
        }
        GrdClientMaster.DataSource = "";
        GrdClientMaster.DataSource = FCustList;
        GrdClientMaster.DataBind();
        Pnlgv.Visible = true;
        pnlAdd.Visible = true;

    }
    private void ClientSave()
    {
        WCompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
        ClientMsg Cmp = new ClientMsg();
        if (btnSave.Text.ToUpper() == "SAVE")
        {
            Cmp.Flag = "I";
           // Cmp.ClientId = 0;
        }
        else
        {
            Cmp.Flag = "U";
            Cmp.IsActive = chkIsActive.Checked;
        }
        Cmp.CompanyId = WCompanyId;
        Cmp.ClientCode = txtClientCode.Text.Trim();
        Cmp.ClientName = txtClientName.Text.Trim();
        Cmp.ClientShortName = txtClientShortName.Text.Trim();
        Cmp.Addr1 = txtAddr1.Text.Trim();
        Cmp.Addr2 = txtAddr2.Text.Trim();
        Cmp.BuildingName = txtBuildingName.Text.Trim();
        Cmp.City = txtCity.Text.Trim();
        Cmp.StateName = ddlState.SelectedItem.ToString(); //txtState.Text.Trim();
        Cmp.StateId = Convert.ToInt32(ddlState.SelectedValue);
        Cmp.PinCode = txtPinCode.Text.Trim();

      
        Cmp.ClientType = ddlClientType.SelectedItem.ToString(); //scs 230217();
       
        Cmp.IsActive = chkIsActive.Checked; //scs 230221
        Cmp.CreatedBy = BaseMsg.EmployeeCode;
        
        ClientList = Bus.MasClientInsertUpdateandDelete(Cmp);
        //Output Dispay
        foreach (ClientMsg cmp in ClientList)
        {
            if (cmp.ClientResult == "0")
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.SuccessFullySaved + "');", true);
                AllClear();
                //pnlPendind.Enabled = false;
                btnSave.Text = "Save";
                txtClientCode.Enabled = true; //client code cannot be allowed to change scs 260323
                break;

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + cmp.ClientResult + "');", true);
                break;
            }
        }
    }
 
 
    #region Validation

    private int IsValidSave()
    {
        int Error = 0;
        string DisplayError = "";
        if (txtClientCode.Text.Trim() == "" || Convert.ToInt32(txtClientCode.Text.Trim().Length) == 0)
        {
            DisplayError = DisplayError + " ClientCode is Mandatory .";
            //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.ErrorClientCode + "');", true);
            Error = 1;
        }
        if (txtClientName.Text.Trim() == "" || Convert.ToInt32(txtClientName.Text.Trim().Length) == 0)
        {
            DisplayError = DisplayError + "--" + " Client Name is Mandatory . ";
            // ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.ErrorClientName + "');", true);
            Error = 1;
        }
        if (txtClientShortName.Text.Trim() == "" || Convert.ToInt32(txtClientShortName.Text.Trim().Length) == 0)
        {
            DisplayError = DisplayError + "--" + " Client short Name is Mandatory . ";
            // ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.ErrorClientShortName + "');", true);
            Error = 1;
        }
        if (ddlState.SelectedIndex ==0)
        {
            DisplayError = DisplayError + "--" + " State Has to be selected . ";
            Error = 1;
        }
        if (ddlClientType.SelectedIndex == 0)
        {
            DisplayError = DisplayError + "--" + " Client Type has to be selected . ";
            Error = 1;
        }
   

        if (txtPinCode.Text.Trim() == "" || Convert.ToInt32(txtPinCode.Text.Trim().Length) == 0)
        {
            DisplayError = DisplayError + "--" + " Client PinCode is Mandatory . ";
            // ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.ErrorClientShortName + "');", true);
            Error = 1;
        }
        try
        {
            Int64 X = Convert.ToInt32(txtPinCode.Text.Trim());
            if (txtPinCode.Text.Trim().Length != 6) //PinCode should be six characters
            {
                DisplayError = DisplayError + "--" + " PINCode should be 6 characters integer . ";
                // ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.ErrorClientShortName + "');", true);
                Error = 1;
            }
        }
        catch
        {
            DisplayError = DisplayError + "--" + " PINCode should be 6 characters Integer . ";
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
        txtClientCode.Text = "";
        txtClientName.Text = "";
        txtClientShortName.Text = "";
        txtAddr1.Text = "";
        txtAddr2.Text = "";
        txtBuildingName.Text = "";
        txtCity.Text = "";
        ddlState.SelectedIndex = 0;
        ddlClientType.SelectedIndex = 0;
        txtPinCode.Text = "";
        
        Pnlgv.Visible = true;
        pnlAdd.Visible = true;
        pnlPendind.Enabled = true;
        LoadGridClient(WCompanyId); //SCS 210301
        pnlPendind.Enabled = true;
        chkIsActive.Visible = false;
        pnlAdd.GroupingText = "Add Client";

    }
    #endregion
    #endregion        
    }