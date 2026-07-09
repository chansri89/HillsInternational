<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.master" AutoEventWireup="true" CodeFile="TenderIOWMappingCost.aspx.cs" Inherits="TenderIOWMappingCost" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="Panel2" runat="server" CssClass="XSmall"> </asp:Panel>
<h1 class="tis-page-title">
    <asp:Label ID="lblStatMas" runat="server" Text="Tender-IOW Mapping Cost for Selected Year Month" />
</h1>
<div class="tis-card">
    <asp:panel ID="pnlAdd" runat="server" GroupingText="Select Rate Year Month" CssClass="XXSmall">
    <div class="tis-form-grid">
        <div class="tis-field">
            <asp:Label ID="lblCompany" runat="server" Text="Company" AssociatedControlID="ddlCompany" CssClass="tis-label" />
            <asp:DropDownList ID="ddlCompany" runat="server" DataTextField="CompanyName" DataValueField="CompanyId"
                AutoPostBack="true" OnSelectedIndexChanged="ddlCompanyChanged" />
        </div>
        <div class="tis-field">
            <asp:Label ID="lblClients" runat="server" Text="Clients" AssociatedControlID="ddlClient" CssClass="tis-label" />
            <asp:DropDownList ID="ddlClient" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlClientChanged"
                DataTextField="ClientName" DataValueField="ClientCode" />
        </div>
        <div class="tis-field">
            <asp:Label ID="lblProject" runat="server" Text="Project" AssociatedControlID="ddlProject" CssClass="tis-label" />
            <asp:DropDownList ID="ddlProject" runat="server" DataTextField="ProjectName" DataValueField="ClientProjectId" />
        </div>
        <div class="tis-field">
            <asp:Label ID="lblRateMonth" runat="server" Text="Rate Year Month" AssociatedControlID="ddlForYearMonth" CssClass="tis-label" />
            <asp:DropDownList ID="ddlForYearMonth" runat="server" MaxLength="8" DataTextField="ForYearMonth" DataValueField="ForYearMonth" />
        </div>
        <div class="tis-field">
            <asp:Label ID="lblRegion" runat="server" Text="Region" AssociatedControlID="ddlRegion" CssClass="tis-label" />
            <asp:DropDownList ID="ddlRegion" runat="server" DataTextField="Region" DataValueField="Region" />
        </div>
    </div>
    <div class="tis-toolbar" style="margin-top:var(--tis-space-4);">
        <asp:Button ID="btnUpdate" runat="server" Text="Update" onclick="btnUpdate_Click" />
    </div>
    </asp:panel>
    </div>

</asp:Content>

