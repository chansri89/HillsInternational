<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.Master" AutoEventWireup="true" CodeFile="ForgotPassword.aspx.cs" Inherits="ForgotPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="tis-page tis-page--narrow">
    <div class="tis-page-header">
        <div class="tis-page-header__text">
            <span class="tis-eyebrow">Admin</span>
            <h1 class="tis-page-header__title">Reset a user's password</h1>
            <p class="tis-page-header__desc">For users who have forgotten their password. Enter their username and the result is shown once the reset runs.</p>
        </div>
    </div>

    <asp:Panel ID="pnlForgotPwd" runat="server" CssClass="tis-card">
        <div class="tis-card__body">
            <div class="tis-field">
                <asp:Label ID="lblUserName" runat="server" Text="Username" AssociatedControlID="txtUsername" CssClass="tis-label" />
                <asp:TextBox ID="txtUsername" runat="server" placeholder="Employee username" autocomplete="off" spellcheck="false" />
                <span class="tis-help">This is the employee code the user signs in with.</span>
            </div>
        </div>
        <div class="tis-card__footer">
            <asp:Button ID="btnGo" runat="server" OnClick="btnGo_Click" Text="Reset password" CssClass="tis-btn tis-btn--primary" />
        </div>
    </asp:Panel>
</div>
</asp:Content>
