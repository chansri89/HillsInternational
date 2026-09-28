<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.Master" AutoEventWireup="true" CodeFile="CompanyMaster.aspx.cs" Inherits="CompanyMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="tis-page">
    <div class="tis-page-header">
        <div class="tis-page-header__text">
            <span class="tis-eyebrow">Masters</span>
            <h1 class="tis-page-header__title"><asp:Label ID="lblcompanymaster" runat="server" Text="Location Master" /></h1>
            <p class="tis-page-header__desc">Company locations (corporate, head office and branches) with their enterprise, parent and state.</p>
        </div>
    </div>

    <div class="tis-split">
        <asp:Panel ID="Pnlgv" runat="server" CssClass="tis-card tis-split__list">
            <div class="tis-card__header">
                <div class="tis-card__heading">
                    <div class="tis-card__title">Locations</div>
                    <div class="tis-card__subtitle">Use Edit on a row to change it in place.</div>
                </div>
                <asp:Button ID="btnNew" runat="server" Text="New location" CssClass="tis-btn tis-btn--primary tis-btn--sm"
                    OnClick="btnNew_Click" CausesValidation="false" />
            </div>
            <div id="divCompany" runat="server" class="tis-table-wrap tis-table-wrap--tall">
                <asp:GridView ID="GrdCompanyMaster" runat="server" AutoGenerateColumns="False" DataKeyNames="CompanyCode"
                    OnRowCancelingEdit="GrdCompanyMaster_RowCancelingEdit"
                    OnRowDeleting="GrdCompanyMaster_RowDeleting"
                    OnRowEditing="GrdCompanyMaster_RowEditing"
                    OnRowUpdating="GrdCompanyMaster_RowUpdating">
                    <Columns>
                        <asp:TemplateField HeaderText="Code" ItemStyle-CssClass="code">
                            <ItemTemplate><asp:Label ID="lblCompanyCode" runat="server" Text='<%# Eval("CompanyCode") %>' /></ItemTemplate>
                            <EditItemTemplate><asp:TextBox ID="txtCompanyCode" runat="server" Text='<%# Bind("CompanyCode") %>' ReadOnly="true" Enabled="false" /></EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Location">
                            <ItemTemplate><asp:Label ID="lblCompanyName" runat="server" Text='<%# Eval("CompanyName") %>' /></ItemTemplate>
                            <EditItemTemplate><asp:TextBox ID="txtCompanyName" runat="server" Text='<%# Bind("CompanyName") %>' /></EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Short name">
                            <ItemTemplate><asp:Label ID="lblCompanyShortName" runat="server" Text='<%# Eval("CompanyShortName") %>' /></ItemTemplate>
                            <EditItemTemplate><asp:TextBox ID="txtCompanyShortName" runat="server" Text='<%# Bind("CompanyShortName") %>' MaxLength="8" /></EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="LocationTypeId" Visible="false">
                            <ItemTemplate><asp:Label ID="lblLocationTypeId" runat="server" Text='<%# Eval("LocationTypeId") %>' /></ItemTemplate>
                            <EditItemTemplate><asp:TextBox ID="txtLocationTypeId" runat="server" Text='<%# Bind("LocationTypeId") %>' /></EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Enterprise">
                            <ItemTemplate><asp:Label ID="lblentName" runat="server" Text='<%# Eval("EnterpriseName") %>' /></ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlEntName" runat="server" DataValueField="EnterpriseId" DataTextField="EnterpriseName" DataSource='<%#getEnterprise() %>'>
                                    <asp:ListItem Text="--Select Pls--" Value="0" />
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Type">
                            <ItemTemplate><asp:Label ID="lblCompnyType" runat="server" Text='<%# Eval("CompanyFlag") %>' /></ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlCompnyType" runat="server" DataValueField="CompanyTypeId" DataTextField="CompanyTypeName" DataSource='<%#LoadCompanyType() %>'>
                                    <asp:ListItem Text="--Parent--" Value="0" />
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Parent company">
                            <ItemTemplate><asp:Label ID="lblParentCompanyName" runat="server" Text='<%# Eval("ParentCompanyName") %>' /></ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlParentCompanyName" runat="server" DataValueField="CompanyCode" DataTextField="CompanyName" DataSource='<%#getParentCompanyName() %>'>
                                    <asp:ListItem Text="--Parent--" Value="0" />
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="State" ItemStyle-CssClass="nowrap">
                            <ItemTemplate><asp:Label ID="lblStateShortName" runat="server" Text='<%# Eval("StateShortName") %>' /></ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlStateShortName" runat="server" DataValueField="StateId" DataTextField="StateShortName" DataSource='<%#getState() %>' />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate><asp:CheckBox ID="chkActive" runat="server" CssClass="tis-status" Checked='<%# Eval("IsActive") %>' Enabled="false" /></ItemTemplate>
                            <EditItemTemplate><asp:CheckBox ID="chkIsActive" runat="server" Text="Active" Checked='<%# Bind("IsActive") %>' /></EditItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField HeaderText="Edit" ShowEditButton="True" ItemStyle-CssClass="actions" />
                        <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" ItemStyle-CssClass="actions" />
                    </Columns>
                    <EmptyDataTemplate>
                        <tis:EmptyState ID="emptyLocations" runat="server" Icon="building" Title="No locations yet"
                            Text="Use New location to add the first company location." />
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </asp:Panel>

        <asp:Panel ID="pnlAdd" runat="server" GroupingText="Add / Edit Location" CssClass="tis-card tis-card--legend tis-split__detail" DefaultButton="btnSave">
            <div class="tis-card__body">
                <div class="tis-form-grid tis-form-grid--2">
                    <div class="tis-field">
                        <asp:Label ID="lblCompanyCode" runat="server" Text="Location code" AssociatedControlID="txtCompanyCode" CssClass="tis-label" />
                        <asp:TextBox ID="txtCompanyCode" runat="server" />
                    </div>
                    <div class="tis-field">
                        <asp:Label ID="lblCompanyShortName" runat="server" Text="Short name" AssociatedControlID="txtCompanyShortName" CssClass="tis-label" />
                        <asp:TextBox ID="txtCompanyShortName" runat="server" MaxLength="8" />
                    </div>
                    <div class="tis-field tis-field--full">
                        <asp:Label ID="lblCompanyName" runat="server" Text="Location name" AssociatedControlID="txtCompanyName" CssClass="tis-label" />
                        <asp:TextBox ID="txtCompanyName" runat="server" />
                    </div>
                    <div class="tis-field tis-field--full">
                        <asp:Label ID="Label1" runat="server" Text="Enterprise" AssociatedControlID="ddlEnterpriseName" CssClass="tis-label" />
                        <asp:DropDownList ID="ddlEnterpriseName" runat="server" DataValueField="EnterPriseId" DataTextField="EnterPriseShortName" />
                    </div>
                    <div class="tis-field">
                        <asp:Label ID="lblCompanyType" runat="server" Text="Location type" AssociatedControlID="ddlCompanyType" CssClass="tis-label" />
                        <asp:DropDownList ID="ddlCompanyType" runat="server" DataValueField="CompanyTypeId" DataTextField="CompanyTypeName" />
                    </div>
                    <div class="tis-field">
                        <asp:Label ID="lblStateShortName" runat="server" Text="State" AssociatedControlID="ddlStateShortName" CssClass="tis-label" />
                        <asp:DropDownList ID="ddlStateShortName" runat="server" />
                    </div>
                    <div class="tis-field tis-field--full">
                        <asp:Label ID="lblParentCompanyName" runat="server" Text="Parent company" AssociatedControlID="ddlParentCompanyName" CssClass="tis-label" />
                        <asp:DropDownList ID="ddlParentCompanyName" runat="server" />
                    </div>
                    <div class="tis-field tis-field--check">
                        <asp:CheckBox ID="chkIsActive" runat="server" Text="IsActive" Visible="False" />
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
