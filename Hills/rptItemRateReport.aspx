<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.master"  AutoEventWireup="true" CodeFile="rptItemRateReport.aspx.cs" Inherits="rptItemRateReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%--<asp:Label ID = "lblSeparator" runat = "server" align = "center" Height="15px" Width = "901px" ></asp:Label>--%>
    <asp:Label ID="lblExceptionList" runat="server" Align= "center" 
        Text="Item Rate Report" CssClass="XSmall"
        width="753px"  Font-Size ="12pt" Font-Bold="True"   
        style="text-align: center" Height="29px"></asp:Label>
       
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

 <asp:Panel ID="pnlPendind" runat="server" Height="35px" Width="892px" 
        CssClass="XXSmall">
        <table style="height: 22px; width: 884px;">
            <tr>
                      <td >
                <asp:Label ID="lblCompany" runat="server" Text="Company" Visible="true"
                  Font-Bold="true"  ></asp:Label>
           </td>
             <td  >
                <asp:DropDownList ID="ddlCompany"  runat="server"  Visible="true" AutoPostBack="true" OnSelectedIndexChanged="ddlCompanyChanged"
                    DataTextField="CompanyName" DataValueField="CompanyId"   Width="158px" Font-Size="X-Small"   Height="16px"  >
                 </asp:DropDownList>
            </td>

          <td>
                <asp:Label ID="lblItemCategory" runat="server" Text="Category" Visible="true"
                  Font-Bold="true" ></asp:Label>
           </td>
             <td>
                <asp:DropDownList ID="ddlItemCategory"  runat="server"  AutoPostBack="true" OnSelectedIndexChanged="ddlItemCategoryChanged"
                   DataTextField="ItemCategoryName" DataValueField="ItemCategoryId" Font-Size="X-Small"
                    
                   Width="109px" height="24px"  >
                 </asp:DropDownList>
            </td>
             <td>
                <asp:Label ID="lblSubCategory" runat="server" Text="Sub Category" Visible="true"
                  Font-Bold="true" ></asp:Label>
           </td>
             <td>
                <asp:DropDownList ID="ddlItemSubCategory"  runat="server"  AutoPostBack="true" OnSelectedIndexChanged="ddlItemSubCategoryChanged"
                   DataTextField="ItemSubCategoryName" DataValueField="ItemSubCategoryId" Font-Size="X-Small"
                    
                   Width="109px" height="24px"  >
                 </asp:DropDownList>
            </td>
                <td>
                <asp:Label ID="lblRegion" runat="server" Text="Region" Visible="true"
                  Font-Bold="true" ></asp:Label>
           </td>
             <td>
                <asp:DropDownList ID="ddlRegion"  runat="server"  Visible="true"
                   DataTextField="Region" DataValueField="Region" Font-Size="X-Small"
                    
                   Width="99px" height="25px"  >
                 </asp:DropDownList>
            </td>  

                <td  >
                         <asp:Button ID="btnView" runat="server" Height="21px" onclick="btnView_Click"  Text="View" Width="43px" />
                 </td>        
                  <td >
                           
                           <asp:Button ID="btnClear" runat="server"  Height="21px" onclick="btnClear_Click"   Text="Clear" Width="43px" />
                       </td>  
 
          <td>
                   <asp:Textbox ID="txtItem" runat="server" Visible="false"  Text = ""
                                Height="17px" Width="33px" MaxLength = "8" 
                                  ></asp:Textbox> <%--ontextchanged="txtEmpCode_TextChanged" --%>
          </td>
            <td style="text-align: left;" >
                   <asp:radiobuttonlist id="rbtType"  Visible="false" 
                                    RepeatDirection="Horizontal" runat="server" Height="18px" 
                    Width="157px"     Font-Bold="true">
                        <asp:listitem Text="Cost Only" Selected="true" Value="1" />
	                    <asp:listitem Text="Detail" Selected="false" Value="2"  />
	                                
                                    
                </asp:radiobuttonlist>              
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
        Width="1248px" >

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

