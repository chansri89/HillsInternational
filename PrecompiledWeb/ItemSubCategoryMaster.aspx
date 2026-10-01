<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.master" AutoEventWireup="true" CodeFile="ItemSubCategoryMaster.aspx.cs" Inherits="ItemSubCategoryMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="tis-page">
    <asp:Panel ID="Panel2" runat="server" CssClass="tis-hidden"></asp:Panel>
    <div class="tis-page-header">
        <div class="tis-page-header__text">
            <span class="tis-eyebrow">Item Masters</span>
            <h1 class="tis-page-header__title"><asp:Label ID="lblStatMas" runat="server" Text="Item sub-categories" /></h1>
            <p class="tis-page-header__desc">Finer classifications within item categories. Choose a company, then edit a sub-category in place or add a new one.</p>
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
                    <asp:Label ID="lblFilter" runat="server" Text="Search by sub-category name" Visible="true" AssociatedControlID="txtFilter" CssClass="tis-label" />
                    <asp:TextBox ID="txtFilter" runat="server" Text="" Visible="true" placeholder="e.g. Armoured" />
                </div>
                <asp:Button ID="btnFilter" runat="server" OnClick="btnFilter_Click" Text="Filter" CssClass="tis-btn" />
            </div>
        </div>
    </asp:Panel>

    <div class="tis-split">
        <asp:Panel ID="Pnlgv" runat="server" CssClass="tis-card tis-split__list">
            <div class="tis-card__header">
                <div class="tis-card__heading">
                    <div class="tis-card__title">Item sub-categories</div>
                    <div class="tis-card__subtitle">Use Edit on a row to change its code, name or status.</div>
                </div>
                <asp:Button ID="btnNew" runat="server" Text="New sub-category" CssClass="tis-btn tis-btn--primary tis-btn--sm"
                    OnClick="btnNew_Click" CausesValidation="false" />
            </div>
            <div class="tis-table-wrap tis-table-wrap--tall">
                <asp:GridView ID="GrdItemSubCategoryMaster" runat="server" AutoGenerateColumns="False"
                    OnRowCancelingEdit="GrdItemSubCategoryMaster_RowCancelingEdit"
                    OnRowEditing="GrdItemSubCategoryMaster_RowEditing"
                    OnRowUpdating="GrdItemSubCategoryMaster_RowUpdating">
                    <Columns>
                        <asp:TemplateField HeaderText="Item SubCategoryId" Visible="false">
                            <ItemTemplate><asp:Label ID="lblItemSubCategoryId" runat="server" Text='<%# Eval("ItemSubCategoryId") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Company" Visible="true" HeaderStyle-CssClass="num" ItemStyle-CssClass="num">
                            <ItemTemplate><asp:Label ID="lblCompanyId" runat="server" Text='<%# Eval("CompanyId") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Code" Visible="true" ItemStyle-CssClass="code">
                            <ItemTemplate><asp:Label ID="lblItemSubCategoryCode" runat="server" Text='<%# Eval("ItemSubCategoryCode") %>' /></ItemTemplate>
                            <EditItemTemplate><asp:TextBox ID="txtItemSubCategoryCode" runat="server" Text='<%# Bind("ItemSubCategoryCode") %>' /></EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sub-category name">
                            <ItemTemplate><asp:Label ID="lblItemSubCategoryName" runat="server" Text='<%# Eval("ItemSubCategoryName") %>' /></ItemTemplate>
                            <EditItemTemplate><asp:TextBox ID="txtItemSubCategoryName" runat="server" Text='<%# Bind("ItemSubCategoryName") %>' /></EditItemTemplate>
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
                        <tis:EmptyState ID="emptyItemSubCategories" runat="server" Icon="tag" Title="No item sub-categories yet"
                            Text="Choose a company, then use New sub-category to add the first one." />
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </asp:Panel>

        <asp:Panel ID="pnlAdd" runat="server" GroupingText="Add item sub-category" CssClass="tis-card tis-card--legend tis-split__detail" DefaultButton="btnSave">
            <div class="tis-card__body">
                <div class="tis-form-grid tis-form-grid--2">
                    <div class="tis-field">
                        <asp:Label ID="lblItemSubCategoryCode" runat="server" Text="Sub-category code" AssociatedControlID="txtItemSubCategorycode" CssClass="tis-label" />
                        <asp:TextBox ID="txtItemSubCategorycode" runat="server" MaxLength="8" />
                    </div>
                    <div class="tis-field tis-field--check">
                        <asp:CheckBox ID="ChkIsActive" runat="server" Text="Active" Checked="true" />
                    </div>
                    <div class="tis-field tis-field--full">
                        <asp:Label ID="lblItemSubCategoryName" runat="server" Text="Sub-category name" AssociatedControlID="txtItemSubCategoryName" CssClass="tis-label" />
                        <asp:TextBox ID="txtItemSubCategoryName" runat="server" MaxLength="64" />
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
