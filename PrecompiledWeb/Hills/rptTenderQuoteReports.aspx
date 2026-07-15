<%@ page title="" language="C#" masterpagefile="~/MasterPage1.master" autoeventwireup="true" inherits="rptTenderQuoteReports, App_Web_mlnhxkpa" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%--<asp:Label ID = "lblSeparator" runat = "server" align = "center" Height="15px" Width = "901px" ></asp:Label>--%>
    <h1 class="tis-page-title">
        <asp:Label ID="lblExceptionList" runat="server" />
    </h1>
       
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

 <asp:Panel ID="pnlPendind" runat="server" CssClass="tis-card">
        <div class="tis-form-grid">
            <div class="tis-field">
                <asp:Label ID="lblCompany" runat="server" Text="Company" AssociatedControlID="ddlCompany" CssClass="tis-label" />
                <asp:DropDownList ID="ddlCompany" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCompanyChanged"
                    DataTextField="CompanyName" DataValueField="CompanyId" />
            </div>
            <div class="tis-field">
                <asp:Label ID="lblClients" runat="server" Text="Clients" AssociatedControlID="ddlClient" CssClass="tis-label" />
                <asp:DropDownList ID="ddlClient" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlClientChanged"
                    DataTextField="ClientName" DataValueField="ClientCode" />
            </div>
            <div class="tis-field">
                <asp:Label ID="lblProject" runat="server" Text="Project" AssociatedControlID="ddlProject" CssClass="tis-label" />
                <asp:DropDownList ID="ddlProject" runat="server" DataTextField="ProjectName" DataValueField="ProjectCode" />
            </div>
            <div class="tis-field">
                <span class="tis-label">Type</span>
                <asp:radiobuttonlist id="rbtType" Enabled="true" RepeatDirection="Horizontal" runat="server">
                    <asp:listitem Text="Cost Only" Selected="false" Value="1" />
                    <asp:listitem Text="Detail" Selected="True" Value="2" />
                </asp:radiobuttonlist>
            </div>
        </div>
        <p style="color:var(--tis-text-muted); font-size:var(--tis-fs-sm); margin:var(--tis-space-3) 0 0;">
            <asp:Label ID="Label1" runat="server" Text="Detail-- Display as in Package while cost will be on Budget Upload" ForeColor="DarkBlue" />
        </p>
        <div class="tis-toolbar" style="margin-top:var(--tis-space-4);">
            <asp:Button ID="btnView" runat="server" onclick="btnView_Click" Text="View" />
            <asp:Button ID="btnClear" runat="server" onclick="btnClear_Click" Text="Clear" CssClass="tis-btn-secondary" />
        </div>
        <asp:Textbox ID="txtItem" runat="server" Visible="false" Text="" MaxLength="8" />
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
        .style41
    {
        width: 63px;
    }
        .style43
        {
            width: 355px;
        }
    </style>
<asp:Panel ID="pnlExList" runat="server">

        <div class="tis-table-wrap" style="padding:0;">
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

