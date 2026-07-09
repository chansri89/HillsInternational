<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.Master" AutoEventWireup="true" CodeFile="TenderIOWMapping.aspx.cs" Inherits="TenderIOWMapping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <%-- <asp:Label ID = "lblSeparator" runat = "server" align = "center" Height="15px" Width = "901px" ></asp:Label>--%>
 <script type="text/javascript">
     function scrollToTargetRow() {
         var targetId = document.getElementById('<%= hdnTargetRowId.ClientID %>').value;
         if (targetId) {
             var rowElement = document.getElementById(targetId);
             var containerElement = document.getElementById('scrollContainer');
             if (rowElement && containerElement) {
                 // Scroll the container to the row's vertical position
                 containerElement.scrollTop = rowElement.offsetTop - containerElement.offsetTop;
                 // Optional: Highlight the row for visibility
                 rowElement.style.backgroundColor = '#ffffcc';
             }
         }
     }
</script>
 <style type="text/css">
    .WrapTextColumn {
        white-space: normal;
        word-break: break-all; /* Ensures long words without spaces wrap */
        /*width: 60px;  Set a fixed width */
    }
    </style>
  <asp:Label ID="lblIOWMaster" runat="server" Align= "center" Text="Tender IOW Mapping" CssClass="XSmall"
         width="787px" Font-Size ="12pt" Font-Bold="True"  style="text-align: center"></asp:Label>
   <%--<asp:Label ID = "lblSeparator1" runat = "server" align = "center" Height="15px" Width = "901px" ></asp:Label>--%>
 <%--   <div style="overflow:auto; height: 509px; width: 1246px; margin-right: 21px;">--%>
        <%--</td></tr>
    </table>--%>    <%--</asp:Panel>DataKeyNames="IOWCode"--%>
    
<asp:HiddenField ID="hdnTargetRowId" runat="server" Value="" />

       <asp:Panel ID="Panel3" runat="server" Height="55px" Width="1077px" 
        CssClass="XSmall">
       <table style="width: 1018px">
        <tr>
        <td class="style34" style="width: 392px">
    <asp:Panel ID="pnlPendind" runat="server" Height="50px" Width="786px" CssClass="XSmall"
            GroupingText="Selection" >
        <table style="height:33px; width: 743px;">
            <tr>
              <td style="width: 70px">
                <asp:Label ID="lblCompany" runat="server" Text="Company" Visible="true"
                  Font-Bold="true"  ></asp:Label>
           </td>
             <td style="width: 100px" >
                <asp:DropDownList ID="ddlCompany"  runat="server"  Visible="true"
                   DataTextField="CompanyName" DataValueField="CompanyId" Font-Size="X-Small"
                   Width="190px"  AutoPostBack="true" 
                     OnSelectedIndexChanged="ddlCompanyChanged" Height="18px"  >
                 </asp:DropDownList>
            </td>
             <td  style="width: 70px">
                <asp:Label ID="lblClients" runat="server" Text="Clients" Visible="true"
                  Font-Bold="true"  ></asp:Label>
           </td>
             <td style="width: 100px" >
                <asp:DropDownList ID="ddlClient"  runat="server"  Visible="true" AutoPostBack="true" 
                     OnSelectedIndexChanged="ddlClientChanged"  Font-Size="X-Small"
                    DataTextField="ClientName" DataValueField="ClientCode"
                    Width="158px"  Height="16px"  >
                 </asp:DropDownList>
            </td>

             <td style="width: 70px">
                <asp:Label ID="lblProject" runat="server" Text="Project" Visible="true"
                  Font-Bold="true"  ></asp:Label>
           </td>
             <td style="width: 100px">
                <asp:DropDownList ID="ddlProject"  runat="server"  Visible="true"
                     DataTextField="ProjectName" DataValueField="ClientProjectId" Font-Size="X-Small"
                    Width="206px"  Height="16px"  >
                 </asp:DropDownList>
            </td>
 
                <td  style="width: 62px" >
                         <asp:Button ID="btnGo" runat="server"
                               Height="21px" onclick="btnGo_Click"   Text="Go" Width="36px" />
                 </td>   
         
            </tr>
          
        </table>
    </asp:Panel>
    </td> <td>
       <%--<asp:Panel ID="Panel2" runat="server" Height="50px" Width="227px" CssClass="XSmall"
            GroupingText="Display Tender Row" >
        <table style="height:31px; width: 209px;">
            <tr>
                 <td>
                <asp:TextBox ID="txtTenderRowId" runat="server" Text="" Enabled="false"
                  Font-Bold="true"  Width="60px" ></asp:TextBox>
           </td> 
           <td style="width: 129px">  
                <asp:Button ID="btnGoToSelectRow" runat="server"
                     Height="21px" onclick="btnGoToSelectRow_Click"   Text="DispalyTenderRow" 
                    Width="122px" />
                </td>                
            </tr>
          
        </table>
    </asp:Panel>--%>
    </td>
    </tr>
    </table>
