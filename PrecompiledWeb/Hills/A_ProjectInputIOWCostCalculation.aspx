<%@ page title="" language="C#" masterpagefile="~/MasterPage1.master" autoeventwireup="true" inherits="A_ProjectInputIOWCostCalculation, App_Web_lkpdlk5o" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
    <asp:Label ID="lblExceptionList" runat="server" Align= "center" 
        Text="Project input IOW Cost Calculation" CssClass="XSmall"
        width="844px"  Font-Size ="12pt" Font-Bold="True"  
        style="text-align: center" Height="29px"></asp:Label>
      <script src="js/jquery-1.9.1.js" type="text/javascript"></script> <%--MasterPageFile="~/MasterPage4.master"--%>
   <script type="text/javascript">
       var prm = Sys.WebForms.PageRequestManager.getInstance();
       //Raised before processing of an asynchronous postback starts and the postback request is sent to the server.
       prm.add_beginRequest(BeginRequestHandler);
       // Raised after an asynchronous postback is finished and control has been returned to the browser.
       prm.add_endRequest(EndRequestHandler);

       function BeginRequestHandler(sender, args) {
           //Shows the modal popup - the update progress
           var popup = $find('<%= modalPopup.ClientID %>');
           if (popup != null) {
               popup.show();
           }
       }
       function EndRequestHandler(sender, args) {
           //Hide the modal popup - the update progress
           var popup = $find('<%= modalPopup.ClientID %>');
           if (popup != null) {
               popup.hide();
           }
       }

       </script>
       <asp:UpdateProgress ID="UpdateProgress" runat="server">
<ProgressTemplate>

<asp:Image ID="imgprocess" ImageUrl="~/Images/progressBar.gif" AlternateText="Processing" runat="server" />
</ProgressTemplate>
</asp:UpdateProgress>

<asp:modalpopupextender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
   <asp:Panel ID="Panel1" runat="server" Height="28px" Width="932px" 
        CssClass="XSmall">
        <table style="height: 22px; width: 915px;">
            <tr>
                      <td  style="width: 70px">
                <asp:Label ID="Label1" runat="server" Text="Company" Visible="true"
                  Font-Bold="true"  ></asp:Label>
           </td>
             <td style="width: 37px" class="style32" >
                <asp:DropDownList ID="ddlCompany"  runat="server"  Font-Size="X-Small"
                   DataTextField="CompanyName" DataValueField="CompanyId"           
                     Width="181px"  AutoPostBack="true" 
                     OnSelectedIndexChanged="ddlCompanyChanged" Height="26px"  >
                 </asp:DropDownList>
            </td>
              <td class="style28" style="width: 70px">
                <asp:Label ID="lblClients" runat="server" Text="Clients" Visible="true"
                  Font-Bold="true"   ></asp:Label>
           </td>
             <td style="width: 100px" class="style21">
                <asp:DropDownList ID="ddlClient"  runat="server"  Visible="true" AutoPostBack="true" 
                     OnSelectedIndexChanged="ddlClientChanged" Font-Size="X-Small"
                     DataTextField="ClientName" DataValueField="ClientCode"
                     Width="121px"  Height="22px"  >
                 </asp:DropDownList>
            </td>
          <td>
                <asp:Label ID="lblProject" runat="server" Text="Project" Visible="true"
                  Font-Bold="true"  ></asp:Label>
           </td>
             <td>
                <asp:DropDownList ID="ddlProject"  runat="server" 
                   DataTextField="ProjectName" DataValueField="ProjectCode" Font-Size="X-Small"
                    
                    Width="119px" height="22px"  >
                 </asp:DropDownList>
            </td>
           <td>
                <asp:Label ID="lblYearMonth" runat="server" Text="Year Month" Visible="true"
                  Font-Bold="true" ></asp:Label>
           </td>
             <td>
                <asp:DropDownList ID="ddlForYearMonth"  runat="server"  Visible="true"
                    DataTextField="ForYearMonth" DataValueField="ForYearMonth" Font-Size="X-Small"
                     Width="109px" height="24px"  >
                 </asp:DropDownList>
            </td>  
 
 
                <td class="style45" >
                         <asp:Button ID="btnCalc" runat="server"  Height="21px" onclick="btnCalc_Click" 
                               Text="Calculate" Width="71px" />
                 </td>        
                  <td >
                           
                           <asp:Button ID="btnClear" runat="server" 
                               Height="21px" onclick="btnClear_Click" 
                               Text="Clear" Width="43px" />
                       </td>  
                                                            
            </tr>
               
        </table>
    </asp:Panel>
 
    <style type="text/css">
        .WordWrap {
            width: 100%;
            word-break: break-all;
        }
        .style31
        {
            height: 32px;
            width: 60px;
        }
        .style32
        {
            width: 104px;
        }
        .style35
        {
            width: 73px;
        }
        .style37
        {
            height: 32px;
            width: 109px;
        }
        .style41
    {
        width: 63px;
    }
        .style42
        {
            width: 82px;
        }
        .style43
        {
            width: 67px;
        }
    </style>
<asp:Panel ID="pnlExList" runat="server" Height="459px" style="margin-left: 0px" 
        Width="1248px">

        <div style="width: 1238px; height: 438px;">
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
        Font-Size="8pt" Height="420px" InteractiveDeviceInfos="(Collection)" 
        WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="1216px"
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


</asp:Content>

