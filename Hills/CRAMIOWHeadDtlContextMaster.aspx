<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.Master" AutoEventWireup="true" CodeFile="CRAMIOWHeadDtlContextMaster.aspx.cs" Inherits="CRAMIOWHeadDtlContextMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="tis-page" data-density="compact">
    <div class="tis-page-header">
        <div class="tis-page-header__text">
            <span class="tis-eyebrow">CRAM</span>
            <h1 class="tis-page-header__title"><asp:Label ID="lblIOWHeadMaster" runat="server" Text="CRAM IOW head detail context" /></h1>
            <p class="tis-page-header__desc">Pick an IOW head and one of its IOW codes, then build its numbered context lines with the items or IOWs they use.</p>
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
                    <asp:DropDownList ID="ddlsubGroup" runat="server" DataTextField="SubGroupName" DataValueField="SubGroupCode" Visible="true" />
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
                <div class="tis-card__subtitle">Select an IOW head name to see its IOW codes.</div>
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
                <div class="tis-card__subtitle">Select an IOW description to enter its context.</div>
            </div>
        </div>
        <div class="tis-card__body">
            <div class="tis-form-grid tis-form-grid--4">
                <div class="tis-field">
                    <asp:Label ID="Label9" runat="server" Text="IOW head code" AssociatedControlID="txtCRAMIOWHeadCode" CssClass="tis-label" />
                    <asp:TextBox ID="txtCRAMIOWHeadCode" runat="server" ReadOnly="true" />
                </div>
                <div class="tis-field tis-field--span-2">
                    <asp:Label ID="Label5" runat="server" Text="IOW head name" AssociatedControlID="txtCRAMIOWHeadName" CssClass="tis-label" />
                    <asp:TextBox ID="txtCRAMIOWHeadName" runat="server" TextMode="MultiLine" ReadOnly="true" Rows="2" />
                </div>
            </div>
        </div>
        <div id="div2" runat="server" class="tis-table-wrap tis-table-wrap--tall">
            <asp:GridView ID="grdIOWdtl" runat="server" ToolTip="Click on IOW dtl Name row to Modify IOW "
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
                    <tis:EmptyState ID="emptyIOW" runat="server" Icon="list" Title="No IOW codes under this head"
                        Text="Add IOW codes on the CRAM IOW head detail page first." />
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
    </asp:Panel>

    <asp:Panel ID="PnlContext" runat="server" CssClass="tis-card">
        <div class="tis-card__header">
            <div class="tis-card__heading">
                <div class="tis-card__title">Context entry</div>
                <div class="tis-card__subtitle">Write the context line, give it a serial number, then add the item or IOW it uses.</div>
            </div>
        </div>
        <div class="tis-card__body">
            <div class="tis-form-grid tis-form-grid--4">
                <div class="tis-field">
                    <asp:Label ID="Label4" runat="server" Text="IOW head code" AssociatedControlID="txtIOWHeadCode" CssClass="tis-label" />
                    <asp:TextBox ID="txtIOWHeadCode" runat="server" ReadOnly="true" />
                </div>
                <div class="tis-field tis-field--span-2">
                    <asp:Label ID="Label10" runat="server" Text="IOW head name" AssociatedControlID="txtIOWHeadName" CssClass="tis-label" />
                    <asp:TextBox ID="txtIOWHeadName" runat="server" TextMode="MultiLine" ReadOnly="true" Rows="2" />
                </div>
                <div class="tis-field">
                    <asp:Label ID="Label2" runat="server" Text="IOW code" AssociatedControlID="txtIOWCode" CssClass="tis-label" />
                    <asp:TextBox ID="txtIOWCode" runat="server" ReadOnly="true" />
                </div>
                <div class="tis-field tis-field--full">
                    <asp:Label ID="lblIOWNm" runat="server" Text="IOW name" AssociatedControlID="txtIOWDescription" CssClass="tis-label" />
                    <asp:TextBox ID="txtIOWDescription" runat="server" TextMode="MultiLine" ReadOnly="true" Rows="2" />
                </div>
            </div>

            <div class="tis-section-title">Context line</div>
            <div class="tis-form-grid tis-form-grid--4">
                <div class="tis-field tis-field--full">
                    <asp:Label ID="lblContext" runat="server" Text="Context" AssociatedControlID="txtContext" CssClass="tis-label" />
                    <asp:TextBox ID="txtContext" runat="server" MaxLength="6000" TextMode="MultiLine" Rows="3" />
                </div>
                <div class="tis-field">
                    <asp:Label ID="Label11" runat="server" Text="Serial no." AssociatedControlID="txtContextSrlNo" CssClass="tis-label" />
                    <asp:TextBox ID="txtContextSrlNo" runat="server" MaxLength="16" Enabled="true" />
                </div>
                <div class="tis-field tis-field--check">
                    <asp:CheckBox ID="chkIsItem" runat="server" Text="Uses an item (clear to pick an IOW)" Visible="true" Checked="true"
                        AutoPostBack="true" OnCheckedChanged="chkIsItem_Changed" />
                </div>
            </div>

            <asp:Panel ID="Panel2" runat="server">
                <div class="tis-section-title">Item or IOW</div>
                <div class="tis-form-grid tis-form-grid--4">
                    <div class="tis-field tis-field--span-2">
                        <asp:Label ID="Label6" runat="server" Text="Item" AssociatedControlID="ddlItem" CssClass="tis-label" />
                        <asp:DropDownList ID="ddlItem" runat="server" MaxLength="128" DataTextField="ItemName" DataValueField="ItemId"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlItemChanged" />
                    </div>
                    <div class="tis-field">
                        <asp:Label ID="Label13" runat="server" Text="IOW code" AssociatedControlID="txtIOWItemCode" CssClass="tis-label" />
                        <asp:TextBox ID="txtIOWItemCode" runat="server" MaxLength="16" Enabled="false" />
                    </div>
                    <div class="tis-field">
                        <asp:Label ID="Label7" runat="server" Text="UOM" AssociatedControlID="txtItemUOM" CssClass="tis-label" />
                        <asp:TextBox ID="txtItemUOM" runat="server" MaxLength="16" Enabled="false" />
                    </div>
                    <div class="tis-field">
                        <asp:Label ID="Label8" runat="server" Text="Quantity" AssociatedControlID="txtItemQty" CssClass="tis-label" />
                        <asp:TextBox ID="txtItemQty" runat="server" MaxLength="16" />
                    </div>
                    <div class="tis-field">
                        <asp:Label ID="Label1" runat="server" Text="Wastage" AssociatedControlID="txtWastage" CssClass="tis-label" />
                        <asp:TextBox ID="txtWastage" runat="server" MaxLength="16" Text="0" />
                    </div>
                    <div class="tis-field tis-field--check">
                        <asp:CheckBox ID="chkIsImported" runat="server" Text="Imported" Visible="true" Enabled="false" />
                    </div>
                    <div class="tis-field tis-field--check">
                        <div class="tis-cluster tis-cluster--end">
                            <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" CssClass="tis-btn tis-btn--ghost" />
                            <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" CssClass="tis-btn tis-btn--primary" />
                        </div>
                    </div>
                </div>
                <asp:TextBox ID="txtItemCode" runat="server" MaxLength="16" Visible="false" />
                <asp:TextBox ID="txtGridItemId" runat="server" MaxLength="16" Visible="false" />
                <asp:TextBox ID="txtCRAMIOWHeadDtlId" runat="server" MaxLength="16" Visible="false" Text="0" />
                <asp:TextBox ID="txtIOWItemDescription" runat="server" MaxLength="16" Visible="false" Text="" />
            </asp:Panel>
        </div>
    </asp:Panel>

    <asp:Panel ID="Panel1" runat="server" CssClass="tis-card">
        <div class="tis-card__header">
            <div class="tis-card__heading">
                <div class="tis-card__title">Pick an IOW</div>
                <div class="tis-card__subtitle">Select an IOW name to use it in the context line instead of an item.</div>
            </div>
        </div>
        <div class="tis-card__body">
            <asp:Panel ID="pnlfilt" runat="server" CssClass="tis-toolbar" DefaultButton="btnIOWItemFilter">
                <div class="tis-field tis-toolbar__grow">
                    <asp:Label ID="TextBox1" runat="server" Text="Search IOWs" Visible="true" AssociatedControlID="txtIowItemFilter" CssClass="tis-label" />
                    <asp:TextBox ID="txtIowItemFilter" runat="server" Text="" Visible="true" placeholder="Code or description" />
                </div>
                <asp:Button ID="btnIOWItemFilter" runat="server" OnClick="btnIOWItemFilter_Click" Text="Filter" CssClass="tis-btn" />
            </asp:Panel>
        </div>
        <asp:Panel ID="PnlIowGrdSel" runat="server" CssClass="tis-table-wrap tis-table-wrap--tall">
            <asp:GridView ID="grdIOWItemSel" runat="server" ToolTip="Click on IOW Name row Select IOW Item "
                AutoGenerateColumns="False" OnRowCommand="GrdIOWItemSel_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="CompanyId" Visible="false">
                        <ItemTemplate><asp:Label ID="lblCompanyId" runat="server" Text='<%# Eval("CompanyId") %>' /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Dtl Id" Visible="false">
                        <ItemTemplate><asp:Label ID="lblCRAMIOWHeadDtlId" runat="server" Text='<%# Eval("CRAMIOWHeadDtlId") %>' /></ItemTemplate>
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
                    <asp:TemplateField HeaderText="Temporary" HeaderStyle-CssClass="center" ItemStyle-CssClass="check">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkIsTemproryIOW" runat="server" Checked='<%# Eval("IsTemproryIOW") %>' Enabled="false" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <tis:EmptyState ID="emptyIOWSel" runat="server" Icon="search" Title="No matching IOWs"
                        Text="Try a shorter search text." />
                </EmptyDataTemplate>
            </asp:GridView>
        </asp:Panel>
    </asp:Panel>

    <asp:Panel ID="pnlIOWItem" runat="server" CssClass="tis-card">
        <div class="tis-card__header">
            <div class="tis-card__heading">
                <div class="tis-card__title">Context lines</div>
                <div class="tis-card__subtitle">Lines added so far for this IOW. Existing saved lines are read-only.</div>
            </div>
        </div>
        <asp:Panel ID="Panel3" runat="server">
            <div id="div3" runat="server" class="tis-table-wrap tis-table-wrap--tall">
                <asp:GridView ID="grdIOWItem" runat="server" AutoGenerateColumns="False" OnRowCommand="grdIOWItem_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="CompanyId" Visible="false">
                            <ItemTemplate><asp:Label ID="lblCompanyId" runat="server" Text='<%# Eval("CompanyId") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="IOW Code" Visible="false">
                            <ItemTemplate><asp:Label ID="lblCRAMIOWCode" runat="server" Text='<%# Eval("CRAMIOWCode") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="IOW Name" Visible="false">
                            <ItemTemplate><asp:Label ID="lblCRAMIOWName" runat="server" Text='<%# Eval("CRAMIOWName") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="No." Visible="true" HeaderStyle-CssClass="num" ItemStyle-CssClass="num">
                            <ItemTemplate><asp:Label ID="lblContextSrlNo" runat="server" Text='<%# Eval("ContextSrlNo") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Context" Visible="false">
                            <ItemTemplate><asp:Label ID="lblContext" runat="server" Text='<%# Eval("Context") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Context" Visible="true" ItemStyle-CssClass="wrap">
                            <ItemTemplate><asp:Label ID="lblContextDisplay" runat="server" Text='<%# Eval("ContextDisplay") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ItemID" Visible="false">
                            <ItemTemplate><asp:Label ID="lblItemId" runat="server" Text='<%# Eval("ItemId") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="IOW Id" Visible="false">
                            <ItemTemplate><asp:Label ID="lblIOWHeadDtlId" runat="server" Text='<%# Eval("IOWHeadDtlId") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="IOW code" Visible="true" ItemStyle-CssClass="code">
                            <ItemTemplate><asp:Label ID="lblIOWItemCode" runat="server" Text='<%# Eval("IOWCode") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item / IOW" Visible="true" ItemStyle-CssClass="wrap">
                            <ItemTemplate><asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ItemName") %>' /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item code" Visible="true" ItemStyle-CssClass="code">
                            <ItemTemplate><asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("ItemCode") %>' /></ItemTemplate>
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
                                <asp:LinkButton ID="lnkIOWItemNameDel" runat="server" CssClass="tis-link" CommandArgument='<%#Eval("ContextSrlNo")%>'
                                    CommandName="IOWItemDel" Text="Delete" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <tis:EmptyState ID="emptyLines" runat="server" Icon="clipboard" Title="No context lines yet"
                            Text="Add a context line above; it appears here before you save." />
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </asp:Panel>
        <div class="tis-card__footer">
            <asp:Button ID="btnItemSave" runat="server" Text="Save" OnClick="btnItemSave_Click" CssClass="tis-btn tis-btn--primary" />
        </div>
    </asp:Panel>
</div>
</asp:Content>
