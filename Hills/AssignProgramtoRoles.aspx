<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.Master" AutoEventWireup="true" CodeFile="AssignProgramtoRoles.aspx.cs" Inherits="AssignProgramtoRoles" %>
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
<div class="tis-page" data-density="compact">
    <div class="tis-page-header">
        <div class="tis-page-header__text">
            <span class="tis-eyebrow">Admin</span>
            <h1 class="tis-page-header__title"><asp:Label ID="lblassignprogramtoroles" runat="server" Text="Assign programs to roles" /></h1>
            <p class="tis-page-header__desc">Create a role with its screen permissions, or pick an existing role and edit its permissions one program at a time.</p>
        </div>
    </div>

    <asp:Panel ID="pnlRoleDetails" runat="server" CssClass="tis-card">
        <div class="tis-card__body">
            <div class="tis-toolbar">
                <div class="tis-field">
                    <span class="tis-label">Mode</span>
                    <asp:RadioButtonList ID="rdbtnRole" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal" CssClass="tis-segmented"
                        OnSelectedIndexChanged="rdbtnRole_SelectedIndexChanged" AutoPostBack="True">
                        <asp:ListItem Selected="True" Value="1" Text="New role" />
                        <asp:ListItem Value="2" Text="Update role" />
                    </asp:RadioButtonList>
                </div>
                <div class="tis-field tis-toolbar__grow">
                    <div id="tdRoleName" runat="server" visible="true">
                        <asp:Label ID="lblRoleName" runat="server" Text="Role name" AssociatedControlID="txtNewRoleName" CssClass="tis-label" />
                    </div>
                    <div id="tdRoleFields" runat="server" visible="true">
                        <asp:TextBox ID="txtNewRoleName" runat="server" Visible="true" MaxLength="20" placeholder="e.g. Estimator" />
                        <asp:DropDownList ID="ddlRoleName" runat="server" Visible="False" AutoPostBack="True"
                            DataTextField="RoleName" DataValueField="RoleId"
                            OnSelectedIndexChanged="ddlRoleName_SelectedIndexChanged" />
                    </div>
                </div>
                <div class="tis-field">
                    <asp:Label ID="lblMainmenu" runat="server" Text="Main menu" AssociatedControlID="ddlMainMenu" CssClass="tis-label" />
                    <asp:DropDownList ID="ddlMainMenu" runat="server" DataTextField="MainMenu" DataValueField="MainMenu" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlMainMenu_SelectedIndexChanged" />
                </div>
                <asp:TextBox ID="txtMainMenu" runat="server" Visible="false" />
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="Pnlgv" runat="server" CssClass="tis-card">
        <div class="tis-card__header">
            <div class="tis-card__heading">
                <div class="tis-card__title">Program permissions</div>
                <div class="tis-card__subtitle">Tick what the role can access, create, edit and delete. The header boxes tick a whole column.</div>
            </div>
        </div>
        <div class="tis-table-wrap tis-table-wrap--xtall">
            <asp:GridView ID="GrdAssignProgam" runat="server" AutoGenerateColumns="False"
                OnRowCancelingEdit="GrdAssignProgam_RowCancelingEdit"
                OnRowDeleting="GrdAssignProgam_RowDeleting"
                OnRowEditing="GrdAssignProgam_RowEditing"
                OnRowUpdating="GrdAssignProgam_RowUpdating"
                DataKeyNames="ProgramId" OnRowCommand="GrdAssignProgam_RowCommand"
                OnRowDataBound="GrdAssignProgam_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="ProgramId" Visible="false">
                        <ItemTemplate><asp:Label ID="lblProgramId" runat="server" Text='<%# Eval("ProgramId") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Program" ItemStyle-CssClass="wrap">
                        <ItemTemplate><asp:Label ID="lblProgramName" runat="server" Text='<%# Eval("ProgramName") %>' /></ItemTemplate>
                        <EditItemTemplate><asp:Label ID="lblProgramName" runat="server" Text='<%# Bind("ProgramName") %>' /></EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Access" HeaderStyle-CssClass="check" ItemStyle-CssClass="check">
                        <HeaderTemplate>
                            <asp:CheckBox ID="ShowAllAccess" runat="server" Text="Access" Checked="false" AutoPostBack="true" OnCheckedChanged="ShowAllAccess_CheckedChanged" />
                            <asp:Label ID="lblAccess" runat="server" Text="Access" Visible="false" />
                        </HeaderTemplate>
                        <ItemTemplate><asp:CheckBox ID="chkAccess" runat="server" Checked='<%# Eval("CanAccess") %>' /></ItemTemplate>
                        <EditItemTemplate><asp:CheckBox ID="chkEditAccess" runat="server" Checked='<%# Bind("CanAccess") %>' /></EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Create" HeaderStyle-CssClass="check" ItemStyle-CssClass="check">
                        <HeaderTemplate>
                            <asp:CheckBox ID="ShowAllCreate" runat="server" Text="Create" Checked="false" AutoPostBack="true" OnCheckedChanged="ShowAllCreate_CheckedChanged" />
                            <asp:Label ID="lblCreate" runat="server" Text="Create" Visible="false" />
                        </HeaderTemplate>
                        <ItemTemplate><asp:CheckBox ID="chkCreate" runat="server" Checked='<%# Eval("CanCreate") %>' /></ItemTemplate>
                        <EditItemTemplate><asp:CheckBox ID="chkEditCreate" runat="server" Checked='<%# Bind("CanCreate") %>' /></EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Edit" HeaderStyle-CssClass="check" ItemStyle-CssClass="check">
                        <HeaderTemplate>
                            <asp:CheckBox ID="ShowAllEdit" runat="server" Text="Edit" Checked="false" AutoPostBack="true" OnCheckedChanged="ShowAllEdit_CheckedChanged" />
                            <asp:Label ID="lblEdit" runat="server" Text="Edit" Visible="false" />
                        </HeaderTemplate>
                        <ItemTemplate><asp:CheckBox ID="chkEdit" runat="server" Checked='<%# Eval("CanEdit") %>' /></ItemTemplate>
                        <EditItemTemplate><asp:CheckBox ID="chkModEdit" runat="server" Checked='<%# Bind("CanEdit") %>' /></EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Delete" HeaderStyle-CssClass="check" ItemStyle-CssClass="check">
                        <HeaderTemplate>
                            <asp:CheckBox ID="ShowAllDelete" runat="server" Text="Delete" Checked="false" AutoPostBack="true" OnCheckedChanged="ShowAllDelete_CheckedChanged" />
                            <asp:Label ID="lblDelete" runat="server" Text="Delete" Visible="false" />
                        </HeaderTemplate>
                        <ItemTemplate><asp:CheckBox ID="chkDelete" runat="server" Checked='<%# Eval("CanDelete") %>' /></ItemTemplate>
                        <EditItemTemplate><asp:CheckBox ID="chkEditDelete" runat="server" Checked='<%# Bind("CanDelete") %>' /></EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="View" Visible="false" HeaderStyle-CssClass="check" ItemStyle-CssClass="check">
                        <ItemTemplate><asp:CheckBox ID="chkPrint" runat="server" Checked='<%# Eval("CanPrint") %>' /></ItemTemplate>
                        <EditItemTemplate><asp:CheckBox ID="chkEditPrint" runat="server" Checked='<%# Bind("CanPrint") %>' /></EditItemTemplate>
                    </asp:TemplateField>
                    <asp:ButtonField ButtonType="Link" Text="Select all" HeaderText="Row" CommandName="SelectAll" CausesValidation="True"
                        ControlStyle-CssClass="tis-link" HeaderStyle-CssClass="actions" ItemStyle-CssClass="actions" />
                    <asp:CommandField HeaderText="Edit" ShowEditButton="True"
                        ControlStyle-CssClass="tis-link" HeaderStyle-CssClass="actions" ItemStyle-CssClass="actions" />
                </Columns>
                <EmptyDataTemplate>
                    <tis:EmptyState ID="emptyPrograms" runat="server" Icon="shield" Title="No programs to show"
                        Text="Pick a role, or choose another main menu, to see its program permissions." />
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
        <asp:Panel ID="pnlSave" runat="server" CssClass="tis-card__footer tis-card__footer--between">
            <span class="tis-help">Save creates the new role with the ticked permissions. Existing roles are changed row by row with Edit.</span>
            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="tis-btn tis-btn--primary" />
            <asp:HiddenField ID="HidDeleteCount" Value="0" runat="server" />
            <asp:HiddenField ID="HidUpdateCount" Value="0" runat="server" />
        </asp:Panel>
    </asp:Panel>
</div>
</asp:Content>
