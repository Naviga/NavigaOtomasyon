<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uscStandartSayfa.ascx.cs" Inherits="Ws.usc.uscStandartSayfa" %>
<%@ Import Namespace="BLL" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Entity" %>

<% enSiteHaritasi sayfa = SayfaId == 0 ? bllSiteHaritasi.SayfaGetirFiziksel(SayfaYolu) : bllSiteHaritasi.SayfaGetir(SayfaId); %>

<script>
    var _sayfaId = '<%= sayfa.Id %>'; 
</script>

<% List<enSiteHaritasi> altSayfalar = bllSiteHaritasi.AktifAltSayfalariGetirList(sayfa.Id); %>
<% List<enIcerikResim> resimler = bllIcerikResimleri.ResimleriGetir(sayfa.Id, true); %>
<% List<enSiteHaritasi> tumUrunler = bllSiteHaritasi.UrunleriGetir(sayfa.Id); %>

<%--<% if (sayfa.Icerik.xBosMu() && resimler.Count == 0 && altSayfalar.Count > 0)
   {
       Response.Redirect(altSayfalar[0].Url);
   } %>--%>

<% if (!IsPostBack) SiteMapOlustur(sayfa); %>



<%---------------------------------BAŞLIK--%>

<% if (sayfa.BaslikAlani)
   {%>
<div class="page-header row">
    <div class="large-12 column">
        <h1 id='hPageTitle<%= sayfa.Id %>' <%= SessionManager.Admin!= null ? "class='editable-simple sayfaBasliklar' op='page-title' contenteditable='true'":"" %>>
            <%= sayfa.Adi %>
        </h1>

    </div>
    <hr class="fancy-line" />
</div>
<%} %>

<%---------------------------------SİTE HARİTASI--%>

<% if (sayfa.SayfaYolu || sayfa.PaylasimAlani)
   {%>

<% if (!sayfa.UrunMu)
   {%>

<div class="page-breadcrumbs-share row">
    <% if (sayfa.SayfaYolu)
       {%>
    <div class="page-breadcrumbs large-9 column">
        <asp:Literal ID="lblSiteMap" runat="server" />
    </div>
    <%} %>
    <% if (sayfa.PaylasimAlani)
       {%>
    <div id="dvShare" class="page-share large-3 column"></div>
    <%} %>
</div>
<%} %>

<% } %>
<%---------------------------------ALT SAYFALAR--%>
<div class="row">

    <% if (altSayfalar.Count == 0 && sayfa.Parent != null)
       {
           altSayfalar = bllSiteHaritasi.AktifAltSayfalariGetirList(sayfa.Parent.Value);
       } %>

    <% if ((sayfa.SayfaMenu && altSayfalar.Count > 0))
       { %>

    <% if (!sayfa.UrunMu)
       {%>

    <div class="large-2 column">
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
    <div class="large-10 column">

        <%}
       else
       {%>
        <div class="large-12 column">
            <%} %>

            <%---------------------------------CAROUSEL--%>

            <% if (sayfa.CarouselId != null || SessionManager.Admin != null)
               {%>
            <div class="page-main-picture row">

                <div class="large-12 column">
                    <%= CarouselOlustur(sayfa.CarouselId) %>
                </div>
            </div>
            <%} %>

            <div class="page-content row">
                <div id='dvPageContent<%= sayfa.Id %>' class='large-12 column <%= SessionManager.Admin!= null ? " editable-full' op='page-content' contenteditable='true'":"'" %>'>
                    <%= sayfa.Icerik %>
                </div>
            </div>


            <% if (resimler.Count > 0 || SessionManager.Admin != null)
               {%>
            <div class="page-pictures row">

                <div class="large-12 column">
                    <% if (sayfa.FotoBaslik.xBosMu() == false)
                       {%>
                    <h3 id='hPagePictureTitle<%= sayfa.Id %>' <%= SessionManager.Admin!= null ? "class='editable-simple' op='page-pictures-title' contenteditable='true'":"" %>><%= sayfa.FotoBaslik %></h3>
                    <%} %>

                    <div class="row">
                        <% int i = 0; foreach (enIcerikResim resim in resimler)
                           {%>

                        <div class="Thumb small-4 large-2 columns <%= i == resimler.Count-1 ? " end" : "" %>">
                            <div class="imgThumb th" style="background-image: url(<%= resim.Orta %>)">
                                <a class="picture-gallery" href='<%= resim.Buyuk %>' title='<%= resim.Aciklama %>'></a>
                            </div>
                            <p><%= resim.Baslik %></p>
                        </div>


                        <% i++;
                           } %>
                    </div>
                </div>
            </div>
            <%} %>
        </div>
    </div>

    <% } %>

    <%if (tumUrunler.Count != 0)
      {%>

    <div class="large-10 columns">
        <div class="large-10 columns">
            <div class="page-pictures row">

                <div class="large-10 columns">
                    <% if (sayfa.FotoBaslik.xBosMu() == false)
                       {%>
                    <h3 id='h1' <%= SessionManager.Admin!= null ? "class='editable-simple' op='page-pictures-title' contenteditable='true'":"" %>><%= sayfa.FotoBaslik %></h3>
                    <%} %>

                    <div class="row">
                        <% int i = 0; foreach (enSiteHaritasi urun in tumUrunler)
                           {%>

                        <div class="medium-4 columns">
                            <a class="fancybox" rel="group" href='<%= urun.Url %>'>
                                <img src="<%= urun.FotoOrta %>" alt="" /></a>
                        </div>


                        <% i++;
                           } %>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%} %>
</div>
