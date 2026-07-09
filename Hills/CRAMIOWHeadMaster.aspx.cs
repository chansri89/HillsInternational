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

public partial class CRAMIOWHeadMaster : System.Web.UI.Page
{
    #region Declaration
    ProcessBus Bus = new ProcessBus(); LibFunctions Lib = new LibFunctions();
    //List<EmployeeMasterMsg> EmpList = new List<EmployeeMasterMsg>();
    List<CRAMIOWHeadMsg> IOWList = new List<CRAMIOWHeadMsg>();
    private static List<CompanyMessage> CmpList = new List<CompanyMessage>();
    public static List<CRAMIOWGroupMsg> GrpList = new List<CRAMIOWGroupMsg>();
    public static List<CRAMIOWSubGroupMsg> SgrpList = new List<CRAMIOWSubGroupMsg>();
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
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        
        AllClear();
        Pnlgv.Visible = false;
        pnlAdd.Visible = false;
        ddlCompany.SelectedIndex = 0;
    }
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedIndex > 0)
        {
            LoadGridIOWHead(Convert.ToInt32(ddlCompany.SelectedValue));
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Company Not Selected" + "');", true);
            Pnlgv.Visible = false;
            pnlAdd.Visible = false;
        }
    }
    protected void ddlCompany_Changed(object sender, EventArgs e)
    {
        
        if (ddlCompany.SelectedIndex > 0)
        {
            CompanyChanged();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Location Not Selected" + "');", true);
            Pnlgv.Visible = false;
            pnlAdd.Visible = false;
        }
    }
    protected void ddlGroupFilter_Changed(object sender, EventArgs e)
    {
        groupddlchange(0);
    }

    protected void Groupddlchanged(object sender, EventArgs e)
    {
        groupddlchange(1);
       
       
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
                    IOWSave();
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
                    IOWSave();
                }
            }
        }
        else
        {
        }
    }
    #region GridEditing
    protected void GrdIOWHead_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string WCommandName = e.CommandName.ToString(); 
        string WIOWCode = e.CommandArgument.ToString();
        if (WCommandName == "UpdateIOW") //This it to Modify the IOW data
        {
            foreach (GridViewRow row in GrdIOWHeadMaster.Rows)
            {
                if (((Label)row.FindControl("lblIOWHeadCode")).Text == WIOWCode.ToString())
                {
                    string WGroup = ((Label)row.FindControl("lblGroupCode")).Text;
                    string WSubGroupCode = ((Label)row.FindControl("lblSubGroupCode")).Text;
                    txtIOWHeadCode.Text = ((Label)row.FindControl("lblIOWHeadCode")).Text;
                    txtIOWHeadName.Text = ((Label)row.FindControl("lblIOWHeadName")).Text;
                    foreach (CRAMIOWGroupMsg pk in GrpList)
                    {
                        if (pk.GroupCode == WGroup)
                        {
                            ddlGroup.SelectedValue = WGroup;
                            break;
                        }
                    }
                    foreach (CRAMIOWSubGroupMsg trd in SgrpList)
                    {
                        if (trd.SubGroupCode == WSubGroupCode)
                        {
                            ddlsubGroup.SelectedValue = WSubGroupCode;
                            break;
                        }
                    }
                    foreach (CRAMIOWSubGroupMsg trd in SgrpList)
                    {
                        if (trd.SubGroupCode == WSubGroupCode)
                        {
                            ddlSubGroupFilter.SelectedValue = WSubGroupCode;
                            break;
                        }
                    }

                    btnSave.Text = "Update";
                    Pnlgv.Visible = false;
                    txtIOWHeadCode.Enabled = false;
                    pnlPendind.Enabled = false;
                    break;
                }
            }
        }
    }

    #endregion
    #endregion
    #region Methods
    #region IOW
    private void LoadCompany()
    {
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
                ddlCompany.Enabled = false;
                //WCompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                CompanyChanged();
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
        txtIOWHeadCode.Enabled = true;
    }
    private void CompanyChanged()
    {
        WCompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
        //Pnlgv.Visible = true;
        //pnlAdd.Visible = true;
        LoadIOWGroup(WCompanyId);
        AllClear();
        //LoadIOWSubGroup(WCompanyId);
        //LoadGridIOWHead(Convert.ToInt32(ddlCompany.SelectedValue));
    }
    private void groupddlchange(int ddl)
    {
        string WGroupCode = "0"; WCompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
        if (ddlCompany.SelectedIndex > 0 && ddlGroupFilter.SelectedIndex > 0 && ddl==0)
        {
            WGroupCode = ddlGroupFilter.SelectedValue;
        }
        else if (ddlCompany.SelectedIndex > 0 && ddlGroup.SelectedIndex > 0 && ddl == 1)
        {
            WGroupCode = ddlGroup.SelectedValue;
            foreach (CRAMIOWGroupMsg pk in GrpList) //restore the filter ddlgroup to what is selected here
            {
                if (pk.GroupCode == WGroupCode)
                {
                    ddlGroupFilter.SelectedValue = WGroupCode;
                    break;
                }
            }

        }
        else
        {
            WGroupCode = "0";
        }
        LoadIOWSubGroup(WCompanyId, WGroupCode);
    }
    private void LoadIOWSubGroup(Int32 Companyid)
    {
        CRAMIOWSubGroupMsg Cmp = new CRAMIOWSubGroupMsg();
        Cmp.Flag = "R";
        Cmp.CreatedBy = BaseMsg.EmployeeCode;
        Cmp.CompanyId = Companyid;
        SgrpList = Bus.MasCRAMSubGroupInsertUpdate(Cmp);
        ddlsubGroup.DataSource = SgrpList;
        ddlsubGroup.DataBind();
        ddlsubGroup.Items.Insert(0, "-- Select Please --");
        ddlsubGroup.SelectedIndex = 0;

        ddlSubGroupFilter.DataSource = SgrpList;
        ddlSubGroupFilter.DataBind();
        ddlSubGroupFilter.Items.Insert(0, "ALL");
        ddlSubGroupFilter.SelectedIndex = 0;
    }
    private void LoadIOWSubGroup(Int32 Companyid, String WGrpcode)
    {
        List<CRAMIOWSubGroupMsg> sglst = new List<CRAMIOWSubGroupMsg>();
        CRAMIOWSubGroupMsg Cmp = new CRAMIOWSubGroupMsg();
        Cmp.Flag = "R";
        Cmp.CreatedBy = BaseMsg.EmployeeCode;
        Cmp.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
        SgrpList = Bus.MasCRAMSubGroupInsertUpdate(Cmp);
        if (WGrpcode == "0")
        {
            sglst = SgrpList;
        }
        else
        {
            sglst = (from sg in SgrpList where sg.GroupCode == WGrpcode select sg).ToList();
        }
        ddlSubGroupFilter.DataSource = sglst;
        ddlSubGroupFilter.DataBind();
        ddlSubGroupFilter.Items.Insert(0, "ALL");
        ddlSubGroupFilter.SelectedIndex = 0;

        ddlsubGroup.DataSource = sglst;
        ddlsubGroup.DataBind();
        ddlsubGroup.Items.Insert(0, "ALL");
        ddlsubGroup.SelectedIndex = 0;
    }

    private void LoadIOWGroup(Int32 Companyid)
    {
        CRAMIOWGroupMsg Cmp = new CRAMIOWGroupMsg();
        Cmp.Flag = "R";
        Cmp.CreatedBy = BaseMsg.EmployeeCode;
        Cmp.CompanyId = Companyid;

        GrpList = Bus.MasCRAMGroupInsertUpdate(Cmp);
        ddlGroup.DataSource = GrpList;
        ddlGroup.DataBind();
        ddlGroup.Items.Insert(0, "-- Select Please --");
        ddlGroup.SelectedIndex = 0;

        ddlGroupFilter.DataSource = GrpList;
        ddlGroupFilter.DataBind();
        ddlGroupFilter.Items.Insert(0, "ALL");
        ddlGroupFilter.SelectedIndex = 0;
        LoadIOWSubGroup(Companyid);
    }
    private void LoadGridIOWHead(Int32 CompanyId)//To load data into grid.
    {
        CRAMIOWHeadMsg iow = new CRAMIOWHeadMsg();
        iow.CompanyId = CompanyId;
        iow.Flag = "R";
        IOWList = Bus.MasCRAMIOWHeadInsertUpdate(iow);
        IOWFilter(IOWList);
    }
    private void IOWFilter(List<CRAMIOWHeadMsg> IOwlst)
    {
        List<CRAMIOWHeadMsg> WIOWList = new List<CRAMIOWHeadMsg>();
        string WGroupCode = ""; string WSubGroupCode = "";
        if (ddlGroupFilter.SelectedIndex == 0)
        {
            WGroupCode = "0";
            WIOWList = IOwlst.ToList();
        }
        else
        {
            WGroupCode = ddlGroupFilter.SelectedValue;
            WIOWList = (from iow in IOwlst where iow.GroupCode == WGroupCode select iow).ToList();
        }
        if (ddlSubGroupFilter.SelectedIndex <= 0)
        {
            WSubGroupCode = "0";
            WIOWList = WIOWList.ToList();
        }
        else
        {
            WSubGroupCode = ddlSubGroupFilter.SelectedValue;
            WIOWList = (from iow in WIOWList where iow.SubGroupCode == WSubGroupCode select iow).ToList();
        }

        List<CRAMIOWHeadMsg> FIOWList = new List<CRAMIOWHeadMsg>();
        if (txtFilter.Text.Trim().Length == 0)
        {
            FIOWList = WIOWList.ToList();
        }
        else
        {
            FIOWList = (from cm in WIOWList where cm.IOWHeadName.ToUpper().Contains(txtFilter.Text.Trim().ToUpper()) select cm).ToList();
        }
        if (FIOWList.Count == 0)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "No IOW Head for the selected Filter Found .." + "');", true);
            FIOWList = IOwlst.ToList();
        }
        GrdIOWHeadMaster.DataSource = "";
        GrdIOWHeadMaster.DataSource = FIOWList;
        GrdIOWHeadMaster.DataBind();
        Pnlgv.Visible = true;
        pnlAdd.Visible = true;
    }

   // private void IOWFilter(List<CRAMIOWHeadMsg> IOwlst)
   // {

   //     List<CRAMIOWHeadMsg> FIOWList = new List<CRAMIOWHeadMsg>();
   //     if (txtFilter.Text.Trim().Length == 0)
   //     {
   //         FIOWList = IOwlst.ToList();
   //     }
   //     else
   //     {
   //         FIOWList = (from cm in IOWList where cm.IOWHeadName.ToUpper().Contains(txtFilter.Text.Trim().ToUpper()) select cm).ToList();
   //     }
   //     if (FIOWList.Count == 0)
   //     {
   //         ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "No IOW Head for the Filter Found .." + "');", true);
   //         FIOWList = IOwlst.ToList();
   //     }
   //     GrdIOWHeadMaster.DataSource = "";
   //     GrdIOWHeadMaster.DataSource = FIOWList;
   //     GrdIOWHeadMaster.DataBind();
   //     Pnlgv.Visible = true;
   //     pnlAdd.Visible = true;
   //}
    private void IOWSave()
    {
        WCompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
        CRAMIOWHeadMsg Cmp = new CRAMIOWHeadMsg();
        if (btnSave.Text.ToUpper() == "SAVE")
        {
            Cmp.Flag = "I";
            Cmp.IsActive = true;
        }
        else
        {
            Cmp.Flag = "U";
            Cmp.IsActive = true;// Cmp.IsActive;
        }
        Cmp.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue); //WCompanyId;
        Cmp.GroupCode = ddlGroup.SelectedValue; //scs 220217 introduced
        Cmp.SubGroupCode = ddlsubGroup.SelectedValue;
        Cmp.IOWHeadCode = txtIOWHeadCode.Text.Trim();
        Cmp.IOWHeadName = txtIOWHeadName.Text.Trim();    
        Cmp.CreatedBy = BaseMsg.EmployeeCode;
        IOWList = Bus.MasCRAMIOWHeadInsertUpdate(Cmp);
        //Output Dispay
        foreach (CRAMIOWHeadMsg cmp in IOWList)
        {
            if (cmp.Result == "0")
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.SuccessFullySaved + "');", true);
                AllClear();
                //pnlPendind.Enabled = false;
                txtIOWHeadCode.Enabled = true;
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
    #endregion

    #region Validation
    private int IsValidSave()
    {
        int Error = 0;
        string DisplayError = "";
        if (txtIOWHeadCode.Text.Trim() == "" || Convert.ToInt32(txtIOWHeadCode.Text.Trim().Length) == 0)
        {
            DisplayError = DisplayError + " IOW Head Code is Mandatory .";
            Error = 1;
        }
        if (txtIOWHeadName.Text.Trim() == "" || Convert.ToInt32(txtIOWHeadName.Text.Trim().Length) == 0)
        {
            DisplayError = DisplayError + "--" + " IOW Head Name is Mandatory . ";
            Error = 1;
        }
        if (ddlGroup.SelectedIndex == 0 || ddlsubGroup.SelectedIndex == 0)
        {
            DisplayError = DisplayError + "--" + " Group and Sub group are Mandatory . ";
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
        txtIOWHeadCode.Text = "";
        txtIOWHeadName.Text = "";
        txtIOWHeadCode.Enabled = true;
        Pnlgv.Visible = false;
        pnlAdd.Visible = false;
       // LoadGridIOWHead(WCompanyId); //SCS 210301
        pnlPendind.Enabled = true;
        ddlCompany.Enabled = true;
        ddlGroup.SelectedIndex = 0;
        
    }
    #endregion
    #endregion        
    }