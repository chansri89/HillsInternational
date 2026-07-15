<%@ page title="" language="C#" masterpagefile="~/MasterPage1.Master" autoeventwireup="true" inherits="AssignProgramtoRoles, App_Web_sx5sst5a" %>
<script runat="server">

    void ShowAllCreate_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox Create = (CheckBox)sender;
        if (Create.Checked)
        {
            ShowCreateRows(true);
        }
        else
        {
            ShowCreateRows(false);
        }
        
    }
    void ShowCreateRows(bool show)
    {
       // GrdAssignProgam.Visible = false;
        foreach (GridViewRow row in GrdAssignProgam.Rows)
        {
            CheckBox chkCreate = (CheckBox)row.FindControl("chkCreate");
            chkCreate.Checked = show;
        }

  }
    void ShowAllAccess_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox Access = (CheckBox)sender;
        if (Access.Checked)
        {
            ShowAccessRows(true);
        }
        else
        {
            ShowAccessRows(false);
        }

    }
    void ShowAccessRows(bool show)
    {
        // GrdAssignProgam.Visible = false;
        foreach (GridViewRow row in GrdAssignProgam.Rows)
        {
            CheckBox chkAccess = (CheckBox)row.FindControl("chkAccess");
            chkAccess.Checked = show;
        }

    }
    void ShowAllEdit_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox Edit = (CheckBox)sender;
        if (Edit.Checked)
        {
            ShowEditRows(true);
        }
        else
        {
            ShowEditRows(false);
        }

    }
    void ShowEditRows(bool show)
    {
        // GrdAssignProgam.Visible = false;
        foreach (GridViewRow row in GrdAssignProgam.Rows)
        {
            CheckBox chkEdit = (CheckBox)row.FindControl("chkEdit");
            chkEdit.Checked = show;
        }

    }
    void ShowAllDelete_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox Delete = (CheckBox)sender;
        if (Delete.Checked)
        {
            ShowDeleteRows(true);
        }
        else
        {
            ShowDeleteRows(false);
        }

    }
    void ShowDeleteRows(bool show)
    {
        // GrdAssignProgam.Visible = false;
        foreach (GridViewRow row in GrdAssignProgam.Rows)
        {
            CheckBox chkDelete = (CheckBox)row.FindControl("chkDelete");
            chkDelete.Checked = show;
        }

    }

    void GrdAssignProgam_Load(object sender, EventArgs e)
    {
        if (rdbtnRole.SelectedIndex == 0)
        {
            
        }
        if (rdbtnRole.SelectedIndex == 1)
        {
            GridView gvr = (GridView)sender;
        }
    }
</script>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h1 class="tis-page-title">
    <asp:Label ID="lblassignprogramtoroles" runat="server" Text="Assign Program To Roles" />
