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

public partial class CRAMSubGroupMaster : System.Web.UI.Page
{
    #region Declaration
    ProcessBus Bus = new ProcessBus(); LibFunctions Lib = new LibFunctions();
    List<EmployeeMasterMsg> EmpList = new List<EmployeeMasterMsg>();
    private static List<CRAMIOWSubGroupMsg> SubGroupList = new List<CRAMIOWSubGroupMsg>();
    private static List<CRAMIOWGroupMsg> GroupList = new List<CRAMIOWGroupMsg>();
 
    private static List<CompanyMessage> CmpList = new List<CompanyMessage>();

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
            
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        
        AllClear();
       // Pnlgv.Visible = false;
        pnlAdd.Visible = false;
        LoadGridIOWSubGroup(WCompanyId);
        //LoadGridIOWSubGroup(WCompanyId);
    }
    protected void ddlCompanyChanged(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedIndex > 0)
        {
            WCompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
            Pnlgv.Visible = true;
            pnlAdd.Visible = true;
            LoadIOWGroup();
            LoadGridIOWSubGroup(WCompanyId);
           
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Pls Select Company" + "');", true);
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
                    IOWGroupSave();
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
                    IOWGroupSave();
                }
            }
        }
        else
        {
        }
    }
  
    protected void btnFilter_Click(object sender, EventArgs e)
    {
       // IOWGroupFilter();
        Int32 WCompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
        LoadGridIOWSubGroup(WCompanyId);
    }
    #region GridEditing
    protected void GrdIOWSubGroup_RowCommand(object sender, GridViewCommandEventArgs e)
    {
     
        int WIOWSubGroupId = Convert.ToInt32(e.CommandArgument.ToString());
        foreach(GridViewRow row in GrdIOWSubGroupMaster.Rows)
        {
            if (((Label)row.FindControl("lblCRAMSubGroupId")).Text == WIOWSubGroupId.ToString())
            {
                txtCRAMSubGroupId.Text = (((Label)row.FindControl("lblCRAMSubGroupId")).Text);
                txtGroupCode.Text = ((Label)row.FindControl("lblGroupCode")).Text;
                txtSubGroupCode.Text = ((Label)row.FindControl("lblSubGroupCode")).Text;
                txtSubGroupName.Text = ((Label)row.FindControl("lblSubGroupName")).Text;
                foreach (CRAMIOWGroupMsg gs in GroupList)
                {
                    if (gs.GroupCode == txtGroupCode.Text)
                    {
                        ddlGroupName.SelectedValue = txtGroupCode.Text;
                        break;
                    }
                }
              
                //chkIsActive.Checked = ((CheckBox)row.FindControl("chkActive")).Checked;
                //chkIsActive.Visible = true;
                btnSave.Text = "Update";
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
                //ddlCompany.Enabled = false;
                WCompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                LoadGridIOWSubGroup(WCompanyId);
                //LoadIOWCategory();
            }
            else
            {
                ddlCompany.Enabled = true;
            }

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.ErrCompanyData + "');", true);
            // ddlCompany.IOWs.Insert(0, "--Select Please--");
        }

    }
    private void LoadGridIOWSubGroup(Int32 CompanyId)//To load data into grid.
    {
        CRAMIOWSubGroupMsg Cmp = new CRAMIOWSubGroupMsg();
        Cmp.Flag = "R";
        Cmp.CreatedBy = BaseMsg.EmployeeCode;
        Cmp.CompanyId = CompanyId;
        //Cmp.IOWGroupId =
        SubGroupList = Bus.MasCRAMSubGroupInsertUpdate(Cmp);
        IOWGroupFilter(SubGroupList);
        LoadIOWGroup();
    }
    private void LoadIOWGroup()//To load data into grid.
    {
        CRAMIOWGroupMsg Cmp = new CRAMIOWGroupMsg();
        Int32 CompanyId =Convert.ToInt32(ddlCompany.SelectedValue);
        Cmp.Flag = "R";
        Cmp.CreatedBy = BaseMsg.EmployeeCode;
        Cmp.CompanyId = CompanyId;
      
        GroupList = Bus.MasCRAMGroupInsertUpdate(Cmp);
        ddlGroupName.DataSource = GroupList;
        ddlGroupName.DataBind();
        ddlGroupName.Items.Insert(0, "-- Select Please --");
        ddlGroupName.SelectedIndex = 0;
    }
    private void IOWGroupFilter(List<CRAMIOWSubGroupMsg> cli)
    {

        List<CRAMIOWSubGroupMsg> FCustList = new List<CRAMIOWSubGroupMsg>();
        if (txtFilter.Text.Trim().Length == 0)
        {
            FCustList = cli.ToList();
        }
        else
        {
            foreach (CRAMIOWSubGroupMsg cm in cli)
            {
                if (cm.SubGroupName.ToUpper().Contains(txtFilter.Text.Trim().ToUpper()))
                {
                    FCustList.Add(cm);
                }
            }

        }
        if (FCustList.Count == 0)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "No IOW SUB Group for the Filter Found .." + "');", true);
            FCustList = cli.ToList();
        }
        GrdIOWSubGroupMaster.DataSource = "";
        GrdIOWSubGroupMaster.DataSource = FCustList;
        GrdIOWSubGroupMaster.DataBind();
        Pnlgv.Visible = true;
        pnlAdd.Visible = true;

    }
    private void IOWGroupSave()
    {
        WCompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
        CRAMIOWSubGroupMsg Cmp = new CRAMIOWSubGroupMsg();
        if (btnSave.Text.ToUpper() == "SAVE")
        {
            Cmp.Flag = "I";
            Cmp.CRAMSubGroupId = 0;
            Cmp.IsActive = true;
        }
        else
        {
            Cmp.Flag = "U";
            Cmp.IsActive = chkIsActive.Checked;
            Cmp.CRAMSubGroupId = Convert.ToInt32(txtCRAMSubGroupId.Text);
        }
        Cmp.CompanyId = WCompanyId;
        Cmp.GroupCode = ddlGroupName.SelectedValue;//txtGroupCode.Text.Trim();
        Cmp.SubGroupCode = txtSubGroupCode.Text.Trim();
        Cmp.SubGroupName = txtSubGroupName.Text.Trim();
       // Cmp.IOWCategoryId = Convert.ToInt32(ddlIOWCategory.SelectedValue);       
        //Cmp.IsActive = chkIsActive.Checked; //scs 230221
        Cmp.CreatedBy = BaseMsg.EmployeeCode;
        SubGroupList = Bus.MasCRAMSubGroupInsertUpdate(Cmp);
        //Output Dispay
        foreach (CRAMIOWSubGroupMsg cmp in SubGroupList)
        {
            if (cmp.Result == "0")
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.SuccessFullySaved + "');", true);
                AllClear();
                //pnlPendind.Enabled = false;
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
        if (txtSubGroupCode.Text.Trim() == "" || Convert.ToInt32(txtSubGroupCode.Text.Trim().Length) == 0)
        {
            DisplayError = DisplayError + " IOW sub GroupCode is Mandatory .";
            //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.ErrorIOWGroupCode + "');", true);
            Error = 1;
        }
        if (txtSubGroupName.Text.Trim() == "" || Convert.ToInt32(txtSubGroupName.Text.Trim().Length) == 0)
        {
            DisplayError = DisplayError + "--" + " SUB Group Name is Mandatory . ";
            // ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.ErrorIOWGroupName + "');", true);
            Error = 1;
        }
        
        //if (ddlIOWCategory.SelectedIndex ==0)
        //{
        //    DisplayError = DisplayError + "--" + " IOW Category Has to be selected . ";
        //    Error = 1;
        //}
        
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
        txtGroupCode.Text = "";
        txtSubGroupName.Text = "";
        txtSubGroupCode.Text = "";
        ddlGroupName.SelectedIndex = 0;
        
        Pnlgv.Visible = true;
        pnlAdd.Visible = true;
        pnlPendind.Enabled = true;
        LoadGridIOWSubGroup(Convert.ToInt32(ddlCompany.SelectedValue)); //SCS 210301
        pnlPendind.Enabled = true;
        chkIsActive.Visible = false;

    }
    #endregion
       
    }