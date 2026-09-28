<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.master"  AutoEventWireup="true" CodeFile="CRAMIOWCostCalculation.aspx.cs" Inherits="CRAMIOWCostCalculation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="tis-page">
    <div class="tis-page-header">
        <div class="tis-page-header__text">
            <span class="tis-eyebrow">CRAM</span>
            <h1 class="tis-page-header__title"><asp:Label ID="lblExceptionList" runat="server" Text="IOW cost calculation" /></h1>
            <p class="tis-page-header__desc">Recalculate item-of-work costs from item rates for a company, month and region.</p>
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

    <asp:Panel ID="pnlPendind" runat="server" CssClass="tis-card">
        <div class="tis-card__header">
            <div class="tis-card__heading">
                <div class="tis-card__title">Calculation parameters</div>
                <div class="tis-card__subtitle">Choose the company, rate year and month, and region to calculate.</div>
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
                    <asp:Label ID="lblYear" runat="server" Text="Year" AssociatedControlID="txtYear" CssClass="tis-label" />
                    <asp:TextBox ID="txtYear" runat="server" Text="" MaxLength="4" placeholder="YYYY" />
                </div>
                <div class="tis-field">
                    <asp:Label ID="lblForMonth" runat="server" Text="Month" AssociatedControlID="ddlMonth" CssClass="tis-label" />
                    <asp:DropDownList ID="ddlMonth" runat="server" DataTextField="ForMonth" DataValueField="ForMonth">
                        <asp:ListItem Text="Select Pls" Value="0" />
                        <asp:ListItem Text="01" Value="01" />
                        <asp:ListItem Text="02" Value="02" />
                        <asp:ListItem Text="03" Value="03" />
                        <asp:ListItem Text="04" Value="04" />
                        <asp:ListItem Text="05" Value="05" />
                        <asp:ListItem Text="06" Value="06" />
                        <asp:ListItem Text="07" Value="07" />
                        <asp:ListItem Text="08" Value="08" />
                        <asp:ListItem Text="09" Value="09" />
                        <asp:ListItem Text="10" Value="10" />
                        <asp:ListItem Text="11" Value="11" />
                        <asp:ListItem Text="12" Value="12" />
                    </asp:DropDownList>
                </div>
                <div class="tis-field">
                    <asp:Label ID="lblRegion" runat="server" Text="Region" AssociatedControlID="ddlRegion" CssClass="tis-label" />
                    <asp:DropDownList ID="ddlRegion" runat="server" DataTextField="Region" DataValueField="Region" />
                </div>
            </div>
            <asp:TextBox ID="txtItem" runat="server" Visible="false" Text="" MaxLength="8" />
            <asp:DropDownList ID="ddlForYearMonth" runat="server" Visible="false" DataTextField="ForYearMonth" DataValueField="ForYearMonth" />
        </div>
        <div class="tis-card__footer tis-card__footer--between">
            <span class="tis-help">The year must be later than 2010. A message confirms when the calculation is done.</span>
            <span class="tis-cluster">
                <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" CssClass="tis-btn tis-btn--ghost" />
                <asp:Button ID="btnView" runat="server" OnClick="btnView_Click" Text="Calculate" CssClass="tis-btn tis-btn--primary" />
            </span>
        </div>
    </asp:Panel>

    <asp:Panel ID="pnlExList" runat="server" CssClass="tis-card tis-card--flush">
        <div class="tis-card__header">
            <div class="tis-card__heading">
                <div class="tis-card__title">Calculation output</div>
                <div class="tis-card__subtitle">Report output for the last calculation, when available.</div>
            </div>
        </div>
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
