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

public partial class TenderIOWMapping : System.Web.UI.Page
{
    #region Declaration
    ProcessBus Bus = new ProcessBus(); LibFunctions Lib = new LibFunctions();
    List<EmployeeMasterMsg> EmpList = new List<EmployeeMasterMsg>();
    List<CRAMIOWCodeMsg> IOWList = new List<CRAMIOWCodeMsg>();
    List<IOWItemMsg> IOWItList = new List<IOWItemMsg>();
   // List<StateMasterMsg> StateList = new List<StateMasterMsg>();
    private static List<CompanyMessage> CmpList = new List<CompanyMessage>();
    private static List<ClientMsg> ClientList = new List<ClientMsg>();
    private static List<ProjectMsg> projList = new List<ProjectMsg>();
    //List<IOWTypeMasterMsg> IOWTypeList = new List<IOWTypeMasterMsg>();
    public static List<PackageMasterMsg> pklst = new List<PackageMasterMsg>();
    public static List<TradeMasterMsg> Trlst = new List<TradeMasterMsg>();
    public static List<ElementMasterMsg> elelst = new List<ElementMasterMsg>();
    public static List<ElementMasterMsg> mklst = new List<ElementMasterMsg>();
    private static List<ItemMsg> ITList = new List<ItemMsg>();
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
            chkQtyOnly.Checked = false;
            txtFilter.Text = ""; //txtTenderRowId.Text = "0";//scs251223 tenderquerydoc3 get the last clicked checkbox to the front
            PnlIOWdtl.Visible = false;
            btnIOWSave.Enabled = false;
            List<CRAMIOWCodeMsg> dummy = new List<CRAMIOWCodeMsg>();
            grdIOWSelected.DataSource = dummy;
            grdIOWSelected.DataBind();
            pnlPendind.Enabled = true;
            LoadCompany();

        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        AllClear();
        Pnlgv.Visible = false;
        ddlCompany.SelectedIndex = 0;
    }
    protected void btnIOWSave_Click(object sender, EventArgs e)
    {
        if (IsValidSave() == 0)
        {
            List<CRAMIOWCodeMsg> Seliowlst = new List<CRAMIOWCodeMsg>();
            Seliowlst = SelectedIOWGrodToList();
            string Result = Bus.TenderCRAMIOWMapInsert(Seliowlst, BaseMsg.EmployeeCode);
            if (Result.Substring(0, 1) == "0")
            {
                ClearforLoadEvent();
               ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Tender Item Mapped with IOW successfully.." + "');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + Result.Substring(1,Result.Length-1) + "');", true);
            }
            
        }
    }
    private void ClearforLoadEvent()
    {
        btnGoClicked();
    }
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        LoadGridTender();
    }
    protected void ddlCompanyChanged(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedIndex > 0)
        {
            WCompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
           //AllClear();
            LoadClient(WCompanyId);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Location Not Selected" + "');", true);
            Pnlgv.Visible = false;
           // pnlAdd.Visible = false;
        }
    }
    protected void ddlClientChanged(object sender, EventArgs e)
    {
        if (ddlClient.SelectedIndex > 0 && ddlCompany.SelectedIndex > 0)
        {
            Int32 WCompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
            string WClientCode = ddlClient.SelectedValue;
            LoadProject(WCompanyId, WClientCode);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Company and Client are to be Selected" + "');", true);
        }
    }
    protected void ddlGroupChanged(object sender, EventArgs e)
    {
         Int32 WCompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
         LoadIOWSubGroup(WCompanyId);
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        if (IsValidGo() == 0)
        {
            btnGoClicked();
            txtFilter.Text = "";

        }
       
    }
     protected void btnSelect_Click(object sender, EventArgs e)
    {
        if (ValidSelect() == 0)
        {
            LoadIOWCodes();
           // PnlTender.Visible = false;
            btnIOWSave.Enabled = true;
            btnShowTender.Enabled = true;
            pnlPendind.Enabled = false;
            grdIOWSelected.Visible = true; // scs 260105 do not show once filter btn is clicked
            //lblTenderDesc.Text = "";
            //lblTenderSrlNo.Text = "";
        }
    }
    protected void btnShowTender_Click(object sender, EventArgs e)
    {
       // txtTenderRowId.Text = "0"; //scs251224 when btn tender is clicked reset the tenderid
        PnlTender.Visible = true;
        btnShowTender.Enabled = true;
        //lblTenderDesc.Text = "";
        //lblTenderSrlNo.Text = "";
        btnGoClicked();
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Pnlgv.Visible = true;
        PnlIOWdtl.Visible = false;
    }
    //protected void btnGoToSelectRow_Click(object sender, EventArgs e)
    //{
    //    Int32 rowNumber = Convert.ToInt32(txtTenderRowId.Text);
    //    if (rowNumber >0)
    //    {
    //        foreach (GridViewRow row in GrdTender.Rows)
    //        {
    //            // Assuming the target column is the first cell (index 0)
    //            // Adjust the cell index to match your target column
    //            if (row.Cells[3].Text.Trim() == rowNumber.ToString())
    //            {
    //                // Store the unique row ID in the hidden field
    //                hdnTargetRowId.Value = row.ID;
    //                break; // Stop searching once found
    //            }
    //        }

    //        // Register client-side script to execute after postback
    //        if (!string.IsNullOrEmpty(hdnTargetRowId.Value))
    //        {
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "ScrollScript", "scrollToTargetRow();", true);
    //        }
    //    }
    //}
    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    { //scs 251224 for showing the selected checkbox
        // Reference the CheckBox that raised the event
        int WTenderRowId = 0;
        CheckBox chk = (CheckBox)sender;
        foreach (GridViewRow gvr in GrdTender.Rows)
        {           
           // txtTenderRowId.Text = WTenderRowId.ToString();
           // hdnTargetRowId.Value = txtTenderRowId.Text;
            if (((CheckBox)gvr.FindControl("chkSelectBox")).Checked == true)
            {
                txtClientProjectTenderId.Text = (((Label)gvr.FindControl("lblClientTenderId")).Text);
                WTenderRowId = Convert.ToInt32(((Label)gvr.FindControl("lblExcelRowNo")).Text);
                txtFilter.Text = WTenderRowId.ToString();
                break;
            }
        }
        LoadSelectedTenderToList();
        btnShowTender.Enabled = true;
        // Get the GridViewRow containing the CheckBox using NamingContainer
        //GridViewRow row = (GridViewRow)chk.NamingContainer;

        //// Retrieve the row index
        //int rowIndex = row.RowIndex;
        //txtTenderRowId.Text = (rowIndex + 2).ToString();
    }

    #region GridEditing
    protected void GridTender_Databound(object sender, GridViewRowEventArgs e)
    {
        //Checking the RowType of the Row  
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string col1 = ((Label)e.Row.FindControl("lblQuantity")).Text;
            string col2 = ((Label)e.Row.FindControl("lblDescription")).Text;
            string TMapped = ((Label)e.Row.FindControl("lblTenderIOWMapped")).Text;
            int WTMapped = Convert.ToInt16(TMapped); // when 0 Tender Not Mapped else it is mapped.
            if (col1 == "0" && col2.Length > 0 && WTMapped==0) // scs 251223 tenderquerydoc3 to show mapped item with qty 0 in green added WTMapped = 0
            {
                e.Row.BackColor = System.Drawing.Color.LightBlue; // not mapped show it in Magenta
            }
            else if (col1 == "0" && col2 == "")  //(col1 == "0" || col2 == "")
            {
                e.Row.BackColor = System.Drawing.Color.White;
                e.Row.Enabled = false;
            }
            else
            {
                if (WTMapped == 0) 
                {
                    e.Row.BackColor = System.Drawing.Color.LightBlue; // not mapped show it in Magenta
                }
                else if (WTMapped == 1 && col1 == "0" && col2.Length > 0)
                {
                    e.Row.BackColor = System.Drawing.Color.LightGreen; // when mapped for quantity 0 show it in green tender query doc 3 from hills point 3
                }
                else
                {
                    e.Row.BackColor = System.Drawing.Color.LightGreen; // when mapped show it in green
                }
                e.Row.Enabled = true;
            }
        }
    }
    protected void GrdIOW_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Int32 WCompanyid = Convert.ToInt32(ddlCompany.SelectedValue);
        string WCommandName = e.CommandName.ToString();
        string WIOWCode = e.CommandArgument.ToString();
        List<CRAMIOWCodeMsg> IOWHdList = new List<CRAMIOWCodeMsg>();
        if (WCommandName == "IOWCode")
        {
            IOWHdList = Bus.IOWHeadSelect(WCompanyid, WIOWCode);
            if (IOWHdList.Count > 0)
            {
                GrdIOWDtl.DataSource = IOWHdList;
                GrdIOWDtl.DataBind();
                PnlIOWdtl.Visible = true;
                Pnlgv.Visible = false; //scs250708 to disable and show the iow details
            }
            else
            {
               //no work
            }
        }
        else if (WCommandName == "IOWItemSel")
        {
            LoadSelectedIOW(WIOWCode);
            grdIow.Enabled = false; //Hills wanted only one row to be selected. meeting on 240730
        }
        else
        {
        }

       

    }
    protected void GrdIOWSelected_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string WCommandName = e.CommandName.ToString();
        string WIOWCode = e.CommandArgument.ToString();
        List<CRAMIOWCodeMsg> IOWHdList = new List<CRAMIOWCodeMsg>();
        if (WCommandName == "Drop")
        {
            DropSelectedIOW(WIOWCode);
            grdIow.Enabled = true; //Hills wanted only one row to be selected. meeting on 240730
        }
        else
        {
        }



    }
    protected void SelectRowByNumber(int rowNumber)
    {
        // Row numbers are typically 0-indexed in C# collections, 
        // so subtract 1 if the input is 1-based (e.g., from a UI display)
        int rowIndex = rowNumber; 

        if (rowIndex >= 0 && rowIndex < GrdTender.Rows.Count)
        {
            GridViewRow row = GrdTender.Rows[rowIndex];

            // You can now access cells or controls within that row
            // Example: Get text from a Label control inside a TemplateField
            //Label myLabel = (Label)row.FindControl("lblRowNumber");
            //if (myLabel != null)
            //{
            //    string rowText = myLabel.Text;
            //    // Perform actions with the row data
            //    Console.WriteLine($"Accessed data in row {rowNumber}: {rowText}");
            //}

            // To visually highlight the row as "selected"
            GrdTender.SelectedIndex = rowIndex;
        }
    }

    //
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
                //ddlCompany.Enabled = false;
                WCompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                LoadClient(WCompanyId);
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
    private void LoadClient(Int32 WCompanyId)
    {
        ClientMsg Cmp = new ClientMsg();
        Cmp.Flag = "R";
        Cmp.CreatedBy = BaseMsg.EmployeeCode;
        Cmp.CompanyId = WCompanyId;
        ClientList = Bus.MasClientInsertUpdateandDelete(Cmp);
        ClientList = (from Cl in ClientList where Cl.ClientType.Trim().ToUpper() == "CLIENT" select Cl).ToList();
        ddlClient.DataSource = ClientList;
        ddlClient.DataBind();
        ddlClient.Items.Insert(0, "-- Select Please --");
        ddlClient.Enabled = true;
        if (ClientList.Count > 1)
        {
            //just Leave
        }
        else if (ClientList.Count == 1)
        {
            ddlClient.SelectedIndex = 1;
            string WClientCode = ddlClient.SelectedValue;
            LoadProject(WCompanyId, WClientCode);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Pls Create Clients for the selected Company" + "');", true);
        }
        
    }
    private void LoadIOWGroup(Int32 WCompanyId)
    {
            List<CRAMIOWGroupMsg> grplist = new List<CRAMIOWGroupMsg>();
            grplist = Bus.MasCRAMGroupSelect(WCompanyId);

            ddlGroup.DataSource = grplist;
            ddlGroup.DataBind();
            ddlGroup.Items.Insert(0, "ALL");
            ddlGroup.Enabled = true;
            if (grplist.Count > 1)
            {
                //just Leave
            }
            else if (grplist.Count == 1)
            {
                ddlGroup.SelectedIndex = 1;
                LoadIOWSubGroup(WCompanyId);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Pls Create Group for the selected Company" + "');", true);
            }
       

    }
    private void LoadIOWSubGroup(Int32 WCompanyId)
    {
        List<CRAMIOWSubGroupMsg> Sgrplist = new List<CRAMIOWSubGroupMsg>();
        Sgrplist = Bus.MasCRAMSubGroupSelect(WCompanyId);
        if (ddlGroup.SelectedIndex > 0)
        {
            Sgrplist = (from sgrp in Sgrplist where sgrp.GroupCode == ddlGroup.SelectedValue select sgrp).ToList();
        }
        
        ddlSubGroup.DataSource = Sgrplist;
        ddlSubGroup.DataBind();
        ddlSubGroup.Items.Insert(0, "ALL");
        ddlSubGroup.Enabled = true;
        if (Sgrplist.Count > 1)
        {
            //just Leave
        }
        else if (Sgrplist.Count == 1)
        {
            ddlSubGroup.SelectedIndex = 1;
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Pls Create SubGroup for the selected Company and group" + "');", true);
        }


    }
    private void btnGoClicked()
    {
        
        chkQtyOnly.Checked = false;
        txtIOWFilter.Text = "";
        LoadGridTender();

        LoadIOWGroup(WCompanyId);
        LoadIOWSubGroup(WCompanyId);
        chkQtyOnly.Checked = false;
        //txtFilter.Text = "";
        btnShowTender.Enabled = false;
        lblTenderDesc.Text = "";
        lblTenderSrlNo.Text = "";
        grdIow.DataSource = null;
        grdIow.DataBind();
        grdIOWSelected.DataSource = null;
        grdIOWSelected.DataBind();
        //if (txtTenderRowId.Text != "0")
        //{
        //    btnGoToSelectRow.Enabled = true;
        //}
    }

    private void LoadIOWCodes()
    {
        Int32 WCompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
        Int64 WClientProjectTenderId = 0;
        string WGroupCode = "";
        if (ddlGroup.SelectedIndex == 0)
        {
            WGroupCode = "0";
        }
        else
        {
           WGroupCode = ddlGroup.SelectedValue;
        }
        string WSubGroupCode = "";
        if (ddlSubGroup.SelectedIndex == 0)
        {
            WSubGroupCode = "0";
        }
        else
        {
            WSubGroupCode = ddlSubGroup.SelectedValue;
        }

        foreach (GridViewRow gvr in GrdTender.Rows) //scs 251224 commenting below as it is moved to on chkSelect_CheckedChanged event
        {

            if (((CheckBox)gvr.FindControl("chkSelectBox")).Checked == true)
            {
                WClientProjectTenderId = Convert.ToInt64(((Label)gvr.FindControl("lblClientTenderId")).Text);
                txtClientProjectTenderId.Text = WClientProjectTenderId.ToString();
                break;
            }

        }
        LoadSelectedTenderToList();
        if (txtIOWFilter.Text == "")
        {
            txtIOWFilter.Text = " ";
        }
        else
        {
        }
        List<CRAMIOWCodeMsg> IowList = new List<CRAMIOWCodeMsg>();
        if (grdIOWSelected.Rows.Count == 0)
        { // when there is no data in grdselected IOW populate from database
            List<CRAMIOWCodeMsg> MappedIowlst = new List<CRAMIOWCodeMsg>();
            IowList = Bus.IOWCodeForTenderMappingSelect(WCompanyId, WClientProjectTenderId, "0", "0", " "); // get All codes so that already ticked is known
            MappedIowlst = (from iow in IowList where iow.TenderIOWMapped == 1 select iow).ToList();
            if (MappedIowlst.Count > 0)
            {
                grdIOWSelected.DataSource = MappedIowlst;
                grdIOWSelected.DataBind();
                grdIow.Enabled = false; // One Tender Item mapped to one IOW Item only meeting on 240730
            }
            else
            {
                grdIow.Enabled = true;
            }
        }
        if (txtIOWFilter.Text !=" ") //scs250707 only when filter has data it should fetch
        {
            IowList = Bus.IOWCodeForTenderMappingSelect(WCompanyId, WClientProjectTenderId, WGroupCode, WSubGroupCode, txtIOWFilter.Text.Trim());
        }
       
        if (IowList.Count > 0)
        {
            grdIow.DataSource = IowList;
            grdIow.DataBind();
            grdIow.Visible = true;
            GrdIOWCheckBoxTick();
            PnlIOWdtl.Visible = false; //scs250707 set visible otherwise it is seen always
            //
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "IOW Code not found for the selected Company, Group , subgroup and filter" + "');", true);
            grdIow.Visible = false;
        }
    }


    protected void SelectAndHighlightRow(int targetRowId)
    {
        // Clear previous selection (optional but good practice)
        GrdTender.SelectedIndex = -1;

        foreach (GridViewRow row in GrdTender.Rows)
        {
            int currentRowId = Convert.ToInt32(GrdTender.DataKeys[row.RowIndex].Value);

            if (currentRowId == targetRowId)
            {
                // Set the row as selected programmatically
                GrdTender.SelectedIndex = row.RowIndex;

                // Optional: Change background color for visibility if not using SelectedRowStyle
                //row.BackColor = System.Drawing.ColorTranslator.FromHtml("#A1DCF2");
                CheckBox myTextBox = (CheckBox)row.FindControl("chkSelectBox");
                myTextBox.Focus();
                break; // Exit the loop once the row is found
            }
        }
        // Rebind the GridView if necessary after setting properties
        // GridView1.DataBind(); 
    }

    private void LoadSelectedTenderToList()
    {
        List<TenderMsg> WTendList = new List<TenderMsg>();
        foreach (GridViewRow gvr in GrdTender.Rows)
        {
            if (((CheckBox)gvr.FindControl("chkSelectBox")).Checked == true)
            {
                txtClientProjectTenderId.Text = (((Label)gvr.FindControl("lblClientTenderId")).Text);

                TenderMsg tmsg = new TenderMsg();
                tmsg.CompanyId = Convert.ToInt32(((Label)gvr.FindControl("lblCompanyId")).Text);
                tmsg.ClientProjectTenderId = Convert.ToInt32(((Label)gvr.FindControl("lblClientTenderId")).Text);//
                tmsg.TenderIOWMapped = Convert.ToInt32(((Label)gvr.FindControl("lblTenderIOWMapped")).Text);

                tmsg.ExcelSheetName = (((Label)gvr.FindControl("lblExcelSheetName")).Text);
                tmsg.ExcelRowNumber = Convert.ToInt32(((Label)gvr.FindControl("lblExcelRowNo")).Text);
                tmsg.SrlNo = (((Label)gvr.FindControl("lblSrlNo")).Text);
                tmsg.Description = (((Label)gvr.FindControl("lblDescription")).Text);
                tmsg.UOM = (((Label)gvr.FindControl("lblUOM")).Text);
                //tmsg.Quantity = Convert.ToInt32(((Label)gvr.FindControl("lblQuantity")).Text);
                tmsg.Quantity = Convert.ToDouble(((Label)gvr.FindControl("lblQuantity")).Text);
                WTendList.Add(tmsg);
                //tmsg.chkSelectBox = (((Label)gvr.FindControl("chkSelectBox")).Text);
                break;
            }
        }

        GrdTender.DataSource = WTendList;
        GrdTender.DataBind();
        foreach (GridViewRow gvr in GrdTender.Rows)
        {
            ((CheckBox)gvr.FindControl("chkSelectBox")).Checked = true;
        }

    }
    private void LoadProject(Int32 WCompanyId, string WClientCode)//To load data into grid.
    {
      
        //ProjectMsg Cmp = new ProjectMsg();
        //Cmp.Flag = "R";
        //Cmp.CreatedBy = BaseMsg.EmployeeCode;
        //Cmp.ClientCode = WClientCode;
       // Cmp.CompanyId = WCompanyId;

        projList = Bus.MasClientProjectSelect(WCompanyId);
        var proj = (from pj in projList where pj.ClientCode == WClientCode select new { pj.ClientProjectId, pj.ProjectName }).Distinct().ToList();
        ddlProject.DataSource = proj;
        ddlProject.DataBind();
        ddlProject.Items.Insert(0, "-- Select Please --");

    }
    private List<CRAMIOWCodeMsg> SelectedIOWGrodToList()
    {
        List<CRAMIOWCodeMsg> Seliowlst = new List<CRAMIOWCodeMsg>();
        if (grdIOWSelected.Rows.Count > 0)
        { // When Already selected Item is there prepare the list
            foreach (GridViewRow gvr in grdIOWSelected.Rows)
            {
                CRAMIOWCodeMsg SelIow = new CRAMIOWCodeMsg();
                SelIow.CompanyId = Convert.ToInt32(((Label)gvr.FindControl("lblCompanyId")).Text);

                SelIow.GroupCode = (((Label)gvr.FindControl("lblGroupCode")).Text);
                SelIow.SubGroupCode = (((Label)gvr.FindControl("lblSubGroupCode")).Text);

                SelIow.IOWCode = (((Label)gvr.FindControl("lblIOWCode")).Text);
                SelIow.IOWUOM = (((Label)gvr.FindControl("lblIOWUOM")).Text);
                SelIow.CRAMIOWHeadDtlId = Convert.ToInt64(((Label)gvr.FindControl("lblCRAMIOWHeadDtlId")).Text);
                SelIow.IOWDescription = (((Label)gvr.FindControl("lblIOwName")).Text);
                SelIow.IsTemproryIOW = (((CheckBox)gvr.FindControl("lblTempIOW")).Checked);
                SelIow.TenderMapId = Convert.ToInt64(txtClientProjectTenderId.Text);
                SelIow.ClientProjectId = Convert.ToInt64(ddlProject.SelectedValue);
                Seliowlst.Add(SelIow);
            }
        }
        return Seliowlst;
    }
    private void LoadSelectedIOW(string WIOWCode)
    {
        List<CRAMIOWCodeMsg> Seliowlst = new List<CRAMIOWCodeMsg>();
        Seliowlst = SelectedIOWGrodToList();
        //if (grdIOWSelected.Rows.Count > 0)
        //{ // When Already selected Item is there prepare the list
        //    foreach (GridViewRow gvr in grdIOWSelected.Rows)
        //    {
        //        CRAMIOWCodeMsg SelIow = new CRAMIOWCodeMsg();
        //        SelIow.CompanyId = Convert.ToInt32(((Label)gvr.FindControl("lblCompanyId")).Text);
        //        SelIow.IOWCode = (((Label)gvr.FindControl("lblIOWCode")).Text);
        //        SelIow.CRAMIOWHeadDtlId = Convert.ToInt64(((Label)gvr.FindControl("lblCRAMIOWHeadDtlId")).Text);
        //        SelIow.IOWDescription = (((Label)gvr.FindControl("lblIOwName")).Text);
        //        SelIow.IsTemproryIOW = (((CheckBox)gvr.FindControl("lblTempIOW")).Checked);
        //        Seliowlst.Add(SelIow);
        //    }
        //}
        List<CRAMIOWCodeMsg> dupSeliowlst = new List<CRAMIOWCodeMsg>();
        dupSeliowlst = (from sel in Seliowlst where sel.IOWCode == WIOWCode select sel).ToList();
        if (dupSeliowlst.Count == 0)
        {
            foreach (GridViewRow gvr in grdIow.Rows)
            {
                if ((((Label)gvr.FindControl("lblIOWCode")).Text) == WIOWCode)
                { // add the new selected one below
                    CRAMIOWCodeMsg SelIowNew = new CRAMIOWCodeMsg();
                    ((CheckBox)gvr.FindControl("chkSelectBox")).Checked = true;
                    SelIowNew.CompanyId = Convert.ToInt32(((Label)gvr.FindControl("lblCompanyId")).Text);
                    SelIowNew.IOWCode = (((Label)gvr.FindControl("lblIOWCode")).Text); ;
                    SelIowNew.GroupCode = (((Label)gvr.FindControl("lblGroupCode")).Text);
                    SelIowNew.SubGroupCode = (((Label)gvr.FindControl("lblSubGroupCode")).Text);
                    SelIowNew.CRAMIOWHeadDtlId = Convert.ToInt64(((Label)gvr.FindControl("lblCRAMIOWHeadDtlId")).Text);
                    SelIowNew.IOWDescription = (((Label)gvr.FindControl("lblIOwName")).Text);
                    SelIowNew.IOWUOM = (((Label)gvr.FindControl("lblIOWUOM")).Text);
                    SelIowNew.IsTemproryIOW = (((CheckBox)gvr.FindControl("lblTempIOW")).Checked);
                    SelIowNew.TenderMapId = Convert.ToInt64(txtClientProjectTenderId.Text);
                    SelIowNew.ClientProjectId = Convert.ToInt64(ddlProject.SelectedValue);
                    Seliowlst.Add(SelIowNew);
                    gvr.BackColor = System.Drawing.Color.LightGreen; // when mapped show it in green
                    break;
                }
            }
            grdIOWSelected.DataSource = Seliowlst;
            grdIOWSelected.DataBind();
            GrdIOWBasedOnSelectedIow(); // based on Selected IOW Make the IOW item selected green and checkbox ticked.
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + WIOWCode + "  :IOW Code Already Selected.." + "');", true);
        }
       
    }
    private void DropSelectedIOW(string WIOWCode)
    {
        List<CRAMIOWCodeMsg> Seliowlst = new List<CRAMIOWCodeMsg>();
        foreach (GridViewRow gvr in grdIOWSelected.Rows)
        {
            if ((((Label)gvr.FindControl("lblIOWCode")).Text) == WIOWCode)
            {
                //do not save the grid data as you want to drop it
            }
            else
            {
                CRAMIOWCodeMsg SelIow = new CRAMIOWCodeMsg();
                SelIow.CompanyId = Convert.ToInt32(((Label)gvr.FindControl("lblCompanyId")).Text);
                SelIow.IOWCode = (((Label)gvr.FindControl("lblIOWCode")).Text);
                SelIow.CRAMIOWHeadDtlId = Convert.ToInt64(((Label)gvr.FindControl("lblCRAMIOWHeadDtlId")).Text);
                SelIow.IOWDescription = (((Label)gvr.FindControl("lblIOwName")).Text);
                SelIow.IsTemproryIOW = (((CheckBox)gvr.FindControl("lblTempIOW")).Checked);
                Seliowlst.Add(SelIow);
            }
        }
        grdIOWSelected.DataSource = Seliowlst;
        grdIOWSelected.DataBind();
        foreach (GridViewRow gvr in grdIow.Rows)
        {
            if ((((Label)gvr.FindControl("lblIOWCode")).Text) == WIOWCode)
            {
                ((CheckBox)gvr.FindControl("chkSelectBox")).Checked = false;
                gvr.BackColor = System.Drawing.Color.White;
                break;
            }
        }
    }
    private void LoadGridTender()//To load data into grid.
    {
        List<TenderMsg> TendList = new List<TenderMsg>();
        WCompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
        string WClientCode = ddlClient.SelectedValue;
        Int64 WClientProjectId = Convert.ToInt64(ddlProject.SelectedValue);
        TendList = Bus.MasClientProjectTenderSelect(WCompanyId, WClientCode, WClientProjectId,txtFilter.Text);
        LoadTenderGrd(TendList);
        grdIow.Visible = false; // scs 260105 do not show once filter btn is clicked
        grdIOWSelected.Visible = false; // scs 260105 do not show once filter btn is clicked
    }
    private void LoadTenderGrd(List<TenderMsg> Tend)
    {
        List<TenderMsg> FiltTend = new List<TenderMsg>();

        if (chkQtyOnly.Checked == false)
        {
            FiltTend = Tend.ToList();
        }
        else 
        {
            FiltTend = (from fil in Tend where fil.Quantity != 0 select fil).ToList();
        }
        #region Nomap tender scs 260430
        if (chkNoMap.Checked == true)
        {
            FiltTend = (from fil in Tend where fil.TenderIOWMapped == 0 select fil).ToList();
        }
        #endregion
        if (FiltTend.Count > 0)
        {
            GrdTender.DataSource = FiltTend;
            GrdTender.DataBind();
            Pnlgv.Visible = true;
            PnlTender.Visible = true;
            //if (txtTenderRowId.Text != "0")
            //{
            //    SelectRowByNumber(Convert.ToInt32(txtTenderRowId.Text));
            //}
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Tender Item not found for the selected Company, Project and filter" + "');", true);
            Pnlgv.Visible = false;
            pnlPendind.Enabled = true; //need to enable otherwise need to go to menu scs 260218
            txtFilter.Text = "";
        }
    }

    private void GrdIOWBasedOnSelectedIow()
    {
        foreach(GridViewRow gvr in grdIOWSelected.Rows)
        {
            string WIOwCode = (((Label)gvr.FindControl("lblIOWCode")).Text);
            foreach (GridViewRow gvri in grdIow.Rows)
            {
                if ((((Label)gvri.FindControl("lblIOWCode")).Text) == WIOwCode)
                {
                    ((CheckBox)gvri.FindControl("chkSelectBox")).Checked = true;
                    gvri.BackColor = System.Drawing.Color.LightGreen; // when mapped show it in green
                }
            }
        }
       
    }
    private void GrdIOWCheckBoxTick()
    {
        foreach (GridViewRow gvr in grdIow.Rows)
        {
            string TMapped = ((Label)gvr.FindControl("lblTenderIOWMapped")).Text;
            int WTMapped = Convert.ToInt16(TMapped); // when 0 Tender Not Mapped else it is mapp
            if (WTMapped == 1)
            {
                gvr.BackColor = System.Drawing.Color.LightGreen; // when mapped show it in green
                ((CheckBox)gvr.FindControl("chkSelectBox")).Checked = true;
                string WIOWCode = (((Label)gvr.FindControl("lblIOWCode")).Text);
                LoadSelectedIOW(WIOWCode);
            }
        }
        GrdIOWBasedOnSelectedIow(); //make green background from selected IOW on to the IOW grid
    }
 
 
    #endregion

 

    #region Validation

    private int IsValidGo()
    {
        int Error = 0;
        string DisplayError = "";
        if (ddlCompany.SelectedIndex == 0)
        {
            DisplayError = DisplayError + "--" + " Company Is Mandatory pls Select .... ";
            Error = 1;
        }

        if (ddlClient.SelectedIndex == 0)
        {
            DisplayError = DisplayError + "--" + " Client Is Mandatory pls Select .... ";
            Error = 1;
        }
        if (ddlProject.SelectedIndex == 0)
        {
            DisplayError = DisplayError + "--" + " Project Is Mandatory pls Select .... ";
            Error = 1;
        }
        if (Error == 1)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + DisplayError + "');", true);
        }
        return Error;
    }
    private int ValidSelect()
    {
        Int32 SelCount = 0;
        int Error = 0;
        string DisplayError = "";
        foreach (GridViewRow gvr in GrdTender.Rows)
        {
            if (((CheckBox)gvr.FindControl("chkSelectBox")).Checked == true)
            {
                SelCount = SelCount + 1;
                if (SelCount > 1) // do not allow more than one row to be selected.
                {
                    Error = 1;
                    DisplayError = "  More than One Tender Item is Selected...";
                    break;
                }
                else
                {
                    lblTenderSrlNo.Text = (((Label)gvr.FindControl("lblSrlNo")).Text);
                    lblTenderDesc.Text = (((Label)gvr.FindControl("lblDescription")).Text);
                    string WClientTenderId = (((Label)gvr.FindControl("lblClientTenderId")).Text);
                    if (txtClientProjectTenderId.Text == WClientTenderId)
                    {
                        //txtNewTenderId.Text = "N";
                    }
                    else
                    {
                        //txtNewTenderId.Text = "Y"; // initialise selected IOW grd
                        txtClientProjectTenderId.Text = WClientTenderId;
                        List<CRAMIOWCodeMsg> Seliowlst = new List<CRAMIOWCodeMsg>();
                        grdIOWSelected.DataSource = Seliowlst;
                        grdIOWSelected.DataBind();
                    }
                }
            }
        }
        if (SelCount == 0)
        {
            Error = 1;
            DisplayError = " NO Tender Item Is Selected ...";
        }
        if (Error == 1)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + DisplayError + "');", true);
            lblTenderSrlNo.Text = "";
            lblTenderDesc.Text = "";
        }
        return Error;
    }

    private int IsValidSave()
    {
        int Error = 0;
        string DisplayError = "";
        if (ValidSelect() > 0)
        {
            Error = 1;
            DisplayError = DisplayError + " Only One Tender Item should be selected for Save...";
        }
        
       
        if (grdIOWSelected.Rows.Count == 0)
        {
            Error = 1;
            DisplayError = DisplayError + "  One IOW Code should be selected for Save...";
        }
        else if (grdIOWSelected.Rows.Count > 1)
        {
            Error = 1;
            DisplayError = DisplayError + " One IOW Code only should be selected for Save...";
        }
        else
        {
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
       
      
    }
    #endregion
    #endregion        
    }