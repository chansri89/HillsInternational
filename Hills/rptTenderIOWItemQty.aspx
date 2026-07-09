<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.master" AutoEventWireup="true" CodeFile="rptTenderIOWItemQty.aspx.cs" Inherits="rptTenderIOWItemQty" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="Panel2" runat="server" Height="16px" Width="870px" CssClass="XSmall"> </asp:Panel>
<asp:Label ID="lblStatMas" runat="server" Align= "center" Text="Tender Project -IOW Item Wise Quantity Cost" 
        width="823px" Font-Size ="12pt" Font-Bold="True"  style="text-align: center" Height="26px"></asp:Label>
<div style="overflow:auto; height: 664px;">

    <asp:panel ID="pnlAdd" runat="server" Width="1125px" GroupingText="Select Rate Year Month" 
       Height="56px" CssClass="XSmall">
    <table style="width: 99%; height: 38px;"  align="left">
            <tr>
                <td >
                <asp:Label ID="lblCompany" runat="server" Text="Company" Visible="true"
                  Font-Bold="true"  ></asp:Label>
           </td>
             <td  class="style21">
                <asp:DropDownList ID="ddlCompany"  runat="server"  Visible="true"
                    DataTextField="CompanyName" DataValueField="CompanyId" Font-Size="X-Small"
                   Width="171px"  AutoPostBack="true" 
                     OnSelectedIndexChanged="ddlCompanyChanged" Height="20px"  >
                 </asp:DropDownList>
            </td>
               <td class="style28" >
                <asp:Label ID="lblClients" runat="server" Text="Clients" Visible="true"
                  Font-Bold="true"   ></asp:Label>
           </td>
             <td  class="style21">
                <asp:DropDownList ID="ddlClient"  runat="server"  Visible="true" AutoPostBack="true" 
                     OnSelectedIndexChanged="ddlClientChanged" Font-Size="X-Small"
                     DataTextField="ClientName" DataValueField="ClientCode"
                     Width="121px"  Height="22px"  >
                 </asp:DropDownList>
            </td>
         <td>
                <asp:Label ID="lblProject" runat="server" Text="Project" Visible="true"
                  Font-Bold="true"   ></asp:Label>
           </td>
             <td class="style34" style="width: 158px">
                <asp:DropDownList ID="ddlProject"  runat="server"  Visible="true"
                   DataTextField="ProjectName" DataValueField="ClientProjectId" Font-Size="X-Small"
                    
                     Width="152px" height="27px"  >
                 </asp:DropDownList>
            </td>
         <td  >
                <asp:Label ID="lblRegion" runat="server" Text="Region" Visible="true"
                  Font-Bold="true"   ></asp:Label>
           </td>
             <td >
                <asp:DropDownList ID="ddlRegion"  runat="server"  Visible="true" 
                    DataTextField="Region" DataValueField="Region"  Style="font-size: X-Small" 
                     Width="124px"    Height="17px"  >
                 </asp:DropDownList>
            </td>               
                <td>
                    <asp:Label ID="lblfromRateMonth" runat="server" Text="For Year Month" 
                         Font-Bold="True"></asp:Label>
                </td>
                <td >
                    <asp:DropDownList ID="ddlFromYearMonth" runat="server" 
                        MaxLength="8" DataTextField="ForYearMonth" DataValueField="ForYearMonth" Font-Size="X-Small"
                        Height="16px" Width="73px"></asp:DropDownList>
                </td>
  <%--               <td >
                    <asp:Label ID="lblTOYearMonth" runat="server" Text="To YYYYMM" 
                       Font-Bold="True"></asp:Label>
                </td>
                <td >
                    <asp:DropDownList ID="ddlToYearMonth" runat="server" 
                        MaxLength="8" DataTextField="ForYearMonth" DataValueField="ForYearMonth" Font-Size="X-Small"
                        Height="16px" Width="73px"></asp:DropDownList>
                </td>   --%>
                <td> 
                         <asp:RadioButtonList ID="rdbtnMatrix" runat="server" 
                             RepeatDirection="Horizontal" Visible="true"
                            Width="124px"  Font-Bold="False" 
                             Height="24px">
                            <asp:ListItem Selected="true" Value="1" Text="Matrix"/>
                            <asp:ListItem Selected="false" Value="2" Text="Table"/>
                        </asp:RadioButtonList>  
                   </td>                                     
                <td>
                    <asp:Button ID="btnGo" runat="server" Text="Go"  onclick="btnGo_Click" Font-Size="X-Small" />
                </td>
               
                </tr>
               
        </table>
    </asp:panel>
<asp:Panel ID="pnlExList" runat="server" Height="395px" style="margin-left: 0px" 
        Width="1225px">

        <div style="width: 1209px; height: 380px;">
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
        Font-Size="8pt" Height="370px" InteractiveDeviceInfos="(Collection)" 
        WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="1192px"
        ShowFindControls="false" ShowBackButton="false"   
                ShowPageNavigationControls = "true" ShowPrintButton = "false" 
                ShowRefreshButton = "false">
        <LocalReport ReportPath="">
        </LocalReport>
    </rsweb:ReportViewer>
          <%--  <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" 
                SelectCommand="SELECT * FROM [BugNet_ApplicationLog]"></asp:SqlDataSource>--%>
     </div>
</asp:Panel>
    </div>

</asp:Content>

