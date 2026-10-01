<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.Master" AutoEventWireup="true" CodeFile="EmployeeMaster.aspx.cs" Inherits="EmployeeMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="tis-page">
    <span class="tis-hidden"><asp:Label ID="lblSeparator" runat="server" /><asp:Label ID="lblSeparator1" runat="server" /></span>
    <div class="tis-page-header">
        <div class="tis-page-header__text">
            <span class="tis-eyebrow">Masters</span>
            <h1 class="tis-page-header__title"><asp:Label ID="lblempoyeemaster" runat="server" Text="Employee Master" /></h1>
            <p class="tis-page-header__desc">Employees who can sign in, with their location, manager and designation.</p>
        </div>
    </div>

    <div class="tis-split">
        <asp:Panel ID="Pnlgv" runat="server" CssClass="tis-card tis-split__list">
            <div class="tis-card__header">
                <div class="tis-card__heading">
                    <div class="tis-card__title">Employees</div>
                    <div class="tis-card__subtitle">Use Edit on a row to change it in place.</div>
                </div>
                <asp:Button ID="btnNew" runat="server" Text="New employee" CssClass="tis-btn tis-btn--primary tis-btn--sm"
                    OnClick="btnNew_Click" CausesValidation="false" />
            </div>
            <div class="tis-table-wrap tis-table-wrap--tall">
                <asp:GridView ID="GrdEmployeeMaster" runat="server" AutoGenerateColumns="False"
                    OnRowCancelingEdit="GrdEmployeeMaster_RowCancelingEdit"
                    OnRowDeleting="GrdEmployeeMaster_RowDeleting"
                    OnRowEditing="GrdEmployeeMaster_RowEditing"
                    OnRowUpdating="GrdEmployeeMaster_RowUpdating">
                    <Columns>
                        <asp:TemplateField HeaderText="Code" ItemStyle-CssClass="code">
                            <ItemTemplate><asp:Label ID="lblEmployeeCode" runat="server" Text='<%# Eval("EmployeeCode") %>' /></ItemTemplate>
                            <EditItemTemplate><asp:TextBox ID="txtEmployeeCode" runat="server" Text='<%# Bind("EmployeeCode") %>' ReadOnly="true" /></EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Employee" ItemStyle-CssClass="strong">
                            <ItemTemplate><asp:Label ID="lblEmployeeName" runat="server" Text='<%# Eval("EmployeeName") %>' /></ItemTemplate>
                            <EditItemTemplate><asp:TextBox ID="txtEmpName" runat="server" Text='<%# Bind("EmployeeName") %>' /></EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Email">
                            <ItemTemplate><asp:Label ID="lblEmailId" runat="server" Text='<%# Eval("EmailId") %>' /></ItemTemplate>
                            <EditItemTemplate><asp:TextBox ID="txtEmailId" runat="server" Text='<%# Bind("EmailId") %>' /></EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Location">
                            <ItemTemplate><asp:Label ID="lblCompanyName" runat="server" Text='<%# Eval("CompanyName") %>' /></ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlCompanyName" runat="server" DataValueField="CompanyCode" DataTextField="CompanyName" DataSource='<%#getCompanyName() %>' />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Manager">
                            <ItemTemplate><asp:Label ID="lblManagerName" runat="server" Text='<%# Eval("ManagerName") %>' /></ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlManagerName" runat="server" DataValueField="EmployeeCode" DataTextField="EmployeeName" DataSource='<%#getManagerName() %>' />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Designation">
                            <ItemTemplate><asp:Label ID="lblEmployeeDesignation" runat="server" Text='<%# Eval("EmployeeDesignation") %>' /></ItemTemplate>
                            <EditItemTemplate><asp:TextBox ID="txtEmployeeDesignation" runat="server" Text='<%# Bind("EmployeeDesignation") %>' /></EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Auditor" Visible="false">
                            <ItemTemplate><asp:CheckBox ID="chkAuditor" runat="server" CssClass="tis-status tis-status--yes" Checked='<%# Eval("IsAuditor") %>' Enabled="false" /></ItemTemplate>
                            <EditItemTemplate><asp:CheckBox ID="chkIsAuditor" runat="server" Checked='<%# Bind("IsAuditor") %>' /></EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Company admin" HeaderStyle-CssClass="center" ItemStyle-CssClass="center">
                            <ItemTemplate><asp:CheckBox ID="chkCompanyAdmin" runat="server" CssClass="tis-status tis-status--yes" Checked='<%# Eval("IsCompanyAdmin") %>' Enabled="false" /></ItemTemplate>
                            <EditItemTemplate><asp:CheckBox ID="chkCompanyAdmin" runat="server" Checked='<%# Bind("IsCompanyAdmin") %>' /></EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate><asp:CheckBox ID="chkActive" runat="server" CssClass="tis-status" Checked='<%# Eval("IsActive") %>' Enabled="false" /></ItemTemplate>
                            <EditItemTemplate><asp:CheckBox ID="chkIsActive" runat="server" Text="Active" Checked='<%# Bind("IsActive") %>' /></EditItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField HeaderText="Edit" ShowEditButton="True" CausesValidation="False" ItemStyle-CssClass="actions" />
                    </Columns>
                    <EmptyDataTemplate>
                        <tis:EmptyState ID="emptyEmployees" runat="server" Icon="user" Title="No employees yet"
                            Text="Use New employee to add the first employee." />
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </asp:Panel>

        <asp:Panel ID="pnlAdd" runat="server" CssClass="tis-card tis-split__detail" DefaultButton="Button1">
            <div class="tis-card__header">
                <div class="tis-card__heading">
                    <div class="tis-card__title">Add employee</div>
                    <div class="tis-card__subtitle">All fields except manager are required.</div>
                </div>
            </div>
            <div class="tis-card__body">
                <div class="tis-form-grid tis-form-grid--2">
                    <div class="tis-field">
                        <asp:Label ID="lblEmployeeCode" runat="server" Text="Employee code" AssociatedControlID="txtEmployeeCode" CssClass="tis-label" />
                        <asp:TextBox ID="txtEmployeeCode" runat="server" />
                    </div>
                    <div class="tis-field">
                        <asp:Label ID="lblUserPassword" runat="server" Text="Password" AssociatedControlID="txtPassword" CssClass="tis-label" />
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" />
                    </div>
                    <div class="tis-field tis-field--full">
                        <asp:Label ID="lblEmpname" runat="server" Text="Employee name" AssociatedControlID="txtEmpname" CssClass="tis-label" />
                        <asp:TextBox ID="txtEmpname" runat="server" />
                    </div>
                    <div class="tis-field tis-field--full">
                        <asp:Label ID="lblEmailid" runat="server" Text="Email" AssociatedControlID="txtEmailId" CssClass="tis-label" />
                        <asp:TextBox ID="txtEmailId" runat="server" />
                    </div>
                    <div class="tis-field tis-field--full">
                        <asp:Label ID="lblCompanyName" runat="server" Text="Location" AssociatedControlID="ddlCompanyName" CssClass="tis-label" />
                        <asp:DropDownList ID="ddlCompanyName" runat="server" />
                    </div>
                    <div class="tis-field tis-field--full">
                        <asp:Label ID="lblManagerName" runat="server" Text="Manager" AssociatedControlID="ddlManagerName" CssClass="tis-label" />
                        <asp:DropDownList ID="ddlManagerName" runat="server" />
                    </div>
                    <div class="tis-field tis-field--full">
                        <asp:Label ID="lblEmployeeDesignation" runat="server" Text="Designation" AssociatedControlID="txtEmployeeDesignation" CssClass="tis-label" />
                        <asp:TextBox ID="txtEmployeeDesignation" runat="server" />
                    </div>
                    <div class="tis-field tis-field--check">
                        <asp:CheckBox ID="ChkIsCompanyAdmin" runat="server" Text="Company admin" Visible="True" />
                    </div>
                    <div class="tis-field tis-field--check">
                        <asp:CheckBox ID="ChkIsAuditor" runat="server" Text="IsAuditor" Visible="false"
                            OnCheckedChanged="ChkIsAuditor_CheckedChanged" AutoPostBack="True" />
                        <asp:CheckBox ID="chkIsActive" runat="server" Text="IsActive" Visible="False" />
                    </div>
                </div>
                <asp:HiddenField ID="HidDeleteCount" Value="0" runat="server" />
                <asp:HiddenField ID="HidUpdateCount" Value="0" runat="server" />
            </div>
            <div class="tis-card__footer">
                <asp:Button ID="Button1" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="tis-btn tis-btn--primary" />
            </div>
        </asp:Panel>
    </div>
</div>
</asp:Content>
