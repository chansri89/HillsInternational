<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.Master" AutoEventWireup="true" CodeFile="LoginUser.aspx.cs" Inherits="LoginUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="tis-page">
    <div class="tis-page-header">
        <div class="tis-page-header__text">
            <span class="tis-eyebrow">Admin</span>
            <h1 class="tis-page-header__title"><asp:Label ID="lblempoyeemaster" runat="server" Text="Login Employee Master" /></h1>
            <p class="tis-page-header__desc">Sign-in accounts, their location and department, and admin rights. Passwords can only be set when a user is created.</p>
        </div>
    </div>

    <div class="tis-split">
        <asp:Panel ID="Pnlgv" runat="server" CssClass="tis-card tis-split__list">
            <div class="tis-card__header">
                <div class="tis-card__heading">
                    <div class="tis-card__title">Users</div>
                    <div class="tis-card__subtitle">Select a user name to edit it.</div>
                </div>
                <asp:Button ID="btnNew" runat="server" Text="New user" CssClass="tis-btn tis-btn--primary tis-btn--sm"
                    OnClick="btnNew_Click" CausesValidation="false" />
            </div>
            <div class="tis-table-wrap tis-table-wrap--tall">
                <asp:GridView ID="GrdEmployeeMaster" runat="server" AutoGenerateColumns="False" OnRowCommand="GrdLoginUser_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="Id" Visible="true" HeaderStyle-CssClass="num" ItemStyle-CssClass="num">
                            <ItemTemplate><asp:Label ID="lblLoginUserId" runat="server" Text='<%# Eval("LoginUserId") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="User name">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkLoginUserId" runat="server" CssClass="tis-link tis-link--strong" CommandArgument='<%#Eval("LoginUserId")%>'
                                    CommandName="selectUserName" Text='<%#Eval("UserName") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Location">
                            <ItemTemplate><asp:Label ID="lblCompanyName" runat="server" Text='<%# Eval("CompanyName") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Department">
                            <ItemTemplate><asp:Label ID="lblDepartmentName" runat="server" Text='<%# Eval("DepartmentName") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Email">
                            <ItemTemplate><asp:Label ID="lblEmailId" runat="server" Text='<%# Eval("EmailId") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Admin" Visible="true" HeaderStyle-CssClass="center" ItemStyle-CssClass="center">
                            <ItemTemplate><asp:CheckBox ID="chkAdmin" runat="server" CssClass="tis-status tis-status--yes" Checked='<%# Eval("IsAdmin") %>' Enabled="false" /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Super user" HeaderStyle-CssClass="center" ItemStyle-CssClass="center">
                            <ItemTemplate><asp:CheckBox ID="chklblSuperUser" runat="server" CssClass="tis-status tis-status--yes" Checked='<%# Eval("IsSuperUser") %>' Enabled="false" /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate><asp:CheckBox ID="chkActive" runat="server" CssClass="tis-status" Checked='<%# Eval("IsActive") %>' Enabled="false" /></ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <tis:EmptyState ID="emptyUsers" runat="server" Icon="key" Title="No users yet"
                            Text="Use New user to create the first sign-in account." />
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </asp:Panel>

        <asp:Panel ID="pnlAdd" runat="server" GroupingText="Add User" CssClass="tis-card tis-card--legend tis-split__detail" DefaultButton="btnSave">
            <div class="tis-card__body">
                <div class="tis-form-grid tis-form-grid--2">
                    <div class="tis-field tis-field--full">
                        <asp:Label ID="lblUserName" runat="server" Text="User name" AssociatedControlID="txtUserName" CssClass="tis-label" />
                        <asp:TextBox ID="txtUserName" runat="server" />
                    </div>
                    <div class="tis-field tis-field--full">
                        <asp:Label ID="lblUserPassword" runat="server" Text="Password" AssociatedControlID="txtUserPassword" CssClass="tis-label" />
                        <asp:TextBox ID="txtUserPassword" runat="server" TextMode="Password" />
                    </div>
                    <div class="tis-field tis-field--full">
                        <asp:Label ID="lblEmailid" runat="server" Text="Email" AssociatedControlID="txtEmailId" CssClass="tis-label" />
                        <asp:TextBox ID="txtEmailId" runat="server" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" CssClass="tis-field-error"
                            ControlToValidate="txtEmailId" ErrorMessage="Enter EmailId in Correct Format"
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                    </div>
                    <div class="tis-field">
                        <asp:Label ID="lblCompanyName" runat="server" Text="Location" AssociatedControlID="ddlCompanyName" CssClass="tis-label" />
                        <asp:DropDownList ID="ddlCompanyName" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCompanyChanged" />
                    </div>
                    <div class="tis-field">
                        <asp:Label ID="lblDepartmentName" runat="server" Text="Department" AssociatedControlID="ddlDepartName" CssClass="tis-label" />
                        <asp:DropDownList ID="ddlDepartName" runat="server" />
                    </div>
                    <div class="tis-field tis-field--check">
                        <asp:CheckBox ID="ChkIsCompanyAdmin" runat="server" Text="Admin" Visible="True" />
                    </div>
                    <div class="tis-field tis-field--check">
                        <asp:CheckBox ID="ChkIssuperUser" runat="server" Text="Super User" Visible="True" />
                    </div>
                    <div class="tis-field tis-field--check">
                        <asp:CheckBox ID="chkIsActive" runat="server" Text="IsActive" Visible="False" />
                    </div>
                </div>
                <asp:TextBox ID="txtLoginUserId" runat="server" Visible="false" />
            </div>
            <div class="tis-card__footer">
                <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" CssClass="tis-btn tis-btn--primary" />
            </div>
        </asp:Panel>
    </div>
</div>
</asp:Content>
