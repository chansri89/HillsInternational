<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.Master" AutoEventWireup="true" CodeFile="CRAMIOWHeadDtlMaster.aspx.cs" Inherits="CRAMIOWHeadDtlMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="tis-page" data-density="compact">
    <div class="tis-page-header">
        <div class="tis-page-header__text">
            <span class="tis-eyebrow">CRAM</span>
            <h1 class="tis-page-header__title"><asp:Label ID="lblIOWHeadMaster" runat="server" Text="CRAM IOW head details" /></h1>
            <p class="tis-page-header__desc">Pick an IOW head, then add or edit the IOW codes, units and quantities that sit under it.</p>
        </div>
    </div>

    <asp:Panel ID="pnlPendind" runat="server" CssClass="tis-card" DefaultButton="btnFilter">
        <div class="tis-card__body">
            <div class="tis-toolbar">
                <div class="tis-field">
                    <asp:Label ID="lblCompany" runat="server" Text="Company" Visible="true" AssociatedControlID="ddlCompany" CssClass="tis-label" />
                    <asp:DropDownList ID="ddlCompany" runat="server" DataTextField="CompanyName" DataValueField="CompanyId"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_Changed" />
                </div>
                <div class="tis-field">
                    <asp:Label ID="lblGroup" runat="server" Text="Group" AssociatedControlID="ddlGroup" CssClass="tis-label" />
                    <asp:DropDownList ID="ddlGroup" runat="server" DataTextField="GroupName" DataValueField="GroupCode"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlGroup_Changed" />
                </div>
                <div class="tis-field">
                    <asp:Label ID="lblSubGroup" runat="server" Text="Sub group" AssociatedControlID="ddlsubGroup" CssClass="tis-label" />
                    <asp:DropDownList ID="ddlsubGroup" runat="server" DataTextField="SubGroupName" DataValueField="SubGroupCode" />
                </div>
                <div class="tis-field tis-toolbar__grow">
                    <asp:Label ID="lblFilter" runat="server" Text="Search by IOW head name" Visible="true" AssociatedControlID="txtFilter" CssClass="tis-label" />
                    <asp:TextBox ID="txtFilter" runat="server" Text="" Visible="true" placeholder="e.g. Excavation" />
                </div>
                <asp:Button ID="btnFilter" runat="server" OnClick="btnFilter_Click" Text="Filter" CssClass="tis-btn" />
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="Pnlgv" runat="server" CssClass="tis-card">
        <div class="tis-card__header">
            <div class="tis-card__heading">
                <div class="tis-card__title">IOW heads</div>
                <div class="tis-card__subtitle">Select an IOW head name to add or update its IOW codes.</div>
            </div>
        </div>
        <div id="divIOW" runat="server" class="tis-table-wrap tis-table-wrap--tall">
            <asp:GridView ID="GrdIOWHeadMaster" runat="server" ToolTip="Click on IOW Name row to create IOW data"
                AutoGenerateColumns="False" OnRowCommand="GrdIOWHead_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="CompanyId" Visible="false">
                        <ItemTemplate><asp:Label ID="lblCompanyId" runat="server" Text='<%# Eval("CompanyId") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Group" Visible="true" ItemStyle-CssClass="code">
                        <ItemTemplate><asp:Label ID="lblGroupCode" runat="server" Text='<%# Eval("GroupCode") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Group name" Visible="true" ItemStyle-CssClass="nowrap">
                        <ItemTemplate><asp:Label ID="lblGroupName" runat="server" Text='<%# Eval("GroupName") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sub group" Visible="true" ItemStyle-CssClass="code">
                        <ItemTemplate><asp:Label ID="lblSubGroupCode" runat="server" Text='<%# Eval("SubGroupCode") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sub group name" Visible="true" ItemStyle-CssClass="nowrap">
                        <ItemTemplate><asp:Label ID="lblSubGroupName" runat="server" Text='<%# Eval("SubGroupName") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Level 1" Visible="true" ItemStyle-CssClass="code">
                        <ItemTemplate><asp:Label ID="lblIOWLevel1" runat="server" Text='<%# Eval("IOWLevel1") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Level 2" Visible="true" ItemStyle-CssClass="code">
                        <ItemTemplate><asp:Label ID="lblIOWLevel2" runat="server" Text='<%# Eval("IOWLevel2") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Level 3" Visible="true" ItemStyle-CssClass="code">
                        <ItemTemplate><asp:Label ID="lblIOWLevel3" runat="server" Text='<%# Eval("IOWLevel3") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Level 4" Visible="true" ItemStyle-CssClass="code">
                        <ItemTemplate><asp:Label ID="lblIOWLevel4" runat="server" Text='<%# Eval("IOWLevel4") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Head code" Visible="true" ItemStyle-CssClass="code">
                        <ItemTemplate><asp:Label ID="lblIOWHeadCode" runat="server" Text='<%# Eval("IOWHeadCode") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="IOW Head Name" Visible="false">
                        <ItemTemplate><asp:Label ID="lblIOWHeadName" runat="server" Text='<%# Eval("IOWHeadName") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="IOW head" Visible="true" ItemStyle-CssClass="wrap">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkIOWHeadName" runat="server" CssClass="tis-link tis-link--strong"
                                CommandArgument='<%#Eval("IOWHeadCode")%>' CommandName="SelIOW" Text='<%#Eval("IOWHeadName") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <tis:EmptyState ID="emptyHeads" runat="server" Icon="layers" Title="No IOW heads found"
                        Text="Change the group, sub group or search text and select Filter again." />
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
    </asp:Panel>

    <asp:Panel ID="pnlIOW" runat="server" CssClass="tis-card">
        <div class="tis-card__header">
            <div class="tis-card__heading">
                <div class="tis-card__title">IOW codes under this head</div>
                <div class="tis-card__subtitle">Select an IOW description to modify it.</div>
            </div>
        </div>
        <div id="div2" runat="server" class="tis-table-wrap tis-table-wrap--tall">
            <asp:GridView ID="grdIOWdtl" runat="server" ToolTip="Click on IOW Name row to Modify IOW "
                AutoGenerateColumns="False" OnRowCommand="GrdIOWDtl_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="CompanyId" Visible="false">
                        <ItemTemplate><asp:Label ID="lblCompanyId" runat="server" Text='<%# Eval("CompanyId") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Dtl Id" Visible="false">
                        <ItemTemplate><asp:Label ID="lblCRAMIOWHeadDtlId" runat="server" Text='<%# Eval("CRAMIOWHeadDtlId") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Head code" Visible="true" ItemStyle-CssClass="code">
                        <ItemTemplate><asp:Label ID="lblPreviousIOWLevel" runat="server" Text='<%# Eval("PreviousIOWLevel") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="IOW code" Visible="true" ItemStyle-CssClass="code">
                        <ItemTemplate><asp:Label ID="lblIOWCode" runat="server" Text='<%# Eval("IOWCode") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="IOW dtl Name" Visible="false">
                        <ItemTemplate><asp:Label ID="lblIOWDescription" runat="server" Text='<%# Eval("IOWDescription") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="IOW description" Visible="true" ItemStyle-CssClass="wrap">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkIOWDescription" runat="server" CssClass="tis-link tis-link--strong"
                                CommandArgument='<%#Eval("IOWCode")%>' CommandName="IOW" Text='<%#Eval("IOWDescription") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="UOM" Visible="true" ItemStyle-CssClass="nowrap">
                        <ItemTemplate><asp:Label ID="lblIOWUOM" runat="server" Text='<%# Eval("IOWUOM") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Qty" Visible="true" HeaderStyle-CssClass="num" ItemStyle-CssClass="num">
                        <ItemTemplate><asp:Label ID="lblIOWQty" runat="server" Text='<%# Eval("IOWQuantity") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Temporary" HeaderStyle-CssClass="center" ItemStyle-CssClass="check">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkIsTemproryIOW" runat="server" Checked='<%# Eval("IsTemproryIOW") %>' Enabled="false" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <tis:EmptyState ID="emptyIOW" runat="server" Icon="list" Title="No IOW codes yet"
                        Text="Use the form below to add the first IOW code under this head." />
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
    </asp:Panel>

    <asp:Panel ID="pnlIOWAdd" runat="server" CssClass="tis-card" DefaultButton="btnSaveIOW">
        <div class="tis-card__header">
            <div class="tis-card__heading">
                <div class="tis-card__title">Add / modify IOW code</div>
                <div class="tis-card__subtitle">The IOW code must start with the head code followed by a dot.</div>
            </div>
        </div>
        <div class="tis-card__body">
            <div class="tis-form-grid tis-form-grid--4">
                <div class="tis-field">
                    <asp:Label ID="Label5" runat="server" Text="IOW head code" AssociatedControlID="txtCRAMIOWHeadCode" CssClass="tis-label" />
                    <asp:TextBox ID="txtCRAMIOWHeadCode" runat="server" ReadOnly="true" />
                </div>
                <div class="tis-field tis-field--span-2">
                    <asp:Label ID="Label9" runat="server" Text="IOW head name" AssociatedControlID="txtCRAMIOWHeadName" CssClass="tis-label" />
                    <asp:TextBox ID="txtCRAMIOWHeadName" runat="server" TextMode="MultiLine" ReadOnly="true" Rows="2" />
                </div>
                <div class="tis-field">
                    <asp:Label ID="Label2" runat="server" Text="IOW code" AssociatedControlID="txtIOWCode" CssClass="tis-label" />
                    <asp:TextBox ID="txtIOWCode" runat="server" MaxLength="16" />
                </div>
                <div class="tis-field tis-field--full">
                    <asp:Label ID="lblIOWNm" runat="server" Text="IOW description" AssociatedControlID="txtIOWDescription" CssClass="tis-label" />
                    <asp:TextBox ID="txtIOWDescription" runat="server" MaxLength="6000" TextMode="MultiLine" Rows="4" />
                </div>
            </div>
            <asp:Panel ID="Panel2" runat="server" CssClass="tis-form-grid tis-form-grid--4">
                <div class="tis-field">
                    <asp:Label ID="Label10" runat="server" Text="IOW UOM" AssociatedControlID="txtCRAMIOWUOM" CssClass="tis-label" />
                    <asp:TextBox ID="txtCRAMIOWUOM" runat="server" MaxLength="16" />
                </div>
                <div class="tis-field">
                    <asp:Label ID="Label11" runat="server" Text="IOW quantity" AssociatedControlID="txtCRAMIOWQuantity" CssClass="tis-label" />
                    <asp:TextBox ID="txtCRAMIOWQuantity" runat="server" MaxLength="16" />
                </div>
                <div class="tis-field tis-field--check">
                    <asp:CheckBox ID="chkIsTempIOW" runat="server" Text="Temporary IOW" Visible="true" Enabled="true" />
                </div>
            </asp:Panel>
        </div>
        <div class="tis-card__footer">
            <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" CssClass="tis-btn tis-btn--ghost" />
            <asp:Button ID="btnSaveIOW" runat="server" Text="Save IOW" OnClick="btnSaveIOW_Click" CssClass="tis-btn tis-btn--primary" />
        </div>
    </asp:Panel>

    <asp:Panel ID="PnlContext" runat="server" CssClass="tis-card">
        <div class="tis-card__header">
            <div class="tis-card__heading">
                <div class="tis-card__title">IOW context and items</div>
                <div class="tis-card__subtitle">Describe the context, then add the items it uses.</div>
            </div>
        </div>
        <div class="tis-card__body">
            <div class="tis-form-grid tis-form-grid--1">
                <div class="tis-field">
                    <asp:Label ID="lblContext" runat="server" Text="Context" AssociatedControlID="txtContext" CssClass="tis-label" />
                    <asp:TextBox ID="txtContext" runat="server" MaxLength="6000" TextMode="MultiLine" Rows="4" />
                </div>
            </div>
            <div class="tis-section-title">Item</div>
            <asp:Panel ID="Panel1" runat="server" CssClass="tis-form-grid tis-form-grid--4">
                <div class="tis-field">
                    <asp:Label ID="Label6" runat="server" Text="Item" AssociatedControlID="ddlItem" CssClass="tis-label" />
                    <asp:DropDownList ID="ddlItem" runat="server" MaxLength="128" DataTextField="ItemName" DataValueField="ItemId"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlItemChanged" />
                </div>
                <div class="tis-field">
                    <asp:Label ID="Label4" runat="server" Text="IOW" AssociatedControlID="ddlCRAMIOW" CssClass="tis-label" />
                    <asp:DropDownList ID="ddlCRAMIOW" runat="server" MaxLength="128" DataTextField="CRAMIOWName" DataValueField="CRAMIOWCode" />
                </div>
                <div class="tis-field">
                    <asp:Label ID="Label7" runat="server" Text="Item UOM" AssociatedControlID="txtItemUOM" CssClass="tis-label" />
                    <asp:TextBox ID="txtItemUOM" runat="server" MaxLength="16" ReadOnly="true" />
                </div>
                <div class="tis-field">
                    <asp:Label ID="Label8" runat="server" Text="Item quantity" AssociatedControlID="txtItemQty" CssClass="tis-label" />
                    <asp:TextBox ID="txtItemQty" runat="server" MaxLength="16" />
                </div>
                <div class="tis-field">
                    <asp:Label ID="Label1" runat="server" Text="Wastage" AssociatedControlID="txtWastage" CssClass="tis-label" />
                    <asp:TextBox ID="txtWastage" runat="server" MaxLength="16" />
                </div>
                <div class="tis-field tis-field--check">
                    <asp:CheckBox ID="chkIsImported" runat="server" Text="Imported" Visible="true" Enabled="true" />
                </div>
                <asp:TextBox ID="txtGridItemId" runat="server" MaxLength="16" Visible="false" />
                <asp:TextBox ID="txtItemCode" runat="server" MaxLength="16" Visible="false" />
            </asp:Panel>
        </div>
        <div class="tis-card__footer">
            <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" CssClass="tis-btn" />
        </div>
    </asp:Panel>

    <asp:Panel ID="pnlIOWItem" runat="server" CssClass="tis-card">
        <div class="tis-card__header">
            <div class="tis-card__heading">
                <div class="tis-card__title">IOW items</div>
                <div class="tis-card__subtitle"><asp:Label ID="Label3" runat="server" Text="Save to store the items listed in this grid." /></div>
            </div>
            <asp:Button ID="btnItemSave" runat="server" Text="Save" OnClick="btnItemSave_Click" CssClass="tis-btn tis-btn--primary tis-btn--sm" />
        </div>
        <div id="div1" runat="server" class="tis-table-wrap tis-table-wrap--tall">
            <asp:GridView ID="grdIOWItem" runat="server" AutoGenerateColumns="False" OnRowCommand="grdIOWItem_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="CompanyId" Visible="false">
                        <ItemTemplate><asp:Label ID="lblCompanyId" runat="server" Text='<%# Eval("CompanyId") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="IOW Code" Visible="false">
                        <ItemTemplate><asp:Label ID="lblIOWCode" runat="server" Text='<%# Eval("IOWCode") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="IOW Name" Visible="false">
                        <ItemTemplate><asp:Label ID="lblIOWName" runat="server" Text='<%# Eval("IOWName") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Item ID" Visible="true" ItemStyle-CssClass="code">
                        <ItemTemplate><asp:Label ID="lblItemId" runat="server" Text='<%# Eval("ItemId") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Item" Visible="true" ItemStyle-CssClass="wrap">
                        <ItemTemplate><asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ItemName") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Item code" Visible="true" ItemStyle-CssClass="code">
                        <ItemTemplate><asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("ItemCode") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="IOW code" Visible="true" ItemStyle-CssClass="code">
                        <ItemTemplate><asp:Label ID="lblCRAMIOWCode" runat="server" Text='<%# Eval("CRAMIOWCode") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="IOW" Visible="true" ItemStyle-CssClass="wrap">
                        <ItemTemplate><asp:Label ID="lblIOWName" runat="server" Text='<%# Eval("IOWName") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="UOM" Visible="true" ItemStyle-CssClass="nowrap">
                        <ItemTemplate><asp:Label ID="lblItemUOM" runat="server" Text='<%# Eval("ITEMUOM") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Qty" Visible="true" HeaderStyle-CssClass="num" ItemStyle-CssClass="num">
                        <ItemTemplate><asp:Label ID="lblItemQty" runat="server" Text='<%# Eval("ItemQuantity") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Wastage" Visible="true" HeaderStyle-CssClass="num" ItemStyle-CssClass="num">
                        <ItemTemplate><asp:Label ID="lblWastage" runat="server" Text='<%# Eval("Wastage") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Imported" HeaderStyle-CssClass="center" ItemStyle-CssClass="check">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkIsImported" runat="server" Checked='<%# Eval("IsImported") %>' Enabled="false" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="" Visible="true" ItemStyle-CssClass="actions">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkIOWItemNameDel" runat="server" CssClass="tis-link" CommandArgument='<%#Eval("ItemId")%>'
                                CommandName="IOWItemDel" Text="Delete" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="" Visible="true" ItemStyle-CssClass="actions">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkIOWItemNameMod" runat="server" CssClass="tis-link" CommandArgument='<%#Eval("ItemId")%>'
                                CommandName="IOWItemMod" Text="Edit" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <tis:EmptyState ID="emptyItems" runat="server" Icon="box" Title="No items added"
                        Text="Add items from the context section above." />
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
    </asp:Panel>
</div>
</asp:Content>
