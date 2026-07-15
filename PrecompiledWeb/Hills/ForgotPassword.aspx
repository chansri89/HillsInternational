<%@ page title="" language="C#" masterpagefile="~/MasterPage1.Master" autoeventwireup="true" inherits="ForgotPassword, App_Web_sx5sst5a" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="pnlForgotPwd" runat="server">
        <div class="tis-card" style="max-width:520px;">
            <h1 class="tis-page-title">Forgot Password</h1>
            <div class="tis-field">
                <label for="<%= txtUsername.ClientID %>">
                    <asp:Label ID="lblUserName" runat="server" Text="User Name" />
                </label>
                <asp:TextBox ID="txtUsername" runat="server" Placeholder="Enter your username" />
            </div>
            <div class="tis-toolbar" style="margin-top:var(--tis-space-4);">
                <asp:Button ID="btnGo" runat="server" onclick="btnGo_Click" Text="Go" />
            </div>
        </div>
    </asp:Panel>
</asp:Content>
