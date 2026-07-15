<%@ page title="" language="C#" masterpagefile="~/MasterPage1.Master" autoeventwireup="true" inherits="ItemGroupMaster, App_Web_mlnhxkpa" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%-- <asp:Label ID = "lblSeparator" runat = "server" align = "center" Height="15px" Width = "901px" ></asp:Label>--%>

  <asp:Label ID="lblItemGroupMaster" runat="server" Align= "center" Text="Item Group Master" 
         width="653px" CssClass="XSmall"    Font-Size ="12pt" Font-Bold="True"  
        style="text-align: center"></asp:Label>
 <%--  <asp:Label ID = "lblSeparator1" runat = "server" align = "center" Height="15px" Width = "901px" ></asp:Label>--%>
    <div style="overflow:auto; height: 471px; width: 1202px;">
        <%--</td></tr>
    </table>--%>    <%--</asp:Panel>DataKeyNames="Item GroupCode"--%>
    <asp:Panel ID="pnlPendind" runat="server" Height="28px" Width="657px" 
            CssClass="XSmall">
        <table style="height: 22px; width: 647px;">
            <tr>
                      <td  style="width: 70px">
                <asp:Label ID="lblCompany" runat="server" Text="Company" Visible="true"
                  Font-Bold="true"  ></asp:Label>
           </td>
             <td style="width: 89px" >
                <asp:DropDownList ID="ddlCompany"  runat="server"  Visible="true"
                     DataTextField="CompanyName" DataValueField="CompanyId" Font-Size="XX-Small"
                     Width="224px"  AutoPostBack="true" 
                     OnSelectedIndexChanged="ddlCompanyChanged" Height="16px"  >
                 </asp:DropDownList>
            </td>
                  <td style="width: 98px">
                <asp:Label ID="lblFilter" runat="server" Text="Item Group Filter" Visible="true"
                  ></asp:Label>
           </td>

            <td  style="width: 70px">
                <asp:TextBox ID="txtFilter" runat="server" Text="" Visible="true"
                  Font-Bold="true"  ></asp:TextBox>
           </td>
                <td style="width: 62px" >
                         <asp:Button ID="btnFilter" runat="server"  Height="21px" onclick="btnFilter_Click"   Text="Filter" Width="43px" />
                 </td>                               
            </tr>
               
        </table>
    </asp:Panel>

    <asp:panel ID="Pnlgv" runat="server" Width="659px" Height="360px" CssClass="XXSmall" ToolTip="Click On ItemGroup Name link for Updating.."
            GroupingText="Item Group Grid">
        <div id="divItemGroup" runat="server"  
            style="overflow:auto; height:340px; width:643px">

    <asp:GridView ID="GrdItemGroupMaster" runat="server" CellPadding="3" 
             Width="615px" AutoGenerateColumns="False" 
            Height="16px" GridLines="Vertical" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
            OnRowCommand="GrdItemGroup_RowCommand">

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
            <asp:Label ID="lblCompanyId" runat="server" Text='<%# Eval("CompanyId") %>' ></asp:Label></ItemTemplate>
             <HeaderStyle Width="10px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ItemGrp Id"  Visible="false">
            <ItemTemplate>
            <asp:Label ID="lblItemGroupId" runat="server" Text='<%# Eval("ItemGroupId") %>' ></asp:Label></ItemTemplate>
             <HeaderStyle Width="10px" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Item Group Code" Visible="true">
            <ItemTemplate>
            <asp:Label ID="lblItemGroupCode" runat="server"  Width="90px" Text='<%# Eval("ItemGroupCode") %>'  ></asp:Label></ItemTemplate>
              <HeaderStyle Width="90px" HorizontalAlign="Center"/>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ItemGroup Name" Visible="false">
            <ItemTemplate>
            <asp:Label ID="lblItemGroupName" runat="server" Text='<%# Eval("ItemGroupName") %>' Width="60px" ></asp:Label></ItemTemplate>
              <HeaderStyle Width="60px"/>
               </asp:TemplateField>

         <asp:TemplateField HeaderText="ItemGroup Name" Visible="true" >
            <ItemTemplate>  
            <asp:LinkButton ID="lnkCustName" Width="140px" runat ="server" CommandArgument='<%#Eval("ItemGroupId")%>'
             CommandName ="selectItemGroup" Text ='<%#Eval("ItemGroupName") %>'></asp:LinkButton>
              </ItemTemplate> <HeaderStyle Width="140px" HorizontalAlign="Center"/>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Is Active">
             <ItemTemplate>    <asp:CheckBox ID="chkActive" Width="60px" runat="server" Checked='<%# Eval("IsActive") %>'
             Enabled="false"></asp:CheckBox>         </ItemTemplate>
               <HeaderStyle Width="60px" HorizontalAlign="Center"/>     <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            
        </Columns>
        <sortedascendingcellstyle backcolor="#F1F1F1" />
        <sortedascendingheaderstyle backcolor="#0000A9" />
        <sorteddescendingcellstyle backcolor="#CAC9C9" />
        <sorteddescendingheaderstyle backcolor="#000065" />
    </asp:GridView>
    </div>
 
    </asp:panel>
    <br />
    <asp:panel ID="pnlAdd" runat="server" Width="662px"  GroupingText="Add Item Group" CssClass="XXSmall"
            style="margin-top: 0px" Height="57px">
    <table style="width: 98%; height: 39px;" >
            <tr>
 
                    <td style="width: 135px; text-align: left;">
                    <asp:Label ID="lblItemGroupCode" runat="server" Text="Item Group Code"  
                        Font-Bold="True"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtItemGroupCode" runat="server"  height="18px" Width="76px"></asp:TextBox>
                </td>
      
                <td  style="width: 145px; ">
                    <asp:Label ID="lblItemGroupName" runat="server" Text="Item Group Name"  
                        Font-Bold="True"></asp:Label>
                </td>
                <td style="width: 237px">
                    <asp:TextBox ID="txtItemGroupName" runat="server" height="18px" width="216px"></asp:TextBox>
                </td>
 
                <td  style="width: 36px">
                    <asp:CheckBox ID="chkIsActive" runat="server" Text="Is Active" Visible="False" />
                </td>
                <td>
                    <asp:Button ID="btnSave" runat="server" Text="Save"    onclick="btnSave_Click"  />
                </td>
                <td  style="width: 64px">
                    <asp:Button ID="btnClear" runat="server" Text="Clear"  onclick="btnClear_Click"  />
                </td>
                <td  style="width: 35px">
                    <asp:TextBox ID="txtItemGroupId" runat="server"  Visible="false"
                        height="18px" width="16px"></asp:TextBox>
                </td>
                </tr>
               
        </table>
    </asp:panel>
     </div>
</asp:Content>

