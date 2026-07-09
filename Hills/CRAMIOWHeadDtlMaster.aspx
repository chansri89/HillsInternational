<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.Master" AutoEventWireup="true" CodeFile="CRAMIOWHeadDtlMaster.aspx.cs" Inherits="CRAMIOWHeadDtlMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%-- <asp:Label ID = "lblSeparator" runat = "server" align = "center" Height="15px" Width = "901px" ></asp:Label>--%>

  <asp:Label ID="lblIOWHeadMaster" runat="server" Align= "center" Text="CRAM IOW Head Detail Master" 
       CssClass="XSmall" width="787px"   Font-Size ="12pt" Font-Bold="True"   style="text-align: center"></asp:Label>
   <%--<asp:Label ID = "lblSeparator1" runat = "server" align = "center" Height="15px" Width = "901px" ></asp:Label>--%>
  <%--  <div style="overflow:auto; height: 988px; width: 1246px;">--%>
        <%--</td></tr>
    </table>--%>    <%--</asp:Panel>DataKeyNames="IOWCode"--%>
    <asp:Panel ID="pnlPendind" runat="server" Height="28px" Width="962px" 
        CssClass="XSmall">
        <table style="height: 22px; width: 946px;">
            <tr>
                      <td  style="width: 70px">
                <asp:Label ID="lblCompany" runat="server" Text="Company" Visible="true"
                  Font-Bold="true"   ></asp:Label>
           </td>
             <td style="width: 100px" class="style21">
                <asp:DropDownList ID="ddlCompany"  runat="server" Font-Size="XX-Small"
                    DataTextField="CompanyName" DataValueField="CompanyId"
                    Style= Width="248px"  AutoPostBack="true" 
                     OnSelectedIndexChanged="ddlCompany_Changed" Height="16px"  >
                 </asp:DropDownList>
            </td>
                  <td style="width: 47px; text-align: left; height: 26px;">
                    <asp:Label ID="lblGroup" runat="server" Text="Group"  
                         Font-Bold="True"></asp:Label>
                </td>
                    <td style="height: 26px">
                    <asp:DropDownList ID="ddlGroup"  runat="server"   Font-Size="XX-Small"
                    DataTextField="GroupName" DataValueField="GroupCode"
                    Style= Width="102px"  Height="16px"  AutoPostBack="true" 
                     OnSelectedIndexChanged="ddlGroup_Changed" >
                 </asp:DropDownList>
                </td>
                            <td style="width: 83px; text-align: left; height: 23px;">
                                <asp:Label ID="lblSubGroup" runat="server" Font-Bold="True" 
                                   Text="Sub Group"></asp:Label>
                            </td>
                            <td style="height: 23px">
                                <asp:DropDownList ID="ddlsubGroup" runat="server" DataTextField="SubGroupName" 
                                    DataValueField="SubGroupCode" Height="16px"   Font-Size="XX-Small" Width="102px">
                                </asp:DropDownList>
                            </td>

                <td  style="width: 36px">
                <asp:Label ID="lblFilter" runat="server" Text="Filter" Visible="true"
                  Font-Bold="true"  ></asp:Label>
           </td>

            <td  style="width: 70px">
                <asp:TextBox ID="txtFilter" runat="server" Text="" Visible="true"
                  Font-Bold="true" ></asp:TextBox>
           </td>
                <td  style="width: 62px" >
                         <asp:Button ID="btnFilter" runat="server"    Height="21px" onclick="btnFilter_Click" 
                               Text="Filter" Width="43px" />
                 </td>      
                             
            </tr>
               
        </table>
    </asp:Panel>

    <asp:panel ID="Pnlgv" runat="server" Width="1216px" Height="436px" CssClass="XXSmall"
            GroupingText="CRAM IOW Head Grid" 
            >
        <div id="divIOW" runat="server"  
            style="overflow:auto; height:415px; width:99%">

    <asp:GridView ID="GrdIOWHeadMaster" runat="server" CellPadding="3" ToolTip="Click on IOW Name row to create IOW data"
             Width="98%"  AutoGenerateColumns="False" 
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
            <asp:Label ID="lblCompanyId" runat="server" Text='<%# Eval("CompanyId") %>'></asp:Label></ItemTemplate>
             <HeaderStyle Width="10px"  HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Grp Code" Visible="true">
            <ItemTemplate>
            <asp:Label ID="lblGroupCode" runat="server" Text='<%# Eval("GroupCode") %>'></asp:Label></ItemTemplate>
              <HeaderStyle Width="30px"/><ItemStyle Width = "30px" />
               </asp:TemplateField>
            <asp:TemplateField HeaderText="Grp Name" Visible="true">
            <ItemTemplate>
            <asp:Label ID="lblGroupName" runat="server" Text='<%# Eval("GroupName") %>' ></asp:Label></ItemTemplate>
              <HeaderStyle Width="70px" HorizontalAlign="Center" /><ItemStyle Width = "70px" />
               </asp:TemplateField>
 
            <asp:TemplateField HeaderText="Sub Grp Code" Visible="true">
            <ItemTemplate>
            <asp:Label ID="lblSubGroupCode" runat="server" Text='<%# Eval("SubGroupCode") %>'></asp:Label></ItemTemplate>
              <HeaderStyle Width="30px"/><ItemStyle Width = "30px" />
               </asp:TemplateField>

          <asp:TemplateField HeaderText="Sub Grp Name" Visible="true">
             <ItemTemplate>
            <asp:Label ID="lblSubGroupName" runat="server" Text='<%# Eval("SubGroupName") %>'   Width="70px"></asp:Label></ItemTemplate>
                <HeaderStyle Width="70px" HorizontalAlign="Center"  />
            </asp:TemplateField>
           <asp:TemplateField HeaderText="IOW level1" Visible="true">
            <ItemTemplate>
            <asp:Label ID="lblIOWLevel1" runat="server" Text='<%# Eval("IOWLevel1") %>' Width="30px"></asp:Label></ItemTemplate>
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
            <asp:Label ID="lblIOWHeadName" runat="server" Text='<%# Eval("IOWHeadName") %>' ></asp:Label></ItemTemplate>
              <HeaderStyle Width="50px" HorizontalAlign="Center" /><ItemStyle Width = "50px" />
               </asp:TemplateField>
 
         <asp:TemplateField HeaderText="Click IOW Head for Add / Update" Visible="true" >
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
        <asp:panel ID="pnlIOW" runat="server" Width="1221px" Height="288px" CssClass="XXSmall"
            GroupingText="CRAM IOW Dtl Grid"     >
        <div id="div2" runat="server"  
            style="overflow:auto; height:267px; width:99%">

    <asp:GridView ID="grdIOWdtl" runat="server" CellPadding="3" ToolTip="Click on IOW Name row to Modify IOW "
             Width="99%"  AutoGenerateColumns="False"   Height="16px" GridLines="Vertical" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
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
            <asp:Label ID="lblCompanyId" runat="server" Text='<%# Eval("CompanyId") %>' ></asp:Label></ItemTemplate>
             <HeaderStyle Width="10px"  HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Dtl Id"  Visible="false">
            <ItemTemplate>
            <asp:Label ID="lblCRAMIOWHeadDtlId" runat="server" Text='<%# Eval("CRAMIOWHeadDtlId") %>' ></asp:Label></ItemTemplate>
             <HeaderStyle Width="10px"  HorizontalAlign="Center" />
            </asp:TemplateField>
          <asp:TemplateField HeaderText="IOW Head Code" Visible="true">
            <ItemTemplate>
            <asp:Label ID="lblPreviousIOWLevel" runat="server" Text='<%# Eval("PreviousIOWLevel") %>'  ></asp:Label></ItemTemplate>
              <HeaderStyle Width="30px"/><ItemStyle Width = "30px" />
               </asp:TemplateField>
            <asp:TemplateField HeaderText="IOW Code" Visible="true">
            <ItemTemplate>
            <asp:Label ID="lblIOWCode" runat="server" Text='<%# Eval("IOWCode") %>' ></asp:Label></ItemTemplate>
              <HeaderStyle Width="30px"/><ItemStyle Width = "30px" />
               </asp:TemplateField>
            <asp:TemplateField HeaderText="IOW dtl Name" Visible="false">
            <ItemTemplate>
            <asp:Label ID="lblIOWDescription" runat="server" Text='<%# Eval("IOWDescription") %>' ></asp:Label></ItemTemplate>
              <HeaderStyle Width="50px" HorizontalAlign="Center" /><ItemStyle Width = "50px" />
               </asp:TemplateField>
 
         <asp:TemplateField HeaderText="Click IOW Head Dtl Name for Update" Visible="true" >
            <ItemTemplate>  
            <asp:LinkButton ID="lnkIOWDescription" Width="680px" runat ="server" CommandArgument='<%#Eval("IOWCode")%>'
             CommandName ="IOW" Text ='<%#Eval("IOWDescription") %>'></asp:LinkButton>
              </ItemTemplate> <HeaderStyle Width="680px"  HorizontalAlign="Center" />
            </asp:TemplateField>
           <asp:TemplateField HeaderText="IOW UOM" Visible="true">
            <ItemTemplate>
            <asp:Label ID="lblIOWUOM" runat="server" Text='<%# Eval("IOWUOM") %>' Width="50px"></asp:Label></ItemTemplate>
              <HeaderStyle Width="50px"  HorizontalAlign="Center" />
            </asp:TemplateField>
           <asp:TemplateField HeaderText="IOW Qty" Visible="true">
            <ItemTemplate>
            <asp:Label ID="lblIOWQty" runat="server" Text='<%# Eval("IOWQuantity") %>' Width="50px" ></asp:Label></ItemTemplate>
              <HeaderStyle Width="50px"  HorizontalAlign="Center" />
            </asp:TemplateField>
              <asp:TemplateField HeaderText="Is Temprory IOW">
             <ItemTemplate>
              <asp:CheckBox ID="chkIsTemproryIOW" runat="server" Checked='<%# Eval("IsTemproryIOW") %>'  Width="30px" Enabled="false"></asp:CheckBox>
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
    
          <asp:panel ID="pnlIOWAdd" runat="server" Width="1211px"  CssClass="XXSmall"
            style="margin-top: 0px; margin-right: 0px;" Height="183px" 
            GroupingText = "Add / Modify IOW Code">
            <table style="width: 1203px; height: 156px;">
            <tr>
             <td style="width: 37px" >
                    <asp:Label ID="Label5" runat="server" Text="IOW Head Code"  
                       Font-Bold="True"></asp:Label>
                </td>
                <td>
                      <asp:TextBox ID="txtCRAMIOWHeadCode" runat="server"  ReadOnly="true"  height="24px" Width="88px"></asp:TextBox>
                </td>
                    <td  style="width: 35px; height: 46px;">
                    <asp:Label ID="Label9" runat="server" Text="IOW Head Name" Font-Bold="True"></asp:Label>
                </td>
                <td style="height: 46px; width: 865px;">
                    <asp:TextBox ID="txtCRAMIOWHeadName" runat="server" TextMode="MultiLine" ReadOnly="true"
                       height="49px" width="852px"></asp:TextBox>
                </td>
                   <td  style="width: 64px; height: 23px;">
                                <asp:Button ID="btnClear" runat="server" onclick="btnClear_Click" Text="Clear" />
                            </td>

            </tr>
                <tr>
                    <td style="width: 37px; text-align: left; height: 46px;">
                    <asp:Label ID="Label2" runat="server" Text="IOW Code"  Font-Bold="True"></asp:Label>
                </td>
                    <td style="height: 46px">
                    <asp:TextBox ID="txtIOWCode" runat="server" MaxLength="16" 
                        height="24px" Width="90px"></asp:TextBox>
                </td>
                     <td class="style28" style="width: 35px; height: 46px;">
                    <asp:Label ID="lblIOWNm" runat="server" Text="IOW dtl Name"  
                        Font-Bold="True"></asp:Label>
                </td>
                     <td style="height: 46px; width: 865px;">
                    <asp:TextBox ID="txtIOWDescription" runat="server"  MaxLength="6000" TextMode="MultiLine"
                        height="84px" width="856px"></asp:TextBox>
                </td>
                <td>
                  <asp:panel ID="Panel2" runat="server" Width="161px" Height="97px" CssClass="XXSmall"
                        >
                   <table style="width: 94%; height: 98px;" >
                <tr>
                <td style="width: 88px; text-align: left; height: 27px;">
                    <asp:Label ID="Label10" runat="server" Text="IOW UOM"  
                        Font-Bold="True"></asp:Label>
                </td>
                <td style="height: 27px; width: 61px;">
                    <asp:TextBox ID="txtCRAMIOWUOM" runat="server"  MaxLength="16"  height="18px" width="69px"></asp:TextBox>
                </td>
                </tr>
                <tr>
                <td style="width: 88px; text-align: left; height: 25px;">
                    <asp:Label ID="Label11" runat="server" Text="IOW Qty"  Font-Bold="True"></asp:Label>
                </td>
    
                 <td style="height: 25px; width: 61px;">
                    <asp:TextBox ID="txtCRAMIOWQuantity" runat="server" MaxLength="16"
                        Width="71px" height="18px"></asp:TextBox>
                </td>
                </tr>
 
                <tr>
                <td style="width: 88px" >    
                 <asp:CheckBox ID="chkIsTempIOW" runat="server"  Text="Is Temp IOW" Visible="true"  Enabled="true" /></td>
                    <td  >
                     <asp:Button ID="btnSaveIOW" runat="server" Text="Save IOW" BackColor="Aqua"
                        onclick="btnSaveIOW_Click" Width="70px" />
                   
                    </td>
                </tr>
                
                </table>
                  </asp:panel>
                </td>
                </tr>
            </table>
       </asp:panel>  
         <asp:panel ID="PnlContext" runat="server" Width="1221px" Height="135px" CssClass="XXSmall"
            GroupingText="IOW Context Item grid">
            <table style="height: 105px; width: 1210px;">
                <tr>
                    <td  style="width: 35px; height: 46px;">
                    <asp:Label ID="lblContext" runat="server" Text="Context"  
                        Font-Bold="True"></asp:Label>
                </td>
                     <td style="height: 46px; width: 865px;">
                    <asp:TextBox ID="txtContext" runat="server"  MaxLength="6000" TextMode="MultiLine"
                        height="88px" width="683px"></asp:TextBox>
                </td>
                <td>
                   <asp:panel ID="Panel1" runat="server" Width="433px" Height="108px" CssClass="XXSmall"
                    GroupingText="IOW Item grid">
                      <table style="width: 81%; height: 91px;" >
    
                    <tr>
                      <td style="width: 57px; text-align: left; height: 24px;">
                        <asp:Label ID="Label6" runat="server" Text="Item" 
                         Font-Bold="True"></asp:Label>
                      </td>
                        <td style="height: 24px">
                         <asp:DropDownList ID="ddlItem" runat="server"  MaxLength="128"
                          DataTextField="ItemName" DataValueField="ItemId" AutoPostBack="true" OnSelectedIndexChanged = "ddlItemChanged"
                         Width="125px" height="17px">
                        </asp:DropDownList>
                      </td>
                    
                  <td style="width: 50px; text-align: left; height: 24px;">
                    <asp:Label ID="Label4" runat="server" Text="IOW" 
                         Font-Bold="True"></asp:Label>
                </td>
                 <td style="height: 24px">
                    <asp:DropDownList ID="ddlCRAMIOW" runat="server"  MaxLength="128"
                     DataTextField="CRAMIOWName" DataValueField="CRAMIOWCode" 
                        Width="97px" height="16px"></asp:DropDownList>
                </td>

                <td style="width: 70px; text-align: left; height: 24px;">
                    <asp:Label ID="Label7" runat="server" Text="Item UOM"  
                         Font-Bold="True"></asp:Label>
                </td>
                <td style="height: 24px">
                    <asp:TextBox ID="txtItemUOM" runat="server"  MaxLength="16" ReadOnly="true"
                       height="18px" width="69px"></asp:TextBox>
                </td>
                </tr>
                    <tr>
                <td style="width: 79px; text-align: left; height: 23px;">
                    <asp:Label ID="Label8" runat="server" Text="Item Qty"  
                        Font-Bold="True"></asp:Label>
                </td>
                 <td style="height: 23px">
                    <asp:TextBox ID="txtItemQty" runat="server"  MaxLength="16"
                         Width="126px" height="18px"></asp:TextBox>
                </td>

                 <td style="width: 50px; text-align: left; height: 23px;">
                    <asp:Label ID="Label1" runat="server" Text="Was tage"  
                       Font-Bold="True"></asp:Label>
                </td>
                 <td style="height: 23px">
                    <asp:TextBox ID="txtWastage" runat="server"  MaxLength="16"
                        Width="92px" height="18px"></asp:TextBox>
                </td>
                <td style="height: 23px"></td>
                <td style="height: 23px">
                <asp:CheckBox ID="chkIsImported" runat="server"
                        Text="Is Imported" Visible="true"  Enabled="true" />
                </td>
                </tr>
                   <tr>        
                <td style="width: 41px; height: 21px;">
                    <asp:Button ID="btnAdd" runat="server" Text="Add" BackColor="Aqua"
                        onclick="btnAdd_Click"  Width="38px" />
                </td>
                <td style="width: 41px; height: 21px;">
                    <asp:TextBox ID="txtGridItemId" runat="server"  MaxLength="16" Visible="false"
                        Width="10px" height="18px"></asp:TextBox>
                </td>
                 <td style="width: 50px; height: 21px;">
                    <asp:TextBox ID="txtItemCode" runat="server"  MaxLength="16" Visible="false"
                        Width="10px" height="18px"></asp:TextBox>
                </td>
                </tr>

                </table>
                </asp:panel>
                </td>
                </tr>
            </table>

    </asp:panel>  
         <asp:panel ID="pnlIOWItem" runat="server" Width="1179px" Height="306px"  CssClass="XXSmall"
            GroupingText="IOW Item grid">
        <div id="div1" runat="server"  
            style="overflow:auto; height:284px; width:1166px">
