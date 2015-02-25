<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uscAdminUstMenu.ascx.cs"
    Inherits="Ws.usc.uscAdminUstMenu" %>
<div class="adminUst">
    <table border="0" cellpadding="4" cellspacing="0">
        <tr>
            <td style="border-right:1px solid silver;">
                <a href="/admin/Default.aspx">
                    <img src="/admin/css/img/home.png" width="36px" height="36px" /><br />
                    Anasayfa</a>
            </td>
            <td style="border-right:1px solid silver;">
                <a href="/admin/Admin.aspx">
                    <img src="/admin/css/img/user.png" width="36px" height="36px" /><br />
                    Kullanıcı</a>
            </td>
            <%--<td style="border-right:1px solid silver;">
                <a href="/admin/EpostaTalep.aspx">
                    <img src="/admin/css/img/mail1.png" width="36px" /><br />
                    E-Posta</a>
            </td>
            <td style="border-right:1px solid silver;">
                <a href="/admin/Yardim.aspx">
                    <img src="/admin/css/img/help.png" width="36px" height="36px" /><br />
                    Yardım</a>
            </td>--%>
            <td>
                <a href="/Default.aspx" target="_blank">
                    <img src="/admin/css/img/web.png" width="36px" height="36px" /><br />
                    Web Siteniz</a>
            </td>
        </tr>
    </table>
</div>
