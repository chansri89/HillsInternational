<%@ page title="" language="C#" masterpagefile="~/MasterPage1.Master" autoeventwireup="true" inherits="ControlProcessingForUpload, App_Web_sx5sst5a" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%-- <asp:Label ID = "lblSeparator" runat = "server" align = "center" Height="15px" Width = "901px" ></asp:Label>--%>

  <asp:Label ID="lblCustomerMaster" runat="server" Align= "center" Text="Monthly Closure Control" CssClass="XSmall"
        width="787px"    Font-Size ="12pt" Font-Bold="True"   style="text-align: center"></asp:Label>
   <asp:Label ID = "lblSeparator1" runat = "server" align = "center" Height="15px" Width = "901px" ></asp:Label>
    <div style="overflow:auto; height: 471px; width: 1163px;">

 
    <asp:panel ID="Pnlgv" runat="server" Width="725px" Height="286px" CssClass="XSmall"
            GroupingText = "Processing Month Change" >
     <table style="height: 40px; width: 669px;">
       <tr>
         <td style="text-align: left; width: 114px;" >
            <asp:Label ID="lblCompName" runat="server" Text="Company Name" 
                Font-Bold="True"></asp:Label>
        </td>
         <td style="width: 171px"> <asp:DropDownList ID="ddlCompanyName" runat="server" 
                 AutoPostBack="true" OnSelectedIndexChanged = "ddlCompanyChanged"   Height="16px" Width="268px">
                    </asp:DropDownList>
        </td>
        <td>
             <asp:Button ID="btnSave" runat="server"  Height="21px"   Text="Save" Width="43px" onclick="btnSave_Click" />

        </td>
       </tr>
    </table>
  
        <div id="divCustomer" runat="server"  
            style="overflow:auto; height:208px; width:711px">
    <asp:GridView ID="GrdControlProcess" runat="server" CellPadding="3" 
             Width="694px" Font-Names="Verdana" AutoGenerateColumns="False" 
            Height="16px" GridLines="Vertical" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
        >
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
            <asp:Label ID="lblCompanyId" runat="server" Text='<%# Eval("CompanyId") %>'  ></asp:Label></ItemTemplate>
             <HeaderStyle Width="10px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ControlProcessingId"  Visible="false">
            <ItemTemplate>
            <asp:Label ID="lblControlProcessingId" runat="server" Text='<%# Eval("ControlProcessingId") %>'  ></asp:Label></ItemTemplate>
             <HeaderStyle Width="10px" />
            </asp:TemplateField>
           <asp:TemplateField HeaderText="Company Name"  Visible="false">
            <ItemTemplate>
            <asp:Label ID="lblCompanyName" runat="server" Text='<%# Eval("CompanyName") %>' Width="250px"  ></asp:Label></ItemTemplate>
             <HeaderStyle Width="250px" /><ItemStyle Width = "250px" />
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Year Month" Visible="true">
            <ItemTemplate>
            <asp:Label ID="lblYYYYMM" runat="server" Text='<%# Eval("PaySlipYearMonth") %>' width ="60px" ></asp:Label></ItemTemplate>
              <HeaderStyle Width="60px"/>
            </asp:TemplateField>

           <asp:TemplateField HeaderText="PF From Date" Visible="true" >
                    <ItemTemplate> <asp:label ID="lblPFFrom" runat="server" Text='<%# Eval("PFPeriodFrom","{0:dd-MM-yyyy}") %>' Width="70px"  > </asp:label>
                    </ItemTemplate>  <HeaderStyle Width="90px" /><ItemStyle HorizontalAlign ="Right" />
                    </asp:TemplateField>
          <asp:TemplateField HeaderText="PF To Date" Visible="true" >
                    <ItemTemplate> <asp:label ID="lblPFTo" runat="server" Text='<%# Eval("PFPeriodTo","{0:dd-MM-yyyy}") %>' Width="70px"  ></asp:label>
                    </ItemTemplate>  <HeaderStyle Width="90px" /><ItemStyle HorizontalAlign ="Right" />
                    </asp:TemplateField>
         <asp:TemplateField HeaderText="ESI From Date" Visible="true" >
                    <ItemTemplate> <asp:label ID="lblESIFrom" runat="server" Text='<%# Eval("ESIPeriodFrom","{0:dd-MM-yyyy}") %>' Width="70px"  ></asp:label>
                    </ItemTemplate>  <HeaderStyle Width="90px" /><ItemStyle HorizontalAlign ="Right" />
                    </asp:TemplateField>
         <asp:TemplateField HeaderText="ESI To Date" Visible="true" >
                    <ItemTemplate> <asp:label ID="lblESITo" runat="server" Text='<%# Eval("ESIPeriodTo","{0:dd-MM-yyyy}") %>' Width="70px"  ></asp:label>
                    </ItemTemplate>  <HeaderStyle Width="90px" /><ItemStyle HorizontalAlign ="Right" />
                    </asp:TemplateField>
         <asp:TemplateField HeaderText="Overtime From Date" Visible="true" >
                    <ItemTemplate> <asp:label ID="lblOvertimeFrom" runat="server" Text='<%# Eval("OverTimePeriodFrom","{0:dd-MM-yyyy}") %>' Width="70px"  ></asp:label>
                    </ItemTemplate>  <HeaderStyle Width="90px" /><ItemStyle HorizontalAlign ="Right" />
                    </asp:TemplateField>
         <asp:TemplateField HeaderText="PF From Date" Visible="true" >
                    <ItemTemplate> <asp:label ID="lblOvertimeTo" runat="server" Text='<%# Eval("OverTimePeriodTo","{0:dd-MM-yyyy}") %>' Width="70px"  ></asp:Label>
                    </ItemTemplate>  <HeaderStyle Width="90px" /><ItemStyle HorizontalAlign ="Right" />
                    </asp:TemplateField>
         <asp:TemplateField HeaderText="Working Days" Visible="true" >
                    <ItemTemplate> <asp:TextBox ID="txtWorkingDays" runat="server" Text='<%# Eval("WorkingDays") %>' Width="70px"  ></asp:TextBox>
                    </ItemTemplate>  <HeaderStyle Width="90px" /><ItemStyle HorizontalAlign ="Right" />
                    </asp:TemplateField>

        </Columns>
        <sortedascendingcellstyle backcolor="#F1F1F1" />
        <sortedascendingheaderstyle backcolor="#0000A9" />
        <sorteddescendingcellstyle backcolor="#CAC9C9" />
        <sorteddescendingheaderstyle backcolor="#000065" />
    </asp:GridView>
    </div>

    </asp:panel>
    <br />

    </div>
</asp:Content>

