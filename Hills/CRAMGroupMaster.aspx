<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.Master" AutoEventWireup="true" CodeFile="CRAMGroupMaster.aspx.cs" Inherits="CRAMGroupMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%-- <asp:Label ID = "lblSeparator" runat = "server" align = "center" Height="15px" Width = "901px" ></asp:Label>--%>

  <asp:Label ID="lblIOWGroupMaster" runat="server" Align= "center" Text="IOW Group Master" 
        width="787px"    Font-Size ="12pt" Font-Bold="True"     style="text-align: center"></asp:Label>

    <div style="overflow:auto; height: 471px; width: 1202px;">
 
    <asp:Panel ID="pnlPendind" runat="server" Height="28px" Width="647px" CssClass="XXSmall">
        <table style="height: 22px; width: 627px;">
            <tr>
                      <td  style="width: 58px" class="style32">
                <asp:Label ID="lblCompany" runat="server" Text="Company" Visible="true"        Font-Bold="true"   ></asp:Label>
           </td>
             <td style="width: 100px" >
                <asp:DropDownList ID="ddlCompany"  runat="server"  Font-Size="XX-Small"
                    DataTextField="CompanyName" DataValueField="CompanyId"    Width="240px"  AutoPostBack="true" 
                     OnSelectedIndexChanged="ddlCompanyChanged" Height="16px"  >
                 </asp:DropDownList>
            </td>
                  <td  style="width: 73px">
                <asp:Label ID="lblFilter" runat="server" Text="IOW Group Filter" Visible="true"
                  Font-Bold="true"   ></asp:Label>
           </td>

            <td style="width: 70px">
                <asp:TextBox ID="txtFilter" runat="server" Text="" Visible="true"   
                    Font-Bold="true"  ></asp:TextBox>
           </td>
                <td  style="width: 62px" >
                         <asp:Button ID="btnFilter" runat="server" Font-Size="X-Small" Height="21px" onclick="btnFilter_Click"   
                               Text="Filter" Width="43px" />
                 </td>     
 
            </tr>
               
        </table>
    </asp:Panel>

    <asp:panel ID="Pnlgv" runat="server" Width="594px" Height="360px" CssClass="XSmall"       GroupingText="IOW Group Grid">
        <div id="divIOWGroup" runat="server"   style="overflow:auto; height:340px; width:575px">

    <asp:GridView ID="GrdIOWGroupMaster" runat="server" CellPadding="3" 
           Width="549px" AutoGenerateColumns="False" Height="16px" GridLines="Vertical" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
            OnRowCommand="GrdIOWGroup_RowCommand">

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
            <asp:TemplateField HeaderText="Grp Id"  Visible="false">
            <ItemTemplate>
            <asp:Label ID="lblCRAMGroupId" runat="server" Text='<%# Eval("CRAMGroupId") %>' ></asp:Label></ItemTemplate>
             <HeaderStyle Width="10px" />
            </asp:TemplateField>
 <%--               <asp:TemplateField HeaderText="ItemCategory Name" Visible="true">
             <ItemTemplate>
            <asp:Label ID="lblItemCategoryName" runat="server" Text='<%# Eval("ItemCategoryName") %>' Width="120px"></asp:Label></ItemTemplate>
                <HeaderStyle Width="120px" />
            </asp:TemplateField>
            --%>
            <asp:TemplateField HeaderText="Group Code" Visible="true">
            <ItemTemplate>
            <asp:Label ID="lblGroupCode" runat="server"  Width="60px" Text='<%# Eval("GroupCode") %>' ></asp:Label></ItemTemplate>
              <HeaderStyle Width="60px" HorizontalAlign="Center"/>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Group Name" Visible="false">
            <ItemTemplate>
            <asp:Label ID="lblGroupName" runat="server" Text='<%# Eval("GroupName") %>' Width="60px"></asp:Label></ItemTemplate>
              <HeaderStyle Width="60px"/>
               </asp:TemplateField>

         <asp:TemplateField HeaderText="Group Name" Visible="true" >
            <ItemTemplate>  
            <asp:LinkButton ID="lnkIOWName" Width="150px" runat ="server" CommandArgument='<%#Eval("CRAMGroupId")%>'
             CommandName ="selectIOWGroup" Text ='<%#Eval("GroupName") %>'></asp:LinkButton>
              </ItemTemplate> <HeaderStyle Width="150px" HorizontalAlign="Center"/>
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
    <br />
    <asp:panel ID="pnlAdd" runat="server" Width="600px" Font-Size="X-Small" GroupingText="Add IOW Group" 
            style="margin-top: 0px" Height="57px">
    <table style="width: 98%; height: 39px;" >
            <tr>
 
                    <td style="width: 71px; text-align: left;">
                    <asp:Label ID="lblIOWGroupCode" runat="server" Text="Group Code"  
                        Font-Bold="True"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtGroupCode" runat="server" 
                         height="18px" Width="76px"></asp:TextBox>
                </td>
      
                <td class="style21" style="width: 145px; ">
                    <asp:Label ID="lblGroupName" runat="server" Text="IOW Group Name"  
                        Font-Bold="True"></asp:Label>
                </td>
                <td style="width: 192px">
                    <asp:TextBox ID="txtGroupName" runat="server" 
                        height="18px" width="190px"></asp:TextBox>
                </td>
 

                <td>
                    <asp:Button ID="btnSave" runat="server" Text="Save"  onclick="btnSave_Click" />
                </td>
                <td class="style28" style="width: 64px">
                    <asp:Button ID="btnClear" runat="server" Text="Clear" 
                        onclick="btnClear_Click"  />
                </td>
                                <td class="style28" style="width: 20px">
                    <asp:TextBox ID="txtCRAMGroupId" runat="server" Visible="false"
                        height="18px" width="10px"></asp:TextBox>
                </td>
                <td class="style28" style="width: 36px">
                    <asp:CheckBox ID="chkIsActive" runat="server"   Text="Is Active" Visible="False" Checked="true" />
                </td>
                </tr>
               
        </table>
    </asp:panel>

    </div>
</asp:Content>

