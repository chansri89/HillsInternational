<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.master" AutoEventWireup="true" CodeFile="TenderIOWMappingCost.aspx.cs" Inherits="TenderIOWMappingCost" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="tis-page tis-page--narrow" data-density="compact">
    <div class="tis-page-header">
        <div class="tis-page-header__text">
            <span class="tis-eyebrow">Tender</span>
            <h1 class="tis-page-header__title"><asp:Label ID="lblStatMas" runat="server" Text="Tender IOW mapping cost" /></h1>
            <p class="tis-page-header__desc">Recompute the cost of every mapped tender item for a project, using the item rates of the chosen year, month and region.</p>
        </div>
    </div>
    <asp:Panel ID="Panel2" runat="server" />

    <asp:Panel ID="pnlAdd" runat="server" CssClass="tis-card" DefaultButton="btnUpdate">
        <div class="tis-card__header">
            <div class="tis-card__heading">
                <div class="tis-card__title">Rate year and month</div>
                <div class="tis-card__subtitle">Only the latest rate month and the month after it can be chosen.</div>
            </div>
        </div>
        <div class="tis-card__body">
            <div class="tis-form-grid tis-form-grid--2">
                <div class="tis-field">
                    <asp:Label ID="lblCompany" runat="server" Text="Company" AssociatedControlID="ddlCompany" CssClass="tis-label" />
                    <asp:DropDownList ID="ddlCompany" runat="server" DataTextField="CompanyName" DataValueField="CompanyId"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlCompanyChanged" />
                </div>
                <div class="tis-field">
                    <asp:Label ID="lblClients" runat="server" Text="Client" AssociatedControlID="ddlClient" CssClass="tis-label" />
                    <asp:DropDownList ID="ddlClient" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlClientChanged"
                        DataTextField="ClientName" DataValueField="ClientCode" />
                </div>
                <div class="tis-field tis-field--full">
                    <asp:Label ID="lblProject" runat="server" Text="Project" AssociatedControlID="ddlProject" CssClass="tis-label" />
                    <asp:DropDownList ID="ddlProject" runat="server" DataTextField="ProjectName" DataValueField="ClientProjectId" />
                </div>
                <div class="tis-field">
                    <asp:Label ID="lblRateMonth" runat="server" Text="Rate year month" AssociatedControlID="ddlForYearMonth" CssClass="tis-label" />
                    <asp:DropDownList ID="ddlForYearMonth" runat="server" MaxLength="8" DataTextField="ForYearMonth" DataValueField="ForYearMonth" />
                </div>
                <div class="tis-field">
                    <asp:Label ID="lblRegion" runat="server" Text="Region" AssociatedControlID="ddlRegion" CssClass="tis-label" />
                    <asp:DropDownList ID="ddlRegion" runat="server" DataTextField="Region" DataValueField="Region" />
                </div>
            </div>
        </div>
        <div class="tis-card__footer tis-card__footer--between">
            <span class="tis-help">Company, project, rate year month and region are all required.</span>
            <asp:Button ID="btnUpdate" runat="server" Text="Compute cost" OnClick="btnUpdate_Click" CssClass="tis-btn tis-btn--primary" />
        </div>
    </asp:Panel>
</div>
</asp:Content>
