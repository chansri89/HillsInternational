using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Resources;

public partial class ItemSubCategoryMaster : System.Web.UI.Page
{ 
    #region Declaration
    ProcessBus Bus = new ProcessBus(); LibFunctions Lib = new LibFunctions();
    List<ItemSubCategoryMsg> ItemSubCategoryList = new List<ItemSubCategoryMsg>();
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
            //if (ItemSubCategoryList.Count == 0)
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
                ItemSubCategorySave();
            }
        }
    }
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        // ClientFilter();
        Int32 WCompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
        LoadGrdItemSubCategoryMaster();
    }
    protected void ddlCompanyChanged(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedIndex > 0)
        {
            WCompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
            Pnlgv.Visible = true;
            pnlAdd.Visible = true;
            LoadGrdItemSubCategoryMaster();
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
               
                LoadGrdItemSubCategoryMaster();
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

    private void LoadGrdItemSubCategoryMaster()
    {
        ItemSubCategoryMsg ItemSubCategory = new ItemSubCategoryMsg();
        WCompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
        ItemSubCategory.Flag = "R";
        ItemSubCategory.CompanyId = WCompanyId;
        ItemSubCategoryList = Bus.MasItemSubCategoryInsertUpdate(ItemSubCategory);
        ItemSubCategoryFilter(ItemSubCategoryList);
        //GrdItemSubCategoryMaster.DataSource = "";
        //GrdItemSubCategoryMaster.DataSource = ItemSubCategoryList;
        //GrdItemSubCategoryMaster.DataBind();
        if (!user.HasPermission(ProgramName, UserPermission.CanEdit.ToString()))
        {
            GrdItemSubCategoryMaster.Columns[GrdItemSubCategoryMaster.Columns.Count - 2].Visible = false;
        }
        if (!user.HasPermission(ProgramName, UserPermission.CanDelete.ToString()))
        {
            GrdItemSubCategoryMaster.Columns[GrdItemSubCategoryMaster.Columns.Count - 1].Visible = false;
        }
        Pnlgv.Visible = true;
    }
    private void ItemSubCategoryFilter(List<ItemSubCategoryMsg> cli)
    {

        List<ItemSubCategoryMsg> FList = new List<ItemSubCategoryMsg>();
        if (txtFilter.Text.Trim().Length == 0)
        {
            FList = cli.ToList();
        }
        else
        {
            foreach (ItemSubCategoryMsg cm in cli)
            {
                if (cm.ItemSubCategoryName.ToUpper().Contains(txtFilter.Text.Trim().ToUpper()))
                {
                    FList.Add(cm);
                }
            }

        }
        if (FList.Count == 0)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "No Item Sub Category for the Filter Found .." + "');", true);
            FList = cli.ToList();
        }
        GrdItemSubCategoryMaster.DataSource = "";
        GrdItemSubCategoryMaster.DataSource = FList;
        GrdItemSubCategoryMaster.DataBind();
        Pnlgv.Visible = true;
        pnlAdd.Visible = true;

    }
    private void ItemSubCategorySave()
    {
        ItemSubCategoryMsg ItemSubCategory = new ItemSubCategoryMsg();
        ItemSubCategory.Flag = "I";
        ItemSubCategory.ItemSubCategoryId = 0;
        ItemSubCategory.ItemSubCategoryName = txtItemSubCategoryName.Text.Trim();
        ItemSubCategory.ItemSubCategoryCode = txtItemSubCategorycode.Text.Trim();
        ItemSubCategory.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
        ItemSubCategory.IsActive = ChkIsActive.Checked;
        ItemSubCategoryList = Bus.MasItemSubCategoryInsertUpdate(ItemSubCategory);
        //Output Dispay
        foreach (ItemSubCategoryMsg ItemSubCategorySave in ItemSubCategoryList)
        {
            if (ItemSubCategorySave.Result == "0")
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.SuccessFullySaved + "');", true);
                AllClear();
                break;

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + ItemSubCategorySave.Result + "');", true);
                break;
            }
        }
    }
    public void ItemSubCategoryUpdate()
    {
        GridViewRow row = GrdItemSubCategoryMaster.Rows[UpdateIndex];
        ItemSubCategoryMsg ItemSubCategory = new ItemSubCategoryMsg();
        ItemSubCategory.Flag = "U";
        ItemSubCategory.ItemSubCategoryId = Convert.ToInt32(((Label)row.FindControl("lblItemSubCategoryId")).Text);
        ItemSubCategory.CompanyId = Convert.ToInt32(((Label)row.FindControl("lblCompanyId")).Text);
        ItemSubCategory.ItemSubCategoryCode = ((TextBox)row.FindControl("txtItemSubCategoryCode")).Text;
        ItemSubCategory.ItemSubCategoryName = ((TextBox)row.FindControl("txtItemSubCategoryName")).Text;

        ItemSubCategory.IsActive = ((CheckBox)row.FindControl("chkActive")).Checked;
        ItemSubCategoryList = Bus.MasItemSubCategoryInsertUpdate(ItemSubCategory);
        GrdItemSubCategoryMaster.EditIndex = -1;

        foreach (ItemSubCategoryMsg ItemSubCategoryUpdate in ItemSubCategoryList)
        {
            if (ItemSubCategoryUpdate.Result == "0")
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.UpdatedSuccessfully + "');", true);

                AllClear();
                break;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + ItemSubCategoryUpdate.Result + "');", true);
                break;
            }
        }
    }
  
    #endregion
    #region Clear
    public void AllClear()
    {

        txtItemSubCategoryName.Text = "";
        txtItemSubCategorycode.Text = "";
        LoadGrdItemSubCategoryMaster();

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
         if ((txtItemSubCategoryName.Text.Trim() == "" || txtItemSubCategoryName.Text.Trim().Length.ToString() == "0"))
        {
            DisplayError=DisplayError+"ItemSubCategory Name Mandatory..";
            Error = 1;
        }
         if (txtItemSubCategorycode.Text.Trim() == "" || txtItemSubCategorycode.Text.Trim().Length.ToString() == "0")
        {
            DisplayError = DisplayError + "--" + "ItemSubCategory Code Mandatory..";
            Error = 1;
        }
        if (Error == 1)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + DisplayError + "');", true);
        }
        return Error;
    }

    private int IsValidGridSave(string ItemSubCategoryName,string ItemSubCategoryCode)
    {

        int Error = 0;
        string DisplayError = "";
        if (ddlCompany.SelectedIndex == 0)
        {
            DisplayError = DisplayError + "Select Company ..";
            Error = 1;
        }
        if ((ItemSubCategoryName.Trim() == "" || ItemSubCategoryName.Trim().Length.ToString() == "0"))
        {
            DisplayError = DisplayError + "ItemSubCategory Name Mandatory.. ";
            Error = 1;
        }
        if (ItemSubCategoryCode.Trim() == "" || ItemSubCategoryCode.Trim().Length.ToString() == "0")
        {
            DisplayError = DisplayError + "--" + "ItemSubCategory Code Is Mandatory..";
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

    protected void GrdItemSubCategoryMaster_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        UpdateIndex = e.RowIndex;
        GridViewRow row = GrdItemSubCategoryMaster.Rows[UpdateIndex];
        TextBox txtItemSubCategoryname = (TextBox)row.FindControl("txtItemSubCategoryName");
        string ItemSubCategoryCode = ((TextBox)row.FindControl("txtItemSubCategoryCode")).Text;
        if (IsValidGridSave(txtItemSubCategoryname.Text.Trim(), ItemSubCategoryCode.Trim()) == 0)
        {
            ItemSubCategoryUpdate();
        }
    }
    
    protected void GrdItemSubCategoryMaster_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GrdItemSubCategoryMaster.EditIndex = -1;
        LoadGrdItemSubCategoryMaster();
    }
    protected void GrdItemSubCategoryMaster_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GrdItemSubCategoryMaster.EditIndex = e.NewEditIndex;
        LoadGrdItemSubCategoryMaster();
        GridViewRow row = GrdItemSubCategoryMaster.Rows[GrdItemSubCategoryMaster.EditIndex];
        TextBox ItemSubCategoryname = (TextBox)row.FindControl("txtItemSubCategoryName");
        ItemSubCategoryname.Focus();
    }

    #endregion
}