<%@ Page Language="C#" MasterPageFile="~/MasterPage1.Master"  AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 642px">
<tr>
<td align="center">
    <asp:Panel ID="Panel4" runat="server" Height="500px" Width="950px" CssClass="XSmall">
   
                      <asp:Label ID="lblWelcomeCompany" runat="server" Text="Welcome to Tender Insight System " 
                          ForeColor="#3333CC"  Width="373px"  Font-Size="Large" style="text-align: center; margin-top: 36px;" Font-Bold="True" 
                      Height="34px"></asp:Label>

       <%-- <asp:Label ID="lblWelcome" runat="server" Text="Welcome to KDHP "
        Font-Size="Larger" ForeColor="#3333CC" ></asp:Label>--%>
    </asp:Panel>
</td></tr></table>
   
</asp:Content>
