<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="TeknikDestek.aspx.cs" Inherits="Ws.TeknikDestek" %>

<%@ Import Namespace="BLL" %>
<%@ Import Namespace="Entity" %>
<%@ Register Src="~/usc/uscTeknikDestekFormu.ascx" TagPrefix="uc1" TagName="uscTeknikDestekFormu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <% enSiteHaritasi sayfa = bllSiteHaritasi.SayfaGetirFiziksel("~/TeknikDestek.aspx"); %>

    <script>
        var _sayfaId = '<%= sayfa.Id %>'; 
    </script>

    <% List<enSiteHaritasi> altSayfalar = bllSiteHaritasi.AktifAltSayfalariGetirList(sayfa.Id); %>
    <% List<enIcerikResim> resimler = bllIcerikResimleri.ResimleriGetir(sayfa.Id, true); %>
    <% List<enSiteHaritasi> tumUrunler = bllSiteHaritasi.UrunleriGetir(sayfa.Id); %>

    <% if (sayfa.Icerik.xBosMu() && resimler.Count == 0 && altSayfalar.Count > 0 && tumUrunler.Count == 0)
       {
           Response.Redirect(altSayfalar[0].Url);
       } %>

    <% if (!IsPostBack) SiteMapOlustur(sayfa); %>
    <div id="dvPageWrapper" class="bg-color-light-gray">


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

        <% } %>
        <%---------------------------------ALT SAYFALAR--%>
        <div class="row">

            <% if (altSayfalar.Count == 0 && sayfa.Parent != null)
               {
                   altSayfalar = bllSiteHaritasi.AktifAltSayfalariGetirList(sayfa.Parent.Value);
               } %>

            <% if ((sayfa.SayfaMenu && altSayfalar.Count > 0))
               { %>

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
                    <%}%>

                    <%---------------------------------CAROUSEL--%>

                    <% if (sayfa.CarouselId != null)
                       {%>
                    <div class="page-main-picture row">

                        <div class="large-12 column">
                            <%= CarouselOlustur(sayfa.CarouselId) %>
                        </div>
                    </div>
                    <%} %>

                    <div class="page-content row">
                        <div id='dvPageContent<%= sayfa.Id %>' class='large-16 column'>
                            <div class="row">
                                <div class="large-8 column">
                                    <uc1:uscTeknikDestekFormu runat="server" ID="uscTeknikDestekFormu" />
                                </div>
                                <div class="large-8 column">
                                    <%= sayfa.Icerik %>
                                </div>
                            </div>
                        </div>
                        <%if (tumUrunler.Count != 0)
                          {%>
                        <div class="large-16 columns">
                            <div class="row">
                                <% int i = 0; foreach (enSiteHaritasi urun in tumUrunler)
                                   {%>

                                <div class="large-2 columns <%= i == tumUrunler.Count-1 ? " end":"" %>">
                                    <a href='<%= urun.Url %>' class="th">
                                        <img src="<%= urun.FotoOrta %>" alt="" />

                                    </a>
                                    <br />
                                    <small><a href='<%= urun.Url %>'>
                                        <%= urun.Adi %></a></small>
                                </div>


                                <% i++;
                               } %>
                            </div>
                        </div>
                        <%} %>
                    </div>
                    <% if (resimler.Count > 0)
                       {%>
                    <div class="page-pictures row">

                        <div class="large-12 column">
                            <% if (sayfa.FotoBaslik.xBosMu() == false)
                               {%>
                            <h3 id='hPagePictureTitle<%= sayfa.Id %>'><%= sayfa.FotoBaslik %></h3>
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
        </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="maincntScript" runat="server">
</asp:Content>
