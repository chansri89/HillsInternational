<%@ page title="" language="C#" masterpagefile="~/MasterPage1.Master" autoeventwireup="true" inherits="RoleNameChange, App_Web_hmntzrlf" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="tis-card">
<h1 class="tis-page-title">
    <asp:Label ID="lblrolenamechange" runat="server" Text="Role Name Change" />
</h1>

    <asp:panel ID="Pnlgv" runat="server" CssClass="XSmall">
        <div class="tis-table-wrap">
    <asp:GridView ID="GrdRole" runat="server" CellPadding="3" 
            Width="267px" AutoGenerateColumns="False" Height="16px" GridLines="Vertical" BackColor="White" BorderColor="#999999" 
                BorderStyle="None" BorderWidth="1px" 
                onrowcancelingedit="GrdRole_RowCancelingEdit"                
                onrowediting="GrdRole_RowEditing" 
                onrowupdating="GrdRole_RowUpdating" onrowdeleting="GrdRole_RowDeleting">
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" 
            HorizontalAlign="Left" />
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
         <asp:TemplateField HeaderText="RoleId" Visible="False">
            <ItemTemplate>
            <asp:Label ID="lblRoleId" runat="server" Text='<%# Eval("RoleId") %>'  ></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtRoleId" runat="server" Text='<%# Bind("RoleId") %>' ReadOnly="true" ></asp:TextBox></EditItemTemplate>
                <HeaderStyle Width="40px" />
            </asp:TemplateField>

             <asp:TemplateField HeaderText="RoleName">
            <ItemTemplate>
            <asp:Label ID="lblRoleName" runat="server" Text='<%# Eval("RoleName") %>'   Width="70px"></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtRoleName" runat="server" Text='<%# Bind("RoleName") %>'   Width="120px"></asp:TextBox></EditItemTemplate>
                <HeaderStyle Width="70px" />
                 <ItemStyle Width="70px" />
            </asp:TemplateField>
             <asp:CommandField HeaderText="Edit" ShowEditButton="True" >
            <HeaderStyle Width="30px" />
            </asp:CommandField>
            <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" Visible="False" >
            <HeaderStyle Width="20px" />
            </asp:CommandField>
                    </Columns>
        <sortedascendingcellstyle backcolor="#F1F1F1" />
        <sortedascendingheaderstyle backcolor="#0000A9" />
        <sorteddescendingcellstyle backcolor="#CAC9C9" />
        <sorteddescendingheaderstyle backcolor="#000065" />
    </asp:GridView>
    </div>
    </asp:panel>
    
    </div>

</asp:Content>

