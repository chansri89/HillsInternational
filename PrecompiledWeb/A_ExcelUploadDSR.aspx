<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage3.master" AutoEventWireup="true" CodeFile="A_ExcelUploadDSR.aspx.cs" Inherits="A_ExcelUploadDSR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="tis-page tis-page--medium">
    <div class="tis-page-header">
        <div class="tis-page-header__text">
            <span class="tis-eyebrow">Uploads</span>
            <h1 class="tis-page-header__title"><asp:Label ID="lblPgmHdr" runat="server" Text="DSR upload" /></h1>
            <p class="tis-page-header__desc"><asp:Label ID="Label1" runat="server" Text="Load the BasicRate, DSRH, DSRI and DAR sheets in that order, one sheet at a time. For items only, upload BasicRate alone. A DAR upload can take more than 5 minutes." /></p>
        </div>
    </div>

    <tis:UploadSteps ID="steps" runat="server" SheetPanelId="pnlSheetName"
        Step1="Choose company and workbook" Step2="Select a sheet" Step3="Save to DSR" />

    <div id="tblFileType" runat="server" class="tis-card">
        <div class="tis-card__header">
            <div class="tis-card__heading">
                <div class="tis-card__title">Workbook</div>
                <div class="tis-card__subtitle">Only .xlsx files are supported.</div>
            </div>
        </div>
        <div class="tis-card__body tis-stack">
            <div class="tis-form-grid tis-form-grid--2">
                <div class="tis-field">
                    <asp:Label ID="lblCompany" runat="server" Text="Company" AssociatedControlID="ddlCompany" CssClass="tis-label" />
                    <asp:DropDownList ID="ddlCompany" runat="server" DataTextField="CompanyName" DataValueField="CompanyId" />
                </div>
            </div>
            <div class="tis-field">
                <asp:Label ID="lblFileUpload" runat="server" Text="Excel file" AssociatedControlID="FlUpdExcel" CssClass="tis-label" />
                <div class="tis-dropzone">
                    <span class="tis-dropzone__icon"><svg class="tis-icon" aria-hidden="true"><use href="Images/icons.svg#i-sheet"></use></svg></span>
                    <span class="tis-dropzone__text">
                        <span class="tis-dropzone__title">Drop an .xlsx file here, or click to browse</span>
                        <span class="tis-dropzone__hint">Load one sheet at a time: BasicRate, DSRH, DSRI, then DAR.</span>
                    </span>
                    <span class="tis-btn tis-btn--sm tis-dropzone__action">Browse</span>
                    <asp:FileUpload ID="FlUpdExcel" runat="server" accept=".xlsx" />
                </div>
            </div>
            <asp:Label ID="lblMessage" runat="server" CssClass="tis-alert tis-alert--warning" />
            <asp:TextBox ID="txtWParameterId" runat="server" Visible="false" />
            <asp:TextBox ID="txtFileName" runat="server" Visible="false" />
            <asp:Label ID="lblOperationGroupName" runat="server" Text="File Type" Visible="false" />
            <asp:RadioButtonList ID="rbtExcelType" runat="server" Visible="false" Enabled="false" RepeatDirection="Horizontal" RepeatLayout="Flow">
                <asp:ListItem Text="Excel" Selected="True" Value="1" />
            </asp:RadioButtonList>
            <asp:Panel ID="Panel1" runat="server" CssClass="tis-alert tis-alert--warning tis-cluster--between">
                <asp:Label ID="lblRevnoChanged" runat="server" Text="Are you sure you want to change the revision number?" />
                <span class="tis-cluster">
                    <asp:Button ID="btnYes" runat="server" OnClick="btnYes_Click" Text="Yes" CssClass="tis-btn tis-btn--primary tis-btn--sm" TabIndex="16" />
                    <asp:Button ID="btnNo" runat="server" OnClick="btnNo_Click" Text="No" CssClass="tis-btn tis-btn--sm" TabIndex="16" />
                </span>
                <asp:TextBox ID="txtSheetName" runat="server" Visible="false" />
                <asp:TextBox ID="txtOnlyAmount" runat="server" Visible="false" />
            </asp:Panel>
        </div>
        <div class="tis-card__footer">
            <asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click" Text="Upload workbook" CssClass="tis-btn tis-btn--primary" TabIndex="16" />
        </div>
    </div>

    <asp:Panel ID="pnlSheetName" runat="server" CssClass="tis-card">
        <div class="tis-card__header">
            <div class="tis-card__heading">
                <div class="tis-card__title">Select sheets to load</div>
                <div class="tis-card__subtitle"><asp:Label ID="Label4" runat="server" Text="Tick the sheet to load, then save. A DAR sheet can take more than 5 minutes." /></div>
            </div>
        </div>
        <div class="tis-table-wrap tis-table-wrap--tall">
            <asp:GridView ID="grdSheetName" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:TemplateField HeaderText="Workbook">
                        <ItemTemplate><asp:Label ID="lblExcelFileName" runat="server" Text='<%# Eval("ExeclFileName") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sheet Name" Visible="false">
                        <ItemTemplate><asp:Label ID="lblSheetName" runat="server" Text='<%# Eval("ExeclSheetName") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sheet" ItemStyle-CssClass="strong">
                        <ItemTemplate><asp:Label ID="lblDispSheetName" runat="server" Text='<%# Eval("DispSheetName") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Amount only" Visible="false" HeaderStyle-CssClass="center" ItemStyle-CssClass="check">
                        <ItemTemplate><asp:CheckBox ID="chkOnlyAmount" runat="server" Checked="false" /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Load" HeaderStyle-CssClass="center" ItemStyle-CssClass="check">
                        <ItemTemplate><asp:CheckBox ID="chkUpLoadSheet" runat="server" Checked="false" /></ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div class="tis-card__footer">
            <asp:Button ID="btnDeselect" runat="server" OnClick="btnDeSelect_Click" Text="Deselect all" CssClass="tis-btn tis-btn--ghost" />
            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save selected sheets" CssClass="tis-btn tis-btn--primary"
                UseSubmitBehavior="false" OnClientClick="this.Disabled='true'; this.Value='Pls Wait..';" />
        </div>
    </asp:Panel>

    <asp:Panel ID="PnlNotOKDeptSales" runat="server" CssClass="tis-card">
        <div class="tis-card__header">
            <div class="tis-card__heading">
                <div class="tis-card__title">Rows that need attention</div>
                <div class="tis-card__subtitle"><asp:Label ID="lblGrdNotOK" runat="server" Text="Rows highlighted in red were not loaded. Fix them in the workbook and upload again." /></div>
            </div>
            <span class="tis-badge tis-badge--danger">Not loaded</span>
        </div>
        <div class="tis-table-wrap tis-table-wrap--tall">
            <asp:GridView ID="GrdWtNotOKDeptSales" runat="server" AutoGenerateColumns="False" RowStyle-CssClass="is-error">
                <Columns>
                    <asp:TemplateField HeaderText="#" HeaderStyle-CssClass="num" ItemStyle-CssClass="num">
                        <ItemTemplate><asp:Label ID="lblRowNumber" runat="server" Text='<%# Eval("RKount") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Invoice number">
                        <ItemTemplate><asp:Label ID="lblEventName" runat="server" Text='<%# Eval("InvoiceNumber") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Invoice date" ItemStyle-CssClass="nowrap">
                        <ItemTemplate><asp:Label ID="lblInvoiceDate" runat="server" Text='<%# Eval("InvoiceDate","{0: dd-MM-yyyy}") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Prompt date" ItemStyle-CssClass="nowrap">
                        <ItemTemplate><asp:Label ID="lblPromptDate" runat="server" Text='<%# Eval("PromptDate","{0: dd-MM-yyyy}") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Buyer">
                        <ItemTemplate><asp:Label ID="lblBuyerName" runat="server" Text='<%# Eval("BuyerName") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Grade">
                        <ItemTemplate><asp:Label ID="lblGrade" runat="server" Text='<%# Eval("Grade") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status" ItemStyle-CssClass="wrap">
                        <ItemTemplate><asp:Label ID="lblResult" runat="server" Text='<%# Eval("Result") %>' /></ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </asp:Panel>

    <asp:Panel ID="PnlDepotSalesStatus" runat="server" CssClass="tis-card">
        <div class="tis-card__header">
            <div class="tis-card__heading">
                <div class="tis-card__title">Uploaded rows</div>
                <div class="tis-card__subtitle">All rows below were loaded successfully.</div>
            </div>
            <span class="tis-badge tis-badge--success">Uploaded</span>
        </div>
        <div class="tis-table-wrap tis-table-wrap--tall">
            <asp:GridView ID="GrdDepotSalesStatus" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:TemplateField HeaderText="#" HeaderStyle-CssClass="num" ItemStyle-CssClass="num">
                        <ItemTemplate><asp:Label ID="lblRowNumber" runat="server" Text='<%# Eval("RKount") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Invoice number">
                        <ItemTemplate><asp:Label ID="lblEventName" runat="server" Text='<%# Eval("InvoiceNumber") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Invoice date" ItemStyle-CssClass="nowrap">
                        <ItemTemplate><asp:Label ID="lblInvoiceDate" runat="server" Text='<%# Eval("InvoiceDate","{0: dd-MM-yyyy}") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Prompt date" ItemStyle-CssClass="nowrap">
                        <ItemTemplate><asp:Label ID="lblPromptDate" runat="server" Text='<%# Eval("PromptDate","{0: dd-MM-yyyy}") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Buyer">
                        <ItemTemplate><asp:Label ID="lblBuyerName" runat="server" Text='<%# Eval("BuyerName") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Grade">
                        <ItemTemplate><asp:Label ID="lblGrade" runat="server" Text='<%# Eval("Grade") %>' /></ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </asp:Panel>

    <asp:Panel ID="pnlNotOKTeaBoard" runat="server" CssClass="tis-card">
        <div class="tis-card__header">
            <div class="tis-card__heading">
                <div class="tis-card__title">Rows that need attention</div>
                <div class="tis-card__subtitle"><asp:Label ID="Label2" runat="server" Text="Rows highlighted in red were not loaded. Fix them in the workbook and upload again." /></div>
            </div>
            <span class="tis-badge tis-badge--danger">Not loaded</span>
        </div>
        <div class="tis-table-wrap tis-table-wrap--tall">
            <asp:GridView ID="grdNotOKTeaBoard" runat="server" AutoGenerateColumns="False" RowStyle-CssClass="is-error">
                <Columns>
                    <asp:TemplateField HeaderText="Deal ID">
                        <ItemTemplate><asp:Label ID="lblDealIdentificationId" runat="server" Text='<%# Eval("DealIdentificationId") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Bank UTR">
                        <ItemTemplate><asp:Label ID="lblBankUTR" runat="server" Text='<%# Eval("BankUTR") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Bank date" ItemStyle-CssClass="nowrap">
                        <ItemTemplate><asp:Label ID="lblBankDate" runat="server" Text='<%# Eval("BankDate","{0: dd-MM-yyyy}") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Buyer entity code">
                        <ItemTemplate><asp:Label ID="lblBuyerEntityCode" runat="server" Text='<%# Eval("BuyerEntityCode") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Amount" HeaderStyle-CssClass="num" ItemStyle-CssClass="num">
                        <ItemTemplate><asp:Label ID="lblAmountPaid" runat="server" Text='<%# Eval("AmountPaid") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status" ItemStyle-CssClass="wrap">
                        <ItemTemplate><asp:Label ID="lblResult" runat="server" Text='<%# Eval("Result") %>' /></ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </asp:Panel>

    <asp:Panel ID="PnlTeaBoardStatus" runat="server" CssClass="tis-card">
        <div class="tis-card__header">
            <div class="tis-card__heading">
                <div class="tis-card__title">Uploaded rows</div>
                <div class="tis-card__subtitle">All rows below were loaded successfully.</div>
            </div>
            <span class="tis-badge tis-badge--success">Uploaded</span>
        </div>
        <div class="tis-table-wrap tis-table-wrap--tall">
            <asp:GridView ID="grdTeaBoardStatus" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:TemplateField HeaderText="#" HeaderStyle-CssClass="num" ItemStyle-CssClass="num">
                        <ItemTemplate><asp:Label ID="lblRowNumber" runat="server" Text='<%# Eval("RKount") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Invoice number">
                        <ItemTemplate><asp:Label ID="lblEventName" runat="server" Text='<%# Eval("InvoiceNumber") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Bank date" ItemStyle-CssClass="nowrap">
                        <ItemTemplate><asp:Label ID="lblBankDate" runat="server" Text='<%# Eval("Bank Date","{0: dd-MM-yyyy}") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Buyer">
                        <ItemTemplate><asp:Label ID="lblBuyerName" runat="server" Text='<%# Eval("BuyerName") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Amount paid" HeaderStyle-CssClass="num" ItemStyle-CssClass="num">
                        <ItemTemplate><asp:Label ID="lblAmountPaid" runat="server" Text='<%# Eval("AmountPaid") %>' /></ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </asp:Panel>

    <asp:Panel ID="pnlNotOkBankReceipt" runat="server" CssClass="tis-card">
        <div class="tis-card__header">
            <div class="tis-card__heading">
                <div class="tis-card__title">Rows that need attention</div>
                <div class="tis-card__subtitle"><asp:Label ID="Label3" runat="server" Text="Rows highlighted in red were not loaded. Fix them in the workbook and upload again." /></div>
            </div>
            <span class="tis-badge tis-badge--danger">Not loaded</span>
        </div>
        <div class="tis-table-wrap tis-table-wrap--tall">
            <asp:GridView ID="grdNotOkBankReceipt" runat="server" AutoGenerateColumns="False" RowStyle-CssClass="is-error">
                <Columns>
                    <asp:TemplateField HeaderText="#" HeaderStyle-CssClass="num" ItemStyle-CssClass="num">
                        <ItemTemplate><asp:Label ID="lblRowNumber" runat="server" Text='<%# Eval("RKount") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Company">
                        <ItemTemplate><asp:Label ID="lblCompanyName" runat="server" Text='<%# Eval("CompanyName") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Bank code">
                        <ItemTemplate><asp:Label ID="lblBankCode" runat="server" Text='<%# Eval("BankCode") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Account number">
                        <ItemTemplate><asp:Label ID="lblAccountNumber" runat="server" Text='<%# Eval("AccountNumber") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Currency">
                        <ItemTemplate><asp:Label ID="lblCurrency" runat="server" Text='<%# Eval("Currency") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Transaction date" ItemStyle-CssClass="nowrap">
                        <ItemTemplate><asp:Label ID="lblTransactionDate" runat="server" Text='<%# Eval("TransactionDate","{0: dd-MM-yyyy}") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Amount" HeaderStyle-CssClass="num" ItemStyle-CssClass="num">
                        <ItemTemplate><asp:Label ID="lblTransactionAmount" runat="server" Text='<%# Eval("TransactionAmount") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status" ItemStyle-CssClass="wrap">
                        <ItemTemplate><asp:Label ID="lblResult" runat="server" Text='<%# Eval("Result") %>' /></ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </asp:Panel>

    <asp:Panel ID="pnlBankReceiptStatus" runat="server" CssClass="tis-card">
        <div class="tis-card__header">
            <div class="tis-card__heading">
                <div class="tis-card__title">Uploaded rows</div>
                <div class="tis-card__subtitle">All rows below were loaded successfully.</div>
            </div>
            <span class="tis-badge tis-badge--success">Uploaded</span>
        </div>
        <div class="tis-table-wrap tis-table-wrap--tall">
            <asp:GridView ID="grdBankReceiptStatus" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:TemplateField HeaderText="#" HeaderStyle-CssClass="num" ItemStyle-CssClass="num">
                        <ItemTemplate><asp:Label ID="lblRowNumber" runat="server" Text='<%# Eval("RKount") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Company">
                        <ItemTemplate><asp:Label ID="lblCompanyName" runat="server" Text='<%# Eval("CompanyName") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Bank code">
                        <ItemTemplate><asp:Label ID="lblBankCode" runat="server" Text='<%# Eval("BankCode") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Account number">
                        <ItemTemplate><asp:Label ID="lblAccountNumber" runat="server" Text='<%# Eval("AccountNumber") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Currency">
                        <ItemTemplate><asp:Label ID="lblCurrency" runat="server" Text='<%# Eval("Currency") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Transaction date" ItemStyle-CssClass="nowrap">
                        <ItemTemplate><asp:Label ID="lblTransactionDate" runat="server" Text='<%# Eval("TransactionDate","{0: dd-MM-yyyy}") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Amount" HeaderStyle-CssClass="num" ItemStyle-CssClass="num">
                        <ItemTemplate><asp:Label ID="lblTransactionAmount" runat="server" Text='<%# Eval("TransactionAmount") %>' /></ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </asp:Panel>
</div>
</asp:Content>
