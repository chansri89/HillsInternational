<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.Master" AutoEventWireup="true" CodeFile="Roles.aspx.cs" Inherits="Roles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="tis-page">
    <div class="tis-page-header">
        <div class="tis-page-header__text">
            <span class="tis-eyebrow">Admin</span>
            <h1 class="tis-page-header__title">Roles</h1>
            <p class="tis-page-header__desc">Role maintenance. Until this screen is ready, create and edit roles from Assign programs to roles.</p>
        </div>
    </div>

    <asp:Panel ID="Panel4" runat="server" CssClass="tis-card">
        <tis:EmptyState ID="emptyRoles" runat="server" Icon="flag" Title="This screen is being prepared"
            Text="Role maintenance is not available here yet." />
        <span class="tis-hidden"><asp:Label ID="lblWelcome" runat="server" Text="Roles on Under Construction" /></span>
    </asp:Panel>
</div>
</asp:Content>
