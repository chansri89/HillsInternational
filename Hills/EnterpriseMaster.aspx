<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.Master" AutoEventWireup="true" CodeFile="EnterpriseMaster.aspx.cs" Inherits="EnterpriseMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:Label ID = "lblSeparator" runat = "server" align = "center" Height="15px" Width = "901px" ></asp:Label>
    <asp:Label ID="lblenterprisemas" runat="server" Align= "center" Text="Enterprise Master" 
        Font ="Verdana" width="787px"
        Font-Size ="12pt" Font-Bold="True" Font-Names="Verdana" 
        style="text-align: center"></asp:Label>
 <asp:Label ID = "lblSeparator1" runat = "server" align = "center" Height="15px" Width = "901px" ></asp:Label>

<div style="overflow:auto; height: 849px; ">
    <asp:panel ID="Pnlgv" runat="server" Height="219px" Width="882px">
        <div style="overflow:auto; height:219px; width:1014px">
        
    <asp:GridView ID="GrdEnterpriseMaster" runat="server" CellPadding="3" 
            Font-Size="XX-Small" Width="849px" Font-Names="Verdana" AutoGenerateColumns="False" 
            Height="16px" GridLines="Vertical" BackColor="White" BorderColor="#999999" 
                
                onrowcancelingedit="GrdEnterpriseMaster_RowCancelingEdit"                
                onrowediting="GrdEnterpriseMaster_RowEditing" 
                onrowupdating="GrdEnterpriseMaster_RowUpdating">
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" 
            HorizontalAlign="Left" />
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
         <asp:TemplateField HeaderText="Id" Visible="False">
            <ItemTemplate>
            <asp:Label ID="lblEnterpriseId" runat="server" Text='<%# Eval("EnterpriseId") %>' Font-Names="Verdana" Font-Size="XX-Small" ></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtEnterpriseId" runat="server" Text='<%# Bind("EnterpriseId") %>' ReadOnly="true" Font-Names="Verdana" Font-Size="XX-Small" ></asp:TextBox></EditItemTemplate>
                <HeaderStyle Width="40px" />
            </asp:TemplateField>

             <asp:TemplateField HeaderText="Enterprise Name">
            <ItemTemplate>
            <asp:Label ID="lblEnterpriseName" runat="server" Text='<%# Eval("EnterpriseName") %>' Font-Names="Verdana" Font-Size="XX-Small" ></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtEnterpriseName" runat="server" Text='<%# Bind("EnterpriseName") %>'  Font-Names="Verdana" Font-Size="XX-Small"></asp:TextBox></EditItemTemplate>
                <HeaderStyle Width="120px" />
                <ItemStyle  Width = "120px" />
            </asp:TemplateField>

             <asp:TemplateField HeaderText="Address1">
            <ItemTemplate>
            <asp:Label ID="lblAddr1" runat="server" Text='<%# Eval("Addr1") %>' Font-Names="Verdana" Font-Size="XX-Small" ></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtAddr1" runat="server" Text='<%# Bind("Addr1") %>' Font-Names="Verdana" Font-Size="XX-Small" ></asp:TextBox></EditItemTemplate>
                <HeaderStyle Width="120px" />
                <ItemStyle  Width = "120px" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Address2">
            <ItemTemplate>
            <asp:Label ID="lblAddr2" runat="server" Text='<%# Eval("Addr2") %>' Font-Names="Verdana" Font-Size="XX-Small"  ></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtAddr2" runat="server" Text='<%# Bind("Addr2") %>'  Font-Names="Verdana" Font-Size="XX-Small" ></asp:TextBox></EditItemTemplate>
                <HeaderStyle Width="60px" />
                <ItemStyle  Width = "60px" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Address3">
            <ItemTemplate>
            <asp:Label ID="lblAddr3" runat="server" Text='<%# Eval("Addr3") %>' Font-Names="Verdana" Font-Size="XX-Small"></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtAddr3" runat="server" Text='<%# Bind("Addr3") %>'  Font-Names="Verdana" Font-Size="XX-Small" ></asp:TextBox></EditItemTemplate>
                <HeaderStyle Width="50px" />
                <ItemStyle  Width = "50px" />
            </asp:TemplateField>

            <%--<asp:TemplateField HeaderText="Address4">
            <ItemTemplate>
            <asp:Label ID="lblAddr4" runat="server" Text='<%# Eval("Addr4") %>' Font-Names="Verdana" Font-Size="XX-Small" ></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtAddr4" runat="server" Text='<%# Bind("Addr4") %>'  Font-Names="Verdana" Font-Size="XX-Small" Width="100px"></asp:TextBox></EditItemTemplate>
                <HeaderStyle Width="80px" />
                <ItemStyle  Width = "80px" />
            </asp:TemplateField>--%>

            <asp:TemplateField HeaderText="Phone1">
            <ItemTemplate>
            <asp:Label ID="lblPhone1" runat="server" Text='<%# Eval("Phone1") %>' Font-Names="Verdana" Width = "50px" Font-Size="XX-Small"></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtPhone1" runat="server" Text='<%# Bind("Phone1") %>' Font-Names="Verdana" Width = "50px" Font-Size="XX-Small" ></asp:TextBox></EditItemTemplate>
                <HeaderStyle Width="30px" />
                <ItemStyle  Width = "30px" />
            </asp:TemplateField>

           <%--<asp:TemplateField HeaderText="Phone2">
            <ItemTemplate>
            <asp:Label ID="lblPhone2" runat="server" Text='<%# Eval("Phone2") %>' Font-Names="Verdana" Font-Size="XX-Small"></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtPhone2" runat="server" Text='<%# Bind("Phone2") %>' Font-Names="Verdana" Font-Size="XX-Small" ></asp:TextBox></EditItemTemplate>
                <HeaderStyle Width="40px" />
                <ItemStyle  Width = "40px" />
            </asp:TemplateField>--%>

            <asp:TemplateField HeaderText="Fax">
            <ItemTemplate>
            <asp:Label ID="lblFax" runat="server" Text='<%# Eval("Fax") %>'  Font-Names="Verdana" Width = "50px" Font-Size="XX-Small"></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtFax" runat="server" Text='<%# Bind("Fax") %>' Font-Names="Verdana" Width = "50px" Font-Size="XX-Small" ></asp:TextBox></EditItemTemplate>
                <HeaderStyle Width="30px" />
                <ItemStyle  Width = "30px" />
            </asp:TemplateField>

           <%-- <asp:TemplateField HeaderText="Email Id">
            <ItemTemplate>
            <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("Email") %>'  Font-Names="Verdana" Font-Size="XX-Small"></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtEmail" runat="server" Text='<%# Bind("Email") %>' Font-Names="Verdana" Font-Size="XX-Small" ></asp:TextBox></EditItemTemplate>
                <HeaderStyle Width="100px" />
                <ItemStyle  Width = "100px" />
            </asp:TemplateField>--%>

            <asp:TemplateField HeaderText="Website">
            <ItemTemplate>
            <asp:Label ID="lblWebsite" runat="server" Text='<%# Eval("Website") %>' Font-Names="Verdana" Font-Size="XX-Small" ></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtWebsite" runat="server" Text='<%# Bind("Website") %>'  Font-Names="Verdana" Font-Size="XX-Small" ></asp:TextBox></EditItemTemplate>
                <HeaderStyle Width="80px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="IsActive">
             <ItemTemplate>
                <asp:CheckBox ID="chkActive" runat="server" Checked='<%# Eval("IsActive") %>'
            Font-Names="Verdana" Font-Size="XX-Small" Enabled="false"></asp:CheckBox>
             </ItemTemplate>
             <EditItemTemplate>
           <asp:CheckBox ID="chkIsActive" runat="server" Checked='<%# Bind("IsActive") %>'
            Font-Names="Verdana" Font-Size="XX-Small"></asp:CheckBox>
           </EditItemTemplate>
                <HeaderStyle Width="30px" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>

             <asp:CommandField HeaderText="Edit" ShowEditButton="True" >
            <HeaderStyle Width="50px" />
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
    <asp:panel ID="pnlAdd" runat="server" Width="726px">
    <table style="width: 100%"  align="left">
            <tr>
                <td style="width: 100px; text-align: left;">
                    <asp:Label ID="lblEnterprisename" runat="server" Text="Enterprise Name"  
                        Font-Names="Verdana" Font-Size="X-Small"  style="font-weight: bold"></asp:Label>
                </td>
                <td style="width: 118px">
                    <asp:TextBox ID="txtEnterprisename" runat="server" Font-Names="Verdana" 
                        Font-Size="X-Small" style="font-weight: bold"  Width="195px"></asp:TextBox>
                </td>
                <td style="width: 83px; text-align: left;">
                    <asp:Label ID="lbladdr1" runat="server" Text="Address1"  
                        Font-Names="Verdana" Font-Size="X-Small" style="font-weight: bold"></asp:Label>
                </td>
                <td style="width: 129px">
                    <asp:TextBox ID="txtaddr1" runat="server" Font-Names="Verdana" 
                        Font-Size="X-Small" style="font-weight: bold"  Width="200px" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 100px; text-align: left;">
                    <asp:Label ID="lbladdr2" runat="server" Text="Address2"  
                        Font-Names="Verdana" Font-Size="XX-Small" 
                        Font-Bold="False"></asp:Label>
                </td>
                <td style="width: 118px">
                    <asp:TextBox ID="txtaddr2" runat="server" Font-Names="Verdana" 
                        Font-Size="X-Small" Width="195px" ></asp:TextBox>
                </td>
                <td style="width: 83px; text-align: left;">
                    <asp:Label ID="lbladdr3" runat="server" Text="Address3"  
                        Font-Names="Verdana" Font-Size="X-Small"></asp:Label>
                </td>
                <td style="width: 129px">
                    <asp:TextBox ID="txtaddr3" runat="server" Font-Names="Verdana" 
                        Font-Size="X-Small" Width="195px" ></asp:TextBox>
                </td>
            </tr>
            <tr>
               <%-- <td style="width: 100px; text-align: left;">
                    <asp:Label ID="lbladdr4" runat="server" Text="Address4"  
                        Font-Names="Verdana" Font-Size="X-Small"  
                        Font-Bold="False"></asp:Label>
                </td>
                <td style="width: 118px">
                    <asp:TextBox ID="txtaddr4" runat="server" Font-Names="Verdana" 
                        Font-Size="X-Small" Width="195px" ></asp:TextBox>
                </td>--%>
                <td style="width: 83px; text-align: left;">
                    <asp:Label ID="lblEnterprisePhone1" runat="server" Text="Phone1"  
                        Font-Names="Verdana" Font-Size="X-Small" style="font-weight: bold"></asp:Label>
                </td>
                <td style="width: 129px">
                    <asp:TextBox ID="txtEnterprisePhone1" runat="server" Font-Names="Verdana" 
                        Font-Size="X-Small" Width="125px" ></asp:TextBox>
                </td>
                <td style="width: 83px; text-align: left;">
                    <asp:Label ID="lblfax" runat="server" Text="Fax"  
                        Font-Names="Verdana" Font-Size="X-Small" ></asp:Label>
                </td>
                <td style="width: 129px">
                    <asp:TextBox ID="txtfax" runat="server" Font-Names="Verdana" 
                        Font-Size="X-Small" Width="127px" ></asp:TextBox>
                </td>
            </tr>
            <%--<tr>
                <td style="width: 100px; text-align: left;">
                    <asp:Label ID="lblEnterprisePhone2" runat="server" Text="Phone2"  
                        Font-Names="Verdana" Font-Size="X-Small" ></asp:Label>
                </td>
                <td style="width: 118px">
                    <asp:TextBox ID="txtEnterprisePhone2" runat="server" Font-Names="Verdana" 
                        Font-Size="X-Small" Width="195px" ></asp:TextBox>
                </td>
                
            </tr>--%>
             <tr>
                <%--<td style="width: 100px; text-align: left;">
                    <asp:Label ID="lblEmailId" runat="server" Text="Email Id"  
                        Font-Names="Verdana" Font-Size="X-Small" ></asp:Label>
                </td>
                <td style="width: 118px">
                    <asp:TextBox ID="txtEmailId" runat="server" Font-Names="Verdana" 
                        Font-Size="X-Small" Width="187px" ></asp:TextBox>--%>
                <%--</td>
                <td class="style55">--%>
                    <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                        ControlToValidate="txtEmailId" ErrorMessage="Enter EmailId in Correct Format" 
                        Font-Names="Verdana" Font-Size="XX-Small" ForeColor="#FF3300" 
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>--%>
                <%-- </td>--%>
                <td style="width: 83px; text-align: left;">
                    <asp:Label ID="lblwebsite" runat="server" Text="Website"  
                        Font-Names="Verdana" Font-Size="X-Small" ></asp:Label>
                </td>
                <td style="width: 129px">
                    <asp:TextBox ID="txtwebsite" runat="server" Font-Names="Verdana" 
                        Font-Size="X-Small" Width="124px" ></asp:TextBox>
                </td>
             </tr>
             
            <tr>
                <td class="style1" style="width: 100px; text-align: left;">
                       <asp:CheckBox ID="ChkIsActive" runat="server" Checked="true" 
                            Font-Names="Verdana" Font-Size="XX-Small" Text="IsActive" Visible="true" 
                            style="font-weight: bold; text-align: left;" />
                </td>
               
                <td style="width: 83px">
                    <asp:Button ID="btnSave" runat="server" Text="Save" Font-Names="Verdana" 
                        onclick="btnSave_Click" Font-Size="XX-Small" />
                </td>
                </tr>
                <tr>
                <td style="width: 100px; text-align: left"> <asp:HiddenField ID="HidDeleteCount" Value="0" runat="server" />
                <asp:HiddenField ID="HidUpdateCount" Value="0" runat="server" />
                </td>
            </tr>
        </table>
    </asp:panel>
    
    </div>

</asp:Content>

