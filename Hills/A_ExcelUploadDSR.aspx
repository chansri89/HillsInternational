<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage3.master" AutoEventWireup="true" CodeFile="A_ExcelUploadDSR.aspx.cs" Inherits="A_ExcelUploadDSR" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<h1 class="tis-page-title">
    <asp:Label ID="lblPgmHdr" runat="server" />
</h1>
<asp:Label ID="Label1" runat="server" CssClass="XSmall" Font-Bold="True"
         Text = "Load BasicRate, DSRH, DSRI, DAR in that order one sheet at a Time...... For Item alone Only BasicRate to be Uploaded.... DAR Upload May take more than 5 Mts.. Pls wait"
            ForeColor="BlueViolet"></asp:Label>

    <div class="tis-card XSmall">
    <table id="tblFileType" runat="server" style="width: 993px; height: 47px;" >
        <tr>
 
            <td >
                <asp:Label ID="lblCompany" runat="server" Text="Company" Visible="true"
                  Font-Bold="true"  ></asp:Label>
                  
           </td>
             <td>
                <asp:DropDownList ID="ddlCompany"  runat="server"  Visible="true"
                    DataTextField="CompanyName" DataValueField="CompanyId" Font-Size="X-Small"
                    Width="138px"  height="16px" >
                 </asp:DropDownList>
            </td>
            

            <td>
                <asp:Label ID="lblFileUpload" runat="server" Text="File Path: " Font-Size="X-Small"
                  Font-Bold="true"  ></asp:Label>
            </td>
            <td>
                <asp:FileUpload ID="FlUpdExcel" runat="server" Width="322px" Height="21px" Font-Size="X-Small" />
            </td>
            <td>
                <asp:Button ID="btnUpload" runat="server" Font-Size="X-Small"
              ForeColor="Black" BackColor="LightBlue" OnClick="btnUpload_Click" Text="Upload" Width="63px" 
              TabIndex="16" />
            </td>

            <td>
                    <asp:TextBox ID="txtWParameterId" runat="server" 
                        height="15px" Visible="false"  Width="10px"></asp:TextBox>
                </td>
            <td>
                    <asp:TextBox ID="txtFileName" runat="server"      height="15px" Visible="false"
                        Width="10px"></asp:TextBox>
                </td>
 
           <td>
                <asp:Label ID="lblOperationGroupName" runat="server" Text="File Type"  Font-Bold="false" Visible="false"></asp:Label>
            </td>
           <td >
                <asp:radiobuttonlist id="rbtExcelType"  Visible="false" Enabled="false"
                                    RepeatDirection="Horizontal" runat="server" Height="16px" 
                    Width="63px">
	                                <asp:listitem Text="Excel" Selected="True" Value="1"  />
	                               <%-- <asp:listitem Text="CSV"  Value="2"/>--%>
                </asp:radiobuttonlist>
           </td>
 
        </tr>
    </table>

    <table style="width: 1070px; height: 24px">
    <tr>
    <td style="width: 748px">
     <asp:Label ID="lblMessage" runat="server" CssClass="XSmall" 
            Width="734px" ForeColor="#FF3300" Height="19px"></asp:Label>

    </td>
    <td>
       <asp:panel ID="Panel1" runat="server" Height="36px" Width="172px" CssClass="XSmall" >
       <table> <tr>
       <td>
        <asp:Label ID="lblRevnoChanged" runat="server" Text="Are You Sure For Changing Revision Number"
            Width="158px" ForeColor="#FF3300" Height="19px"></asp:Label>
         </td>
           
            <td>
                <asp:Button ID="btnYes" runat="server" 
              ForeColor="Black" BackColor="LawnGreen" OnClick="btnYes_Click" Text="Yes" Width="30px" 
              TabIndex="16" />
            </td>
            <td>
                <asp:Button ID="btnNo" runat="server"     ForeColor="Black" BackColor ="Red" OnClick="btnNo_Click" Text="No" Width="30px" 
              TabIndex="16" />
            </td>
             <td>
                    <asp:TextBox ID="txtSheetName" runat="server" 
                        height="15px" Visible="false"      Width="10px"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtOnlyAmount" runat="server" 
                        height="15px" Visible="false"      Width="10px"></asp:TextBox>
                </td>
            </tr>
        </table>
       </asp:panel>
    </td>
    </tr>
    </table>
   
           

