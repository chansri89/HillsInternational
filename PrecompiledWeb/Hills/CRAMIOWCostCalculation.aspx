<%@ page title="" language="C#" masterpagefile="~/MasterPage1.master" autoeventwireup="true" inherits="CRAMIOWCostCalculation, App_Web_mlnhxkpa" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
    <asp:Label ID="lblExceptionList" runat="server" Align= "center" 
        Text="IOW Cost Calculation" CssClass="XSmall"
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

 <asp:Panel ID="pnlPendind" runat="server" Height="35px" Width="865px" 
        CssClass="XSmall">
        <table style="height: 22px; width: 711px;">
            <tr>
               <td  style="width: 70px">
                <asp:Label ID="lblCompany" runat="server" Text="Company" Visible="true"
                  Font-Bold="true"  ></asp:Label>
           </td>
             <td style="width: 100px" class="style21">
                <asp:DropDownList ID="ddlCompany"  runat="server"  Visible="true" AutoPostBack="true" OnSelectedIndexChanged="ddlCompanyChanged"
                     DataTextField="CompanyName" DataValueField="CompanyId"
                    Style="font-size: X-Small" Width="158px"    Height="16px"  >
                 </asp:DropDownList>
            </td>

          <td>
                <asp:Label ID="lblYear" runat="server" Text="Year" Visible="true"
                  Font-Bold="true"  ></asp:Label>
           </td>
                  <td>
                <asp:TextBox ID="txtYear" runat="server" Text="" Visible="true" MaxLength="4"
                  Font-Bold="true"   Width="48px" ></asp:TextBox>
           </td>   
           <td>
                <asp:Label ID="lblForMonth" runat="server" Text="Month" Visible="true"
                  Font-Bold="true"  ></asp:Label>
           </td>
            <td>
                <asp:DropDownList ID="ddlMonth"  runat="server"  Font-Size="XX-Small"
                     DataTextField="ForMonth" DataValueField="ForMonth"  Width="68px" height="24px"  >
                    <asp:ListItem Text="Select Pls" Value="0" />
                     <asp:ListItem Text="01" Value="01" />
                     <asp:ListItem Text="02" Value="02" />
                     <asp:ListItem Text="03" Value="03" />

                     <asp:ListItem Text="04" Value="04" />
                     <asp:ListItem Text="05" Value="05" />
                     <asp:ListItem Text="06" Value="06" />

                     <asp:ListItem Text="07" Value="07" />
                     <asp:ListItem Text="08" Value="08" />
                     <asp:ListItem Text="09" Value="09" />

                     <asp:ListItem Text="10" Value="10" />
                     <asp:ListItem Text="11" Value="11" />
                     <asp:ListItem Text="12" Value="12" />

                 </asp:DropDownList>
            </td>
       <td  style="width: 70px">
                <asp:Label ID="lblRegion" runat="server" Text="Region" Visible="true"
                  Font-Bold="true"  ></asp:Label>
           </td>
             <td style="width: 100px" class="style21">
                <asp:DropDownList ID="ddlRegion"  runat="server"  Visible="true" 
                    DataTextField="Region" DataValueField="Region"  Style="font-size: X-Small" Width="158px"    Height="16px"  >
                 </asp:DropDownList>
            </td>
                <td class="style41" >
                         <asp:Button ID="btnView" runat="server"   Height="22px" onclick="btnView_Click"  
                               Text="Calculate" Width="78px" />
                 </td>        
                  <td >
                           
                           <asp:Button ID="btnClear" runat="server"   Height="21px" onclick="btnClear_Click"  
                               Text="Clear" Width="43px" />
                       </td>  
 
          <td>
                   <asp:Textbox ID="txtItem" runat="server" Visible="false"  Text = ""
                                Height="17px" Width="33px" MaxLength = "8" ></asp:Textbox> <%--ontextchanged="txtEmpCode_TextChanged" --%>
          </td>
   
             <td>
                <asp:DropDownList ID="ddlForYearMonth"  runat="server"  Visible="false"
                    DataTextField="ForYearMonth" DataValueField="ForYearMonth"
                    
                    Style="font-size: X-Small" Width="30px" height="26px"  >
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

