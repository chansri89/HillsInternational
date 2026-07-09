<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.master" AutoEventWireup="true" CodeFile="TenderIOWMappingCost.aspx.cs" Inherits="TenderIOWMappingCost" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="Panel2" runat="server" Height="16px" Width="870px" CssClass="XSmall"> </asp:Panel>
<asp:Label ID="lblStatMas" runat="server" Align= "center" Text="Tender-IOW Mapping Cost for Selected Year Month" 
         width="549px" Font-Size ="12pt" Font-Bold="True"   
        style="text-align: center" Height="26px"></asp:Label>
<div style="overflow:auto; height: 664px;">
  
    <asp:panel ID="pnlAdd" runat="server" Width="1061px" 
        GroupingText="Select Rate Year Month" CssClass="XXSmall"  Height="59px">
    <table style="width: 99%; height: 36px;"  align="left">
            <tr>
                                <td class="style28" style="width: 70px">
                <asp:Label ID="lblCompany" runat="server" Text="Company" Visible="true"
                  Font-Bold="true" ></asp:Label>
           </td>
             <td style="width: 100px" class="style21">
                <asp:DropDownList ID="ddlCompany"  runat="server"  Visible="true"
                    DataTextField="CompanyName" DataValueField="CompanyId" Font-Size="X-Small"
                    Width="184px"  AutoPostBack="true" 
                     OnSelectedIndexChanged="ddlCompanyChanged" Height="16px"  >
                 </asp:DropDownList>
            </td>
             <td >
                <asp:Label ID="lblClients" runat="server" Text="Clients" Visible="true"
                  Font-Bold="true"  ></asp:Label>
           </td>
             <td  >
                <asp:DropDownList ID="ddlClient"  runat="server"  Visible="true" AutoPostBack="true" 
                     OnSelectedIndexChanged="ddlClientChanged"  Font-Size="X-Small"
                    DataTextField="ClientName" DataValueField="ClientCode"
                    Width="158px"  Height="16px"  >
                 </asp:DropDownList>
            </td>


         <td>
                <asp:Label ID="lblProject" runat="server" Text="Project" Visible="true"
                  Font-Bold="true"  ></asp:Label>
           </td>
             <td>
                <asp:DropDownList ID="ddlProject"  runat="server"  Visible="true"
                    DataTextField="ProjectName" DataValueField="ClientProjectId" Font-Size="X-Small"
                    
                   Width="170px" height="21px"  >
                 </asp:DropDownList>
            </td>
 
                <td style="width: 229px; text-align: left;">
                    <asp:Label ID="lblRateMonth" runat="server" Text="Rate Year Month" 
                        Font-Bold="True"></asp:Label>
                </td>
                <td style="width: 129px">
                    <asp:DropDownList ID="ddlForYearMonth" runat="server"  MaxLength="8" DataTextField="ForYearMonth" DataValueField="ForYearMonth" 
                      Font-Size="X-Small"  ></asp:DropDownList>
                </td>
        <td  style="width: 70px">
                <asp:Label ID="lblRegion" runat="server" Text="Region" Visible="true"
                  Font-Bold="true"   ></asp:Label>
           </td>
             <td style="width: 100px" class="style21">
                <asp:DropDownList ID="ddlRegion"  runat="server"  Visible="true" 
                    DataTextField="Region" DataValueField="Region"  Style="font-size: X-Small" 
                     Width="136px"    Height="16px"  >
                 </asp:DropDownList>
            </td>                              
                <td style="width: 129px">
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" 
                        onclick="btnUpdate_Click" Font-Size="X-Small" />
                </td>
                </tr>
               
        </table>
    </asp:panel>
    
    </div>

</asp:Content>

