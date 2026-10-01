<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.Master" AutoEventWireup="true" CodeFile="FinancialYearMaster.aspx.cs" Inherits="FinancialYearMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="tis-page">
    <span class="tis-hidden"><asp:Label ID="lblSeparator" runat="server" /><asp:Label ID="lblSeparator1" runat="server" /><asp:Label ID="lblSeparator2" runat="server" /></span>
    <div class="tis-page-header">
        <div class="tis-page-header__text">
            <span class="tis-eyebrow">Masters</span>
            <h1 class="tis-page-header__title"><asp:Label ID="lblFinancialYearmas" runat="server" Text="Financial Year Master" /></h1>
            <p class="tis-page-header__desc">Financial years and their date ranges. The fiscal year is taken from the from date.</p>
        </div>
    </div>

    <div class="tis-split">
        <asp:Panel ID="Pnlgv" runat="server" CssClass="tis-card tis-split__list">
            <div class="tis-card__header">
                <div class="tis-card__heading">
                    <div class="tis-card__title">Financial years</div>
                    <div class="tis-card__subtitle">Use Edit on a row to change its dates in place (dd/mm/yyyy).</div>
                </div>
                <asp:Button ID="btnNew" runat="server" Text="New year" CssClass="tis-btn tis-btn--primary tis-btn--sm"
                    OnClick="btnNew_Click" CausesValidation="false" />
            </div>
            <div class="tis-table-wrap tis-table-wrap--tall">
                <asp:GridView ID="GrdFinancialYearMaster" runat="server" AutoGenerateColumns="False"
                    OnRowCancelingEdit="GrdFinancialYearMaster_RowCancelingEdit"
                    OnRowEditing="GrdFinancialYearMaster_RowEditing"
                    OnRowUpdating="GrdFinancialYearMaster_RowUpdating">
                    <Columns>
                        <asp:TemplateField HeaderText="Id" Visible="False">
                            <ItemTemplate><asp:Label ID="lblFinancialYearId" runat="server" Text='<%# Eval("FinancialYearId") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fiscal year" Visible="true" ItemStyle-CssClass="strong">
                            <ItemTemplate><asp:Label ID="lblFiscalDate" runat="server" Text='<%# Eval("FiscalYear") %>' /></ItemTemplate>
                            <EditItemTemplate><asp:TextBox ID="txtFiscalDate" runat="server" Text='<%# Bind("FiscalYear") %>' ReadOnly="false" /></EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="From" Visible="true" ItemStyle-CssClass="nowrap">
                            <ItemTemplate><asp:Label ID="lblFromDate" runat="server" Text='<%# Eval("FromDate","{0:dd/MM/yyyy}") %>' /></ItemTemplate>
                            <EditItemTemplate><asp:TextBox ID="txtFromDate" runat="server" Text='<%# Bind("FromDate","{0:dd/MM/yyyy}") %>' ReadOnly="false" /></EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="To" Visible="true" ItemStyle-CssClass="nowrap">
                            <ItemTemplate><asp:Label ID="lblToDate" runat="server" Text='<%# Eval("ToDate","{0:dd/MM/yyyy}") %>' /></ItemTemplate>
                            <EditItemTemplate><asp:TextBox ID="txtToDate" runat="server" Text='<%# Bind("ToDate","{0:dd/MM/yyyy}") %>' ReadOnly="false" /></EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate><asp:CheckBox ID="chkActive" runat="server" CssClass="tis-status" Checked='<%# Eval("IsActive") %>' Enabled="false" /></ItemTemplate>
                            <EditItemTemplate><asp:CheckBox ID="chkIsActive" runat="server" Text="Active" Checked='<%# Bind("IsActive") %>' /></EditItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField HeaderText="Edit" ShowEditButton="True" ItemStyle-CssClass="actions" />
                        <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" Visible="False" ItemStyle-CssClass="actions" />
                    </Columns>
                    <EmptyDataTemplate>
                        <tis:EmptyState ID="emptyYears" runat="server" Icon="calendar" Title="No financial years yet"
                            Text="Use New year to add the first financial year." />
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </asp:Panel>

        <asp:Panel ID="pnlAdd" runat="server" CssClass="tis-card tis-split__detail">
            <div class="tis-card__header">
                <div class="tis-card__heading">
                    <div class="tis-card__title">Add financial year</div>
                    <div class="tis-card__subtitle">You will be asked to confirm before it is saved.</div>
                </div>
            </div>
            <div class="tis-card__body">
                <div class="tis-form-grid tis-form-grid--2">
                    <div class="tis-field">
                        <asp:Label ID="lblFromDate" runat="server" Text="From date" AssociatedControlID="txtFromDate" CssClass="tis-label" />
                        <div class="tis-input-group">
                            <asp:TextBox ID="txtFromDate" runat="server" Text="" />
                            <asp:ImageButton ID="imgfromdate" runat="server" ImageUrl="~/Images/Calendar.gif" AlternateText="Pick from date" />
                        </div>
                        <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtFromdate"
                            runat="server" PopupButtonID="imgfromdate" Format="dd/MM/yyyy"></asp:CalendarExtender>
                    </div>
                    <div class="tis-field">
                        <asp:Label ID="lblToDate" runat="server" Text="To date" AssociatedControlID="txtToDate" CssClass="tis-label" />
                        <div class="tis-input-group">
                            <asp:TextBox ID="txtToDate" runat="server" Text="" />
                            <asp:ImageButton ID="imgToDate" runat="server" ImageUrl="~/Images/Calendar.gif" AlternateText="Pick to date" />
                        </div>
                        <asp:CalendarExtender ID="CalendarExtender2" TargetControlID="txtToDate" runat="server"
                            PopupButtonID="imgToDate" Format="dd/MM/yyyy"></asp:CalendarExtender>
                    </div>
                    <div class="tis-field tis-field--check">
                        <asp:CheckBox ID="chkIsActive" runat="server" Checked="false" Text="IsActive" Visible="true" />
                    </div>
                </div>
                <asp:HiddenField ID="HidCount" runat="server" Value="0" />
            </div>
            <div class="tis-card__footer">
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="tis-btn tis-btn--primary" />
            </div>
        </asp:Panel>
    </div>
</div>
</asp:Content>
