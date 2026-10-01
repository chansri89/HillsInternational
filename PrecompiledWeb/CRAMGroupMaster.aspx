<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.Master" AutoEventWireup="true" CodeFile="CRAMGroupMaster.aspx.cs" Inherits="CRAMGroupMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="tis-page">
    <div class="tis-page-header">
        <div class="tis-page-header__text">
            <span class="tis-eyebrow">CRAM</span>
            <h1 class="tis-page-header__title"><asp:Label ID="lblIOWGroupMaster" runat="server" Text="IOW groups" /></h1>
            <p class="tis-page-header__desc">Top-level groups for CRAM items of work. Choose a company to see and edit its groups.</p>
        </div>
    </div>

    <asp:Panel ID="pnlPendind" runat="server" CssClass="tis-card" DefaultButton="btnFilter">
        <div class="tis-card__body">
            <div class="tis-toolbar">
                <div class="tis-field">
                    <asp:Label ID="lblCompany" runat="server" Text="Company" Visible="true" AssociatedControlID="ddlCompany" CssClass="tis-label" />
                    <asp:DropDownList ID="ddlCompany" runat="server" DataTextField="CompanyName" DataValueField="CompanyId"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlCompanyChanged" />
                </div>
                <div class="tis-field tis-toolbar__grow">
                    <asp:Label ID="lblFilter" runat="server" Text="Search by group name" Visible="true" AssociatedControlID="txtFilter" CssClass="tis-label" />
                    <asp:TextBox ID="txtFilter" runat="server" Text="" Visible="true" placeholder="e.g. Civil" />
                </div>
                <asp:Button ID="btnFilter" runat="server" OnClick="btnFilter_Click" Text="Filter" CssClass="tis-btn" />
            </div>
        </div>
    </asp:Panel>

    <div class="tis-split">
        <asp:Panel ID="Pnlgv" runat="server" CssClass="tis-card tis-split__list">
            <div class="tis-card__header">
                <div class="tis-card__heading">
                    <div class="tis-card__title">IOW groups</div>
                    <div class="tis-card__subtitle">Select a group name to edit it.</div>
                </div>
                <asp:Button ID="btnNew" runat="server" Text="New group" CssClass="tis-btn tis-btn--primary tis-btn--sm"
                    OnClick="btnNew_Click" CausesValidation="false" />
            </div>
            <div id="divIOWGroup" runat="server" class="tis-table-wrap tis-table-wrap--tall">
                <asp:GridView ID="GrdIOWGroupMaster" runat="server" AutoGenerateColumns="False" OnRowCommand="GrdIOWGroup_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="CompanyId" Visible="false">
                            <ItemTemplate><asp:Label ID="lblCompanyId" runat="server" Text='<%# Eval("CompanyId") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Grp Id" Visible="false">
                            <ItemTemplate><asp:Label ID="lblCRAMGroupId" runat="server" Text='<%# Eval("CRAMGroupId") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Code" Visible="true" ItemStyle-CssClass="code">
                            <ItemTemplate><asp:Label ID="lblGroupCode" runat="server" Text='<%# Eval("GroupCode") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Group Name" Visible="false">
                            <ItemTemplate><asp:Label ID="lblGroupName" runat="server" Text='<%# Eval("GroupName") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Group" Visible="true">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkIOWName" runat="server" CssClass="tis-link tis-link--strong"
                                    CommandArgument='<%#Eval("CRAMGroupId")%>' CommandName="selectIOWGroup" Text='<%#Eval("GroupName") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <tis:EmptyState ID="emptyGroups" runat="server" Icon="folder" Title="No IOW groups yet"
                            Text="Use New group to add the first IOW group for this company." />
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </asp:Panel>

        <asp:Panel ID="pnlAdd" runat="server" GroupingText="IOW group details" CssClass="tis-card tis-card--legend tis-split__detail" DefaultButton="btnSave">
            <div class="tis-card__body">
                <div class="tis-form-grid tis-form-grid--2">
                    <div class="tis-field">
                        <asp:Label ID="lblIOWGroupCode" runat="server" Text="Group code" AssociatedControlID="txtGroupCode" CssClass="tis-label" />
                        <asp:TextBox ID="txtGroupCode" runat="server" />
                    </div>
                    <div class="tis-field tis-field--check">
                        <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" Visible="False" Checked="true" />
                    </div>
                    <div class="tis-field tis-field--full">
                        <asp:Label ID="lblGroupName" runat="server" Text="Group name" AssociatedControlID="txtGroupName" CssClass="tis-label" />
                        <asp:TextBox ID="txtGroupName" runat="server" />
                    </div>
                </div>
                <asp:TextBox ID="txtCRAMGroupId" runat="server" Visible="false" />
            </div>
            <div class="tis-card__footer">
                <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" CssClass="tis-btn tis-btn--ghost" />
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="tis-btn tis-btn--primary" />
            </div>
        </asp:Panel>
    </div>
</div>
</asp:Content>
