<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.Master" AutoEventWireup="true" CodeFile="A_ProjectInputIOWCommonFactor.aspx.cs" Inherits="A_ProjectInputIOWCommonFactor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="tis-page" data-density="compact">
    <div class="tis-page-header">
        <div class="tis-page-header__text">
            <span class="tis-eyebrow">Project Input</span>
            <h1 class="tis-page-header__title"><asp:Label ID="lblCRAMCommonFactorMaster" runat="server" Text="Project input IOW common factors" /></h1>
            <p class="tis-page-header__desc">Percentage factors applied across IOWs for one project and rate month. Choose the project and month, then view its factors.</p>
        </div>
    </div>

    <asp:Panel ID="pnlPendind" runat="server" CssClass="tis-card" DefaultButton="btnView">
        <div class="tis-card__body">
            <div class="tis-toolbar">
                <div class="tis-field">
                    <asp:Label ID="lblCompany" runat="server" Text="Company" Visible="true" AssociatedControlID="ddlCompany" CssClass="tis-label" />
                    <asp:DropDownList ID="ddlCompany" runat="server" DataTextField="CompanyName" DataValueField="CompanyId"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlCompanyChanged" />
                </div>
                <div class="tis-field">
                    <asp:Label ID="lblClients" runat="server" Text="Client" Visible="true" AssociatedControlID="ddlClient" CssClass="tis-label" />
                    <asp:DropDownList ID="ddlClient" runat="server" Visible="true" AutoPostBack="true" OnSelectedIndexChanged="ddlClientChanged"
                        DataTextField="ClientName" DataValueField="ClientCode" />
                </div>
                <div class="tis-field tis-toolbar__grow">
                    <asp:Label ID="lblProject" runat="server" Text="Project" Visible="true" AssociatedControlID="ddlProject" CssClass="tis-label" />
                    <asp:DropDownList ID="ddlProject" runat="server" DataTextField="ProjectName" DataValueField="ProjectCode" />
                </div>
                <div class="tis-field">
                    <asp:Label ID="lblYearMonth" runat="server" Text="Year month" Visible="true" AssociatedControlID="ddlForYearMonth" CssClass="tis-label" />
                    <asp:DropDownList ID="ddlForYearMonth" runat="server" Visible="true" DataTextField="ForYearMonth" DataValueField="ForYearMonth" />
                </div>
                <asp:Button ID="btnView" runat="server" OnClick="btnView_Click" Text="View" CssClass="tis-btn tis-btn--primary" />
                <asp:Button ID="Button1" runat="server" OnClick="btnClear_Click" Text="Clear" CssClass="tis-btn tis-btn--ghost" />
            </div>
        </div>
    </asp:Panel>

    <div class="tis-split">
        <asp:Panel ID="Pnlgv" runat="server" CssClass="tis-card tis-split__list">
            <div class="tis-card__header">
                <div class="tis-card__heading">
                    <div class="tis-card__title">Common factors</div>
                    <div class="tis-card__subtitle">Select a factor name to edit it.</div>
                </div>
                <asp:Button ID="btnNew" runat="server" Text="New factor" CssClass="tis-btn tis-btn--primary tis-btn--sm"
                    OnClick="btnNew_Click" CausesValidation="false" />
            </div>
            <div id="divCRAMCommonFactor" runat="server" class="tis-table-wrap tis-table-wrap--tall">
                <asp:GridView ID="GrdIOWCommonFactor" runat="server" AutoGenerateColumns="False" OnRowCommand="GrdIOWCommonFactor_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="CompanyId" Visible="false">
                            <ItemTemplate><asp:Label ID="lblCompanyId" runat="server" Text='<%# Eval("CompanyId") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ClientCode" Visible="false">
                            <ItemTemplate><asp:Label ID="lblClientCode" runat="server" Text='<%# Eval("ClientCode") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ProjectCode" Visible="false">
                            <ItemTemplate><asp:Label ID="lblProjectCode" runat="server" Text='<%# Eval("ProjectCode") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="YearMonth" Visible="false">
                            <ItemTemplate><asp:Label ID="lblYearMonth" runat="server" Text='<%# Eval("ForYearMonth") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Comm Fact Id" Visible="false">
                            <ItemTemplate><asp:Label ID="lblIOWCommonFactorId" runat="server" Text='<%# Eval("IOWCommonFactorId") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Comm Fact " Visible="false">
                            <ItemTemplate><asp:Label ID="lblIOWCommonFactor" runat="server" Text='<%# Eval("IOWCommonFactor") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Common factor" Visible="true" ItemStyle-CssClass="wrap">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkCommonFactorName" runat="server" CssClass="tis-link tis-link--strong"
                                    CommandArgument='<%#Eval("IOWCommonFactorId")%>' CommandName="selectCommonFactor" Text='<%#Eval("IOWCommonFactor") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Seq #" Visible="true" HeaderStyle-CssClass="num" ItemStyle-CssClass="num">
                            <ItemTemplate><asp:Label ID="lblSequenceNumber" runat="server" Text='<%# Eval("SequenceNumber") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Seq group" Visible="true" HeaderStyle-CssClass="num" ItemStyle-CssClass="num">
                            <ItemTemplate><asp:Label ID="lblSequenceGroup" runat="server" Text='<%# Eval("SequenceGroup") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Factor %" Visible="true" HeaderStyle-CssClass="num" ItemStyle-CssClass="num">
                            <ItemTemplate><asp:Label ID="lblFactorPercentage" runat="server" Text='<%# Eval("FactorPercentage") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Effective %" Visible="true" HeaderStyle-CssClass="num" ItemStyle-CssClass="num">
                            <ItemTemplate><asp:Label ID="lblEffectivePercentage" runat="server" Text='<%# Eval("EffectivePercentage") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkActive" runat="server" CssClass="tis-status" Checked='<%# Eval("IsActive") %>' Enabled="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <tis:EmptyState ID="emptyFactors" runat="server" Icon="percent" Title="No common factors"
                            Text="Use New factor to add the first common factor for this project and month." />
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </asp:Panel>

        <asp:Panel ID="pnlAdd" runat="server" CssClass="tis-card tis-card--legend tis-split__detail" GroupingText="Add Common Factor" DefaultButton="btnSave">
            <div class="tis-card__body">
                <div class="tis-form-grid tis-form-grid--2">
                    <div class="tis-field tis-field--full">
                        <asp:Label ID="lblIOWCommonFactorName" runat="server" Text="Common factor name" AssociatedControlID="txtIOWCommonFactor" CssClass="tis-label" />
                        <asp:TextBox ID="txtIOWCommonFactor" runat="server" TextMode="MultiLine" Rows="3" />
                    </div>
                    <div class="tis-field">
                        <asp:Label ID="lblSequenceNumber" runat="server" Text="Sequence no." AssociatedControlID="txtSequenceNumber" CssClass="tis-label" />
                        <asp:TextBox ID="txtSequenceNumber" runat="server" placeholder="e.g. 010" />
                        <span class="tis-help">Exactly 3 digits, unique in this list.</span>
                    </div>
                    <div class="tis-field">
                        <asp:Label ID="lblFactorPercentage" runat="server" Text="Factor %" AssociatedControlID="txtFactorPercentage" CssClass="tis-label" />
                        <asp:TextBox ID="txtFactorPercentage" runat="server" />
                    </div>
                    <div class="tis-field">
                        <asp:Label ID="lblEffectivePercentage" runat="server" Text="Effective %" AssociatedControlID="txtEffectivePercentage" CssClass="tis-label" />
                        <asp:TextBox ID="txtEffectivePercentage" runat="server" />
                        <span class="tis-help">Must be equal to or more than the factor %.</span>
                    </div>
                    <div class="tis-field tis-field--check">
                        <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" Visible="False" />
                    </div>
                </div>
                <asp:TextBox ID="txtIOWCommonFactorId" runat="server" Visible="false" />
            </div>
            <div class="tis-card__footer">
                <asp:Button ID="btnClear" runat="server" Text="Close" OnClick="btnClear_Click" CssClass="tis-btn tis-btn--ghost" />
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="tis-btn tis-btn--primary" />
            </div>
        </asp:Panel>
    </div>
</div>
</asp:Content>
