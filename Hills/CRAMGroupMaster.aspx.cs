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

public partial class CRAMGroupMaster : System.Web.UI.Page
{
    #region Declaration
    ProcessBus Bus = new ProcessBus(); LibFunctions Lib = new LibFunctions();
    List<EmployeeMasterMsg> EmpList = new List<EmployeeMasterMsg>();
    private static List<CRAMIOWGroupMsg> IOWGroupList = new List<CRAMIOWGroupMsg>();
 
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
        LoadGridIOWGroup(WCompanyId);
    }
    protected void ddlCompanyChanged(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedIndex > 0)
        {
            WCompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
            Pnlgv.Visible = true;
            pnlAdd.Visible = true;
            //LoadIOWCategory();
            LoadGridIOWGroup(WCompanyId);
           
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
        LoadGridIOWGroup(WCompanyId);
    }
    #region GridEditing
    protected void GrdIOWGroup_RowCommand(object sender, GridViewCommandEventArgs e)
    {
     
        int WIOWGroupId = Convert.ToInt32(e.CommandArgument.ToString());
        foreach(GridViewRow row in GrdIOWGroupMaster.Rows)
        {
            if (((Label)row.FindControl("lblCRAMGroupId")).Text == WIOWGroupId.ToString())
            {
                //txtIOWGroupId.Text = ((Label)row.FindControl("lblIOWGroupId")).Text;
                txtGroupCode.Text = ((Label)row.FindControl("lblGroupCode")).Text;
                txtGroupName.Text = ((Label)row.FindControl("lblGroupName")).Text;
                txtCRAMGroupId.Text = ((Label)row.FindControl("lblCRAMGroupId")).Text;
                //string WIOWCategoryName = ((Label)row.FindControl("lblIOWCategoryName")).Text;
               // Int32 WIOWCategoryId = Convert.ToInt32(((Label)row.FindControl("lblIOWCategoryId")).Text);
                //LoadIOWCategory();
                //foreach (IOWCategoryMsg gs in IOWCategoryList)
                //{
                //    if (gs.IOWCategoryName == WIOWCategoryName)
                //    {
                //        ddlIOWCategory.SelectedValue = WIOWCategoryId.ToString();
                //        break;
                //    }
                //}
              
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
                LoadGridIOWGroup(WCompanyId);
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
 
    private void LoadGridIOWGroup(Int32 CompanyId)//To load data into grid.
    {
        CRAMIOWGroupMsg Cmp = new CRAMIOWGroupMsg();
        Cmp.Flag = "R";
        Cmp.CreatedBy = BaseMsg.EmployeeCode;
        Cmp.CompanyId = CompanyId;
        //Cmp.IOWGroupId =
        IOWGroupList = Bus.MasCRAMGroupInsertUpdate(Cmp);
        IOWGroupFilter(IOWGroupList);
       
    }
    private void IOWGroupFilter(List<CRAMIOWGroupMsg> cli)
    {

        List<CRAMIOWGroupMsg> FCustList = new List<CRAMIOWGroupMsg>();
        if (txtFilter.Text.Trim().Length == 0)
        {
            FCustList = cli.ToList();
        }
        else
        {
            foreach (CRAMIOWGroupMsg cm in cli)
            {
                if (cm.GroupName.ToUpper().Contains(txtFilter.Text.Trim().ToUpper()))
                {
                    FCustList.Add(cm);
                }
            }

        }
        if (FCustList.Count == 0)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "No IOW Group for the Filter Found .." + "');", true);
            FCustList = cli.ToList();
        }
        GrdIOWGroupMaster.DataSource = "";
        GrdIOWGroupMaster.DataSource = FCustList;
        GrdIOWGroupMaster.DataBind();
        Pnlgv.Visible = true;
        pnlAdd.Visible = true;

    }
    private void IOWGroupSave()
    {
        WCompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
        CRAMIOWGroupMsg Cmp = new CRAMIOWGroupMsg();
        if (btnSave.Text.ToUpper() == "SAVE")
        {
            Cmp.Flag = "I";
            Cmp.CRAMGroupId = 0;
            Cmp.IsActive = true;
        }
        else
        {
            Cmp.Flag = "U";
            Cmp.IsActive = true;//chkIsActive.Checked;
            Cmp.CRAMGroupId = Convert.ToInt32(txtCRAMGroupId.Text);
        }
        Cmp.CompanyId = WCompanyId;
        Cmp.GroupCode = txtGroupCode.Text.Trim();
        Cmp.GroupName = txtGroupName.Text.Trim();
       // Cmp.IOWCategoryId = Convert.ToInt32(ddlIOWCategory.SelectedValue);       
        //Cmp.IsActive = chkIsActive.Checked; //scs 230221
        Cmp.CreatedBy = BaseMsg.EmployeeCode;
        IOWGroupList = Bus.MasCRAMGroupInsertUpdate(Cmp);
        //Output Dispay
        foreach (CRAMIOWGroupMsg cmp in IOWGroupList)
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
        if (txtGroupCode.Text.Trim() == "" || Convert.ToInt32(txtGroupCode.Text.Trim().Length) == 0)
        {
            DisplayError = DisplayError + " IOWGroupCode is Mandatory .";
            //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.ErrorIOWGroupCode + "');", true);
            Error = 1;
        }
        if (txtGroupName.Text.Trim() == "" || Convert.ToInt32(txtGroupName.Text.Trim().Length) == 0)
        {
            DisplayError = DisplayError + "--" + " IOWGroup Name is Mandatory . ";
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
        txtGroupName.Text = "";
        //ddlIOWCategory.SelectedIndex = 0;
        
        Pnlgv.Visible = true;
        pnlAdd.Visible = true;
        pnlPendind.Enabled = true;
        LoadGridIOWGroup(Convert.ToInt32(ddlCompany.SelectedValue)); //SCS 210301
        pnlPendind.Enabled = true;
        chkIsActive.Visible = false;

    }
    #endregion

  
}