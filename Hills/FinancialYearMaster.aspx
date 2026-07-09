<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.Master" AutoEventWireup="true" CodeFile="FinancialYearMaster.aspx.cs" Inherits="FinancialYearMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:Label ID = "lblSeparator" runat = "server" align = "center" Height="15px" Width = "901px" ></asp:Label>
<asp:Label ID = "lblSeparator1" runat = "server" align = "center" Height="15px" Width = "901px" ></asp:Label>
<asp:Label ID="lblFinancialYearmas" runat="server" Align= "center" Text="Financial Year Master" 
        Font ="Verdana" width="787px"
        Font-Size ="12pt" Font-Bold="True" Font-Names="Verdana" 
        style="text-align: center"></asp:Label>
<div style="overflow:auto; height: 849px; ">
<asp:Label ID = "lblSeparator2" runat = "server" align = "center" Height="15px" Width = "901px" ></asp:Label>
    <asp:panel ID="Pnlgv" runat="server" Height="377px" Width="975px">
        <div style="overflow:auto; height:357px; width:534px">
        
    <asp:GridView ID="GrdFinancialYearMaster" runat="server" CellPadding="3" 
            Font-Size="XX-Small" Width="405px" Font-Names="Verdana" AutoGenerateColumns="False" 
            Height="16px" GridLines="Vertical" BackColor="White" BorderColor="#999999" 
                BorderStyle="None" BorderWidth="1px" 
                onrowcancelingedit="GrdFinancialYearMaster_RowCancelingEdit"                
                onrowediting="GrdFinancialYearMaster_RowEditing" 
                onrowupdating="GrdFinancialYearMaster_RowUpdating">
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" 
            HorizontalAlign="Left" />
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
         <asp:TemplateField HeaderText="Id" Visible="False">
            <ItemTemplate>
            <asp:Label ID="lblFinancialYearId" runat="server" Text='<%# Eval("FinancialYearId") %>'  Font-Names="Verdana" Font-Size="XX-Small"></asp:Label></ItemTemplate>
             <%--<EditItemTemplate>
            <asp:TextBox ID="txtIssueCategoryId" runat="server" Text='<%# Bind("IssueCategoryId") %>' ReadOnly="true" Font-Names="Verdana" Font-Size="XX-Small"></asp:TextBox></EditItemTemplate>
                <HeaderStyle Width="40px" />--%>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="FiscalYear" Visible="true">
                            <ItemTemplate>
                                <asp:Label ID="lblFiscalDate" runat="server" Text='<%# Eval("FiscalYear") %>' ></asp:Label></ItemTemplate> 
                                <EditItemTemplate>
                      <asp:TextBox ID="txtFiscalDate" runat="server" Text='<%# Bind("FiscalYear") %>' Width="75px" ReadOnly="false" CssClass="Grdtxtbox"></asp:TextBox>
                        </EditItemTemplate>
                            <HeaderStyle Width="40px" />
                        </asp:TemplateField>

           <%-- <asp:TemplateField HeaderText="FisicalYear" Visible="true">
                 <ItemTemplate>
                 <asp:Label ID="lblFisicalDate" runat="server" Text='<%# Eval("FisicalYear","{0:dd/MM/yyyy}") %>' ></asp:Label></ItemTemplate> 
                 <EditItemTemplate>
            <asp:TextBox ID="txtFisicalDate" runat="server" Text='<%# Bind("FisicalYear","{0:dd/MM/yyyy}") %>' Width="75px" ReadOnly="false" CssClass="Grdtxtbox"></asp:TextBox></EditItemTemplate>
                 <HeaderStyle Width="40px" />
                 </asp:TemplateField>--%>

             <asp:TemplateField HeaderText="FromDate" Visible="true" >
                    <ItemTemplate> 
                     <asp:Label ID="lblFromDate" runat="server" Text='<%# Eval("FromDate","{0:dd/MM/yyyy}") %>' >
                      </asp:Label></ItemTemplate>
                      <EditItemTemplate>
                      <asp:TextBox ID="txtFromDate" runat="server" Text='<%# Bind("FromDate","{0:dd/MM/yyyy}") %>' 
                      Width="75px" ReadOnly="false" CssClass="Grdtxtbox"></asp:TextBox></EditItemTemplate>
                   <HeaderStyle Width="20px" />
                 </asp:TemplateField>

                 <asp:TemplateField HeaderText="ToDate" Visible="true">
                    <ItemTemplate> 
                      <asp:Label ID="lblToDate" runat="server" Text='<%# Eval("ToDate","{0:dd/MM/yyyy}") %>' ></asp:Label></ItemTemplate>
                   <EditItemTemplate>
                   <asp:TextBox ID="txtToDate" runat="server" Text='<%# Bind("ToDate","{0:dd/MM/yyyy}") %>' Width="75px" ReadOnly="false" CssClass="Grdtxtbox"></asp:TextBox></EditItemTemplate>
                   <%-- <asp:ImageButton ID="ImgToDate" runat="server" ImageUrl="~/Images/Calendar.gif" />
                    <asp:CalendarExtender ID="cldToDate" TargetControlID="txtToDate" runat="server" PopupButtonID="ImgToDate" Format="dd/MM/yyyy" CssClass="Grdtxtbox">
                    </asp:CalendarExtender>--%>
                    
                    <HeaderStyle Width="20px" />
                 </asp:TemplateField>

            <asp:TemplateField HeaderText="IsActive">
             <ItemTemplate>
                <asp:CheckBox ID="chkActive" runat="server" Checked='<%# Eval("IsActive") %>'
            Font-Names="Verdana" Font-Size="XX-Small" Enabled="false"></asp:CheckBox>
             </ItemTemplate>
             <EditItemTemplate>
           <asp:CheckBox ID="chkIsActive" runat="server" Checked='<%# Bind("IsActive") %>'
            Font-Names="Verdana" Font-Size="XX-Small"></asp:CheckBox>
           </EditItemTemplate>
                <HeaderStyle Width="40px" />
                <ItemStyle HorizontalAlign="Center" />
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
    <asp:panel ID="pnlAdd" runat="server" Width="550px">
    <table style="width: 99%; height: 75px;"  align="left">
            <%--<tr>
                <td style="width: 106px; text-align: left;">
                    <asp:Label ID="lblFiscalYear" runat="server" Text="Fiscal Year"  
                        Font-Names="Verdana" Font-Size="XX-Small" style="font-weight: bold"></asp:Label>
                </td>
                <td style="width: 129px">
                    <asp:TextBox ID="txtFiscalYear" runat="server" Font-Names="Verdana" 
                        Font-Size="XX-Small"></asp:TextBox>
                </td>
            </tr>--%>
            
            <tr>
                <td style="width: 117px; height: 14px;">
                       <asp:Label ID="lblFromDate" runat="server" Font-Names="Verdana" 
                           Font-Size="XX-Small" style="font-weight: 700" Text="From Date"></asp:Label>
                </td>
                <td style="width: 177px; height: 14px;">
                    <asp:TextBox ID="txtFromDate" runat="server" Font-Size="XX-Small" Text="" 
                        Width="100px"></asp:TextBox>
                    <asp:ImageButton ID="imgfromdate" runat="server" ImageUrl="~/Images/Calendar.gif" />
                    <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtFromdate" 
                        runat="server" PopupButtonID="imgfromdate" Format="dd/MM/yyyy"></asp:CalendarExtender>
                </td>
                <td style="width: 100px; height: 14px;">
                    <asp:Label ID="lblToDate" runat="server" Font-Bold="False" Font-Names="Verdana" 
                        Font-Size="XX-Small" style="font-weight: 700" Text="To Date"></asp:Label>
                </td>
                <td style="width: 229px; height: 14px;">
                    <asp:TextBox ID="txtToDate" runat="server" Font-Size="XX-Small" Text="" 
                        Width="100px"></asp:TextBox>
                    <asp:ImageButton ID="imgToDate" runat="server" 
                        ImageUrl="~/Images/Calendar.gif" />
                     <asp:CalendarExtender ID="CalendarExtender2" TargetControlID="txtToDate" runat="server" 
                        PopupButtonID="imgToDate" Format="dd/MM/yyyy"></asp:CalendarExtender>
                </td>
                <tr>
                    <td class="style1" style="width: 136px; text-align: left;">
                        <asp:CheckBox ID="chkIsActive" runat="server" Checked="false" 
                            Font-Names="Verdana" Font-Size="XX-Small" 
                            style="font-weight: bold; text-align: left;" Text="IsActive" Visible="true" />
                    </td>
                </tr>

                <tr>
                    <td style="width: 106px; text-align: left;">
                        &nbsp;</td>
                    <td style="width: 129px">
                        <asp:Button ID="btnSave" runat="server" Font-Names="Verdana" 
                            Font-Size="XX-Small" onclick="btnSave_Click" Text="Save" />
                    </td>

                </tr>

              
             
                   <tr>
                       <td style="height: 14px">
                           <asp:HiddenField ID="HidCount" runat="server" Value="0" />
                       </td>
                       <%-- <tr>
                <td style="width: 106px; text-align: left"> <asp:HiddenField ID="HidDeleteCount" Value="0" runat="server" />
                <asp:HiddenField ID="HidUpdateCount" Value="0" runat="server" />
                </td>
            </tr>--%>
                </tr>
           </tr>

        </table>
    </asp:panel>
    
    </div>

</asp:Content>

