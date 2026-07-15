<%@ page title="" language="C#" masterpagefile="~/MasterPage1.master" autoeventwireup="true" inherits="rptTenderRateComparision, App_Web_sx5sst5a" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="Panel2" runat="server" Height="16px" Width="870px" CssClass="XSmall"> </asp:Panel>
<asp:Label ID="lblStatMas" runat="server" Align= "center" Text="Tender-Rate Comparision " 
        width="823px" Font-Size ="12pt" Font-Bold="True"  style="text-align: center" Height="26px"></asp:Label>
<div style="overflow:auto; height: 664px;">

    <asp:panel ID="pnlAdd" runat="server" Width="1047px" GroupingText="Select Rate Year Month" 
       Height="63px" CssClass="XSmall">
    <table style="width: 99%; height: 38px;"  align="left">
            <tr>
                <td >
                <asp:Label ID="lblCompany" runat="server" Text="Company" Visible="true"
                  Font-Bold="true"  ></asp:Label>
           </td>
             <td >
                <asp:DropDownList ID="ddlCompany"  runat="server"  Visible="true"
                    DataTextField="CompanyName" DataValueField="CompanyId" Font-Size="X-Small"
                   Width="144px"  AutoPostBack="true" 
                     OnSelectedIndexChanged="ddlCompanyChanged" Height="16px"  >
                 </asp:DropDownList>
            </td>
               <td  >
                <asp:Label ID="lblClients" runat="server" Text="Clients" Visible="true"
                  Font-Bold="true"   ></asp:Label>
           </td>
             <td  class="style21">
                <asp:DropDownList ID="ddlClient"  runat="server"  Visible="true" AutoPostBack="true" 
                     OnSelectedIndexChanged="ddlClientChanged" Font-Size="X-Small"
                     DataTextField="ClientName" DataValueField="ClientCode"
                     Width="117px"  Height="22px"  >
                 </asp:DropDownList>
            </td>
         <td>
                <asp:Label ID="lblProject" runat="server" Text="Project" Visible="true"
                  Font-Bold="true"   ></asp:Label>
           </td>
             <td>
                <asp:DropDownList ID="ddlProject"  runat="server"  Visible="true"
                   DataTextField="ProjectName" DataValueField="ClientProjectId" Font-Size="X-Small"
                    
                     Width="132px" height="27px"  >
                 </asp:DropDownList>
            </td>
         <td  >
                <asp:Label ID="lblRegion" runat="server" Text="Region" Visible="true"
                  Font-Bold="true"   ></asp:Label>
           </td>
             <td >
                <asp:DropDownList ID="ddlRegion"  runat="server"  Visible="true" 
                    DataTextField="Region" DataValueField="Region"  Style="font-size: X-Small" 
                     Width="118px"    Height="17px"  >
                 </asp:DropDownList>
            </td>               
                <td >
                    <asp:Label ID="lblfromRateMonth" runat="server" Text="For Year Month" 
                         Font-Bold="True"></asp:Label>
                </td>
                <td >
                    <asp:DropDownList ID="ddlFromYearMonth" runat="server" 
                        MaxLength="8" DataTextField="ForYearMonth" DataValueField="ForYearMonth" Font-Size="X-Small"
                        Height="16px" Width="73px"></asp:DropDownList>
                </td>

                                    
                <td>
                    <asp:Button ID="btnGo" runat="server" Text="Go"  onclick="btnGo_Click" Font-Size="X-Small" />
                </td>
                     <td >
                    <asp:Label ID="lblTOYearMonth" runat="server" Text="To YYYYMM" Visible="false"  
                       Font-Bold="True"></asp:Label>
                </td>
                <td >
                    <asp:DropDownList ID="ddlToYearMonth" runat="server"  Visible="false"  
                        MaxLength="8" DataTextField="ForYearMonth" DataValueField="ForYearMonth" Font-Size="X-Small"
                        Height="16px" Width="73px"></asp:DropDownList>
                </td>   
                               <td> 
                         <asp:RadioButtonList ID="rdbtnSummary" runat="server" 
                             RepeatDirection="Horizontal" Visible="false"
                            Width="32px"  Font-Bold="False" 
                             Height="16px">
                            <asp:ListItem Selected="true" Value="1" Text=""/>
                           <%-- <asp:ListItem Selected="false" Value="2" Text="sum"/>--%>
                        </asp:RadioButtonList>  
                   </td>  
                </tr>
               
        </table>
    </asp:panel>
<asp:Panel ID="pnlExList" runat="server" Height="391px" style="margin-left: 0px" 
        Width="1232px">

        <div style="width: 1218px; height: 372px;">
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
        Font-Size="8pt" Height="361px" InteractiveDeviceInfos="(Collection)" 
        WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="1202px"
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

