<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.Master" AutoEventWireup="true" CodeFile="ItemMaster.aspx.cs" Inherits="ItemMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="tis-page">
    <div class="tis-page-header">
        <div class="tis-page-header__text">
            <span class="tis-eyebrow">Item Masters</span>
            <h1 class="tis-page-header__title"><asp:Label ID="lblItemGroupMaster" runat="server" Text="Items" /></h1>
            <p class="tis-page-header__desc">Materials and equipment with their category, group, unit and make. Choose a company and press Filter to list its items.</p>
        </div>
    </div>

    <asp:Panel ID="pnlPendind" runat="server" CssClass="tis-card" DefaultButton="btnFilter">
        <div class="tis-card__body">
            <div class="tis-toolbar">
                <div class="tis-field">
                    <asp:Label ID="lblCompany" runat="server" Text="Company" Visible="true" AssociatedControlID="ddlCompany" CssClass="tis-label" />
                    <asp:DropDownList ID="ddlCompany" runat="server" Visible="true" DataTextField="CompanyName" DataValueField="CompanyId"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlCompanyChanged" />
                </div>
                <div class="tis-field tis-toolbar__grow">
                    <asp:Label ID="lblFilter" runat="server" Text="Search by item name" Visible="true" AssociatedControlID="txtFilter" CssClass="tis-label" />
                    <asp:TextBox ID="txtFilter" runat="server" Text="" Visible="true" placeholder="e.g. Copper cable" />
                </div>
                <asp:Button ID="btnFilter" runat="server" OnClick="btnFilter_Click" Text="Filter" CssClass="tis-btn" />
                <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" CssClass="tis-btn tis-btn--ghost" />
            </div>
        </div>
    </asp:Panel>

    <div class="tis-split tis-split--wide-detail">
        <asp:Panel ID="Pnlgv" runat="server" CssClass="tis-card tis-split__list">
            <div class="tis-card__header">
                <div class="tis-card__heading">
                    <div class="tis-card__title">Items</div>
                    <div class="tis-card__subtitle">Select an item name to edit it.</div>
                </div>
                <asp:Button ID="btnNew" runat="server" Text="New item" CssClass="tis-btn tis-btn--primary tis-btn--sm"
                    OnClick="btnNew_Click" CausesValidation="false" />
            </div>
            <div id="divItemGroup" runat="server" class="tis-table-wrap tis-table-wrap--tall">
                <asp:GridView ID="GrdItemMaster" runat="server" AutoGenerateColumns="False" OnRowCommand="GrdItem_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="CompanyId" Visible="false">
                            <ItemTemplate><asp:Label ID="lblCompanyId" runat="server" Text='<%# Eval("CompanyId") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ItemCat Id" Visible="false">
                            <ItemTemplate><asp:Label ID="lblItemCatId" runat="server" Text='<%# Eval("ItemCategoryId") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ItemSubCat Id" Visible="false">
                            <ItemTemplate><asp:Label ID="lblItemSubCatId" runat="server" Text='<%# Eval("ItemSubCategoryId") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ItemGrpId" Visible="false">
                            <ItemTemplate><asp:Label ID="lblItemGroupId" runat="server" Text='<%# Eval("ItemGroupId") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ItemId" Visible="false">
                            <ItemTemplate><asp:Label ID="lblItemId" runat="server" Text='<%# Eval("ItemId") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Category" Visible="true">
                            <ItemTemplate><asp:Label ID="lblItemCategoryName" runat="server" Text='<%# Eval("ItemCategoryName") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sub-category" Visible="true">
                            <ItemTemplate><asp:Label ID="lblItemSubCategoryName" runat="server" Text='<%# Eval("ItemSubCategoryName") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Group" Visible="true">
                            <ItemTemplate><asp:Label ID="lblItemGroupName" runat="server" Text='<%# Eval("ItemGroupName") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Code" Visible="True" ItemStyle-CssClass="code">
                            <ItemTemplate><asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("ItemCode") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Make" Visible="true">
                            <ItemTemplate><asp:Label ID="lblItemMake" runat="server" Text='<%# Eval("ItemMake") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="UOM" Visible="true" ItemStyle-CssClass="nowrap">
                            <ItemTemplate><asp:Label ID="lblItemUOM" runat="server" Text='<%# Eval("ItemUOM") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item Name" Visible="false">
                            <ItemTemplate><asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ItemName") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item" Visible="true" ItemStyle-CssClass="wrap">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkItemName" runat="server" CssClass="tis-link tis-link--strong"
                                    CommandArgument='<%#Eval("ItemId")%>' CommandName="selectItem" Text='<%#Eval("ItemName") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Imported">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkImported" runat="server" CssClass="tis-status tis-status--yes" Checked='<%# Eval("IsImported") %>' Enabled="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkActive" runat="server" CssClass="tis-status" Checked='<%# Eval("IsActive") %>' Enabled="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <tis:EmptyState ID="emptyItems" runat="server" Icon="box" Title="No items yet"
                            Text="Use New item to add the first item for this company." />
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </asp:Panel>

        <asp:Panel ID="pnlAdd" runat="server" GroupingText="Item details" CssClass="tis-card tis-card--legend tis-split__detail" DefaultButton="btnSave">
            <div class="tis-card__body">
                <div class="tis-form-grid tis-form-grid--2">
                    <div class="tis-field">
                        <asp:Label ID="lblItemCategory" runat="server" Text="Category" Visible="true" AssociatedControlID="ddlItemCategory" CssClass="tis-label" />
                        <asp:DropDownList ID="ddlItemCategory" runat="server" Visible="true" DataTextField="ItemCategoryName" DataValueField="ItemCategoryId" />
                    </div>
                    <div class="tis-field">
                        <asp:Label ID="lblItemSubCategory" runat="server" Text="Sub-category" Visible="true" AssociatedControlID="ddlItemSubCategory" CssClass="tis-label" />
                        <asp:DropDownList ID="ddlItemSubCategory" runat="server" Visible="true" DataTextField="ItemSubCategoryName" DataValueField="ItemSubCategoryId" />
                    </div>
                    <div class="tis-field tis-field--full">
                        <asp:Label ID="lblItemGroup" runat="server" Text="Item group" AssociatedControlID="ddlItemGroup" CssClass="tis-label" />
                        <asp:DropDownList ID="ddlItemGroup" runat="server" Visible="true" DataTextField="ItemGroupName" DataValueField="ItemGroupId" />
                    </div>
                    <div class="tis-field tis-field--full">
                        <asp:Label ID="lblItemName" runat="server" Text="Item name" AssociatedControlID="txtItemName" CssClass="tis-label" />
                        <asp:TextBox ID="txtItemName" runat="server" />
                    </div>
                    <div class="tis-field">
                        <asp:Label ID="lblItemCode" runat="server" Text="Item code" AssociatedControlID="txtItemCode" CssClass="tis-label" />
                        <asp:TextBox ID="txtItemCode" runat="server" />
                    </div>
                    <div class="tis-field">
                        <asp:Label ID="lblItemUOM" runat="server" Text="Unit of measure" AssociatedControlID="txtItemUOM" CssClass="tis-label" />
                        <asp:TextBox ID="txtItemUOM" runat="server" placeholder="e.g. Nos, m, kg" />
                    </div>
                    <div class="tis-field tis-field--full">
                        <asp:Label ID="lblItemMake" runat="server" Text="Make (optional)" AssociatedControlID="txtItemMake" CssClass="tis-label" />
                        <asp:TextBox ID="txtItemMake" runat="server" />
                    </div>
                    <div class="tis-field tis-field--check">
                        <asp:CheckBox ID="ChkIsImported" runat="server" Text="Imported item" Visible="true" />
                    </div>
                    <div class="tis-field tis-field--check">
                        <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" Visible="False" />
                    </div>
                </div>
                <asp:TextBox ID="txtItemId" runat="server" Visible="false" />
                <asp:TextBox ID="txtItemCategoryId" runat="server" Visible="false" />
                <asp:TextBox ID="txtItemSubCategoryId" runat="server" Visible="false" />
            </div>
            <div class="tis-card__footer">
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="tis-btn tis-btn--primary" />
            </div>
        </asp:Panel>
    </div>
</div>
</asp:Content>
