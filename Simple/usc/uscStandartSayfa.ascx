<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uscStandartSayfa.ascx.cs" Inherits="Ws.usc.uscStandartSayfa" %>
<%@ Import Namespace="BLL" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Entity" %>

<% enSiteHaritasi sayfa = Session["ürünler_sayfaId"].xToIntDefault() == 0 ? bllSiteHaritasi.SayfaGetirFiziksel(Session["sayfa_yolu"].ToString()) : bllSiteHaritasi.SayfaGetir(Session["ürünler_sayfaId"].xToIntDefault()); %>

<script>
    var _sayfaId = '<%= sayfa.Id %>'; 
</script>

<% List<enSiteHaritasi> altSayfalar = bllSiteHaritasi.AktifAltSayfalariGetirList(sayfa.Id); %>
<% List<enIcerikResim> resimler = bllIcerikResimleri.ResimleriGetir(sayfa.Id, true); %>
<% List<enIcerikResim> tumUrunler = bllIcerikResimleri.TumResimleriGetir(true); %>

<% if (sayfa.Icerik.xBosMu() && resimler.Count == 0 && altSayfalar.Count > 0)
   {
       Response.Redirect(altSayfalar[0].Url);
   } %>

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

            <% if (SessionManager.Admin != null)
               { %>
            <li><a href='#!' class='button tiny expand' onclick='OpenNewNavItem(this)' parentid='<%= parent %>' title='<%= bllDiziler.DiziGetir("Main.AdminMenu.NewPage.ToolTip")%>'><span class='fa fa-plus-circle'></span>&nbsp;<%= bllDiziler.DiziGetir("Main.AdminMenu.NewPage")%></a></li>
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
                <% if (SessionManager.Admin != null)
                   {%>
                <div class='dvSettings'>
                    <a class="iframe has-tip" href="/admin/Carousel.aspx?iframe&id=<%= sayfa.Id %>" title="Slayt / Carousel resim yükle/değiştir" data-tooltip aria-haspopup="true">
                        <img src="/css/img/settings.png" width="32px" /></a>
                </div>
                <%} %>
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
                <% if (SessionManager.Admin != null)
                   {%>
                <div class='dvSettings'>
                    <a class="iframe has-tip" href="/admin/Pages.aspx?iframe&i=1&ai=2&dzid=<%= sayfa.Id %>" title="Resim galerisini yönetin" data-tooltip aria-haspopup="true">
                        <img src="/css/img/settings.png" width="32px" /></a>
                </div>
                <%} %>

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

    <%if (sayfa.UrunMu)
      {%>

    <div class="large-12 columns">
        <div class="large-12 columns">
            <div class="page-pictures row">
                <% if (SessionManager.Admin != null)
                   {%>
                <div class='dvSettings'>
                    <a class="iframe has-tip" href="/admin/Pages.aspx?iframe&i=1&ai=2&dzid=<%= sayfa.Id %>" title="Resim galerisini yönetin" data-tooltip aria-haspopup="true">
                        <img src="/css/img/settings.png" width="32px" /></a>
                </div>
                <%} %>

                <div class="large-12 columns">
                    <% if (sayfa.FotoBaslik.xBosMu() == false)
                       {%>
                    <h3 id='h1' <%= SessionManager.Admin!= null ? "class='editable-simple' op='page-pictures-title' contenteditable='true'":"" %>><%= sayfa.FotoBaslik %></h3>
                    <%} %>

                    <div class="row">
                        <% int i = 0; foreach (enIcerikResim resim in tumUrunler)
                           {%>

                        <div class="medium-4 columns">
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
        </div>
    </div>

    <%} %>
