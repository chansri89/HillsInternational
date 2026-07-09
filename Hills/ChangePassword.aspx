<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.Master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:Label ID="lblchangepassword" runat="server" Align= "center" Text="Change Password" 
        width="348px"     Font-Size ="12pt" Font-Bold="True"   
        style="text-align: center"></asp:Label>

<table style="height: 115px; width: 351px" class="XSmall">
<tr>
<td colspan="25" style="width: 113px; text-align: left;">
        <asp:Label ID="lblOldPassword" runat="server" Text="Old Password" Font-Bold="False" 
            style="font-weight: bold"></asp:Label></td>
<td colspan="20">
   <asp:TextBox id="txtOldPassword" runat="server" Width="119px" TextMode="Password" MaxLength="20" 
        AutoCompleteType="Disabled" TabIndex="1" Height="19px" 
        ToolTip="Enter minimum 6  characters"></asp:TextBox>
    </td>
</tr>
<tr>
<td colspan="25" style="width: 113px; height: 24px; text-align: left;">
   <asp:Label ID="lblNewPassword" runat="server"  Text="New Password" Font-Bold="False" 
        style="font-weight: bold"></asp:Label></td>
<td colspan="20" style="height: 23px">
   <asp:TextBox id="txtNewPassword" runat="server" Width="119px" TextMode="Password" MaxLength="20" 
        AutoCompleteType="Disabled" TabIndex="2" style="margin-left: 1px" 
        Height="18px" ToolTip="Enter minimum 6  characters"></asp:TextBox>
    </td>
</tr>
<tr>
<td colspan="25" style="width: 113px; text-align: left;">
    <asp:Label ID="lblConfirmPassword" runat="server"    Text="Confirm Password" Font-Bold="False" style="font-weight: bold"></asp:Label></td>
<td colspan="20">


<asp:TextBox id="txtConfirmPassword" tabIndex="3" runat="server" Width="119px" 
        TextMode="Password" MaxLength="20" AutoCompleteType="Disabled" Height="20px" 
        ToolTip="Enter minimum 6  characters"></asp:TextBox>
</td>
</tr>
<tr>
<td colspan="25" style="text-align: left; width: 113px"></td>
<td colspan ="40" align="left">
    <asp:Button id="btnSave" tabIndex=4 onclick="btnSave_Click" runat="server" 
        Width="44px"  Text="Save" ToolTip="Save"        ForeColor="Black"></asp:Button>
</td>
</tr>
</table>
    &nbsp;
     <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
</asp:Content>

