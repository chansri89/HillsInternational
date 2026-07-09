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

public partial class CRAMIOWHeadDtlMaster : System.Web.UI.Page
{
    #region Declaration
    ProcessBus Bus = new ProcessBus(); LibFunctions Lib = new LibFunctions();
    //List<EmployeeMasterMsg> EmpList = new List<EmployeeMasterMsg>();
    List<CRAMIOWHeadMsg> IOWList = new List<CRAMIOWHeadMsg>();
    List<CRAMIOWDtlMsg> IOWdtlList = new List<CRAMIOWDtlMsg>();
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
            //pnlAdd.Visible = false;
            LoadCompany();
            PnlContext.Visible = false;
            pnlIOWItem.Visible = false;
            pnlIOWAdd.Visible = false;
            pnlIOW.Visible = false;
            Pnlgv.Visible = false;
            
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        
        AllClear();
        ClearText();
        Pnlgv.Visible = false;
        //pnlAdd.Visible = false;
        ddlCompany.SelectedIndex = 0;
    }
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedIndex > 0 )
        {
            LoadGridIOWHead(Convert.ToInt32(ddlCompany.SelectedValue));
            pnlIOW.Visible = false;
            pnlIOWAdd.Visible = false;
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Company, Group and Sub group should be selected." + "');", true);
            Pnlgv.Visible = false;
           // pnlAdd.Visible = false;
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
            //pnlAdd.Visible = false;
        }
    }
    protected void ddlGroup_Changed(object sender, EventArgs e)
    {
        string WGroupCode = "0";
        if (ddlCompany.SelectedIndex > 0 && ddlGroup.SelectedIndex >0)
        {
            WCompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
            WGroupCode = ddlGroup.SelectedValue;           
        }
        else
        {
            WGroupCode = "0";
            //Pnlgv.Visible = false;
            //pnlAdd.Visible = false;
        }
        LoadIOWSubGroup(WCompanyId, WGroupCode);
    }
    protected void ddlItemChanged(object sender, EventArgs e)
    {
        if (ddlItem.SelectedIndex > 0)
        {
            Int32 WItemId = Convert.ToInt32(ddlItem.SelectedValue);
            LoadddlItem();
            ddlItem.SelectedValue = WItemId.ToString();
            //foreach (ItemMsg im in ITList)
            //{
            //    if (im.ItemId == WItemId)
            //    {
            //        txtItemCode.Text = im.ItemCode;
            //        txtItemUOM.Text = im.ItemUOM;
            //        chkIsItemImported.Checked = im.IsImported;
            //        break;
            //    }
            //}
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Item is Not Selected" + "');", true);
        }
    }
    protected void btnSaveIOW_Click(object sender, EventArgs e)
    {
        if (IsValidSaveIOW() == 0)
        {
            CRAMIOWSave();
        }

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (IsValidAdd() == 0)
        {
            if (btnAdd.Text == "Add")
            {
                //btnAddclick();
            }
            else
            {
                btnAdd.Text = "Add";
               // btnUpdateClick();
            }
        }

    }
    protected void btnItemSave_Click(object sender, EventArgs e)
    {
        List<IOWItemMsg> IowItemLst = new List<IOWItemMsg>();
        //IowItemLst = LoadGridToList();
        //IOWItemInsertUpdate(IowItemLst);   //Save item
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //if (btnSave.Text.ToUpper() == "SAVE")
        //{
        //    if (!user.HasPermission(ProgramName, UserPermission.CanCreate.ToString()))
        //    {
        //        ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.CreatePermissionRestricted + "');", true);
        //    }
        //    else
        //    {

        //        if (IsValidSave() == 0)
        //        {
        //            IOWSave();
        //        }
        //    }
        //}
        //else if (btnSave.Text.ToUpper() == "UPDATE")
        //{
        //    if (!user.HasPermission(ProgramName, UserPermission.CanEdit.ToString()))
        //    {
        //        ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.EditPermissionRestricted + "');", true);
        //        AllClear();
        //    }
        //    else
        //    {

        //        if (IsValidSave() == 0)
        //        {
        //            IOWSave();
        //        }
        //    }
        //}
        //else
        //{
        //}
    }
     #region GridEditing
    protected void GrdIOWHead_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string WCommandName = e.CommandName.ToString(); 
        string WIOWCode = e.CommandArgument.ToString();
        if (WCommandName.ToUpper() == "SELIOW") //This it to Modify the IOW data
        {
            foreach (GridViewRow row in GrdIOWHeadMaster.Rows)
            {
                if (((Label)row.FindControl("lblIOWHeadCode")).Text == WIOWCode.ToString())
                {
                    txtCRAMIOWHeadCode.Text = ((Label)row.FindControl("lblIOWHeadCode")).Text;
                    txtCRAMIOWHeadName.Text = ((Label)row.FindControl("lblIOWHeadName")).Text;
                    txtIOWCode.Text = txtCRAMIOWHeadCode.Text+"."; // first charcaters to match IOW Headcode
                    Pnlgv.Visible = false;
                    pnlIOWAdd.Visible = true;
                    LoadGridIOWDtl(Convert.ToInt32(ddlCompany.SelectedValue));
                    pnlIOW.Visible = true;
                    break;
                }
            }
        }
    }
    protected void GrdIOWDtl_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string WCommandName = e.CommandName.ToString();
        string WIOWCode = e.CommandArgument.ToString();
        if (WCommandName.ToUpper() == "IOW") //This it to Modify the IOW data
        {
            foreach (GridViewRow row in grdIOWdtl.Rows)
            {
                if (((Label)row.FindControl("lblIOWCode")).Text == WIOWCode.ToString())
                {
                    txtIOWCode.Text = ((Label)row.FindControl("lblIOWCode")).Text;
                    txtIOWDescription.Text = ((Label)row.FindControl("lblIOWDescription")).Text;
                    txtCRAMIOWHeadCode.Text = ((Label)row.FindControl("lblPreviousIOWLevel")).Text;

                    txtCRAMIOWQuantity.Text = ((Label)row.FindControl("lblIOWQty")).Text;
                    txtCRAMIOWUOM.Text = ((Label)row.FindControl("lblIOWUOM")).Text;
                    chkIsTempIOW.Checked = ((CheckBox)row.FindControl("chkIsTemproryIOW")).Checked;
                    Pnlgv.Visible = false;
                    pnlIOWAdd.Visible = true;
                    pnlIOW.Visible = false;
                    btnSaveIOW.Text = "UPDATE IOW";
                    break;
                }
            }
        }
    }

    protected void grdIOWItem_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string WCommandName = e.CommandName.ToString();
        string WItemId = e.CommandArgument.ToString();
        txtGridItemId.Text = ""; // initialise txtGridItemId selected
        if (WCommandName == "IOWItemMod")
        {
            txtGridItemId.Text = WItemId;
            foreach (GridViewRow row in grdIOWItem.Rows)
            {
                if (((Label)row.FindControl("lblItemId")).Text == WItemId)
                {
                    //txtIOWcd.Text = ((Label)row.FindControl("lblIOWCode")).Text;
                    //txtIOWNm.Text = ((Label)row.FindControl("lblIOWName")).Text;
                    LoadddlItem();
                    ddlItem.SelectedValue = WItemId;
                    txtItemQty.Text = ((Label)row.FindControl("lblItemQty")).Text;
                    txtWastage.Text = ((Label)row.FindControl("lblWastage")).Text;
                    txtItemUOM.Text = ((Label)row.FindControl("lblItemUOM")).Text;
                    btnAdd.Text = "Update";
                    Pnlgv.Visible = false;
                 //   DelIOWItem(txtGridItemId.Text);  // delet the item in grid as it is moved for modification
                    break;
                }
            }
        }
        else
        {
            // delete IOWItem
            //DelIOWItem(WItemId);

        }



    }

    #endregion
    #endregion
    #region Methods
    #region IOW
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
        //txtIOWHeadCode.Enabled = true;
    }
    private void CompanyChanged()
    {
        WCompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
        Pnlgv.Visible = true;
       // pnlAdd.Visible = true;
        AllClear();
        LoadIOWGroup(WCompanyId);
        //LoadIOWSubGroup(WCompanyId);
       // LoadGridIOWHead(Convert.ToInt32(ddlCompany.SelectedValue));
    }
    private void LoadIOWSubGroup(Int32 Companyid, String WGrpcode)
    {
        List<CRAMIOWSubGroupMsg> sglst = new List<CRAMIOWSubGroupMsg>();
        CRAMIOWSubGroupMsg Cmp = new CRAMIOWSubGroupMsg();
        Cmp.Flag = "R";
        Cmp.CreatedBy = BaseMsg.EmployeeCode;
        Cmp.CompanyId = Companyid;
        SgrpList = Bus.MasCRAMSubGroupInsertUpdate(Cmp);
        if (WGrpcode == "0")
        {
            sglst = SgrpList;
        }
        else
        {
            sglst = (from sg in SgrpList where sg.GroupCode == WGrpcode select sg).ToList();
        }
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
        ddlGroup.Items.Insert(0, "ALL");
        ddlGroup.SelectedIndex = 0;
        ddlsubGroup.Items.Insert(0, "ALL");
    }
    private void LoadGridIOWHead(Int32 CompanyId)//To load data into grid.
    {
        CRAMIOWHeadMsg iow = new CRAMIOWHeadMsg();
        iow.CompanyId = CompanyId;
        iow.Flag = "R";
        IOWList = Bus.MasCRAMIOWHeadInsertUpdate(iow);
        IOWFilter(IOWList);
    }
    private void LoadGridIOWDtl(Int32 CompanyId)//To load data into grid.
    {
        CRAMIOWDtlMsg iow = new CRAMIOWDtlMsg();
        iow.CompanyId = CompanyId;
        iow.PreviousIOWLevel = txtCRAMIOWHeadCode.Text;
        iow.Flag = "R";
        IOWdtlList = Bus.MasCRAMIOWDtlInsertUpdate(iow);
        LoadGridIOwDtl(IOWdtlList);
    }
    private void LoadGridIOwDtl(List<CRAMIOWDtlMsg> DtlList)
    {
        grdIOWdtl.DataSource = DtlList;
        grdIOWdtl.DataBind();
        pnlIOW.Visible = true;
    }
    private void IOWFilter(List<CRAMIOWHeadMsg> IOwlst)
    {
        List<CRAMIOWHeadMsg> WIOWList = new List<CRAMIOWHeadMsg>();
        string WGroupCode = ""; string WSubGroupCode = "";
        if (ddlGroup.SelectedIndex == 0)
        {
            WGroupCode = "0";
            WIOWList = IOwlst.ToList();
        }
        else
        {
            WGroupCode = ddlGroup.SelectedValue;
            WIOWList = (from iow in IOwlst where iow.GroupCode == WGroupCode select iow).ToList();
        }
        if (ddlsubGroup.SelectedIndex == 0)
        {
            WSubGroupCode = "0";
            WIOWList = WIOWList.ToList();
        }
        else
        {
            WSubGroupCode = ddlsubGroup.SelectedValue;
            WIOWList = (from iow in WIOWList where iow.SubGroupCode == WSubGroupCode select iow).ToList();
        }

        List<CRAMIOWHeadMsg> FIOWList = new List<CRAMIOWHeadMsg>();
        if (txtFilter.Text.Trim().Length == 0 )
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
        //pnlAdd.Visible = true;
   }
    private void LoadddlItem()//To load data into grid.
    {
        if (ddlCompany.SelectedIndex > 0) //&& ddlItemCategory.SelectedIndex > 0 && ddlItemSubCategory.SelectedIndex > 0)
        {
            ItemMsg Cmp = new ItemMsg();
            Cmp.Flag = "R";
            Cmp.CreatedBy = BaseMsg.EmployeeCode;
            Cmp.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
            //if (ddlItemCategory.SelectedIndex == 0)
            //{
            Cmp.ItemCategoryId = 0;
            //}
            //else
            //{
            //    Cmp.ItemCategoryId = Convert.ToInt32(ddlItemCategory.SelectedValue);
            //}
            //if (ddlItemSubCategory.SelectedIndex == 0)
            //{
            Cmp.ItemSubCategoryId = 0;
            //}
            //else
            //{
            //     Cmp.ItemSubCategoryId = Convert.ToInt32(ddlItemSubCategory.SelectedValue);
            //}
            // List<ItemMsg> ITList = Bus.MasItemMasterInsertUpdateandDelete(Cmp);
            //ITList = Bus.MasItemMasterInsertUpdateandDelete(Cmp);
            //ddlLoadItem(ITList);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Pls Select Company " + "');", true);
            Pnlgv.Visible = false;
           // pnlAdd.Visible = false;
        }
    }
    private void ddlLoadItem(List<ItemMsg> ITList)
    {
        ddlItem.DataSource = ITList;
        ddlItem.DataBind();
        ddlItem.Items.Insert(0, "--Select Please--");
        ddlItem.SelectedIndex = 0;
        ddlItem.Enabled = true;
    }
    private void CRAMIOWSave()
    {
        WCompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
        CRAMIOWDtlMsg iow = new CRAMIOWDtlMsg();
        if (btnSaveIOW.Text.ToUpper() == "UPDATE IOW")
        {
            iow.Flag = "U";
        }
        else
        {
            iow.Flag = "I";
        }
       
        iow.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
        iow.PreviousIOWLevel = txtCRAMIOWHeadCode.Text;
        iow.IOWCode = txtIOWCode.Text;
        iow.IOWDescription = txtIOWDescription.Text;
        iow.IOWUOM = txtCRAMIOWUOM.Text;
        iow.IOWQuantity = Convert.ToDouble(txtCRAMIOWQuantity.Text);
        iow.IsTemproryIOW = chkIsTempIOW.Checked;
        iow.CreatedBy = BaseMsg.EmployeeCode;
        IOWdtlList = Bus.MasCRAMIOWDtlInsertUpdate(iow);
        foreach (CRAMIOWDtlMsg iowr in IOWdtlList)
        {
            if (iowr.Result.Substring(0, 1) != "0")
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + iowr.Result + "');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + txtIOWCode.Text + " Saved Successfully" + "');", true);
                LoadGridIOwDtl(IOWdtlList);
                ClearText();
            }
        }
    }
    //private void IOWSave()
    //{
    //    WCompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
    //    CRAMIOWHeadMsg Cmp = new CRAMIOWHeadMsg();
    //    //if (btnSave.Text.ToUpper() == "SAVE")
    //    //{
    //    //    Cmp.Flag = "I";
    //    //    Cmp.IsActive = true;
    //    //}
    //    //else
    //    //{
    //    //    Cmp.Flag = "U";
    //    //    Cmp.IsActive = true;// Cmp.IsActive;
    //    //}
    //    //Cmp.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue); //WCompanyId;
    //    //Cmp.GroupCode = ddlGroup.SelectedValue; //scs 220217 introduced
    //    //Cmp.SubGroupCode = ddlsubGroup.SelectedValue;
    //    //Cmp.IOWHeadCode = txtIOWHeadCode.Text.Trim();
    //    //Cmp.IOWHeadName = txtIOWHeadName.Text.Trim();    
    //    //Cmp.CreatedBy = BaseMsg.EmployeeCode;
    //    //IOWList = Bus.MasCRAMIOWHeadInsertUpdate(Cmp);
    //    ////Output Dispay
    //    //foreach (CRAMIOWHeadMsg cmp in IOWList)
    //    //{
    //    //    if (cmp.Result == "0")
    //    //    {
    //    //        ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.SuccessFullySaved + "');", true);
    //    //        AllClear();
    //    //        //pnlPendind.Enabled = false;
    //    //        txtIOWHeadCode.Enabled = true;
    //    //        btnSave.Text = "Save";
    //    //        break;

    //    //    }
    //    //    else
    //    //    {
    //    //        ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + cmp.Result + "');", true);
    //    //        break;
    //    //    }
    //    //}
    //} //not used
    #endregion

    #region Validation
    private int IsValidSave()
    {
        int Error = 0;
        string DisplayError = "";
        //if (txtIOWHeadCode.Text.Trim() == "" || Convert.ToInt32(txtIOWHeadCode.Text.Trim().Length) == 0)
        //{
        //    DisplayError = DisplayError + " IOW Head Code is Mandatory .";
        //    Error = 1;
        //}
        //if (txtIOWHeadName.Text.Trim() == "" || Convert.ToInt32(txtIOWHeadName.Text.Trim().Length) == 0)
        //{
        //    DisplayError = DisplayError + "--" + " IOW Head Name is Mandatory . ";
        //    Error = 1;
        //}
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
    private int IsValidAdd()
    {
        int Error = 0;
        string DisplayError = "";
        if (ddlItem.SelectedIndex == 0)
        {
            DisplayError = DisplayError + "--" + " Item Is Mandatory pls Select .... ";
            Error = 1;
        }

        if (txtItemQty.Text.Trim() == "" || Convert.ToInt32(txtItemQty.Text.Trim().Length) == 0)
        {
            DisplayError = DisplayError + "--" + " Item Quantity is Mandatory . ";
            Error = 1;
        }
        try
        {
            Double Qty = Convert.ToDouble(txtItemQty.Text);
            if (Qty <= 0)
            {
                DisplayError = DisplayError + "--" + " Item Quantity to be numeric and greater than 0  . ";
                Error = 1;
            }
        }
        catch
        {
            DisplayError = DisplayError + "--" + " Item Quantity to be numeric  . ";
            Error = 1;
        }
        try
        {
            if (txtWastage.Text == "")
            {
                txtWastage.Text = "0";
            }
            else
            {
                Double Wastage = Convert.ToDouble(txtWastage.Text);
                if (Wastage < 0)
                {
                    DisplayError = DisplayError + "--" + " Wastage to be numeric and Positive number  . ";
                    Error = 1;
                }
            }

        }
        catch
        {
            DisplayError = DisplayError + "--" + " Wastage to be numeric  . ";
            Error = 1;
        }
        int DupItemKount = 0;
        foreach (GridViewRow gvr in grdIOWItem.Rows)
        {
            if (((Label)gvr.FindControl("lblItemId")).Text == (ddlItem.SelectedValue))
            {
                DupItemKount = DupItemKount + 1;
            }
        }
        if (DupItemKount > 0)
        {
            DisplayError = DisplayError + "--" + " Item Getting Added is Already Available in the Grid..Pls Select Some other Item  . ";
            Error = 1;
        }
        if (Error == 1)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + DisplayError + "');", true);
        }
        return Error;
    }
    private int IsValidSaveIOW()
    {
        int Error = 0;
        string DisplayError = "";
        if (txtIOWCode.Text.Trim() == "" || Convert.ToInt32(txtIOWCode.Text.Trim().Length) == 0)
        {
            DisplayError = DisplayError + "--" + " IOW code is Mandatory . ";
            Error = 1;
        }
        if (txtIOWDescription.Text.Trim() == "" || Convert.ToInt32(txtIOWDescription.Text.Trim().Length) == 0)
        {
            DisplayError = DisplayError + "--" + " IOW Description is Mandatory . ";
            Error = 1;
        }
        if (txtCRAMIOWUOM.Text.Trim() == "" || Convert.ToInt32(txtCRAMIOWUOM.Text.Trim().Length) == 0)
        {
            DisplayError = DisplayError + "--" + " IOW UOM is Mandatory . ";
            Error = 1;
        }
        try
        {
            Double Qty = Convert.ToDouble(txtCRAMIOWQuantity.Text);
            if (Qty <= 0)
            {
                DisplayError = DisplayError + "--" + " IOW Quantity to be numeric and greater than 0  . ";
                Error = 1;
            }
        }
        catch
        {
            DisplayError = DisplayError + "--" + " IOW Quantity to be numeric  . ";
            Error = 1;
        }
        string x = txtIOWCode.Text.Substring(0, txtCRAMIOWHeadCode.Text.Length);
        if (x != txtCRAMIOWHeadCode.Text)
        {
            DisplayError = DisplayError + "--" + " IOWHead Code should be in the begining and part of the IOW Code  . ";
            Error = 1;
        }
        try
        {
            if (txtIOWCode.Text.Substring(txtCRAMIOWHeadCode.Text.Length , 1) != ".")
            {
                DisplayError = DisplayError + "--" + " IOW Code should start with . after Head Code ";
                Error = 1;
            }
        }
        catch
        {
            DisplayError = DisplayError + "--" + " IOW Code should start with . after Head Code ";
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
    private void ClearText()
    {
        txtIOWCode.Text = "";
        txtIOWDescription.Text = "";
        txtCRAMIOWUOM.Text = "";
        txtCRAMIOWQuantity.Text = "";
        btnSaveIOW.Text = "Save IOW";
    }
    public void AllClear()
    {
        ClearText();
        Pnlgv.Visible = true;
       // pnlAdd.Visible = true;
       // LoadGridIOWHead(WCompanyId); //SCS 210301
        pnlPendind.Enabled = true;
        ddlCompany.Enabled = true;
        pnlIOW.Visible = false;
        pnlIOWAdd.Visible = false;
    }
    #endregion
    #endregion        
    }