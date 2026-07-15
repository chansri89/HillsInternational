<%@ page title="" language="C#" masterpagefile="~/MasterPage1.Master" autoeventwireup="true" inherits="ClientProject, App_Web_hmntzrlf" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <style type="text/css">
    .WrapTextColumn {
        white-space: normal;
        word-break: break-all; /* Ensures long words without spaces wrap */
        /*width: 60px;  Set a fixed width */
    }
        .style37
        {
            width: 136px;
        }
        .style38
        {
            width: 108px;
        }
    </style>

  <asp:Label ID="lblClientMaster" runat="server" Align= "center" Text="Client Project Master Entry" 
        width="787px"  Font-Size ="12pt" Font-Bold="True"    style="text-align: center"></asp:Label>
 
    <div style="overflow:auto; height: 471px; width: 1202px;">

    <asp:Panel ID="pnlPendind" runat="server" Height="28px" Width="720px" CssClass="XSmall">
        <table >
            <tr>
                <td >
                <asp:Label ID="lblCompany" runat="server" Text="Company" 
                  Font-Bold="true"  ></asp:Label>
           </td>
             <td  >
                <asp:DropDownList ID="ddlCompany"  runat="server"  
                    DataTextField="CompanyName" DataValueField="CompanyId"        Width="248px"  AutoPostBack="true" 
                     OnSelectedIndexChanged="ddlCompanyChanged" Height="19px"  >
                 </asp:DropDownList>
            </td>
                <td  >
                <asp:Label ID="lblClntCode" runat="server" Text="Client" Font-Bold="true"  ></asp:Label>
           </td>
             <td style="width: 100px">
                <asp:DropDownList ID="ddlClient"  runat="server" 
                   DataTextField="ClientName" DataValueField="ClientCode" Width="248px"   Height="19px"  >
                 </asp:DropDownList>
            </td>
                <td style="width: 62px" >
                         <asp:Button ID="btnGo" runat="server" Height="21px" onclick="btnGo_Click"  Text="Go" Width="43px" />
                 </td>     
 
          
                             
            </tr>
               
        </table>
    </asp:Panel>

    <asp:panel ID="Pnlgv" runat="server" Width="1187px" Height="280px" CssClass="XXSmall"
            GroupingText="Client Project" >
        <div id="divClient" runat="server"  class="WrapTextColumn"
            style="overflow:auto; height:262px; width:1169px">

    <asp:GridView ID="GrdProjMaster" runat="server" CellPadding="3" Width="1158px"  AutoGenerateColumns="False" 
            Height="16px" GridLines="Vertical" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
            OnRowCommand="GrdClient_RowCommand">
 
        <EditRowStyle Font-Size="XX-Small" />
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" 
            HorizontalAlign="Left" />
        <AlternatingRowStyle BackColor="#DCDCDC" />

       
        <Columns>
            <asp:TemplateField HeaderText="ClientCode"  Visible="false">
            <ItemTemplate>
            <asp:Label ID="lblClientCode" runat="server" Text='<%# Eval("ClientCode") %>' Width="10px"></asp:Label></ItemTemplate>
             <HeaderStyle Width="10px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Cli Proj Id"  Visible="false">
            <ItemTemplate>
            <asp:Label ID="lblClientProjectId" runat="server" Text='<%# Eval("ClientProjectId") %>' Width="10px"></asp:Label></ItemTemplate>
             <HeaderStyle Width="10px" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Sector SubGroup Id"  Visible="false">
            <ItemTemplate>
            <asp:Label ID="lblProjectSectorSubGroupId" runat="server" Text='<%# Eval("ProjectSectorSubGroupId") %>' Width="10px"></asp:Label></ItemTemplate>
             <HeaderStyle Width="10px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Sector GroupName" >
             <ItemTemplate>
            <asp:Label ID="lblProjectSectorGroupName" runat="server" Text='<%# Eval("ProjectSectorGroupName") %>'   Width="70px"></asp:Label></ItemTemplate>
                <HeaderStyle Width="70px" />
            </asp:TemplateField>
 
            <asp:TemplateField HeaderText="Sector Class" >
             <ItemTemplate>
            <asp:Label ID="lblProjectSectorClass" runat="server" Text='<%# Eval("ProjectSectorClass") %>'   Width="70px"></asp:Label></ItemTemplate>
                <HeaderStyle Width="70px" />
            </asp:TemplateField>
                       
            <asp:TemplateField HeaderText="Project Code" >
            <ItemTemplate>
            <asp:Label ID="lblProjectCode" runat="server"  Width="70px" Text='<%# Eval("ProjectCode") %>' ></asp:Label></ItemTemplate>
              <HeaderStyle Width="70px"/>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Proj Name" Visible="false">
            <ItemTemplate>
            <asp:Label ID="lblProjectName" runat="server" Text='<%# Eval("ProjectName") %>' ></asp:Label></ItemTemplate>
              <HeaderStyle Width="50px"/>
               </asp:TemplateField>
         <asp:TemplateField HeaderText="Project Name" >
            <ItemTemplate>  
            <asp:LinkButton ID="lnkCustName" Width="150px" runat ="server" CommandArgument='<%#Eval("ProjectCode")%>'
             CommandName ="selectProject" Text ='<%#Eval("ProjectName") %>'></asp:LinkButton>
              </ItemTemplate> <HeaderStyle Width="150px" />
            </asp:TemplateField>
  
            <asp:TemplateField HeaderText="Proj Location">
             <ItemTemplate>
            <asp:Label ID="lblProjectLocation" runat="server" Text='<%# Eval("ProjectLocation") %>' Width="100px" ></asp:Label></ItemTemplate>
                <HeaderStyle Width="100px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Project City" >
             <ItemTemplate>
            <asp:Label ID="lblProjectCity" runat="server" Text='<%# Eval("ProjectCity") %>'   Width="80px"></asp:Label></ItemTemplate>
                <HeaderStyle Width="80px" />
            </asp:TemplateField>
             <asp:TemplateField HeaderText="StateId" Visible="false">
             <ItemTemplate>
            <asp:Label ID="lblStateId" runat="server" Text='<%# Eval("StateId") %>' Width="25px" ></asp:Label></ItemTemplate>
                <HeaderStyle Width="25px" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText=" State" Visible="true">
             <ItemTemplate>
            <asp:Label ID="lblStateName" runat="server" Text='<%# Eval("StateName") %>'   Width="35px"></asp:Label></ItemTemplate>
                <HeaderStyle Width="35px" />
            </asp:TemplateField>

             <asp:TemplateField HeaderText="Start Date" Visible="true">
             <ItemTemplate>
             <asp:Label ID="lblStartDate" runat="server" Text='<%# Eval("StartDate","{0:dd-MM-yyyy}") %>' Width="60px" ></asp:Label>
           </ItemTemplate>
                <HeaderStyle Width="60px" />
            </asp:TemplateField>

             <asp:TemplateField HeaderText="End Date" >
             <ItemTemplate>
             <asp:Label ID="lblEndDate" runat="server" Text='<%# Eval("EndDate","{0:dd-MM-yyyy}") %>' Width="60px" ></asp:Label>
           </ItemTemplate>
                <HeaderStyle Width="60px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Devi ation " Visible="true">
             <ItemTemplate>
            <asp:Label ID="lblDeviation" runat="server" Text='<%# Eval("DeviationMonths") %>'  Width="30px"></asp:Label></ItemTemplate>
                <HeaderStyle Width="30px" />
            </asp:TemplateField>

          <asp:TemplateField HeaderText="Tender Period" Visible="true">
             <ItemTemplate>
             <asp:Label ID="lblTenderDate" runat="server" Text='<%# Eval("TenderPeriod","{0:dd-MM-yyyy}") %>' Width="60px" ></asp:Label>
           </ItemTemplate>
                <HeaderStyle Width="60px" />
            </asp:TemplateField>

             <asp:TemplateField HeaderText="Constr Start" Visible="true">
             <ItemTemplate>
             <asp:Label ID="lblConstructionStart" runat="server" Text='<%# Eval("ConstructionStart","{0:dd-MM-yyyy}") %>' Width="60px" ></asp:Label>
           </ItemTemplate>
                <HeaderStyle Width="60px" />
            </asp:TemplateField>
                        <asp:TemplateField HeaderText="Constr Compl" Visible="true">
             <ItemTemplate>
             <asp:Label ID="lblConstructionCompleted" runat="server" Text='<%# Eval("ConstructionCompleted","{0:dd-MM-yyyy}") %>' Width="60px" ></asp:Label>
           </ItemTemplate>
                <HeaderStyle Width="60px" />
            </asp:TemplateField>


 
            <asp:TemplateField HeaderText="Is Active">
             <ItemTemplate>    <asp:CheckBox ID="chkActive" Width="30px" runat="server" Checked='<%# Eval("IsActive") %>'
            Enabled="false"></asp:CheckBox>         </ItemTemplate>
               <HeaderStyle Width="30px" />     <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            
     <%--       <asp:CommandField HeaderText="Select" ShowEditButton="True" Visible="true"   CausesValidation="False">
            <HeaderStyle Width="40px" />
            </asp:CommandField>--%>

        </Columns>
        <sortedascendingcellstyle backcolor="#F1F1F1" />
        <sortedascendingheaderstyle backcolor="#0000A9" />
        <sorteddescendingcellstyle backcolor="#CAC9C9" />
        <sorteddescendingheaderstyle backcolor="#000065" />
    </asp:GridView>
    </div>
    <%--</td>
    </tr>--%>
    </asp:panel>
    <br />
    <asp:panel ID="pnlAdd" runat="server" Width="1156px"  CssClass="XXSmall"
            GroupingText="Add Client Project"    Height="102px">
    <table style="width: 98%; height: 82px;" >
            <tr>
                     <td >
                    <asp:Label ID="lblProjectCode" runat="server" Text="Project Code"  
                        Font-Bold="True"></asp:Label>
                </td>
                <td >
                    <asp:TextBox ID="txtProjectCode" runat="server" MaxLength="16"
                        height="19px"></asp:TextBox>
                </td>
                <td >
                    <asp:Label ID="lblProjectName" runat="server" Text="Projet Name"  
                        Font-Bold="True"></asp:Label>
                </td>
                <td >
                    <asp:TextBox ID="txtProjectName" runat="server" MaxLength="256" 
                        TextMode="MultiLine"   height="21px" width="240px"></asp:TextBox>
                </td>
                <td style=" text-align: left; width: 76px;">
                    <asp:Label ID="lblProjectLocation" runat="server" Text="Proj Location"  
                        Font-Bold="false"></asp:Label>
                </td>
                <td class="style37" >
                    <asp:TextBox ID="txtProjectLocation" runat="server"  MaxLength="64"
                        height="19px" width="95px"></asp:TextBox>
                </td>

             <td >
                    <asp:Label ID="lblProjectCity" runat="server" Text="Project City"  
                        Font-Bold="false"></asp:Label>
                </td>
                <td class="style38" >
                     <asp:TextBox ID="txtProjectCity" runat="server"  MaxLength="64"  height="19px" 
                         width="78px"></asp:TextBox>
                </td>
            <td  >
                <asp:Label ID="Label2" runat="server" Text="Sector Group" Font-Bold="true"  ></asp:Label>
           </td>
             <td >
                <asp:DropDownList ID="ddlSectorGroup"  runat="server"  Font-Size="XX-Small"
                   DataTextField="ProjectSectorGroupName" DataValueField="ProjectSectorGroupId" 
                     Width="123px"   Height="19px" AutoPostBack="true" 
                     OnSelectedIndexChanged="ddlSectorGroupChanged" >
                 </asp:DropDownList>
            </td>
             </tr>
            <tr>
               <td  >
                <asp:Label ID="Label1" runat="server" Text="Sector Class" Font-Bold="true"  ></asp:Label>
           </td>
             <td >
                <asp:DropDownList ID="ddlSectorClass"  runat="server"  Font-Size="XX-Small"
                   DataTextField="ProjectSectorClass" DataValueField="ProjectSectorSubGroupId" 
                     Width="118px"   Height="19px"  >
                 </asp:DropDownList>
            </td>
           

             <td >
                    <asp:Label ID="lblState" runat="server" Text="State"  
                        Font-Bold="True"></asp:Label>
                </td>
                  <td  >
                <asp:DropDownList ID="ddlState"  runat="server"  Font-Size="XX-Small"
                   DataTextField="StateName" DataValueField="StateId"   Width="120px"  
                          Height="19px"  >
                 </asp:DropDownList>
                 </td>
 
                <td >
                    <asp:Label ID="lblStartDate" runat="server" Text="Start Date"  
                        Font-Bold="True"></asp:Label>
                </td>
                 <td >
                  <asp:TextBox ID="txtStartDate" runat="server" Text="" Height="19px"
                        Width="100px"></asp:TextBox>
                    <asp:ImageButton ID="imgStartdate" runat="server" ImageUrl="~/Images/Calendar.gif" />
                    <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtStartDate" 
                        runat="server" PopupButtonID="imgStartdate" Format="dd-MM-yyyy"></asp:CalendarExtender>

                </td>
                <td >
                    <asp:Label ID="lblEndDate" runat="server" Text="End Date"  
                        Font-Bold="True"></asp:Label>
                </td>
                 <td class="style37" >
                  <asp:TextBox ID="txtEndDate" runat="server"  Text=""    Width="96px" Height="19px"></asp:TextBox>
                    <asp:ImageButton ID="imgEnddate" runat="server" ImageUrl="~/Images/Calendar.gif" />
                    <asp:CalendarExtender ID="CalendarExtender2" TargetControlID="txtEndDate" 
                        runat="server" PopupButtonID="imgEnddate" Format="dd-MM-yyyy"></asp:CalendarExtender>

                </td>
                <td >
                    <asp:Label ID="lblTenderPeriod" runat="server" Text="Tender Period"  
                        Font-Bold="True"></asp:Label>
                </td>
                 <td class="style38" >
                  <asp:TextBox ID="txtTenderPeriod" runat="server"  Text="" 
                        Width="73px" Height="19px"></asp:TextBox>
                    <asp:ImageButton ID="imgTenderPeriod" runat="server" ImageUrl="~/Images/Calendar.gif" />
                    <asp:CalendarExtender ID="CalendarExtender5" TargetControlID="txtTenderPeriod" 
                        runat="server" PopupButtonID="imgTenderPeriod" Format="dd-MM-yyyy"></asp:CalendarExtender>

                </td>   
     
                  <td  >
                    <asp:TextBox ID="txtClientProjectId" runat="server"  Visible="false"
                        height="18px" width="18px"></asp:TextBox>
                </td>
  </tr>
                 <tr>

                <td>
                    <asp:Label ID="lblConstructionStart" runat="server" Text="Constr Start"  
                        Font-Bold="True"></asp:Label>
                </td>
                 <td >
                  <asp:TextBox ID="txtConstructionStart" runat="server" Text="" 
                        Width="108px" Height="19px"></asp:TextBox>
                    <asp:ImageButton ID="imgConstructionStart" runat="server" ImageUrl="~/Images/Calendar.gif" />
                    <asp:CalendarExtender ID="CalendarExtender3" TargetControlID="txtConstructionStart" 
                        runat="server" PopupButtonID="imgConstructionStart" Format="dd-MM-yyyy"></asp:CalendarExtender>

                </td>
                 <td >
                    <asp:Label ID="lblConstrEnd" runat="server" Text="Constr End"  
                        Font-Bold="True"></asp:Label>
                </td>
                 <td  >
                  <asp:TextBox ID="txtConstructionEndDate" runat="server"  Text="" 
                        Width="94px" Height="19px"></asp:TextBox>
                    <asp:ImageButton ID="imgConstructionEnd" runat="server" ImageUrl="~/Images/Calendar.gif" />
                    <asp:CalendarExtender ID="CalendarExtender4" TargetControlID="txtConstructionEndDate" 
                        runat="server" PopupButtonID="imgConstructionEnd" Format="dd-MM-yyyy"></asp:CalendarExtender>

                </td>
                   <td >
                    <asp:Label ID="lblDeviationMonths" runat="server" Text="Deviation Months"  
                        Font-Bold="True"></asp:Label>
                </td>
                <td class="style37" >
                    <asp:TextBox ID="txtDeviationMonths" runat="server"  MaxLength="16"     
                        height="19px" width="81px"></asp:TextBox>
                </td>            
 
                <td  >
                    <asp:CheckBox ID="chkIsActive" runat="server"  Text="IsActive" Visible="False" />
                </td>
                <td class="style38"></td>
                <td  >
                    <asp:Button ID="btnSave" runat="server" Text="Save" 
                        onclick="btnSave_Click"  />
                </td>
                <td  >
                    <asp:Button ID="btnClear" runat="server" Text="Clear"    onclick="btnClear_Click" />
                </td>
                </tr>
               
        </table>
    </asp:panel>

    </div>
</asp:Content>

