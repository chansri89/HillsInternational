<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.Master" AutoEventWireup="true" CodeFile="LoginUser.aspx.cs" Inherits="LoginUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="tis-card">
<h1 class="tis-page-title">
    <asp:Label ID="lblempoyeemaster" runat="server" Text="Login Employee Master" />
</h1>

    <asp:panel ID="Pnlgv" runat="server" CssClass="XXSmall" GroupingText="User Grid">
        <div class="tis-table-wrap">
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
    <asp:panel ID="pnlAdd" runat="server" GroupingText="Add User" CssClass="XXSmall">
    <div class="tis-form-grid">
        <div class="tis-field">
            <asp:Label ID="lblUserName" runat="server" Text="User Name" AssociatedControlID="txtUserName" CssClass="tis-label" />
            <asp:TextBox ID="txtUserName" runat="server" />
        </div>
        <div class="tis-field">
            <asp:Label ID="lblCompanyName" runat="server" Text="Company Name" AssociatedControlID="ddlCompanyName" CssClass="tis-label" />
            <asp:DropDownList ID="ddlCompanyName" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCompanyChanged" />
        </div>
        <div class="tis-field">
            <asp:Label ID="lblDepartmentName" runat="server" Text="Department Name" AssociatedControlID="ddlDepartName" CssClass="tis-label" />
            <asp:DropDownList ID="ddlDepartName" runat="server" />
        </div>
        <div class="tis-field">
            <asp:Label ID="lblUserPassword" runat="server" Text="Password" AssociatedControlID="txtUserPassword" CssClass="tis-label" />
            <asp:TextBox ID="txtUserPassword" runat="server" TextMode="Password" />
        </div>
        <div class="tis-field">
            <asp:Label ID="lblEmailid" runat="server" Text="EmailId" AssociatedControlID="txtEmailId" CssClass="tis-label" />
            <asp:TextBox ID="txtEmailId" runat="server" />
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                ControlToValidate="txtEmailId" ErrorMessage="Enter EmailId in Correct Format"
                ForeColor="#FF3300" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
        </div>
        <div class="tis-field">
            <span class="tis-label">Options</span>
            <div style="display:flex; flex-wrap:wrap; gap:var(--tis-space-4); align-items:center;">
                <asp:CheckBox ID="ChkIsCompanyAdmin" runat="server" Text="Admin" Visible="True" />
                <asp:CheckBox ID="ChkIssuperUser" runat="server" Text="Super User" Visible="True" />
                <asp:CheckBox ID="chkIsActive" runat="server" Text="IsActive" Visible="False" />
            </div>
        </div>
    </div>
    <asp:TextBox ID="txtLoginUserId" runat="server" Visible="false" />
    <div class="tis-toolbar" style="margin-top:var(--tis-space-4);">
        <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" Text="Save" />
    </div>
    </asp:panel>
</div>
</asp:Content>

