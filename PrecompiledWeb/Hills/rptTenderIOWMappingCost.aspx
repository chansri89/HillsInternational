<%@ page title="" language="C#" masterpagefile="~/MasterPage1.master" autoeventwireup="true" inherits="rptTenderIOWMappingCost, App_Web_lkpdlk5o" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="Panel2" runat="server" Height="16px" Width="870px" CssClass="XSmall"> </asp:Panel>
<asp:Label ID="lblStatMas" runat="server" Align= "center" Text="Tender-IOW Mapping Cost Listing Report Between Year Month -- Only for Latest Package" 
        width="823px" Font-Size ="12pt" Font-Bold="True"  style="text-align: center" Height="26px"></asp:Label>
<div style="overflow:auto; height: 664px;">

    <asp:panel ID="pnlAdd" runat="server" Width="1149px" GroupingText="Select Rate Year Month" 
       Height="56px" CssClass="XSmall">
    <table style="width: 98%; height: 38px;"  align="left">
            <tr>
                <td >
                <asp:Label ID="lblCompany" runat="server" Text="Company" Visible="true"
                  Font-Bold="true"  ></asp:Label>
           </td>
             <td  class="style21">
                <asp:DropDownList ID="ddlCompany"  runat="server"  Visible="true"
                    DataTextField="CompanyName" DataValueField="CompanyId" Font-Size="X-Small"
                   Width="155px"  AutoPostBack="true" 
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
                    
                     Width="138px" height="26px"  >
                 </asp:DropDownList>
            </td>
         <td  >
                <asp:Label ID="lblRegion" runat="server" Text="Region" Visible="true"
                  Font-Bold="true"   ></asp:Label>
           </td>
             <td >
                <asp:DropDownList ID="ddlRegion"  runat="server"  Visible="true" 
                    DataTextField="Region" DataValueField="Region"  Style="font-size: X-Small" 
                     Width="119px"    Height="17px"  >
                 </asp:DropDownList>
            </td>               
                <td>
                    <asp:Label ID="lblfromRateMonth" runat="server" Text="Fm YYYYMM" 
                         Font-Bold="True"></asp:Label>
                </td>
                <td >
                    <asp:DropDownList ID="ddlFromYearMonth" runat="server" 
                        MaxLength="8" DataTextField="ForYearMonth" DataValueField="ForYearMonth" Font-Size="X-Small"
                        Height="16px" Width="73px"></asp:DropDownList>
                </td>
                 <td >
                    <asp:Label ID="lblTOYearMonth" runat="server" Text="To YYYYMM" 
                       Font-Bold="True"></asp:Label>
                </td>
                <td >
                    <asp:DropDownList ID="ddlToYearMonth" runat="server" 
                        MaxLength="8" DataTextField="ForYearMonth" DataValueField="ForYearMonth" Font-Size="X-Small"
                        Height="16px" Width="73px"></asp:DropDownList>
                </td>   
                <td> 
                         <asp:RadioButtonList ID="rdbtnSummary" runat="server" 
                             RepeatDirection="Horizontal" Visible="true"
                            Width="87px"  Font-Bold="False" 
                             Height="24px">
                            <asp:ListItem Selected="true" Value="1" Text="Dtl"/>
                            <asp:ListItem Selected="false" Value="2" Text="sum"/>
                        </asp:RadioButtonList>  
                   </td>                                     
                <td>
                    <asp:Button ID="btnGo" runat="server" Text="Go"  onclick="btnGo_Click" Font-Size="X-Small" />
                </td>
               
                </tr>
               
        </table>
    </asp:panel>
<asp:Panel ID="pnlExList" runat="server" Height="420px" style="margin-left: 0px" 
        Width="1231px">

        <div style="width: 1217px; height: 409px;">
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
        Font-Size="8pt" Height="377px" InteractiveDeviceInfos="(Collection)" 
        WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="1203px"
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

