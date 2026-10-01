<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.Master" AutoEventWireup="true" CodeFile="AssignUserRoles.aspx.cs" Inherits="AssignUserRoles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="tis-page tis-page--medium">
    <div class="tis-page-header">
        <div class="tis-page-header__text">
            <span class="tis-eyebrow">Admin</span>
            <h1 class="tis-page-header__title"><asp:Label ID="lblassignuserroles" runat="server" Text="Assign user roles" /></h1>
            <p class="tis-page-header__desc">Choose a user, move roles between the two lists, then save to update what that user can open.</p>
        </div>
    </div>

    <asp:Panel ID="pnlUsers" runat="server" CssClass="tis-card">
        <div class="tis-card__body">
            <div class="tis-toolbar">
                <div class="tis-field tis-toolbar__grow">
                    <asp:Label ID="Label1" runat="server" Text="User" AssociatedControlID="ddlUserName" CssClass="tis-label" />
                    <asp:DropDownList ID="ddlUserName" runat="server" DataTextField="EmployeeName" DataValueField="EmployeeCode"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlUserName_SelectedIndexChanged" />
                </div>
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="pnlAssignRoles" runat="server" Visible="true" CssClass="tis-card">
        <div class="tis-card__header">
            <div class="tis-card__heading">
                <div class="tis-card__title">Roles</div>
                <div class="tis-card__subtitle">Select a role and use the arrow buttons to move it.</div>
            </div>
        </div>
        <div class="tis-card__body">
            <div class="tis-duallist">
                <div class="tis-field">
                    <asp:Label ID="lblAvailableRoles" runat="server" Text="Available roles" AssociatedControlID="lstbxAvailableRole" CssClass="tis-label" />
                    <asp:ListBox ID="lstbxAvailableRole" runat="server" DataTextField="RoleName" DataValueField="AvRoleId"
                        Rows="8" TabIndex="10" />
                </div>
                <div class="tis-duallist__moves">
                    <asp:Button ID="btnMove" runat="server" CausesValidation="False" CssClass="tis-btn" OnClick="btnMove_Click" TabIndex="11" Text=">" ToolTip="Assign the selected role" />
                    <asp:Button ID="btnMoveFull" runat="server" CausesValidation="False" CssClass="tis-btn" OnClick="btnMoveFull_Click" TabIndex="12" Text=">>" Visible="False" ToolTip="Assign all roles" />
                    <asp:Button ID="btnRemove" runat="server" CausesValidation="False" CssClass="tis-btn" OnClick="btnRemove_Click" TabIndex="13" Text="<" ToolTip="Remove the selected role" />
                    <asp:Button ID="btnRemoveFull" runat="server" CausesValidation="False" CssClass="tis-btn" OnClick="btnRemoveFull_Click" TabIndex="14" Text="<<" Visible="False" ToolTip="Remove all roles" />
                </div>
                <div class="tis-field">
                    <asp:Label ID="lblAssignedRoles" runat="server" Text="Assigned roles" AssociatedControlID="lstbxAssignedRole" CssClass="tis-label" />
                    <asp:ListBox ID="lstbxAssignedRole" runat="server" DataTextField="RoleName" DataValueField="AsgRoleId"
                        Rows="8" TabIndex="15" />
                </div>
            </div>
        </div>
        <div class="tis-card__footer tis-card__footer--between">
            <span class="tis-help">Save stores the assigned list for the selected user.</span>
            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="tis-btn tis-btn--primary" />
        </div>
    </asp:Panel>

    <asp:Panel ID="pnlGetIds" runat="server" Visible="False">
        <asp:TextBox ID="txtUserId" runat="server" Visible="False" />
    </asp:Panel>
</div>
</asp:Content>
