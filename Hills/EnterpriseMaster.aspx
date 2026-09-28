<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.Master" AutoEventWireup="true" CodeFile="EnterpriseMaster.aspx.cs" Inherits="EnterpriseMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="tis-page">
    <span class="tis-hidden"><asp:Label ID="lblSeparator" runat="server" /><asp:Label ID="lblSeparator1" runat="server" /></span>
    <div class="tis-page-header">
        <div class="tis-page-header__text">
            <span class="tis-eyebrow">Masters</span>
            <h1 class="tis-page-header__title"><asp:Label ID="lblenterprisemas" runat="server" Text="Enterprise Master" /></h1>
            <p class="tis-page-header__desc">Enterprises that own your company locations, with their address and contact details.</p>
        </div>
    </div>

    <div class="tis-split">
        <asp:Panel ID="Pnlgv" runat="server" CssClass="tis-card tis-split__list">
            <div class="tis-card__header">
                <div class="tis-card__heading">
                    <div class="tis-card__title">Enterprises</div>
                    <div class="tis-card__subtitle">Use Edit on a row to change it in place.</div>
                </div>
                <asp:Button ID="btnNew" runat="server" Text="New enterprise" CssClass="tis-btn tis-btn--primary tis-btn--sm"
                    OnClick="btnNew_Click" CausesValidation="false" />
            </div>
            <div class="tis-table-wrap tis-table-wrap--tall">
                <asp:GridView ID="GrdEnterpriseMaster" runat="server" AutoGenerateColumns="False"
                    OnRowCancelingEdit="GrdEnterpriseMaster_RowCancelingEdit"
                    OnRowEditing="GrdEnterpriseMaster_RowEditing"
                    OnRowUpdating="GrdEnterpriseMaster_RowUpdating">
                    <Columns>
                        <asp:TemplateField HeaderText="Id" Visible="False">
                            <ItemTemplate><asp:Label ID="lblEnterpriseId" runat="server" Text='<%# Eval("EnterpriseId") %>' /></ItemTemplate>
                            <EditItemTemplate><asp:TextBox ID="txtEnterpriseId" runat="server" Text='<%# Bind("EnterpriseId") %>' ReadOnly="true" /></EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Enterprise" ItemStyle-CssClass="strong">
                            <ItemTemplate><asp:Label ID="lblEnterpriseName" runat="server" Text='<%# Eval("EnterpriseName") %>' /></ItemTemplate>
                            <EditItemTemplate><asp:TextBox ID="txtEnterpriseName" runat="server" Text='<%# Bind("EnterpriseName") %>' /></EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Address 1" ItemStyle-CssClass="wrap">
                            <ItemTemplate><asp:Label ID="lblAddr1" runat="server" Text='<%# Eval("Addr1") %>' /></ItemTemplate>
                            <EditItemTemplate><asp:TextBox ID="txtAddr1" runat="server" Text='<%# Bind("Addr1") %>' /></EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Address 2" ItemStyle-CssClass="wrap">
                            <ItemTemplate><asp:Label ID="lblAddr2" runat="server" Text='<%# Eval("Addr2") %>' /></ItemTemplate>
                            <EditItemTemplate><asp:TextBox ID="txtAddr2" runat="server" Text='<%# Bind("Addr2") %>' /></EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Address 3" ItemStyle-CssClass="wrap">
                            <ItemTemplate><asp:Label ID="lblAddr3" runat="server" Text='<%# Eval("Addr3") %>' /></ItemTemplate>
                            <EditItemTemplate><asp:TextBox ID="txtAddr3" runat="server" Text='<%# Bind("Addr3") %>' /></EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Phone" ItemStyle-CssClass="nowrap">
                            <ItemTemplate><asp:Label ID="lblPhone1" runat="server" Text='<%# Eval("Phone1") %>' /></ItemTemplate>
                            <EditItemTemplate><asp:TextBox ID="txtPhone1" runat="server" Text='<%# Bind("Phone1") %>' /></EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fax" ItemStyle-CssClass="nowrap">
                            <ItemTemplate><asp:Label ID="lblFax" runat="server" Text='<%# Eval("Fax") %>' /></ItemTemplate>
                            <EditItemTemplate><asp:TextBox ID="txtFax" runat="server" Text='<%# Bind("Fax") %>' /></EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Website">
                            <ItemTemplate><asp:Label ID="lblWebsite" runat="server" Text='<%# Eval("Website") %>' /></ItemTemplate>
                            <EditItemTemplate><asp:TextBox ID="txtWebsite" runat="server" Text='<%# Bind("Website") %>' /></EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate><asp:CheckBox ID="chkActive" runat="server" CssClass="tis-status" Checked='<%# Eval("IsActive") %>' Enabled="false" /></ItemTemplate>
                            <EditItemTemplate><asp:CheckBox ID="chkIsActive" runat="server" Text="Active" Checked='<%# Bind("IsActive") %>' /></EditItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField HeaderText="Edit" ShowEditButton="True" ItemStyle-CssClass="actions" />
                        <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" Visible="False" ItemStyle-CssClass="actions" />
                    </Columns>
                    <EmptyDataTemplate>
                        <tis:EmptyState ID="emptyEnterprises" runat="server" Icon="briefcase" Title="No enterprises yet"
                            Text="Use New enterprise to add the first enterprise." />
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </asp:Panel>

        <asp:Panel ID="pnlAdd" runat="server" CssClass="tis-card tis-split__detail" DefaultButton="btnSave">
            <div class="tis-card__header">
                <div class="tis-card__heading">
                    <div class="tis-card__title">Add enterprise</div>
                    <div class="tis-card__subtitle">Name, first address line and phone are required.</div>
                </div>
            </div>
            <div class="tis-card__body">
                <div class="tis-form-grid tis-form-grid--2">
                    <div class="tis-field tis-field--full">
                        <asp:Label ID="lblEnterprisename" runat="server" Text="Enterprise name" AssociatedControlID="txtEnterprisename" CssClass="tis-label" />
                        <asp:TextBox ID="txtEnterprisename" runat="server" />
                    </div>
                    <div class="tis-field tis-field--full">
                        <asp:Label ID="lbladdr1" runat="server" Text="Address line 1" AssociatedControlID="txtaddr1" CssClass="tis-label" />
                        <asp:TextBox ID="txtaddr1" runat="server" />
                    </div>
                    <div class="tis-field tis-field--full">
                        <asp:Label ID="lbladdr2" runat="server" Text="Address line 2" AssociatedControlID="txtaddr2" CssClass="tis-label" />
                        <asp:TextBox ID="txtaddr2" runat="server" />
                    </div>
                    <div class="tis-field tis-field--full">
                        <asp:Label ID="lbladdr3" runat="server" Text="Address line 3" AssociatedControlID="txtaddr3" CssClass="tis-label" />
                        <asp:TextBox ID="txtaddr3" runat="server" />
                    </div>
                    <div class="tis-field">
                        <asp:Label ID="lblEnterprisePhone1" runat="server" Text="Phone" AssociatedControlID="txtEnterprisePhone1" CssClass="tis-label" />
                        <asp:TextBox ID="txtEnterprisePhone1" runat="server" />
                    </div>
                    <div class="tis-field">
                        <asp:Label ID="lblfax" runat="server" Text="Fax" AssociatedControlID="txtfax" CssClass="tis-label" />
                        <asp:TextBox ID="txtfax" runat="server" />
                    </div>
                    <div class="tis-field tis-field--full">
                        <asp:Label ID="lblwebsite" runat="server" Text="Website" AssociatedControlID="txtwebsite" CssClass="tis-label" />
                        <asp:TextBox ID="txtwebsite" runat="server" />
                    </div>
                    <div class="tis-field tis-field--check">
                        <asp:CheckBox ID="ChkIsActive" runat="server" Checked="true" Text="IsActive" Visible="true" />
                    </div>
                </div>
                <asp:HiddenField ID="HidDeleteCount" Value="0" runat="server" />
                <asp:HiddenField ID="HidUpdateCount" Value="0" runat="server" />
            </div>
            <div class="tis-card__footer">
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="tis-btn tis-btn--primary" />
            </div>
        </asp:Panel>
    </div>
</div>
</asp:Content>
