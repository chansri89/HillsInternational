<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.Master" AutoEventWireup="true" CodeFile="AssignUserRoles.aspx.cs" Inherits="AssignUserRoles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:Label ID="lblassignuserroles" runat="server" Align= "center" Text="Assign User Roles" 
         width="787px" Font-Size ="12pt" Font-Bold="True"  style="text-align: center"></asp:Label>

    <div style="overflow:auto;">
    
        <asp:Panel ID="pnlUsers" runat="server" CssClass="XSmall">
    <table>
    <tr>
    <td colspan="20" align="right">
        <asp:Label ID="Label1" runat="server" 
            Text="User Name" Width="126px" Font-Bold="True"></asp:Label></td>
    <td colspan="20">
        <asp:DropDownList ID="ddlUserName" runat="server" DataTextField="EmployeeName" DataValueField="EmployeeCode"
            Width="120px" Font-Size="XX-Small" AutoPostBack="True"    OnSelectedIndexChanged="ddlUserName_SelectedIndexChanged">
        </asp:DropDownList></td>
    </tr>
    </table>    
        
    </asp:Panel>
        <asp:Panel ID="pnlAssignRoles" runat="server" Visible="true">
      <table style="width: 435px"> 
  <tr>
  <td colspan ="20">
  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
  <asp:Label ID="lblAvailableRoles" runat="server" Font-Bold="False" 
          ForeColor="Blue"      Text="Available Roles"></asp:Label>
  </td>
  <td colspan="20">
  
  </td>
  <td colspan="20">
     
      <asp:Label ID="lblAssignedRoles" runat="server" Font-Bold="False" 
             ForeColor="Blue" Text="Assigned Roles" Width="123px"></asp:Label></td>
  </tr>
  <tr>
  <td colspan="20" align="right" style="height: 80px; width: 209px;">
      <asp:ListBox ID="lstbxAvailableRole" runat="server" DataTextField="RoleName" DataValueField="AvRoleId"
         Height="114px"     TabIndex="10" Width="131px">
      </asp:ListBox></td> 
  <td colspan="20" align="center" style="height: 80px; width: 119px;" >
      <br />
      <asp:Button ID="btnMove" runat="server" CausesValidation="False"  ForeColor="Blue" OnClick="btnMove_Click" TabIndex="11" Text=">"
          Width="61px" /><br />
      <asp:Button ID="btnMoveFull" runat="server" CausesValidation="False"  ForeColor="Blue" OnClick="btnMoveFull_Click" 
          TabIndex="12" Text=">>"
          Width="61px" Visible="False" /><br />
      <asp:Button ID="btnRemove" runat="server" CausesValidation="False" ForeColor="Blue" OnClick="btnRemove_Click" 
          TabIndex="13" Text="<"
          Width="61px" /><br />
      <asp:Button ID="btnRemoveFull" runat="server" CausesValidation="False"  ForeColor="Blue" OnClick="btnRemoveFull_Click" TabIndex="14"
          Text="<<" Width="61px" Visible="False" /><br />
      <br />
      </td>
  <td colspan="20" align="left" style="width: 93px; height: 80px">
      <asp:ListBox ID="lstbxAssignedRole" runat="server" DataTextField="RoleName" DataValueField="AsgRoleId"
        Height="114px"     TabIndex="15" Width="131px"></asp:ListBox></td>
  </tr> 
  <tr>
  <td colspan="20"></td>
  <td colspan ="20" align="center">
      
      <asp:Button ID="btnSave" runat="server"  ForeColor="Blue"
          Text="Save" Width="61px" OnClick="btnSave_Click" />
  </td>
  </tr>
   </table>
      </asp:Panel>
      
    <asp:Panel ID="pnlGetIds" runat="server" Visible="False" Width="46px" style="z-index: 103; left: 11px; position: absolute; top: 307px">
        <asp:TextBox ID="txtUserId" runat="server" Height="25px" Visible="False" Width="21px"></asp:TextBox></asp:Panel>
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

    </div>
</asp:Content>

