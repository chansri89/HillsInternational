<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.master" AutoEventWireup="true" CodeFile="ItemSubCategoryMaster.aspx.cs" Inherits="ItemSubCategoryMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="Panel2" runat="server" Height="16px" Width="870px" CssClass="XSmall"> </asp:Panel>
<asp:Label ID="lblStatMas" runat="server" Align= "center" Text="Item SubCategory Master" 
        width="450px" Font-Size ="12pt" Font-Bold="True"  style="text-align: center" Height="26px"></asp:Label>
<div style="overflow:auto; height: 664px;">
    <asp:Panel ID="pnlPendind" runat="server" Height="28px" Width="644px" CssClass="XSmall">
        <table style="height: 22px; width: 634px;">
            <tr>
                      <td style="width: 70px">
                <asp:Label ID="Label1" runat="server" Text="Company" Visible="true"
                  Font-Bold="true"   ></asp:Label>
           </td>
             <td style="width: 100px" >
                <asp:DropDownList ID="ddlCompany"  runat="server"  Visible="true"
                    DataTextField="CompanyName" DataValueField="CompanyId"   Width="197px"  AutoPostBack="true" 
                     OnSelectedIndexChanged="ddlCompanyChanged" Height="16px"  >
                 </asp:DropDownList>
            </td>
                  <td style="width: 104px">
                <asp:Label ID="lblFilter" runat="server" Text="Filter SubCategory Name" Visible="true"
                  Font-Size="X-Small" ></asp:Label>
           </td>

            <td style="width: 70px">
                <asp:TextBox ID="txtFilter" runat="server" Text="" Visible="true"
                  Font-Bold="true"   ></asp:TextBox>
           </td>
                <td style="width: 62px" >
                         <asp:Button ID="btnFilter" runat="server"  Height="21px" onclick="btnFilter_Click"  Text="Filter" Width="43px" />
                 </td>     
            </tr>
               
        </table>
    </asp:Panel>
    <asp:panel ID="Pnlgv" runat="server" Height="350px" Width="779px" CssClass="XSmall" ToolTip="Click On Edit for Updating.."
        GroupingText="Edit ItemSubCategory Grid">
        <div style="overflow:auto; height:330px; width:765px">
    <asp:GridView ID="GrdItemSubCategoryMaster" runat="server" CellPadding="3" 
             Width="755px" AutoGenerateColumns="False" Height="102px" GridLines="Vertical" 
                BackColor="White" BorderColor="#999999" 
                BorderStyle="None" BorderWidth="1px" 
                onrowcancelingedit="GrdItemSubCategoryMaster_RowCancelingEdit"                
                onrowediting="GrdItemSubCategoryMaster_RowEditing" 
                onrowupdating="GrdItemSubCategoryMaster_RowUpdating">
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" 
            HorizontalAlign="Left" />
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
         <asp:TemplateField HeaderText="Item SubCategoryId" Visible="false" >
            <ItemTemplate>
            <asp:Label ID="lblItemSubCategoryId" runat="server" Text='<%# Eval("ItemSubCategoryId") %>' Width="20px" ></asp:Label></ItemTemplate>
             <HeaderStyle Width="20px" />  </asp:TemplateField>
                 <asp:TemplateField HeaderText="Company Id" Visible="true" >
            <ItemTemplate>
            <asp:Label ID="lblCompanyId" runat="server" Text='<%# Eval("CompanyId") %>' Width="30px"  ></asp:Label></ItemTemplate>
             <HeaderStyle Width="30px" />  </asp:TemplateField>

         <asp:TemplateField HeaderText="Item SubCategory Code" Visible="true" >
            <ItemTemplate>
            <asp:Label ID="lblItemSubCategoryCode" runat="server" Width="40px"  Text='<%# Eval("ItemSubCategoryCode") %>' ></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtItemSubCategoryCode" runat="server" Width="40px" Text='<%# Bind("ItemSubCategoryCode") %>'></asp:TextBox></EditItemTemplate>
                <HeaderStyle Width="40px" />
            </asp:TemplateField>

             <asp:TemplateField HeaderText="Item SubCategory Name" >
            <ItemTemplate>
            <asp:Label ID="lblItemSubCategoryName" runat="server" Text='<%# Eval("ItemSubCategoryName") %>'  Width="170px"></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtItemSubCategoryName" runat="server" Text='<%# Bind("ItemSubCategoryName") %>'  Width="170px"></asp:TextBox></EditItemTemplate>
                <HeaderStyle Width="170px" />
                 <ItemStyle Width="170px" />
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
    <asp:panel ID="pnlAdd" runat="server" Width="774px" 
        GroupingText="Add ItemSubCategory" CssClass="XSmall"
        Height="52px">
    <table style="width: 99%"  align="left">
            <tr>
                 <td style="width: 120px; text-align: left;">
                    <asp:Label ID="lblItemSubCategoryCode" runat="server" Text="Item Sub Category Code"  
                       Font-Bold="True"></asp:Label>
                </td>
                <td style="width: 129px">
                    <asp:TextBox ID="txtItemSubCategorycode" runat="server" MaxLength="8" Width="114px" 
                      ></asp:TextBox>
                </td>
                <td style="width: 123px; text-align: left;">
                    <asp:Label ID="lblItemSubCategoryName" runat="server" Text="Item Sub Category Name"  
                       Font-Bold="True"></asp:Label>
                </td>
                <td style="width: 129px">
                    <asp:TextBox ID="txtItemSubCategoryName" runat="server" MaxLength="64"
                        Width="218px"></asp:TextBox>
                </td>
               <td style="width: 59px" class="style28">
                    <asp:CheckBox ID="ChkIsActive" runat="server" Text="IsActive"
                      Checked="true" Width="68px"></asp:CheckBox>
                </td>
                
                <td style="width: 63px" class="style23">
                    <asp:Button ID="btnSave" runat="server" Text="Save"
                        onclick="btnSave_Click"  />
                </td>
                </tr>
 
        </table>
    </asp:panel>
    
    </div>

</asp:Content>

