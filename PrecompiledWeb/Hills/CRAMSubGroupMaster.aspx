<%@ page title="" language="C#" masterpagefile="~/MasterPage1.Master" autoeventwireup="true" inherits="CRAMSubGroupMaster, App_Web_mlnhxkpa" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%-- <asp:Label ID = "lblSeparator" runat = "server" align = "center" Height="15px" Width = "901px" ></asp:Label>--%>

  <asp:Label ID="lblIOWGroupMaster" runat="server" Align= "center" Text="IOW Sub Group Master" 
        width="782px"   Font-Size ="12pt" Font-Bold="True"    
        style="text-align: center"></asp:Label>
 <%--  <asp:Label ID = "lblSeparator1" runat = "server" align = "center" Height="15px" Width = "901px" ></asp:Label>--%>
 <%--   <div style="overflow:auto; height: 471px; width: 1202px;">--%>
        <%--</td></tr>
    </table>--%>    <%--</asp:Panel>DataKeyNames="IOW GroupCode"--%>
    <asp:Panel ID="pnlPendind" runat="server" Height="30px" Width="785px" 
        CssClass="XSmall">
        <table style="height: 22px; width: 667px;">
            <tr>
                      <td >
                <asp:Label ID="lblCompany" runat="server" Text="Company" Visible="true" Font-Bold="true" ></asp:Label>
           </td>
             <td style="width: 250px"  >
                <asp:DropDownList ID="ddlCompany"  runat="server"  Visible="true" Font-Size="XX-Small"
                     DataTextField="CompanyName" DataValueField="CompanyId"  Width="200px"  AutoPostBack="true" 
                     OnSelectedIndexChanged="ddlCompanyChanged" Height="16px"  >
                 </asp:DropDownList>
            </td>

                  <td class="style23" style="width: 109px">
                <asp:Label ID="lblFilter" runat="server" Text="SubGroup Filter" Visible="true"
                  Font-Bold="true" ></asp:Label>
           </td>

            <td  style="width: 70px">
                <asp:TextBox ID="txtFilter" runat="server" Text="" Visible="true"
                  Font-Bold="true"  ></asp:TextBox>
           </td>
                <td  style="width: 62px" >
                         <asp:Button ID="btnFilter" runat="server" Height="21px" onclick="btnFilter_Click"  
                               Text="Filter" Width="43px" />
                 </td> 
             <td  style="width: 10px">
                <asp:TextBox ID="txtGroupCode" runat="server" Text="" Visible="false"
                  Font-Bold="true"   Width="16px" ></asp:TextBox>
           </td>    
          
                             
            </tr>
               
        </table>
    </asp:Panel>

    <asp:panel ID="Pnlgv" runat="server" Width="782px" Height="368px" 
            CssClass="XXSmall" GroupingText="IOW Sub Group Grid">
        <div id="divIOWGroup" runat="server"   
            style="overflow:auto; height:347px; width:769px">

    <asp:GridView ID="GrdIOWSubGroupMaster" runat="server" CellPadding="3" 
            Width="752px" Font-Names="Verdana" AutoGenerateColumns="False" 
            Height="16px" GridLines="Vertical" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
            OnRowCommand="GrdIOWSubGroup_RowCommand">
 
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
            <asp:Label ID="lblCRAMSubGroupId" runat="server" Text='<%# Eval("CRAMSubGroupId") %>' ></asp:Label></ItemTemplate>
             <HeaderStyle Width="10px" />
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Group Code" Visible="true">
            <ItemTemplate>
            <asp:Label ID="lblGroupCode" runat="server"  Width="60px" Text='<%# Eval("GroupCode") %>' ></asp:Label></ItemTemplate>
              <HeaderStyle Width="60px" HorizontalAlign="Center"/>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Group Name" Visible="true">
            <ItemTemplate>
            <asp:Label ID="lblGroupName" runat="server"  Width="120px" Text='<%# Eval("GroupName") %>' ></asp:Label></ItemTemplate>
              <HeaderStyle Width="120px" HorizontalAlign="Center"/>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Sub Group Code" Visible="true">
            <ItemTemplate>
            <asp:Label ID="lblSubGroupCode" runat="server"  Width="90px" Text='<%# Eval("SubGroupCode") %>' ></asp:Label></ItemTemplate>
              <HeaderStyle Width="90px" HorizontalAlign="Center"/>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Group Name" Visible="false">
            <ItemTemplate>
            <asp:Label ID="lblSubGroupName" runat="server" Text='<%# Eval("SubGroupName") %>' Width="60px"></asp:Label></ItemTemplate>
              <HeaderStyle Width="60px"/>
               </asp:TemplateField>

         <asp:TemplateField HeaderText="Sub Group Name" Visible="true" >
            <ItemTemplate>  
            <asp:LinkButton ID="lnkSubGroupName" Width="150px" runat ="server" CommandArgument='<%#Eval("CRAMSubGroupId")%>'
             CommandName ="selectsubGroup" Text ='<%#Eval("SubGroupName") %>'></asp:LinkButton>
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
    <asp:panel ID="pnlAdd" runat="server" Width="785px" Font-Size="X-Small" 
        GroupingText="Add IOW Group" CssClass="XSmall"
            style="margin-top: 0px" Height="57px">
    <table style="width: 99%; height: 39px;" >
            <tr>
                     <td  style="width: 51px">
                <asp:Label ID="lblGroup" runat="server" Text="Group Name" Visible="true"
                  Font-Bold="true"  ></asp:Label>
           </td>
             <td style="width: 100px" >
                <asp:DropDownList ID="ddlGroupName"  runat="server"  Visible="true" Font-Size="XX-Small"
                    DataTextField="GroupName" DataValueField="GroupCode"  Width="146px" 
                     Height="17px"    >
                 </asp:DropDownList>
            </td>
                    <td style="width: 82px; text-align: left;" >
                    <asp:Label ID="lblSubGroupCode" runat="server" Text="Sub Group Code"  
                        Font-Bold="True"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtSubGroupCode" runat="server" height="18px" Width="58px"></asp:TextBox>
                </td>
      
                <td  style="width: 89px; ">
                    <asp:Label ID="lblSubGroupName" runat="server" Text="Sub Group Name"  
                        Font-Bold="True"></asp:Label>
                </td>
                <td style="width: 208px">
                    <asp:TextBox ID="txtSubGroupName" runat="server" height="18px" width="169px"></asp:TextBox>
                </td>
 
                <td class="style28" style="width: 36px">
                    <asp:CheckBox ID="chkIsActive" runat="server"  Visible="false"     Text="Is Active" Checked="true" />
                </td>
                <td>
                    <asp:Button ID="btnSave" runat="server" Text="Save"      onclick="btnSave_Click"  />
                </td>
                <td class="style28" style="width: 64px">
                    <asp:Button ID="btnClear" runat="server" Text="Clear"   onclick="btnClear_Click"  />
                </td>
                                <td  style="width: 35px">
                    <asp:TextBox ID="txtCRAMSubGroupId" runat="server"  Visible="false"
                        height="18px" width="16px"></asp:TextBox>
                </td>
                </tr>
               
        </table>
    </asp:panel>

  <%--  </div>--%>
</asp:Content>

