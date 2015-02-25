<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="Ws.admin.Default" %>

<%@ Import Namespace="BLL" %>
<%@ Import Namespace="Entity" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="lblLisansUyari" runat="server" ForeColor="Red" Font-Bold="true" Font-Size="Larger" />
    <br />
    <div class="panel">
        <p>
            <b>Lisans Tarihi :</b>
            <%= bllGenelAyarlar.GenelAyarGetir(enEnumaration.enmGenelAyarlar.YayinTarihi).Icerik.xToDateTimeDefault().ToShortDateString() %>
            <br />
            <b>Lisans Süreniz :</b>
            <%= bllGenelAyarlar.GenelAyarGetir(enEnumaration.enmGenelAyarlar.YayinSuresi).Icerik %>
            Yıl
        <br />
            <b>Lisans Bitiş Tarihi :</b>
            <%= bllGenelAyarlar.GenelAyarGetir(enEnumaration.enmGenelAyarlar.YayinTarihi).Icerik.xToDateTimeDefault().Date.AddYears(bllGenelAyarlar.GenelAyarGetir(enEnumaration.enmGenelAyarlar.YayinSuresi).Icerik.xToIntDefault()).Date.AddDays(-1).ToShortDateString()%>
        </p>
    </div>
    <div id="dvAnalytics">
        <%--<uc1:uscAnalytics runat="server" id="uscAnalytics" />--%>
    </div>

</asp:Content>
