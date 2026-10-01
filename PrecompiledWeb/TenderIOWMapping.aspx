<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.Master" AutoEventWireup="true" CodeFile="TenderIOWMapping.aspx.cs" Inherits="TenderIOWMapping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">
    function scrollToTargetRow() {
        var targetId = document.getElementById('<%= hdnTargetRowId.ClientID %>').value;
        if (targetId) {
            var rowElement = document.getElementById(targetId);
            var containerElement = document.getElementById('scrollContainer');
            if (rowElement && containerElement) {
                // Scroll the container to the row's vertical position
                containerElement.scrollTop = rowElement.offsetTop - containerElement.offsetTop;
                // Optional: Highlight the row for visibility
                rowElement.style.backgroundColor = '#ffffcc';
            }
        }
    }
</script>
<div class="tis-page" data-density="compact">
    <div class="tis-page-header">
        <div class="tis-page-header__text">
            <span class="tis-eyebrow">Tender</span>
            <h1 class="tis-page-header__title"><asp:Label ID="lblIOWMaster" runat="server" Text="Tender IOW mapping" /></h1>
            <p class="tis-page-header__desc">Tick one tender row, find the matching CRAM IOW, select it and save to map the two.</p>
        </div>
    </div>

    <asp:HiddenField ID="hdnTargetRowId" runat="server" Value="" />

    <asp:Panel ID="Panel3" runat="server" CssClass="tis-card">
        <asp:Panel ID="pnlPendind" runat="server" CssClass="tis-card__body" DefaultButton="btnGo">
            <div class="tis-toolbar">
                <div class="tis-field">
                    <asp:Label ID="lblCompany" runat="server" Text="Company" Visible="true" AssociatedControlID="ddlCompany" CssClass="tis-label" />
                    <asp:DropDownList ID="ddlCompany" runat="server" Visible="true" DataTextField="CompanyName" DataValueField="CompanyId"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlCompanyChanged" />
                </div>
                <div class="tis-field">
                    <asp:Label ID="lblClients" runat="server" Text="Client" Visible="true" AssociatedControlID="ddlClient" CssClass="tis-label" />
                    <asp:DropDownList ID="ddlClient" runat="server" Visible="true" AutoPostBack="true" OnSelectedIndexChanged="ddlClientChanged"
                        DataTextField="ClientName" DataValueField="ClientCode" />
                </div>
                <div class="tis-field tis-toolbar__grow">
                    <asp:Label ID="lblProject" runat="server" Text="Project" Visible="true" AssociatedControlID="ddlProject" CssClass="tis-label" />
                    <asp:DropDownList ID="ddlProject" runat="server" Visible="true" DataTextField="ProjectName" DataValueField="ClientProjectId" />
                </div>
                <asp:Button ID="btnGo" runat="server" OnClick="btnGo_Click" Text="Load tender" CssClass="tis-btn tis-btn--primary" />
            </div>
        </asp:Panel>
    </asp:Panel>

    <asp:Panel ID="Pnlgv" runat="server" CssClass="tis-stack" ToolTip="">
        <div id="divIOWdtl" runat="server" class="tis-form-grid tis-form-grid--3">
            <asp:Panel ID="PnlTender" runat="server" CssClass="tis-card" DefaultButton="btnFilter">
                <div class="tis-card__header">
                    <div class="tis-card__heading">
                        <div class="tis-card__title">Tender items</div>
                        <div class="tis-card__subtitle">Tick the row you want to map.</div>
                    </div>
                    <asp:Label ID="Label1" runat="server" Text="Mapped" Visible="true" CssClass="tis-badge tis-badge--success" />
                    <asp:Label ID="Label2" runat="server" Text="Not mapped" CssClass="tis-badge tis-badge--info" />
                </div>
                <div class="tis-card__body">
                    <div class="tis-toolbar">
                        <div class="tis-field">
                            <asp:Label ID="lblFilter" runat="server" Text="Excel row" Visible="true" AssociatedControlID="txtFilter" CssClass="tis-label" />
                            <asp:TextBox ID="txtFilter" runat="server" Text="" Visible="true" placeholder="Row #" />
                        </div>
                        <div class="tis-field tis-field--check">
                            <asp:CheckBox ID="chkQtyOnly" runat="server" Checked="false" Text="With qty only" Enabled="true" />
                        </div>
                        <div class="tis-field tis-field--check">
                            <asp:CheckBox ID="chkNoMap" runat="server" Checked="false" Text="Unmapped only" Enabled="true" />
                        </div>
                        <asp:Button ID="btnFilter" runat="server" OnClick="btnFilter_Click" Text="Filter" CssClass="tis-btn tis-btn--sm" />
                    </div>
                </div>
                <div id="div2" runat="server" class="tis-table-wrap tis-table-wrap--xtall">
                    <asp:GridView ID="GrdTender" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridTender_Databound">
                        <Columns>
                            <asp:TemplateField HeaderText="CompanyId" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblCompanyId" runat="server" Text='<%# Eval("CompanyId") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Client Project TenderId" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblClientTenderId" runat="server" Text='<%# Eval("ClientProjectTenderId") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tender IOW Mapped" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblTenderIOWMapped" runat="server" Text='<%# Eval("TenderIOWMapped") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sheet" Visible="true" ItemStyle-CssClass="nowrap">
                                <ItemTemplate>
                                    <asp:Label ID="lblExcelSheetName" runat="server" Text='<%# Eval("ExcelSheetName") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Row" Visible="true" HeaderStyle-CssClass="num" ItemStyle-CssClass="num">
                                <ItemTemplate>
                                    <asp:Label ID="lblExcelRowNo" runat="server" Text='<%# Eval("ExcelRowNumber") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sr no." Visible="true" ItemStyle-CssClass="code">
                                <ItemTemplate>
                                    <asp:Label ID="lblSrlNo" runat="server" Text='<%# Eval("Srlno") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description" Visible="true" ItemStyle-CssClass="wrap">
                                <ItemTemplate>
                                    <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UOM" Visible="true" ItemStyle-CssClass="nowrap">
                                <ItemTemplate>
                                    <asp:Label ID="lblUOM" runat="server" Text='<%# Eval("UOM") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Qty" Visible="true" HeaderStyle-CssClass="num" ItemStyle-CssClass="num">
                                <ItemTemplate>
                                    <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("Quantity") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Pick" HeaderStyle-CssClass="center" ItemStyle-CssClass="check">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelectBox" runat="server" Checked="false" Enabled="true" AutoPostBack="True"
                                        OnCheckedChanged="chkSelect_CheckedChanged" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <tis:EmptyState ID="emptyTender" runat="server" Icon="sheet" Title="No tender rows"
                                Text="Choose a company, client and project, then load the tender." />
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
                <input type="hidden" id="hdnScrollTop" runat="server" value="0" />
            </asp:Panel>

            <asp:Panel ID="Panel1" runat="server" CssClass="tis-card" DefaultButton="btnSelect">
                <div class="tis-card__header">
                    <div class="tis-card__heading">
                        <div class="tis-card__title">CRAM IOWs</div>
                        <div class="tis-card__subtitle">Select an IOW to add it to the mapping; click its code to see the head details.</div>
                    </div>
                </div>
                <div class="tis-card__body">
                    <asp:TextBox ID="txtClientProjectTenderId" runat="server" Text="" Visible="false" />
                    <div class="tis-form-grid tis-form-grid--2">
                        <div class="tis-field">
                            <asp:Label ID="lblGroup" runat="server" Text="Group" Visible="true" AssociatedControlID="ddlGroup" CssClass="tis-label" />
                            <asp:DropDownList ID="ddlGroup" runat="server" Visible="true" AutoPostBack="true" OnSelectedIndexChanged="ddlGroupChanged"
                                DataTextField="GroupName" DataValueField="GroupCode" />
                        </div>
                        <div class="tis-field">
                            <asp:Label ID="lblSubGroup" runat="server" Text="Sub group" Visible="true" AssociatedControlID="ddlSubGroup" CssClass="tis-label" />
                            <asp:DropDownList ID="ddlSubGroup" runat="server" Visible="true" DataTextField="SubGroupName" DataValueField="SubGroupCode" />
                        </div>
                        <div class="tis-field">
                            <asp:Label ID="lblIOWFilter" runat="server" Text="IOW search" Visible="true" AssociatedControlID="txtIOWFilter" CssClass="tis-label" />
                            <asp:TextBox ID="txtIOWFilter" runat="server" Text="" Visible="true" MaxLength="32" placeholder="Code or words" />
                        </div>
                        <div class="tis-field tis-field--check">
                            <asp:Button ID="btnSelect" runat="server" OnClick="btnSelect_Click" Text="Get IOW" CssClass="tis-btn" />
                        </div>
                    </div>
                </div>
                <div id="div1" runat="server" class="tis-table-wrap tis-table-wrap--xtall">
                    <asp:GridView ID="grdIow" runat="server" AutoGenerateColumns="False" OnRowCommand="GrdIOW_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="CompanyId" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblCompanyId" runat="server" Text='<%# Eval("CompanyId") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tender IOW Mapped" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblTenderIOWMapped" runat="server" Text='<%# Eval("TenderIOWMapped") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Group Code" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblGroupCode" runat="server" Text='<%# Eval("GroupCode") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SubGroup Code" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblSubGroupCode" runat="server" Text='<%# Eval("SubGroupCode") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CRAMIOWHeadDtlId" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblCRAMIOWHeadDtlId" runat="server" Text='<%# Eval("CRAMIOWHeadDtlId") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="IOW code" Visible="true" ItemStyle-CssClass="nowrap">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkIOWCode" runat="server" CssClass="tis-link" CommandArgument='<%#Eval("IOWCode")%>'
                                        CommandName="IOWCode" Text='<%#Eval("IOWCode") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="IOW Code" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblIOWCode" runat="server" Text='<%# Eval("IOWCode") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="IOW" Visible="true" ItemStyle-CssClass="wrap">
                                <ItemTemplate>
                                    <asp:Label ID="lblIOwName" runat="server" Text='<%# Eval("IOWDescription") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UOM" Visible="True" ItemStyle-CssClass="nowrap">
                                <ItemTemplate>
                                    <asp:Label ID="lblIOWUOM" runat="server" Text='<%# Eval("IOWUOM") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Temp" Visible="true" HeaderStyle-CssClass="center" ItemStyle-CssClass="check">
                                <ItemTemplate>
                                    <asp:CheckBox ID="lblTempIOW" runat="server" Checked='<%# Eval("IsTemproryIOW") %>' Enabled="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Select" Visible="false">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelectBox" runat="server" Checked="false" Enabled="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="" Visible="true" ItemStyle-CssClass="actions">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkIOWItem" runat="server" CssClass="tis-link tis-link--strong" CommandArgument='<%#Eval("IOWCode")%>'
                                        CommandName="IOWItemSel" Text="Select" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <tis:EmptyState ID="emptyIow" runat="server" Icon="search" Title="No IOWs listed"
                                Text="Tick a tender row, narrow by group or search text, then select Get IOW." />
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </asp:Panel>

            <asp:Panel ID="pnlIOWSelected" runat="server" CssClass="tis-card">
                <div class="tis-card__header">
                    <div class="tis-card__heading">
                        <div class="tis-card__title">Mapped IOW</div>
                        <div class="tis-card__subtitle">Exactly one IOW can be mapped to a tender row. Drop it to choose another.</div>
                    </div>
                    <asp:Label ID="Label3" runat="server" Text="" Visible="true" CssClass="tis-help" />
                </div>
                <div id="div4" runat="server" class="tis-table-wrap tis-table-wrap--xtall">
                    <asp:GridView ID="grdIOWSelected" runat="server" AutoGenerateColumns="False" OnRowCommand="GrdIOWSelected_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="CompanyId" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblCompanyId" runat="server" Text='<%# Eval("CompanyId") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Group Code" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblGroupCode" runat="server" Text='<%# Eval("GroupCode") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SubGroup Code" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblSubGroupCode" runat="server" Text='<%# Eval("SubGroupCode") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tender IOW Mapped" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblTenderIOWMapped" runat="server" Text='<%# Eval("TenderIOWMapped") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CRAMIOWHeadDtlId" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblCRAMIOWHeadDtlId" runat="server" Text='<%# Eval("CRAMIOWHeadDtlId") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="IOW code" Visible="true" ItemStyle-CssClass="code">
                                <ItemTemplate>
                                    <asp:Label ID="lblIOWCode" runat="server" Text='<%# Eval("IOWCode") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="IOW" Visible="true" ItemStyle-CssClass="wrap">
                                <ItemTemplate>
                                    <asp:Label ID="lblIOwName" runat="server" Text='<%# Eval("IOWDescription") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UOM" Visible="True" ItemStyle-CssClass="nowrap">
                                <ItemTemplate>
                                    <asp:Label ID="lblIOWUOM" runat="server" Text='<%# Eval("IOWUOM") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Temp IOW" Visible="false">
                                <ItemTemplate>
                                    <asp:CheckBox ID="lblTempIOW" runat="server" Checked='<%# Eval("IsTemproryIOW") %>' Enabled="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="" Visible="true" ItemStyle-CssClass="actions">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkIOWselectedCode" runat="server" CssClass="tis-link" CommandArgument='<%#Eval("IOWCode")%>'
                                        CommandName="Drop" Text="Drop" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <tis:EmptyState ID="emptySelected" runat="server" Icon="network" Title="Nothing mapped yet"
                                Text="Select an IOW from the middle list to map it to the ticked tender row." />
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </asp:Panel>
        </div>

        <div class="tis-actionbar">
            <div class="tis-actionbar__text">
                Tender row <strong><asp:Label ID="lblTenderSrlNo" runat="server" Text="" Visible="true" /></strong>
                <asp:Label ID="lblTenderDesc" runat="server" Visible="False" />
            </div>
            <asp:Button ID="btnShowTender" runat="server" OnClick="btnShowTender_Click" Text="All tender rows" CssClass="tis-btn tis-btn--ghost" />
            <asp:Button ID="btnIOWSave" runat="server" OnClick="btnIOWSave_Click" Text="Save" CssClass="tis-btn tis-btn--primary" />
        </div>
    </asp:Panel>

    <asp:Panel ID="PnlIOWdtl" runat="server" CssClass="tis-card" ToolTip="">
        <div class="tis-card__header">
            <div class="tis-card__heading">
                <div class="tis-card__title">IOW head details</div>
                <div class="tis-card__subtitle">Where this IOW sits in the CRAM group, sub group and level hierarchy.</div>
            </div>
            <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Text="Back to mapping" CssClass="tis-btn tis-btn--sm" />
        </div>
        <div id="div3" runat="server" class="tis-table-wrap tis-table-wrap--tall">
            <asp:GridView ID="GrdIOWDtl" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:TemplateField HeaderText="CompanyId" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblCompanyId" runat="server" Text='<%# Eval("CompanyId") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Group" Visible="true" ItemStyle-CssClass="nowrap">
                        <ItemTemplate>
                            <asp:Label ID="lblGroupName" runat="server" Text='<%# Eval("GroupName") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sub group" Visible="true" ItemStyle-CssClass="nowrap">
                        <ItemTemplate>
                            <asp:Label ID="lblSubGroupName" runat="server" Text='<%# Eval("SubGroupName") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="IOW code" Visible="true" ItemStyle-CssClass="code">
                        <ItemTemplate>
                            <asp:Label ID="lblIOWCode" runat="server" Text='<%# Eval("IOWCode") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="IOW" Visible="true" ItemStyle-CssClass="wrap">
                        <ItemTemplate>
                            <asp:Label ID="lblIOwName" runat="server" Text='<%# Eval("IOWDescription") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Temp" Visible="true" HeaderStyle-CssClass="center" ItemStyle-CssClass="check">
                        <ItemTemplate>
                            <asp:CheckBox ID="lblTempIOW" runat="server" Checked='<%# Eval("IsTemproryIOW") %>' Enabled="false" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Level 1" Visible="true" ItemStyle-CssClass="wrap">
                        <ItemTemplate>
                            <asp:Label ID="lblL1Desc" runat="server" Text='<%# Eval("L1Desc") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Level 2" Visible="true" ItemStyle-CssClass="wrap">
                        <ItemTemplate>
                            <asp:Label ID="lblL2Desc" runat="server" Text='<%# Eval("L2Desc") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Level 3" Visible="true" ItemStyle-CssClass="wrap">
                        <ItemTemplate>
                            <asp:Label ID="lblL3Desc" runat="server" Text='<%# Eval("L3Desc") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Level 4" Visible="true" ItemStyle-CssClass="wrap">
                        <ItemTemplate>
                            <asp:Label ID="lblL4Desc" runat="server" Text='<%# Eval("L4Desc") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </asp:Panel>
</div>
</asp:Content>