<table>
<tr>
 <td style="width: 562px; text-align: right; height: 25px;">
                    <asp:Label ID="Label3" runat="server" Text="Click to Save Item In Grid ----->"  
                       Font-Bold="True"></asp:Label>
                </td>
     <td style="width: 41px; height: 25px;">
                    <asp:Button ID="btnItemSave" runat="server" Text="Save" BackColor="Aquamarine"
                        onclick="btnItemSave_Click"  Width="50px" />
                </td>
</tr>
</table>
    <asp:GridView ID="grdIOWItem" runat="server" CellPadding="3" 
            Width="1153px"  AutoGenerateColumns="False" 
            Height="16px" GridLines="Vertical" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
             OnRowCommand="grdIOWItem_RowCommand"
           >      
        <EditRowStyle Font-Size="XX-Small" />    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black"  />  <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />   <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" 
            HorizontalAlign="Left" />  <AlternatingRowStyle BackColor="#DCDCDC" />

       
        <Columns>
            <asp:TemplateField HeaderText="CompanyId"  Visible="false">
            <ItemTemplate>
            <asp:Label ID="lblCompanyId" runat="server" Text='<%# Eval("CompanyId") %>' ></asp:Label></ItemTemplate>
             <HeaderStyle Width="10px" />
            </asp:TemplateField>
 

           <asp:TemplateField HeaderText="IOW Code" Visible="false">
            <ItemTemplate>
            <asp:Label ID="lblIOWCode" runat="server" Text='<%# Eval("IOWCode") %>' ></asp:Label></ItemTemplate>
              <HeaderStyle Width="40px"/><ItemStyle Width = "40px" />
               </asp:TemplateField>
            <asp:TemplateField HeaderText="IOW Name" Visible="false">
            <ItemTemplate>
            <asp:Label ID="lblIOWName" runat="server" Text='<%# Eval("IOWName") %>' ></asp:Label></ItemTemplate>
              <HeaderStyle Width="300px"/><ItemStyle Width = "300px" />
               </asp:TemplateField>
 
            <asp:TemplateField HeaderText="ItemID" Visible="true">
            <ItemTemplate>
            <asp:Label ID="lblItemId" runat="server" Text='<%# Eval("ItemId") %>' ></asp:Label></ItemTemplate>
              <HeaderStyle Width="50px"/><ItemStyle Width = "50px" />
               </asp:TemplateField>

          <asp:TemplateField HeaderText="Item Name" Visible="true">
             <ItemTemplate>
            <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ItemName") %>'   Width="200px"></asp:Label></ItemTemplate>
                <HeaderStyle Width="200px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Item Code" Visible="true">
            <ItemTemplate>
            <asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("ItemCode") %>'  ></asp:Label></ItemTemplate>
              <HeaderStyle Width="30px"/><ItemStyle Width = "30px" />
               </asp:TemplateField>
            <asp:TemplateField HeaderText="IOW Code" Visible="true">
            <ItemTemplate>
            <asp:Label ID="lblCRAMIOWCode" runat="server" Text='<%# Eval("CRAMIOWCode") %>' ></asp:Label></ItemTemplate>
              <HeaderStyle Width="30px"/><ItemStyle Width = "30px" />
               </asp:TemplateField>
          <asp:TemplateField HeaderText="IOW Name" Visible="true">
             <ItemTemplate>
            <asp:Label ID="lblIOWName" runat="server" Text='<%# Eval("IOWName") %>'   Width="200px" ></asp:Label></ItemTemplate>
                <HeaderStyle Width="200px" />
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

            <asp:TemplateField HeaderText="Wastage" Visible="true">
            <ItemTemplate>
            <asp:Label ID="lblWastage" runat="server" Text='<%# Eval("Wastage") %>' ></asp:Label></ItemTemplate>
              <HeaderStyle Width="50px"/><ItemStyle Width = "50px" />
               </asp:TemplateField>
      
             <asp:TemplateField HeaderText="Is Imported">
             <ItemTemplate>
              <asp:CheckBox ID="chkIsImported" runat="server" Checked='<%# Eval("IsImported") %>'  Width="30px" Enabled="false"></asp:CheckBox>
             </ItemTemplate>
                <HeaderStyle Width="30px" />     <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>

 
        <asp:TemplateField HeaderText="Del Item" Visible="true" >
            <ItemTemplate>  
            <asp:LinkButton ID="lnkIOWItemNameDel" Width="60px" runat ="server" CommandArgument='<%#Eval("ItemId")%>'
             CommandName ="IOWItemDel" Text ="Del Item"></asp:LinkButton>
              </ItemTemplate> <HeaderStyle Width="60px" />
            </asp:TemplateField>
        <asp:TemplateField HeaderText="Mod Item" Visible="true" >
            <ItemTemplate>  
            <asp:LinkButton ID="lnkIOWItemNameMod" Width="60px" runat ="server" CommandArgument='<%#Eval("ItemId")%>'
             CommandName ="IOWItemMod" Text ="Mod Item"></asp:LinkButton>
              </ItemTemplate> <HeaderStyle Width="60px" />
            </asp:TemplateField>
                       
        </Columns>
        <sortedascendingcellstyle backcolor="#F1F1F1" />
        <sortedascendingheaderstyle backcolor="#0000A9" />
        <sorteddescendingcellstyle backcolor="#CAC9C9" />
        <sorteddescendingheaderstyle backcolor="#000065" />
    </asp:GridView>
    </div>

    </asp:panel>

</asp:Content>

