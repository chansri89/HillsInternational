<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.master" AutoEventWireup="true" CodeFile="ItemCategoryMaster.aspx.cs" Inherits="ItemCategoryMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="tis-page">
    <asp:Panel ID="Panel2" runat="server" CssClass="tis-hidden"></asp:Panel>
    <div class="tis-page-header">
        <div class="tis-page-header__text">
            <span class="tis-eyebrow">Item Masters</span>
            <h1 class="tis-page-header__title"><asp:Label ID="lblStatMas" runat="server" Text="Item categories" /></h1>
            <p class="tis-page-header__desc">Categories used to classify items. Choose a company, then edit a category in place or add a new one.</p>
        </div>
    </div>

    <asp:Panel ID="pnlPendind" runat="server" CssClass="tis-card" DefaultButton="btnFilter">
        <div class="tis-card__body">
            <div class="tis-toolbar">
                <div class="tis-field">
                    <asp:Label ID="Label1" runat="server" Text="Company" Visible="true" AssociatedControlID="ddlCompany" CssClass="tis-label" />
                    <asp:DropDownList ID="ddlCompany" runat="server" Visible="true" DataTextField="CompanyName" DataValueField="CompanyId"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlCompanyChanged" />
                </div>
                <div class="tis-field tis-toolbar__grow">
                    <asp:Label ID="lblFilter" runat="server" Text="Search by category name" Visible="true" AssociatedControlID="txtFilter" CssClass="tis-label" />
                    <asp:TextBox ID="txtFilter" runat="server" Text="" Visible="true" placeholder="e.g. Cables" />
                </div>
                <asp:Button ID="btnFilter" runat="server" OnClick="btnFilter_Click" Text="Filter" CssClass="tis-btn" />
            </div>
        </div>
    </asp:Panel>

    <div class="tis-split">
        <asp:Panel ID="Pnlgv" runat="server" CssClass="tis-card tis-split__list">
            <div class="tis-card__header">
                <div class="tis-card__heading">
                    <div class="tis-card__title">Item categories</div>
                    <div class="tis-card__subtitle">Use Edit on a row to change its name or status.</div>
                </div>
                <asp:Button ID="btnNew" runat="server" Text="New category" CssClass="tis-btn tis-btn--primary tis-btn--sm"
                    OnClick="btnNew_Click" CausesValidation="false" />
            </div>
            <div class="tis-table-wrap tis-table-wrap--tall">
                <asp:GridView ID="GrdItemCategoryMaster" runat="server" AutoGenerateColumns="False"
                    OnRowCancelingEdit="GrdItemCategoryMaster_RowCancelingEdit"
                    OnRowEditing="GrdItemCategoryMaster_RowEditing"
                    OnRowUpdating="GrdItemCategoryMaster_RowUpdating">
                    <Columns>
                        <asp:TemplateField HeaderText="Item Category Id" Visible="false">
                            <ItemTemplate><asp:Label ID="lblItemCategoryId" runat="server" Text='<%# Eval("ItemCategoryId") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CompanyId" Visible="false">
                            <ItemTemplate><asp:Label ID="lblCompanyId" runat="server" Text='<%# Eval("CompanyId") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Code" Visible="true" ItemStyle-CssClass="code">
                            <ItemTemplate><asp:Label ID="lblItemCategoryCode" runat="server" Text='<%# Eval("ItemCategoryCode") %>' /></ItemTemplate>
                            <EditItemTemplate><asp:TextBox ID="txtItemCategoryCode" runat="server" Text='<%# Bind("ItemCategoryCode") %>' ReadOnly="true" /></EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Category name">
                            <ItemTemplate><asp:Label ID="lblItemCategoryName" runat="server" Text='<%# Eval("ItemCategoryName") %>' /></ItemTemplate>
                            <EditItemTemplate><asp:TextBox ID="txtItemCategoryName" runat="server" Text='<%# Bind("ItemCategoryName") %>' /></EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Active" HeaderStyle-CssClass="center" ItemStyle-CssClass="center">
                            <ItemTemplate><asp:CheckBox ID="chkActive" runat="server" Checked='<%# Eval("IsActive") %>' Enabled="true" /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField HeaderText="Edit" ShowEditButton="True" ControlStyle-CssClass="tis-link"
                            HeaderStyle-CssClass="actions" ItemStyle-CssClass="actions" />
                        <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" Visible="False" ControlStyle-CssClass="tis-link"
                            HeaderStyle-CssClass="actions" ItemStyle-CssClass="actions" />
                    </Columns>
                    <EmptyDataTemplate>
                        <tis:EmptyState ID="emptyItemCategories" runat="server" Icon="tag" Title="No item categories yet"
                            Text="Choose a company, then use New category to add the first one." />
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </asp:Panel>

        <asp:Panel ID="pnlAdd" runat="server" GroupingText="Add item category" CssClass="tis-card tis-card--legend tis-split__detail" DefaultButton="btnSave">
            <div class="tis-card__body">
                <div class="tis-form-grid tis-form-grid--2">
                    <div class="tis-field">
                        <asp:Label ID="lblItemCategoryCode" runat="server" Text="Category code" AssociatedControlID="txtItemCategorycode" CssClass="tis-label" />
                        <asp:TextBox ID="txtItemCategorycode" runat="server" MaxLength="8" />
                    </div>
                    <div class="tis-field tis-field--check">
                        <asp:CheckBox ID="ChkIsActive" runat="server" Text="Active" Checked="true" />
                    </div>
                    <div class="tis-field tis-field--full">
                        <asp:Label ID="lblItemCategoryName" runat="server" Text="Category name" AssociatedControlID="txtItemCategoryName" CssClass="tis-label" />
                        <asp:TextBox ID="txtItemCategoryName" runat="server" MaxLength="64" />
                    </div>
                </div>
            </div>
            <div class="tis-card__footer">
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="tis-btn tis-btn--primary" />
            </div>
        </asp:Panel>
    </div>
</div>
</asp:Content>
