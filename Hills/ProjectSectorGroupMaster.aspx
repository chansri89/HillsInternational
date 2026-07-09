<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.master" AutoEventWireup="true" CodeFile="ProjectSectorGroupMaster.aspx.cs" Inherits="ProjectSectorGroupMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%--<asp:Panel ID="Panel2" runat="server" Height="16px" Width="870px"> </asp:Panel>--%>
<asp:Label ID="lblStatMas" runat="server" Align= "center" Text="Project Sector Group " 
        width="450px" Font-Size ="12pt" Font-Bold="True" 
        style="text-align: center" Height="26px"></asp:Label>
<div style="overflow:auto; height: 872px;">
    <%--<asp:Panel ID="Panel1" runat="server" Height="41px">
        <asp:Button ID="btnAdd" runat="server" Text="Add"
            Font-Names="arial" onclick="btnAdd_Click1" />
    </asp:Panel>--%>
    <asp:panel ID="Pnlgv" runat="server" Height="394px" Width="512px" 
        CssClass="XXSmall" ToolTip="Click Edit for Updating.."
        GroupingText="Sector Group Grid" >
        <div style="overflow:auto; height:373px; width:502px">
    <asp:GridView ID="GrdSectorGrp" runat="server" CellPadding="3" 
            Width="482px"  AutoGenerateColumns="False" Height="102px" GridLines="Vertical" 
                BackColor="White" BorderColor="#999999" 
                BorderStyle="None" BorderWidth="1px" 
                onrowcancelingedit="GrdSectorGrp_RowCancelingEdit"                
                onrowediting="GrdSectorGrp_RowEditing" 
                onrowupdating="GrdSectorGrp_RowUpdating"
                 OnRowCommand="GrdSectorGrp_RowCommand">
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" 
            HorizontalAlign="Left" />
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
         <asp:TemplateField HeaderText="ProjectSectorGroupId" Visible="False" >
            <ItemTemplate>
            <asp:Label ID="lblProjectSectorGroupId" runat="server" Text='<%# Eval("ProjectSectorGroupId") %>'  ></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtProjectSectorGroupId" runat="server" Text='<%# Bind("ProjectSectorGroupId") %>' ReadOnly="true"  Font-Size="X-Small"></asp:TextBox></EditItemTemplate>
                <HeaderStyle Width="40px" />
            </asp:TemplateField>

             <asp:TemplateField HeaderText="Sector Group Name" >
            <ItemTemplate>
            <asp:Label ID="lblProjectSectorGroupName" runat="server" Text='<%# Eval("ProjectSectorGroupName") %>'  Width="200px"></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtProjectSectorGroupName" runat="server" Text='<%# Bind("ProjectSectorGroupName") %>' Width="200px"></asp:TextBox></EditItemTemplate>
                <HeaderStyle Width="200px" />
                 <ItemStyle Width="200px" />
            </asp:TemplateField>

         <asp:TemplateField HeaderText="Add Class" >
            <ItemTemplate>  
            <asp:LinkButton ID="lnkCustName" Width="50px" runat ="server" CommandArgument='<%#Eval("ProjectSectorGroupId")%>'
             CommandName ="AddClass" Text ="Add Class"></asp:LinkButton>
              </ItemTemplate> <HeaderStyle Width="50px" />
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
    <asp:panel ID="pnlAdd" runat="server" Width="520px" GroupingText="Add Sector Group" 
       >
    <table style="width: 95%"  align="left" class="XXSmall">
            <tr>
                <td  >
                    <asp:Label ID="lblProjectSectorGroupName" runat="server" Text="Sector Group Name"  
                        Font-Bold="True"></asp:Label>
                </td>
                <td >
                    <asp:TextBox ID="txtProjectSectorGroupName" runat="server" Width="270px"></asp:TextBox>
                </td>
  
                <td style="width: 69px" >
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
        <asp:panel ID="pnlClass" runat="server" Width="572px" Height="363px"  
        GroupingText="SectorClass Grid" CssClass="XXSmall" >

        <div id="divSectorClass" runat="server"  
                style="overflow:auto; height:342px; width:98%">
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
    <asp:GridView ID="GrdSectorClass" runat="server" CellPadding="3" 
            Width="98%"  AutoGenerateColumns="False" 
            Height="16px" GridLines="Vertical" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
           >

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

     <asp:panel ID="pnlAddClass" runat="server" Width="572px" 
        GroupingText="Add Sector Class" CssClass="XXSmall"
            style="margin-top: 0px" Height="65px">
    <table style="width: 91%; height: 45px;" >
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
                    <asp:Button ID="btnSaveClass" runat="server" Text="Save Class"  
                        onclick="btnSaveClass_Click" Width="79px"  />
                </td>
                <td >
                    <asp:Button ID="btnBack" runat="server" Text="Back"  onclick="btnBack_Click" 
                        Width="40px"  />
                </td>
 
                </tr>
               
        </table>
    </asp:panel>  
    </div>

</asp:Content>