</div>

   <asp:panel ID="pnlSheetName" runat="server" Height="378px" Width="1017px" CssClass="XXSmall">
    <table><tr>
    <td style="width: 834px"> 
    <asp:Label ID="Label4" runat="server" Text ="Select the Sheets to be Uploaded and mark for OnlyAmount and click Save "
        Font-Bold="True"  ForeColor="DarkBlue"></asp:Label> </td>
        <td></td>
        <td>
                <asp:Button ID="btnSave" runat="server"  ForeColor="Black" BackColor ="Aqua" OnClick="btnSave_Click" Text="Save" Width="56px" 
                         UseSubmitBehavior="false"    OnClientClick="this.Disabled='true'; this.Value='Pls Wait..';"
               />
               </td>
            <td>
                <asp:Button ID="btnDeselect" runat="server"  ForeColor="Black" BackColor ="Orange" OnClick="btnDeSelect_Click" 
                    Text="DeSelect All" Width="83px" 
               />
               </td>
        </tr></table>
     <div style="overflow:auto; height:340px; width:1000px"      title = "SheetName">
       <asp:GridView ID="grdSheetName" runat="server" CellPadding="3"  
             ForeColor="Red"      Width="985px"   AutoGenerateColumns="False" Height="16px" 
             GridLines="Vertical" BackColor="White" BorderColor="#999999"      
             BorderStyle="None" BorderWidth="1px" >
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />     <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" /> <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White"      HorizontalAlign="Left" />
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
                       <asp:TemplateField HeaderText="File Name" Visible="true" >
            <ItemTemplate>
            <asp:Label ID="lblExcelFileName" runat="server" Text='<%# Eval("ExeclFileName") %>' Width="600px" ></asp:Label></ItemTemplate>
            <HeaderStyle Width="600px" />
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Sheet Name" Visible="false" >
            <ItemTemplate>
            <asp:Label ID="lblSheetName" runat="server" Text='<%# Eval("ExeclSheetName") %>' Width="150px" ></asp:Label></ItemTemplate>
            <HeaderStyle Width="150px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Sheet Name" Visible="true" >
            <ItemTemplate>
            <asp:Label ID="lblDispSheetName" runat="server" Text='<%# Eval("DispSheetName") %>' Width="150px" ></asp:Label></ItemTemplate>
            <HeaderStyle Width="150px" /><ItemStyle Width= "150px"   /> 
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Only Amount" Visible="false" >
            <ItemTemplate>
            <asp:CheckBox ID="chkOnlyAmount" runat="server" checked = "false"  Width="40px" ></asp:CheckBox></ItemTemplate>
            <HeaderStyle Width="40px" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="UpLoad Sheet" Visible="true" >
            <ItemTemplate>
            <asp:CheckBox ID="chkUpLoadSheet" runat="server" checked = "false"  Width="40px" ></asp:CheckBox></ItemTemplate>
            <HeaderStyle Width="40px" />
            </asp:TemplateField>
      
           
        </Columns>
        <sortedascendingcellstyle backcolor="#F1F1F1" />
        <sortedascendingheaderstyle backcolor="#0000A9" />
        <sorteddescendingcellstyle backcolor="#CAC9C9" />
        <sorteddescendingheaderstyle backcolor="#000065" />
    </asp:GridView>
    </div>
    </asp:panel>
   <asp:panel ID="PnlNotOKDeptSales" runat="server" Height="297px" Width="1153px" CssClass="XSmall">
    <table><tr>
    <td> 
    <asp:Label ID="lblGrdNotOK" runat="server" Text ="Grid Showing Data Status ... If in Red Some Rows Status not be OK "
        Font-Bold="True"  ForeColor="Red"></asp:Label> </td></tr></table>
     <div style="overflow:auto; height:259px; width:1145px" 
           title = "OK Data in UpLoad">
       <asp:GridView ID="GrdWtNotOKDeptSales" runat="server" CellPadding="3"   ForeColor="Red"
            Width="1092px"  AutoGenerateColumns="False" Height="37px" 
             GridLines="Vertical" BackColor="White" BorderColor="#999999"      BorderStyle="None" BorderWidth="1px" >
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />     <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" /> <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White"      HorizontalAlign="Left" />
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>

                <asp:TemplateField HeaderText="#" Visible="true" >
            <ItemTemplate>
            <asp:Label ID="lblRowNumber" runat="server" Text='<%# Eval("RKount") %>' Width="20px" ></asp:Label></ItemTemplate>
            <HeaderStyle Width="20px" />
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Invoice Number" Visible="true" >
            <ItemTemplate>
            <asp:Label ID="lblEventName" runat="server" Text='<%# Eval("InvoiceNumber") %>' Width="80px" ></asp:Label></ItemTemplate>
            <HeaderStyle Width="80px" />
            </asp:TemplateField>
           <asp:TemplateField HeaderText="InvoiceDate" >
            <ItemTemplate>
            <asp:Label ID="lblInvoiceDate" runat="server" Text='<%# Eval("InvoiceDate","{0: dd-MM-yyyy}") %>'></asp:Label></ItemTemplate>
            <HeaderStyle Width="80px" /> <ItemStyle Width="80px" />
            </asp:TemplateField>
      
            <asp:TemplateField HeaderText="PromptDate" >
            <ItemTemplate>
            <asp:Label ID="lblPromptDate" runat="server" Text='<%# Eval("PromptDate","{0: dd-MM-yyyy}") %>' ></asp:Label></ItemTemplate>
            <HeaderStyle Width="80px" /> <ItemStyle Width="80px" />
            </asp:TemplateField>

                 <asp:TemplateField HeaderText="BuyerName" Visible="true" >
            <ItemTemplate>
            <asp:Label ID="lblBuyerName" runat="server" Text='<%# Eval("BuyerName") %>' Width="150px" ></asp:Label></ItemTemplate>
            <HeaderStyle Width="150px" />
            </asp:TemplateField>

             <asp:TemplateField HeaderText="Grade" Visible="true" >
            <ItemTemplate>
            <asp:Label ID="lblGrade" runat="server" Text='<%# Eval("Grade") %>' Width="120px" ></asp:Label></ItemTemplate>
            <HeaderStyle Width="120px" />
            </asp:TemplateField>

             <asp:TemplateField HeaderText="Status" Visible="true" >
            <ItemTemplate>
            <asp:Label ID="lblResult" runat="server" Text='<%# Eval("Result") %>'></asp:Label></ItemTemplate>
            <HeaderStyle Width="350px" /> <ItemStyle Width="350px" />
            </asp:TemplateField>


        </Columns>
        <sortedascendingcellstyle backcolor="#F1F1F1" />
        <sortedascendingheaderstyle backcolor="#0000A9" />
        <sorteddescendingcellstyle backcolor="#CAC9C9" />
        <sorteddescendingheaderstyle backcolor="#000065" />
    </asp:GridView>
    </div>
    </asp:panel>
   <asp:panel ID="PnlDepotSalesStatus" runat="server" Height="364px" Width="947px" CssClass="XXSmall">
        <div style="overflow:auto; height:352px; width:936px">
        <table><tr><td> <b style ="color: #33CC33"> Grid Showing all Rows in Uploaded Status</b></td></tr></table>
           <asp:GridView ID="GrdDepotSalesStatus" runat="server" CellPadding="3" 
                Width="921px"   AutoGenerateColumns="False"   Height="37px" GridLines="Vertical" BackColor="White" BorderColor="#999999"  
                BorderStyle="None" BorderWidth="1px" >
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />     <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" /> <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White"      HorizontalAlign="Left" />
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>

                 <asp:TemplateField HeaderText="#" Visible="true" >
            <ItemTemplate>
            <asp:Label ID="lblRowNumber" runat="server" Text='<%# Eval("RKount") %>' Width="20px" ></asp:Label></ItemTemplate>
            <HeaderStyle Width="20px" />
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Invoice Number" Visible="true" >
            <ItemTemplate>
            <asp:Label ID="lblEventName" runat="server" Text='<%# Eval("InvoiceNumber") %>' Width="80px" ></asp:Label></ItemTemplate>
            <HeaderStyle Width="80px" />
            </asp:TemplateField>
           <asp:TemplateField HeaderText="InvoiceDate" >
            <ItemTemplate>
            <asp:Label ID="lblInvoiceDate" runat="server" Text='<%# Eval("InvoiceDate","{0: dd-MM-yyyy}") %>'></asp:Label></ItemTemplate>
            <HeaderStyle Width="80px" /> <ItemStyle Width="80px" />
            </asp:TemplateField>
      
            <asp:TemplateField HeaderText="PromptDate" >
            <ItemTemplate>
            <asp:Label ID="lblPromptDate" runat="server" Text='<%# Eval("PromptDate","{0: dd-MM-yyyy}") %>' ></asp:Label></ItemTemplate>
            <HeaderStyle Width="80px" /> <ItemStyle Width="80px" />
            </asp:TemplateField>

                 <asp:TemplateField HeaderText="BuyerName" Visible="true" >
            <ItemTemplate>
            <asp:Label ID="lblBuyerName" runat="server" Text='<%# Eval("BuyerName") %>' Width="150px" ></asp:Label></ItemTemplate>
            <HeaderStyle Width="150px" />
            </asp:TemplateField>

             <asp:TemplateField HeaderText="Grade" Visible="true" >
            <ItemTemplate>
            <asp:Label ID="lblGrade" runat="server" Text='<%# Eval("Grade") %>' Width="120px" ></asp:Label></ItemTemplate>
            <HeaderStyle Width="120px" />
            </asp:TemplateField>
        </Columns>
        <sortedascendingcellstyle backcolor="#F1F1F1" />
        <sortedascendingheaderstyle backcolor="#0000A9" />
        <sorteddescendingcellstyle backcolor="#CAC9C9" />
        <sorteddescendingheaderstyle backcolor="#000065" />
    </asp:GridView>

    </div>
    </asp:panel>

    <asp:panel ID="pnlNotOKTeaBoard" runat="server" Height="297px" Width="1153px" CssClass="XXSmall">
    <table><tr>
    <td> 
    <asp:Label ID="Label2" runat="server" Text ="Grid Showing Data Status ... If in Red Some Rows Status may not be OK "
        Font-Bold="True"  ForeColor="Red"></asp:Label> </td></tr></table>

     <div style="overflow:auto; height:259px; width:1145px"  title = "OK Data in UpLoad">
       <asp:GridView ID="grdNotOKTeaBoard" runat="server" CellPadding="3"      
             ForeColor="Red"    Width="934px"   AutoGenerateColumns="False" Height="37px" 
             GridLines="Vertical" BackColor="White" BorderColor="#999999"    
             BorderStyle="None" BorderWidth="1px" >
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />     <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" /> <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White"      HorizontalAlign="Left" />
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>

                <asp:TemplateField HeaderText="Deal Identification Id" Visible="true" >
            <ItemTemplate> 
            <asp:Label ID="lblDealIdentificationId" runat="server" Text='<%# Eval("DealIdentificationId") %>' Width="90px" ></asp:Label></ItemTemplate>
            <HeaderStyle Width="90px" />
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Bank UTR" Visible="true" >
            <ItemTemplate>
            <asp:Label ID="lblBankUTR" runat="server" Text='<%# Eval("BankUTR") %>' Width="80px" ></asp:Label></ItemTemplate>
            <HeaderStyle Width="80px" />
            </asp:TemplateField>
           <asp:TemplateField HeaderText="Bank Date" >
            <ItemTemplate>
            <asp:Label ID="lblBankDate" runat="server" Text='<%# Eval("BankDate","{0: dd-MM-yyyy}") %>'></asp:Label></ItemTemplate>
            <HeaderStyle Width="90px" /> <ItemStyle Width="90px" />
            </asp:TemplateField>
      
             <asp:TemplateField HeaderText="Buyer Entity Code" Visible="true" >
            <ItemTemplate>
            <asp:Label ID="lblBuyerEntityCode" runat="server" Text='<%# Eval("BuyerEntityCode") %>' Width="150px" ></asp:Label></ItemTemplate>
            <HeaderStyle Width="150px" />
            </asp:TemplateField>

             <asp:TemplateField HeaderText="Amount " Visible="true" >
            <ItemTemplate>
            <asp:Label ID="lblAmountPaid" runat="server" Text='<%# Eval("AmountPaid") %>' Width="120px" ></asp:Label></ItemTemplate>
            <HeaderStyle Width="120px" />
            </asp:TemplateField>

             <asp:TemplateField HeaderText="Status" Visible="true" >
            <ItemTemplate>
            <asp:Label ID="lblResult" runat="server" Text='<%# Eval("Result") %>'></asp:Label></ItemTemplate>
            <HeaderStyle Width="250px" /> <ItemStyle Width="250px" />
            </asp:TemplateField>


        </Columns>
        <sortedascendingcellstyle backcolor="#F1F1F1" />
        <sortedascendingheaderstyle backcolor="#0000A9" />
        <sorteddescendingcellstyle backcolor="#CAC9C9" />
        <sorteddescendingheaderstyle backcolor="#000065" />
    </asp:GridView>
    </div>
    </asp:panel>
   <asp:panel ID="PnlTeaBoardStatus" runat="server" Height="364px" Width="947px" CssClass="XSmall">
        <div style="overflow:auto; height:352px; width:936px">
        <table><tr><td> <b style ="color: #33CC33"> Grid Showing all Rows in Uploaded Status</b></td></tr></table>
           <asp:GridView ID="grdTeaBoardStatus" runat="server" CellPadding="3"  Width="921px"  AutoGenerateColumns="False" 
            Height="37px" GridLines="Vertical" BackColor="White" BorderColor="#999999"  
                BorderStyle="None" BorderWidth="1px" >
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />     <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" /> <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White"      HorizontalAlign="Left" />
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>

                 <asp:TemplateField HeaderText="#" Visible="true" >
            <ItemTemplate>
            <asp:Label ID="lblRowNumber" runat="server" Text='<%# Eval("RKount") %>' Width="20px" ></asp:Label></ItemTemplate>
            <HeaderStyle Width="20px" />
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Invoice Number" Visible="true" >
            <ItemTemplate>
            <asp:Label ID="lblEventName" runat="server" Text='<%# Eval("InvoiceNumber") %>' Width="80px" ></asp:Label></ItemTemplate>
            <HeaderStyle Width="80px" />
            </asp:TemplateField>
           <asp:TemplateField HeaderText="Bank Date" >
            <ItemTemplate>
            <asp:Label ID="lblBankDate" runat="server" Text='<%# Eval("Bank Date","{0: dd-MM-yyyy}") %>'></asp:Label></ItemTemplate>
            <HeaderStyle Width="80px" /> <ItemStyle Width="80px" />
            </asp:TemplateField>
      
             <asp:TemplateField HeaderText="BuyerName" Visible="true" >
            <ItemTemplate>
            <asp:Label ID="lblBuyerName" runat="server" Text='<%# Eval("BuyerName") %>' Width="150px" ></asp:Label></ItemTemplate>
            <HeaderStyle Width="150px" />
            </asp:TemplateField>

             <asp:TemplateField HeaderText="Amount Paid" Visible="true" >
            <ItemTemplate>
            <asp:Label ID="lblAmountPaid" runat="server" Text='<%# Eval("AmountPaid") %>' Width="120px" ></asp:Label></ItemTemplate>
            <HeaderStyle Width="120px" />
            </asp:TemplateField>
        </Columns>
        <sortedascendingcellstyle backcolor="#F1F1F1" />
        <sortedascendingheaderstyle backcolor="#0000A9" />
        <sorteddescendingcellstyle backcolor="#CAC9C9" />
        <sorteddescendingheaderstyle backcolor="#000065" />
    </asp:GridView>

    </div>
    </asp:panel>

     <asp:panel ID="pnlNotOkBankReceipt" runat="server" Height="297px" Width="1153px" CssClass="XSmall" >
    <table><tr>
    <td> 
    <asp:Label ID="Label3" runat="server" Text ="Grid Showing Data Status ... If in Red Some Rows Status may not be OK "
        Font-Bold="True"  ForeColor="Red"></asp:Label> </td></tr></table>

     <div style="overflow:auto; height:259px; width:1145px" 
           title = "OK Data in UpLoad">
       <asp:GridView ID="grdNotOkBankReceipt" runat="server" CellPadding="3" 
             ForeColor="Red" Width="1092px"  AutoGenerateColumns="False" Height="37px" 
             GridLines="Vertical" BackColor="White" BorderColor="#999999"  
                BorderStyle="None" BorderWidth="1px" >
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />     <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" /> <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White"      HorizontalAlign="Left" />
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>

                <asp:TemplateField HeaderText="#" Visible="true" >
            <ItemTemplate>
            <asp:Label ID="lblRowNumber" runat="server" Text='<%# Eval("RKount") %>' Width="20px" ></asp:Label></ItemTemplate>
            <HeaderStyle Width="20px" />
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Company Name" Visible="true" >
            <ItemTemplate>
            <asp:Label ID="lblCompanyName" runat="server" Text='<%# Eval("CompanyName") %>' Width="80px" ></asp:Label></ItemTemplate>
            <HeaderStyle Width="80px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Bank Code" Visible="true" >
            <ItemTemplate>
            <asp:Label ID="lblBankCode" runat="server" Text='<%# Eval("BankCode") %>' Width="50px" ></asp:Label></ItemTemplate>
            <HeaderStyle Width="50px" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="AccountNumber" Visible="true" >
            <ItemTemplate>
            <asp:Label ID="lblAccountNumber" runat="server" Text='<%# Eval("AccountNumber") %>' Width="50px" ></asp:Label></ItemTemplate>
            <HeaderStyle Width="50px" />
            </asp:TemplateField>

             <asp:TemplateField HeaderText="Currency" Visible="true" >
            <ItemTemplate>
            <asp:Label ID="lblCurrency" runat="server" Text='<%# Eval("Currency") %>' Width="50px" ></asp:Label></ItemTemplate>
            <HeaderStyle Width="50px" />
            </asp:TemplateField>


           <asp:TemplateField HeaderText="Transaction Date" >
            <ItemTemplate>
            <asp:Label ID="lblTransactionDate" runat="server" Text='<%# Eval("TransactionDate","{0: dd-MM-yyyy}") %>'></asp:Label></ItemTemplate>
            <HeaderStyle Width="80px" /> <ItemStyle Width="80px" />
            </asp:TemplateField>

             <asp:TemplateField HeaderText="TransactionAmount" Visible="true" >
            <ItemTemplate>
            <asp:Label ID="lblTransactionAmount" runat="server" Text='<%# Eval("TransactionAmount") %>' Width="120px" ></asp:Label></ItemTemplate>
            <HeaderStyle Width="120px" />
            </asp:TemplateField>

             <asp:TemplateField HeaderText="Status" Visible="true" >
            <ItemTemplate>
            <asp:Label ID="lblResult" runat="server" Text='<%# Eval("Result") %>'></asp:Label></ItemTemplate>
            <HeaderStyle Width="350px" /> <ItemStyle Width="350px" />
            </asp:TemplateField>


        </Columns>
        <sortedascendingcellstyle backcolor="#F1F1F1" />
        <sortedascendingheaderstyle backcolor="#0000A9" />
        <sorteddescendingcellstyle backcolor="#CAC9C9" />
        <sorteddescendingheaderstyle backcolor="#000065" />
    </asp:GridView>
    </div>
    </asp:panel>
   <asp:panel ID="pnlBankReceiptStatus" runat="server" Height="364px" Width="947px" CssClass="XSmall">
        <div style="overflow:auto; height:352px; width:936px">
        <table><tr><td> <b style ="color: #33CC33"> Grid Showing all Rows in Uploaded Status</b></td></tr></table>
           <asp:GridView ID="grdBankReceiptStatus" runat="server" CellPadding="3" Font-Size="X-Small" 
                Width="904px"  AutoGenerateColumns="False" 
            Height="37px" GridLines="Vertical" BackColor="White" BorderColor="#999999"  
                BorderStyle="None" BorderWidth="1px" >
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />     <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" /> <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White"      HorizontalAlign="Left" />
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>

                 <asp:TemplateField HeaderText="#" Visible="true" >
            <ItemTemplate>
            <asp:Label ID="lblRowNumber" runat="server" Text='<%# Eval("RKount") %>' Width="20px" ></asp:Label></ItemTemplate>
            <HeaderStyle Width="20px" />
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Company Name" Visible="true" >
            <ItemTemplate>
            <asp:Label ID="lblCompanyName" runat="server" Text='<%# Eval("CompanyName") %>' Width="80px" ></asp:Label></ItemTemplate>
            <HeaderStyle Width="80px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Bank Code" Visible="true" >
            <ItemTemplate>
            <asp:Label ID="lblBankCode" runat="server" Text='<%# Eval("BankCode") %>' Width="50px" ></asp:Label></ItemTemplate>
            <HeaderStyle Width="50px" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="AccountNumber" Visible="true" >
            <ItemTemplate>
            <asp:Label ID="lblAccountNumber" runat="server" Text='<%# Eval("AccountNumber") %>' Width="50px" ></asp:Label></ItemTemplate>
            <HeaderStyle Width="50px" />
            </asp:TemplateField>

             <asp:TemplateField HeaderText="Currency" Visible="true" >
            <ItemTemplate>
            <asp:Label ID="lblCurrency" runat="server" Text='<%# Eval("Currency") %>' Width="50px" ></asp:Label></ItemTemplate>
            <HeaderStyle Width="50px" />
            </asp:TemplateField>


           <asp:TemplateField HeaderText="Transaction Date" >
            <ItemTemplate>
            <asp:Label ID="lblTransactionDate" runat="server" Text='<%# Eval("TransactionDate","{0: dd-MM-yyyy}") %>'></asp:Label></ItemTemplate>
            <HeaderStyle Width="80px" /> <ItemStyle Width="80px" />
            </asp:TemplateField>

             <asp:TemplateField HeaderText="TransactionAmount" Visible="true" >
            <ItemTemplate>
            <asp:Label ID="lblTransactionAmount" runat="server" Text='<%# Eval("TransactionAmount") %>' Width="120px" ></asp:Label></ItemTemplate>
            <HeaderStyle Width="120px" />
            </asp:TemplateField>

        </Columns>
        <sortedascendingcellstyle backcolor="#F1F1F1" />
        <sortedascendingheaderstyle backcolor="#0000A9" />
        <sorteddescendingcellstyle backcolor="#CAC9C9" />
        <sorteddescendingheaderstyle backcolor="#000065" />
    </asp:GridView>

    </div>
    </asp:panel>

</asp:Content>

