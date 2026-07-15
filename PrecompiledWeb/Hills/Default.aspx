<%@ page language="C#" masterpagefile="~/MasterPage1.Master" autoeventwireup="true" inherits="_Default, App_Web_sx5sst5a" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="Panel4" runat="server" CssClass="XSmall">
        <div class="tis-card" style="text-align:center; padding:var(--tis-space-8) var(--tis-space-5);">
            <div style="font-size:3rem; color:var(--tis-primary); margin-bottom:var(--tis-space-3); line-height:1;">&#127970;</div>
            <h1 style="margin:0 0 var(--tis-space-2);">
                <asp:Label ID="lblWelcomeCompany" runat="server" Text="Welcome to Tender Insight System" ForeColor="#3333CC" />
            </h1>
            <p style="color:var(--tis-text-muted); font-size:var(--tis-fs-md); margin:0;">
                Select an option from the menu to begin working with tenders, masters and reports.
            </p>
        </div>
    </asp:Panel>
</asp:Content>