</asp:Panel>
    <asp:panel ID="Pnlgv" runat="server" Width="1240px" Height="460px" CssClass="XXSmall"
            ToolTip="">
        <div id="divIOWdtl" runat="server"  
            style="overflow:auto;  width:1230px; height:450px">
           <table style="width: 1220px; height: 440px;">
    <tr>
        <td style="width: 450px">
           <asp:panel ID="PnlTender" runat="server" Width="430px" Height="430px" 
                GroupingText="Tender Data"  >
                <table style="width: 425px">
                <tr>
                     <td class="style31" style="width: 33px">
                <asp:Label ID="lblFilter" runat="server" Text="XL Row #" Visible="true"
                  Font-Bold="true" ></asp:Label>
           </td>

            <td class="style31" style="width: 16px">
                <asp:TextBox ID="txtFilter" runat="server" Text="" Visible="true"
                  Font-Bold="true"  Width="40px" ></asp:TextBox>
           </td>
          
                 <td  style="width: 10px" >
                      <asp:CheckBox ID="chkQtyOnly" runat="server" Checked="false" Text="Only Qty"
                    Width="62px" Enabled="true"></asp:CheckBox>
                 </td> 
                    <td  >
                      <asp:CheckBox ID="chkNoMap" runat="server" Checked="false" Text="No Map"
                    Width="62px" Enabled="true"></asp:CheckBox>
                 </td>
                   <td  >
                         <asp:Button ID="btnFilter" runat="server" Font-Size="XX-Small"
                               Height="21px" onclick="btnFilter_Click"  Text="Filter" Width="32px" />
                 </td>  

                       <td style="width: 130px">
                <asp:Label ID="Label1" runat="server" Text=" Green - Mapping Done. " Visible="true" ForeColor="Green"
                  Font-Bold="true"   ></asp:Label>
              
                <asp:Label ID="Label2" runat="server" Width = "184px" 
                               Text="Light Blue - Mapping to be done" ForeColor="Blue"
                  Font-Bold="True"   ></asp:Label>
                 
                 </td>

                 
                </tr>
                </table>
             <div id="div2" runat="server" class="WrapTextColumn"
                    style="overflow:auto; width: 420px; height:410px; " >
           
                <asp:GridView ID="GrdTender" runat="server" CellPadding="3" 
                    Width="410px"  AutoGenerateColumns="False" 
                    Height="16px" GridLines="Vertical" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
                    OnRowDataBound="GridTender_Databound">
                <EditRowStyle Font-Size="XX-Small" />  <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                <RowStyle BackColor="#EEEEEE" ForeColor="Black" />     <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" 
                    HorizontalAlign="Left" />      <AlternatingRowStyle BackColor="#DCDCDC" />

                <Columns>
                    <asp:TemplateField HeaderText="CompanyId"  Visible="false">
                    <ItemTemplate>
                    <asp:Label ID="lblCompanyId" runat="server" Text='<%# Eval("CompanyId") %>'  ></asp:Label></ItemTemplate>
                     <HeaderStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Client Project TenderId" Visible="false">
                    <ItemTemplate>
                    <asp:Label ID="lblClientTenderId" runat="server" Text='<%# Eval("ClientProjectTenderId") %>'  ></asp:Label></ItemTemplate>
                      <HeaderStyle Width="10px"/><ItemStyle Width = "10px" />
                       </asp:TemplateField>
                     <asp:TemplateField HeaderText="Tender IOW Mapped"  Visible="false">
                    <ItemTemplate>
                    <asp:Label ID="lblTenderIOWMapped" runat="server" Text='<%# Eval("TenderIOWMapped") %>'  ></asp:Label></ItemTemplate>
                     <HeaderStyle Width="10px" />
                    </asp:TemplateField>

                       <asp:TemplateField HeaderText="XL Sheet" Visible="true">
                    <ItemTemplate>
                    <asp:Label ID="lblExcelSheetName" runat="server" Text='<%# Eval("ExcelSheetName") %>' Width="40px" ></asp:Label></ItemTemplate>
                      <HeaderStyle Width="40px"/><ItemStyle Width = "40px" />
                       </asp:TemplateField>

                    <asp:TemplateField HeaderText="XL #" Visible="true">
                    <ItemTemplate>
                    <asp:Label ID="lblExcelRowNo" runat="server" Text='<%# Eval("ExcelRowNumber") %>'  ></asp:Label></ItemTemplate>
                      <HeaderStyle Width="20px"/><ItemStyle Width = "20px" />
                       </asp:TemplateField>
                  <asp:TemplateField HeaderText="Srlno" Visible="true">
                     <ItemTemplate>
                    <asp:Label ID="lblSrlNo" runat="server" Text='<%# Eval("Srlno") %>'   Width="40px" ></asp:Label></ItemTemplate>
                        <HeaderStyle Width="40px" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Description" Visible="true">
                     <ItemTemplate>
                    <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>'   Width="150px" ></asp:Label></ItemTemplate>
                        <HeaderStyle Width="150px" />
                    </asp:TemplateField>
                  <asp:TemplateField HeaderText="UOM" Visible="true">
                     <ItemTemplate>
                    <asp:Label ID="lblUOM" runat="server" Text='<%# Eval("UOM") %>'   Width="35px" ></asp:Label></ItemTemplate>
                        <HeaderStyle Width="35px" />
                    </asp:TemplateField>

                   <asp:TemplateField HeaderText="Qty" Visible="true">
                     <ItemTemplate>
                    <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("Quantity") %>'   Width="30px" ></asp:Label></ItemTemplate>
                        <HeaderStyle Width="30px" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Sel ect">
                     <ItemTemplate>
                      <asp:CheckBox ID="chkSelectBox" runat="server" Checked="false"
                     Width="30px" Enabled="true"  AutoPostBack="True" 
                              OnCheckedChanged="chkSelect_CheckedChanged"></asp:CheckBox>
                     </ItemTemplate>         <HeaderStyle Width="30px" />       <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

          <%--
                <asp:TemplateField HeaderText="Click IowItem" Visible="true" >
                    <ItemTemplate>  
                    <asp:LinkButton ID="lnkIOWItemName" Width="60px" runat ="server" CommandArgument='<%#Eval("IOWCode")%>'
                     CommandName ="IOWItem" Text ="IOW Item"></asp:LinkButton>
                      </ItemTemplate> <HeaderStyle Width="60px" />
                    </asp:TemplateField>--%>
            
                </Columns>
                <sortedascendingcellstyle backcolor="#F1F1F1" />
                <sortedascendingheaderstyle backcolor="#0000A9" />
                <sorteddescendingcellstyle backcolor="#CAC9C9" />
                <sorteddescendingheaderstyle backcolor="#000065" />
            </asp:GridView>
            </div>
            <input type="hidden" id="hdnScrollTop" runat="server" value="0" />
           </asp:panel>
        </td>
        <td>
           <asp:panel ID="Panel1" runat="server" Width="410px" Height="430px"   GroupingText="IOW Data" 
                style="margin-left: 0px" >
            <table> 
              <tr>
             <td>
                <asp:TextBox ID="txtClientProjectTenderId" runat="server" Text="" Visible="false"
                  Font-Bold="true"   Width="22px" Height="16px" ></asp:TextBox>
           </td>

                    <td  style="width: 85px">
                <asp:Label ID="lblTenderSrlNo" runat="server" Text="" Visible="true"  ForeColor="DarkBlue"
                  Font-Bold="true"  ></asp:Label>
           </td>

            <td style="width: 70px">
                <asp:Label ID="lblTenderDesc" runat="server" ForeColor="DarkBlue" Visible="False"
                  Font-Bold="True"   Width="216px"      Height="16px" ></asp:Label>
           </td>
            <td  >
                         <asp:Button ID="btnShowTender" runat="server"  BackColor="Aqua" Font-Size="X-Small"
                               Height="20px" onclick="btnShowTender_Click"  
                               Text="Tender" Width="49px" />
                 </td>   
                </tr>
             </table>
             <table style="width: 400px">
                <tr>

                  <td>
                             <asp:Label ID="lblGroup" runat="server" Text="Group" Visible="true"
                            Font-Bold="true"   ></asp:Label>
                        </td>
                  <td>
                        <asp:DropDownList ID="ddlGroup"  runat="server"  Visible="true" AutoPostBack="true" 
                             OnSelectedIndexChanged="ddlGroupChanged" Font-Size="XX-Small"
                             DataTextField="GroupName" DataValueField="GroupCode"
                             Width="61px"  Height="16px"  >
                         </asp:DropDownList>
                    </td>
                  <td>
                             <asp:Label ID="lblSubGroup" runat="server" Text="Sub Group" Visible="true"
                            Font-Bold="true"   ></asp:Label>
                   </td>
                  <td >
                        <asp:DropDownList ID="ddlSubGroup"  runat="server"  Visible="true" 
                             DataTextField="SubGroupName" DataValueField="SubGroupCode" Font-Size="XX-Small"
                             Width="63px"  Height="20px"  >
                         </asp:DropDownList>
                    </td>
                    <td >
                <asp:Label ID="lblIOWFilter" runat="server" Text="IOW Filter" Visible="true"
                  Font-Bold="true"  ></asp:Label>
           </td>

            <td>
                <asp:TextBox ID="txtIOWFilter" runat="server" Text="" Visible="true" MaxLength="32"
                  Font-Bold="true"   Width="88px" ></asp:TextBox>
           </td>
          
                      <td  >
                         <asp:Button ID="btnSelect" runat="server" Font-Size="X-Small"
                               Height="21px" onclick="btnSelect_Click"  Text="Get IOW" Width="58px" />
                 </td>  
                              
                </tr>

             </table> 
 
                          <div id="div1" runat="server" 
                   style="overflow:auto;  width:400px; height:392px;">
                <asp:GridView ID="grdIow" runat="server" CellPadding="3" 
                    Width="383px"  AutoGenerateColumns="False" 
                    Height="16px" GridLines="Vertical" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
                     OnRowCommand="GrdIOW_RowCommand" >
                <EditRowStyle Font-Size="XX-Small" />  <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                <RowStyle BackColor="#EEEEEE" ForeColor="Black" />     <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" 
                    HorizontalAlign="Left" />      <AlternatingRowStyle BackColor="#DCDCDC" />

                <Columns>
                    <asp:TemplateField HeaderText="CompanyId"  Visible="false">
                    <ItemTemplate>
                    <asp:Label ID="lblCompanyId" runat="server" Text='<%# Eval("CompanyId") %>'  ></asp:Label></ItemTemplate>
                     <HeaderStyle Width="10px" />
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Tender IOW Mapped"  Visible="false">
                    <ItemTemplate>
                    <asp:Label ID="lblTenderIOWMapped" runat="server" Text='<%# Eval("TenderIOWMapped") %>'  ></asp:Label></ItemTemplate>
                     <HeaderStyle Width="10px" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Group Code" Visible="false">
                    <ItemTemplate>
                    <asp:Label ID="lblGroupCode" runat="server" Text='<%# Eval("GroupCode") %>'  ></asp:Label></ItemTemplate>
                      <HeaderStyle Width="30px"/><ItemStyle Width = "30px" />
                       </asp:TemplateField>
                    <asp:TemplateField HeaderText="SubGroup Code" Visible="false">
                    <ItemTemplate>
                    <asp:Label ID="lblSubGroupCode" runat="server" Text='<%# Eval("SubGroupCode") %>'  ></asp:Label></ItemTemplate>
                      <HeaderStyle Width="30px"/><ItemStyle Width = "30px" />
                       </asp:TemplateField>


                    <asp:TemplateField HeaderText="CRAMIOWHeadDtlId"  Visible="false">
                    <ItemTemplate>
                    <asp:Label ID="lblCRAMIOWHeadDtlId" runat="server" Text='<%# Eval("CRAMIOWHeadDtlId") %>'  ></asp:Label></ItemTemplate>
                     <HeaderStyle Width="10px" />
                    </asp:TemplateField>
                <asp:TemplateField HeaderText="IOW Code" Visible="true" >
                    <ItemTemplate>  
                    <asp:LinkButton ID="lnkIOWCode" Width="60px" runat ="server" CommandArgument='<%#Eval("IOWCode")%>'
                     CommandName ="IOWCode" Text ='<%#Eval("IOWCode") %>'></asp:LinkButton>
                      </ItemTemplate> <HeaderStyle Width="60px" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="IOW Code" Visible="false">
                    <ItemTemplate>
                    <asp:Label ID="lblIOWCode" runat="server" Text='<%# Eval("IOWCode") %>'  ></asp:Label></ItemTemplate>
                      <HeaderStyle Width="30px"/><ItemStyle Width = "30px" />
                       </asp:TemplateField>
                    <asp:TemplateField HeaderText="IOWName" Visible="true">
                     <ItemTemplate>
                    <asp:Label ID="lblIOwName" runat="server" Text='<%# Eval("IOWDescription") %>'   Width="170px" ></asp:Label></ItemTemplate>
                        <HeaderStyle Width="17px" />
                    </asp:TemplateField>
                 <asp:TemplateField HeaderText="IOW UOM" Visible="True">
                    <ItemTemplate>
                    <asp:Label ID="lblIOWUOM" runat="server" Text='<%# Eval("IOWUOM") %>'  ></asp:Label></ItemTemplate>
                      <HeaderStyle Width="30px"/><ItemStyle Width = "30px" />
                       </asp:TemplateField>
                    <asp:TemplateField HeaderText="Temp IOW" Visible="true">
                     <ItemTemplate>
                    <asp:CheckBox ID="lblTempIOW" runat="server" Checked='<%# Eval("IsTemproryIOW") %>' Enabled="false"  Width="20px" >
                    </asp:CheckBox></ItemTemplate>          <HeaderStyle Width="20px" />
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Select" Visible="false">
                     <ItemTemplate>
                      <asp:CheckBox ID="chkSelectBox" runat="server" Checked="false" Enabled="false"  
                     Width="30px"></asp:CheckBox>
                     </ItemTemplate>         <HeaderStyle Width="30px" />       <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

          
                <asp:TemplateField HeaderText="Select Iow" Visible="true" >
                    <ItemTemplate>  
                    <asp:LinkButton ID="lnkIOWItem" Width="40px" runat ="server" CommandArgument='<%#Eval("IOWCode")%>'
                     CommandName ="IOWItemSel" Text ="Select"></asp:LinkButton>
                      </ItemTemplate> <HeaderStyle Width="40px" />
                    </asp:TemplateField>
            
                </Columns>
                <sortedascendingcellstyle backcolor="#F1F1F1" />
                <sortedascendingheaderstyle backcolor="#0000A9" />
                <sorteddescendingcellstyle backcolor="#CAC9C9" />
                <sorteddescendingheaderstyle backcolor="#000065" />
            </asp:GridView>
            </div>
           </asp:panel>
        </td>
        <td>
                      <asp:panel ID="pnlIOWSelected" runat="server" Width="375px" Height="430px"  
                        GroupingText="IOW Selected Data"  ForeColor="DarkGreen" >
                        <table>
                        <tr>
                         <td class="style28" style="width: 241px">
                            <asp:Label ID="Label3" runat="server" Text="" Visible="true"
                              Font-Bold="true"   ></asp:Label>
                       </td>
                         <td class="style28" style="width: 41px" >
                         <asp:Button ID="btnIOWSave" runat="server"  BackColor="Aqua" Font-Size="X-Small"
                               Height="21px" onclick="btnIOWSave_Click"    Text="Save" Width="37px" />
                 </td>     
                        </tr>
                        </table>
               <div id="div4" runat="server" 
                   style="overflow:auto; width:365px; height:400px; ">
                <asp:GridView ID="grdIOWSelected" runat="server" CellPadding="3" 
                    Width="346px"  AutoGenerateColumns="False" 
                    Height="16px" GridLines="Vertical" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
                    OnRowCommand = "GrdIOWSelected_RowCommand" >
                <EditRowStyle Font-Size="XX-Small" />  <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                <RowStyle BackColor="#EEEEEE" ForeColor="Black" />     <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" 
                    HorizontalAlign="Left" />      <AlternatingRowStyle BackColor="#DCDCDC" />

                <Columns>
                    <asp:TemplateField HeaderText="CompanyId"  Visible="false">
                    <ItemTemplate>
                    <asp:Label ID="lblCompanyId" runat="server" Text='<%# Eval("CompanyId") %>'  ></asp:Label></ItemTemplate>
                     <HeaderStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Group Code" Visible="false">
                    <ItemTemplate>
                    <asp:Label ID="lblGroupCode" runat="server" Text='<%# Eval("GroupCode") %>'  ></asp:Label></ItemTemplate>
                      <HeaderStyle Width="30px"/><ItemStyle Width = "30px" />
                       </asp:TemplateField>
                    <asp:TemplateField HeaderText="SubGroup Code" Visible="false">
                    <ItemTemplate>
                    <asp:Label ID="lblSubGroupCode" runat="server" Text='<%# Eval("SubGroupCode") %>'  ></asp:Label></ItemTemplate>
                      <HeaderStyle Width="30px"/><ItemStyle Width = "30px" />
                       </asp:TemplateField>


                     <asp:TemplateField HeaderText="Tender IOW Mapped"  Visible="false">
                    <ItemTemplate>
                    <asp:Label ID="lblTenderIOWMapped" runat="server" Text='<%# Eval("TenderIOWMapped") %>'  ></asp:Label></ItemTemplate>
                     <HeaderStyle Width="10px" />
                    </asp:TemplateField>
                  

                    <asp:TemplateField HeaderText="CRAMIOWHeadDtlId"  Visible="false">
                    <ItemTemplate>
                    <asp:Label ID="lblCRAMIOWHeadDtlId" runat="server" Text='<%# Eval("CRAMIOWHeadDtlId") %>'  ></asp:Label></ItemTemplate>
                     <HeaderStyle Width="10px" />
                    </asp:TemplateField>
 
                    <asp:TemplateField HeaderText="IOW Code" Visible="true">
                    <ItemTemplate>
                    <asp:Label ID="lblIOWCode" runat="server" Text='<%# Eval("IOWCode") %>'  ></asp:Label></ItemTemplate>
                      <HeaderStyle Width="40px"/><ItemStyle Width = "40px" />
                       </asp:TemplateField>
                    <asp:TemplateField HeaderText="IOWName" Visible="true">
                     <ItemTemplate>
                    <asp:Label ID="lblIOwName" runat="server" Text='<%# Eval("IOWDescription") %>'   Width="180px" ></asp:Label></ItemTemplate>
                        <HeaderStyle Width="180px" />
                    </asp:TemplateField>
                 <asp:TemplateField HeaderText="IOW UOM" Visible="True">
                    <ItemTemplate>
                    <asp:Label ID="lblIOWUOM" runat="server" Text='<%# Eval("IOWUOM") %>'  ></asp:Label></ItemTemplate>
                      <HeaderStyle Width="30px"/><ItemStyle Width = "30px" />
                       </asp:TemplateField>
                    <asp:TemplateField HeaderText="Temp IOW" Visible="false">
                     <ItemTemplate>
                    <asp:CheckBox ID="lblTempIOW" runat="server" Checked='<%# Eval("IsTemproryIOW") %>' Enabled="false"  Width="20px" >
                    </asp:CheckBox></ItemTemplate>          <HeaderStyle Width="20px" />
                    </asp:TemplateField>

                <asp:TemplateField HeaderText="Drop IOW" Visible="true" >
                    <ItemTemplate>  
                    <asp:LinkButton ID="lnkIOWselectedCode" Width="30px" runat ="server" CommandArgument='<%#Eval("IOWCode")%>'
                     CommandName ="Drop" Text ="Drop"></asp:LinkButton>
                      </ItemTemplate> <HeaderStyle Width="30px" />
                    </asp:TemplateField>

          <%--
                <asp:TemplateField HeaderText="Click IowItem" Visible="true" >
                    <ItemTemplate>  
                    <asp:LinkButton ID="lnkIOWItemName" Width="60px" runat ="server" CommandArgument='<%#Eval("IOWCode")%>'
                     CommandName ="IOWItem" Text ="IOW Item"></asp:LinkButton>
                      </ItemTemplate> <HeaderStyle Width="60px" />
                    </asp:TemplateField>--%>
            
                </Columns>
                <sortedascendingcellstyle backcolor="#F1F1F1" />
                <sortedascendingheaderstyle backcolor="#0000A9" />
                <sorteddescendingcellstyle backcolor="#CAC9C9" />
                <sorteddescendingheaderstyle backcolor="#000065" />
            </asp:GridView>
            </div>
           </asp:panel>
        </td>

    </tr>
