<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Login.aspx.cs" Inherits="Login" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="Panel1" runat="server" Height="97%"  width="1286px"  CssClass="XSmall" HorizontalAlign="Center"  > 
      
    <table style="width: 1284px; height: 79%" bgcolor="White">
        <tr>
            <td style="width: 74%" bgcolor="White"  >
                <br />
             <%--   <h2 font-style: italic; color: #e8f6f6"></h2>--%>

            </td>
            <td style="width: 290px" bgcolor="White" >
                <asp:Panel ID="Panel3" runat="server" Height="365px" Width="328px" CssClass="XSmall"
                    BackColor="white">
                <table style="background-color:White; height: 49px; width:316px" border="2px" >
                    <tr>
                        <td style="width: 439px">
                            <asp:Label ID="lblLogin" runat="server" Height="20px" Text="Login" Font-Size = "Large"
                                Width="69px" ForeColor="#000080"  />
                        </td>
                    </tr>
                </table>
                <table border="2px" style="background-color:White;width: 316px">
                    <tr>
                        <td style="width:  86px" align="center" >
                            <asp:Label ID="lblUserName" runat="server" Text="UserName"  ForeColor="#000080"></asp:Label>
                        </td>
                        <td style="width:  160px" align="center" >
                           <asp:TextBox ID="txtUsername" runat="server" Placeholder="UserName" Width="190px"
                                                                         height="15px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:  86px" align="center" >
                            <asp:Label ID="lblPwd" runat="server" Text="Password"  ForeColor="#000080"></asp:Label>
                        </td>
                        <td style="width:  160px" align="center" >
                           <asp:TextBox ID="txtPwd" runat="server" TextMode="Password" Placeholder="Password"
                                            Width="190px" height="15px"    ></asp:TextBox>
                        </td>
                    </tr>
                    <tr><td style="width: 86px"></td><td style="width: 160px"></td></tr>
                    <tr>
                        <td style="width:  86px" align="center"  >
                           <asp:Button ID="btnLogin" runat="server" OnClick="btnLogin_Click" Text="Login" 
                                                        ForeColor="#000080"  Width="56px" height="20px"/>
                        </td>
                        <td style="width:  160px" align="center" >
                           <asp:LinkButton ID="LinkButton1" runat="server" 
                                 OnClick="lnkForgot_Click" ForeColor="#000080">Forgot Password</asp:LinkButton>
                        </td>
                    </tr>
                </table>
                </asp:Panel>
            </td>
            
        </tr>
      </table> 
       </asp:Panel>
<%--<asp:Panel ID="Panel2" runat="server" Height="97%"  width="50%"  Font="Arial"   Font-Size="Small" HorizontalAlign="Center">
      <table>--%>
                <%--<asp:Panel ID="pnlogin" runat="server" Height="50%" Width = "50%">--%>
                  
                   <%-- <div class="rightLogin">
                        <div class="cont" style="height: 495px; margin-top: 0px">
                            &nbsp;<table style="height: 346px">--%>
                                <%--<tr>
                                    <td>
                                        <img src="Images/login_topbg.jpg" alt="" class="fl" style="width: 392px; height: 12px;
                                            margin-left: 0px" />
                                        <div class="boxMid">
                                            <div class="pad10">
                                                <div class="txtBoxs" align="left" style="width: 387px; height: 262px; border-right-style: groove;
                                                    border-left-style: groove;">
                                                    <div class="clr">
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        &nbsp;
                                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/login.jpg" Width="80px"
                                                            ImageAlign="Middle" Height="36px" />
                                                     </div>
                                                    <table style="width: 380px; height: 226px">
                                                        <tr>
                                                            <td style="width: 73px; height: 54px;">
                                                                <asp:Label ID="lblUserName" runat="server" Text="UserName" Font-Size="Small"></asp:Label>
                                                            </td>
                                                            <td style="height: 54px">
                                                                <asp:TextBox ID="txtUsername" runat="server" Placeholder="UserName" Width="246px"
                                                                    Font-Size="X-Small"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 73px; height: 45px;">
                                                                <asp:Label ID="lblPwd" runat="server" Text="Password" Font-Size="Small"></asp:Label>
                                                            </td>
                                                            <td style="height: 45px">
                                                                <asp:TextBox ID="txtPwd" runat="server" TextMode="Password" Placeholder="Password"
                                                                    Width="244px" Font-Size="X-Small"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 73px; height: 24px;">
                                                            </td>
                                                            <td style="width: 75px; height: 24px;">
                                                                <asp:Button ID="btnLogin" runat="server" OnClick="btnLogin_Click" Text="Login" 
                                                                    Font-Size="X-Small" Width="56px" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 41px">
                                                            </td>
                                                            <td style="width: 70px; height: 41px;">
                                                                <asp:LinkButton ID="LinkButton1" runat="server" 
                                                                    OnClick="lnkForgot_Click" CssClass="forgotpwd">Forgot Password</asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <img src="Images/login_bottombg.jpg" alt="" class="fl" style="width: 388px; height: 11px;"/>
                                                   </div>
                                            </div>
                                        </div>
                                       </td>
                                </tr>--%>
                        <%--</div>
                    </div>--%>
                <%--</asp:Panel>--%>
      <%--</table>--%>
       
 <%-- </asp:Panel>--%>
   </asp:Content>
   