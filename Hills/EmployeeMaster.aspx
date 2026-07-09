<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.Master" AutoEventWireup="true" CodeFile="EmployeeMaster.aspx.cs" Inherits="EmployeeMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID = "lblSeparator" runat = "server" Height="15px" Width = "901px" ></asp:Label>
<asp:Label ID="lblempoyeemaster" runat="server"  Text="Employee Master" CssClass="XSmall"
        width="787px" Font-Size ="12pt" Font-Bold="True"  style="text-align: center"></asp:Label>
<asp:Label ID = "lblSeparator1" runat = "server"  Height="15px" Width = "901px" ></asp:Label>
   
    <asp:panel ID="Pnlgv" runat="server" Height="310px" Width="928px" 
        CssClass="XXSmall">
        <div style="overflow:auto; height:297px; width:908px">
    <asp:GridView ID="GrdEmployeeMaster" runat="server" CellPadding="3" 
             Width="883px" AutoGenerateColumns="False"   Height="37px" GridLines="Vertical" BackColor="White" BorderColor="#999999"  
                BorderStyle="None" BorderWidth="1px" 
                onrowcancelingedit="GrdEmployeeMaster_RowCancelingEdit" 
                onrowdeleting="GrdEmployeeMaster_RowDeleting" 
                onrowediting="GrdEmployeeMaster_RowEditing" 
                onrowupdating="GrdEmployeeMaster_RowUpdating">
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" 
            HorizontalAlign="Left" />
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
            <asp:TemplateField HeaderText="Employee Code" >
            <ItemTemplate>
            <asp:Label ID="lblEmployeeCode" runat="server" Text='<%# Eval("EmployeeCode") %>' Width="30px" ></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtEmployeeCode" runat="server" Text='<%# Bind("EmployeeCode") %>' ReadOnly="true"  Width="60px"  ></asp:TextBox></EditItemTemplate>
                <HeaderStyle Width="60px" />
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Employee Name" >
            <ItemTemplate>
            <asp:Label ID="lblEmployeeName" runat="server" Text='<%# Eval("EmployeeName") %>'></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtEmpName" runat="server" Text='<%# Bind("EmployeeName") %>' ></asp:TextBox></EditItemTemplate>
                <HeaderStyle Width="80px" />
            </asp:TemplateField>
             <asp:TemplateField HeaderText="EmailId">
             <ItemTemplate>
            <asp:Label ID="lblEmailId" runat="server" Text='<%# Eval("EmailId") %>'></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtEmailId" runat="server" Text='<%# Bind("EmailId") %>' ></asp:TextBox></EditItemTemplate>
                 <HeaderStyle Width="100px" />
            </asp:TemplateField>
               <asp:TemplateField HeaderText="Company Name">
             <ItemTemplate>
            <asp:Label ID="lblCompanyName" runat="server" Text='<%# Eval("CompanyName") %>'></asp:Label></ItemTemplate>
             <EditItemTemplate>
                 <asp:DropDownList ID="ddlCompanyName" runat="server" DataValueField="CompanyCode" DataTextField="CompanyName"   DataSource='<%#getCompanyName() %>'
                 >
                 </asp:DropDownList>
            </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Manager Name">
             <ItemTemplate>
            <asp:Label ID="lblManagerName" runat="server" Text='<%# Eval("ManagerName") %>'></asp:Label></ItemTemplate>
             <EditItemTemplate>
                 <asp:DropDownList ID="ddlManagerName" runat="server" Width="60px" DataValueField="EmployeeCode" DataTextField="EmployeeName"   DataSource='<%#getManagerName() %>'
                 >
                
                 </asp:DropDownList>
            </EditItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Designation">
            <ItemTemplate>
            <asp:Label ID="lblEmployeeDesignation" runat="server" Text='<%# Eval("EmployeeDesignation") %>'></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtEmployeeDesignation" runat="server" Width="60px"  Text='<%# Bind("EmployeeDesignation") %>'>
            </asp:TextBox></EditItemTemplate>
                <HeaderStyle Width="60px" />
            </asp:TemplateField>
          
          <asp:TemplateField HeaderText="Is Auditor" Visible ="false">
             <ItemTemplate>
  
              <asp:CheckBox ID="chkAuditor" runat="server" Checked='<%# Eval("IsAuditor") %>'
             Enabled="false"></asp:CheckBox>
             </ItemTemplate>

             <EditItemTemplate>
           <asp:CheckBox ID="chkIsAuditor" runat="server" Checked='<%# Bind("IsAuditor") %>'
            ></asp:CheckBox>
           </EditItemTemplate>
                <HeaderStyle Width="30px" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
           
          <asp:TemplateField HeaderText="Is Company Admin">
             <ItemTemplate>
 
              <asp:CheckBox ID="chkCompanyAdmin" runat="server" Checked='<%# Eval("IsCompanyAdmin") %>'
             Enabled="false"></asp:CheckBox>
             </ItemTemplate>
             <EditItemTemplate>
           <asp:CheckBox ID="chkCompanyAdmin" runat="server" Width="30px" Checked='<%# Bind("IsCompanyAdmin") %>' 
            ></asp:CheckBox>
           </EditItemTemplate>
                <HeaderStyle Width="30px" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>


             <asp:TemplateField HeaderText="Is Active">
             <ItemTemplate>
 
              <asp:CheckBox ID="chkActive" runat="server" Checked='<%# Eval("IsActive") %>'
             Width="30px" Enabled="false"></asp:CheckBox>
             </ItemTemplate>

             <EditItemTemplate>
           <asp:CheckBox ID="chkIsActive" runat="server" Width="30px" Checked='<%# Bind("IsActive") %>'
            ></asp:CheckBox>
           </EditItemTemplate>
                <HeaderStyle Width="30px" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>

 
            
            <asp:CommandField HeaderText="Edit" ShowEditButton="True" 
                CausesValidation="False">
            <HeaderStyle Width="40px" />
            </asp:CommandField>
 
        </Columns>
        <sortedascendingcellstyle backcolor="#F1F1F1" />
        <sortedascendingheaderstyle backcolor="#0000A9" />
        <sorteddescendingcellstyle backcolor="#CAC9C9" />
        <sorteddescendingheaderstyle backcolor="#000065" />
    </asp:GridView>
    </div>
    </asp:panel>
    <asp:panel ID="pnlAdd" runat="server" Width="640px" Height="182px" 
        CssClass="XSmall" >
    <table style="width: 99%; height: 97%;">
            <tr>
                <td  style="width: 116px; text-align: left;">
                    <asp:Label ID="lblEmployeeCode" runat="server" Text="Employee Code"  
                         style="font-weight: bold"></asp:Label>
                </td>
                <td style="width: 125px">
                    <asp:TextBox ID="txtEmployeeCode" runat="server"  height="15px"
                        Width="96px"></asp:TextBox>
                </td>

            </tr>
             <tr>
                <td  style="width: 116px; text-align: left;">
                    <asp:Label ID="lblEmpname" runat="server" Text="Employee Name"  
                         Font-Bold="True"></asp:Label>
                </td>
                <td style="width: 125px">
                    <asp:TextBox ID="txtEmpname" runat="server"  height="15px"
                        Width="231px"></asp:TextBox>
                </td>

                  <td style="width: 118px; text-align: left;">
                    <asp:CheckBox ID="ChkIsCompanyAdmin" runat="server"  
                            Text="Company Admin" Visible="True" />
                </td>
                  <td  style="width: 80px; text-align: left;">
                    <asp:CheckBox ID="ChkIsAuditor" runat="server"  
                         Text="IsAuditor" Visible="false"  
                         oncheckedchanged="ChkIsAuditor_CheckedChanged" AutoPostBack="True"/>
                </td>
            </tr>
             
             <tr>
                <td  style="width: 116px; text-align: left;">
                    <asp:Label ID="lblEmailid" runat="server" Text="EmailId"  
                         style="font-weight: bold"></asp:Label>
                </td>
                <td style="width: 125px">
                    <asp:TextBox ID="txtEmailId" runat="server"  
                        Width="230px" height="15px"></asp:TextBox>
                </td>
                          
 
       <%--     </tr>
            <tr>--%>
                <td  style="width: 116px; text-align: left;">
                    <asp:Label ID="lblUserPassword" runat="server" Text="Password"  
                         Font-Bold="True"></asp:Label>
                </td>
                <td style="width: 125px">
                    <asp:TextBox ID="txtPassword" runat="server"  height="16px"
                        TextMode="Password" Width="146px"></asp:TextBox>
                </td>

            </tr>
            <tr>
                <td style="width: 116px; text-align: left;">
                    <asp:Label ID="lblCompanyName" runat="server" Text="Company Name"  
                         style="font-weight: 700"></asp:Label>
                </td>
                <td style="width: 125px">
                    <asp:DropDownList ID="ddlCompanyName" runat="server"  
                        Height="15px" Width="239px">
                    </asp:DropDownList>
                </td>
                
        <%--    </tr>
            <tr>--%>
                <td  style="width: 116px; text-align: left;">
                    <asp:Label ID="lblManagerName" runat="server" Text="Manager Name"  
                        ></asp:Label>
                </td>
                <td style="width: 125px">
                    <asp:DropDownList ID="ddlManagerName" runat="server"  Height="16px" 
                        Width="160px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td  style="width: 116px; text-align: left;">
                    <asp:Label ID="lblEmployeeDesignation" runat="server" 
                        Text="Designation"   
                        style="font-weight: 700"></asp:Label>
                </td>
                <td style="width: 125px">
                    <asp:TextBox ID="txtEmployeeDesignation" runat="server"  height="15px"
                        Width="228px"></asp:TextBox>
                </td>
           <%-- </tr>
           

            <tr>
             <td></td>--%>
                <td  style="width: 116px; text-align: left;">
                    <asp:CheckBox ID="chkIsActive" runat="server"  
                         Text="IsActive  " Visible="False" />
                </td>
                <td>
                        <asp:Button ID="Button1" runat="server"  Text="Save" 
                        onclick="btnSave_Click" style="height: 20px" />
                </td>

                 

              <%--  <td style="width: 125px">
                    <asp:Button ID="btnSave" runat="server"  Text="Save" 
                        onclick="btnSave_Click" style="height: 20px" />
                </td>--%>
           </tr>
           <tr><td></td>
                <td style="width: 116px" > <asp:HiddenField ID="HidDeleteCount" Value="0" runat="server"  />
                
                </td><td></td>
                <td class="style23" style="width: 80px"> <asp:HiddenField ID="HidUpdateCount" Value="0" runat="server" /> </td>
            </tr>
            
        </table>
    </asp:panel>
    
  <%--  </div>--%>
</asp:Content>

