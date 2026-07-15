<%@ page title="" language="C#" masterpagefile="~/MasterPage1.Master" autoeventwireup="true" inherits="CRAMIOWHeadMaster, App_Web_lkpdlk5o" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 
  <asp:Label ID="lblIOWHeadMaster" runat="server" Align= "center" Text="CRAM IOW Head Master" CssClass="XSmall"
        width="787px"  Font-Size ="12pt" Font-Bold="True" style="text-align: center"></asp:Label>
 
    <div style="overflow:auto; height: 988px; width: 1246px;">

    <asp:Panel ID="pnlPendind" runat="server" Height="28px" Width="886px" 
            CssClass="XSmall">
        <table style="height: 22px; width: 380px;">
            <tr>
               <td  style="width: 70px">
                <asp:Label ID="lblCompany" runat="server" Text="Company" Visible="true"  Font-Bold="true"   ></asp:Label>
           </td>
             <td style="width: 100px" >
                <asp:DropDownList ID="ddlCompany"  runat="server"  Visible="true" Font-Size="XX-Small"
                    DataTextField="CompanyName" DataValueField="CompanyId"     Width="248px"  AutoPostBack="true" 
                     OnSelectedIndexChanged="ddlCompany_Changed" Height="16px"  >
                 </asp:DropDownList>
            </td>
                  <td style="width: 47px; text-align: left; height: 26px;">
                    <asp:Label ID="Label1" runat="server" Text="Group"    Font-Bold="True"></asp:Label>
                </td>
                    <td style="height: 26px">
                    <asp:DropDownList ID="ddlGroupFilter"  runat="server"  Visible="true" Font-Size="XX-Small"
                     DataTextField="GroupName" DataValueField="GroupCode"     Width="102px"  Height="16px"  AutoPostBack="true" 
                     OnSelectedIndexChanged="ddlGroupFilter_Changed" >
                 </asp:DropDownList>
                </td>
                            <td style="width: 71px; text-align: left; height: 23px;">
                                <asp:Label ID="Label2" runat="server" Font-Bold="True"  Text="Sub Group"></asp:Label>
                            </td>
                            <td style="height: 23px">
                                <asp:DropDownList ID="ddlSubGroupFilter" runat="server" DataTextField="SubGroupName" Font-Size="XX-Small"
                                    DataValueField="SubGroupCode"  Height="16px"   Visible="true" Width="102px">
                                </asp:DropDownList>
                            </td>
                <td style="width: 70px">
                <asp:Label ID="lblFilter" runat="server" Text="Filter" Visible="true"
                  Font-Bold="true"  ></asp:Label>
           </td>

            <td  style="width: 70px">
                <asp:TextBox ID="txtFilter" runat="server" Text="" Visible="true"  Font-Bold="true"   ></asp:TextBox>
           </td>
                <td style="width: 62px" >
                         <asp:Button ID="btnFilter" runat="server" 
                               Height="21px" onclick="btnFilter_Click"  Text="Filter" Width="43px" />
                 </td>      
                             
            </tr>
               
        </table>
    </asp:Panel>

    <asp:panel ID="Pnlgv" runat="server" Width="1172px" Height="319px" CssClass="XXSmall"
            GroupingText="CRAM IOW Grid"   
            ToolTip="Click on IOW Name row to Edit and update">
        <div id="divIOW" runat="server"  style="overflow:auto; height:297px; width:99%">

    <asp:GridView ID="GrdIOWHeadMaster" runat="server" CellPadding="3" 
            Width="99%"  AutoGenerateColumns="False" 
            Height="16px" GridLines="Vertical" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
            OnRowCommand="GrdIOWHead_RowCommand">

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
             <HeaderStyle Width="10px"  HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Grp Code" Visible="true">
            <ItemTemplate>
            <asp:Label ID="lblGroupCode" runat="server" Text='<%# Eval("GroupCode") %>'  ></asp:Label></ItemTemplate>
              <HeaderStyle Width="30px"/><ItemStyle Width = "30px" />
               </asp:TemplateField>
            <asp:TemplateField HeaderText="Grp Name" Visible="true">
            <ItemTemplate>
            <asp:Label ID="lblGroupName" runat="server" Text='<%# Eval("GroupName") %>'  ></asp:Label></ItemTemplate>
              <HeaderStyle Width="70px" HorizontalAlign="Center" /><ItemStyle Width = "70px" />
               </asp:TemplateField>
 
            <asp:TemplateField HeaderText="Sub Grp Code" Visible="true">
            <ItemTemplate>
            <asp:Label ID="lblSubGroupCode" runat="server" Text='<%# Eval("SubGroupCode") %>'  ></asp:Label></ItemTemplate>
              <HeaderStyle Width="30px"/><ItemStyle Width = "30px" />
               </asp:TemplateField>

          <asp:TemplateField HeaderText="Sub Grp Name" Visible="true">
             <ItemTemplate>
            <asp:Label ID="lblSubGroupName" runat="server" Text='<%# Eval("SubGroupName") %>'   Width="70px" ></asp:Label></ItemTemplate>
                <HeaderStyle Width="70px" HorizontalAlign="Center"  />
            </asp:TemplateField>
           <asp:TemplateField HeaderText="IOW level1" Visible="true">
            <ItemTemplate>
            <asp:Label ID="lblIOWLevel1" runat="server" Text='<%# Eval("IOWLevel1") %>' Width="30px" ></asp:Label></ItemTemplate>
              <HeaderStyle Width="30px"  HorizontalAlign="Center" />
            </asp:TemplateField>
           <asp:TemplateField HeaderText="IOW level2" Visible="true">
            <ItemTemplate>
            <asp:Label ID="lblIOWLevel2" runat="server" Text='<%# Eval("IOWLevel2") %>' Width="30px" ></asp:Label></ItemTemplate>
              <HeaderStyle Width="30px"  HorizontalAlign="Center" />
            </asp:TemplateField>
           <asp:TemplateField HeaderText="IOW level3" Visible="true">
            <ItemTemplate>
            <asp:Label ID="lblIOWLevel3" runat="server" Text='<%# Eval("IOWLevel3") %>' Width="40px" ></asp:Label></ItemTemplate>
              <HeaderStyle Width="40px"  HorizontalAlign="Center" />
            </asp:TemplateField>
           <asp:TemplateField HeaderText="IOW level4" Visible="true">
            <ItemTemplate>
            <asp:Label ID="lblIOWLevel4" runat="server" Text='<%# Eval("IOWLevel4") %>' Width="40px" ></asp:Label></ItemTemplate>
              <HeaderStyle Width="40px"  HorizontalAlign="Center" />
            </asp:TemplateField>

           <asp:TemplateField HeaderText="IOW Head Code" Visible="true">
            <ItemTemplate>
            <asp:Label ID="lblIOWHeadCode" runat="server" Text='<%# Eval("IOWHeadCode") %>' Width="50px" ></asp:Label></ItemTemplate>
              <HeaderStyle Width="50px"  HorizontalAlign="Center" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="IOW Head Name" Visible="false">
            <ItemTemplate>
            <asp:Label ID="lblIOWHeadName" runat="server" Text='<%# Eval("IOWHeadName") %>'  ></asp:Label></ItemTemplate>
              <HeaderStyle Width="50px" HorizontalAlign="Center" /><ItemStyle Width = "50px" />
               </asp:TemplateField>
 
         <asp:TemplateField HeaderText="Click IOW Name for Update" Visible="true" >
            <ItemTemplate>  
            <asp:LinkButton ID="lnkIOWHeadName" Width="680px" runat ="server" CommandArgument='<%#Eval("IOWHeadCode")%>'
             CommandName ="UpdateIOW" Text ='<%#Eval("IOWHeadName") %>'></asp:LinkButton>
              </ItemTemplate> <HeaderStyle Width="680px"  HorizontalAlign="Center" />
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
    <asp:panel ID="pnlAdd" runat="server" Width="1170px"  CssClass="XXSmall"
            GroupingText=" Iow Add / Modify"   style="margin-top: 0px" Height="125px">
    <table style="width: 1150px; height: 120px;" >
            <tr>
    
                <td>
                    <asp:Label ID="lblIOWName" runat="server" Text="IOW Head Name"  
                         Font-Bold="True"></asp:Label>
                </td>
                <td >
                    <asp:TextBox ID="txtIOWHeadName" runat="server"  MaxLength="6000" 
                        TextMode="MultiLine"  height="100px" width="819px"></asp:TextBox>
                </td>
                <td style="width: 320px">
                 <asp:panel ID="Panel1" runat="server" Width="205px"  CssClass="XXSmall"
                    style="margin-top: 0px; margin-left: 10px;" Height="111px">
                    <table style="height: 106px; width: 200px">
                 <tr>
                    <td style="width: 71px; text-align: left; height: 20px;">
                    <asp:Label ID="lblIOWHeadCode" runat="server" Text="IOW Code"  
                         Font-Bold="True"></asp:Label>
                </td>
                    <td style="height: 20px">
                    <asp:TextBox ID="txtIOWHeadCode" runat="server"  MaxLength="16"
                        height="18px" Width="89px"></asp:TextBox>
                </td>
                </tr>
                <tr>
                    <td style="width: 71px; text-align: left; height: 26px;">
                    <asp:Label ID="lblGroup" runat="server" Text="Group"  
                         Font-Bold="True"></asp:Label>
                </td>
                    <td style="height: 26px">
                    <asp:DropDownList ID="ddlGroup"  runat="server"  Visible="true"
                    DataTextField="GroupName" DataValueField="GroupCode" Font-Size="XX-Small"
                    Width="102px"  Height="16px" AutoPostBack="true"    OnSelectedIndexChanged = "Groupddlchanged" >
                 </asp:DropDownList>
                </td>
                        <td style="width: 64px; height: 23px;">
                                <asp:Button ID="btnClear" runat="server" onclick="btnClear_Click" Text="Clear" />
                            </td>
            </tr>
                <tr>
                            <td style="width: 71px; text-align: left; height: 23px;">
                                <asp:Label ID="lblSubGroup" runat="server" Font-Bold="True" 
                                     Text="Sub Group"></asp:Label>
                            </td>
                            <td style="height: 23px">
                                <asp:DropDownList ID="ddlsubGroup" runat="server" DataTextField="SubGroupName" 
                                    DataValueField="SubGroupCode"  Height="16px"  Font-Size="XX-Small"    Visible="true" Width="102px">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 41px; height: 23px;">
                                <asp:Button ID="btnSave" runat="server"  BackColor="LightGreen" 
                                    onclick="btnSave_Click" Text="Save" />
                            </td>
 
                 
                        </tr>
            </table>
            </asp:panel>
            </td>
            </tr>
 
               
        </table>
    </asp:panel>
           
 
    </div>
</asp:Content>

