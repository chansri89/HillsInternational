<%@ page title="" language="C#" masterpagefile="~/MasterPage1.Master" autoeventwireup="true" inherits="ClientMaster, App_Web_lkpdlk5o" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%-- <asp:Label ID = "lblSeparator" runat = "server" align = "center" Height="15px" Width = "901px" ></asp:Label>--%>

  <asp:Label ID="lblClientMaster" runat="server" Align= "center" Text="Client / Tenderer Master"  CssClass="XSmall"
       width="787px"    Font-Size ="12pt" Font-Bold="True"  style="text-align: center"></asp:Label>
  <%-- <asp:Label ID = "lblSeparator1" runat = "server" align = "center" Height="15px" Width = "901px" ></asp:Label>--%>
    <div style="overflow:auto; height: 471px; width: 1202px;">

    <asp:Panel ID="pnlPendind" runat="server" Height="31px" Width="530px" 
            CssClass="XSmall">
        <table style="height: 22px; width: 380px;">
            <tr>
                      <td  style="width: 70px">
                <asp:Label ID="lblCompany" runat="server" Text="Company" 
                  Font-Bold="true"  ></asp:Label>
           </td>
             <td style="width: 100px">
                <asp:DropDownList ID="ddlCompany"  runat="server" Font-Size="XX-Small"
                    DataTextField="CompanyName" DataValueField="CompanyId"
                   Width="248px"  AutoPostBack="true"        OnSelectedIndexChanged="ddlCompanyChanged" Height="16px"  >
                 </asp:DropDownList>
            </td>
                  <td style="width: 70px">
                <asp:Label ID="lblFilter" runat="server" Text="Filter" 
                  Font-Bold="true"  ></asp:Label>
           </td>

            <td  style="width: 70px">
                <asp:TextBox ID="txtFilter" runat="server" Text="" 
                  Font-Bold="true"   ></asp:TextBox>
           </td>
                <td style="width: 62px" >
                         <asp:Button ID="btnFilter" runat="server"  Height="21px" onclick="btnFilter_Click" 
                               Text="Filter" Width="43px" />
                 </td>     
          
                             
            </tr>
               
        </table>
    </asp:Panel>

    <asp:panel ID="Pnlgv" runat="server" Width="1057px" Height="280px" 
            GroupingText="Client Grid" CssClass="XXSmall">
        <div id="divClient" runat="server"  
            style="overflow:auto; height:262px; width:1043px">

    <asp:GridView ID="GrdClientMaster" runat="server" CellPadding="3" 
            Width="1028px"  AutoGenerateColumns="False" 
            Height="16px" GridLines="Vertical" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
            OnRowCommand="GrdClient_RowCommand">

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
         <%--   <asp:TemplateField HeaderText="Cust Id"  Visible="false">
            <ItemTemplate>
            <asp:Label ID="lblClientId" runat="server" Text='<%# Eval("ClientId") %>'  ></asp:Label></ItemTemplate>
             <HeaderStyle Width="10px" />
            </asp:TemplateField>--%>
            
            <asp:TemplateField HeaderText="Client Code" Visible="true">
            <ItemTemplate>
            <asp:Label ID="lblClientCode" runat="server"  Width="30px" Text='<%# Eval("ClientCode") %>'  ></asp:Label></ItemTemplate>
              <HeaderStyle Width="30px" HorizontalAlign="Center"/>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Cli Name" Visible="false">
            <ItemTemplate>
            <asp:Label ID="lblClientName" runat="server" Text='<%# Eval("ClientName") %>'  ></asp:Label></ItemTemplate>
              <HeaderStyle Width="50px"/>
               </asp:TemplateField>
         <asp:TemplateField HeaderText="Client Name" Visible="true" >
            <ItemTemplate>  
            <asp:LinkButton ID="lnkCustName" Width="170px" runat ="server" CommandArgument='<%#Eval("ClientCode")%>'
             CommandName ="selectClient" Text ='<%#Eval("ClientName") %>'></asp:LinkButton>
              </ItemTemplate> <HeaderStyle Width="170px" HorizontalAlign="Center" />
            </asp:TemplateField>

           
            <asp:TemplateField HeaderText="Cust Sh Name">
             <ItemTemplate>
            <asp:Label ID="lblClientShortName" runat="server" Text='<%# Eval("ClientShortName") %>'  ></asp:Label></ItemTemplate>
                <HeaderStyle Width="50px"  HorizontalAlign="Center"/>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Client Type" Visible="true">
             <ItemTemplate>
            <asp:Label ID="lblClientType" runat="server" Text='<%# Eval("ClientType") %>' Width="40px"   ></asp:Label></ItemTemplate>
                <HeaderStyle Width="40px"  HorizontalAlign="Center"/>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Building Name" Visible="true">
             <ItemTemplate>
            <asp:Label ID="lblBuildingName" runat="server" Text='<%# Eval("BuildingName") %>'   Width="80px" ></asp:Label></ItemTemplate>
                <HeaderStyle Width="80px"  HorizontalAlign="Center"/>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Address1" Visible="true">
             <ItemTemplate>
            <asp:Label ID="lblClientAddress1" runat="server" Text='<%# Eval("Addr1") %>'   Width="200px" ></asp:Label></ItemTemplate>
                <HeaderStyle Width="200px"  HorizontalAlign="Center"/>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Address2" Visible="false">
             <ItemTemplate>
            <asp:Label ID="lblClientAddress2" runat="server" Text='<%# Eval("Addr2") %>'   Width="100px" ></asp:Label></ItemTemplate>
                <HeaderStyle Width="100px"  HorizontalAlign="Center"/>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="StateId" Visible="false">
             <ItemTemplate>
            <asp:Label ID="lblStateId" runat="server" Text='<%# Eval("StateId") %>' Width="55px"  ></asp:Label></ItemTemplate>
                <HeaderStyle Width="55px"  HorizontalAlign="Center"/>
            </asp:TemplateField>

               <asp:TemplateField HeaderText="City" Visible="true">
             <ItemTemplate>
            <asp:Label ID="lblCity" runat="server" Text='<%# Eval("City") %>' Width="55px" ></asp:Label></ItemTemplate>
                <HeaderStyle Width="55px"  HorizontalAlign="Center"/>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="State" Visible="true">
             <ItemTemplate>
            <asp:Label ID="lblStateName" runat="server" Text='<%# Eval("StateName") %>' Width="65px"  ></asp:Label></ItemTemplate>
                <HeaderStyle Width="65px" HorizontalAlign="Center" />
            </asp:TemplateField>
             <asp:TemplateField HeaderText="PinCode" Visible="true">
             <ItemTemplate>
            <asp:Label ID="lblPinCode" runat="server" Text='<%# Eval("PinCode") %>'  Width="40px" ></asp:Label></ItemTemplate>
                <HeaderStyle Width="40px"  HorizontalAlign="Center"/>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Is Active">
             <ItemTemplate>    <asp:CheckBox ID="chkActive" Width="30px" runat="server" Checked='<%# Eval("IsActive") %>'
             Enabled="false"></asp:CheckBox>         </ItemTemplate>
               <HeaderStyle Width="30px"  HorizontalAlign="Center"/>     <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            
     <%--       <asp:CommandField HeaderText="Select" ShowEditButton="True" Visible="true"   CausesValidation="False">
            <HeaderStyle Width="40px" />
            </asp:CommandField>--%>

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
    <asp:panel ID="pnlAdd" runat="server" Width="1062px" GroupingText="Add Client" CssClass="XXSmall"
            style="margin-top: 0px" Height="122px">
    <table style="width: 98%; height: 97px;" >
            <tr>
                     <td  text-align: left; height: 29px;">
                    <asp:Label ID="lblClientCode" runat="server" Text="Client Code"  
                         Font-Bold="True"></asp:Label>
                </td>
                <td style="height: 29px">
                    <asp:TextBox ID="txtClientCode" runat="server"  height="16px"></asp:TextBox>
                </td>

      
                <td  height: 29px;">
                    <asp:Label ID="lblClientName" runat="server" Text="Client Name"  
                         Font-Bold="True"></asp:Label>
                </td>
                <td style="height: 29px">
                    <asp:TextBox ID="txtClientName" runat="server"  height="16px" width="209px"></asp:TextBox>
                </td>
                <td style="text-align: left; height: 29px;">
                    <asp:Label ID="lblClientShortName" runat="server" Text="Cli Sh Name"  
                         Font-Bold="True"></asp:Label>
                </td>
                <td style="height: 29px">
                    <asp:TextBox ID="txtClientShortName" runat="server" MaxLength="8"
                      height="16px" width="95px"></asp:TextBox>
                </td>

             <td style=" text-align: left; height: 29px;">
                    <asp:Label ID="lblClientType" runat="server" Text="Client Type"  
                         Font-Bold="True"></asp:Label>
                </td>
                <td style="height: 29px">
                    <asp:DropDownList ID="ddlClientType"  runat="server"  Font-Size="XX-Small"
                    DataTextField="ClientType" DataValueField="ClientType"
                    Width="115px"  Height="16px"  >
                       
                 </asp:DropDownList>
                </td>

                <td style=" height: 29px;">
                    <asp:TextBox ID="txtClientId" runat="server" Visible="false"
                        height="16px" width="16px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                        <td style=" text-align: left; height: 30px;">
                    <asp:Label ID="lblBuildingName" runat="server" Text="Building Name"  
                         Font-Bold="True"></asp:Label>
                </td>
                 <td style="height: 30px">
                    <asp:TextBox ID="txtBuildingName" runat="server"  MaxLength="64"
                         height="16px" width="148px"></asp:TextBox>
                </td>
                <td style=" text-align: left; height: 30px;">
                    <asp:Label ID="lblAddr1" runat="server" Text="Addr1"  
                         Font-Bold="True"></asp:Label>
                </td>
                 <td style="height: 30px">
                    <asp:TextBox ID="txtAddr1" runat="server"  MaxLength="64"
                       Width="210px" height="16px"></asp:TextBox>
                </td>
                            <td style="width: 90px; text-align: left; height: 20px;">
                    <asp:Label ID="lblAddr2" runat="server" Text="Addr2"  
                         Font-Bold="True"></asp:Label>
                </td>
                 <td style="height: 20px">
                    <asp:TextBox ID="txtAddr2" runat="server"  MaxLength="64"    height="16px" 
                         width="113px"></asp:TextBox>
                </td>
                <td style=" text-align: left; height: 30px;">
                    <asp:Label ID="lblCity" runat="server" Text="City"  
                         Font-Bold="True"></asp:Label>
                </td>
                 <td style="height: 30px">
                    <asp:TextBox ID="txtCity" runat="server"  MaxLength="32"
                       height="16px" Width="98px"></asp:TextBox>
                </td>
       
 

                 </tr>



            <tr>
                           <td style=" text-align: left; height: 30px;">
                    <asp:Label ID="lblState" runat="server" Text="State"  
                         Font-Bold="True"></asp:Label>
                </td>
                  <td style=" height: 30px;">
                <asp:DropDownList ID="ddlState"  runat="server"  Font-Size="XX-Small"
                    DataTextField="StateName" DataValueField="StateId"       Width="125px"  AutoPostBack="true" 
                     OnSelectedIndexChanged="ddlStateChanged" Height="16px"  >
                 </asp:DropDownList>
                 </td>
                <td style="width: 82px; text-align: left; height: 20px;">
                    <asp:Label ID="lblPinCode" runat="server" Text="PinCode"  Font-Bold="True"></asp:Label>
                </td>
                 <td style="height: 20px">
                    <asp:TextBox ID="txtPinCode" runat="server"  MaxLength="6"   height="16px"></asp:TextBox>
                </td>
 

                
                <td style="width: 62px; height: 20px;">
                    <asp:CheckBox ID="chkIsActive" runat="server" Text="IsActive" Visible="False" />
                </td>
                <td style="height: 20px">
                    <asp:Button ID="btnSave" runat="server" Text="Save"  onclick="btnSave_Click"  />
                </td>
                <td  style="width: 72px; height: 20px;">
                    <asp:Button ID="btnClear" runat="server" Text="Clear"  onclick="btnClear_Click"  />
                </td>
                </tr>
               
        </table>
    </asp:panel>
    <%--</td></tr>
    </table>--%>
    <%--</asp:Panel>--%>
    </div>
</asp:Content>

