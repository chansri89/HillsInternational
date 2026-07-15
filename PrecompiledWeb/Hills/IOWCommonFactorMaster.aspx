<%@ page title="" language="C#" masterpagefile="~/MasterPage1.Master" autoeventwireup="true" inherits="IOWCommonFactorMaster, App_Web_sx5sst5a" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


  <asp:Label ID="lblCRAMCommonFactorMaster" runat="server" Align= "center" Text="IOW Common Factor Master" 
         width="787px"  Font-Size ="12pt" Font-Bold="True" style="text-align: center"></asp:Label>

    <asp:Panel ID="pnlPendind" runat="server" Height="28px" Width="647px" CssClass="XSmall">
        <table style="height: 22px; width: 627px;">
            <tr>
                      <td  style="width: 70px">
                <asp:Label ID="lblCompany" runat="server" Text="Company" Visible="true"
                  Font-Bold="true"  ></asp:Label>
           </td>
             <td style="width: 100px" >
                <asp:DropDownList ID="ddlCompany"  runat="server"  Visible="true"
                   DataTextField="CompanyName" DataValueField="CompanyId"           Width="248px"  AutoPostBack="true" 
                     OnSelectedIndexChanged="ddlCompanyChanged" Height="16px"  >
                 </asp:DropDownList>
            </td>
                            
            </tr>
               
        </table>
    </asp:Panel>

    <asp:panel ID="Pnlgv" runat="server" Width="883px" Height="360px" CssClass="XSmall"      GroupingText="Common Factor Grid">
        <div id="divCRAMCommonFactor" runat="server"     style="overflow:auto; height:340px; width:863px">

    <asp:GridView ID="GrdIOWCommonFactor" runat="server" CellPadding="3" 
            Width="833px"  AutoGenerateColumns="False" 
            Height="16px" GridLines="Vertical" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
            OnRowCommand="GrdIOWCommonFactor_RowCommand">

        <EditRowStyle Font-Size="XX-Small" />
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" 
            HorizontalAlign="Left" />
        <AlternatingRowStyle BackColor="#DCDCDC" />
       
        <Columns>
            <asp:TemplateField HeaderText="CompanyId"  Visible="false">
            <ItemTemplate>
            <asp:Label ID="lblCompanyId" runat="server" Text='<%# Eval("CompanyId") %>' ></asp:Label></ItemTemplate>
             <HeaderStyle Width="10px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Comm Fact Id"  Visible="false">
            <ItemTemplate>
            <asp:Label ID="lblIOWCommonFactorId" runat="server" Text='<%# Eval("IOWCommonFactorId") %>' ></asp:Label></ItemTemplate>
             <HeaderStyle Width="10px" />
            </asp:TemplateField>
           <asp:TemplateField HeaderText="Comm Fact "  Visible="false">
            <ItemTemplate>
            <asp:Label ID="lblIOWCommonFactor" runat="server" Text='<%# Eval("IOWCommonFactor") %>' ></asp:Label></ItemTemplate>
             <HeaderStyle Width="10px" />
            </asp:TemplateField>

          <asp:TemplateField HeaderText="IOW Comm Factor" Visible="true" >
            <ItemTemplate>  
            <asp:LinkButton ID="lnkCommonFactorName" Width="340px" runat ="server" CommandArgument='<%#Eval("IOWCommonFactorId")%>'
             CommandName ="selectCommonFactor" Text ='<%#Eval("IOWCommonFactor") %>'></asp:LinkButton>
              </ItemTemplate> <HeaderStyle Width="340px" HorizontalAlign="Center"/>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Seq #" Visible="true">
            <ItemTemplate>
            <asp:Label ID="lblSequenceNumber" runat="server"  Width="60px" Text='<%# Eval("SequenceNumber") %>' ></asp:Label></ItemTemplate>
              <HeaderStyle Width="60px" HorizontalAlign="Center"/> <ItemStyle HorizontalAlign="Right"/>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Seq Group" Visible="true">
            <ItemTemplate>
            <asp:Label ID="lblSequenceGroup" runat="server" Text='<%# Eval("SequenceGroup") %>' Width="60px"></asp:Label></ItemTemplate>
              <HeaderStyle Width="60px" HorizontalAlign="Center"/> <ItemStyle HorizontalAlign="Right"/>
               </asp:TemplateField>

               <asp:TemplateField HeaderText="Fact %" Visible="true">
             <ItemTemplate>
            <asp:Label ID="lblFactorPercentage" runat="server" Text='<%# Eval("FactorPercentage") %>' Width="80px"></asp:Label></ItemTemplate>
                <HeaderStyle Width="80px" HorizontalAlign="Center" /> <ItemStyle HorizontalAlign="Right"/>
            </asp:TemplateField>
               <asp:TemplateField HeaderText="Eff. %" Visible="true">
             <ItemTemplate>
            <asp:Label ID="lblEffectivePercentage" runat="server" Text='<%# Eval("EffectivePercentage") %>' Width="80px"></asp:Label></ItemTemplate>
                <HeaderStyle Width="80px" HorizontalAlign="Center" /> <ItemStyle HorizontalAlign="Right"/>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Is Active">
             <ItemTemplate>    <asp:CheckBox ID="chkActive" Width="60px" runat="server" Checked='<%# Eval("IsActive") %>'
            Enabled="false"></asp:CheckBox>         </ItemTemplate>
               <HeaderStyle Width="60px" HorizontalAlign="Center"/>     <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            
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
    <asp:panel ID="pnlAdd" runat="server" Width="880px" CssClass="XSmall" GroupingText="Add Common Factor" 
            style="margin-top: 0px" Height="165px">
    <table style="width: 99%; height: 46px;" >
            <tr>
 
                <td  style="width: 326px; ">
                    <asp:Label ID="lblIOWCommonFactorName" runat="server" Text="Comm. Fact. Name"  
                        Font-Bold="True"></asp:Label>
                </td>
                <td style="width: 237px">
                    <asp:TextBox ID="txtIOWCommonFactor" runat="server"  TextMode="MultiLine"
                         height="29px" width="308px"></asp:TextBox>
                </td>
 
                <td style="width: 110px; ">
                    <asp:Label ID="lblSequenceNumber" runat="server" Text="Seq. No"  
                        Font-Bold="True"></asp:Label>
                </td>
                <td style="width: 137px">
                    <asp:TextBox ID="txtSequenceNumber" runat="server" height="18px" width="37px"></asp:TextBox>
                </td>
                  <td  style="width: 110px; ">
                    <asp:Label ID="lblFactorPercentage" runat="server" Text="Factor %"  
                        Font-Bold="True"></asp:Label>
                </td>
                <td style="width: 100px" >
                    <asp:TextBox ID="txtFactorPercentage" runat="server" height="18px" width="47px"></asp:TextBox>
                </td>
                 <td  style="width: 110px; ">
                    <asp:Label ID="lblEffectivePercentage" runat="server" Text="Eff. %"    Font-Bold="True"></asp:Label>
                </td>
                <td style="width: 55px" class="style23">
                    <asp:TextBox ID="txtEffectivePercentage" runat="server" height="18px" width="50px"></asp:TextBox>
                </td>

                <td style="width: 36px">
                    <asp:CheckBox ID="chkIsActive" runat="server"  Text="Is Active" Visible="False" />
                </td>
                <td>
                    <asp:Button ID="btnSave" runat="server" Text="Save"  onclick="btnSave_Click" />
                </td>
                <td  style="width: 64px">
                    <asp:Button ID="btnClear" runat="server" Text="Clear"  onclick="btnClear_Click"  />
                </td>
                                <td  style="width: 35px">
                    <asp:TextBox ID="txtIOWCommonFactorId" runat="server"  Visible="false"
                       height="18px" width="16px"></asp:TextBox>
                </td>
                </tr>
               
        </table>
    </asp:panel>
    <%--</td></tr>
    </table>--%>
    <%--</asp:Panel>--%>

</asp:Content>

