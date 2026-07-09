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

public partial class CRAMIOWHeadDtlContextMaster : System.Web.UI.Page
{
    #region Declaration
    ProcessBus Bus = new ProcessBus(); LibFunctions Lib = new LibFunctions();
    //List<EmployeeMasterMsg> EmpList = new List<EmployeeMasterMsg>();
    List<CRAMIOWHeadMsg> IOWList = new List<CRAMIOWHeadMsg>();
    List<CRAMIOWDtlMsg> IOWdtlList = new List<CRAMIOWDtlMsg>();
    private static List<CompanyMessage> CmpList = new List<CompanyMessage>();
    public static List<CRAMIOWGroupMsg> GrpList = new List<CRAMIOWGroupMsg>();
    public static List<CRAMIOWSubGroupMsg> SgrpList = new List<CRAMIOWSubGroupMsg>();
    List<ItemMsg> ITList = new List<ItemMsg>();
    List<IOWItemMsg> IowItem = new List<IOWItemMsg>();
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
            LoadCompany();
            ClearText();
            AllClear();           
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        AllClear();
        ClearText();
        ddlCompany.SelectedIndex = 0;
    }
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        ClearText();
        AllClear();
        if (ddlCompany.SelectedIndex > 0 )
        {
            LoadGridIOWHead(Convert.ToInt32(ddlCompany.SelectedValue));
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
            foreach (ItemMsg im in ITList)
            {
                if (im.ItemId == WItemId)
                {
                    txtItemCode.Text = im.ItemCode;
                    txtItemUOM.Text = im.ItemUOM;
                    chkIsImported.Checked = im.IsImported;
                    break;
                }
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Item is Not Selected" + "');", true);
        }
    }

 
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (IsValidAdd() == 0)
        {
            if (btnAdd.Text == "Add")
            {
                List<IOWItemMsg> itlst = IowItemgrid();
                List<IOWItemMsg> itlstAdd = iowitemAdd();
                IowItem = itlst.ToList();
                IowItem = IowItem.Concat(itlstAdd).OrderBy(m=> m.ContextSrlNo).ToList();
                grdIOWItem.DataSource = IowItem;
                grdIOWItem.DataBind();
                ClearItemAddtxt();
                pnlIOWItem.Visible = true;
                Panel1.Visible = false;
                chkIsItem.Checked = true;
                chkItemChanged();
            }
            else
            {
                btnAdd.Text = "Add";
               // btnUpdateClick();
            }
        }

    }
    protected void chkIsItem_Changed(object sender, EventArgs e)
    {
        chkItemChanged();
    }
    protected void btnIOWItemFilter_Click(object sender, EventArgs e) //for filtering to select IOW Item
    {
        IOWItemFilter();
    }

    protected void btnItemSave_Click(object sender, EventArgs e)
    {
        if (grdIOWItem.Rows.Count > 0)
        {
            List<IOWItemMsg> itlst = IowItemgrid();
            IowItem = Bus.MasCRAMIOWItemInsertUpdate(itlst, BaseMsg.EmployeeCode);   //Save item
            foreach (IOWItemMsg im in IowItem)
            {
                if (im.Result.Substring(0, 1) == "0")
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.SuccessFullySaved + "');", true);
                    AllClear();
                    break;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + im.Result + "');", true);
                }
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "No Iem in the Grid for Saving" + "');", true);
        }
    }

     #region GridEditing
    protected void GrdIOWHead_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        List<CRAMIOWDtlMsg> iowlst = new List<CRAMIOWDtlMsg>();
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
                    Pnlgv.Visible = false;
                    iowlst = LoadGridIOWDtl(Convert.ToInt32(ddlCompany.SelectedValue), txtCRAMIOWHeadCode.Text);
                    LoadGridIOwDtlDisp(iowlst);
                    pnlIOW.Visible = true;
                    break;
                }
            }
        }
    }
    protected void GrdIOWDtl_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        List<CRAMIOWDtlMsg> iowlst = new List<CRAMIOWDtlMsg>();
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
                    Pnlgv.Visible = false;
                    pnlIOW.Visible = false;
                    PnlContext.Visible = true;
                    pnlIOWItem.Visible = true;
                    txtIOWHeadCode.Text = txtCRAMIOWHeadCode.Text;
                    txtIOWHeadName.Text = txtCRAMIOWHeadName.Text;
                    LoadddlItem();
                    IOWItemFilter();
                    chkItemChanged();
                    pnlIOWItem.Visible = false;
                    //check for already created IOW Item
                    List<IOWItemMsg> itlst = new List<IOWItemMsg>();
                    IOWItemMsg iim = new IOWItemMsg();
                    iim.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                    iim.IOWCode = WIOWCode;
                    iim.Flag = "R";
                    itlst.Add(iim);
                    IowItem = Bus.MasCRAMIOWItemInsertUpdate(itlst, BaseMsg.EmployeeCode);
                    btnAdd.Enabled = true;
                    if (IowItem.Count > 0)
                    {
                        grdIOWItem.DataSource = IowItem;
                        grdIOWItem.DataBind();
                        pnlIOWItem.Visible = true;
                        pnlIOWItem.Enabled = false; //if alreay available do not alter and just allow to see.
                        btnAdd.Enabled = false;
                    }
                    // Check over
                   
                    break;
                }
            }
        }
    }
    
    protected void GrdIOWItemSel_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string WCommandName = e.CommandName.ToString();
        string WIOWCode = e.CommandArgument.ToString();
        txtIOWItemCode.Text = WIOWCode;
        foreach (GridViewRow gvr in grdIOWItemSel.Rows)
        {
            if (((Label)gvr.FindControl("lblIOWCode")).Text == WIOWCode.ToString())
            {
                txtCRAMIOWHeadDtlId.Text = ((Label)gvr.FindControl("lblCRAMIOWHeadDtlId")).Text;
                txtItemUOM.Text = ((Label)gvr.FindControl("lblIOWUOM")).Text;
                txtIOWItemDescription.Text = ((Label)gvr.FindControl("lblIOWDescription")).Text;
                txtIOWItemCode.Text = ((Label)gvr.FindControl("lblIOWCode")).Text;
                if (txtIOWItemDescription.Text.Length > 160)
                {
                    txtIOWItemDescription.Text = txtIOWItemDescription.Text.Substring(0, 159);
                }
                break;
            }
        }
    }
    protected void grdIOWItem_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string WCommandName = e.CommandName.ToString();
        string WContextSrlNo = e.CommandArgument.ToString();
        txtGridItemId.Text = ""; // initialise txtGridItemId selected
        if (WCommandName == "IOWItemDel")
        {
            foreach (GridViewRow row in grdIOWItem.Rows)
            {
                if (((Label)row.FindControl("lblContextSrlNo")).Text == WContextSrlNo)
                {
                    List<IOWItemMsg> itlst = IowItemgrid();
                    itlst = (from it in itlst where it.ContextSrlNo != WContextSrlNo select it).ToList();
                    grdIOWItem.DataSource = null;
                    grdIOWItem.DataSource = itlst;
                    grdIOWItem.DataBind();
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
    private List<CRAMIOWDtlMsg> LoadGridIOWDtl(Int32 CompanyId, string PreviousIOWLevel )//To load data into grid.
    {
        CRAMIOWDtlMsg iow = new CRAMIOWDtlMsg();
        iow.CompanyId = CompanyId;
        iow.PreviousIOWLevel = PreviousIOWLevel; //txtCRAMIOWHeadCode.Text;
        iow.Flag = "R";
        IOWdtlList = Bus.MasCRAMIOWDtlInsertUpdate(iow);
       
        return IOWdtlList;
    }
    private void LoadGridIOwDtlDisp(List<CRAMIOWDtlMsg> DtlList)
    {
        grdIOWdtl.DataSource = DtlList;
        grdIOWdtl.DataBind();
        pnlIOW.Visible = true;
    }
    private List<IOWItemMsg> IowItemgrid()
    {
        List<IOWItemMsg> itlst = new List<IOWItemMsg>();
        if (grdIOWItem.Rows.Count > 0)
        {
            foreach (GridViewRow gvr in grdIOWItem.Rows)
            {
                IOWItemMsg itmsg = new IOWItemMsg();
                itmsg.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                itmsg.Context = (((Label)gvr.FindControl("lblContext")).Text);
                if (itmsg.Context.Trim().Length > 160)
                {
                    itmsg.ContextDisplay = itmsg.Context.Trim().Substring(0, 159);
                }
                else
                {
                    itmsg.ContextDisplay = itmsg.Context.Trim();
                }
                itmsg.ContextSrlNo = (((Label)gvr.FindControl("lblContextSrlNo")).Text);
                itmsg.CRAMIOWCode = (((Label)gvr.FindControl("lblCRAMIOWcode")).Text);
                itmsg.CRAMIOWName = (((Label)gvr.FindControl("lblCRAMIOWName")).Text);
                itmsg.IOWCode = "";
                itmsg.IOWName = "";
                itmsg.IOWHeadDtlId = Convert.ToInt64(((Label)gvr.FindControl("lblIOWHeadDtlId")).Text);
                itmsg.IOWCode = (((Label)gvr.FindControl("lblIOWItemCode")).Text); // just for display but for db IOWHeadDtlId is used

                itmsg.IsImported = (((CheckBox)gvr.FindControl("chkIsImported")).Checked);  //chkIsImported.Checked;
                itmsg.ItemCode = (((Label)gvr.FindControl("lblItemCode")).Text);
                itmsg.ItemId = Convert.ToInt64(((Label)gvr.FindControl("lblItemId")).Text);
                itmsg.IOWHeadDtlId = Convert.ToInt64(((Label)gvr.FindControl("lblIOWHeadDtlId")).Text);
                itmsg.ItemName = (((Label)gvr.FindControl("lblItemName")).Text);
                itmsg.ItemQuantity = Convert.ToDouble(((Label)gvr.FindControl("lblItemQty")).Text);
                itmsg.ItemUOM = (((Label)gvr.FindControl("lblItemUOM")).Text);
                itmsg.Wastage = Convert.ToDouble(((Label)gvr.FindControl("lblWastage")).Text);
                itlst.Add(itmsg);
            }
        }
        return itlst;
    }
    private List<IOWItemMsg> iowitemAdd()
    {
        List<IOWItemMsg> itlst = new List<IOWItemMsg>();
        IOWItemMsg itmsg = new IOWItemMsg();
        itmsg.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
        itmsg.Context = txtContext.Text;
        if (txtContext.Text.Trim().Length > 160)
        {
            itmsg.ContextDisplay = txtContext.Text.Trim().Substring(0, 159);
        }
        else
        {
            itmsg.ContextDisplay = txtContext.Text.Trim();
        }
        itmsg.ContextSrlNo = txtContextSrlNo.Text;
        itmsg.CRAMIOWCode = txtIOWCode.Text;
        itmsg.CRAMIOWName = txtIOWDescription.Text;
       

        itmsg.IsImported = chkIsImported.Checked;
        itmsg.ItemCode = txtItemCode.Text;
        if (ddlItem.SelectedIndex > 0)
        {
            itmsg.ItemId = Convert.ToInt64(ddlItem.SelectedValue);
            itmsg.ItemName = ddlItem.SelectedItem.ToString();
            itmsg.IOWHeadDtlId = 0;
            itmsg.IOWCode = ""; // used for displaying IOW Item code in grid
            itmsg.ItemCode = txtItemCode.Text;
        }
        else
        {
            itmsg.ItemId = 0;
            itmsg.ItemName = txtIOWItemDescription.Text; //descrion of IOW Item is displayed in item name column
            itmsg.IOWHeadDtlId = Convert.ToInt64(txtCRAMIOWHeadDtlId.Text);
            itmsg.IOWCode = txtIOWItemCode.Text;
            itmsg.ItemCode = "";
        }
        
        itmsg.ItemQuantity = Convert.ToDouble(txtItemQty.Text);
        itmsg.ItemUOM = txtItemUOM.Text;
        itmsg.Wastage = Convert.ToDouble(txtWastage.Text);
        itlst.Add(itmsg);

        return itlst;
    }
    private void IOWItemFilter()
    {
        List<CRAMIOWDtlMsg> iowlst = new List<CRAMIOWDtlMsg>();
        iowlst = LoadGridIOWDtl(Convert.ToInt32(ddlCompany.SelectedValue), "ALL");

        if (txtIowItemFilter.Text.Trim().Length > 0)
        {
            iowlst = (from iow in iowlst where iow.IOWDescription.ToUpper().Contains(txtIowItemFilter.Text.ToUpper()) select iow).ToList();
        }
        else
        {
        }
        grdIOWItemSel.DataSource = iowlst;
        grdIOWItemSel.DataBind();
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
            //List<ItemMsg> ITList = Bus.MasItemMasterInsertUpdateandDelete(Cmp);
            ITList = Bus.MasItemMasterInsertUpdateandDelete(Cmp);
            ddlLoadItem(ITList);
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
    private void chkItemChanged()
    {
        //pnlIOWSel.Visible = true;
        Panel2.Visible = true;
        if (chkIsItem.Checked == true)
        {
            ddlItem.Enabled = true;
            Panel1.Visible = false;
        }
        else
        {
            ddlItem.Enabled = false;         
            Panel1.Visible = true;
        }
    }

    #endregion
    #endregion
    #region Validation
    private int IsValidAdd()
    {
        int Error = 0;
        string DisplayError = "";
        if (txtContext.Text.Trim().Length < 4 )
        {
            DisplayError = DisplayError + "--" + " Context Is Mandatory and with minimum four characters .... ";
            Error = 1;
        }
        
            try
            {
                Int32 x = Convert.ToInt32(txtContextSrlNo.Text);
                if (x<=0)
                {
                    DisplayError = DisplayError + "--" + " Serial Number is Mandatory and be integer .... ";
                    Error = 1;
                }
            }
            catch
            {
                DisplayError = DisplayError + "--" + " Serial Number is Mandatory and be integer .... ";
                Error = 1;
            }

            if (ddlItem.SelectedIndex == 0 && txtIOWItemCode.Text.Trim().Length == 0)
            {
                txtItemQty.Text = "0";
            }
            else
            {
                try
                {
                    Double Qty = Convert.ToDouble(txtItemQty.Text);
                    if (Qty == 0)
                    {
                        DisplayError = DisplayError + "--" + " Qty should be numeric and Not zero .... ";
                        Error = 1;
                    }
                }
                catch
                {
                    DisplayError = DisplayError + "--" + " Qty should be numeric when you select an Item or IOW .... ";
                    Error = 1;
                }
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
        //int DupItemKount = 0; 
        int WSrlkount = 0;
        foreach (GridViewRow gvr in grdIOWItem.Rows)
        {
            //if (((Label)gvr.FindControl("lblItemId")).Text == (ddlItem.SelectedValue))
            //{
            //    DupItemKount = DupItemKount + 1;
            //}
            if (((Label)gvr.FindControl("lblContextSrlNo")).Text == txtContextSrlNo.Text.Trim())
            {
                WSrlkount = WSrlkount + 1;
            }
        }
        //if (DupItemKount > 0)
        //{
        //    DisplayError = DisplayError + "--" + " Item Getting Added is Already Available in the Grid..Pls Select Some other Item  . ";
        //    Error = 1;
        //}
        if (WSrlkount > 0)
        {
            DisplayError = DisplayError + "--" + " Serial Number Duplicated with Context  . ";
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
    private void ClearItemAddtxt()
    {
        txtContext.Text = "";
        txtContextSrlNo.Text = "";
        if (ddlItem.Items.Count > 0)
        {
            ddlItem.SelectedIndex = 0;
        }
        txtItemQty.Text = "";
        txtItemUOM.Text = "";
        txtIOWItemCode.Text = "";
        txtCRAMIOWHeadDtlId.Text = "0";
        txtGridItemId.Text = "0";
        txtIOWItemDescription.Text = "";
       
    }
    private void ClearText()
    {
        txtIOWCode.Text = "";
        txtIOWDescription.Text = "";
        txtItemUOM.Text = "";
        txtItemQty.Text = "";
        txtWastage.Text = "";
        txtIOWHeadCode.Text = "";
        txtIOWHeadName.Text = "";
        txtContext.Text = "";
        txtContextSrlNo.Text = "";     
        chkIsImported.Checked = false;
        //ddlItem.SelectedIndex = 0;
    }
    public void AllClear()
    {
        ClearText();
        ClearItemAddtxt();
        Pnlgv.Visible = false;
       // pnlAdd.Visible = true;
       // LoadGridIOWHead(WCompanyId); //SCS 210301
        pnlPendind.Enabled = true;
        ddlCompany.Enabled = true;
        pnlIOW.Visible = false;
        PnlContext.Visible = false;
        pnlIOWItem.Visible = false;
        Panel1.Visible = false;
        grdIOWItem.DataSource = null;
        grdIOWItem.DataBind();
       // pnlIOWAdd.Visible = false;
    }
    #endregion
      
    }