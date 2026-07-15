<%@ page title="" language="C#" masterpagefile="~/MasterPage1.Master" autoeventwireup="true" inherits="ItemMaster, App_Web_lkpdlk5o" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%-- <asp:Label ID = "lblSeparator" runat = "server" align = "center" Height="15px" Width = "901px" ></asp:Label>--%>
 
  <asp:Label ID="lblItemGroupMaster" runat="server" Align= "center" Text="Item  Master" 
         width="787px"    Font-Size ="12pt" Font-Bold="True"     style="text-align: center"></asp:Label>
   <%--<asp:Label ID = "lblSeparator1" runat = "server" align = "center" Height="15px" Width = "901px" ></asp:Label>--%>
    <div style="overflow:auto; height: 471px; width: 1202px;">
        <%--</td></tr>
    </table>--%>    <%--</asp:Panel>DataKeyNames="Item GroupCode"--%>
    <asp:Panel ID="pnlPendind" runat="server" Height="30px" Width="755px">
        <table style="height: 22px; width: 712px;">
            <tr>
             <td class="XSmall" >
                <asp:Label ID="lblCompany"  runat="server" Text="Company" Visible="true"
                  Font-Bold="true"  ></asp:Label>
           </td>
             <td style="width: 100px" >
                <asp:DropDownList ID="ddlCompany"  runat="server"  Visible="true"
                     DataTextField="CompanyName" DataValueField="CompanyId"
                    Style="font-size: X-Small" Width="248px"  AutoPostBack="true" 
                     OnSelectedIndexChanged="ddlCompanyChanged" Height="16px"  >
                 </asp:DropDownList>
            </td>


                  <td  style="width: 70px">
                <asp:Label ID="lblFilter" runat="server" Text="Item Filter" Visible="true"
                  Font-Bold="true"   ></asp:Label>
           </td>

            <td  style="width: 70px">
                <asp:TextBox ID="txtFilter" runat="server" Text="" Visible="true"
                  Font-Bold="true"  ></asp:TextBox>
           </td>
                <td class="XSmall"  >
                         <asp:Button ID="btnFilter" runat="server" Height="21px" onclick="btnFilter_Click"   
                               Text="Filter" Width="43px" />
                 </td>     
   <%--             <td class="XSmall" style="width: 62px" >
                         <asp:Button ID="btnView" runat="server" Font-Names="Verdana" Font-Size="X-Small" 
                               Height="21px" onclick="btnView_Click"  style="font-size: small; font-family: 'Times New Roman', Times, serif; font-weight: 700;" 
                               Text="Go" Width="43px" />
                 </td>       --%> 

                 <td></td>
                  <td class="XSmall" >
                           
                           <asp:Button ID="btnClear" runat="server" 
                               Height="21px" onclick="btnClear_Click"    Text="Clear" Width="43px" />
                       </td>  
 
          
                             
            </tr>
               
        </table>
    </asp:Panel>

    <asp:panel ID="Pnlgv" runat="server" Width="1068px" Height="351px" 
            GroupingText="Item  Grid">
        <div id="divItemGroup" runat="server"  
            style="overflow:auto; height:328px; width:1045px">

    <asp:GridView ID="GrdItemMaster" runat="server" CellPadding="3" CssClass="XSmall" ToolTip="Click On Item Name link for Updating.."
            Width="1022px"  AutoGenerateColumns="False" 
            Height="16px" GridLines="Vertical" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
            OnRowCommand="GrdItem_RowCommand">
        <EditRowStyle Font-Size="XX-Small" />
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" 
            HorizontalAlign="Left" />
        <AlternatingRowStyle BackColor="#DCDCDC" />

       
        <Columns>
            <asp:TemplateField HeaderText="CompanyId"  Visible="false">
            <ItemTemplate>
            <asp:Label ID="lblCompanyId" runat="server" Text='<%# Eval("CompanyId") %>'  ></asp:Label></ItemTemplate>
             <HeaderStyle Width="10px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ItemCat Id"  Visible="false">
            <ItemTemplate>
            <asp:Label ID="lblItemCatId" runat="server" Text='<%# Eval("ItemCategoryId") %>' ></asp:Label></ItemTemplate>
             <HeaderStyle Width="10px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ItemSubCat Id"  Visible="false">
            <ItemTemplate>
            <asp:Label ID="lblItemSubCatId" runat="server" Text='<%# Eval("ItemSubCategoryId") %>' ></asp:Label></ItemTemplate>
             <HeaderStyle Width="10px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ItemGrpId" Visible="false">
             <ItemTemplate>
            <asp:Label ID="lblItemGroupId" runat="server" Text='<%# Eval("ItemGroupId") %>' Width="25px"  ></asp:Label></ItemTemplate>
                <HeaderStyle Width="25px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ItemId" Visible="false">
             <ItemTemplate>
            <asp:Label ID="lblItemId" runat="server" Text='<%# Eval("ItemId") %>' Width="25px" ></asp:Label></ItemTemplate>
                <HeaderStyle Width="25px" />
            </asp:TemplateField>


            <asp:TemplateField HeaderText="Item Category Name" Visible="true">
             <ItemTemplate>
            <asp:Label ID="lblItemCategoryName" runat="server" Text='<%# Eval("ItemCategoryName") %>' Width="100px" ></asp:Label></ItemTemplate>
                <HeaderStyle Width="100px" />
            </asp:TemplateField>
 
             <asp:TemplateField HeaderText="Item Sub Category Name" Visible="true">
             <ItemTemplate>
            <asp:Label ID="lblItemSubCategoryName" runat="server" Text='<%# Eval("ItemSubCategoryName") %>' Width="110px" ></asp:Label></ItemTemplate>
                <HeaderStyle Width="110px" />
            </asp:TemplateField>

             <asp:TemplateField HeaderText="Item Group Name" Visible="true">
            <ItemTemplate>
            <asp:Label ID="lblItemGroupName" runat="server" Text='<%# Eval("ItemGroupName") %>' width ="100px" ></asp:Label></ItemTemplate>
              <HeaderStyle Width="100px"/>
               </asp:TemplateField>
                                    
            <asp:TemplateField HeaderText="Item Code" Visible="True">
            <ItemTemplate>
            <asp:Label ID="lblItemCode" runat="server"  Width="40px" Text='<%# Eval("ItemCode") %>' ></asp:Label></ItemTemplate>
              <HeaderStyle Width="40px"/>
            </asp:TemplateField>
 
           <asp:TemplateField HeaderText="Item Make" Visible="true">
            <ItemTemplate>
            <asp:Label ID="lblItemMake" runat="server"  Width="80px" Text='<%# Eval("ItemMake") %>' ></asp:Label></ItemTemplate>
              <HeaderStyle Width="80px"/>
            </asp:TemplateField>

           <asp:TemplateField HeaderText="Item UOM" Visible="true">
            <ItemTemplate>
            <asp:Label ID="lblItemUOM" runat="server"  Width="60px" Text='<%# Eval("ItemUOM") %>' ></asp:Label></ItemTemplate>
              <HeaderStyle Width="60px"/>
            </asp:TemplateField>

           <asp:TemplateField HeaderText="Item Name" Visible="false">
            <ItemTemplate>
            <asp:Label ID="lblItemName" runat="server"  Width="20px" Text='<%# Eval("ItemName") %>' ></asp:Label></ItemTemplate>
              <HeaderStyle Width="20px"/>
            </asp:TemplateField>


         <asp:TemplateField HeaderText="Item Name" Visible="true" >
            <ItemTemplate>  
            <asp:LinkButton ID="lnkItemName" Width="250px" runat ="server" CommandArgument='<%#Eval("ItemId")%>'
             CommandName ="selectItem" Text ='<%#Eval("ItemName") %>'></asp:LinkButton>
              </ItemTemplate> <HeaderStyle Width="250px" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Is Imported">
             <ItemTemplate>    <asp:CheckBox ID="chkImported" Width="30px" runat="server" Checked='<%# Eval("IsImported") %>'
            Enabled="false"></asp:CheckBox>         </ItemTemplate>
               <HeaderStyle Width="30px" />     <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Is Active">
             <ItemTemplate>    <asp:CheckBox ID="chkActive" Width="30px" runat="server" Checked='<%# Eval("IsActive") %>'
             Enabled="false"></asp:CheckBox>         </ItemTemplate>
               <HeaderStyle Width="30px" />     <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            
        </Columns>
        <sortedascendingcellstyle backcolor="#F1F1F1" />
        <sortedascendingheaderstyle backcolor="#0000A9" />
        <sorteddescendingcellstyle backcolor="#CAC9C9" />
        <sorteddescendingheaderstyle backcolor="#000065" />
    </asp:GridView>
    </div>
    <%--</td>
    </tr>--%>
    </asp:panel>
    
    <asp:panel ID="pnlAdd" runat="server" Width="1069px" Font-Size="X-Small" GroupingText="Add Item Group" 
            style="margin-top: 0px" Height="79px">
    <table style="width: 98%; height: 63px;" >
            <tr>

                          <td  style="width: 70px">
                <asp:Label ID="lblItemCategory" runat="server" Text="Item Category" Visible="true"
                  Font-Bold="true"   ></asp:Label>
           </td>
             <td style="width: 100px" >
                <asp:DropDownList ID="ddlItemCategory"  runat="server"  Visible="true" Font-Size="XX-Small"
                     DataTextField="ItemCategoryName" DataValueField="ItemCategoryId"
                    Width="125px" Height="18px"   >
                 </asp:DropDownList>
            </td>

             <td  style="width: 70px">
                <asp:Label ID="lblItemSubCategory" runat="server" Text="Item Sub Category" Visible="true"
                  Font-Bold="true" ></asp:Label>
           </td>
             <td style="width: 100px" >
                <asp:DropDownList ID="ddlItemSubCategory"  runat="server"  Visible="true" Font-Size="XX-Small"
                     DataTextField="ItemSubCategoryName" DataValueField="ItemSubCategoryId"
                    Width="123px" Height="18px"   >
                 </asp:DropDownList>
            </td>

                    <td  style="width: 60px; text-align: left;">
                    <asp:Label ID="lblItemGroup" runat="server" Text="Item Group"  Font-Bold="True"></asp:Label>
                </td>
                <td >
                    <asp:DropDownList ID="ddlItemGroup"  runat="server"  Visible="true" Font-Size="XX-Small"
                   DataTextField="ItemGroupName" DataValueField="ItemGroupId"    Width="137px"  Height="18px"  >
                       
                 </asp:DropDownList>
                </td>
                 <td  style="width: 69px; ">
                    <asp:Label ID="lblItemName" runat="server" Text="Item Name"     Font-Bold="True"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtItemName" runat="server" height="18px" width="201px"></asp:TextBox>
                </td>
            <td class="XSmall" style="width: 64px">
                    <asp:TextBox ID="txtItemId" runat="server" Visible="false" height="18px" width="10px"></asp:TextBox>
                </td>

               <td class="XSmall" style="width: 38px">
                    <asp:TextBox ID="txtItemCategoryId" runat="server"  Visible="false"
                       height="18px" width="10px"></asp:TextBox>
                </td>
              <td class="XSmall" style="width: 30px">
                    <asp:TextBox ID="txtItemSubCategoryId" runat="server"  Visible="false"
                         height="18px" width="10px"></asp:TextBox>
                </td>
                </tr>
                <tr>
                    <td style="width: 71px; text-align: left;">
                    <asp:Label ID="lblItemCode" runat="server" Text="Item Code"  
                         Font-Bold="True"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtItemCode" runat="server" height="18px" Width="90px"></asp:TextBox>
                </td>
      
    

                <td  style="width: 69px; ">
                    <asp:Label ID="lblItemUOM" runat="server" Text="Item UOM"  
                       Font-Bold="True"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtItemUOM" runat="server" height="18px" width="68px"></asp:TextBox>
                </td>
                <td  style="width: 53px; ">
                    <asp:Label ID="lblItemMake" runat="server" Text="Item Make" Font-Bold="false"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtItemMake" runat="server"  height="18px" width="100px"></asp:TextBox>
                </td>
                 <td  style="width: 100px" align="center">
                    <asp:CheckBox ID="ChkIsImported" runat="server"   Text="Is Imported" Visible="true"  />
                </td>
                <td >
                    <asp:Button ID="btnSave" runat="server" Text="Save"   onclick="btnSave_Click"  />
                </td>
                <td  style="width: 100px" align="center">
                    <asp:CheckBox ID="chkIsActive" runat="server"  Text="Is Active" Visible="False" />
                </td>
            </tr>
               
        </table>
    </asp:panel>

    </div>
</asp:Content>

