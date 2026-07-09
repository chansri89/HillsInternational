using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI; 
using System.Web.UI.WebControls;
using System.Configuration;
using System.IO;
using Ganini.Lib;
using System.Data.OleDb;
using System.Data;
using Resources;
using System.Security.Cryptography;
using System.Text;
using Ganini.Security;
using Microsoft.Reporting.WebForms; //required for creating local report scs 231213



public partial class NewExcelTenderQuoteUploadRev : System.Web.UI.Page
{
    #region Declaration
    #region XL
    List<ExcelUploadMsg> excelUploadMsgList = new List<ExcelUploadMsg>();
    List<ExcelUploadMsg> excelUploadMsgListTotal = new List<ExcelUploadMsg>();
    ExcelUploadMsg exceUploadMsg = new ExcelUploadMsg();
    string XLfilepath = "";
    string filename = "";
    string Validmsg = "";
    private static string SheetName = "";
    string ConnectionString = "";
    string Query = "";
    string Filetype = "";
    int ExFormat = 0;
    string OraFlag = "U";
    int ChkResult = 0;
    public static string UploadError = "";
    private static string TranType = "";
    public static string CreatePermission = "";
    private static string DepotName = "";
    private static string BankName = "";
    #endregion
    #region general
    ProcessBus Bus = new ProcessBus(); LibFunctions Lib = new LibFunctions();
    BaseClass BaseMsg = new BaseClass();
    UserAccess user = new UserAccess();
    public static string ProgramName = string.Empty;
    public static List<ProjectMsg> PMList = new List<ProjectMsg>();
    public static List<ClientMsg> ClList = new List<ClientMsg>();
   
    public static int OperationCount;
    private static List<CompanyMessage> CmpList = new List<CompanyMessage>();
    //int DBError = 0;

    #endregion

    #endregion Declaration
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        ProgramName = System.IO.Path.GetFileName(Request.PhysicalPath);
       
