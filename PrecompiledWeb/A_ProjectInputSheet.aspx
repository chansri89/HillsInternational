<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage3.master" AutoEventWireup="true" CodeFile="A_ProjectInputSheet.aspx.cs" Inherits="A_ProjectInputSheet" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="tis-page tis-page--medium" data-density="compact">
    <div class="tis-page-header">
        <div class="tis-page-header__text">
            <span class="tis-eyebrow">Project Input</span>
            <h1 class="tis-page-header__title"><asp:Label ID="lblPgmHdr" runat="server" Text="Project input sheet" /></h1>
            <p class="tis-page-header__desc"><asp:Label ID="Label1" runat="server" Text="Load the material, labour and plant rate input sheet for the selected project from an Excel workbook." /></p>
        </div>
    </div>

    <tis:UploadSteps ID="steps" runat="server" SheetPanelId="pnlSheetName"
        Step1="Choose project and workbook" Step2="Select the sheet" Step3="Save to project input" />

    <div id="tblFileType" runat="server" class="tis-card">
        <div class="tis-card__header">
            <div class="tis-card__heading">
                <div class="tis-card__title">Project and workbook</div>
                <div class="tis-card__subtitle">.xls and .xlsx files are supported.</div>
            </div>
        </div>
        <div class="tis-card__body tis-stack">
            <div class="tis-form-grid tis-form-grid--3">
                <div class="tis-field">
                    <asp:Label ID="lblCompany" runat="server" Text="Company" Visible="true" AssociatedControlID="ddlCompany" CssClass="tis-label" />
                    <asp:DropDownList ID="ddlCompany" runat="server" Visible="true" AutoPostBack="true" OnSelectedIndexChanged="ddlCompanyChanged"
                        DataTextField="CompanyName" DataValueField="CompanyId" />
                </div>
                <div class="tis-field">
                    <asp:Label ID="lblClients" runat="server" Text="Client" Visible="true" AssociatedControlID="ddlClient" CssClass="tis-label" />
                    <asp:DropDownList ID="ddlClient" runat="server" Visible="true" AutoPostBack="true" OnSelectedIndexChanged="ddlClientChanged"
                        DataTextField="ClientName" DataValueField="ClientCode" />
                </div>
                <div class="tis-field">
                    <asp:Label ID="lblProject" runat="server" Text="Project" Visible="true" AssociatedControlID="ddlProject" CssClass="tis-label" />
                    <asp:DropDownList ID="ddlProject" runat="server" DataTextField="ProjectName" DataValueField="ClientProjectId" />
                </div>
            </div>
            <div class="tis-field">
                <asp:Label ID="lblFileUpload" runat="server" Text="Excel file" AssociatedControlID="FlUpdExcel" CssClass="tis-label" />
                <div class="tis-dropzone">
                    <span class="tis-dropzone__icon"><svg class="tis-icon" aria-hidden="true"><use href="Images/icons.svg#i-sheet"></use></svg></span>
                    <span class="tis-dropzone__text">
                        <span class="tis-dropzone__title">Drop an Excel file here, or click to browse</span>
                        <span class="tis-dropzone__hint">You choose which sheet to load after the workbook is read.</span>
                    </span>
                    <span class="tis-btn tis-btn--sm tis-dropzone__action">Browse</span>
                    <asp:FileUpload ID="FlUpdExcel" runat="server" accept=".xls,.xlsx" />
                </div>
            </div>
            <asp:Label ID="lblMessage" runat="server" CssClass="tis-alert tis-alert--warning" />
            <asp:TextBox ID="txtWParameterId" runat="server" Visible="false" />
            <asp:TextBox ID="txtFileName" runat="server" Visible="false" />
            <asp:Label ID="lblOperationGroupName" runat="server" Text="File Type" Visible="false" />
            <asp:RadioButtonList ID="rbtExcelType" runat="server" Visible="false" Enabled="false" RepeatDirection="Horizontal" RepeatLayout="Flow">
                <asp:ListItem Text="Excel" Selected="True" Value="1" />
            </asp:RadioButtonList>
            <asp:Panel ID="Panel1" runat="server" CssClass="tis-alert tis-alert--warning">
                <div class="tis-cluster tis-cluster--between">
                    <asp:Label ID="lblRevnoChanged" runat="server" Text="Are you sure you want to change the revision number?" />
                    <div class="tis-cluster">
                        <asp:Button ID="btnNo" runat="server" OnClick="btnNo_Click" Text="No" CssClass="tis-btn tis-btn--sm" TabIndex="16" />
                        <asp:Button ID="btnYes" runat="server" OnClick="btnYes_Click" Text="Yes" CssClass="tis-btn tis-btn--primary tis-btn--sm" TabIndex="16" />
                    </div>
                </div>
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
                <div class="tis-card__title">Select the sheet to load</div>
                <div class="tis-card__subtitle"><asp:Label ID="Label4" runat="server" Text="Tick exactly one sheet, then save." /></div>
            </div>
        </div>
        <div class="tis-table-wrap tis-table-wrap--tall" title="SheetName">
            <asp:GridView ID="grdSheetName" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:TemplateField HeaderText="Workbook" Visible="true">
                        <ItemTemplate><asp:Label ID="lblExcelFileName" runat="server" Text='<%# Eval("ExeclFileName") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sheet Name" Visible="false">
                        <ItemTemplate><asp:Label ID="lblSheetName" runat="server" Text='<%# Eval("ExeclSheetName") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sheet" Visible="true" ItemStyle-CssClass="strong">
                        <ItemTemplate><asp:Label ID="lblDispSheetName" runat="server" Text='<%# Eval("DispSheetName") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Only Amount" Visible="false">
                        <ItemTemplate><asp:CheckBox ID="chkOnlyAmount" runat="server" Checked="false" /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Load" Visible="true" HeaderStyle-CssClass="center" ItemStyle-CssClass="check">
                        <ItemTemplate><asp:CheckBox ID="chkUpLoadSheet" runat="server" Checked="false" /></ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <tis:EmptyState ID="emptySheets" runat="server" Icon="sheet" Title="No sheets found"
                        Text="The workbook has no readable sheets. Check the file and upload it again." />
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
        <div class="tis-card__footer">
            <asp:Button ID="btnDeselect" runat="server" OnClick="btnDeSelect_Click" Visible="false" Text="Deselect all" CssClass="tis-btn tis-btn--ghost" />
            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" CssClass="tis-btn tis-btn--primary"
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
        <div class="tis-table-wrap tis-table-wrap--tall" title="OK Data in UpLoad">
            <asp:GridView ID="GrdWtNotOKDeptSales" runat="server" AutoGenerateColumns="False" RowStyle-CssClass="is-error">
                <Columns>
                    <asp:TemplateField HeaderText="#" Visible="true" HeaderStyle-CssClass="num" ItemStyle-CssClass="num">
                        <ItemTemplate><asp:Label ID="lblRowNumber" runat="server" Text='<%# Eval("RKount") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Invoice number" Visible="true">
                        <ItemTemplate><asp:Label ID="lblEventName" runat="server" Text='<%# Eval("InvoiceNumber") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Invoice date" ItemStyle-CssClass="nowrap">
                        <ItemTemplate><asp:Label ID="lblInvoiceDate" runat="server" Text='<%# Eval("InvoiceDate","{0: dd-MM-yyyy}") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Prompt date" ItemStyle-CssClass="nowrap">
                        <ItemTemplate><asp:Label ID="lblPromptDate" runat="server" Text='<%# Eval("PromptDate","{0: dd-MM-yyyy}") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Buyer" Visible="true">
                        <ItemTemplate><asp:Label ID="lblBuyerName" runat="server" Text='<%# Eval("BuyerName") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Grade" Visible="true">
                        <ItemTemplate><asp:Label ID="lblGrade" runat="server" Text='<%# Eval("Grade") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status" Visible="true" ItemStyle-CssClass="wrap">
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
                    <asp:TemplateField HeaderText="#" Visible="true" HeaderStyle-CssClass="num" ItemStyle-CssClass="num">
                        <ItemTemplate><asp:Label ID="lblRowNumber" runat="server" Text='<%# Eval("RKount") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Invoice number" Visible="true">
                        <ItemTemplate><asp:Label ID="lblEventName" runat="server" Text='<%# Eval("InvoiceNumber") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Invoice date" ItemStyle-CssClass="nowrap">
                        <ItemTemplate><asp:Label ID="lblInvoiceDate" runat="server" Text='<%# Eval("InvoiceDate","{0: dd-MM-yyyy}") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Prompt date" ItemStyle-CssClass="nowrap">
                        <ItemTemplate><asp:Label ID="lblPromptDate" runat="server" Text='<%# Eval("PromptDate","{0: dd-MM-yyyy}") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Buyer" Visible="true">
                        <ItemTemplate><asp:Label ID="lblBuyerName" runat="server" Text='<%# Eval("BuyerName") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Grade" Visible="true">
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
        <div class="tis-table-wrap tis-table-wrap--tall" title="OK Data in UpLoad">
            <asp:GridView ID="grdNotOKTeaBoard" runat="server" AutoGenerateColumns="False" RowStyle-CssClass="is-error">
                <Columns>
                    <asp:TemplateField HeaderText="Deal ID" Visible="true">
                        <ItemTemplate><asp:Label ID="lblDealIdentificationId" runat="server" Text='<%# Eval("DealIdentificationId") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Bank UTR" Visible="true">
                        <ItemTemplate><asp:Label ID="lblBankUTR" runat="server" Text='<%# Eval("BankUTR") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Bank date" ItemStyle-CssClass="nowrap">
                        <ItemTemplate><asp:Label ID="lblBankDate" runat="server" Text='<%# Eval("BankDate","{0: dd-MM-yyyy}") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Buyer entity code" Visible="true">
                        <ItemTemplate><asp:Label ID="lblBuyerEntityCode" runat="server" Text='<%# Eval("BuyerEntityCode") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Amount" Visible="true" HeaderStyle-CssClass="num" ItemStyle-CssClass="num">
                        <ItemTemplate><asp:Label ID="lblAmountPaid" runat="server" Text='<%# Eval("AmountPaid") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status" Visible="true" ItemStyle-CssClass="wrap">
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
                    <asp:TemplateField HeaderText="#" Visible="true" HeaderStyle-CssClass="num" ItemStyle-CssClass="num">
                        <ItemTemplate><asp:Label ID="lblRowNumber" runat="server" Text='<%# Eval("RKount") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Invoice number" Visible="true">
                        <ItemTemplate><asp:Label ID="lblEventName" runat="server" Text='<%# Eval("InvoiceNumber") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Bank date" ItemStyle-CssClass="nowrap">
                        <ItemTemplate><asp:Label ID="lblBankDate" runat="server" Text='<%# Eval("Bank Date","{0: dd-MM-yyyy}") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Buyer" Visible="true">
                        <ItemTemplate><asp:Label ID="lblBuyerName" runat="server" Text='<%# Eval("BuyerName") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Amount paid" Visible="true" HeaderStyle-CssClass="num" ItemStyle-CssClass="num">
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
        <div class="tis-table-wrap tis-table-wrap--tall" title="OK Data in UpLoad">
            <asp:GridView ID="grdNotOkBankReceipt" runat="server" AutoGenerateColumns="False" RowStyle-CssClass="is-error">
                <Columns>
                    <asp:TemplateField HeaderText="#" Visible="true" HeaderStyle-CssClass="num" ItemStyle-CssClass="num">
                        <ItemTemplate><asp:Label ID="lblRowNumber" runat="server" Text='<%# Eval("RKount") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Company" Visible="true">
                        <ItemTemplate><asp:Label ID="lblCompanyName" runat="server" Text='<%# Eval("CompanyName") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Bank code" Visible="true">
                        <ItemTemplate><asp:Label ID="lblBankCode" runat="server" Text='<%# Eval("BankCode") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Account number" Visible="true">
                        <ItemTemplate><asp:Label ID="lblAccountNumber" runat="server" Text='<%# Eval("AccountNumber") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Currency" Visible="true">
                        <ItemTemplate><asp:Label ID="lblCurrency" runat="server" Text='<%# Eval("Currency") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Transaction date" ItemStyle-CssClass="nowrap">
                        <ItemTemplate><asp:Label ID="lblTransactionDate" runat="server" Text='<%# Eval("TransactionDate","{0: dd-MM-yyyy}") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Amount" Visible="true" HeaderStyle-CssClass="num" ItemStyle-CssClass="num">
                        <ItemTemplate><asp:Label ID="lblTransactionAmount" runat="server" Text='<%# Eval("TransactionAmount") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status" Visible="true" ItemStyle-CssClass="wrap">
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
                    <asp:TemplateField HeaderText="#" Visible="true" HeaderStyle-CssClass="num" ItemStyle-CssClass="num">
                        <ItemTemplate><asp:Label ID="lblRowNumber" runat="server" Text='<%# Eval("RKount") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Company" Visible="true">
                        <ItemTemplate><asp:Label ID="lblCompanyName" runat="server" Text='<%# Eval("CompanyName") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Bank code" Visible="true">
                        <ItemTemplate><asp:Label ID="lblBankCode" runat="server" Text='<%# Eval("BankCode") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Account number" Visible="true">
                        <ItemTemplate><asp:Label ID="lblAccountNumber" runat="server" Text='<%# Eval("AccountNumber") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Currency" Visible="true">
                        <ItemTemplate><asp:Label ID="lblCurrency" runat="server" Text='<%# Eval("Currency") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Transaction date" ItemStyle-CssClass="nowrap">
                        <ItemTemplate><asp:Label ID="lblTransactionDate" runat="server" Text='<%# Eval("TransactionDate","{0: dd-MM-yyyy}") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Amount" Visible="true" HeaderStyle-CssClass="num" ItemStyle-CssClass="num">
                        <ItemTemplate><asp:Label ID="lblTransactionAmount" runat="server" Text='<%# Eval("TransactionAmount") %>' /></ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </asp:Panel>
</div>
</asp:Content>
