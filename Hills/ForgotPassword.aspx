<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.Master" AutoEventWireup="true" CodeFile="ForgotPassword.aspx.cs" Inherits="ForgotPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">   
    <asp:Panel ID="pnlForgotPwd" runat="server" Height="595px" Width="831px">                   
    <table>
    <tr>
    <td style="width: 72px">
    <asp:Label ID="lblUserName" runat="server" Text="User Name" Font-Size="XX-Small" 
            Font-Names="Verdana" style="font-weight: 700" ></asp:Label>
    </td>
    <td>
    <asp:TextBox ID="txtUsername" runat="server" Placeholder="UserName" Width="226px" 
            Font-Size="XX-Small"></asp:TextBox>
    </td>
    <td>
        <asp:Button ID="btnGo" runat="server" onclick="btnGo_Click" 
            Text="Go" Font-Size="XX-Small" Font-Names="Verdana" />
           
        </td>
    </tr>
   
    </table>                               
    </asp:Panel>                                          
 </asp:Content>






