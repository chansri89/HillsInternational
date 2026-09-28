<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.Master" AutoEventWireup="true" CodeFile="ClientProject.aspx.cs" Inherits="ClientProject" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="tis-page">
    <div class="tis-page-header">
        <div class="tis-page-header__text">
            <span class="tis-eyebrow">Masters</span>
            <h1 class="tis-page-header__title"><asp:Label ID="lblClientMaster" runat="server" Text="Client Project Master Entry" /></h1>
            <p class="tis-page-header__desc">Projects run for each client. Choose a company and client, then select Go to see and edit its projects.</p>
        </div>
    </div>

    <asp:Panel ID="pnlPendind" runat="server" CssClass="tis-card" DefaultButton="btnGo">
        <div class="tis-card__body">
            <div class="tis-toolbar">
                <div class="tis-field">
                    <asp:Label ID="lblCompany" runat="server" Text="Company" AssociatedControlID="ddlCompany" CssClass="tis-label" />
                    <asp:DropDownList ID="ddlCompany" runat="server" DataTextField="CompanyName" DataValueField="CompanyId"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlCompanyChanged" />
                </div>
                <div class="tis-field tis-toolbar__grow">
                    <asp:Label ID="lblClntCode" runat="server" Text="Client" AssociatedControlID="ddlClient" CssClass="tis-label" />
                    <asp:DropDownList ID="ddlClient" runat="server" DataTextField="ClientName" DataValueField="ClientCode" />
                </div>
                <asp:Button ID="btnGo" runat="server" OnClick="btnGo_Click" Text="Go" CssClass="tis-btn" />
            </div>
        </div>
    </asp:Panel>

    <div class="tis-split tis-split--wide-detail">
        <asp:Panel ID="Pnlgv" runat="server" CssClass="tis-card tis-split__list">
            <div class="tis-card__header">
                <div class="tis-card__heading">
                    <div class="tis-card__title">Client projects</div>
                    <div class="tis-card__subtitle">Select a project name to edit it.</div>
                </div>
                <asp:Button ID="btnNew" runat="server" Text="New project" CssClass="tis-btn tis-btn--primary tis-btn--sm"
                    OnClick="btnNew_Click" CausesValidation="false" />
            </div>
            <div id="divClient" runat="server" class="tis-table-wrap tis-table-wrap--tall">
                <asp:GridView ID="GrdProjMaster" runat="server" AutoGenerateColumns="False" OnRowCommand="GrdClient_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="ClientCode" Visible="false">
                            <ItemTemplate><asp:Label ID="lblClientCode" runat="server" Text='<%# Eval("ClientCode") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cli Proj Id" Visible="false">
                            <ItemTemplate><asp:Label ID="lblClientProjectId" runat="server" Text='<%# Eval("ClientProjectId") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sector SubGroup Id" Visible="false">
                            <ItemTemplate><asp:Label ID="lblProjectSectorSubGroupId" runat="server" Text='<%# Eval("ProjectSectorSubGroupId") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sector group" ItemStyle-CssClass="wrap">
                            <ItemTemplate><asp:Label ID="lblProjectSectorGroupName" runat="server" Text='<%# Eval("ProjectSectorGroupName") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sector class" ItemStyle-CssClass="wrap">
                            <ItemTemplate><asp:Label ID="lblProjectSectorClass" runat="server" Text='<%# Eval("ProjectSectorClass") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Code" ItemStyle-CssClass="code">
                            <ItemTemplate><asp:Label ID="lblProjectCode" runat="server" Text='<%# Eval("ProjectCode") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Proj Name" Visible="false">
                            <ItemTemplate><asp:Label ID="lblProjectName" runat="server" Text='<%# Eval("ProjectName") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Project" ItemStyle-CssClass="wrap">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkCustName" runat="server" CssClass="tis-link tis-link--strong" CommandArgument='<%#Eval("ProjectCode")%>'
                                    CommandName="selectProject" Text='<%#Eval("ProjectName") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Location" ItemStyle-CssClass="wrap">
                            <ItemTemplate><asp:Label ID="lblProjectLocation" runat="server" Text='<%# Eval("ProjectLocation") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="City">
                            <ItemTemplate><asp:Label ID="lblProjectCity" runat="server" Text='<%# Eval("ProjectCity") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="StateId" Visible="false">
                            <ItemTemplate><asp:Label ID="lblStateId" runat="server" Text='<%# Eval("StateId") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="State" Visible="true">
                            <ItemTemplate><asp:Label ID="lblStateName" runat="server" Text='<%# Eval("StateName") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Start" Visible="true" ItemStyle-CssClass="nowrap">
                            <ItemTemplate><asp:Label ID="lblStartDate" runat="server" Text='<%# Eval("StartDate","{0:dd-MM-yyyy}") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="End" ItemStyle-CssClass="nowrap">
                            <ItemTemplate><asp:Label ID="lblEndDate" runat="server" Text='<%# Eval("EndDate","{0:dd-MM-yyyy}") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Deviation (months)" Visible="true" HeaderStyle-CssClass="num" ItemStyle-CssClass="num">
                            <ItemTemplate><asp:Label ID="lblDeviation" runat="server" Text='<%# Eval("DeviationMonths") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tender period" Visible="true" ItemStyle-CssClass="nowrap">
                            <ItemTemplate><asp:Label ID="lblTenderDate" runat="server" Text='<%# Eval("TenderPeriod","{0:dd-MM-yyyy}") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Constr. start" Visible="true" ItemStyle-CssClass="nowrap">
                            <ItemTemplate><asp:Label ID="lblConstructionStart" runat="server" Text='<%# Eval("ConstructionStart","{0:dd-MM-yyyy}") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Constr. end" Visible="true" ItemStyle-CssClass="nowrap">
                            <ItemTemplate><asp:Label ID="lblConstructionCompleted" runat="server" Text='<%# Eval("ConstructionCompleted","{0:dd-MM-yyyy}") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate><asp:CheckBox ID="chkActive" runat="server" CssClass="tis-status" Checked='<%# Eval("IsActive") %>' Enabled="false" /></ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <tis:EmptyState ID="emptyProjects" runat="server" Icon="folder" Title="No projects yet"
                            Text="Use New project to add the first project for this client." />
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </asp:Panel>

        <asp:Panel ID="pnlAdd" runat="server" GroupingText="Add Client Project" CssClass="tis-card tis-card--legend tis-split__detail" DefaultButton="btnSave">
            <div class="tis-card__body">
                <div class="tis-form-grid tis-form-grid--2">
                    <div class="tis-field">
                        <asp:Label ID="lblProjectCode" runat="server" Text="Project code" AssociatedControlID="txtProjectCode" CssClass="tis-label" />
                        <asp:TextBox ID="txtProjectCode" runat="server" MaxLength="16" />
                    </div>
                    <div class="tis-field">
                        <asp:Label ID="lblDeviationMonths" runat="server" Text="Deviation months" AssociatedControlID="txtDeviationMonths" CssClass="tis-label" />
                        <asp:TextBox ID="txtDeviationMonths" runat="server" MaxLength="16" />
                    </div>
                    <div class="tis-field tis-field--full">
                        <asp:Label ID="lblProjectName" runat="server" Text="Project name" AssociatedControlID="txtProjectName" CssClass="tis-label" />
                        <asp:TextBox ID="txtProjectName" runat="server" MaxLength="256" TextMode="MultiLine" Rows="2" />
                    </div>
                    <div class="tis-field">
                        <asp:Label ID="lblProjectLocation" runat="server" Text="Location" AssociatedControlID="txtProjectLocation" CssClass="tis-label" />
                        <asp:TextBox ID="txtProjectLocation" runat="server" MaxLength="64" />
                    </div>
                    <div class="tis-field">
                        <asp:Label ID="lblProjectCity" runat="server" Text="City" AssociatedControlID="txtProjectCity" CssClass="tis-label" />
                        <asp:TextBox ID="txtProjectCity" runat="server" MaxLength="64" />
                    </div>
                    <div class="tis-field tis-field--full">
                        <asp:Label ID="lblState" runat="server" Text="State" AssociatedControlID="ddlState" CssClass="tis-label" />
                        <asp:DropDownList ID="ddlState" runat="server" DataTextField="StateName" DataValueField="StateId" />
                    </div>
                    <div class="tis-field">
                        <asp:Label ID="Label2" runat="server" Text="Sector group" AssociatedControlID="ddlSectorGroup" CssClass="tis-label" />
                        <asp:DropDownList ID="ddlSectorGroup" runat="server"
                            DataTextField="ProjectSectorGroupName" DataValueField="ProjectSectorGroupId"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlSectorGroupChanged" />
                    </div>
                    <div class="tis-field">
                        <asp:Label ID="Label1" runat="server" Text="Sector class" AssociatedControlID="ddlSectorClass" CssClass="tis-label" />
                        <asp:DropDownList ID="ddlSectorClass" runat="server"
                            DataTextField="ProjectSectorClass" DataValueField="ProjectSectorSubGroupId" />
                    </div>
                    <div class="tis-field">
                        <asp:Label ID="lblStartDate" runat="server" Text="Start date" AssociatedControlID="txtStartDate" CssClass="tis-label" />
                        <div class="tis-input-group">
                            <asp:TextBox ID="txtStartDate" runat="server" Text="" placeholder="dd-mm-yyyy" />
                            <asp:ImageButton ID="imgStartdate" runat="server" ImageUrl="~/Images/Calendar.gif" AlternateText="Pick start date" />
                        </div>
                        <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtStartDate"
                            runat="server" PopupButtonID="imgStartdate" Format="dd-MM-yyyy"></asp:CalendarExtender>
                    </div>
                    <div class="tis-field">
                        <asp:Label ID="lblEndDate" runat="server" Text="End date" AssociatedControlID="txtEndDate" CssClass="tis-label" />
                        <div class="tis-input-group">
                            <asp:TextBox ID="txtEndDate" runat="server" Text="" placeholder="dd-mm-yyyy" />
                            <asp:ImageButton ID="imgEnddate" runat="server" ImageUrl="~/Images/Calendar.gif" AlternateText="Pick end date" />
                        </div>
                        <asp:CalendarExtender ID="CalendarExtender2" TargetControlID="txtEndDate"
                            runat="server" PopupButtonID="imgEnddate" Format="dd-MM-yyyy"></asp:CalendarExtender>
                    </div>
                    <div class="tis-field tis-field--full">
                        <asp:Label ID="lblTenderPeriod" runat="server" Text="Tender period" AssociatedControlID="txtTenderPeriod" CssClass="tis-label" />
                        <div class="tis-input-group">
                            <asp:TextBox ID="txtTenderPeriod" runat="server" Text="" placeholder="dd-mm-yyyy" />
                            <asp:ImageButton ID="imgTenderPeriod" runat="server" ImageUrl="~/Images/Calendar.gif" AlternateText="Pick tender period date" />
                        </div>
                        <asp:CalendarExtender ID="CalendarExtender5" TargetControlID="txtTenderPeriod"
                            runat="server" PopupButtonID="imgTenderPeriod" Format="dd-MM-yyyy"></asp:CalendarExtender>
                        <span class="tis-help">Must fall between the start and end dates.</span>
                    </div>
                    <div class="tis-field">
                        <asp:Label ID="lblConstructionStart" runat="server" Text="Construction start" AssociatedControlID="txtConstructionStart" CssClass="tis-label" />
                        <div class="tis-input-group">
                            <asp:TextBox ID="txtConstructionStart" runat="server" Text="" placeholder="dd-mm-yyyy" />
                            <asp:ImageButton ID="imgConstructionStart" runat="server" ImageUrl="~/Images/Calendar.gif" AlternateText="Pick construction start date" />
                        </div>
                        <asp:CalendarExtender ID="CalendarExtender3" TargetControlID="txtConstructionStart"
                            runat="server" PopupButtonID="imgConstructionStart" Format="dd-MM-yyyy"></asp:CalendarExtender>
                    </div>
                    <div class="tis-field">
                        <asp:Label ID="lblConstrEnd" runat="server" Text="Construction end" AssociatedControlID="txtConstructionEndDate" CssClass="tis-label" />
                        <div class="tis-input-group">
                            <asp:TextBox ID="txtConstructionEndDate" runat="server" Text="" placeholder="dd-mm-yyyy" />
                            <asp:ImageButton ID="imgConstructionEnd" runat="server" ImageUrl="~/Images/Calendar.gif" AlternateText="Pick construction end date" />
                        </div>
                        <asp:CalendarExtender ID="CalendarExtender4" TargetControlID="txtConstructionEndDate"
                            runat="server" PopupButtonID="imgConstructionEnd" Format="dd-MM-yyyy"></asp:CalendarExtender>
                    </div>
                    <div class="tis-field tis-field--check">
                        <asp:CheckBox ID="chkIsActive" runat="server" Text="IsActive" Visible="False" />
                    </div>
                </div>
                <asp:TextBox ID="txtClientProjectId" runat="server" Visible="false" />
            </div>
            <div class="tis-card__footer">
                <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" CssClass="tis-btn tis-btn--ghost" />
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="tis-btn tis-btn--primary" />
            </div>
        </asp:Panel>
    </div>
</div>
</asp:Content>
