using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Resources;

public partial class ItemCategoryMaster : System.Web.UI.Page
{ 
    #region Declaration
    ProcessBus Bus = new ProcessBus(); LibFunctions Lib = new LibFunctions();
    List<ItemCategoryMsg> ItemCategoryList = new List<ItemCategoryMsg>();
    private static List<CompanyMessage> CmpList = new List<CompanyMessage>();
    public static int DeleteIndex = 0;
    public static int UpdateIndex = 0;
    UserAccess user = new UserAccess();
    public static string ProgramName = string.Empty;
    Int32 WCompanyId = 0;
    BaseClass BaseMsg = new BaseClass();
    #endregion
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        ProgramName = System.IO.Path.GetFileName(Request.PhysicalPath);
        if (!Page.IsPostBack)
        {
            LoadCompany();
            //if (ItemCategoryList.Count == 0)
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
                ItemCategorySave();
            }
        }
    }
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        // ClientFilter();
        Int32 WCompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
        LoadGrdItemCategoryMaster();
    }
    protected void ddlCompanyChanged(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedIndex > 0)
        {
            WCompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
            Pnlgv.Visible = true;
            pnlAdd.Visible = true;
            LoadGrdItemCategoryMaster();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Location Data Not Available" + "');", true);
            Pnlgv.Visible = false;
            pnlAdd.Visible = false;
        }
    }
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
                ddlCompany.Enabled = false;
               
                LoadGrdItemCategoryMaster();
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

    private void LoadGrdItemCategoryMaster()
    {
        ItemCategoryMsg ItemCategory = new ItemCategoryMsg();
        WCompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
        ItemCategory.Flag = "R";
        ItemCategory.CompanyId = WCompanyId;
        ItemCategoryList = Bus.MasItemCategoryInsertUpdate(ItemCategory);
        ItemCategoryFilter(ItemCategoryList);
        //GrdItemCategoryMaster.DataSource = "";
        //GrdItemCategoryMaster.DataSource = ItemCategoryList;
        //GrdItemCategoryMaster.DataBind();
        if (!user.HasPermission(ProgramName, UserPermission.CanEdit.ToString()))
        {
            GrdItemCategoryMaster.Columns[GrdItemCategoryMaster.Columns.Count - 2].Visible = false;
        }
        if (!user.HasPermission(ProgramName, UserPermission.CanDelete.ToString()))
        {
            GrdItemCategoryMaster.Columns[GrdItemCategoryMaster.Columns.Count - 1].Visible = false;
        }
        Pnlgv.Visible = true;
    }
    private void ItemCategoryFilter(List<ItemCategoryMsg> cli)
    {

        List<ItemCategoryMsg> FList = new List<ItemCategoryMsg>();
        if (txtFilter.Text.Trim().Length == 0)
        {
            FList = cli.ToList();
        }
        else
        {
            foreach (ItemCategoryMsg cm in cli)
            {
                if (cm.ItemCategoryName.ToUpper().Contains(txtFilter.Text.Trim().ToUpper()))
                {
                    FList.Add(cm);
                }
            }

        }
        if (FList.Count == 0)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "No Item Category for the Filter Found .." + "');", true);
            FList = cli.ToList();
        }
        GrdItemCategoryMaster.DataSource = "";
        GrdItemCategoryMaster.DataSource = FList;
        GrdItemCategoryMaster.DataBind();
        Pnlgv.Visible = true;
        pnlAdd.Visible = true;

    }
    private void ItemCategorySave()
    {
        ItemCategoryMsg ItemCategory = new ItemCategoryMsg();
        ItemCategory.Flag = "I";
        ItemCategory.ItemCategoryId = 0;
        ItemCategory.ItemCategoryName = txtItemCategoryName.Text.Trim();
        ItemCategory.ItemCategoryCode = txtItemCategorycode.Text.Trim();
        ItemCategory.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
        ItemCategory.IsActive = ChkIsActive.Checked;
        ItemCategoryList = Bus.MasItemCategoryInsertUpdate(ItemCategory);
        //Output Dispay
        foreach (ItemCategoryMsg ItemCategorySave in ItemCategoryList)
        {
            if (ItemCategorySave.Result == "0")
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.SuccessFullySaved + "');", true);
                AllClear();
                break;

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + ItemCategorySave.Result + "');", true);
                break;
            }
        }
    }
    public void ItemCategoryUpdate()
    {
        GridViewRow row = GrdItemCategoryMaster.Rows[UpdateIndex];
        ItemCategoryMsg ItemCategory = new ItemCategoryMsg();
        ItemCategory.Flag = "U";
        ItemCategory.ItemCategoryId = Convert.ToInt32(((Label)row.FindControl("lblItemCategoryId")).Text);
        ItemCategory.CompanyId = Convert.ToInt32(((Label)row.FindControl("lblCompanyId")).Text);
        ItemCategory.ItemCategoryCode = ((TextBox)row.FindControl("txtItemCategoryCode")).Text;
        ItemCategory.ItemCategoryName = ((TextBox)row.FindControl("txtItemCategoryName")).Text;

        ItemCategory.IsActive = ((CheckBox)row.FindControl("chkActive")).Checked;
        ItemCategoryList = Bus.MasItemCategoryInsertUpdate(ItemCategory);
        GrdItemCategoryMaster.EditIndex = -1;

        foreach (ItemCategoryMsg ItemCategoryUpdate in ItemCategoryList)
        {
            if (ItemCategoryUpdate.Result == "0")
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.UpdatedSuccessfully + "');", true);

                AllClear();
                break;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + ItemCategoryUpdate.Result + "');", true);
                break;
            }
        }
    }
  
    #endregion
    #region Clear
    public void AllClear()
    {

        txtItemCategoryName.Text = "";
        txtItemCategorycode.Text = "";
        LoadGrdItemCategoryMaster();

    }
    #endregion
    #region Validation
    private int IsValidSave()
    {

        int Error = 0;
         string DisplayError = "";
         if (ddlCompany.SelectedIndex == 0)
         {
             DisplayError = DisplayError + "Select Company ..";
             Error = 1;
         }
         if ((txtItemCategoryName.Text.Trim() == "" || txtItemCategoryName.Text.Trim().Length.ToString() == "0"))
        {
            DisplayError=DisplayError+"ItemCategory Name Mandatory..";
            Error = 1;
        }
         if (txtItemCategorycode.Text.Trim() == "" || txtItemCategorycode.Text.Trim().Length.ToString() == "0")
        {
            DisplayError = DisplayError + "--" + "ItemCategory Code Mandatory..";
            Error = 1;
        }
        if (Error == 1)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + DisplayError + "');", true);
        }
        return Error;
    }

    private int IsValidGridSave(string ItemCategoryName,string ItemCategoryCode)
    {

        int Error = 0;
        string DisplayError = "";
        if (ddlCompany.SelectedIndex == 0)
        {
            DisplayError = DisplayError + "Select Company ..";
            Error = 1;
        }
        if ((ItemCategoryName.Trim() == "" || ItemCategoryName.Trim().Length.ToString() == "0"))
        {
            DisplayError = DisplayError + "ItemCategory Name Mandatory.. ";
            Error = 1;
        }
        if (ItemCategoryCode.Trim() == "" || ItemCategoryCode.Trim().Length.ToString() == "0")
        {
            DisplayError = DisplayError + "--" + "ItemCategory Code Is Mandatory..";
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

    protected void GrdItemCategoryMaster_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        UpdateIndex = e.RowIndex;
        GridViewRow row = GrdItemCategoryMaster.Rows[UpdateIndex];
        TextBox txtItemCategoryname = (TextBox)row.FindControl("txtItemCategoryName");
        string ItemCategoryCode = ((TextBox)row.FindControl("txtItemCategoryCode")).Text;
        if (IsValidGridSave(txtItemCategoryname.Text.Trim(), ItemCategoryCode.Trim()) == 0)
        {
            ItemCategoryUpdate();
        }
    }
    
    protected void GrdItemCategoryMaster_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GrdItemCategoryMaster.EditIndex = -1;
        LoadGrdItemCategoryMaster();
    }
    protected void GrdItemCategoryMaster_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GrdItemCategoryMaster.EditIndex = e.NewEditIndex;
        LoadGrdItemCategoryMaster();
        GridViewRow row = GrdItemCategoryMaster.Rows[GrdItemCategoryMaster.EditIndex];
        TextBox ItemCategoryname = (TextBox)row.FindControl("txtItemCategoryName");
        ItemCategoryname.Focus();
    }

    #endregion
}