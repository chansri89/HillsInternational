<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.Master" AutoEventWireup="true" CodeFile="ItemGroupMaster.aspx.cs" Inherits="ItemGroupMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="tis-page">
    <div class="tis-page-header">
        <div class="tis-page-header__text">
            <span class="tis-eyebrow">Item Masters</span>
            <h1 class="tis-page-header__title"><asp:Label ID="lblItemGroupMaster" runat="server" Text="Item groups" /></h1>
            <p class="tis-page-header__desc">Top-level groups used to organise items. Choose a company to see and edit its item groups.</p>
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
                    <asp:Label ID="lblFilter" runat="server" Text="Search by item group name" Visible="true" AssociatedControlID="txtFilter" CssClass="tis-label" />
                    <asp:TextBox ID="txtFilter" runat="server" Text="" Visible="true" placeholder="e.g. Electrical" />
                </div>
                <asp:Button ID="btnFilter" runat="server" OnClick="btnFilter_Click" Text="Filter" CssClass="tis-btn" />
            </div>
        </div>
    </asp:Panel>

    <div class="tis-split">
        <asp:Panel ID="Pnlgv" runat="server" CssClass="tis-card tis-split__list">
            <div class="tis-card__header">
                <div class="tis-card__heading">
                    <div class="tis-card__title">Item groups</div>
                    <div class="tis-card__subtitle">Select an item group name to edit it.</div>
                </div>
                <asp:Button ID="btnNew" runat="server" Text="New item group" CssClass="tis-btn tis-btn--primary tis-btn--sm"
                    OnClick="btnNew_Click" CausesValidation="false" />
            </div>
            <div id="divItemGroup" runat="server" class="tis-table-wrap tis-table-wrap--tall">
                <asp:GridView ID="GrdItemGroupMaster" runat="server" AutoGenerateColumns="False" OnRowCommand="GrdItemGroup_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="CompanyId" Visible="false">
                            <ItemTemplate><asp:Label ID="lblCompanyId" runat="server" Text='<%# Eval("CompanyId") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ItemGrp Id" Visible="false">
                            <ItemTemplate><asp:Label ID="lblItemGroupId" runat="server" Text='<%# Eval("ItemGroupId") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Code" Visible="true" ItemStyle-CssClass="code">
                            <ItemTemplate><asp:Label ID="lblItemGroupCode" runat="server" Text='<%# Eval("ItemGroupCode") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ItemGroup Name" Visible="false">
                            <ItemTemplate><asp:Label ID="lblItemGroupName" runat="server" Text='<%# Eval("ItemGroupName") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item group" Visible="true">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkCustName" runat="server" CssClass="tis-link tis-link--strong"
                                    CommandArgument='<%#Eval("ItemGroupId")%>' CommandName="selectItemGroup" Text='<%#Eval("ItemGroupName") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkActive" runat="server" CssClass="tis-status" Checked='<%# Eval("IsActive") %>' Enabled="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <tis:EmptyState ID="emptyItemGroups" runat="server" Icon="layers" Title="No item groups yet"
                            Text="Use New item group to add the first item group for this company." />
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </asp:Panel>

        <asp:Panel ID="pnlAdd" runat="server" GroupingText="Item group details" CssClass="tis-card tis-card--legend tis-split__detail" DefaultButton="btnSave">
            <div class="tis-card__body">
                <div class="tis-form-grid tis-form-grid--2">
                    <div class="tis-field">
                        <asp:Label ID="lblItemGroupCode" runat="server" Text="Item group code" AssociatedControlID="txtItemGroupCode" CssClass="tis-label" />
                        <asp:TextBox ID="txtItemGroupCode" runat="server" />
                    </div>
                    <div class="tis-field tis-field--check">
                        <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" Visible="False" />
                    </div>
                    <div class="tis-field tis-field--full">
                        <asp:Label ID="lblItemGroupName" runat="server" Text="Item group name" AssociatedControlID="txtItemGroupName" CssClass="tis-label" />
                        <asp:TextBox ID="txtItemGroupName" runat="server" />
                    </div>
                </div>
                <asp:TextBox ID="txtItemGroupId" runat="server" Visible="false" />
            </div>
            <div class="tis-card__footer">
                <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" CssClass="tis-btn tis-btn--ghost" />
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="tis-btn tis-btn--primary" />
            </div>
        </asp:Panel>
    </div>
</div>
</asp:Content>
