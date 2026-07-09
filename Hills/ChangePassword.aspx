<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.Master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="tis-card" style="max-width:520px;">
        <h1 class="tis-page-title">
            <asp:Label ID="lblchangepassword" runat="server" Text="Change Password" />
        </h1>

        <div class="tis-field">
            <label for="<%= txtOldPassword.ClientID %>">
                <asp:Label ID="lblOldPassword" runat="server" Text="Old Password" />
            </label>
            <asp:TextBox id="txtOldPassword" runat="server" TextMode="Password" MaxLength="20"
                AutoCompleteType="Disabled" TabIndex="1" ToolTip="Enter minimum 6 characters" />
        </div>

        <div class="tis-field">
            <label for="<%= txtNewPassword.ClientID %>">
                <asp:Label ID="lblNewPassword" runat="server" Text="New Password" />
            </label>
            <asp:TextBox id="txtNewPassword" runat="server" TextMode="Password" MaxLength="20"
                AutoCompleteType="Disabled" TabIndex="2" ToolTip="Enter minimum 6 characters" />
        </div>

        <div class="tis-field">
            <label for="<%= txtConfirmPassword.ClientID %>">
                <asp:Label ID="lblConfirmPassword" runat="server" Text="Confirm Password" />
            </label>
            <asp:TextBox id="txtConfirmPassword" tabIndex="3" runat="server" TextMode="Password"
                MaxLength="20" AutoCompleteType="Disabled" ToolTip="Enter minimum 6 characters" />
        </div>

        <div class="tis-toolbar" style="margin-top:var(--tis-space-4);">
            <asp:Button id="btnSave" tabIndex="4" onclick="btnSave_Click" runat="server" Text="Save" ToolTip="Save" />
        </div>
    </div>
</asp:Content>