        if (!Page.IsPostBack)
        {
            lblMessage.Text = "";
            lblMessage.Visible = false;
            ddlClient.Enabled = false;
            ddlProject.Enabled = false;
            Panel1.Visible = false;
            LoadCompany();
            //ddlAuctionFormat.SelectedIndex = 1; //hardcode with 1 as of now with Single format
            try
            {
                SheetName = ConfigurationManager.AppSettings["ExcelFileSheetName"].ToString();
                lblPgmHdr.Text = "Tender Quote Upload With Multi Sheet";
            }
            catch
            {
                Response.Redirect("Login.aspx");
            }
            ddlQuote.Visible = false;
            lblQuote.Visible = false;
            ddlQuote.SelectedIndex = 3; // Hardcoded for Tender Quote upload
            pnlSheetName.Visible = false;
        }
        lblMessage.Visible = false;
        btnUpload.Enabled = true;
        DisablePanel();
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        lblMessage.Text = "Upload Click. ";
        lblMessage.Visible = true;
        UploadError = ""; // scs 201008
        UploadInProgress();
    }
    protected void btnDeSelect_Click(object sender, EventArgs e)
    {
        DeSelectAll();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        lblMessage.Text = "Saving from Grid..";
        lblMessage.Visible = true;
        MultiExcelUpload();
    }
    protected void btnYes_Click(object sender, EventArgs e)
    {
        //Panel1.Visible = false;
        //UploadInProgress();
        FlUpdExcel.Enabled = true;
        Panel1.Visible = false;
    }
    protected void btnNo_Click(object sender, EventArgs e)
    {
        Panel1.Visible = false;
        FlUpdExcel.Enabled = true;
        txtNewRevNumber.Text = txtPrevRevisionNumber.Text;
        txtNewRevNumber.Focus();

    }
    protected void RevNoChanged(object sender, EventArgs e)
    {
        RevisionChanged();
    }
    protected void ddlCompanyChanged(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedIndex > 0)
        {
            LoadClient();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Pls Select Company" + "');", true);
        }
    }
    protected void ddlProjectChanged(object sender, EventArgs e)
    {
        pnlSheetName.Visible = false;
        if (ddlCompany.SelectedIndex > 0 && ddlProjClient.SelectedIndex >0)
        {
            LoadRevNo();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Pls Select Company / Client and Project" + "');", true);
        }
    }
    protected void ddlClientChanged(object sender, EventArgs e)
    {
        if (ddlProjClient.SelectedIndex >0  && ddlCompany.SelectedIndex > 0)
        {
            LoadProject();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Pls Select  Company and Client" + "');", true);
        }
    }
 

    #endregion Events
    #region Method
    private void LoadCompany()
    {
      //  CmpList = Bus.CompanyMasterSelect();
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
                LoadClient();
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Company Data not Found" + "');", true);
            // ddlCompany.Items.Insert(0, "--Select Please--");
        }
    }
    private void LoadClient()
    {
        int varcount = 0;
        Int32 CompId = Convert.ToInt32(ddlCompany.SelectedValue);
        //string EmpCode = BaseMsg.EmployeeCode;
        //string uploadFlag = "C";
        //PMList = Bus.MasClientProjectSelect(CompId);
        ClList = Bus.NewClientMasterSelect(CompId);

        var Cli = (from cl in ClList where (cl.ClientType.ToUpper().Contains("TEND")) select new { cl.ClientCode, cl.ClientName }).Distinct().ToList();
        var ProjCli = (from cl in ClList where (cl.ClientType.ToUpper().Contains("CLIEN")) select new { cl.ClientCode, cl.ClientName }).Distinct().ToList();

       
        if (Cli != null)
        {
            ddlClient.DataSource = Cli;
            ddlClient.DataBind();
            ddlClient.Items.Insert(0, "--Select Please--");
            ddlClient.SelectedIndex = 0;
            ddlClient.Enabled = true;
            ddlClient.Focus();
            foreach (var c in Cli)
            {
                varcount = varcount + 1;
            }
            if (varcount == 1)
            {

                ddlClient.SelectedIndex = 1;
                LoadProject();
            }
            pnlSheetName.Visible = false;
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.ErrCenterData + "');", true);
            ddlClient.Items.Insert(0, "--Select Please--");
        }
        // scs260321 mail from Hills 260318 add client ddl
        if (ProjCli != null)
        {
            ddlProjClient.DataSource = ProjCli;
            ddlProjClient.DataBind();
            ddlProjClient.Items.Insert(0, "--Select Please--");
            ddlProjClient.SelectedIndex = 0;
            ddlProjClient.Enabled = true;
            ddlProjClient.Focus();
           
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.ErrCenterData + "');", true);
            ddlProjClient.Items.Insert(0, "ALL");
        }
    }
    private void LoadProject()
    {
        int varcount = 0;
        Int32 CompId = Convert.ToInt32(ddlCompany.SelectedValue);
        //string EmpCode = BaseMsg.EmployeeCode;
        //string uploadFlag = "C";
        PMList = Bus.MasClientProjectSelect(CompId);
        //select Project as per ddlProjclient selection scs260321 mailfrom Hills
        if(ddlProjClient.SelectedIndex > 0)
        {
            PMList = (from cl in PMList where (cl.CompanyId == CompId && cl.ClientCode == ddlProjClient.SelectedValue) select cl).ToList();
        }
        var proj = (from pm in PMList where pm.CompanyId == CompId select new { pm.ProjectCode, pm.ProjectName }).Distinct().ToList();
        //var proj = (from pm in PMList where pm.CompanyId == CompId && pm.ClientCode == ddlClient.SelectedValue select new { pm.ProjectCode, pm.ProjectName }).Distinct().ToList();
        if (proj != null)
        {
            ddlProject.DataSource = proj;
            ddlProject.DataBind();
            ddlProject.Items.Insert(0, "--Select Please--");
            ddlProject.SelectedIndex = 0;
            ddlProject.Enabled = true;
            ddlProject.Focus();
            foreach (var c in proj)
            {
                varcount = varcount + 1;
            }
            if (varcount == 1)
            {
                ddlProject.SelectedIndex = 1;
                btnUpload.Focus();
                LoadRevNo();       
            }
            pnlSheetName.Visible = false;
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Project Data Not found for the Client" + "');", true);
            ddlClient.Items.Insert(0, "--Select Please--");
        }
    }
    private void LoadRevNo()
    {
        foreach (ProjectMsg pm in PMList)
        {
            //if (pm.ProjectCode == ddlProject.SelectedValue && pm.ClientCode == ddlClient.SelectedValue)
            if (pm.ProjectCode == ddlProject.SelectedValue) // here tenderer is the clientcode hence rev number is wrt to project scs 240208
            {
                txtNewRevNumber.Text = pm.RevisionNumber.ToString();
                txtPrevRevisionNumber.Text = pm.RevisionNumber.ToString();
                break;
            }
        }
    }
    private int RevisionChanged()
    {
        pnlSheetName.Visible = false;
        int err = 0; string ErrText = "";
        FlUpdExcel.Enabled = true;
        try
        {
            double NRevno = Convert.ToDouble(txtNewRevNumber.Text);
            double oldRevNo = Convert.ToDouble(txtPrevRevisionNumber.Text);
            if (NRevno > oldRevNo || NRevno <= 0)
            {
                err = 1;
                ErrText = "Cannot Quote for Revision numbers that are greater than current and Less than or equal to 0 ";
            }
            else
            {
                err = 0;
                
            }

        }
        catch
        {
            err = 1;
            ErrText = "Enter Proper Revision Number .. it has to be integer .. ";
        }
        if (err > 0 )
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + ErrText + "');", true);
            lblMessage.Text = ErrText;
            FlUpdExcel.Enabled = false;
        }
        return err;
    }
    private void UploadInProgress()
    {
        if (!user.HasPermission(ProgramName, UserPermission.CanCreate.ToString()))
        // if (CreatePermission !="Y")
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "You Need Permission to Create Data" + "');", true);
        }
        else
        {
            if (ddlCompany.SelectedIndex == 0 || ddlClient.SelectedIndex == 0 || ddlProject.SelectedIndex == 0 || ddlProjClient.SelectedIndex==0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Company / Client/Project/Tenderer are Mandatory .. Ensure they are selected" + "');", true);
            }
            else
            {
                DepotName = ddlClient.SelectedItem.ToString(); // scs 210201 depot selected after the company select
                UploadClick();
            }

        }
    }
    private void DeSelectAll()
    {
        foreach (GridViewRow gvr in grdSheetName.Rows)
        {
            ((CheckBox)gvr.FindControl("chkUpLoadSheet")).Checked = false;
        }
    }
    private void LoadGrdUpLoadNotOK(List<DepotSalesMsg> UploadList)
    {//GrdWtNotOKSubTitle
        List<DepotSalesMsg> upllst = new List<DepotSalesMsg>();
        upllst = (from upl in UploadList where upl.Result.ToUpper() != "OK" select upl).ToList(); //select only not OK data VJ mail 200922
        GrdWtNotOKDeptSales.DataSource = "";
        DisablePanel();

        GrdWtNotOKDeptSales.DataSource = "";
        GrdWtNotOKDeptSales.DataSource = upllst;
        GrdWtNotOKDeptSales.DataBind();
        PnlNotOKDeptSales.Visible = true;
        foreach (GridViewRow gvr in GrdWtNotOKDeptSales.Rows)
        {
            gvr.ForeColor = System.Drawing.Color.Red;
        }
    }
    private void LoadGrdUpLoadStatus(List<DepotSalesMsg> UploadList)
    {//GrdWtNotOKSubTitle

        GrdDepotSalesStatus.DataSource = "";
        DisablePanel();
        GrdDepotSalesStatus.DataSource = UploadList;
        GrdDepotSalesStatus.DataBind();
        PnlDepotSalesStatus.Visible = true;
    }
    private void LoadCheckForRegisteredUsers(DataTable dt)
    {
        DisablePanel();
        //pnlExList.Visible = true;
        //dt.Columns.AddRange(new DataColumn[4] 
        //{   new DataColumn("Result", typeof(string)),  
        //    new DataColumn("WName", typeof(string)),  
        //    new DataColumn("MobileNumber",typeof(string)),
        //    new DataColumn("WDate",typeof(string)) });
        //grdNotRegistered.DataSource = dt;
        //grdNotRegistered.DataBind(); 

    }
    private void DisablePanel()
    {
        PnlDepotSalesStatus.Visible = false;
        PnlNotOKDeptSales.Visible = false;
        pnlNotOKTeaBoard.Visible = false;
        PnlTeaBoardStatus.Visible = false;
        pnlNotOkBankReceipt.Visible = false;
        pnlBankReceiptStatus.Visible = false;
        //pnlExList.Visible = false;
    }
 
    #endregion
    #region Excelupl
    private void UploadClick()
    {
        if (ddlClient.SelectedIndex == 0 || ddlProject.SelectedIndex==0)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Select Client/ Project and File to be uploaded" + "');", true);
        }
        else
        {
            
                filename = Path.GetFileName(FlUpdExcel.FileName);
                lblMessage.Text = "FileName Read"; //scs180310
                string ValidMsg = ValidateUploadedFile();
                if (ValidMsg == "")
                {
                    uploadFile();    //if (lblMessage.Text.Length == 0) //error while uploading

                    if (UploadError.Length > 0) //(lblMessage.Text.Length > 0) //error while uploading scs 180310
                    {
                        // UpLoadType(); // SCS 201008 commented as we upload only excel files and hence below two lines
                        ExFormat = 1;
                        Filetype = "Excel";
                        //XLUpload();
                        LoadExcelSheetGrid();
                    }
                }
                else
                {
                    lblMessage.Text = ValidMsg;
                    lblMessage.Visible = true;
                    pnlSheetName.Visible = false;
                }
            
        }
    }
    public string ValidateUploadedFile()
    {
        Validmsg = "";
        HttpPostedFile UploadFile = FlUpdExcel.PostedFile;
        lblMessage.Text = lblMessage.Text + " Validate File begins. ";
        if (FlUpdExcel.FileName == "")
        {
            Validmsg = Validmsg + " Please select File. ";
        }
        else
        {
            filename = Path.GetFileName(FlUpdExcel.FileName);
           
            if (UploadFile.FileName == null)
            {
                Validmsg = Validmsg + " FileName Not valid and Uploaded. ";
            }
            else if (UploadFile.ContentLength > 0)
            {
                Validmsg = "";
            }
            else
            {
                Validmsg = Validmsg + " Data not found Please check the data. ";
            }
            //
            FileInfo Info = new FileInfo(filename); //scs 230315 added to check only xls or xlsx getting loaded
            string wextn = Info.Extension.ToString();
            if (wextn.ToLower() != ".xls" && wextn.ToLower() != ".xlsx")
            {
                Validmsg = Validmsg + " Only XLS and XLSX files are supported ";
            }
            //
        } // Validate for File extension,data count scs 280613
        return Validmsg;
    }
    public void uploadFile()
    {
        try
        {
            //scs 210416 to keep filename unique added dateand time to file name
            FileInfo Info = new FileInfo(filename);
            string Wtime = System.DateTime.UtcNow.AddMinutes(330).ToString("yyyyMMddHHmmss");
           
            string wextn = Info.Extension.ToString();
            string fname = filename.Substring(0, filename.IndexOf(wextn));
            filename = fname + Wtime+wextn;
         
            // scs 210416 ends
            UploadError = "";
            string filePath = System.IO.Path.GetDirectoryName(FlUpdExcel.PostedFile.FileName);
            XLfilepath = ConfigurationManager.AppSettings["FolderPath"].ToString(); // scs 0703
            lblMessage.Text =  " UpLoad File begins at " + XLfilepath;
            if (File.Exists(@XLfilepath + filename))
            {
                lblMessage.Text = lblMessage.Text + ".... XLFILE with same Name Delete ";
                File.Delete(@XLfilepath + filename);
            }
            lblMessage.Text =  ".. Save New XLFILE";
            FlUpdExcel.SaveAs(XLfilepath + filename);
            UploadError = "OK";
            txtFileName.Text = XLfilepath+filename; //save the file NAme in textbox scs 230912
        }
        catch (Exception ex)
        {
            lblMessage.Text =  " Check if File is being Used or you have Permission " + ex.ToString();
            lblMessage.Visible = true;
        }
    }

    private void LoadExcelSheetGrid()
    {
        List<ExcelSheetNameMsg> Shtlst = GetExcelShtNames(filename);
        grdSheetName.DataSource = Shtlst;
        grdSheetName.DataBind();
        pnlSheetName.Visible = true;
    }
    private List<ExcelSheetNameMsg> GetExcelShtNames(string excelFile)
    {
        OleDbConnection objConn = null;
        System.Data.DataTable dt = null;
        FileInfo Info = new FileInfo(excelFile);
        string WExtension = Info.Extension.ToString(); 

        try
        {
            // Connection String. Change the excel file to the file you       // will search.
            String connString = ""; 
            if (WExtension.ToUpper() == ".XLS")
            {
                connString = "Provider=Microsoft.Jet.OleDb.4.0;Data Source=" + @XLfilepath + filename + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=1'";
            }
            else
            {
                connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + @XLfilepath + @filename + ";Extended Properties='Excel 12.0;HDR=Yes;IMEX=1'"; //HDR =Yes measns data is having column heading
                // connString = "Provider=Microsoft.ACE.OLEDB.16.0;Data Source=" + @XLfilepath + @filename + ";Extended Properties='Excel 16.0;HDR=Yes;IMEX=1'"; //HDR =Yes measns data is having column heading
            }
             objConn = new OleDbConnection(connString);        // Open connection with the database.
            objConn.Open();    // Get the data table containg the schema guid.
            dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

            if (dt == null)
            {
                return null;
            }
            List<ExcelSheetNameMsg> Shtlst = new List<ExcelSheetNameMsg>();

            // String[] excelSheets = new String[dt.Rows.Count];
            // int i = 0;

            // Add the sheet name to the string array.
            foreach (DataRow row in dt.Rows)
            {
                int Windex = row["TABLE_NAME"].ToString().IndexOf("$");
                if (row["TABLE_NAME"].ToString().Contains("'"))
                {
                    Windex = Windex + 2;
                }
                string WSheetName = row["TABLE_NAME"].ToString();
                string DispSheetName = WSheetName.Replace("'", "");
                ExcelSheetNameMsg shtMsg = new ExcelSheetNameMsg();
                if (WSheetName.Length > Windex + 1) //Index starts with 0 hence windex+1
                {
                    ///When Printarea is ther in XL it adds it as Another sheet with larger name
                    //i = i - 1;
                }
                else
                {
                    //excelSheets[i] = WSheetName;
                    shtMsg.ExeclSheetName = WSheetName;
                    shtMsg.DispSheetName = DispSheetName;
                    shtMsg.ExeclFileName = txtFileName.Text;
                    Shtlst.Add(shtMsg);
                }

                //i++;
            }

            // Loop through all of the sheets if you want too...
            //for (int j = 0; j < excelSheets.Length; j++)
            //{
            //    // Query each excel sheet.


            //}

            return Shtlst;
        }
        catch (Exception ex)
        {
            return null;
        }
        finally
        {
            // Clean up.
            if (objConn != null)
            {
                objConn.Close();
                objConn.Dispose();
            }
            if (dt != null)
            {
                dt.Dispose();
            }
        }
    }
    private void MultiExcelUpload()
    {
        int WCount = 0; List<ExcelSheetNameMsg> xlsht = new List<ExcelSheetNameMsg>();
        foreach (GridViewRow gvr in grdSheetName.Rows)
        {
            if (((CheckBox)gvr.FindControl("chkUpLoadSheet")).Checked == true)
            {
                WCount = 1; //Valid Selection
                ExcelSheetNameMsg shmsg = new ExcelSheetNameMsg();
                shmsg.ExeclFileName = ((Label)gvr.FindControl("lblExcelFileName")).Text;
                shmsg.ExeclSheetName = ((Label)gvr.FindControl("lblSheetName")).Text;
                shmsg.DispSheetName = ((Label)gvr.FindControl("lblDispSheetName")).Text;
                shmsg.OnlyAmount = ((CheckBox)gvr.FindControl("chkOnlyAmount")).Checked;
                xlsht.Add(shmsg);
            }
             
        }
        if (WCount == 0)
        {
            lblMessage.Text = "Atleast One Sheet Name needs to be selected..";
        }
        else
        {
            List<ExcelUploadMsg> ExcelUploadMsgListTot = new List<ExcelUploadMsg>();
            ExcelUploadMsgListTot = MultiSheetDataUpload(xlsht);
            CheckandUploadData(ExcelUploadMsgListTot);
        }

    }
    private List<ExcelUploadMsg> MultiSheetDataUpload(List<ExcelSheetNameMsg> XLSht)
    {
        excelUploadMsgListTotal.Clear();
        // Generate the Excel Local Memory temp data
        foreach (ExcelSheetNameMsg sht in XLSht)
        {
            SheetName = sht.ExeclSheetName;     string XlFileName = sht.ExeclFileName;    // Query = "SELECT * FROM [" + SheetName + "$]";
             Query = "SELECT * FROM [" + SheetName + "]"; //$$ is Coming in the sht.ExcelSheetName hence removed here   ////List<ExcelUploadMsg> ExlList = XLUpLoad(Query);
            lblMessage.Visible = true;      lblMessage.Text = " XLUpload Begins for- " + SheetName;       //HttpPostedFile UploadFile = FlUpdExcel.PostedFile;
            FileInfo Info = new FileInfo(sht.ExeclFileName);
            string WExtension = Info.Extension.ToString();
            if (WExtension.ToUpper() == ".XLS")
            {  //ConnectionString = "Provider=Microsoft.Jet.OleDb.4.0;Data Source=" + @XLfilepath + filename + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=1'";
               // ConnectionString = "Provider=Microsoft.Jet.OleDb.4.0;Data Source=" + @XLfilepath + filename + ";Extended Properties='Excel 8.0;HDR=NO;IMEX=1'";
                ConnectionString = "Provider=Microsoft.Jet.OleDb.4.0;Data Source=" + @XlFileName + ";Extended Properties='Excel 8.0;HDR=NO;IMEX=1'";
            }
            else
            {  //ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + @XLfilepath + @filename + ";Extended Properties='Excel 12.0;HDR=Yes;IMEX=1'"; //HDR =Yes measns data is having column heading
               // ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + @XLfilepath + @filename + ";Extended Properties='Excel 12.0;HDR=NO;IMEX=1'"; //HDR =Yes measns data is having column heading
                ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + @XlFileName + ";Extended Properties='Excel 12.0;HDR=NO;IMEX=1'"; //HDR =Yes measns data is having column heading
            }
            OleDbConnection conne = new OleDbConnection(@ConnectionString);
            OleDbCommand command = new OleDbCommand(Query, conne);
            OleDbDataReader dbdr;
            try
            {
                lblMessage.Text = " XLUpload Execute reader to start -" + SheetName; //scs 251215
                OraFlag = "U";
                conne.Open();
                dbdr = command.ExecuteReader(CommandBehavior.CloseConnection);
                DataTable excelData = new DataTable("ExcelData");
                excelData.Load(dbdr); //09122012
                TranType = ddlQuote.SelectedValue.ToString();
                //txtSheetName.Text = sht.ExeclSheetName;
                txtSheetName.Text = sht.DispSheetName;
                if (sht.OnlyAmount == true)
                {
                    txtOnlyAmount.Text = "Y";
                }
                else
                {
                    txtOnlyAmount.Text = "N";
                }
                excelUploadMsgList = TenderDataUpload(excelData, TranType);
                excelUploadMsgListTotal = excelUploadMsgListTotal.Concat(excelUploadMsgList).ToList();
            }
            catch (Exception ex)
            {
                lblMessage.Text = lblMessage.Text + ex.Message.ToString();
                lblMessage.Visible = true;
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + lblMessage.Text + ex.ToString() + "');", true);
            }
            finally
            {
                if (conne != null)
                {
                    ConnectionString = "";
                    command.Dispose();
                    conne.Close();
                }
            }
        }
        return excelUploadMsgListTotal;
    }

    private void XLUpload()
    {
        // scs 201008 Sheet Name is selected in the page load itself
        Query = "SELECT * FROM [" + SheetName + "$]";
        List<ExcelUploadMsg> ExlList = XLUpLoad(Query);
        excelUploadMsgListTotal = null;
        int ErrorCount = 0; //string ErrorMesg = ""; 
        excelUploadMsgListTotal = ExlList.ToList();
        // in Sheet2 store Corporate MDA   //SheetName = ConfigurationManager.AppSettings["ExcelFileSheetName1"].ToString();  //Query = "SELECT * FROM [" + SheetName + "$]";
        //List<ExcelUploadMsg> ExlListCorp = XLUpLoad(Query);  // excelUploadMsgListTotal = excelUploadMsgListTotal.Concat(ExlListCorp).ToList();
        if (excelUploadMsgListTotal.Count > 0)
        {
            ErrorCount = CheckForProperDataInFile();
            if (ErrorCount == 0)
            {
                CheckandUploadData(excelUploadMsgListTotal);
            }
        }
        else
        {
            lblMessage.Text = lblMessage.Text + " Sheet Name not proper or Excel data not proper hence returned with out any Data.. Check data ";
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + lblMessage.Text + "');", true);
        }
    }
    private List<ExcelUploadMsg> XLUpLoad(string Querya)
    {
        lblMessage.Visible = true;
        lblMessage.Text = " XLUpload Begins for- "+SheetName;
        //List<ExcelUploadMsg> excelUploadMsgList = new List<ExcelUploadMsg>(); // scs 190802 moved to declaration
        HttpPostedFile UploadFile = FlUpdExcel.PostedFile;
        System.Globalization.DateTimeFormatInfo dateinfo = new System.Globalization.DateTimeFormatInfo();

        dateinfo.ShortDatePattern = "dd/MM/yyyy";
        DataTable excelData = new DataTable("ExcelData");
       // Querya = "SELECT * FROM [" + ConfigurationManager.AppSettings["ExcelFileSheetName"].ToString() + "$]";
        FileInfo Info = new FileInfo(filename);
        string WExtension = Info.Extension.ToString();
        if (WExtension.ToUpper() == ".XLS")
            // if (ConfigurationManager.AppSettings["ExcelExtension"].ToString() == ".xls")
        {
            //ConnectionString = "Provider=Microsoft.Jet.OleDb.4.0;Data Source=" + @XLfilepath + filename + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=1'";
            ConnectionString = "Provider=Microsoft.Jet.OleDb.4.0;Data Source=" + @XLfilepath + filename + ";Extended Properties='Excel 8.0;HDR=NO;IMEX=1'";
        }
        else
        {
            //ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + @XLfilepath + @filename + ";Extended Properties='Excel 12.0;HDR=Yes;IMEX=1'"; //HDR =Yes measns data is having column heading
            ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + @XLfilepath + @filename + ";Extended Properties='Excel 12.0;HDR=NO;IMEX=1'"; //HDR =Yes measns data is having column heading
        }

            OleDbConnection conne = new OleDbConnection(@ConnectionString);
            OleDbCommand command = new OleDbCommand(Querya, conne);
        OleDbDataReader dbdr;
        #region XLtoMemory
        try
        {
            lblMessage.Text = " XLUpload Execute reader to start -"; //scs 251215
            OraFlag = "U";
            conne.Open();
            dbdr = command.ExecuteReader(CommandBehavior.CloseConnection);
            //int a = dbdr.VisibleFieldCount; //scs to get number of columns in xl
            excelData.Load(dbdr); //09122012
            TranType = ddlQuote.SelectedValue.ToString();
            excelUploadMsgList = TenderDataUpload(excelData, TranType);
        
        }
        catch (Exception ex)
        {
            lblMessage.Text = lblMessage.Text+ ex.Message.ToString();
            lblMessage.Visible = true;
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + lblMessage.Text + ex.ToString() + "');", true);
        }
        finally
        {
            if (conne != null)
            {
                ConnectionString = "";
                command.Dispose();
                conne.Close();
            }
        }
        #endregion
        return excelUploadMsgList; //scs 190802

    }
    private int CheckForProperDataInFile()
    {
        int ErrorKount = 0;
        
        return ErrorKount;
    }
    private void CheckandUploadData(List<ExcelUploadMsg> excelUploadMsgList)
    {
        #region TempInsert
         UploadResultMsg UpMsg = new UploadResultMsg();
         try
         {
              lblMessage.Text = lblMessage.Text + " chk xl data begin-"; //scs 251215
                 ChkResult = 0; // set to OK condition
                  TranType = ddlQuote.SelectedValue;
                  List<DepotSalesMsg> ChkData = CheckData(excelUploadMsgList, TranType);
                     ChkResult = 0; // currently not checking excel data scs 221015
                     if (ChkResult == 0) //Data fine
                     {
                         lblMessage.Text = " Tender PAckage Tempinsert begin-"; //scs 251215
                         UpMsg = Bus.NewExcelUploadTempForTender(excelUploadMsgList, TranType);
                         lblMessage.Text = " Tempinsert Over-"; //scs 251215
                     }
                     else
                     {
                         LoadGrdUpLoadNotOK(ChkData);
                     }
                 
         }
         catch (Exception ex)
         {
             lblMessage.Text = lblMessage.Text + ex.Message.ToString(); //scs 251215
             ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "UpLoad Failed On Temp Table Insert- " + ex.ToString() + "');", true);
         }
         finally
         {
         }

     #endregion
        #region AfterTempInsert
        UploadResultMsg Uploadinsert = new UploadResultMsg();
        List<DepotSalesMsg> UpLoadList = new List<DepotSalesMsg>();
        Int64 WParameterId = UpMsg.WParameterId;
        //try
        //{
            if (ChkResult == 0)
            {
                Filetype = ""; //reinitialise
                if (UpMsg.Result.Substring(0, 1) == "0")
                {
                    lblMessage.Text = "Inserting to Tender Package started";
                    Uploadinsert = Bus.NewExcelTempToTenderInsert(WParameterId);
                    if (Uploadinsert.Result.Substring(0, 1) == "9") //scs 210313 added check for month close
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + Uploadinsert.Result.Substring(1, Uploadinsert.Result.Length - 1) + "');", true);
                        lblMessage.Text = Uploadinsert.Result.Substring(1, Uploadinsert.Result.Length - 1); //scs 251215
                        pnlSheetName.Visible = false;
                    }
                    else
                    {
                        if (Uploadinsert.Result != "0")
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + " NewExcelImportTenderInsert to tbl Failed while inserting Table...   " + Uploadinsert.Result.Substring(1, Uploadinsert.Result.Length - 1) + "');", true);
                            lblMessage.Text = " NewExcelImportTenderpackagesInsert to tbl Failed while inserting Table...   " + Uploadinsert.Result.Substring(1, Uploadinsert.Result.Length - 1); //scs 251215
                            pnlSheetName.Visible = false;
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Succesfully Uploaded" + "');", true);
                            lblMessage.Text = "";
                            pnlSheetName.Visible = false;
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + " NewExcelImportTempInsert Failed while inserting Table" + UpMsg.Result.Substring(1, UpMsg.Result.Length - 1) + "');", true);
                    lblMessage.Text = lblMessage.Text + " - " + UpMsg.Result.Substring(1, UpMsg.Result.Length - 1); //scs 251215
                    pnlSheetName.Visible = false;
                }
            }
 
        #endregion
    }
 
    public List<ExcelUploadMsg> TenderDataUpload(DataTable dtCoup,string TranType)
    {
        System.Globalization.DateTimeFormatInfo dateinfo = new System.Globalization.DateTimeFormatInfo();
        dateinfo.ShortDatePattern = "MM-dd-yyyy";
       // dateinfo.ShortDatePattern = "dd-MM-yyyy";
        lblMessage.Text = TranType+ " UpLoad Method Running";
        List<ExcelUploadMsg> excelUploadList = new List<ExcelUploadMsg>();
        int linecount = 1; // if in excel connection string HDR is Yes the Hdr is taken care hence 1 else 0
        foreach (DataRow dr in dtCoup.Rows)
        {
            string CompId = ddlCompany.SelectedValue;
            string ClientCode = ddlClient.SelectedValue;
            string ProjectCode = ddlProject.SelectedValue;
            string RevNo = txtNewRevNumber.Text;
            string RemovedCharColumn = "";
            string AmountType = "0"; // Assumed default supply & installation amount os quoted as it will have 6 columns only
           // string WDate = ""; string sDate = ""; //string WChar = "";
            int err = 1; // set for error condition reset only if valid data below scs vj 200923
            if (linecount >= 0) // Upload as is else if header is specified then make >=1
            {
                ExcelUploadMsg excelUpload = new ExcelUploadMsg();
                #region TendPack
                        //if ((dr[0].ToString() != string.Empty && dr[2].ToString() != string.Empty && dr[8].ToString() != string.Empty && dr[9].ToString() != string.Empty)) //season,Auctiondate,INvnumber,Dealprice
                        //{
                            excelUpload.Column1 = linecount.ToString(); // xl row number
                            excelUpload.Column2 = (dr[0].ToString() == null || dr[0].ToString() == string.Empty) ? string.Empty : dr[0].ToString(); // SrlNo
                            RemovedCharColumn = RemoveUnWantedCharacter(excelUpload.Column2);
                            excelUpload.Column2 = RemovedCharColumn;
                            excelUpload.Column3 = (dr[1].ToString() == null || dr[1].ToString() == string.Empty) ? string.Empty : dr[1].ToString(); // Descrip
                            RemovedCharColumn = RemoveUnWantedCharacter(excelUpload.Column3);
                            excelUpload.Column3 = RemovedCharColumn;
                            excelUpload.Column4 = (dr[2].ToString() == null || dr[2].ToString() == string.Empty) ? string.Empty : dr[2].ToString(); //Unitofmeasure
                            RemovedCharColumn = RemoveUnWantedCharacter(excelUpload.Column4);
                            excelUpload.Column4 = RemovedCharColumn;
                            excelUpload.Column5 = (dr[3].ToString() == null || dr[3].ToString() == string.Empty) ? string.Empty : dr[3].ToString(); // Qty
                            RemovedCharColumn = RemoveUnWantedCharacter(excelUpload.Column5);
                            excelUpload.Column5 = RemovedCharColumn;
                            ////if (excelUpload.Column5.Length > 0 && txtOnlyAmount.Text == "Y") // Normally it is Qty value but when supply is made Model is captured
                            ////{
                            ////    //int WOutput = 0;
                            ////    bool WIsNumeric = IsNumeric(excelUpload.Column5);// decimal.TryParse(excelUpload.Column5,IsNumeric out WOutput);
                            ////    if (WIsNumeric == false)
                            ////    {
                            ////        AmountType = "1"; //Only Supply --> Rate and value with Model
                            ////    }
                            ////    else
                            ////    {
                            ////        AmountType = "0"; //Only Amount --> Rate and value with Model
                            ////    }
                            ////}
                            ////else if (excelUpload.Column5.Length == 0 && txtOnlyAmount.Text == "Y")
                            ////{
                            ////    bool WIsNumeric = IsNumeric(dr[4].ToString()); // if column5 is blank No UOM but column 6 has value then Amounttype = 1
                            ////    if (WIsNumeric == false)
                            ////    {
                            ////        AmountType = "0"; //Only Supply --> Rate and value with Model
                            ////    }
                            ////    else
                            ////    {
                            ////        AmountType = "1"; //Only Amount --> Rate and value with Model
                            ////    }
                            ////}
                            ////else if (txtOnlyAmount.Text == "N")
                            ////{
                            ////    AmountType = "0";
                            ////}

                            excelUpload.Column6 = (dr[4].ToString() == null || dr[4].ToString() == string.Empty) ? string.Empty : dr[4].ToString(); // Supplyrate
                            try
                            {
                                excelUpload.Column7 = (dr[5].ToString() == null || dr[5].ToString() == string.Empty) ? string.Empty : dr[5].ToString(); //SuppInstarate
                              
                            }
                            catch
                            {
                                excelUpload.Column7 = "0";
                            }
                            try
                            {
                                excelUpload.Column8 = (dr[6].ToString() == null || dr[6].ToString() == string.Empty) ? string.Empty : dr[6].ToString(); // SuppAmt
                                
                            }
                            catch
                            {
                                excelUpload.Column8 = "0";
                            }
                            try
                            {
                                excelUpload.Column9 = (dr[7].ToString() == null || dr[7].ToString() == string.Empty) ? string.Empty : dr[7].ToString(); //SuppInstAmt
                               
                            }
                            catch
                            {
                                excelUpload.Column9 = "0";
                            }
                            try
                            {
                                excelUpload.Column10 = (dr[8].ToString() == null || dr[8].ToString() == string.Empty) ? " " : dr[8].ToString(); //Remarks
                               
                            }
                            catch
                            {
                                excelUpload.Column10 = "";
                            }
                            try
                            {
                                excelUpload.Column11 = (dr[9].ToString() == null || dr[9].ToString() == string.Empty) ? "" : dr[9].ToString(); //LSP_SP
                                
                            }
                            catch
                            {
                                excelUpload.Column11 = "0";
                            }
                            try
                            {
                                excelUpload.Column12 = (dr[10].ToString() == null || dr[19].ToString() == string.Empty) ? "" : dr[10].ToString(); //LSP_SP
                               
                            }
                            catch
                            {
                                excelUpload.Column11 = "0";
                            }
                #region Unwanted
                       
                            //excelUpload.Column13 = (dr[12].ToString() == null || dr[12].ToString() == string.Empty) ? "" : dr[12].ToString(); //Buyer
                            //excelUpload.Column14 = (dr[13].ToString() == null || dr[13].ToString() == string.Empty) ? "" : dr[13].ToString(); //  Auctioneer Name
                            //excelUpload.Column15 = (dr[14].ToString() == null || dr[14].ToString() == string.Empty) ? "" : dr[14].ToString(); // TeaType
                            //excelUpload.Column16 = (dr[15].ToString() == null || dr[15].ToString() == string.Empty) ? "" : dr[15].ToString(); // CAtegory
                            //excelUpload.Column17 = (dr[16].ToString() == null || dr[16].ToString() == string.Empty) ? "" : dr[16].ToString(); // Origion
                            //excelUpload.Column18 = (dr[17].ToString() == null || dr[17].ToString() == string.Empty) ? "0" : dr[17].ToString(); //NoofPAck
                            //excelUpload.Column19 = (dr[18].ToString() == null || dr[18].ToString() == string.Empty) ? "0" : dr[18].ToString(); //grosswt
                            //excelUpload.Column20 = (dr[19].ToString() == null || dr[19].ToString() == string.Empty) ? "0" : dr[19].ToString(); //NetWt

                            //excelUpload.Column21 = (dr[20].ToString() == null || dr[20].ToString() == string.Empty) ? "0" : dr[20].ToString(); // invwt
                            //excelUpload.Column22 = (dr[21].ToString() == null || dr[21].ToString() == string.Empty) ? "0" : dr[21].ToString(); //sampleqty
                            //excelUpload.Column23 = (dr[22].ToString() == null || dr[22].ToString() == string.Empty) ? "0" : dr[22].ToString(); // BAsepeice
                            //excelUpload.Column24 = (dr[23].ToString() == null || dr[23].ToString() == string.Empty) ? "0" : dr[23].ToString(); // //GPNO
                            //excelUpload.Column25 = (dr[24].ToString() == null || dr[24].ToString() == string.Empty) ? "0" : dr[24].ToString(); // GPDate
                            ////WDate = excelUpload.Column25.Replace("/", "-");
                            ////sDate = (Convert.ToDateTime(WDate, dateinfo)).ToString("yyyy-MM-dd");
                            ////excelUpload.Column25 = sDate;
                            //excelUpload.Column26 = (dr[25].ToString() == null || dr[25].ToString() == string.Empty) ? "" : dr[25].ToString(); //   PACKType
                            //excelUpload.Column27 = (dr[26].ToString() == null || dr[26].ToString() == string.Empty) ? "" : dr[26].ToString(); // PAckNO
                            //excelUpload.Column28 = (dr[27].ToString() == null || dr[27].ToString() == string.Empty) ? "" : dr[27].ToString(); // Lotstatus
                            #endregion
                            err = 0;
                        //}
                        #endregion
                if (err == 0) // skip records if data contains invalid or blank values on key fields RevNo
                {
                   // excelUpload.Column46 = AmountType; // when 0 with txtOnlyAmount = 1 then installamount else it is supply amount only
                    excelUpload.Column47 = txtOnlyAmount.Text;
                    excelUpload.Column48 = txtFileName.Text;
                    excelUpload.Column49 = txtSheetName.Text;
                    excelUpload.Column50 = RevNo; 
                    excelUpload.Column51 = CompId; 
                    excelUpload.Column52 = ClientCode;
                    excelUpload.Column53 = TranType;
                    excelUpload.Column54 = ddlProject.SelectedValue; //ProjectCode;//future use
                    excelUpload.Column55 = BaseMsg.EmployeeCode;
                    excelUpload.UploadType = ExFormat; //used for upload file type
                    excelUploadList.Add(excelUpload);
                }
                
              }
            linecount++;
        }
        return excelUploadList;
    }
    private List<DepotSalesMsg> CheckData(List<ExcelUploadMsg> ExData, string TranType)
    {
        System.Globalization.DateTimeFormatInfo dateinfo = new System.Globalization.DateTimeFormatInfo();
        //dateinfo.ShortDatePattern = "MM-dd-yyyy";
        dateinfo.ShortDatePattern = "dd-MM-yyyy";

        lblMessage.Text = "CheckData Method Running";
        List<DepotSalesMsg> DataErr = new List<DepotSalesMsg>();
       
            int RCount = 0; //Since XL has column Heading, the rownumber to start from 2
            ChkResult = 0; // set OK
            DepotSalesMsg dtMsg = new DepotSalesMsg();
            int ChkResult1 = 0;

            
                ChkResult = 0;
                ChkResult1 = 0; //reset 
                dtMsg.Result = "OK";
                DataErr.Add(dtMsg);
            
        
        return DataErr;
    }
    private bool IsNumeric(string WText)
    {
        decimal number3 = 0;
        bool canConvert = decimal.TryParse(WText, out number3);
        return canConvert;
    }
    private string ConvDateFormat(string WDate)
    {
        //char SpaceChar = ' '; //to identify the date and time portion
        string Error = "";
        string WDOB = "";
        char SepChar = '.';
        if (WDate.IndexOf('.', 1) > 0)
        {
            SepChar = '.';
        }
        else if (WDate.IndexOf('/', 1) > 0)
        {
            SepChar = '/';
        }
        else if (WDate.IndexOf('-', 1) > 0)
        {
            SepChar = '-';
        }
        else
        {
            Error = Error + "* Not Valid Date Separator";
        }

        string[] split = WDate.Split(new char[] { SepChar });

        // int lenDate = Convert.ToInt16(WDate.Substring(WDate.IndexOf(SpaceChar, 1)));

        string dd = split[0];
        string WMMYYYY = WDate.Substring(WDate.IndexOf(SepChar, 1)); // get last digits after the 2nd date separator -yyyy ---getting /YYYY
        string DD = WDate.Substring(0, WDate.Length - WMMYYYY.Length);
        string WYYYY = WMMYYYY.Substring(WMMYYYY.IndexOf(SepChar, 1)); // we get the first dd/mm character after excluding last / in WDate getting 9/9
        string MM = WMMYYYY.Substring(1, WMMYYYY.Length - WYYYY.Length - 1); //get last digits after the 1st date separator --MM /9

        string YYYY = WYYYY.Substring(1, 4);


        // string MMYYYY = WDate.Substring((dd.Length + 1), (WDate.Length - split[0].ToString().Length - 1));
        try
        {
            if (Convert.ToInt32(dd) > 0)
                if (dd.Length == 1)
                {
                    dd = "0" + dd;
                }
                else if (dd.Length == 2)
                {

                }
                else
                {
                    Error = Error + "* Error in Format to be DD";

                }
            else
            {
                Error = Error + "* Error in Date DD Format";
            }

            if (Convert.ToInt32(MM) > 0)
                if (MM.Length == 1)
                {

                    MM = "0" + MM;
                }
                else if (MM.Length == 2)
                {

                }
                else
                {

                    Error = Error + "* Error in Month Format to be MM";
                }
            else
            {
                Error = Error + "* Error in Date MM Format";
            }

            if (Convert.ToInt32(YYYY) > 0)
            {
                if (YYYY.Length > 3)
                {
                    YYYY = YYYY.Substring(0, 4);
                }
                else
                {
                    Error = Error + "* Error in Year Format to be YYYY";
                }

            }
            else
            {
                Error = Error + "* Error in Date YYYY Format";
            }
            if ((Convert.ToInt32(MM) == 1 || Convert.ToInt32(MM) == 3 || Convert.ToInt32(MM) == 5 || Convert.ToInt32(MM) == 7 || Convert.ToInt32(MM) == 8 || Convert.ToInt32(MM) == 10
          || Convert.ToInt32(MM) == 12) && Convert.ToInt32(DD) > 31)
            {
                Error = Error + "* Error in Date Format";
            }
            if ((Convert.ToInt32(MM) == 4 || Convert.ToInt32(MM) == 6 || Convert.ToInt32(MM) == 9 || Convert.ToInt32(MM) == 11) && Convert.ToInt32(DD) > 30)
            {
                Error = Error + "* Error in Date Format";
            }
            if (Convert.ToInt32(MM) == 2)
            {
                int lpyr = Convert.ToInt32(YYYY) % 4;
                if (((lpyr == 0) && Convert.ToInt32(DD) > 29) || ((lpyr > 0) && Convert.ToInt32(DD) > 28))
                {
                    Error = Error + "* Error in Date Format";
                }
            }
            if (Error.Length > 0)
            {
                WDOB = Error;
            }
            else
            {
                //WDOB = dd + "." + MM + "." + YYYY;
                WDOB = MM + "/" + dd + "/" + YYYY;
            }

        }
        catch
        {
            Error = Error + "* Error in Date Format only Numbers allowed";
            WDOB = Error;
        }

        return WDOB;
    }
    private string RemoveUnWantedCharacter(string WString)
    {
        string WParticulars = "";

        WParticulars = WString.Replace("\n", ""); //remove New line Quotes
        WParticulars = WParticulars.Replace("\r", ""); //remove New line Quotes
        WParticulars = WParticulars.Replace("\t", ""); //remove termin
        WParticulars = WParticulars.Replace("\"", ""); //remove Double Quotes
        WParticulars = WParticulars.Replace("'", ""); //remove Double Quotes
        WParticulars = WParticulars.Trim();
        string output = new string(WParticulars.Where(c => !char.IsControl(c)).ToArray()); // remove all control characters from your input string 

        return WParticulars;
    }
 

    #endregion

 


}