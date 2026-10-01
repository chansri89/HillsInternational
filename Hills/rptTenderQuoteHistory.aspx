<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.master"  AutoEventWireup="true" CodeFile="rptTenderQuoteHistory.aspx.cs" Inherits="rptTenderQuoteHistory" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="tis-page">
    <div class="tis-page-header">
        <div class="tis-page-header__text">
            <span class="tis-eyebrow">Reports</span>
            <h1 class="tis-page-header__title"><asp:Label ID="lblExceptionList" runat="server" Text="Tender quote history" /></h1>
            <p class="tis-page-header__desc">Quote history for each tender package of a project.</p>
        </div>
    </div>

    <div class="tis-hidden" aria-hidden="true">
        <asp:UpdateProgress ID="UpdateProgress" runat="server">
            <ProgressTemplate>
                <asp:Image ID="imgprocess" ImageUrl="~/Images/progressBar.gif" AlternateText="Processing" runat="server" />
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:modalpopupextender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
            PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
    </div>

    <asp:Panel ID="pnlPendind" runat="server" CssClass="tis-card" DefaultButton="btnView">
        <div class="tis-card__header">
            <div class="tis-card__heading">
                <div class="tis-card__title">Report parameters</div>
                <div class="tis-card__subtitle">Choose a company, client and project, then view the report.</div>
            </div>
        </div>
        <div class="tis-card__body">
            <div class="tis-form-grid tis-form-grid--4 tis-report-filters">
                <div class="tis-field">
                    <asp:Label ID="lblCompany" runat="server" Text="Company" AssociatedControlID="ddlCompany" CssClass="tis-label" />
                    <asp:DropDownList ID="ddlCompany" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCompanyChanged"
                        DataTextField="CompanyName" DataValueField="CompanyId" />
                </div>
                <div class="tis-field">
                    <asp:Label ID="lblClients" runat="server" Text="Client" AssociatedControlID="ddlClient" CssClass="tis-label" />
                    <asp:DropDownList ID="ddlClient" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlClientChanged"
                        DataTextField="ClientName" DataValueField="ClientCode" />
                </div>
                <div class="tis-field">
                    <asp:Label ID="lblProject" runat="server" Text="Project" AssociatedControlID="ddlProject" CssClass="tis-label" />
                    <asp:DropDownList ID="ddlProject" runat="server" DataTextField="ProjectName" DataValueField="ProjectCode" />
                </div>
                <div class="tis-field tis-report-filter-actions">
                    <span class="tis-cluster">
                        <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" CssClass="tis-btn tis-btn--ghost" />
                        <asp:Button ID="btnView" runat="server" OnClick="btnView_Click" Text="View report" CssClass="tis-btn tis-btn--primary" />
                    </span>
                </div>
            </div>
            <asp:RadioButtonList ID="rbtType" runat="server" Visible="false" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="tis-segmented">
                <asp:ListItem Text="Cost only" Selected="true" Value="1" />
            </asp:RadioButtonList>
            <asp:TextBox ID="txtItem" runat="server" Visible="false" Text="" MaxLength="8" />
        </div>
        <div class="tis-card__footer tis-card__footer--between">
            <span class="tis-help"><asp:Label ID="Label1" runat="server" Text="The report shows data once quotes have been received and values are available." /></span>
        </div>
    </asp:Panel>

    <asp:Panel ID="pnlExList" runat="server" CssClass="tis-card tis-card--flush">
        <div class="tis-report">
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Inter, Segoe UI, Verdana" Font-Size="8pt"
                Width="100%" Height="680px" InteractiveDeviceInfos="(Collection)"
                WaitMessageFont-Names="Inter, Segoe UI, Verdana" WaitMessageFont-Size="12pt"
                ShowFindControls="false" ShowBackButton="false" ShowPageNavigationControls="true"
                ShowPrintButton="false" ShowRefreshButton="false">
                <LocalReport ReportPath="">
                </LocalReport>
            </rsweb:ReportViewer>
        </div>
    </asp:Panel>
</div>
</asp:Content>
