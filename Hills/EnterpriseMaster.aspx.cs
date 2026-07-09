using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Resources;
using Ganini.Lib;

public partial class EnterpriseMaster : System.Web.UI.Page
{ 
    #region Declaration
    ProcessBus Bus = new ProcessBus();
    List<EnterpriseMasterMsg> EnterpriseList = new List<EnterpriseMasterMsg>();
    public static int DeleteIndex = 0;
    public static int UpdateIndex = 0;
    UserAccess user = new UserAccess();
    BaseClass BaseClassInfo = new BaseClass();
    Validation valid = new Validation();
    public static string ProgramName = string.Empty;
    #endregion
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        ProgramName = System.IO.Path.GetFileName(Request.PhysicalPath);
        if (!Page.IsPostBack)
        {
            LoadGrdEnterpriseMaster();
            if (EnterpriseList.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.MsgForGrdnotLoad + "');", true);
                Pnlgv.Visible = false;
                pnlAdd.Visible = true;

            }
        }
       
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        ///For Deleting a Record from Grid-Ends
        if (!user.HasPermission(ProgramName, UserPermission.CanCreate.ToString()))
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.CreatePermissionRestricted + "');", true);
        }
        else
        {
            if (IsValidSave() == 0)
            {
                EnterpriseSave();
            }
        }
        if (HidUpdateCount.Value == "1")
        {
            EnterpriseUpdate();
            HidUpdateCount.Value = "0";
            return;
        }
       


    }
    #endregion
    #region Methods
    private void LoadGrdEnterpriseMaster()
    {
        EnterpriseMasterMsg Enterprise = new EnterpriseMasterMsg();
        Enterprise.Flag = "R";
        EnterpriseList = Bus.MasEnterpriseInsertUpdateandDelete(Enterprise);
        GrdEnterpriseMaster.DataSource = "";
        GrdEnterpriseMaster.DataSource = EnterpriseList;
        GrdEnterpriseMaster.DataBind();
        if (!user.HasPermission(ProgramName, UserPermission.CanEdit.ToString()))
        {
            GrdEnterpriseMaster.Columns[GrdEnterpriseMaster.Columns.Count - 2].Visible = false;
        }
        if (!user.HasPermission(ProgramName, UserPermission.CanDelete.ToString()))
        {
            GrdEnterpriseMaster.Columns[GrdEnterpriseMaster.Columns.Count - 1].Visible = false;
        }
    }

    private void EnterpriseSave()
    {
        EnterpriseMasterMsg Enterprise = new EnterpriseMasterMsg();
        Enterprise.Flag = "I";

        Enterprise.EnterpriseName = txtEnterprisename.Text.Trim();
        Enterprise.Addr1 = txtaddr1.Text.Trim();
        //Enterprise.Addr1 = (txtaddr1.Text.Trim() != string.Empty ? txtaddr1.Text.Trim() : "0"); //scs 270714 when mandatory why check this
        Enterprise.Addr2 = txtaddr2.Text.Trim() != string.Empty ? txtaddr2.Text.Trim() : "";
        Enterprise.Addr3 = txtaddr3.Text.Trim() != string.Empty ? txtaddr3.Text.Trim() : "";
        //Enterprise.Addr4 = txtaddr4.Text.Trim() != string.Empty ? txtaddr4.Text.Trim() : "";
        Enterprise.Phone1 = Convert.ToInt64(txtEnterprisePhone1.Text.Trim());
        //Enterprise.Phone1 = Convert.ToInt64(txtEnterprisePhone1.Text.Trim() != string.Empty ? txtEnterprisePhone1.Text.Trim() : "0"); //scs 270714 when mandatory why check this
        //Enterprise.Phone2 = Convert.ToInt64(txtEnterprisePhone2.Text.Trim() != string.Empty ? txtEnterprisePhone2.Text.Trim() : "0");
        Enterprise.Fax = Convert.ToInt32(txtfax.Text.Trim() != string.Empty ? txtfax.Text.Trim() : "");
        //Enterprise.Email = txtEmailId.Text.Trim();
        Enterprise.Website = txtwebsite.Text.Trim() != string.Empty ? txtwebsite.Text.Trim() : "";
        Enterprise.CreatedBy = BaseClassInfo.EmployeeCode;
        Enterprise.IsActive = ChkIsActive.Checked;
        EnterpriseList = Bus.MasEnterpriseInsertUpdateandDelete(Enterprise);

        //Output Dispay
        foreach (EnterpriseMasterMsg EnterpriseSave in EnterpriseList)
        {
            if (EnterpriseSave.EnterpriseResult == "0")
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.SuccessFullySaved + "');", true);
                LoadGrdEnterpriseMaster();
                AllClear();
                if (EnterpriseList.Count >0)
                    {
                        Pnlgv.Visible = true;
                        pnlAdd.Visible = true;
                    }
                break;

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + EnterpriseSave.EnterpriseResult + "');", true);
                break;
            }
        }
    }
    public void EnterpriseUpdate()
    {
        GridViewRow row = GrdEnterpriseMaster.Rows[UpdateIndex];
        EnterpriseMasterMsg Enterprise = new EnterpriseMasterMsg();
        Enterprise.Flag = "U";
        TextBox txtEnterpriseId = (TextBox)row.FindControl("txtEnterpriseId");
        TextBox txtEnterpriseName = (TextBox)row.FindControl("txtEnterpriseName");
        TextBox txtAddr1 = (TextBox)row.FindControl("txtAddr1");
        TextBox txtAddr2 = (TextBox)row.FindControl("txtAddr2");
        TextBox txtAddr3 = (TextBox)row.FindControl("txtAddr3");
        //TextBox txtAddr4 = (TextBox)row.FindControl("txtAddr4");
        TextBox txtPhone1 = (TextBox)row.FindControl("txtPhone1");
        //TextBox txtPhone2 = (TextBox)row.FindControl("txtPhone2");
        TextBox txtFax = (TextBox)row.FindControl("txtFax"); 
        //TextBox txtEmail = (TextBox)row.FindControl("txtEmail");
        TextBox txtWebsite = (TextBox)row.FindControl("txtWebsite");
        //TextBox txtCertificationId = (TextBox)row.FindControl("txtCertificationId");
        CheckBox chkActive = (CheckBox)row.FindControl("chkIsActive");
        
        Enterprise.EnterpriseId = Convert.ToInt32(txtEnterpriseId.Text.Trim());
        Enterprise.EnterpriseName = txtEnterpriseName.Text.Trim();
        Enterprise.Addr1 = txtAddr1.Text.Trim();
        Enterprise.Addr2 = txtAddr2.Text.Trim();
        Enterprise.Addr3 = txtAddr3.Text.Trim();
        //Enterprise.Addr4 = txtAddr4.Text.Trim();
        Enterprise.Phone1 = Convert.ToInt64(txtPhone1.Text.Trim());
        //Enterprise.Phone2 = Convert.ToInt64(txtPhone2.Text.Trim()); //Convert.ToInt32(txtfax.Text.Trim() != string.Empty ? txtfax.Text.Trim() : "");
        Enterprise.Fax = Convert.ToInt32(txtfax.Text.Trim() != string.Empty ? txtfax.Text.Trim() : "0");
        //if (txtPhone1.Text.Trim() == "")
        //{
        //    Enterprise.Phone1 = 0;
        //}
        //else
        //{
        //    Enterprise.Phone1 = Convert.ToInt64(txtPhone1.Text.Trim());
        //}
        
        //Enterprise.Email = txtEmail.Text.Trim();
        Enterprise.Website = txtWebsite.Text.Trim();
        Enterprise.IsActive = chkActive.Checked;
        Enterprise.CreatedBy = BaseClassInfo.EmployeeCode;

        EnterpriseList = Bus.MasEnterpriseInsertUpdateandDelete(Enterprise);
        GrdEnterpriseMaster.EditIndex = -1;

        foreach (EnterpriseMasterMsg EnterpriseUpdate in EnterpriseList)
        {
            if (EnterpriseUpdate.EnterpriseResult == "0")
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + AgilerMail.UpdatedSuccessfully + "');", true);
                AllClear();
                break;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + EnterpriseUpdate.EnterpriseResult + "');", true);
                break;
            }
        }
    }
  
    #endregion
    #region Clear
    public void AllClear()
    {

        txtEnterprisename.Text = "";
        txtaddr1.Text = "";
        txtaddr2.Text = "";
        txtaddr3.Text = "";
       // txtaddr4.Text = "";
        txtEnterprisePhone1.Text = "";
        //txtEnterprisePhone2.Text = "";
        txtfax.Text = "";
       // txtEmailId.Text = "";
        txtwebsite.Text = "";
        ChkIsActive.Checked = false;
        LoadGrdEnterpriseMaster();

    }
    #endregion
    #region Validation
    private int IsValidSave()
    {
        int Error = 0;
         string DisplayError = "";
        if ((txtEnterprisename.Text.Trim() == "" || txtEnterprisename.Text.Length.ToString().Trim() == "0"))
        {
            DisplayError = DisplayError + AgilerMail.ErrorEnterpriseName;
            Error = 1;
        }
        if (txtaddr1.Text.Trim() == "" || txtaddr1.Text.Length.ToString().Trim() == "0")
        {
            DisplayError = DisplayError + "--" + AgilerMail.ErrorAddr1;
            Error = 1;
        }
        //if (txtEmailId.Text.Trim() == "" || txtEmailId.Text.Length.ToString().Trim() == "0")
        //{
        //    DisplayError = DisplayError + "--" + AgilerMail.ErrEmail;
        //    Error = 1;
        //} //scs 270714 why enterprise emailid not necessary at all
        if (txtEnterprisePhone1.Text.Trim() == "" || txtEnterprisePhone1.Text.Length.ToString().Trim() == "0")
        {
            DisplayError = DisplayError + "--" + AgilerMail.ErrPhone;
            Error = 1;
        }
        else if (!valid.IsPositiveNumber(txtEnterprisePhone1.Text.Trim()))
        {
            //DisplayError = DisplayError + "--" + AgilerMail.ErrorNumber;
            Error = 1;
        }
        else
        {
            int chkPhone1;
            string Phone1 = txtEnterprisePhone1.Text.Trim();
            if (Phone1 != "")
            {
                if (int.TryParse(Phone1, out chkPhone1))
                {
                    Error = 0;
                }
                else
                {
                    DisplayError = DisplayError + "Phone1 --" + AgilerMail.ErrorNumber;
                    Error = 1;
                }
            }

        }

       // int chkPhone2;
        //string Phone2 = txtEnterprisePhone2.Text.Trim();
        //if (Phone2 != "")
        //{
        //    if (int.TryParse(Phone2, out chkPhone2))
        //    {
        //        Error = 0;
        //    }
        //    else
        //    {
        //        DisplayError = DisplayError + "Phone2 --" + AgilerMail.ErrorNumber;
        //        Error = 1;
        //    }
        //}

        int chkFax;
        string Fax = txtfax.Text.Trim();
        if (Fax != "")
        {
            if (int.TryParse(Fax, out chkFax))
            {
                Error = 0;
            }
            else
            {
                //DisplayError = DisplayError + "Fax --" + AgilerMail.ErrorNumber;
                Error = 1;
            }
        }

        if (Error == 1)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + DisplayError + "');", true);
        }
        return Error;
    }
    private int IsValidGrid(string EnterpriseName, string Addr1, string Phone1)
    {

        int Error = 0;
        string DisplayError = "";
        if ((EnterpriseName.Trim() == "" || EnterpriseName.Trim().ToString().Length == 0))
        {
           // DisplayError = DisplayError + AgilerMail.ErrorEnterpriseName;
            Error = 1;
        }
        if (Addr1.Trim() == "" || Addr1.Trim().ToString().Length == 0)
        {
            DisplayError = DisplayError + "--" + AgilerMail.ErrorAddr1;
            Error = 1;
        }
        //if (Email.Trim() == "" || Email.Trim().ToString().Length == 0)
        //{
        //    DisplayError = DisplayError + "--" + AgilerMail.ErrEmail;
        //    Error = 1;
        //}
        if (Phone1.Trim() == "" || Phone1.Trim().ToString().Length == 0)
        {
            DisplayError = DisplayError + "--" + AgilerMail.ErrPhone;
            Error = 1;
        }
        else if (!valid.IsPositiveNumber(Phone1.Trim()))
        {
            DisplayError = DisplayError + "--" + AgilerMail.ErrorNumber;
            Error = 1;
        }
        else
        {
            //int chkPhone1;
            //string Phone = Phone1.Trim();
            //if (Phone1 != "")
            //{
            //    if (int.TryParse(Phone1, out chkPhone1))
            //    {
            //        Error = 0;
            //    }
            //    else
            //    {
            //        DisplayError = DisplayError + "Phone1 --" + AgilerMail.ErrorNumber;
            //        Error = 1;
            //    }
            //}

        }

        if (Error == 1)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + DisplayError + "');", true);
        }
        return Error;
    }
    #endregion
    #region GrdEdit

    protected void GrdEnterpriseMaster_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        UpdateIndex = e.RowIndex;
        GridViewRow row = GrdEnterpriseMaster.Rows[UpdateIndex];
        string EnterpriseName = ((TextBox)row.FindControl("txtEnterpriseName")).Text.Trim();
        string Addr1 = ((TextBox)row.FindControl("txtAddr1")).Text.Trim();
        string Addr2 = ((TextBox)row.FindControl("txtAddr2")).Text.Trim();
        string Addr3 = ((TextBox)row.FindControl("txtAddr3")).Text.Trim();
        //string Addr4 = ((TextBox)row.FindControl("txtAddr4")).Text.Trim();
        string Phone1 = ((TextBox)row.FindControl("txtPhone1")).Text.Trim();
        //string Phone2 = ((TextBox)row.FindControl("txtPhone2")).Text.Trim();
        //string Email = ((TextBox)row.FindControl("txtEmail")).Text.Trim();
        string Fax = ((TextBox)row.FindControl("txtFax")).Text.Trim();
        string Website = ((TextBox)row.FindControl("txtWebsite")).Text.Trim();
        if (IsValidGrid(EnterpriseName,Addr1,Phone1) == 0)
        {
            EnterpriseUpdate();
        }
    }
   
    protected void GrdEnterpriseMaster_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GrdEnterpriseMaster.EditIndex = -1;
        LoadGrdEnterpriseMaster();
    }
    protected void GrdEnterpriseMaster_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GrdEnterpriseMaster.EditIndex = e.NewEditIndex;
        LoadGrdEnterpriseMaster();
        GridViewRow row = GrdEnterpriseMaster.Rows[GrdEnterpriseMaster.EditIndex];
        TextBox EnterpriseName = (TextBox)row.FindControl("txtEnterpriseName");
        EnterpriseName.Focus();
    }

    #endregion
}