<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.Master" AutoEventWireup="true" CodeFile="CRAMIOWHeadMaster.aspx.cs" Inherits="CRAMIOWHeadMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="tis-page">
    <div class="tis-page-header">
        <div class="tis-page-header__text">
            <span class="tis-eyebrow">CRAM</span>
            <h1 class="tis-page-header__title"><asp:Label ID="lblIOWHeadMaster" runat="server" Text="CRAM IOW heads" /></h1>
            <p class="tis-page-header__desc">Item-of-work headings filed under a CRAM group and sub-group. Choose a company, narrow by group if needed, then press Filter.</p>
        </div>
    </div>

    <asp:Panel ID="pnlPendind" runat="server" CssClass="tis-card" DefaultButton="btnFilter">
        <div class="tis-card__body">
            <div class="tis-toolbar">
                <div class="tis-field">
                    <asp:Label ID="lblCompany" runat="server" Text="Company" Visible="true" AssociatedControlID="ddlCompany" CssClass="tis-label" />
                    <asp:DropDownList ID="ddlCompany" runat="server" Visible="true" DataTextField="CompanyName" DataValueField="CompanyId"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_Changed" />
                </div>
                <div class="tis-field">
                    <asp:Label ID="Label1" runat="server" Text="Group" AssociatedControlID="ddlGroupFilter" CssClass="tis-label" />
                    <asp:DropDownList ID="ddlGroupFilter" runat="server" Visible="true" DataTextField="GroupName" DataValueField="GroupCode"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlGroupFilter_Changed" />
                </div>
                <div class="tis-field">
                    <asp:Label ID="Label2" runat="server" Text="Sub-group" AssociatedControlID="ddlSubGroupFilter" CssClass="tis-label" />
                    <asp:DropDownList ID="ddlSubGroupFilter" runat="server" DataTextField="SubGroupName" DataValueField="SubGroupCode" Visible="true" />
                </div>
                <div class="tis-field tis-toolbar__grow">
                    <asp:Label ID="lblFilter" runat="server" Text="Search by IOW head name" Visible="true" AssociatedControlID="txtFilter" CssClass="tis-label" />
                    <asp:TextBox ID="txtFilter" runat="server" Text="" Visible="true" placeholder="e.g. Excavation" />
                </div>
                <asp:Button ID="btnFilter" runat="server" OnClick="btnFilter_Click" Text="Filter" CssClass="tis-btn" />
            </div>
        </div>
    </asp:Panel>

    <div class="tis-split">
        <asp:Panel ID="Pnlgv" runat="server" CssClass="tis-card tis-split__list">
            <div class="tis-card__header">
                <div class="tis-card__heading">
                    <div class="tis-card__title">IOW heads</div>
                    <div class="tis-card__subtitle">Select an IOW head name to edit it.</div>
                </div>
                <asp:Button ID="btnNew" runat="server" Text="New IOW head" CssClass="tis-btn tis-btn--primary tis-btn--sm"
                    OnClick="btnNew_Click" CausesValidation="false" />
            </div>
            <div id="divIOW" runat="server" class="tis-table-wrap tis-table-wrap--tall">
                <asp:GridView ID="GrdIOWHeadMaster" runat="server" AutoGenerateColumns="False" OnRowCommand="GrdIOWHead_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="CompanyId" Visible="false">
                            <ItemTemplate><asp:Label ID="lblCompanyId" runat="server" Text='<%# Eval("CompanyId") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Grp code" Visible="true" ItemStyle-CssClass="code">
                            <ItemTemplate><asp:Label ID="lblGroupCode" runat="server" Text='<%# Eval("GroupCode") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Group" Visible="true">
                            <ItemTemplate><asp:Label ID="lblGroupName" runat="server" Text='<%# Eval("GroupName") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sub-grp code" Visible="true" ItemStyle-CssClass="code">
                            <ItemTemplate><asp:Label ID="lblSubGroupCode" runat="server" Text='<%# Eval("SubGroupCode") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sub-group" Visible="true">
                            <ItemTemplate><asp:Label ID="lblSubGroupName" runat="server" Text='<%# Eval("SubGroupName") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Level 1" Visible="true" HeaderStyle-CssClass="center" ItemStyle-CssClass="center">
                            <ItemTemplate><asp:Label ID="lblIOWLevel1" runat="server" Text='<%# Eval("IOWLevel1") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Level 2" Visible="true" HeaderStyle-CssClass="center" ItemStyle-CssClass="center">
                            <ItemTemplate><asp:Label ID="lblIOWLevel2" runat="server" Text='<%# Eval("IOWLevel2") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Level 3" Visible="true" HeaderStyle-CssClass="center" ItemStyle-CssClass="center">
                            <ItemTemplate><asp:Label ID="lblIOWLevel3" runat="server" Text='<%# Eval("IOWLevel3") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Level 4" Visible="true" HeaderStyle-CssClass="center" ItemStyle-CssClass="center">
                            <ItemTemplate><asp:Label ID="lblIOWLevel4" runat="server" Text='<%# Eval("IOWLevel4") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="IOW code" Visible="true" ItemStyle-CssClass="code">
                            <ItemTemplate><asp:Label ID="lblIOWHeadCode" runat="server" Text='<%# Eval("IOWHeadCode") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="IOW Head Name" Visible="false">
                            <ItemTemplate><asp:Label ID="lblIOWHeadName" runat="server" Text='<%# Eval("IOWHeadName") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="IOW head name" Visible="true" ItemStyle-CssClass="wrap">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkIOWHeadName" runat="server" CssClass="tis-link tis-link--strong"
                                    CommandArgument='<%#Eval("IOWHeadCode")%>' CommandName="UpdateIOW" Text='<%#Eval("IOWHeadName") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <tis:EmptyState ID="emptyIOWHeads" runat="server" Icon="list" Title="No IOW heads found"
                            Text="Change the group filters or use New IOW head to add one." />
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </asp:Panel>

        <asp:Panel ID="pnlAdd" runat="server" GroupingText="IOW head details" CssClass="tis-card tis-card--legend tis-split__detail" DefaultButton="btnSave">
            <div class="tis-card__body">
                <div class="tis-form-grid tis-form-grid--1">
                    <asp:Panel ID="Panel1" runat="server" CssClass="tis-form-grid tis-form-grid--2">
                        <div class="tis-field tis-field--full">
                            <asp:Label ID="lblIOWHeadCode" runat="server" Text="IOW code" AssociatedControlID="txtIOWHeadCode" CssClass="tis-label" />
                            <asp:TextBox ID="txtIOWHeadCode" runat="server" MaxLength="16" />
                        </div>
                        <div class="tis-field">
                            <asp:Label ID="lblGroup" runat="server" Text="Group" AssociatedControlID="ddlGroup" CssClass="tis-label" />
                            <asp:DropDownList ID="ddlGroup" runat="server" Visible="true" DataTextField="GroupName" DataValueField="GroupCode"
                                AutoPostBack="true" OnSelectedIndexChanged="Groupddlchanged" />
                        </div>
                        <div class="tis-field">
                            <asp:Label ID="lblSubGroup" runat="server" Text="Sub-group" AssociatedControlID="ddlsubGroup" CssClass="tis-label" />
                            <asp:DropDownList ID="ddlsubGroup" runat="server" DataTextField="SubGroupName" DataValueField="SubGroupCode" Visible="true" />
                        </div>
                    </asp:Panel>
                    <div class="tis-field">
                        <asp:Label ID="lblIOWName" runat="server" Text="IOW head name" AssociatedControlID="txtIOWHeadName" CssClass="tis-label" />
                        <asp:TextBox ID="txtIOWHeadName" runat="server" MaxLength="6000" TextMode="MultiLine" Rows="8" />
                    </div>
                </div>
            </div>
            <div class="tis-card__footer">
                <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" CssClass="tis-btn tis-btn--ghost" />
                <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" CssClass="tis-btn tis-btn--primary" />
            </div>
        </asp:Panel>
    </div>
</div>
</asp:Content>
