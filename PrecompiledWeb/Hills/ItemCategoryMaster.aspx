<%@ page title="" language="C#" masterpagefile="~/MasterPage1.master" autoeventwireup="true" inherits="ItemCategoryMaster, App_Web_sx5sst5a" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="Panel2" runat="server" CssClass="XSmall" Height="16px" Width="870px"> </asp:Panel>
<asp:Label ID="lblStatMas" runat="server" Align= "center" Text="ItemCategory Master" 
       width="450px" Font-Size ="12pt" Font-Bold="True"   style="text-align: center" Height="26px"></asp:Label>
<div style="overflow:auto; height: 664px;">
    <asp:Panel ID="pnlPendind" runat="server" Height="28px" Width="644px" CssClass="XSmall">
        <table style="height: 22px; width: 634px;">
            <tr>
                      <td class="style28" style="width: 70px">
                <asp:Label ID="Label1" runat="server" Text="Company" Visible="true"
                  Font-Bold="true"  ></asp:Label>
           </td>
             <td style="width: 100px" class="style21">
                <asp:DropDownList ID="ddlCompany"  runat="server"  Visible="true"
                   DataTextField="CompanyName" DataValueField="CompanyId"   Width="197px"  AutoPostBack="true" 
                     OnSelectedIndexChanged="ddlCompanyChanged" Height="16px"  >
                 </asp:DropDownList>
            </td>
                  <td style="width: 104px">
                <asp:Label ID="lblFilter" runat="server" Text="Filter Category Name" Visible="true"
                  Font-Bold="true"  ></asp:Label>
           </td>

            <td style="width: 70px">
                <asp:TextBox ID="txtFilter" runat="server" Text="" Visible="true"
                  Font-Bold="true"   ></asp:TextBox>
           </td>
                <td style="width: 62px" >
                         <asp:Button ID="btnFilter" runat="server" Height="21px" onclick="btnFilter_Click"   Text="Filter" Width="43px" />
                 </td>     
            </tr>
               
        </table>
    </asp:Panel>
    <asp:panel ID="Pnlgv" runat="server" Height="350px" Width="705px" CssClass="XSmall" ToolTip="Click Edit for Updating.."
        GroupingText="Edit ItemCategory Grid">
        <div style="overflow:auto; height:330px; width:686px">
    <asp:GridView ID="GrdItemCategoryMaster" runat="server" CellPadding="3" 
             Width="674px"  AutoGenerateColumns="False" 
            Height="102px" GridLines="Vertical" BackColor="White" BorderColor="#999999" 
                BorderStyle="None" BorderWidth="1px" 
                onrowcancelingedit="GrdItemCategoryMaster_RowCancelingEdit"                
                onrowediting="GrdItemCategoryMaster_RowEditing" 
                onrowupdating="GrdItemCategoryMaster_RowUpdating">
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" 
            HorizontalAlign="Left" />
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
         <asp:TemplateField HeaderText="Item Category Id" Visible="false" >
            <ItemTemplate>
            <asp:Label ID="lblItemCategoryId" runat="server" Text='<%# Eval("ItemCategoryId") %>' Width="40px" ></asp:Label></ItemTemplate>
             <HeaderStyle Width="40px" />  </asp:TemplateField>
                 <asp:TemplateField HeaderText="CompanyId" Visible="false" >
            <ItemTemplate>
            <asp:Label ID="lblCompanyId" runat="server" Text='<%# Eval("CompanyId") %>' Width="40px" ></asp:Label></ItemTemplate>
             <HeaderStyle Width="40px" />  </asp:TemplateField>

         <asp:TemplateField HeaderText="Item Category Code" Visible="true" >
            <ItemTemplate>
            <asp:Label ID="lblItemCategoryCode" runat="server" Text='<%# Eval("ItemCategoryCode") %>'  ></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtItemCategoryCode" runat="server" Text='<%# Bind("ItemCategoryCode") %>' ReadOnly="true" ></asp:TextBox></EditItemTemplate>
                <HeaderStyle Width="40px" />
            </asp:TemplateField>

             <asp:TemplateField HeaderText="Item Category Name" >
            <ItemTemplate>
            <asp:Label ID="lblItemCategoryName" runat="server" Text='<%# Eval("ItemCategoryName") %>'  Width="200px"></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtItemCategoryName" runat="server" Text='<%# Bind("ItemCategoryName") %>' Width="200px"></asp:TextBox></EditItemTemplate>
                <HeaderStyle Width="200px" />
                 <ItemStyle Width="200px" />
            </asp:TemplateField>
           <asp:TemplateField HeaderText="Is Active">
             <ItemTemplate>    <asp:CheckBox ID="chkActive" Width="30px" runat="server" Checked='<%# Eval("IsActive") %>'
            Enabled="true"></asp:CheckBox>         </ItemTemplate>
               <HeaderStyle Width="30px" />     <ItemStyle HorizontalAlign="Center" />
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
    <br />
    <asp:panel ID="pnlAdd" runat="server" Width="708px" GroupingText="Add ItemCategory" CssClass="XSmall"
        Height="52px">
    <table style="width: 98%"  align="left">
            <tr>
                 <td style="width: 102px; text-align: left;">
                    <asp:Label ID="lblItemCategoryCode" runat="server" Text="ItemCategory Code"  
                        Font-Bold="True"></asp:Label>
                </td>
                <td style="width: 129px">
                    <asp:TextBox ID="txtItemCategorycode" runat="server" MaxLength="8" 
                       ></asp:TextBox>
                </td>
                <td style="width: 102px; text-align: left;">
                    <asp:Label ID="lblItemCategoryName" runat="server" Text="ItemCategory Name"  
                         Font-Bold="True"></asp:Label>
                </td>
                <td style="width: 129px">
                    <asp:TextBox ID="txtItemCategoryName" runat="server" MaxLength="64"
                       Width="231px"></asp:TextBox>
                </td>
               <td style="width: 59px" class="style28">
                    <asp:CheckBox ID="ChkIsActive" runat="server"  Text="IsActive"
                      Checked="true"  Width="68px"></asp:CheckBox>
                </td>
                
                <td style="width: 129px">
                    <asp:Button ID="btnSave" runat="server" Text="Save" 
                        onclick="btnSave_Click"  />
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

