<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.Master" AutoEventWireup="true" CodeFile="ProjectSectorClassMaster.aspx.cs" Inherits="ProjectSectorClassMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="tis-page">
    <div class="tis-page-header">
        <div class="tis-page-header__text">
            <span class="tis-eyebrow">Masters</span>
            <h1 class="tis-page-header__title"><asp:Label ID="lblClientMaster" runat="server" Text="Project Sector Classes" /></h1>
            <p class="tis-page-header__desc">Sector classes within each project sector group. Select a class to edit it.</p>
        </div>
    </div>

    <asp:Panel ID="pnlPendind" runat="server" CssClass="tis-card" DefaultButton="btnFilter">
        <div class="tis-card__body">
            <div class="tis-toolbar">
                <div class="tis-field tis-toolbar__grow">
                    <asp:Label ID="lblFilter" runat="server" Text="Filter by class name" AssociatedControlID="txtFilter" CssClass="tis-label" />
                    <asp:TextBox ID="txtFilter" runat="server" Text="" />
                </div>
                <asp:Button ID="btnFilter" runat="server" OnClick="btnFilter_Click" Text="Filter" CssClass="tis-btn" />
            </div>
        </div>
    </asp:Panel>

    <div class="tis-split">
        <asp:Panel ID="Pnlgv" runat="server" CssClass="tis-card tis-split__list">
            <div class="tis-card__header">
                <div class="tis-card__heading">
                    <div class="tis-card__title">Sector classes</div>
                    <div class="tis-card__subtitle">Select a class name to edit it.</div>
                </div>
                <asp:Button ID="btnNew" runat="server" Text="New class" CssClass="tis-btn tis-btn--primary tis-btn--sm"
                    OnClick="btnNew_Click" CausesValidation="false" />
            </div>
            <div id="divSectorClass" runat="server" class="tis-table-wrap tis-table-wrap--tall">
                <asp:GridView ID="GrdSectorClass" runat="server" AutoGenerateColumns="False" OnRowCommand="GrdSectorClass_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="ProjectSectorGroupId" Visible="false">
                            <ItemTemplate><asp:Label ID="lblProjectSectorGroupId" runat="server" Text='<%# Eval("ProjectSectorGroupId") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sector group" Visible="true">
                            <ItemTemplate><asp:Label ID="lblProjectSectorGroupName" runat="server" Text='<%# Eval("ProjectSectorGroupName") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ProjectSectorsubGroupId" Visible="false">
                            <ItemTemplate><asp:Label ID="lblProjectSectorSubGroupId" runat="server" Text='<%# Eval("ProjectSectorSubGroupId") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Class name" Visible="true">
                            <ItemTemplate><asp:Label ID="lblProjectSectorClass" runat="server" Text='<%# Eval("ProjectSectorClass") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sector class" Visible="true">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkSectorClass" runat="server" CssClass="tis-link tis-link--strong" CommandArgument='<%#Eval("ProjectSectorSubGroupId")%>'
                                    CommandName="select" Text='<%#Eval("ProjectSectorClass") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate><asp:CheckBox ID="chkActive" runat="server" CssClass="tis-status" Checked='<%# Eval("IsActive") %>' Enabled="false" /></ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <tis:EmptyState ID="emptyClasses" runat="server" Icon="tag" Title="No sector classes yet"
                            Text="Use New class to add the first project sector class." />
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </asp:Panel>

        <asp:Panel ID="pnlAdd" runat="server" GroupingText="Add Sector Class" CssClass="tis-card tis-card--legend tis-split__detail" DefaultButton="btnSave">
            <div class="tis-card__body">
                <div class="tis-form-grid tis-form-grid--1">
                    <div class="tis-field">
                        <asp:Label ID="lblProjectSectorClass" runat="server" Text="Sector class" AssociatedControlID="txtProjectSectorClass" CssClass="tis-label" />
                        <asp:TextBox ID="txtProjectSectorClass" runat="server" />
                    </div>
                    <div class="tis-field">
                        <asp:Label ID="lblProjectSectorGroup" runat="server" Text="Sector group" AssociatedControlID="ddlProjectSectorGroup" CssClass="tis-label" />
                        <asp:DropDownList ID="ddlProjectSectorGroup" runat="server"
                            DataTextField="ProjectSectorGroupName" DataValueField="ProjectSectorGroupId" />
                    </div>
                    <div class="tis-field tis-field--check">
                        <asp:CheckBox ID="chkIsActive" runat="server" Text="IsActive" Visible="False" />
                    </div>
                </div>
                <asp:TextBox ID="txtProjectSectorGroupId" runat="server" Visible="false" />
                <asp:TextBox ID="txtProjectSectorSubgroupId" runat="server" Visible="false" />
            </div>
            <div class="tis-card__footer">
                <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" CssClass="tis-btn tis-btn--ghost" />
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="tis-btn tis-btn--primary" />
            </div>
        </asp:Panel>
    </div>
</div>
</asp:Content>
