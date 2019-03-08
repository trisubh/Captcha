<%@ Page Title="About" Async="true" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="Captacha.About" %>

<%@ Register Assembly="BotDetect" Namespace="BotDetect.Web.UI"
    TagPrefix="BotDetect" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(function () {
             let mode = '<%=Captacha.CaptcahaMode.mode %>';
            $("#refreshCaptcha").click(function () {
               
                $.ajax({
                    type: "POST",
                    url: `Captcha.aspx/${mode}`,
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        $('#MainContent_Image2').attr('src', `data:image/png;base64,${data.d}`);
                    },
                    error: function (data) {
                        alert('failed');
                    }
                });
            });

            
            if (mode === 'text') {
                $("#pronounce").show();
            }
            $("#pronounce").click(function () {
                $.ajax({
                    type: "POST",
                    async: true,
                    url: `Captcha.aspx/pronounce`,
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                    },
                    error: function (data) {
                        alert('failed');
                    }
                });
            });

        })

    </script>
    <h2><%: Title %>.</h2>
    <h3>Your application description page.</h3>
    <p>Use this area to provide additional information.</p>


    <table>
        <tr>
            <td colspan="2">User Registration  
            </td>
        </tr>
        <tr>
            <td>Full Name  
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtFullName"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Email Id  
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtEmail"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>User Name  
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtUserName"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Password  
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtPassword" TextMode="Password"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Verification Code  
            </td>
            <td>
                <div style="display: flex;">
                    <asp:Image ID="Image2" runat="server" Height="55px" ImageUrl="~/Captcha.aspx" Width="186px" />
                    <br />

                    <%--   <input type="button" id="refreshCaptcha"  /> --%>
                    <img src="images/images.jfif" id="refreshCaptcha" style="width: 11%; height: 1%" />
                    <img src="images/speaker.jfif" id="pronounce" style="width: 11%; height: 1% ;display:none" />
                </div>
                <asp:Label runat="server" ID="lblCaptchaMessage"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>Enter Verifaction Code  
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtVerificationCode"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button runat="server" ID="btnSubmit" Text="Submit" OnClick="Button1_Click" />
            </td>
        </tr>
    </table>
</asp:Content>



