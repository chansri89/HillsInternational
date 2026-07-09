<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.Master" AutoEventWireup="true" CodeFile="CompanyMaster.aspx.cs" Inherits="CompanyMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <%-- <asp:Label ID = "lblSeparator" runat = "server" align = "center" Height="15px" 
        Width = "82px" ></asp:Label>--%>

  <h1 class="tis-page-title">
      <asp:Label ID="lblcompanymaster" runat="server" Text="Location Master" />
  </h1>
    <div class="tis-card">

    <asp:panel ID="Pnlgv" runat="server" ToolTip="Click Edit for Updating.." CssClass="XXSmall" >
        <div id="divCompany" runat="server" class="tis-table-wrap">
    <asp:GridView ID="GrdCompanyMaster" runat="server" CellPadding="3" 
            Width="1026px"  AutoGenerateColumns="False" 
            Height="16px" GridLines="Vertical" 
                onrowcancelingedit="GrdCompanyMaster_RowCancelingEdit" 
                onrowdeleting="GrdCompanyMaster_RowDeleting" 
                onrowediting="GrdCompanyMaster_RowEditing" BackColor="White" 
                BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
                onrowupdating="GrdCompanyMaster_RowUpdating" DataKeyNames="CompanyCode">
        <EditRowStyle Font-Size="XX-Small" />
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" 
            HorizontalAlign="Left" />
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
            <asp:TemplateField HeaderText="Location Code" >
            <ItemTemplate>
            <asp:Label ID="lblCompanyCode" runat="server" Width="80px" Text='<%# Eval("CompanyCode") %>'  ></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtCompanyCode" runat="server" Width="80px" Text='<%# Bind("CompanyCode") %>' ReadOnly="true"   Enabled="false"></asp:TextBox></EditItemTemplate>
                <HeaderStyle Width="80px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Location Name">
            <ItemTemplate>
            <asp:Label ID="lblCompanyName" runat="server" Width="150px" Text='<%# Eval("CompanyName") %>'  ></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtCompanyName" runat="server" Text='<%# Bind("CompanyName") %>'   Width="150px"></asp:TextBox></EditItemTemplate>
                <HeaderStyle />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Location Short Name">
             <ItemTemplate>
            <asp:Label ID="lblCompanyShortName" Width="60px" runat="server" Text='<%# Eval("CompanyShortName") %>'  ></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtCompanyShortName" Width="60px" runat="server" Text='<%# Bind("CompanyShortName") %>' MaxLength="8" ></asp:TextBox></EditItemTemplate>
                <HeaderStyle Width="60px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="LocationTypeId" Visible="false">
             <ItemTemplate>
            <asp:Label ID="lblLocationTypeId" Width="60px" runat="server" Text='<%# Eval("LocationTypeId") %>'  ></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtLocationTypeId" Width="60px" runat="server" Text='<%# Bind("LocationTypeId") %>'  ></asp:TextBox></EditItemTemplate>
                <HeaderStyle Width="60px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="EnterPrise Name">
             <ItemTemplate>
            <asp:Label ID="lblentName" Width="120px" runat="server" Text='<%# Eval("EnterpriseName") %>'  ></asp:Label></ItemTemplate>
             <EditItemTemplate>

             <asp:DropDownList ID="ddlEntName" Width="120px" runat="server" DataValueField="EnterpriseId" DataTextField="EnterpriseName"   DataSource='<%#getEnterprise() %>'
                  >
                 <asp:ListItem Text="--Select Pls--" Value="0" />
                 </asp:DropDownList> <%--added by SCS 270714 --%>
            </EditItemTemplate>
                <HeaderStyle Width="120px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Location Type">
             <ItemTemplate>
                                  
            <asp:Label ID="lblCompnyType" Width="60px" runat="server" Text='<%# Eval("CompanyFlag") %>'  ></asp:Label></ItemTemplate>
             <EditItemTemplate>

             <asp:DropDownList ID="ddlCompnyType" Width="60px" runat="server" DataValueField="CompanyTypeId" DataTextField="CompanyTypeName"   DataSource='<%#LoadCompanyType() %>'
                  >
                 <asp:ListItem Text="--Parent--" Value="0" />
                 </asp:DropDownList>
            </EditItemTemplate>
                <HeaderStyle Width="60px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Parent Company Name">
             <ItemTemplate>
            <asp:Label ID="lblParentCompanyName" runat="server" Text='<%# Eval("ParentCompanyName") %>'  ></asp:Label></ItemTemplate>
             <EditItemTemplate>

                 <asp:DropDownList ID="ddlParentCompanyName" runat="server" DataValueField="CompanyCode" DataTextField="CompanyName"   DataSource='<%#getParentCompanyName() %>'
                  >
                 <asp:ListItem Text="--Parent--" Value="0" />
                 </asp:DropDownList>
            </EditItemTemplate>
                <HeaderStyle Width="200px" />
            </asp:TemplateField>
            
          <asp:TemplateField HeaderText="State Short Name">
             <ItemTemplate>
            <asp:Label ID="lblStateShortName" runat="server" Text='<%# Eval("StateShortName") %>'  ></asp:Label></ItemTemplate>
             <EditItemTemplate>
                 <asp:DropDownList ID="ddlStateShortName" runat="server" DataValueField="StateId" DataTextField="StateShortName"   DataSource='<%#getState() %>'
                  >
                
                 </asp:DropDownList>
            </EditItemTemplate>
              <HeaderStyle Width="60px" />
              <ItemStyle Width="60px" />
            </asp:TemplateField>
            
             <asp:TemplateField HeaderText="IsActive">
             <ItemTemplate>
              <asp:CheckBox ID="chkActive" runat="server" Checked='<%# Eval("IsActive") %>'
             Enabled="false"></asp:CheckBox>
             </ItemTemplate>
             <EditItemTemplate>
           <asp:CheckBox ID="chkIsActive" runat="server" Checked='<%# Bind("IsActive") %>'
            ></asp:CheckBox>
           </EditItemTemplate>
                <HeaderStyle Width="40px" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            
            <asp:CommandField HeaderText="Edit" ShowEditButton="True" />
            <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" >
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
    <br />
    <asp:panel ID="pnlAdd" runat="server" CssClass="XSmall" GroupingText="Add / Edit Location">
    <div class="tis-form-grid">
        <div class="tis-field">
            <asp:Label ID="lblCompanyCode" runat="server" Text="Location Code" AssociatedControlID="txtCompanyCode" CssClass="tis-label" />
            <asp:TextBox ID="txtCompanyCode" runat="server" />
        </div>
        <div class="tis-field">
            <asp:Label ID="lblCompanyName" runat="server" Text="Location Name" AssociatedControlID="txtCompanyName" CssClass="tis-label" />
            <asp:TextBox ID="txtCompanyName" runat="server" />
        </div>
        <div class="tis-field">
            <asp:Label ID="lblCompanyShortName" runat="server" Text="Location Short Name" AssociatedControlID="txtCompanyShortName" CssClass="tis-label" />
            <asp:TextBox ID="txtCompanyShortName" runat="server" MaxLength="8" />
        </div>
        <div class="tis-field">
            <asp:Label ID="Label1" runat="server" Text="EnterPrise Name" AssociatedControlID="ddlEnterpriseName" CssClass="tis-label" />
            <asp:DropDownList ID="ddlEnterpriseName" runat="server" DataValueField="EnterPriseId" DataTextField="EnterPriseShortName" />
        </div>
        <div class="tis-field">
            <asp:Label ID="lblCompanyType" runat="server" Text="Location Type" AssociatedControlID="ddlCompanyType" CssClass="tis-label" />
            <asp:DropDownList ID="ddlCompanyType" runat="server" DataValueField="CompanyTypeId" DataTextField="CompanyTypeName" />
        </div>
        <div class="tis-field">
            <asp:Label ID="lblParentCompanyName" runat="server" Text="Parent Company Name" AssociatedControlID="ddlParentCompanyName" CssClass="tis-label" />
            <asp:DropDownList ID="ddlParentCompanyName" runat="server" />
        </div>
        <div class="tis-field">
            <asp:Label ID="lblStateShortName" runat="server" Text="State Short Name" AssociatedControlID="ddlStateShortName" CssClass="tis-label" />
            <asp:DropDownList ID="ddlStateShortName" runat="server" />
        </div>
        <div class="tis-field">
            <span class="tis-label">&nbsp;</span>
            <asp:CheckBox ID="chkIsActive" runat="server" Text="IsActive" Visible="False" />
        </div>
    </div>
    <div class="tis-toolbar" style="margin-top:var(--tis-space-4);">
        <asp:Button ID="btnSave" runat="server" Text="Save" onclick="btnSave_Click" />
    </div>
    </asp:panel>
 
    </div>
</asp:Content>

