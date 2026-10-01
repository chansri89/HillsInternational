<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.Master" AutoEventWireup="true" CodeFile="RoleNameChange.aspx.cs" Inherits="RoleNameChange" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="tis-page tis-page--narrow">
    <div class="tis-page-header">
        <div class="tis-page-header__text">
            <span class="tis-eyebrow">Admin</span>
            <h1 class="tis-page-header__title"><asp:Label ID="lblrolenamechange" runat="server" Text="Role Name Change" /></h1>
            <p class="tis-page-header__desc">Rename the roles that group user permissions. Renaming does not change what a role can do.</p>
        </div>
    </div>

    <asp:Panel ID="Pnlgv" runat="server" CssClass="tis-card">
        <div class="tis-card__header">
            <div class="tis-card__heading">
                <div class="tis-card__title">Roles</div>
                <div class="tis-card__subtitle">Use Edit on a row to rename it in place.</div>
            </div>
        </div>
        <div class="tis-table-wrap tis-table-wrap--tall">
            <asp:GridView ID="GrdRole" runat="server" AutoGenerateColumns="False"
                OnRowCancelingEdit="GrdRole_RowCancelingEdit"
                OnRowEditing="GrdRole_RowEditing"
                OnRowUpdating="GrdRole_RowUpdating"
                OnRowDeleting="GrdRole_RowDeleting">
                <Columns>
                    <asp:TemplateField HeaderText="RoleId" Visible="False">
                        <ItemTemplate><asp:Label ID="lblRoleId" runat="server" Text='<%# Eval("RoleId") %>' /></ItemTemplate>
                        <EditItemTemplate><asp:TextBox ID="txtRoleId" runat="server" Text='<%# Bind("RoleId") %>' ReadOnly="true" /></EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Role name" ItemStyle-CssClass="strong">
                        <ItemTemplate><asp:Label ID="lblRoleName" runat="server" Text='<%# Eval("RoleName") %>' /></ItemTemplate>
                        <EditItemTemplate><asp:TextBox ID="txtRoleName" runat="server" Text='<%# Bind("RoleName") %>' /></EditItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField HeaderText="Edit" ShowEditButton="True" ItemStyle-CssClass="actions" />
                    <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" Visible="False" ItemStyle-CssClass="actions" />
                </Columns>
                <EmptyDataTemplate>
                    <tis:EmptyState ID="emptyRoles" runat="server" Icon="shield" Title="No roles found"
                        Text="Roles are created by the administrator; none are available yet." />
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
    </asp:Panel>
</div>
</asp:Content>
