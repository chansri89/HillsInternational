<%@ page title="" language="C#" masterpagefile="~/MasterPage1.Master" autoeventwireup="true" inherits="CRAMIOWHeadDtlContextMaster, App_Web_sx5sst5a" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID="lblIOWHeadMaster" runat="server" Align= "center" Text="CRAM IOW Head Detail Context Master" 
         width="787px"   Font-Size ="12pt" Font-Bold="True"   style="text-align: center"></asp:Label>

    <asp:Panel ID="pnlPendind" runat="server" Height="28px" Width="1012px" 
        CssClass="XSmall">
        <table style="height: 22px; width: 989px;">
            <tr>
                      <td  style="width: 70px">
                <asp:Label ID="lblCompany" runat="server" Text="Company" Visible="true"            Font-Bold="true"   ></asp:Label>
           </td>
             <td style="width: 100px">
                <asp:DropDownList ID="ddlCompany"  runat="server"  Font-Size="XX-Small"
                    DataTextField="CompanyName" DataValueField="CompanyId"   Width="248px"  AutoPostBack="true" 
                     OnSelectedIndexChanged="ddlCompany_Changed" Height="16px"  >
                 </asp:DropDownList>
            </td>
                  <td style="width: 47px; text-align: left; height: 26px;">
                    <asp:Label ID="lblGroup" runat="server" Text="Group"     Font-Bold="True"></asp:Label>
                </td>
                    <td style="height: 26px">
                    <asp:DropDownList ID="ddlGroup"  runat="server"   Font-Size="XX-Small"
                    DataTextField="GroupName" DataValueField="GroupCode"  Width="102px"  Height="16px"  AutoPostBack="true" 
                     OnSelectedIndexChanged="ddlGroup_Changed" >
                 </asp:DropDownList>
                </td>
                            <td style="width: 71px; text-align: left; height: 23px;">
                                <asp:Label ID="lblSubGroup" runat="server" Font-Bold="True"   Text="Sub Group"></asp:Label>
                            </td>
                            <td style="height: 23px">
                                <asp:DropDownList ID="ddlsubGroup" runat="server" DataTextField="SubGroupName"  Font-Size="XX-Small"
                                    DataValueField="SubGroupCode"  Height="16px"   Visible="true" Width="102px">
                                </asp:DropDownList>
                            </td>

                <td style="width: 36px">
                <asp:Label ID="lblFilter" runat="server" Text="Filter" Visible="true"   Font-Bold="true"  ></asp:Label>
           </td>

            <td  style="width: 70px">
                <asp:TextBox ID="txtFilter" runat="server" Text="" Visible="true"    Font-Bold="true"  ></asp:TextBox>
           </td>
                <td  style="width: 62px" >
                         <asp:Button ID="btnFilter" runat="server"  Height="21px" onclick="btnFilter_Click"   Text="Filter" Width="43px" />
                 </td>      
                             
            </tr>
               
        </table>
    </asp:Panel>

    <asp:panel ID="Pnlgv" runat="server" Width="1216px" Height="436px" CssClass="XXSmall"
            GroupingText="CRAM IOW Head Grid" 
            >
        <table>
        </table>
        <div id="divIOW" runat="server"       style="overflow:auto; height:415px; width:99%">

    <asp:GridView ID="GrdIOWHeadMaster" runat="server" CellPadding="3" ToolTip="Click on IOW Name row to create IOW data"
            Width="98%"  AutoGenerateColumns="False" Height="16px" GridLines="Vertical" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
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
 
         <asp:TemplateField HeaderText="Click IOW Head Name to Select" Visible="true" >
            <ItemTemplate>  
            <asp:LinkButton ID="lnkIOWHeadName" Width="680px" runat ="server" CommandArgument='<%#Eval("IOWHeadCode")%>'
             CommandName ="SelIOW" Text ='<%#Eval("IOWHeadName") %>'></asp:LinkButton>
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
        <asp:panel ID="pnlIOW" runat="server" Width="1221px" Height="285px" CssClass="XXSmall"
            GroupingText="CRAM IOW Head Dtl Grid"        >
        <table>
        <tr>
                 <td style="width: 37px" >
                    <asp:Label ID="Label9" runat="server" Text="IOW Head Code"  
                         Font-Bold="True"></asp:Label>
                </td>
                <td>
                      <asp:TextBox ID="txtCRAMIOWHeadCode" runat="server" 
                         ReadOnly="true"   height="25px" Width="67px"></asp:TextBox>
                </td>
                     <td style="width: 71px" >
                    <asp:Label ID="Label5" runat="server" Text="IOW Head Name"  
                         Font-Bold="True"></asp:Label>
                </td>
  
                <td style="height: 46px; width: 865px;">
                    <asp:TextBox ID="txtCRAMIOWHeadName" runat="server"  TextMode="MultiLine" ReadOnly="true"  height="31px" width="985px"></asp:TextBox>
                </td>
        </tr>
        </table>
        <div id="div2" runat="server"  
            style="overflow:auto; height:212px; width:99%">

    <asp:GridView ID="grdIOWdtl" runat="server" CellPadding="3" ToolTip="Click on IOW dtl Name row to Modify IOW "
            Width="99%"  AutoGenerateColumns="False"    Height="16px" GridLines="Vertical" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
            OnRowCommand="GrdIOWDtl_RowCommand">

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
            <asp:TemplateField HeaderText="Dtl Id"  Visible="false">
            <ItemTemplate>
            <asp:Label ID="lblCRAMIOWHeadDtlId" runat="server" Text='<%# Eval("CRAMIOWHeadDtlId") %>'  ></asp:Label></ItemTemplate>
             <HeaderStyle Width="10px"  HorizontalAlign="Center" />
            </asp:TemplateField>
          <asp:TemplateField HeaderText="IOW Head Code" Visible="true">
            <ItemTemplate>
            <asp:Label ID="lblPreviousIOWLevel" runat="server" Text='<%# Eval("PreviousIOWLevel") %>'  ></asp:Label></ItemTemplate>
              <HeaderStyle Width="30px"/><ItemStyle Width = "30px" />
               </asp:TemplateField>
            <asp:TemplateField HeaderText="IOW Code" Visible="true">
            <ItemTemplate>
            <asp:Label ID="lblIOWCode" runat="server" Text='<%# Eval("IOWCode") %>'  ></asp:Label></ItemTemplate>
              <HeaderStyle Width="30px"/><ItemStyle Width = "30px" />
               </asp:TemplateField>
            <asp:TemplateField HeaderText="IOW dtl Name" Visible="false">
            <ItemTemplate>
            <asp:Label ID="lblIOWDescription" runat="server" Text='<%# Eval("IOWDescription") %>'  ></asp:Label></ItemTemplate>
              <HeaderStyle Width="50px" HorizontalAlign="Center" /><ItemStyle Width = "50px" />
               </asp:TemplateField>
 
         <asp:TemplateField HeaderText="Click IOW dtl Name for Context entry" Visible="true" >
            <ItemTemplate>  
            <asp:LinkButton ID="lnkIOWDescription" Width="680px" runat ="server" CommandArgument='<%#Eval("IOWCode")%>'
             CommandName ="IOW" Text ='<%#Eval("IOWDescription") %>'></asp:LinkButton>
              </ItemTemplate> <HeaderStyle Width="680px"  HorizontalAlign="Center" />
            </asp:TemplateField>
           <asp:TemplateField HeaderText="IOW UOM" Visible="true">
            <ItemTemplate>
            <asp:Label ID="lblIOWUOM" runat="server" Text='<%# Eval("IOWUOM") %>' Width="50px" ></asp:Label></ItemTemplate>
              <HeaderStyle Width="50px"  HorizontalAlign="Center" />
            </asp:TemplateField>
           <asp:TemplateField HeaderText="IOW Qty" Visible="true">
            <ItemTemplate>
            <asp:Label ID="lblIOWQty" runat="server" Text='<%# Eval("IOWQuantity") %>' Width="50px" ></asp:Label></ItemTemplate>
              <HeaderStyle Width="50px"  HorizontalAlign="Center" />
            </asp:TemplateField>
              <asp:TemplateField HeaderText="Is Temprory IOW">
             <ItemTemplate>
              <asp:CheckBox ID="chkIsTemproryIOW" runat="server" Checked='<%# Eval("IsTemproryIOW") %>'
             Width="30px" Enabled="false"></asp:CheckBox>
             </ItemTemplate>
                <HeaderStyle Width="30px" />
                <ItemStyle HorizontalAlign="Center" />
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
    

         <asp:panel ID="PnlContext" runat="server" Width="1237px" Height="192px" CssClass="XXSmall"
            GroupingText="IOW Context Item grid">

            <table style="height: 73px; width: 1214px;">
                <tr>
                   <td style="width: 35px; height: 40px;" >
                    <asp:Label ID="Label4" runat="server" Text="IOW Head Code"   Font-Bold="True"></asp:Label>
                </td>
                     <td style="height: 35px">
                      <asp:TextBox ID="txtIOWHeadCode" runat="server"   ReadOnly="true"  height="25px" Width="64px"></asp:TextBox>
                </td>
                    <td style="width: 71px; height: 35px;" >
                    <asp:Label ID="Label10" runat="server" Text="IOW Head Name"  
                         Font-Bold="True"></asp:Label>
                </td>
  
                    <td style="height: 35px; width: 865px;">
                    <asp:TextBox ID="txtIOWHeadName" runat="server"      TextMode="MultiLine" ReadOnly="true"
                       height="30px" width="1056px"></asp:TextBox>
                </td>
 
 
        </tr>
            <tr>
               <td style="width: 35px; text-align: left; height: 35px;">
                    <asp:Label ID="Label2" runat="server" Text="IOW Code"           Font-Bold="True"></asp:Label>
                </td>
                    <td style="height: 35px">
                    <asp:TextBox ID="txtIOWCode" runat="server"  ReadOnly="true"  height="24px" Width="64px"></asp:TextBox>
                </td>
                <td class="style28" style="width: 35px; height: 35px;">
                    <asp:Label ID="lblIOWNm" runat="server" Text="IOW Name"  Font-Bold="True"></asp:Label>
                </td>
                     <td style="height: 35px; width: 865px;">
                    <asp:TextBox ID="txtIOWDescription" runat="server" TextMode="MultiLine" ReadOnly="true"
                        height="30px" width="1050px"></asp:TextBox>
                </td>
 
            </tr>
            </table>
            <table style="height: 41px; width: 1214px; margin-bottom: 0px;">
                <tr>
                    <td style="height: 26px" >
                    <asp:Label ID="lblContext" runat="server" Text="Con text"  height = "30px"
                         Font-Bold="True" Width="30px"></asp:Label>
                </td>
                     <td style="width: 768px; height: 26px;">
                    <asp:TextBox ID="txtContext" runat="server"  MaxLength="6000" TextMode="MultiLine"
                        height="28px" width="1022px"></asp:TextBox>
                </td>
                <td style="width: 48px; text-align: left; height: 26px;">
                    <asp:Label ID="Label11" runat="server" Text="Sr. No"  
                         Font-Bold="True"></asp:Label>
                </td>
                <td style="height: 26px">
                    <asp:TextBox ID="txtContextSrlNo" runat="server"
                        MaxLength="16" Enabled="true"   height="18px" width="60px"></asp:TextBox>
                </td>
             <td class="style23" style="width: 55px; height: 26px;" >
                <asp:CheckBox ID="chkIsItem" runat="server"  Width="50px"
                        Text="Item" Visible="true"  checked="true"  AutoPostBack="true" OnCheckedChanged="chkIsItem_Changed"/>
                </td>
        </tr>
        </table>
                
             <asp:panel ID="Panel2" runat="server" Width="1106px" Height="43px" Font-Size="X-Small" CssClass="XXSmall"
                    GroupingText="IOW Item grid">
                      <table style="width: 99%; height: 25px;" >
    
                    <tr>
                           <td style="width: 57px; text-align: left; height: 24px;">
                        <asp:Label ID="Label6" runat="server" Text="Item" 
                         Font-Bold="True"></asp:Label>
                      </td>
                        <td style="height: 24px">
                         <asp:DropDownList ID="ddlItem" runat="server"  MaxLength="128" Font-Size="XX-Small"
                          DataTextField="ItemName" DataValueField="ItemId" AutoPostBack="true" OnSelectedIndexChanged = "ddlItemChanged"
                        Width="185px" height="16px">
                        </asp:DropDownList>
                      </td>             
                  <td style="width: 61px; text-align: left; height: 24px;">
                    <asp:Label ID="Label13" runat="server" Text="IOWCode"  
                         Font-Bold="True"></asp:Label>
                </td>
                <td style="height: 24px">
                    <asp:TextBox ID="txtIOWItemCode" runat="server"  MaxLength="16" Enabled="false"
                       height="18px" width="69px"></asp:TextBox>
                </td>

                 <td style="width: 61px; text-align: left; height: 24px;">
                    <asp:Label ID="Label7" runat="server" Text="UOM"  
                         Font-Bold="True"></asp:Label>
                </td>
                <td style="height: 24px">
                    <asp:TextBox ID="txtItemUOM" runat="server"  MaxLength="16" Enabled="false"
                        height="18px" width="69px"></asp:TextBox>
                </td>

                <td style="width: 79px; text-align: left; height: 23px;">
                    <asp:Label ID="Label8" runat="server" Text="Qty"   Font-Bold="True"></asp:Label>
                </td>

                 <td style="height: 23px; width: 40px;">
                    <asp:TextBox ID="txtItemQty" runat="server"  MaxLength="16"       Width="56px" height="18px"></asp:TextBox>
                </td>
 
                 <td style="width: 107px; text-align: left; height: 23px;">
                    <asp:Label ID="Label1" runat="server" Text="Wastage"  Font-Bold="True"></asp:Label>
                </td>
                 <td style="height: 23px">
                    <asp:TextBox ID="txtWastage" runat="server" MaxLength="16" Text="0"
                        Width="86px" height="18px"></asp:TextBox>
                </td>
                  <td class="style21" style="width: 168px">
                <asp:CheckBox ID="chkIsImported" runat="server"   Text="Imported" Visible="true"  Enabled="false" />
                </td>

                 <td class="style28" style="width: 64px; height: 23px;">
                    <asp:Button ID="btnClear" runat="server" onclick="btnClear_Click" Text="Clear" Width="49px" />
                </td>
      
                <td style="width: 41px; height: 21px;">
                    <asp:Button ID="btnAdd" runat="server" Text="Add"  BackColor="Aqua"    onclick="btnAdd_Click"  Width="44px" />
               </td>
               <td style="width: 40px; height: 21px;">
                    <asp:TextBox ID="txtItemCode" runat="server"  MaxLength="16" Visible="false"  Width="10px" height="18px"></asp:TextBox>
               </td>
                <td >
                    <asp:TextBox ID="txtGridItemId" runat="server" MaxLength="16" Visible="false"   Width="10px" height="18px"></asp:TextBox>
                </td>
                <td >
                    <asp:TextBox ID="txtCRAMIOWHeadDtlId" runat="server"  MaxLength="16" Visible="false"
                        Width="10px" height="18px" Text="0"></asp:TextBox>
                </td>
                 <td >
                    <asp:TextBox ID="txtIOWItemDescription" runat="server"  MaxLength="16" Visible="false"
                        Width="10px" height="18px" Text =""></asp:TextBox>
                </td>
                </tr>

                </table>
                </asp:panel>

    </asp:panel>  
      <asp:panel ID="Panel1" runat="server" Width="1232px" Height="249px" CssClass="XSmall"
                    GroupingText="IOW Item Select grid ">
             <table style="width: 99%; height: 228px;" >
               <tr>
                  <td style="width: 1000px">
                    <asp:Panel ID = "PnlIowGrdSel" runat="server" Width="1037px" Height="230px" >
                     <div style="overflow:auto;height: 226px; width: 1027px;">
      <asp:GridView ID="grdIOWItemSel" runat="server" CellPadding="3" ToolTip="Click on IOW Name row Select IOW Item "
            Width="99%" AutoGenerateColumns="False" 
            Height="16px" GridLines="Vertical" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
            OnRowCommand="GrdIOWItemSel_RowCommand">

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
            <asp:TemplateField HeaderText="Dtl Id"  Visible="false">
            <ItemTemplate>
            <asp:Label ID="lblCRAMIOWHeadDtlId" runat="server" Text='<%# Eval("CRAMIOWHeadDtlId") %>'  ></asp:Label></ItemTemplate>
             <HeaderStyle Width="10px"  HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="IOW Code" Visible="true">
            <ItemTemplate>
            <asp:Label ID="lblIOWCode" runat="server" Text='<%# Eval("IOWCode") %>'  ></asp:Label></ItemTemplate>
              <HeaderStyle Width="30px"/><ItemStyle Width = "30px" />
               </asp:TemplateField>
            <asp:TemplateField HeaderText="IOW dtl Name" Visible="false">
            <ItemTemplate>
            <asp:Label ID="lblIOWDescription" runat="server" Text='<%# Eval("IOWDescription") %>'  ></asp:Label></ItemTemplate>
              <HeaderStyle Width="50px" HorizontalAlign="Center" /><ItemStyle Width = "50px" />
               </asp:TemplateField>
 
         <asp:TemplateField HeaderText="Click IOW Name for Update" Visible="true" >
            <ItemTemplate>  
            <asp:LinkButton ID="lnkIOWDescription" Width="800px" runat ="server" CommandArgument='<%#Eval("IOWCode")%>'
             CommandName ="IOW" Text ='<%#Eval("IOWDescription") %>'></asp:LinkButton>
              </ItemTemplate> <HeaderStyle Width="800px"  HorizontalAlign="Center" />
            </asp:TemplateField>
           <asp:TemplateField HeaderText="IOW UOM" Visible="true">
            <ItemTemplate>
            <asp:Label ID="lblIOWUOM" runat="server" Text='<%# Eval("IOWUOM") %>' Width="50px" ></asp:Label></ItemTemplate>
              <HeaderStyle Width="50px"  HorizontalAlign="Center" />
            </asp:TemplateField>
              <asp:TemplateField HeaderText="Is Temprory IOW">
             <ItemTemplate>
              <asp:CheckBox ID="chkIsTemproryIOW" runat="server" Checked='<%# Eval("IsTemproryIOW") %>'
             Width="30px" Enabled="false"></asp:CheckBox>
             </ItemTemplate>
                <HeaderStyle Width="30px" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
         
        </Columns>
        <sortedascendingcellstyle backcolor="#F1F1F1" />
        <sortedascendingheaderstyle backcolor="#0000A9" />
        <sorteddescendingcellstyle backcolor="#CAC9C9" />
        <sorteddescendingheaderstyle backcolor="#000065" />
    </asp:GridView>
      </div>
       </asp:Panel>
      </td>
                <td  style="width: 70px">
                 <asp:Panel ID="pnlfilt" runat="server" Width="160px" Height="217px" CssClass="XXSmall" >
                 <table style="width: 99%; height: 60px;" >
                    <tr>
                    <td style="width: 200px">
                   <asp:Label ID="TextBox1" runat="server" Text="Filter Text" Visible="true"
                  Font-Bold="true"   Width="142px" ></asp:Label>
                </td>
                </tr>
                <tr>
                       <td style="width: 200px">
                   <asp:TextBox ID="txtIowItemFilter" runat="server" Text="" Visible="true"
                  Font-Bold="true" Width="142px" ></asp:TextBox>
                </td>
                </tr>
                    <tr>
                       <td class="style28" style="width: 62px" >
                         <asp:Button ID="btnIOWItemFilter" runat="server"  Height="21px" onclick="btnIOWItemFilter_Click"   Text="Filter" Width="43px" />
                 </td>      
                      </tr>
                </table>
                </asp:Panel>
                </td>
                </tr>
               </table>
               </asp:panel>
 <asp:panel ID="pnlIOWItem" runat="server" Width="1241px" Height="253px" CssClass="XXSmall"
            GroupingText="IOW Item grid">
    <table>
        <tr>
            <td>
                <asp:Panel ID="Panel3" runat="server" Width="1165px" Height="237px" >
   <div id="div3" runat="server"  style="overflow:auto; height:233px; width:1151px">
    <asp:GridView ID="grdIOWItem" runat="server" CellPadding="1" 
            Width="1134px"  AutoGenerateColumns="False" 
            Height="16px" GridLines="Vertical" BorderColor="#999999" 
           BorderStyle="None" BorderWidth="1px"
             OnRowCommand="grdIOWItem_RowCommand"
           >
               
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
 
            <asp:TemplateField HeaderText="IOW Code" Visible="false">
            <ItemTemplate>
            <asp:Label ID="lblCRAMIOWCode" runat="server" Text='<%# Eval("CRAMIOWCode") %>'  ></asp:Label></ItemTemplate>
              <HeaderStyle Width="30px"/><ItemStyle Width = "30px" />
               </asp:TemplateField>
          <asp:TemplateField HeaderText="IOW Name" Visible="false">
             <ItemTemplate>
            <asp:Label ID="lblCRAMIOWName" runat="server" Text='<%# Eval("CRAMIOWName") %>'   Width="200px" ></asp:Label></ItemTemplate>
                <HeaderStyle Width="200px" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Srl No" Visible="true">
            <ItemTemplate>
            <asp:Label ID="lblContextSrlNo" runat="server" Text='<%# Eval("ContextSrlNo") %>'  ></asp:Label></ItemTemplate>
              <HeaderStyle Width="40px"/><ItemStyle Width = "40px" />
               </asp:TemplateField>
            <asp:TemplateField HeaderText="Context" Visible="false">
            <ItemTemplate>
            <asp:Label ID="lblContext" runat="server" Text='<%# Eval("Context") %>'  ></asp:Label></ItemTemplate>
              <HeaderStyle Width="300px"/><ItemStyle Width = "300px" />
               </asp:TemplateField>
            <asp:TemplateField HeaderText="Context" Visible="true">
            <ItemTemplate>
            <asp:Label ID="lblContextDisplay" runat="server" Text='<%# Eval("ContextDisplay") %>'  ></asp:Label></ItemTemplate>
              <HeaderStyle Width="200px"/><ItemStyle Width = "200px" />
               </asp:TemplateField>
            <asp:TemplateField HeaderText="ItemID" Visible="false">
            <ItemTemplate>
            <asp:Label ID="lblItemId" runat="server" Text='<%# Eval("ItemId") %>'  ></asp:Label></ItemTemplate>
              <HeaderStyle Width="50px"/><ItemStyle Width = "50px" />
               </asp:TemplateField>
            <asp:TemplateField HeaderText="IOW Id" Visible="false">
            <ItemTemplate>
            <asp:Label ID="lblIOWHeadDtlId" runat="server" Text='<%# Eval("IOWHeadDtlId") %>'  ></asp:Label></ItemTemplate>
              <HeaderStyle Width="40px"/><ItemStyle Width = "40px" />
               </asp:TemplateField>
            <asp:TemplateField HeaderText="IOW Item Code" Visible="true">
            <ItemTemplate>
            <asp:Label ID="lblIOWItemCode" runat="server" Text='<%# Eval("IOWCode") %>'  ></asp:Label></ItemTemplate>
              <HeaderStyle Width="40px"/><ItemStyle Width = "40px" />
               </asp:TemplateField>
          <asp:TemplateField HeaderText="Item Name" Visible="true">
             <ItemTemplate>
            <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ItemName") %>'   Width="400px" ></asp:Label></ItemTemplate>
                <HeaderStyle Width="400px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Item Code" Visible="true">
            <ItemTemplate>
            <asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("ItemCode") %>'  ></asp:Label></ItemTemplate>
              <HeaderStyle Width="30px"/><ItemStyle Width = "30px" />
               </asp:TemplateField>
 
            <asp:TemplateField HeaderText="Item UOM" Visible="true">
             <ItemTemplate>
            <asp:Label ID="lblItemUOM" runat="server" Text='<%# Eval("ITEMUOM") %>'   Width="30px" ></asp:Label></ItemTemplate>
                <HeaderStyle Width="30px" />
            </asp:TemplateField>
           <asp:TemplateField HeaderText="Item Qty" Visible="true">
            <ItemTemplate>
            <asp:Label ID="lblItemQty" runat="server" Text='<%# Eval("ItemQuantity") %>' Width="40px" ></asp:Label></ItemTemplate>
              <HeaderStyle Width="40px"/>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Wast age" Visible="true">
            <ItemTemplate>
            <asp:Label ID="lblWastage" runat="server" Text='<%# Eval("Wastage") %>'  ></asp:Label></ItemTemplate>
              <HeaderStyle Width="30px"/><ItemStyle Width = "30px" />
               </asp:TemplateField>
      
             <asp:TemplateField HeaderText="Impo rted">
             <ItemTemplate>
              <asp:CheckBox ID="chkIsImported" runat="server" Checked='<%# Eval("IsImported") %>'
             Width="30px" Enabled="false"></asp:CheckBox>
             </ItemTemplate>
                <HeaderStyle Width="30px" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>

 
        <asp:TemplateField HeaderText="Del Item" Visible="true" >
            <ItemTemplate>  
            <asp:LinkButton ID="lnkIOWItemNameDel" Width="30px" runat ="server" CommandArgument='<%#Eval("ContextSrlNo")%>'
             CommandName ="IOWItemDel" Text ="Del"></asp:LinkButton>
              </ItemTemplate> <HeaderStyle Width="30px" />
            </asp:TemplateField>
                       
        </Columns>
        <sortedascendingcellstyle backcolor="#F1F1F1" />
        <sortedascendingheaderstyle backcolor="#0000A9" />
        <sorteddescendingcellstyle backcolor="#CAC9C9" />
        <sorteddescendingheaderstyle backcolor="#000065" />
    </asp:GridView>
</div>
 </asp:Panel>
            </td>
     <td style="width: 41px; height: 25px;">
                    <asp:Button ID="btnItemSave" runat="server" Text="Save"  BackColor="Aquamarine"
                        onclick="btnItemSave_Click"  Width="39px" />
                </td>
</tr>
</table>

    </asp:panel>
</asp:Content>