</table>
    </div>

    </asp:panel>
  <%--  </div>--%>
   <asp:panel ID="PnlIOWdtl" runat="server" Width="1196px" Height="255px" CssClass="XXSmall"
            ToolTip="">
        <div id="div3" runat="server"  
            style="overflow:auto; height:240px; width:1190px">
            <table>
                <tr>
                <td style="width:900px"></td>
                    <td>
                        <asp:Button ID="btnBack" runat="server"
                               Height="20px" onclick="btnBack_Click"   Text="Back" Width="50px" />
                    </td>
                </tr>
            </table>
                      <asp:GridView ID="GrdIOWDtl" runat="server" CellPadding="3" 
                    Width="1170px"  AutoGenerateColumns="False" 
                    Height="28px" GridLines="Vertical" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
                    >
                <EditRowStyle Font-Size="XX-Small" />  <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                <RowStyle BackColor="#EEEEEE" ForeColor="Black" />     <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" 
                    HorizontalAlign="Left" />      <AlternatingRowStyle BackColor="#DCDCDC" />

                <Columns>
                    <asp:TemplateField HeaderText="CompanyId"  Visible="false">
                    <ItemTemplate>
                    <asp:Label ID="lblCompanyId" runat="server" Text='<%# Eval("CompanyId") %>'  ></asp:Label></ItemTemplate>
                     <HeaderStyle Width="10px" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="GroupName"  Visible="true">
                    <ItemTemplate>
                    <asp:Label ID="lblGroupName" runat="server" Text='<%# Eval("GroupName") %>'  ></asp:Label></ItemTemplate>
                     <HeaderStyle Width="30px" /> <ItemStyle Width="30px" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="SubGroup Name"  Visible="true">
                    <ItemTemplate>
                    <asp:Label ID="lblSubGroupName" runat="server" Text='<%# Eval("SubGroupName") %>'  ></asp:Label></ItemTemplate>
                     <HeaderStyle Width="30px" /> <ItemStyle Width="30px" />
                    </asp:TemplateField>


                   <asp:TemplateField HeaderText="IOW Code" Visible="true">
                    <ItemTemplate>
                    <asp:Label ID="lblIOWCode" runat="server" Text='<%# Eval("IOWCode") %>'  ></asp:Label></ItemTemplate>
                      <HeaderStyle Width="30px"/><ItemStyle Width = "30px" />
                       </asp:TemplateField>
                    <asp:TemplateField HeaderText="IOWName" Visible="true">
                     <ItemTemplate>
                    <asp:Label ID="lblIOwName" runat="server" Text='<%# Eval("IOWDescription") %>'   Width="180px" ></asp:Label></ItemTemplate>
                        <HeaderStyle Width="180px" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Temp IOW" Visible="true">
                     <ItemTemplate>
                    <asp:CheckBox ID="lblTempIOW" runat="server" Checked='<%# Eval("IsTemproryIOW") %>' Enabled="false"  Width="20px" >
                    </asp:CheckBox></ItemTemplate>          <HeaderStyle Width="20px" />
                    </asp:TemplateField>


