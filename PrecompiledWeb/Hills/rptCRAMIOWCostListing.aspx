<%@ page title="" language="C#" masterpagefile="~/MasterPage1.master" autoeventwireup="true" inherits="rptCRAMIOWCostListing, App_Web_sx5sst5a" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%--<asp:Label ID = "lblSeparator" runat = "server" align = "center" Height="15px" Width = "901px" ></asp:Label>--%>
    <asp:Label ID="lblExceptionList" runat="server" Align= "center" Text="IOW Cost Listing Report"
        Font ="Verdana" width="732px"  Font-Size ="12pt" Font-Bold="True" 
        Font-Names="Verdana"  style="text-align: center" Height="29px"></asp:Label>
       
        <%--<asp:Label ID = "lblStkOpDate" runat = "server" align = "center" Height="15px" Width = "901px" Text = "" ></asp:Label>--%>

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

 <asp:Panel ID="pnlPendind" runat="server" Height="41px" Width="1142px" 
        CssClass="XSmall">
        <table style="height: 22px; width: 1131px;">
            <tr>
                      <td class="style44">
                <asp:Label ID="lblCompany" runat="server" Text="Company" Visible="true"
                  Font-Bold="true" ></asp:Label>
           </td>
             <td style="width: 100px" class="style21">
                <asp:DropDownList ID="ddlCompany"  runat="server"  Visible="true" AutoPostBack="true" OnSelectedIndexChanged="ddlCompanyChanged"
                   DataTextField="CompanyName" DataValueField="CompanyId" Font-Size="X-Small"     Width="158px"    Height="16px"  >
                 </asp:DropDownList>
            </td>

          <td>
                <asp:Label ID="lblGroup" runat="server" Text="Group" Visible="true"
                  Font-Bold="true"  ></asp:Label>
           </td>
             <td>
                <asp:DropDownList ID="ddlGroup"  runat="server"  Visible="true" AutoPostBack="true" OnSelectedIndexChanged="ddlGroupChanged"
                  DataTextField="GroupName" DataValueField="GroupCode" Font-Size="X-Small"      Width="109px" height="24px"  >
                 </asp:DropDownList>
            </td>
   
         <td>
                <asp:Label ID="lblSubGroup" runat="server" Text="Sub Group" Visible="true"
                  Font-Bold="true"  ></asp:Label>
           </td>
             <td>
                <asp:DropDownList ID="ddlSubGroup"  runat="server"  Visible="true"
                    DataTextField="SubGroupName" DataValueField="SubGroupCode" Font-Size="X-Small"
                     Width="109px" height="24px"  >
                 </asp:DropDownList>
            </td>   
      <td >
                <asp:Label ID="lblRegion" runat="server" Text="Region" Visible="true"
                  Font-Bold="true"  ></asp:Label>
           </td>
             <td>
                <asp:DropDownList ID="ddlRegion"  runat="server"  Visible="true" 
                    DataTextField="Region" DataValueField="Region"  Style="font-size: X-Small" 
                     Width="67px"    Height="17px"  >
                 </asp:DropDownList>
            </td>
          <td>
                <asp:Label ID="lblYearMonth" runat="server" Text="Year Month" Visible="true"
                  Font-Bold="true" ></asp:Label>
           </td>
             <td>
                <asp:DropDownList ID="ddlForYearMonth"  runat="server"  Visible="true"
                    DataTextField="ForYearMonth" DataValueField="ForYearMonth" Font-Size="X-Small"
                     Width="85px" height="24px"  >
                 </asp:DropDownList>
            </td>  

              <td style="text-align: left;" class="style43" >
                   <asp:radiobuttonlist id="rbtType"  Visible="true" 
                                    RepeatDirection="Horizontal" runat="server" Height="19px" 
                    Width="150px"  Font-Bold="true">
                        <asp:listitem Text="Cost Only" Selected="true" Value="1" />
	                    <asp:listitem Text="Detail" Selected="false" Value="2"  />
	                                
                                    
                </asp:radiobuttonlist>              
                 </td> 

                <td class="style45" >
                         <asp:Button ID="btnView" runat="server"  Height="21px" onclick="btnView_Click" 
                               Text="View" Width="43px" />
                 </td>        
                  <td >
                           
                           <asp:Button ID="btnClear" runat="server" 
                               Height="21px" onclick="btnClear_Click" 
                               Text="Clear" Width="43px" />
                       </td>  
 
          <td>
                   <asp:Textbox ID="txtItem" runat="server" Visible="false"  Text = ""
                               Height="17px" Width="16px" MaxLength = "8" 
                                  ></asp:Textbox>
          </td>
 
             <td>
                <asp:DropDownList ID="ddlIOWHead"  runat="server"  Visible="false"
                    DataTextField="IOWHeadDescription" DataValueField="IOWHeadCode"
                    
                    Width="48px" height="28px"  >
                 </asp:DropDownList>
            </td>                              
            </tr>
               
        </table>
    </asp:Panel>
    <style type="text/css">
        .WordWrap {
            width: 100%;
            word-break: break-all;
        }
        .style41
    {
        width: 55px;
    }
        .style43
        {
            width: 67px;
        }
        .style44
        {
            width: 54px;
        }
        .style45
        {
            width: 50px;
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

