<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.Master" AutoEventWireup="true" CodeFile="ProjectClosure.aspx.cs" Inherits="ProjectClosure" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="tis-page">
    <div class="tis-page-header">
        <div class="tis-page-header__text">
            <span class="tis-eyebrow">Tender</span>
            <h1 class="tis-page-header__title">Project closure</h1>
            <p class="tis-page-header__desc">Close out a project once its tender work is complete.</p>
        </div>
    </div>

    <div class="tis-card">
        <tis:EmptyState ID="emptyProjectClosure" runat="server" Icon="flag" Title="This screen is being prepared"
            Text="Project closure will be available in a later release." />
        <span class="tis-hidden"><asp:Label ID="lblMessage" runat="server" Text="Under Construction" /></span>
    </div>
</div>
</asp:Content>
