<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.master" AutoEventWireup="true" CodeFile="ProjectSectorGroupMaster.aspx.cs" Inherits="ProjectSectorGroupMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="tis-page">
    <div class="tis-page-header">
        <div class="tis-page-header__text">
            <span class="tis-eyebrow">Masters</span>
            <h1 class="tis-page-header__title"><asp:Label ID="lblStatMas" runat="server" Text="Project Sector Group " /></h1>
            <p class="tis-page-header__desc">Sector groups classify projects. Use Add class on a group to see and add its sector classes.</p>
        </div>
    </div>

    <div class="tis-split">
        <asp:Panel ID="Pnlgv" runat="server" CssClass="tis-card tis-split__list">
            <div class="tis-card__header">
                <div class="tis-card__heading">
                    <div class="tis-card__title">Sector groups</div>
                    <div class="tis-card__subtitle">Use Edit to rename a group, or Add class to manage its classes.</div>
                </div>
                <asp:Button ID="btnNew" runat="server" Text="New group" CssClass="tis-btn tis-btn--primary tis-btn--sm"
                    OnClick="btnNew_Click" CausesValidation="false" />
            </div>
            <div class="tis-table-wrap tis-table-wrap--tall">
                <asp:GridView ID="GrdSectorGrp" runat="server" AutoGenerateColumns="False"
                    OnRowCancelingEdit="GrdSectorGrp_RowCancelingEdit"
                    OnRowEditing="GrdSectorGrp_RowEditing"
                    OnRowUpdating="GrdSectorGrp_RowUpdating"
                    OnRowCommand="GrdSectorGrp_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="ProjectSectorGroupId" Visible="False">
                            <ItemTemplate><asp:Label ID="lblProjectSectorGroupId" runat="server" Text='<%# Eval("ProjectSectorGroupId") %>' /></ItemTemplate>
                            <EditItemTemplate><asp:TextBox ID="txtProjectSectorGroupId" runat="server" Text='<%# Bind("ProjectSectorGroupId") %>' ReadOnly="true" /></EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sector group" ItemStyle-CssClass="strong">
                            <ItemTemplate><asp:Label ID="lblProjectSectorGroupName" runat="server" Text='<%# Eval("ProjectSectorGroupName") %>' /></ItemTemplate>
                            <EditItemTemplate><asp:TextBox ID="txtProjectSectorGroupName" runat="server" Text='<%# Bind("ProjectSectorGroupName") %>' /></EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Classes" ItemStyle-CssClass="actions">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkCustName" runat="server" CssClass="tis-link tis-link--strong" CommandArgument='<%#Eval("ProjectSectorGroupId")%>'
                                    CommandName="AddClass" Text="Add Class" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField HeaderText="Edit" ShowEditButton="True" ItemStyle-CssClass="actions" />
                        <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" Visible="False" ItemStyle-CssClass="actions" />
                    </Columns>
                    <EmptyDataTemplate>
                        <tis:EmptyState ID="emptyGroups" runat="server" Icon="layers" Title="No sector groups yet"
                            Text="Use New group to add the first project sector group." />
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </asp:Panel>

        <asp:Panel ID="pnlAdd" runat="server" GroupingText="Add Sector Group" CssClass="tis-card tis-card--legend tis-split__detail" DefaultButton="btnSave">
            <div class="tis-card__body">
                <div class="tis-form-grid tis-form-grid--1">
                    <div class="tis-field">
                        <asp:Label ID="lblProjectSectorGroupName" runat="server" Text="Sector group name" AssociatedControlID="txtProjectSectorGroupName" CssClass="tis-label" />
                        <asp:TextBox ID="txtProjectSectorGroupName" runat="server" />
                    </div>
                </div>
            </div>
            <div class="tis-card__footer">
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="tis-btn tis-btn--primary" />
            </div>
        </asp:Panel>
    </div>

    <div class="tis-split">
        <asp:Panel ID="pnlClass" runat="server" GroupingText="SectorClass Grid" CssClass="tis-card tis-card--legend tis-split__list" DefaultButton="btnFilter">
            <div class="tis-card__body">
                <div class="tis-toolbar">
                    <div class="tis-field tis-toolbar__grow">
                        <asp:Label ID="lblFilter" runat="server" Text="Filter by class name" AssociatedControlID="txtFilter" CssClass="tis-label" />
                        <asp:TextBox ID="txtFilter" runat="server" Text="" />
                    </div>
                    <asp:Button ID="btnFilter" runat="server" OnClick="btnFilter_Click" Text="Filter" CssClass="tis-btn" />
                    <asp:TextBox ID="txtProjectSectorGroupId" runat="server" Visible="false" />
                    <asp:TextBox ID="txtProjectSectorSubgroupId" runat="server" Visible="false" />
                </div>
            </div>
            <div id="divSectorClass" runat="server" class="tis-table-wrap tis-table-wrap--tall">
                <asp:GridView ID="GrdSectorClass" runat="server" AutoGenerateColumns="False">
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
                        <asp:TemplateField HeaderText="Sector class" Visible="true" ItemStyle-CssClass="strong">
                            <ItemTemplate><asp:Label ID="lblProjectSectorClass" runat="server" Text='<%# Eval("ProjectSectorClass") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate><asp:CheckBox ID="chkActive" runat="server" CssClass="tis-status" Checked='<%# Eval("IsActive") %>' Enabled="false" /></ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <tis:EmptyState ID="emptyClasses" runat="server" Icon="tag" Title="No sector classes yet"
                            Text="Add a class for this group with the form alongside." />
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </asp:Panel>

        <asp:Panel ID="pnlAddClass" runat="server" GroupingText="Add Sector Class" CssClass="tis-card tis-card--legend tis-split__detail" DefaultButton="btnSaveClass">
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
            </div>
            <div class="tis-card__footer">
                <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" CssClass="tis-btn tis-btn--ghost" />
                <asp:Button ID="btnSaveClass" runat="server" Text="Save Class" OnClick="btnSaveClass_Click" CssClass="tis-btn tis-btn--primary" />
            </div>
        </asp:Panel>
    </div>
</div>
</asp:Content>
