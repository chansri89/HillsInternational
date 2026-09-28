<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.Master" AutoEventWireup="true" CodeFile="ClientMaster.aspx.cs" Inherits="ClientMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="tis-page">
    <div class="tis-page-header">
        <div class="tis-page-header__text">
            <span class="tis-eyebrow">Masters</span>
            <h1 class="tis-page-header__title"><asp:Label ID="lblClientMaster" runat="server" Text="Clients and tenderers" /></h1>
            <p class="tis-page-header__desc">Organisations that issue tenders to your company. Choose a company to see and edit its clients.</p>
        </div>
    </div>

    <asp:Panel ID="pnlPendind" runat="server" CssClass="tis-card" DefaultButton="btnFilter">
        <div class="tis-card__body">
            <div class="tis-toolbar">
                <div class="tis-field">
                    <asp:Label ID="lblCompany" runat="server" Text="Company" AssociatedControlID="ddlCompany" CssClass="tis-label" />
                    <asp:DropDownList ID="ddlCompany" runat="server" DataTextField="CompanyName" DataValueField="CompanyId"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlCompanyChanged" />
                </div>
                <div class="tis-field tis-toolbar__grow">
                    <asp:Label ID="lblFilter" runat="server" Text="Search by client name" AssociatedControlID="txtFilter" CssClass="tis-label" />
                    <asp:TextBox ID="txtFilter" runat="server" placeholder="e.g. Larsen" />
                </div>
                <asp:Button ID="btnFilter" runat="server" OnClick="btnFilter_Click" Text="Filter" CssClass="tis-btn" />
            </div>
        </div>
    </asp:Panel>

    <div class="tis-split">
        <asp:Panel ID="Pnlgv" runat="server" CssClass="tis-card tis-split__list">
            <div class="tis-card__header">
                <div class="tis-card__heading">
                    <div class="tis-card__title">Clients</div>
                    <div class="tis-card__subtitle">Select a client name to edit it.</div>
                </div>
                <asp:Button ID="btnNew" runat="server" Text="New client" CssClass="tis-btn tis-btn--primary tis-btn--sm"
                    OnClick="btnNew_Click" CausesValidation="false" />
            </div>
            <div id="divClient" runat="server" class="tis-table-wrap tis-table-wrap--tall">
                <asp:GridView ID="GrdClientMaster" runat="server" AutoGenerateColumns="False" OnRowCommand="GrdClient_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="CompanyId" Visible="false">
                            <ItemTemplate><asp:Label ID="lblCompanyId" runat="server" Text='<%# Eval("CompanyId") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Code" ItemStyle-CssClass="code">
                            <ItemTemplate><asp:Label ID="lblClientCode" runat="server" Text='<%# Eval("ClientCode") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cli Name" Visible="false">
                            <ItemTemplate><asp:Label ID="lblClientName" runat="server" Text='<%# Eval("ClientName") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Client">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkCustName" runat="server" CssClass="tis-link tis-link--strong"
                                    CommandArgument='<%#Eval("ClientCode")%>' CommandName="selectClient" Text='<%#Eval("ClientName") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Short name">
                            <ItemTemplate><asp:Label ID="lblClientShortName" runat="server" Text='<%# Eval("ClientShortName") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Type">
                            <ItemTemplate><asp:Label ID="lblClientType" runat="server" Text='<%# Eval("ClientType") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Building">
                            <ItemTemplate><asp:Label ID="lblBuildingName" runat="server" Text='<%# Eval("BuildingName") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Address" ItemStyle-CssClass="wrap">
                            <ItemTemplate><asp:Label ID="lblClientAddress1" runat="server" Text='<%# Eval("Addr1") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Address2" Visible="false">
                            <ItemTemplate><asp:Label ID="lblClientAddress2" runat="server" Text='<%# Eval("Addr2") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="StateId" Visible="false">
                            <ItemTemplate><asp:Label ID="lblStateId" runat="server" Text='<%# Eval("StateId") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="City">
                            <ItemTemplate><asp:Label ID="lblCity" runat="server" Text='<%# Eval("City") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="State">
                            <ItemTemplate><asp:Label ID="lblStateName" runat="server" Text='<%# Eval("StateName") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PIN" ItemStyle-CssClass="nowrap">
                            <ItemTemplate><asp:Label ID="lblPinCode" runat="server" Text='<%# Eval("PinCode") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkActive" runat="server" CssClass="tis-status" Checked='<%# Eval("IsActive") %>' Enabled="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <tis:EmptyState ID="emptyClients" runat="server" Icon="users" Title="No clients yet"
                            Text="Use New client to add the first client or tenderer for this company." />
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </asp:Panel>

        <asp:Panel ID="pnlAdd" runat="server" GroupingText="Add Client" CssClass="tis-card tis-card--legend tis-split__detail" DefaultButton="btnSave">
            <div class="tis-card__body">
                <div class="tis-form-grid tis-form-grid--2">
                    <div class="tis-field">
                        <asp:Label ID="lblClientCode" runat="server" Text="Client code" AssociatedControlID="txtClientCode" CssClass="tis-label" />
                        <asp:TextBox ID="txtClientCode" runat="server" />
                    </div>
                    <div class="tis-field">
                        <asp:Label ID="lblClientShortName" runat="server" Text="Short name" AssociatedControlID="txtClientShortName" CssClass="tis-label" />
                        <asp:TextBox ID="txtClientShortName" runat="server" MaxLength="8" />
                    </div>
                    <div class="tis-field tis-field--full">
                        <asp:Label ID="lblClientName" runat="server" Text="Client name" AssociatedControlID="txtClientName" CssClass="tis-label" />
                        <asp:TextBox ID="txtClientName" runat="server" />
                    </div>
                    <div class="tis-field">
                        <asp:Label ID="lblClientType" runat="server" Text="Client type" AssociatedControlID="ddlClientType" CssClass="tis-label" />
                        <asp:DropDownList ID="ddlClientType" runat="server" DataTextField="ClientType" DataValueField="ClientType" />
                    </div>
                    <div class="tis-field">
                        <asp:Label ID="lblBuildingName" runat="server" Text="Building" AssociatedControlID="txtBuildingName" CssClass="tis-label" />
                        <asp:TextBox ID="txtBuildingName" runat="server" MaxLength="64" />
                    </div>
                    <div class="tis-field tis-field--full">
                        <asp:Label ID="lblAddr1" runat="server" Text="Address line 1" AssociatedControlID="txtAddr1" CssClass="tis-label" />
                        <asp:TextBox ID="txtAddr1" runat="server" MaxLength="64" />
                    </div>
                    <div class="tis-field tis-field--full">
                        <asp:Label ID="lblAddr2" runat="server" Text="Address line 2" AssociatedControlID="txtAddr2" CssClass="tis-label" />
                        <asp:TextBox ID="txtAddr2" runat="server" MaxLength="64" />
                    </div>
                    <div class="tis-field">
                        <asp:Label ID="lblCity" runat="server" Text="City" AssociatedControlID="txtCity" CssClass="tis-label" />
                        <asp:TextBox ID="txtCity" runat="server" MaxLength="32" />
                    </div>
                    <div class="tis-field">
                        <asp:Label ID="lblState" runat="server" Text="State" AssociatedControlID="ddlState" CssClass="tis-label" />
                        <asp:DropDownList ID="ddlState" runat="server" DataTextField="StateName" DataValueField="StateId"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlStateChanged" />
                    </div>
                    <div class="tis-field">
                        <asp:Label ID="lblPinCode" runat="server" Text="PIN code" AssociatedControlID="txtPinCode" CssClass="tis-label" />
                        <asp:TextBox ID="txtPinCode" runat="server" MaxLength="6" />
                    </div>
                    <div class="tis-field tis-field--check">
                        <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" Visible="False" />
                    </div>
                </div>
                <asp:TextBox ID="txtClientId" runat="server" Visible="false" />
            </div>
            <div class="tis-card__footer">
                <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" CssClass="tis-btn tis-btn--ghost" />
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="tis-btn tis-btn--primary" />
            </div>
        </asp:Panel>
    </div>
</div>
</asp:Content>
