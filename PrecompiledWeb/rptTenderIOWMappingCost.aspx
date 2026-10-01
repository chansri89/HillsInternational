<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.master" AutoEventWireup="true" CodeFile="rptTenderIOWMappingCost.aspx.cs" Inherits="rptTenderIOWMappingCost" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="tis-page">
    <div class="tis-page-header">
        <div class="tis-page-header__text">
            <span class="tis-eyebrow">Reports</span>
            <h1 class="tis-page-header__title"><asp:Label ID="lblStatMas" runat="server" Text="Tender IOW mapping cost listing" /></h1>
            <p class="tis-page-header__desc">Mapped item-of-work costs between two rate months, for the latest tender package only.</p>
        </div>
    </div>

    <div class="tis-hidden" aria-hidden="true">
        <asp:Panel ID="Panel2" runat="server" />
    </div>

    <asp:Panel ID="pnlAdd" runat="server" CssClass="tis-card" DefaultButton="btnGo">
        <div class="tis-card__header">
            <div class="tis-card__heading">
                <div class="tis-card__title">Report parameters</div>
                <div class="tis-card__subtitle">Choose a company, client and project, then the region and rate period.</div>
            </div>
        </div>
        <div class="tis-card__body">
            <div class="tis-form-grid tis-form-grid--4">
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
                    <asp:DropDownList ID="ddlProject" runat="server" DataTextField="ProjectName" DataValueField="ClientProjectId" />
                </div>
                <div class="tis-field">
                    <asp:Label ID="lblRegion" runat="server" Text="Region" AssociatedControlID="ddlRegion" CssClass="tis-label" />
                    <asp:DropDownList ID="ddlRegion" runat="server" DataTextField="Region" DataValueField="Region" />
                </div>
                <div class="tis-field">
                    <asp:Label ID="lblfromRateMonth" runat="server" Text="From year month" AssociatedControlID="ddlFromYearMonth" CssClass="tis-label" />
                    <asp:DropDownList ID="ddlFromYearMonth" runat="server" MaxLength="8" DataTextField="ForYearMonth" DataValueField="ForYearMonth" />
                </div>
                <div class="tis-field">
                    <asp:Label ID="lblTOYearMonth" runat="server" Text="To year month" AssociatedControlID="ddlToYearMonth" CssClass="tis-label" />
                    <asp:DropDownList ID="ddlToYearMonth" runat="server" MaxLength="8" DataTextField="ForYearMonth" DataValueField="ForYearMonth" />
                </div>
                <div class="tis-field">
                    <span class="tis-label">Report type</span>
                    <asp:RadioButtonList ID="rdbtnSummary" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="tis-segmented">
                        <asp:ListItem Selected="true" Value="1" Text="Detail" />
                        <asp:ListItem Selected="false" Value="2" Text="Summary" />
                    </asp:RadioButtonList>
                </div>
            </div>
        </div>
        <div class="tis-card__footer tis-card__footer--between">
            <span class="tis-help">Only the latest package of the project is included.</span>
            <span class="tis-cluster">
                <asp:Button ID="btnGo" runat="server" Text="View report" OnClick="btnGo_Click" CssClass="tis-btn tis-btn--primary" />
            </span>
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
