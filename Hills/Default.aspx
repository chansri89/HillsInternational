<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ MasterType VirtualPath="~/MasterPage1.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="tis-page">
        <section class="tis-hero">
            <div class="tis-hero__text">
                <span class="tis-eyebrow"><asp:Label ID="lblToday" runat="server" /></span>
                <h1 class="tis-hero__title"><asp:Label ID="lblWelcomeCompany" runat="server" Text="Welcome to Tender Insight System" /></h1>
                <p class="tis-hero__lead">Pick a module to get started, or press <kbd class="tis-kbd">Ctrl K</kbd> to jump straight to any page.</p>
            </div>
            <div class="tis-cluster">
                <button type="button" class="tis-btn tis-btn--primary" data-tis-palette="1">
                    <svg class="tis-icon" aria-hidden="true"><use href="Images/icons.svg#i-search"></use></svg>Search pages
                </button>
            </div>
        </section>

        <asp:Repeater ID="rptModules" runat="server" EnableViewState="false">
            <HeaderTemplate><div class="tis-modules"></HeaderTemplate>
            <ItemTemplate>
                <section class="tis-module">
                    <div class="tis-module__head">
                        <span class="tis-module__icon"><svg class="tis-icon" aria-hidden="true"><use href="Images/icons.svg#i-<%# Eval("Icon") %>"></use></svg></span>
                        <div>
                            <div class="tis-module__title"><%# Eval("TextHtml") %></div>
                            <div class="tis-module__count"><%# Eval("CountText") %></div>
                        </div>
                    </div>
                    <ul class="tis-module__links">
                        <asp:Repeater ID="rptModuleLinks" runat="server" EnableViewState="false" DataSource='<%# ((TisNavGroup)Container.DataItem).Items %>'>
                            <ItemTemplate>
                                <li class="<%# Eval("ModuleSectionClass") %>"><%# Eval("SectionHtml") %></li>
                                <li><a href="<%# Eval("UrlAttr") %>"><%# Eval("TextHtml") %><svg class="tis-icon" aria-hidden="true"><use href="Images/icons.svg#i-arrow-right"></use></svg></a></li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </section>
            </ItemTemplate>
            <FooterTemplate></div></FooterTemplate>
        </asp:Repeater>

        <asp:Panel ID="pnlNoModules" runat="server" Visible="false" CssClass="tis-card">
            <tis:EmptyState ID="emptyModules" runat="server" Icon="lock" Title="No modules assigned yet"
                Text="Your account does not have access to any programs. Ask an administrator to assign you a role." />
        </asp:Panel>

        <section class="tis-tips" aria-label="Tips">
            <div class="tis-tip">
                <svg class="tis-icon" aria-hidden="true"><use href="Images/icons.svg#i-keyboard"></use></svg>
                <div><strong>Jump anywhere</strong>Press Ctrl K (or /) and type part of a page name.</div>
            </div>
            <div class="tis-tip">
                <svg class="tis-icon" aria-hidden="true"><use href="Images/icons.svg#i-panel-left"></use></svg>
                <div><strong>More room for grids</strong>Collapse the sidebar to an icon rail from the top bar.</div>
            </div>
            <div class="tis-tip">
                <svg class="tis-icon" aria-hidden="true"><use href="Images/icons.svg#i-moon"></use></svg>
                <div><strong>Dark mode</strong>Switch themes from the top bar; your choice is remembered.</div>
            </div>
        </section>
    </div>
</asp:Content>
