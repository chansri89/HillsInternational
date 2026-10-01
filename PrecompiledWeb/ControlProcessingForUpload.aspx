<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.Master" AutoEventWireup="true" CodeFile="ControlProcessingForUpload.aspx.cs" Inherits="ControlProcessingForUpload" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="tis-page">
    <div class="tis-page-header">
        <div class="tis-page-header__text">
            <span class="tis-eyebrow">Tender</span>
            <h1 class="tis-page-header__title"><asp:Label ID="lblCustomerMaster" runat="server" Text="Monthly closure control" /></h1>
            <p class="tis-page-header__desc">Review the current processing month for a company and set the number of working days in it.</p>
        </div>
    </div>
    <span class="tis-hidden"><asp:Label ID="lblSeparator1" runat="server" /></span>

    <asp:Panel ID="Pnlgv" runat="server" CssClass="tis-card">
        <div class="tis-card__header">
            <div class="tis-card__heading">
                <div class="tis-card__title">Processing month change</div>
                <div class="tis-card__subtitle">Working days cannot exceed the number of days in the month.</div>
            </div>
        </div>
        <div class="tis-card__body">
            <div class="tis-toolbar">
                <div class="tis-field tis-toolbar__grow">
                    <asp:Label ID="lblCompName" runat="server" Text="Company" AssociatedControlID="ddlCompanyName" CssClass="tis-label" />
                    <asp:DropDownList ID="ddlCompanyName" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCompanyChanged" />
                </div>
            </div>
        </div>
        <div id="divCustomer" runat="server" class="tis-table-wrap tis-table-wrap--tall">
            <asp:GridView ID="GrdControlProcess" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:TemplateField HeaderText="CompanyId" Visible="false">
                        <ItemTemplate><asp:Label ID="lblCompanyId" runat="server" Text='<%# Eval("CompanyId") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ControlProcessingId" Visible="false">
                        <ItemTemplate><asp:Label ID="lblControlProcessingId" runat="server" Text='<%# Eval("ControlProcessingId") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Company" Visible="false">
                        <ItemTemplate><asp:Label ID="lblCompanyName" runat="server" Text='<%# Eval("CompanyName") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Year month" Visible="true" ItemStyle-CssClass="code">
                        <ItemTemplate><asp:Label ID="lblYYYYMM" runat="server" Text='<%# Eval("PaySlipYearMonth") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PF from" Visible="true" HeaderStyle-CssClass="num" ItemStyle-CssClass="num">
                        <ItemTemplate><asp:Label ID="lblPFFrom" runat="server" Text='<%# Eval("PFPeriodFrom","{0:dd-MM-yyyy}") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PF to" Visible="true" HeaderStyle-CssClass="num" ItemStyle-CssClass="num">
                        <ItemTemplate><asp:Label ID="lblPFTo" runat="server" Text='<%# Eval("PFPeriodTo","{0:dd-MM-yyyy}") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ESI from" Visible="true" HeaderStyle-CssClass="num" ItemStyle-CssClass="num">
                        <ItemTemplate><asp:Label ID="lblESIFrom" runat="server" Text='<%# Eval("ESIPeriodFrom","{0:dd-MM-yyyy}") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ESI to" Visible="true" HeaderStyle-CssClass="num" ItemStyle-CssClass="num">
                        <ItemTemplate><asp:Label ID="lblESITo" runat="server" Text='<%# Eval("ESIPeriodTo","{0:dd-MM-yyyy}") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Overtime from" Visible="true" HeaderStyle-CssClass="num" ItemStyle-CssClass="num">
                        <ItemTemplate><asp:Label ID="lblOvertimeFrom" runat="server" Text='<%# Eval("OverTimePeriodFrom","{0:dd-MM-yyyy}") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Overtime to" Visible="true" HeaderStyle-CssClass="num" ItemStyle-CssClass="num">
                        <ItemTemplate><asp:Label ID="lblOvertimeTo" runat="server" Text='<%# Eval("OverTimePeriodTo","{0:dd-MM-yyyy}") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Working days" Visible="true" HeaderStyle-CssClass="num" ItemStyle-CssClass="num">
                        <ItemTemplate><asp:TextBox ID="txtWorkingDays" runat="server" Text='<%# Eval("WorkingDays") %>' /></ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <tis:EmptyState ID="emptyControlProcess" runat="server" Icon="calendar" Title="No processing month yet"
                        Text="Choose a company. If nothing appears, ask an admin to create its processing month." />
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
        <div class="tis-card__footer">
            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="tis-btn tis-btn--primary" />
        </div>
    </asp:Panel>
</div>
</asp:Content>
