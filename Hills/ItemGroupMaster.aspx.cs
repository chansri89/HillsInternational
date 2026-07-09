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

public partial class ItemGroupMaster : System.Web.UI.Page
{
    #region Declaration
    ProcessBus Bus = new ProcessBus(); LibFunctions Lib = new LibFunctions();
    List<EmployeeMasterMsg> EmpList = new List<EmployeeMasterMsg>();
    private static List<ItemGroupMsg> ItemGroupList = new List<ItemGroupMsg>();
    //List<ItemCategoryMsg> ItemCategoryList = new List<ItemCategoryMsg>();
    //List<StateMasterMsg> StateList = new List<StateMasterMsg>();
    //private static List<ItemGroupTypeMsg> GCusTypLst = new List<ItemGroupTypeMsg>();
    private static List<CompanyMessage> CmpList = new List<CompanyMessage>();
   // private static List<GSTINState> GSTSt = new List<GSTINState>();
    //List<ItemGroupTypeMasterMsg> ItemGroupTypeList = new List<ItemGroupTypeMasterMsg>();
    //public static List<ItemGroupinStateMasterMsg> ItemGroupstateList = new List<ItemGroupinStateMasterMsg>();
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
        LoadGridItemGroup(WCompanyId);
    }
    protected void ddlCompanyChanged(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedIndex > 0)
        {
            WCompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
            Pnlgv.Visible = true;
            pnlAdd.Visible = true;
            //LoadItemCategory();
            LoadGridItemGroup(WCompanyId);
           
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
                    ItemGroupSave();
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
                    ItemGroupSave();
                }
            }
        }
        else
        {
        }
    }
  
    protected void btnFilter_Click(object sender, EventArgs e)
    {
       // ItemGroupFilter();
        Int32 WCompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
        LoadGridItemGroup(WCompanyId);
    }
    #region GridEditing
    protected void GrdItemGroup_RowCommand(object sender, GridViewCommandEventArgs e)
    {
     
        int WItemGroupId = Convert.ToInt32(e.CommandArgument.ToString());
        foreach(GridViewRow row in GrdItemGroupMaster.Rows)
        {
            if (((Label)row.FindControl("lblItemGroupId")).Text == WItemGroupId.ToString())
            {
                //txtItemGroupId.Text = ((Label)row.FindControl("lblItemGroupId")).Text;
                txtItemGroupCode.Text = ((Label)row.FindControl("lblItemGroupCode")).Text;
                txtItemGroupName.Text = ((Label)row.FindControl("lblItemGroupName")).Text;
                txtItemGroupId.Text = ((Label)row.FindControl("lblItemGroupId")).Text;
                //string WItemCategoryName = ((Label)row.FindControl("lblItemCategoryName")).Text;
               // Int32 WItemCategoryId = Convert.ToInt32(((Label)row.FindControl("lblItemCategoryId")).Text);
                //LoadItemCategory();
                //foreach (ItemCategoryMsg gs in ItemCategoryList)
                //{
                //    if (gs.ItemCategoryName == WItemCategoryName)
                //    {
                //        ddlItemCategory.SelectedValue = WItemCategoryId.ToString();
                //        break;
                //    }
                //}
              
                chkIsActive.Checked = ((CheckBox)row.FindControl("chkActive")).Checked;
                chkIsActive.Visible = true;
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
                //ddlCompany.Enabled = false;
                WCompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                LoadGridItemGroup(WCompanyId);
                //LoadItemCategory();
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
    //private void LoadItemCategory()
    //{
    //    ItemCategoryMsg ItemCategory = new ItemCategoryMsg();
    //    WCompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
    //    ItemCategory.Flag = "R";
    //    ItemCategory.CompanyId = WCompanyId;
    //    ItemCategoryList = Bus.MasItemCategoryInsertUpdate(ItemCategory);


    //    ddlItemCategory.DataSource = ItemCategoryList;
    //    ddlItemCategory.DataBind();
    //    ddlItemCategory.Items.Insert(0, "--Select Please--");

            
    //}

    private void LoadGridItemGroup(Int32 CompanyId)//To load data into grid.
    {
        ItemGroupMsg Cmp = new ItemGroupMsg();
        Cmp.Flag = "R";
        Cmp.CreatedBy = BaseMsg.EmployeeCode;
        Cmp.CompanyId = CompanyId;
        //Cmp.ItemGroupId =
        ItemGroupList = Bus.MasItemGroupInsertUpdateandDelete(Cmp);
        ItemGroupFilter(ItemGroupList);
       
    }
    private void ItemGroupFilter(List<ItemGroupMsg> cli)
    {

        List<ItemGroupMsg> FCustList = new List<ItemGroupMsg>();
        if (txtFilter.Text.Trim().Length == 0)
        {
            FCustList = cli.ToList();
        }
        else
        {
            foreach (ItemGroupMsg cm in cli)
            {
                if (cm.ItemGroupName.ToUpper().Contains(txtFilter.Text.Trim().ToUpper()))
                {
                    FCustList.Add(cm);
                }
            }

        }
        if (FCustList.Count == 0)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "No Item Group for the Filter Found .." + "');", true);
            FCustList = cli.ToList();
        }
        GrdItemGroupMaster.DataSource = "";
        GrdItemGroupMaster.DataSource = FCustList;
        GrdItemGroupMaster.DataBind();
        Pnlgv.Visible = true;
        pnlAdd.Visible = true;

    }
    private void ItemGroupSave()
    {
        WCompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
        ItemGroupMsg Cmp = new ItemGroupMsg();
        if (btnSave.Text.ToUpper() == "SAVE")
        {
            Cmp.Flag = "I";
            Cmp.ItemGroupId = 0;
            Cmp.IsActive = true;
        }
        else
        {
            Cmp.Flag = "U";
            Cmp.IsActive = chkIsActive.Checked;
            Cmp.ItemGroupId = Convert.ToInt32(txtItemGroupId.Text);
        }
        Cmp.CompanyId = WCompanyId;
        Cmp.ItemGroupCode = txtItemGroupCode.Text.Trim();
        Cmp.ItemGroupName = txtItemGroupName.Text.Trim();
       // Cmp.ItemCategoryId = Convert.ToInt32(ddlItemCategory.SelectedValue);       
        //Cmp.IsActive = chkIsActive.Checked; //scs 230221
        Cmp.CreatedBy = BaseMsg.EmployeeCode;
        ItemGroupList = Bus.MasItemGroupInsertUpdateandDelete(Cmp);
        //Output Dispay
        foreach (ItemGroupMsg cmp in ItemGroupList)
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
        if (txtItemGroupCode.Text.Trim() == "" || Convert.ToInt32(txtItemGroupCode.Text.Trim().Length) == 0)
        {
            DisplayError = DisplayError + " ItemGroupCode is Mandatory .";
            //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.ErrorItemGroupCode + "');", true);
            Error = 1;
        }
        if (txtItemGroupName.Text.Trim() == "" || Convert.ToInt32(txtItemGroupName.Text.Trim().Length) == 0)
        {
            DisplayError = DisplayError + "--" + " ItemGroup Name is Mandatory . ";
            // ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.ErrorItemGroupName + "');", true);
            Error = 1;
        }
        
        //if (ddlItemCategory.SelectedIndex ==0)
        //{
        //    DisplayError = DisplayError + "--" + " Item Category Has to be selected . ";
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
        txtItemGroupCode.Text = "";
        txtItemGroupName.Text = "";
        //ddlItemCategory.SelectedIndex = 0;
        
        Pnlgv.Visible = true;
        pnlAdd.Visible = true;
        pnlPendind.Enabled = true;
        LoadGridItemGroup(Convert.ToInt32(ddlCompany.SelectedValue)); //SCS 210301
        pnlPendind.Enabled = true;
        chkIsActive.Visible = false;

    }
    #endregion
       
    }