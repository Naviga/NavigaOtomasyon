<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.Master" AutoEventWireup="true"
    CodeBehind="Yardim.aspx.cs" Inherits="Ws.admin.Yardim" %>

<%@ Register Src="/usc/uscUyari.ascx" TagName="uscUyari" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>
        Yardım dökümanlarınız hazırlanmaktadır...
    </h3>
    <p>
        <span class="info">Her hangi bir soru, görüş veya önerinizi aşağıdaki formu kullanarak
            veya <a href="mailto:destek@finexmedia.com">destek@finexmedia.com</a> adresinden
            bize iletebilirsiniz.</span>
    </p>
    <table border="0" cellpadding="5" cellspacing="0" style="margin: 20px">
        <tr>
            <td>
                Adınız
            </td>
            <td>
                <asp:TextBox ID="txtAd" runat="server" Width="200px" MaxLength="60">
                </asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Soyadınız
            </td>
            <td>
                <asp:TextBox ID="txtSoyad" runat="server" Width="200px" MaxLength="60">
                </asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                E-Posta
            </td>
            <td>
                <asp:TextBox ID="txtEposta" runat="server" Width="200px" MaxLength="60">
                </asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Konu
            </td>
            <td>
                <asp:TextBox ID="txtKonu" runat="server" Width="200px" MaxLength="60">
                </asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Mesaj
            </td>
            <td>
                <asp:TextBox ID="txtMesaj" runat="server" Width="200px" MaxLength="60" TextMode="MultiLine"
                    Height="150px">
                </asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="right">
                <asp:Button ID="btnGonder" runat="server" Text="Gönder" OnClick="btnGonder_Click">
                </asp:Button>
            </td>
        </tr>
    </table>
    <uc1:uscUyari ID="uscUyari1" runat="server" />
</asp:Content>
