<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.Master" AutoEventWireup="true"
    CodeBehind="LisansBilgileri.aspx.cs" Inherits="Ws.admin.LisansBilgileri" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table border="0" cellpadding="4" cellspacing="0">
        <tr>
            <td>Yayın Tarihi
            </td>
            <td>
                <asp:TextBox ID="txtYayinTar" runat="server" />
            </td>
        </tr>
        <tr>
            <td>Yayın Süre
            </td>
            <td>
                <asp:TextBox ID="txtYayinSure" runat="server" />
            </td>
        </tr>
        <tr>
            <td>Yayın Durumu
            </td>
            <td>
                <asp:LinkButton ID="lnkYayinDurumuDegistir" runat="server" OnClick="lnkYayinDurumuDegistir_OnClick">
                    <asp:Image ID="imgYayinDurum" runat="server" />
                </asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="right">
                <asp:Button ID="btnGuncelle" Text="Güncelle" runat="server" OnClick="btnGuncelle_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