<%--                    <asp:TemplateField HeaderText="L1Code" Visible="true">
                    <ItemTemplate>
                    <asp:Label ID="lblL1Code" runat="server" Text='<%# Eval("L1Code") %>'  ></asp:Label></ItemTemplate>
                      <HeaderStyle Width="30px"/><ItemStyle Width = "30px" />
                       </asp:TemplateField>--%>

                    <asp:TemplateField HeaderText="L1 Description" Visible="true">
                     <ItemTemplate>
                    <asp:Label ID="lblL1Desc" runat="server" Text='<%# Eval("L1Desc") %>'   Width="180px" ></asp:Label></ItemTemplate>
                        <HeaderStyle Width="180px" />
                    </asp:TemplateField>

 <%--                   <asp:TemplateField HeaderText="L2Code" Visible="true">
                    <ItemTemplate>
                    <asp:Label ID="lblL2Code" runat="server" Text='<%# Eval("L2Code") %>'  ></asp:Label></ItemTemplate>
                      <HeaderStyle Width="30px"/><ItemStyle Width = "30px" />
                       </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="L2 Description" Visible="true">
                     <ItemTemplate>
                    <asp:Label ID="lblL2Desc" runat="server" Text='<%# Eval("L2Desc") %>'   Width="180px" ></asp:Label></ItemTemplate>
                        <HeaderStyle Width="180px" />
                    </asp:TemplateField>

 <%--                   <asp:TemplateField HeaderText="L3Code" Visible="true">
                    <ItemTemplate>
                    <asp:Label ID="lblL3Code" runat="server" Text='<%# Eval("L3Code") %>'  ></asp:Label></ItemTemplate>
                      <HeaderStyle Width="30px"/><ItemStyle Width = "30px" />
                       </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="L3 Description" Visible="true">
                     <ItemTemplate>
                    <asp:Label ID="lblL3Desc" runat="server" Text='<%# Eval("L3Desc") %>'   Width="180px" ></asp:Label></ItemTemplate>
                        <HeaderStyle Width="180px" />
                    </asp:TemplateField>

 <%--                   <asp:TemplateField HeaderText="L4Code" Visible="true">
                    <ItemTemplate>
                    <asp:Label ID="lblL4Code" runat="server" Text='<%# Eval("L4Code") %>'  ></asp:Label></ItemTemplate>
                      <HeaderStyle Width="30px"/><ItemStyle Width = "30px" />
                       </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="L4 Description" Visible="true">
                     <ItemTemplate>
                    <asp:Label ID="lblL4Desc" runat="server" Text='<%# Eval("L4Desc") %>'   Width="180px" ></asp:Label></ItemTemplate>
                        <HeaderStyle Width="180px" />
                    </asp:TemplateField>

 
<%--
                     <asp:TemplateField HeaderText="Select">
                     <ItemTemplate>
                      <asp:CheckBox ID="chkSelectBox" runat="server" Checked="false"
                     Width="30px" Enabled="true"></asp:CheckBox>
                     </ItemTemplate>         <HeaderStyle Width="30px" />       <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>--%>

          <%--
                <asp:TemplateField HeaderText="Click IowItem" Visible="true" >
                    <ItemTemplate>  
                    <asp:LinkButton ID="lnkIOWItemName" Width="60px" runat ="server" CommandArgument='<%#Eval("IOWCode")%>'
                     CommandName ="IOWItem" Text ="IOW Item"></asp:LinkButton>
                      </ItemTemplate> <HeaderStyle Width="60px" />
                    </asp:TemplateField>--%>
            
                </Columns>
                <sortedascendingcellstyle backcolor="#F1F1F1" />
                <sortedascendingheaderstyle backcolor="#0000A9" />
                <sorteddescendingcellstyle backcolor="#CAC9C9" />
                <sorteddescendingheaderstyle backcolor="#000065" />
            </asp:GridView>     
    </div>
    </asp:panel>
 

</asp:Content>

