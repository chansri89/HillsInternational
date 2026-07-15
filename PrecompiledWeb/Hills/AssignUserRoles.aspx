<%@ page title="" language="C#" masterpagefile="~/MasterPage1.Master" autoeventwireup="true" inherits="AssignUserRoles, App_Web_lkpdlk5o" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="tis-card" style="max-width:760px;">
    <h1 class="tis-page-title">
        <asp:Label ID="lblassignuserroles" runat="server" Text="Assign User Roles" />
    </h1>

    <asp:Panel ID="pnlUsers" runat="server" CssClass="XSmall">
        <div class="tis-field" style="max-width:360px;">
            <asp:Label ID="Label1" runat="server" Text="User Name" AssociatedControlID="ddlUserName" CssClass="tis-label" />
            <asp:DropDownList ID="ddlUserName" runat="server" DataTextField="EmployeeName" DataValueField="EmployeeCode"
                AutoPostBack="True" OnSelectedIndexChanged="ddlUserName_SelectedIndexChanged" />
        </div>
    </asp:Panel>

    <asp:Panel ID="pnlAssignRoles" runat="server" Visible="true">
        <div style="display:flex; flex-wrap:wrap; align-items:center; gap:var(--tis-space-4); margin-top:var(--tis-space-5);">
            <div class="tis-field" style="flex:1 1 220px;">
                <asp:Label ID="lblAvailableRoles" runat="server" ForeColor="Blue" Text="Available Roles" CssClass="tis-label" />
                <asp:ListBox ID="lstbxAvailableRole" runat="server" DataTextField="RoleName" DataValueField="AvRoleId"
                    Rows="8" TabIndex="10" style="width:100%; min-height:160px;" />
            </div>
            <div style="display:flex; flex-direction:column; gap:var(--tis-space-2);">
                <asp:Button ID="btnMove" runat="server" CausesValidation="False" CssClass="tis-btn-secondary" OnClick="btnMove_Click" TabIndex="11" Text=">" />
                <asp:Button ID="btnMoveFull" runat="server" CausesValidation="False" CssClass="tis-btn-secondary" OnClick="btnMoveFull_Click" TabIndex="12" Text=">>" Visible="False" />
                <asp:Button ID="btnRemove" runat="server" CausesValidation="False" CssClass="tis-btn-secondary" OnClick="btnRemove_Click" TabIndex="13" Text="<" />
                <asp:Button ID="btnRemoveFull" runat="server" CausesValidation="False" CssClass="tis-btn-secondary" OnClick="btnRemoveFull_Click" TabIndex="14" Text="<<" Visible="False" />
            </div>
            <div class="tis-field" style="flex:1 1 220px;">
                <asp:Label ID="lblAssignedRoles" runat="server" ForeColor="Blue" Text="Assigned Roles" CssClass="tis-label" />
                <asp:ListBox ID="lstbxAssignedRole" runat="server" DataTextField="RoleName" DataValueField="AsgRoleId"
                    Rows="8" TabIndex="15" style="width:100%; min-height:160px;" />
            </div>
        </div>
        <div class="tis-toolbar" style="margin-top:var(--tis-space-4);">
            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
        </div>
    </asp:Panel>

    <asp:Panel ID="pnlGetIds" runat="server" Visible="False">
        <asp:TextBox ID="txtUserId" runat="server" Visible="False" />
    </asp:Panel>
</div>
</asp:Content>
