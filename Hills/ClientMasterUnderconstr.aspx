<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.Master" AutoEventWireup="true" CodeFile="ClientMasterUnderconstr.aspx.cs" Inherits="ClientMasterUnderconstr" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="tis-page">
    <div class="tis-page-header">
        <div class="tis-page-header__text">
            <span class="tis-eyebrow">Masters</span>
            <h1 class="tis-page-header__title">Client master</h1>
            <p class="tis-page-header__desc">This client master screen is not in use yet. Manage clients and tenderers from the Clients and tenderers screen.</p>
        </div>
    </div>

    <div class="tis-card">
        <tis:EmptyState ID="emptyClientMaster" runat="server" Icon="flag" Title="This screen is being prepared"
            Text="It will be available in a later release." />
        <span class="tis-hidden"><asp:Label ID="lblMessage" runat="server" Text="Under Construction" /></span>
    </div>
</div>
</asp:Content>
