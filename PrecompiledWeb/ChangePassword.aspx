<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.Master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="tis-page tis-page--narrow">
    <div class="tis-page-header">
        <div class="tis-page-header__text">
            <span class="tis-eyebrow">Admin</span>
            <h1 class="tis-page-header__title"><asp:Label ID="lblchangepassword" runat="server" Text="Change password" /></h1>
            <p class="tis-page-header__desc">Confirm your current password, then choose a new one for signing in to Hills TIS.</p>
        </div>
    </div>

    <div class="tis-card">
        <div class="tis-card__header">
            <div class="tis-card__heading">
                <div class="tis-card__title">Your password</div>
                <div class="tis-card__subtitle">Passwords can be up to 20 characters.</div>
            </div>
        </div>
        <div class="tis-card__body">
            <div class="tis-form-grid tis-form-grid--1">
                <div class="tis-field">
                    <asp:Label ID="lblOldPassword" runat="server" Text="Current password" AssociatedControlID="txtOldPassword" CssClass="tis-label" />
                    <asp:TextBox ID="txtOldPassword" runat="server" TextMode="Password" MaxLength="20"
                        AutoCompleteType="Disabled" TabIndex="1" ToolTip="Enter minimum 6 characters" />
                </div>
                <div class="tis-field">
                    <asp:Label ID="lblNewPassword" runat="server" Text="New password" AssociatedControlID="txtNewPassword" CssClass="tis-label" />
                    <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" MaxLength="20"
                        AutoCompleteType="Disabled" TabIndex="2" ToolTip="Enter minimum 6 characters" />
                    <span class="tis-help">Use at least 6 characters.</span>
                </div>
                <div class="tis-field">
                    <asp:Label ID="lblConfirmPassword" runat="server" Text="Confirm new password" AssociatedControlID="txtConfirmPassword" CssClass="tis-label" />
                    <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" MaxLength="20"
                        AutoCompleteType="Disabled" TabIndex="3" ToolTip="Enter minimum 6 characters" />
                </div>
            </div>
        </div>
        <div class="tis-card__footer">
            <asp:Button ID="btnSave" runat="server" Text="Save" ToolTip="Save" OnClick="btnSave_Click" TabIndex="4" CssClass="tis-btn tis-btn--primary" />
        </div>
    </div>
</div>
</asp:Content>
