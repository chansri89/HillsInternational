<%@ page title="" language="C#" masterpagefile="~/MasterPage1.Master" autoeventwireup="true" inherits="ProjectSectorClassMaster, App_Web_hmntzrlf" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%-- <asp:Label ID = "lblSeparator" runat = "server" align = "center" Height="15px" Width = "901px" ></asp:Label>--%>

  <asp:Label ID="lblClientMaster" runat="server" Align= "center" Text="Client / Tenderer Master"  CssClass="XSmall"
       width="787px"    Font-Size ="12pt" Font-Bold="True"  style="text-align: center"></asp:Label>
  <%-- <asp:Label ID = "lblSeparator1" runat = "server" align = "center" Height="15px" Width = "901px" ></asp:Label>--%>
    <div style="overflow:auto; height: 471px; width: 1202px;">

    <asp:Panel ID="pnlPendind" runat="server" Height="31px" Width="380px" 
            CssClass="XSmall">
        <table style="height: 22px; width: 357px;">
            <tr>
    
                  <td >
                <asp:Label ID="lblFilter" runat="server" Text="Filter Class Name" 
                   ></asp:Label>
           </td>

            <td  style="width: 70px">
                <asp:TextBox ID="txtFilter" runat="server" Text="" 
                  Font-Bold="true" Width="137px"   ></asp:TextBox>
           </td>
                <td style="width: 62px" >
                         <asp:Button ID="btnFilter" runat="server"  Height="21px" onclick="btnFilter_Click" 
                               Text="Filter" Width="43px" />
                 </td>     
          
                             
            </tr>
               
        </table>
    </asp:Panel>

    <asp:panel ID="Pnlgv" runat="server" Width="674px" Height="345px" ToolTip="Click on Sector Class Link to Update.."
            GroupingText="SectorClass Grid" CssClass="XXSmall">
        <div id="divSectorClass" runat="server"  
            style="overflow:auto; height:324px; width:98%">

    <asp:GridView ID="GrdSectorClass" runat="server" CellPadding="3" 
            Width="98%"  AutoGenerateColumns="False" 
            Height="16px" GridLines="Vertical" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
            OnRowCommand="GrdSectorClass_RowCommand">

        <EditRowStyle Font-Size="XX-Small" />
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White"  />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" 
            HorizontalAlign="Left" />
        <AlternatingRowStyle BackColor="#DCDCDC" />

       
        <Columns>
            <asp:TemplateField HeaderText="ProjectSectorGroupId"  Visible="false">
            <ItemTemplate>
            <asp:Label ID="lblProjectSectorGroupId" runat="server" Text='<%# Eval("ProjectSectorGroupId") %>'  ></asp:Label></ItemTemplate>
             <HeaderStyle Width="10px" />
            </asp:TemplateField>
         <%--   <asp:TemplateField HeaderText="Cust Id"  Visible="false">
            <ItemTemplate>
            <asp:Label ID="lblClientId" runat="server" Text='<%# Eval("ClientId") %>'  ></asp:Label></ItemTemplate>
             <HeaderStyle Width="10px" />
            </asp:TemplateField>--%>
            
            <asp:TemplateField HeaderText="Sector Group Name" Visible="true">
            <ItemTemplate>
            <asp:Label ID="lblProjectSectorGroupName" runat="server"  Width="120px" Text='<%# Eval("ProjectSectorGroupName") %>'  ></asp:Label></ItemTemplate>
              <HeaderStyle Width="120px" HorizontalAlign="Center"/>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ProjectSectorsubGroupId" Visible="false">
            <ItemTemplate>
            <asp:Label ID="lblProjectSectorSubGroupId" runat="server" Text='<%# Eval("ProjectSectorSubGroupId") %>'  ></asp:Label></ItemTemplate>
              <HeaderStyle Width="50px"/>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Sector Class Name" Visible="true">
            <ItemTemplate>
            <asp:Label ID="lblProjectSectorClass" runat="server"  Width="130px" Text='<%# Eval("ProjectSectorClass") %>'  ></asp:Label></ItemTemplate>
              <HeaderStyle Width="130px" HorizontalAlign="Center"/>
            </asp:TemplateField>

         <asp:TemplateField HeaderText="Click Sector Class" Visible="true" >
            <ItemTemplate>  
            <asp:LinkButton ID="lnkSectorClass" Width="150px" runat ="server" CommandArgument='<%#Eval("ProjectSectorSubGroupId")%>'
             CommandName ="select" Text ='<%#Eval("ProjectSectorClass") %>'></asp:LinkButton>
              </ItemTemplate> <HeaderStyle Width="150px" HorizontalAlign="Center" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Is Active">
             <ItemTemplate>    <asp:CheckBox ID="chkActive" Width="30px" runat="server" Checked='<%# Eval("IsActive") %>'
             Enabled="false"></asp:CheckBox>         </ItemTemplate>
               <HeaderStyle Width="30px"  HorizontalAlign="Center"/>     <ItemStyle HorizontalAlign="Center" />
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
    <asp:panel ID="pnlAdd" runat="server" Width="677px" GroupingText="Add Sector Class" CssClass="XXSmall"
            style="margin-top: 0px" Height="65px">
    <table style="width: 98%; height: 45px;" >
            <tr>
      
                <td>
                    <asp:Label ID="lblProjectSectorClass" runat="server" Text="Sector Class"  
                         Font-Bold="True"></asp:Label>
                </td>

                 <td>
                    <asp:TextBox ID="txtProjectSectorClass" runat="server"  height="24px" 
                         Width="192px"></asp:TextBox>
                </td>

             <td >
                    <asp:Label ID="lblProjectSectorGroup" runat="server" Text="Sector Group"  
                         Font-Bold="True"></asp:Label>
                </td>
                <td >
                    <asp:DropDownList ID="ddlProjectSectorGroup"  runat="server"  Font-Size="XX-Small"
                    DataTextField="ProjectSectorGroupName" DataValueField="ProjectSectorGroupId"
                    Width="115px"  Height="24px"  >
                       
                 </asp:DropDownList>
                </td>
              
                <td >
                    <asp:CheckBox ID="chkIsActive" runat="server" Text="IsActive" Visible="False" />
                </td>
                <td >
                    <asp:Button ID="btnSave" runat="server" Text="Save"  onclick="btnSave_Click"  />
                </td>
                <td >
                    <asp:Button ID="btnClear" runat="server" Text="Clear"  onclick="btnClear_Click"  />
                </td>
                 <td >
                    <asp:TextBox ID="txtProjectSectorGroupId" runat="server" Visible="false"
                        height="16px" width="10px"></asp:TextBox>
                </td>
                <td >
                    <asp:TextBox ID="txtProjectSectorSubgroupId" runat="server" Visible="false"
                        height="16px" width="10px"></asp:TextBox>
                </td>
                </tr>
               
        </table>
    </asp:panel>
    <%--</td></tr>
    </table>--%>
    <%--</asp:Panel>--%>
    </div>
</asp:Content>

