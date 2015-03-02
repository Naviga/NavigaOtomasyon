<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uscIletisimSayfasi.ascx.cs" Inherits="Ws.usc.uscIletisimSayfasi" %>
<%@ Import Namespace="BLL" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Entity" %>
<%@ Register Src="~/usc/uscIletisimFormu.ascx" TagPrefix="uc1" TagName="uscIletisimFormu" %>


<% enSiteHaritasi sayfa = SayfaId == 0 ? bllSiteHaritasi.SayfaGetirFiziksel(SayfaYolu) : bllSiteHaritasi.SayfaGetir(SayfaId); %>

<% List<enSiteHaritasi> altSayfalar = bllSiteHaritasi.AktifAltSayfalariGetirList(sayfa.Id); %>
<% List<enIcerikResim> resimler = bllIcerikResimleri.ResimleriGetir(sayfa.Id, true); %>

<% if (sayfa.Icerik.xBosMu() && resimler.Count == 0 && altSayfalar.Count > 0)
   {
       Response.Redirect(altSayfalar[0].Url);
   } %>
<script>
    var _sayfaId = '<%= sayfa.Id %>'; 
</script>

<% if (!IsPostBack) SiteMapOlustur(sayfa); %>

<%---------------------------------BAŞLIK--%>

<% if (sayfa.BaslikAlani)
   {%>
<div class="page-header row">
    <div class="large-16 column">
        <h1 id='hPageTitle<%= sayfa.Id %>'>
            <%= sayfa.Adi %>
        </h1>
    </div>
</div>
<%} %>

<%---------------------------------SİTE HARİTASI--%>

<% if (sayfa.SayfaYolu || sayfa.PaylasimAlani)
   {%>
<div class="page-breadcrumbs-share row">
    <% if (sayfa.SayfaYolu)
       {%>
    <div class="page-breadcrumbs large-12 column">
        <asp:Literal ID="lblSiteMap" runat="server" />
    </div>
    <%} %>
    <% if (sayfa.PaylasimAlani)
       {%>
    <div id="dvShare" class="page-share large-4 column"></div>
    <%} %>
</div>
<%} %>

<%---------------------------------ALT SAYFALAR--%>
<div class="row">


    <% if (sayfa.SayfaMenu && altSayfalar.Count > 0)
       {%>

    <div class="large-4 column">
        <ul class="side-nav">
            <% int? parent = null;
               foreach (enSiteHaritasi altSayfa in altSayfalar)
               {
                   parent = altSayfa.Parent;
            %>

            <li <%= altSayfa.Url == sayfa.DisplayUrl ? " class='active' " : "" %>><a href='<%= altSayfa.Url %>'><%= altSayfa.Adi %></a></li>

            <% } %>
        </ul>
    </div>
    <div class="large-12 column">

        <%}
       else
       {%>
        <div class="large-16 column">
            <%} %>

            <%---------------------------------CAROUSEL--%>

            <% if (bllGenelAyarlar.GenelAyarGetir(enEnumaration.enmGenelAyarlar.GmapKullanimi).Icerik.xToBooleanDefault())
               {%>
            <div class="page-map row">
                <div class="large-16 column">
                    <div id="map-canvas">
                    </div>
                </div>
            </div>
            <%} %>
            <% bool contactForm = bllGenelAyarlar.GenelAyarGetir(enEnumaration.enmGenelAyarlar.IletisimFormuKullanimi).Statu; %>
            <div class="page-content row">
                <% if (contactForm)
                   {%>
                <div id="dvContactForm" class="large-9 column">
                    <uc1:uscIletisimFormu runat="server" ID="uscIletisimFormu" />
                </div>
                <%} %>
                <div id='dvPageContent<%= sayfa.Id %>' class='<%= contactForm ? "large-7":"large-16" %> column '>
                    <%= sayfa.Icerik %>
                </div>

            </div>

            <% if (resimler.Count > 0)
               {%>
            <div class="page-pictures row">

                <div class="large-16 column">
                    <% if (sayfa.FotoBaslik.xBosMu() == false)
                       {%>
                    <h3 id='hPagePictureTitle<%= sayfa.Id %>'><%= sayfa.FotoBaslik %></h3>
                    <%} %>

                    <div class="row">
                        <% int i = 0; foreach (enIcerikResim resim in resimler)
                           {%>

                        <div class="small-4 large-2 columns <%= i == resimler.Count-1 ? " end" : "" %>">
                            <div class="imgThumb th" style="background-image: url(<%= resim.Orta %>)">
                                <a class="picture-gallery" href='<%= resim.Buyuk %>'></a>
                            </div>
                        </div>


                        <% i++;
                           } %>
                    </div>
                </div>
            </div>
            <%} %>
        </div>
    </div>
