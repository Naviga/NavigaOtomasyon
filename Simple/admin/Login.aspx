<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Ws.admin.Login" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Finex Media Bilişim - Admin Paneli</title>
    <link href="css/admin.css" rel="stylesheet" type="text/css" />
    <script src="/js/jquery-1.8.3.min.js"></script>
    <script src="/js/jquery.center.js"></script>
    <script>
        if (window.location !== window.parent.location) {
            // The page is in an iframe

            window.top.location.href = "/admin";
        } else {
            // The page is not in an iframe
        }
    </script>
</head>

<body style="background-image: url('/admin/css/img/bgPat.gif'); background-repeat: repeat;">
    <form id="form1" runat="server" defaultfocus="txtKullaniciAdi" defaultbutton="btnGiris">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
        <div style="left: 10px; top: 10px; position: absolute; width: auto; height: auto">
            <a href="http://www.finexmedia.com" target="_blank">
                <img src="/admin/css/img/webLogo.png" width="250" />
            </a>

        </div>
        <div id="dvLoginForm" style="width: 243px; height: 86px; background-image: url(/admin/css/img/adminLoginBg.png); background-position: center center; background-repeat: no-repeat; padding: 70px;">
            <table cellpadding="4" cellspacing="0" align="center">
                <tr>
                    <td>
                        <telerik:RadTextBox ID="txtKullaniciAdi" runat="server" MaxLength="60" EmptyMessage="Kullanıcı adı" Skin="MetroTouch"></telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <telerik:RadTextBox ID="txtSifre" runat="server" TextMode="Password" MaxLength="60" EmptyMessage="Şifre" Skin="MetroTouch"></telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <telerik:RadButton ID="btnGiris" runat="server" Text="Giriş" OnClick="btnGiris_Click" Skin="MetroTouch"></telerik:RadButton>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Label ID="lbluyari" runat="server" ForeColor="Red" Font-Italic="true" Font-Size="9pt" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
    <script>
        $("#dvLoginForm").center(true);

        //alert($(this).parent().nodeName);

        ////if (window.top) {
        ////    window.top.location.href = "/admin";
        ////}

        
    </script>
</body>
</html>