</h1>

    <div class="tis-card">
    <asp:Panel ID="pnlRoleDetails" runat="server" Height="38px" CssClass="XSmall">
        <table>
            <tr>
                <td colspan="20" align="right">
                        <asp:RadioButtonList ID="rdbtnRole" runat="server" RepeatDirection="Horizontal"
                            Width="242px" 
                            OnSelectedIndexChanged="rdbtnRole_SelectedIndexChanged" AutoPostBack="True" 
                            Font-Bold="False">
                            <asp:ListItem Selected="True" Value="1" Text="New Role"/>
                            <asp:ListItem  Value="2" Text="Update Role"/>
                        </asp:RadioButtonList>
                </td>
              
                <td id="tdRoleName" colspan="20" align ="right" style="height: 41px" runat="server" visible="true">
                    <asp:Label ID="lblRoleName" runat="server" Text="Role Name" Width="76px" 
                    Font-Bold="True"></asp:Label>
                </td>
                <td id="tdRoleFields" colspan="20" style="height: 41px" runat="server" visible="true">
                    <asp:TextBox ID="txtNewRoleName" runat="server"  
                        Width="235px" Visible="true" MaxLength="20"></asp:TextBox>
                    <asp:DropDownList ID="ddlRoleName" runat="server" Width="215px" 
                        Visible="False" AutoPostBack="True" 
                        DataTextField="RoleName" DataValueField="RoleId" 
                        OnSelectedIndexChanged="ddlRoleName_SelectedIndexChanged" Height="19px">
                    </asp:DropDownList>
                </td>
            <td >
                   <asp:Label ID="lblMainmenu" runat="server" Text="MainMenu" Width="59px" 
                             Font-Bold="True"      ForeColor="Black"></asp:Label></td>
                  <td class="style75"  >
                   <asp:DropDownList ID="ddlMainMenu" runat="server" Width="113px" 
                         DataTextField="MainMenu" DataValueField="MainMenu" Height="22px" AutoPostBack = "true"
                   OnSelectedIndexChanged="ddlMainMenu_SelectedIndexChanged">
            </asp:DropDownList></td>
            <td><asp:TextBox ID="txtMainMenu" runat="server"  Width="10px" Visible="false" ></asp:TextBox></td>
           </tr>
       </table>
    </asp:Panel>
    <br />
    <asp:panel ID="Pnlgv" runat="server" CssClass="XXSmall">
        <div class="tis-table-wrap">
    <asp:GridView ID="GrdAssignProgam" runat="server" CellPadding="3" 
            Width="599px"  AutoGenerateColumns="False" Height="126px" GridLines="Vertical" 
             onrowcancelingedit="GrdAssignProgam_RowCancelingEdit" 
                onrowdeleting="GrdAssignProgam_RowDeleting" 
                onrowediting="GrdAssignProgam_RowEditing" BackColor="White" 
                BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
                onrowupdating="GrdAssignProgam_RowUpdating"
                DataKeyNames="ProgramId" onrowcommand="GrdAssignProgam_RowCommand" 
                onrowdatabound="GrdAssignProgam_RowDataBound">
        <EditRowStyle Font-Size="X-Small" />
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" 
            HorizontalAlign="Left" />
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
             <asp:TemplateField HeaderText="ProgramId" Visible="false">
            <ItemTemplate>
            <asp:Label ID="lblProgramId" runat="server" Text='<%# Eval("ProgramId") %>'  ></asp:Label></ItemTemplate>
             <HeaderStyle Width="100px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ProgramName" >
            <ItemTemplate>
            <asp:Label ID="lblProgramName" runat="server" Text='<%# Eval("ProgramName") %>'   Width="300px"></asp:Label></ItemTemplate>
            <EditItemTemplate>
            <asp:Label ID="lblProgramName" runat="server" Text='<%# Bind("ProgramName") %>'   Width="300px"></asp:Label></ItemTemplate>
            </EditItemTemplate>
             <HeaderStyle Width="300px" />
                <ItemStyle Width="300px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Access" >
             <HeaderTemplate>
            <asp:checkbox id="ShowAllAccess" text="Access" checked="false" autopostback="true" runat="server" OnCheckedChanged="ShowAllAccess_CheckedChanged" />
            <asp:Label ID="lblAccess" Text="Access" runat="server" Visible="false"></asp:Label>
            </HeaderTemplate>
            <ItemTemplate>
            <asp:CheckBox ID="chkAccess" runat="server" Checked='<%# Eval("CanAccess") %>'  ></asp:CheckBox></ItemTemplate>
             <EditItemTemplate>
            <asp:CheckBox ID="chkEditAccess" runat="server" Checked='<%# Bind("CanAccess") %>'  ></asp:CheckBox></ItemTemplate>
            </EditItemTemplate>
             <HeaderStyle Width="5px" HorizontalAlign="Center" VerticalAlign="Middle" />
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Create">
            <HeaderTemplate>
            <asp:checkbox id="ShowAllCreate" text="Create" checked="false" autopostback="true" runat="server" OnCheckedChanged="ShowAllCreate_CheckedChanged" />
             <asp:Label ID="lblCreate" Text="Create" runat="server" Visible="false"></asp:Label>
            </HeaderTemplate>
             <ItemTemplate>
              <asp:CheckBox ID="chkCreate" runat="server" Checked='<%# Eval("CanCreate") %>'  ></asp:CheckBox></ItemTemplate>
              <EditItemTemplate>
            <asp:CheckBox ID="chkEditCreate" runat="server" Checked='<%# Bind("CanCreate") %>'  ></asp:CheckBox></ItemTemplate>
            </EditItemTemplate>
             <HeaderStyle Width="10px" HorizontalAlign="Center" VerticalAlign="Middle"  />
             <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Edit">
             <HeaderTemplate>
            <asp:checkbox id="ShowAllEdit" text="Edit" checked="false" autopostback="true" runat="server" OnCheckedChanged="ShowAllEdit_CheckedChanged" />
             <asp:Label ID="lblEdit" Text="Edit" runat="server" Visible="false"></asp:Label>
            </HeaderTemplate>
             <ItemTemplate>
              <asp:CheckBox ID="chkEdit" runat="server" Checked='<%# Eval("CanEdit") %>'  ></asp:CheckBox></ItemTemplate>
              <EditItemTemplate>
            <asp:CheckBox ID="chkModEdit" runat="server" Checked='<%# Bind("CanEdit") %>'  ></asp:CheckBox></ItemTemplate>
            </EditItemTemplate>
                <HeaderStyle Width="10px" HorizontalAlign="Center" VerticalAlign="Middle"  />
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Delete">
             <HeaderTemplate>
            <asp:checkbox id="ShowAllDelete" text="Delete" checked="false" autopostback="true" runat="server" OnCheckedChanged="ShowAllDelete_CheckedChanged" />
            <asp:Label ID="lblDelete" Text="Delete" runat="server" Visible="false"></asp:Label>
            </HeaderTemplate>
             <ItemTemplate>
              <asp:CheckBox ID="chkDelete" runat="server" Checked='<%# Eval("CanDelete") %>'  ></asp:CheckBox></ItemTemplate>
              <EditItemTemplate>
            <asp:CheckBox ID="chkEditDelete" runat="server" Checked='<%# Bind("CanDelete") %>'  ></asp:CheckBox></ItemTemplate>
            </EditItemTemplate>
                <HeaderStyle Width="10px" HorizontalAlign="Center" VerticalAlign="Middle"  />
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:TemplateField>
          <asp:TemplateField HeaderText="View" Visible="false">
             <ItemTemplate>
               <asp:CheckBox ID="chkPrint" runat="server" Checked='<%# Eval("CanPrint") %>'  ></asp:CheckBox></ItemTemplate>
               <EditItemTemplate>
            <asp:CheckBox ID="chkEditPrint" runat="server" Checked='<%# Bind("CanPrint") %>'  ></asp:CheckBox></ItemTemplate>
            </EditItemTemplate>
              <HeaderStyle Width="10px" HorizontalAlign="Center" VerticalAlign="Middle"  />
              <ItemStyle  HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:TemplateField>
            <asp:ButtonField ButtonType="Link" Text="Select All" HeaderText="Select All" 
                 CommandName="SelectAll" ControlStyle-Width="50px" CausesValidation="True">
            <ControlStyle Width="50px" />
            <ItemStyle Width="60px" />
            </asp:ButtonField>
             <asp:CommandField HeaderText="Edit" ShowEditButton="True" />
           
        </Columns>
        <sortedascendingcellstyle backcolor="#F1F1F1" />
        <sortedascendingheaderstyle backcolor="#0000A9" />
        <sorteddescendingcellstyle backcolor="#CAC9C9" />
        <sorteddescendingheaderstyle backcolor="#000065" />
    </asp:GridView>
    </div>
    </asp:panel>
        <asp:Panel ID="pnlSave" runat="server" CssClass="XSmall" Width="728px">
            <table style="width: 670px">
                <tr>
                    <td style="width: 216px; text-align: right">
                        <asp:Button ID="btnSave" Text="Save" runat="server" onclick="btnSave_Click" 
                            style="margin-left: 0px; text-align: right;" />
                    </td>
                    <td> <asp:HiddenField ID="HidDeleteCount" Value="0" runat="server" />
                    </td>
                    <td>
                <asp:HiddenField ID="HidUpdateCount" Value="0" runat="server" />
                </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
</asp:Content>

