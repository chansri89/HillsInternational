<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.master" AutoEventWireup="true" CodeFile="StateMaster.aspx.cs" Inherits="StateMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%--<asp:Panel ID="Panel2" runat="server" Height="16px" Width="870px"> </asp:Panel>--%>
<asp:Label ID="lblStatMas" runat="server" Align= "center" Text="State Master" 
        width="450px" Font-Size ="12pt" Font-Bold="True" 
        style="text-align: center" Height="26px"></asp:Label>
<div style="overflow:auto; height: 664px;">
    <%--<asp:Panel ID="Panel1" runat="server" Height="41px">
        <asp:Button ID="btnAdd" runat="server" Text="Add"
            Font-Names="arial" onclick="btnAdd_Click1" />
    </asp:Panel>--%>
    <asp:panel ID="Pnlgv" runat="server" Height="394px" Width="522px" ToolTip="Click Edit for Updating.."
        GroupingText="State Grid" >
        <div style="overflow:auto; height:373px; width:502px">
    <asp:GridView ID="GrdStateMaster" runat="server" CellPadding="3" 
            Width="464px"  AutoGenerateColumns="False" Height="102px" GridLines="Vertical" BackColor="White" BorderColor="#999999" 
                BorderStyle="None" BorderWidth="1px" 
                onrowcancelingedit="GrdStateMaster_RowCancelingEdit"                
                onrowediting="GrdStateMaster_RowEditing" 
                onrowupdating="GrdStateMaster_RowUpdating">
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" 
            HorizontalAlign="Left" />
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
         <asp:TemplateField HeaderText="StateType Id" Visible="False" >
            <ItemTemplate>
            <asp:Label ID="lblStateId" runat="server" Text='<%# Eval("StateId") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtStateId" runat="server" Text='<%# Bind("StateId") %>' ReadOnly="true" Font-Names="arial" Font-Size="X-Small"></asp:TextBox></EditItemTemplate>
                <HeaderStyle Width="40px" />
            </asp:TemplateField>

             <asp:TemplateField HeaderText="State Name" >
            <ItemTemplate>
            <asp:Label ID="lblStateName" runat="server" Text='<%# Eval("StateName") %>'  Font-Names="arial" Font-Size="X-Small" Width="200px"></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtStateName" runat="server" Text='<%# Bind("StateName") %>'  Font-Names="arial" Font-Size="X-Small" Width="200px"></asp:TextBox></EditItemTemplate>
                <HeaderStyle Width="200px" />
                 <ItemStyle Width="200px" />
            </asp:TemplateField>

             <asp:TemplateField HeaderText="State   ShortName">
            <ItemTemplate>
            <asp:Label ID="lblStateShName" runat="server" Text='<%# Eval("StateShortName") %>'  Font-Names="arial" Font-Size="X-Small" Width="40px"></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtStatehName" runat="server" Text='<%# Bind("StateShortName") %>'  Font-Names="arial" MaxLength="5" Font-Size="X-Small" Width="40px"></asp:TextBox></EditItemTemplate>
                <HeaderStyle Width="40px" />
            </asp:TemplateField>

            <%--   <asp:TemplateField HeaderText="IsActive" Visible="False">
             <ItemTemplate>
              <asp:CheckBox ID="chkActive" runat="server" Checked='<%# Eval("IsActive") %>'
            Font-Names="arial" Font-Size="X-Small" Enabled="false"></asp:CheckBox>
             </ItemTemplate>
             <EditItemTemplate>
           <asp:CheckBox ID="chkIsActive" runat="server" Checked='<%# Bind("IsActive") %>'
            Font-Names="arial" Font-Size="X-Small"></asp:CheckBox>
           </EditItemTemplate>
                <HeaderStyle Width="20px" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>--%>
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
    <asp:panel ID="pnlAdd" runat="server" Width="527px" GroupingText="Add State" 
        Font-Size="X-Small">
    <table style="width: 100%"  align="left" class="XXSmall">
            <tr>
                <td style="width: 59px; text-align: left;">
                    <asp:Label ID="lblStateName" runat="server" Text="State Name"  
                        Font-Bold="True"></asp:Label>
                </td>
                <td style="width: 129px">
                    <asp:TextBox ID="txtStatetName" runat="server" Width="113px"></asp:TextBox>
                </td>
            <%--</tr>
            <tr>--%>
                <td style="width: 102px; text-align: left;">
                    <asp:Label ID="lblStateShortName" runat="server" Text="State Short Name"  
                        Font-Bold="True"></asp:Label>
                </td>
                <td style="width: 100px">
                    <asp:TextBox ID="txtStateShortName" runat="server" MaxLength="5" 
                         Width="60px"></asp:TextBox>
                </td>
           <%-- </tr>
            
            <tr>
                <td style="width: 59px; text-align: left;">
                    &nbsp;</td>--%>
                <td style="width: 80px" >
                    <asp:Button ID="btnSave" runat="server" Text="Save"    onclick="btnSave_Click"  />
                </td>
                </tr>
                <%--<tr>
                <td class="style21" style="width: 141px; text-align: left"> <asp:HiddenField ID="HidDeleteCount" Value="0" runat="server" />
                <asp:HiddenField ID="HidUpdateCount" Value="0" runat="server" />
                </td>
            </tr>--%>
        </table>
    </asp:panel>
    
    </div>

</asp:Content>

