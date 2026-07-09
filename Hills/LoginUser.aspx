<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.Master" AutoEventWireup="true" CodeFile="LoginUser.aspx.cs" Inherits="LoginUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%--<asp:Label ID = "lblSeparator" runat = "server" Height="15px" Width = "901px" ></asp:Label>--%>
<asp:Label ID="lblempoyeemaster" runat="server"  Text="Login Employee Master" CssClass="XSmall"
         width="787px"    Font-Size ="12pt" Font-Bold="True"      style="text-align: center"></asp:Label>
<%--<asp:Label ID = "lblSeparator1" runat = "server"  Height="15px" Width = "901px" ></asp:Label>--%>
 <%--<div style="overflow:auto; height: 766px;">--%>
   
    <asp:panel ID="Pnlgv" runat="server" Height="351px" Width="1002px" CssClass="XXSmall"
        GroupingText="User Grid" >
        <div style="overflow:auto; height:330px; width:982px">
    <asp:GridView ID="GrdEmployeeMaster" runat="server" CellPadding="3" 
           Width="954px" AutoGenerateColumns="False"    Height="37px" GridLines="Vertical" BackColor="White" BorderColor="#999999"  
                BorderStyle="None" BorderWidth="1px" OnRowCommand="GrdLoginUser_RowCommand"
               >
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" 
            HorizontalAlign="Left" />
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
            <asp:TemplateField HeaderText="Login UserId" Visible="true" >
            <ItemTemplate>
            <asp:Label ID="lblLoginUserId" runat="server" Text='<%# Eval("LoginUserId") %>' Width="40px" ></asp:Label></ItemTemplate>
             <HeaderStyle Width="40px" />
            </asp:TemplateField>

             <asp:TemplateField HeaderText ="User Name" >
                <ItemTemplate>    
            <asp:LinkButton ID="lnkLoginUserId" Width="140px" runat ="server"  CommandArgument='<%#Eval("LoginUserId")%>'
             CommandName ="selectUserName" Text ='<%#Eval("UserName") %>'></asp:LinkButton>
              </ItemTemplate> <HeaderStyle Width="140px" /> <ItemStyle HorizontalAlign = "Right" />
            </asp:TemplateField>

               <asp:TemplateField HeaderText="Company Name">
             <ItemTemplate>
            <asp:Label ID="lblCompanyName" runat="server" width = "150px" Text='<%# Eval("CompanyName") %>'></asp:Label></ItemTemplate>
 
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Department Name">
             <ItemTemplate>
            <asp:Label ID="lblDepartmentName" runat="server" width = "120px" Text='<%# Eval("DepartmentName") %>'></asp:Label></ItemTemplate>
 
            </asp:TemplateField>
   
             <asp:TemplateField HeaderText="EmailId">
             <ItemTemplate>
            <asp:Label ID="lblEmailId" runat="server" Width="150px"  Text='<%# Eval("EmailId") %>'></asp:Label></ItemTemplate>
                 <HeaderStyle Width="150px" />
            </asp:TemplateField>

      
          <asp:TemplateField HeaderText="Admin" Visible ="true">
             <ItemTemplate>
              <asp:CheckBox ID="chkAdmin" runat="server" Checked='<%# Eval("IsAdmin") %>'
            Enabled="false"></asp:CheckBox>
             </ItemTemplate>
 
                <HeaderStyle Width="30px" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
           
          <asp:TemplateField HeaderText="Super User">
             <ItemTemplate>
              <asp:CheckBox ID="chklblSuperUser" runat="server" Checked='<%# Eval("IsSuperUser") %>'
            Enabled="false"></asp:CheckBox>
             </ItemTemplate>

                <HeaderStyle Width="30px" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>


             <asp:TemplateField HeaderText="Is Active">
             <ItemTemplate>
              <asp:CheckBox ID="chkActive" runat="server" Checked='<%# Eval("IsActive") %>'
            Width="30px" Enabled="false"></asp:CheckBox>
             </ItemTemplate>

                <HeaderStyle Width="30px" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
           

        </Columns>
        <sortedascendingcellstyle backcolor="#F1F1F1" />
        <sortedascendingheaderstyle backcolor="#0000A9" />
        <sorteddescendingcellstyle backcolor="#CAC9C9" />
        <sorteddescendingheaderstyle backcolor="#000065" />
    </asp:GridView>
    </div>
    </asp:panel>
    <asp:panel ID="pnlAdd" runat="server" Width="1008px" Height="86px" GroupingText="Add User" CssClass="XXSmall">
    <table style="width: 99%; height: 26%;">
            <tr>
                <td class="style1" style="width: 77px; text-align: left; height: 18px;">
                    <asp:Label ID="lblUserName" runat="server" Text="User Name"  
                        style="font-weight: bold"></asp:Label>
                </td>
                   <td style="width: 99px; height: 18px;">
                    <asp:TextBox ID="txtUserName" runat="server" Width="141px" height="20px"></asp:TextBox>
                </td>
                <td class="style1" style="width: 77px; text-align: left; height: 24px;">
                    <asp:Label ID="lblCompanyName" runat="server" Text="Company Name"  
                        style="font-weight: 700"></asp:Label>
                </td>
                <td style="width: 204px; height: 24px;">
                    <asp:DropDownList ID="ddlCompanyName" runat="server"  Height="17px" Width="178px" Font-Size="XX-Small" AutoPostBack = "true" 
                        OnSelectedIndexChanged ="ddlCompanyChanged">
                    </asp:DropDownList>
                </td>
                
         
                <td class="style1" style="width: 78px; text-align: left; height: 24px;">
                    <asp:Label ID="lblDepartmentName" runat="server" Text="Department Name"  
                       ></asp:Label>
                </td>
                <td style="width: 99px; height: 24px;">
                    <asp:DropDownList ID="ddlDepartName" runat="server"  Height="19px" Width="112px" Font-Size="XX-Small">
                    </asp:DropDownList>
                </td>
     
                  <td class="style1" style="width: 10px; text-align: left;">
              <asp:TextBox ID="txtLoginUserId" runat="server"  Visible="false"
                      Width="27px" height="20px"></asp:TextBox>
               </td>
             
            </tr>
                        <tr>
                
                <td class="style1" style="width: 78px; text-align: left; height: 18px;">
                    <asp:Label ID="lblUserPassword" runat="server" Text="Password"  
                        Font-Bold="True"></asp:Label>
                </td>
                <td style="width: 99px; height: 18px;">
                    <asp:TextBox ID="txtUserPassword" runat="server"  height="20px"
                        TextMode="Password" width="149px"></asp:TextBox>
                </td> 
                
                <td style="text-align: left; height: 24px; width: 68px;">
                    <asp:CheckBox ID="ChkIsCompanyAdmin" runat="server"  Width="62px"
                             Text="Admin" Visible="True" />
                </td>
                    <td style="text-align: left; height: 44px; width: 204px;">
                    <asp:CheckBox ID="ChkIssuperUser" runat="server" Width="94px"
                           Text="Super User" Visible="True" />

                      <asp:CheckBox ID="chkIsActive" runat="server"  width = "80px"
                    Text="IsActive  " Visible="False" />

                      
                </td>
       <td class="style1" style="width: 68px; text-align: left; height: 18px;">
                    <asp:Label ID="lblEmailid" runat="server" Text="EmailId"  
                        style="font-weight: bold"></asp:Label>
                </td>
                <td style="width: 125px; height: 18px;">
                    <asp:TextBox ID="txtEmailId" runat="server"  Width="204px" height="20px"></asp:TextBox> </td>
                   
                 
                 <td>
                       <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" style="height: 20px" Text="Save" />
                 </td>
     
   <td class="style55" style="width: 194px; height: 18px;">
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                        ControlToValidate="txtEmailId" ErrorMessage="Enter EmailId in Correct Format" 
                         ForeColor="#FF3300"   ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                         </td>
 </tr>
    
            
        </table>
    </asp:panel>
    
  <%--  </div>--%>
</asp:Content>

