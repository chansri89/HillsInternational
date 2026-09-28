<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Login.aspx.cs" Inherits="Login" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pnlLogin" runat="server" CssClass="tis-auth__stack" DefaultButton="btnLogin">
        <div>
            <h2 class="tis-auth__heading"><asp:Label ID="lblLogin" runat="server" Text="Sign in" /></h2>
            <p class="tis-auth__sub">Use your Hills TIS username and password.</p>
        </div>

        <div class="tis-field">
            <asp:Label ID="lblUserName" runat="server" Text="Username" AssociatedControlID="txtUsername" CssClass="tis-label" />
            <asp:TextBox ID="txtUsername" runat="server" placeholder="Enter your username" autocomplete="username" spellcheck="false" />
        </div>

        <div class="tis-field">
            <div class="tis-auth__row">
                <asp:Label ID="lblPwd" runat="server" Text="Password" AssociatedControlID="txtPwd" CssClass="tis-label" />
                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="lnkForgot_Click" CssClass="tis-link tis-text-sm">Forgot password?</asp:LinkButton>
            </div>
            <asp:TextBox ID="txtPwd" runat="server" TextMode="Password" placeholder="Enter your password" autocomplete="current-password" />
        </div>

        <asp:Button ID="btnLogin" runat="server" OnClick="btnLogin_Click" Text="Sign in" CssClass="tis-btn tis-btn--primary tis-btn--block" />

        <p class="tis-auth__foot">Forgot your password? Enter your username first, then choose Forgot password and it will be emailed to you.</p>
    </asp:Panel>
</asp:Content>
