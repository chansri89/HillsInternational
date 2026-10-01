<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.Master" AutoEventWireup="true" CodeFile="IOWCommonFactorMaster.aspx.cs" Inherits="IOWCommonFactorMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="tis-page">
    <div class="tis-page-header">
        <div class="tis-page-header__text">
            <span class="tis-eyebrow">CRAM</span>
            <h1 class="tis-page-header__title"><asp:Label ID="lblCRAMCommonFactorMaster" runat="server" Text="IOW common factors" /></h1>
            <p class="tis-page-header__desc">Percentage factors applied across items of work, in sequence order. Choose a company to see and edit its factors.</p>
        </div>
    </div>

    <asp:Panel ID="pnlPendind" runat="server" CssClass="tis-card">
        <div class="tis-card__body">
            <div class="tis-toolbar">
                <div class="tis-field">
                    <asp:Label ID="lblCompany" runat="server" Text="Company" Visible="true" AssociatedControlID="ddlCompany" CssClass="tis-label" />
                    <asp:DropDownList ID="ddlCompany" runat="server" Visible="true" DataTextField="CompanyName" DataValueField="CompanyId"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlCompanyChanged" />
                </div>
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
                        <asp:TemplateField HeaderText="Comm Fact Id" Visible="false">
                            <ItemTemplate><asp:Label ID="lblIOWCommonFactorId" runat="server" Text='<%# Eval("IOWCommonFactorId") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Comm Fact" Visible="false">
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
                        <tis:EmptyState ID="emptyCommonFactors" runat="server" Icon="percent" Title="No common factors yet"
                            Text="Use New factor to add the first common factor for this company." />
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </asp:Panel>

        <asp:Panel ID="pnlAdd" runat="server" GroupingText="Add Common Factor" CssClass="tis-card tis-card--legend tis-split__detail" DefaultButton="btnSave">
            <div class="tis-card__body">
                <div class="tis-form-grid tis-form-grid--3">
                    <div class="tis-field tis-field--full">
                        <asp:Label ID="lblIOWCommonFactorName" runat="server" Text="Common factor name" AssociatedControlID="txtIOWCommonFactor" CssClass="tis-label" />
                        <asp:TextBox ID="txtIOWCommonFactor" runat="server" TextMode="MultiLine" Rows="3" />
                    </div>
                    <div class="tis-field">
                        <asp:Label ID="lblSequenceNumber" runat="server" Text="Seq. no" AssociatedControlID="txtSequenceNumber" CssClass="tis-label" />
                        <asp:TextBox ID="txtSequenceNumber" runat="server" placeholder="001" />
                    </div>
                    <div class="tis-field">
                        <asp:Label ID="lblFactorPercentage" runat="server" Text="Factor %" AssociatedControlID="txtFactorPercentage" CssClass="tis-label" />
                        <asp:TextBox ID="txtFactorPercentage" runat="server" />
                    </div>
                    <div class="tis-field">
                        <asp:Label ID="lblEffectivePercentage" runat="server" Text="Effective %" AssociatedControlID="txtEffectivePercentage" CssClass="tis-label" />
                        <asp:TextBox ID="txtEffectivePercentage" runat="server" />
                    </div>
                    <div class="tis-field tis-field--full">
                        <span class="tis-help">Sequence numbers are exactly 3 digits and must be unique. Effective % must be at least the factor %.</span>
                    </div>
                    <div class="tis-field tis-field--check">
                        <asp:CheckBox ID="chkIsActive" runat="server" Text="Active" Visible="False" />
                    </div>
                </div>
                <asp:TextBox ID="txtIOWCommonFactorId" runat="server" Visible="false" />
            </div>
            <div class="tis-card__footer">
                <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" CssClass="tis-btn tis-btn--ghost" />
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="tis-btn tis-btn--primary" />
            </div>
        </asp:Panel>
    </div>
</div>
</asp:Content>
