<%@ page title="" language="C#" masterpagefile="~/MasterPage.master" autoeventwireup="true" inherits="Login, App_Web_sx5sst5a" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <h2 class="tis-login-heading">
        <asp:Label ID="lblLogin" runat="server" Text="Sign in" />
    </h2>

    <div class="tis-field">
        <label for="<%= txtUsername.ClientID %>">
            <asp:Label ID="lblUserName" runat="server" Text="Username" />
        </label>
        <asp:TextBox ID="txtUsername" runat="server" Placeholder="Enter your username" />
    </div>

    <div class="tis-field">
        <label for="<%= txtPwd.ClientID %>">
            <asp:Label ID="lblPwd" runat="server" Text="Password" />
        </label>
        <asp:TextBox ID="txtPwd" runat="server" TextMode="Password" Placeholder="Enter your password" />
    </div>

    <div class="tis-login-actions">
        <asp:Button ID="btnLogin" runat="server" OnClick="btnLogin_Click" Text="Login" />
        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="lnkForgot_Click">Forgot Password?</asp:LinkButton>
    </div>

</asp:Content>
