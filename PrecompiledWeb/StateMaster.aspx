<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.master" AutoEventWireup="true" CodeFile="StateMaster.aspx.cs" Inherits="StateMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="tis-page">
    <div class="tis-page-header">
        <div class="tis-page-header__text">
            <span class="tis-eyebrow">Masters</span>
            <h1 class="tis-page-header__title"><asp:Label ID="lblStatMas" runat="server" Text="State Master" /></h1>
            <p class="tis-page-header__desc">States used in client, location and GST addresses.</p>
        </div>
    </div>

    <div class="tis-split">
        <asp:Panel ID="Pnlgv" runat="server" CssClass="tis-card tis-split__list">
            <div class="tis-card__header">
                <div class="tis-card__heading">
                    <div class="tis-card__title">States</div>
                    <div class="tis-card__subtitle">Use Edit on a row to change it in place.</div>
                </div>
                <asp:Button ID="btnNew" runat="server" Text="New state" CssClass="tis-btn tis-btn--primary tis-btn--sm"
                    OnClick="btnNew_Click" CausesValidation="false" />
            </div>
            <div class="tis-table-wrap tis-table-wrap--tall">
                <asp:GridView ID="GrdStateMaster" runat="server" AutoGenerateColumns="False"
                    OnRowCancelingEdit="GrdStateMaster_RowCancelingEdit"
                    OnRowEditing="GrdStateMaster_RowEditing"
                    OnRowUpdating="GrdStateMaster_RowUpdating">
                    <Columns>
                        <asp:TemplateField HeaderText="StateType Id" Visible="False">
                            <ItemTemplate><asp:Label ID="lblStateId" runat="server" Text='<%# Eval("StateId") %>' /></ItemTemplate>
                            <EditItemTemplate><asp:TextBox ID="txtStateId" runat="server" Text='<%# Bind("StateId") %>' ReadOnly="true" /></EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="State" ItemStyle-CssClass="strong">
                            <ItemTemplate><asp:Label ID="lblStateName" runat="server" Text='<%# Eval("StateName") %>' /></ItemTemplate>
                            <EditItemTemplate><asp:TextBox ID="txtStateName" runat="server" Text='<%# Bind("StateName") %>' /></EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Short name" ItemStyle-CssClass="code">
                            <ItemTemplate><asp:Label ID="lblStateShName" runat="server" Text='<%# Eval("StateShortName") %>' /></ItemTemplate>
                            <EditItemTemplate><asp:TextBox ID="txtStatehName" runat="server" Text='<%# Bind("StateShortName") %>' MaxLength="5" /></EditItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField HeaderText="Edit" ShowEditButton="True" ItemStyle-CssClass="actions" />
                        <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" Visible="False" ItemStyle-CssClass="actions" />
                    </Columns>
                    <EmptyDataTemplate>
                        <tis:EmptyState ID="emptyStates" runat="server" Icon="map" Title="No states yet"
                            Text="Use New state to add the first state." />
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </asp:Panel>

        <asp:Panel ID="pnlAdd" runat="server" GroupingText="Add State" CssClass="tis-card tis-card--legend tis-split__detail" DefaultButton="btnSave">
            <div class="tis-card__body">
                <div class="tis-form-grid tis-form-grid--1">
                    <div class="tis-field">
                        <asp:Label ID="lblStateName" runat="server" Text="State name" AssociatedControlID="txtStatetName" CssClass="tis-label" />
                        <asp:TextBox ID="txtStatetName" runat="server" />
                    </div>
                    <div class="tis-field">
                        <asp:Label ID="lblStateShortName" runat="server" Text="Short name" AssociatedControlID="txtStateShortName" CssClass="tis-label" />
                        <asp:TextBox ID="txtStateShortName" runat="server" MaxLength="5" />
                        <span class="tis-help">Up to 5 characters, for example TN.</span>
                    </div>
                </div>
            </div>
            <div class="tis-card__footer">
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="tis-btn tis-btn--primary" />
            </div>
        </asp:Panel>
    </div>
</div>
</asp:Content>
