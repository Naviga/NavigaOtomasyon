using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Policy;
using System.Text;
using System.Web;
using System.Web.Routing;
using DAL;
using Entity;

namespace BLL
{
    public static class bllSiteHaritasi_Blok
    {
        public static void YukseklikGuncelle(enSiteHaritasi_Blok sBlok)
        {
            enBlokPozisyon pozisyon = bllBlokPozisyonlari.Getir(sBlok.PozisyonId);

            if (pozisyon.Master)
            {
                sBlok.SayfaId = 0;
                new dalSiteHaritasi_Blok().YukseklikGuncelle(sBlok);
            }
            else
            {
                new dalSiteHaritasi_Blok().YukseklikGuncelle(sBlok);
            }
        }

        public static void CerceveRengiGuncelle(enSiteHaritasi_Blok sBlok)
        {
            enBlokPozisyon pozisyon = bllBlokPozisyonlari.Getir(sBlok.PozisyonId);

            if (pozisyon.Master)
            {
                sBlok.SayfaId = 0;
                new dalSiteHaritasi_Blok().CerceveRengiGuncelle(sBlok);
            }
            else
            {
                new dalSiteHaritasi_Blok().CerceveRengiGuncelle(sBlok);
            }
        }

        public static void MetinRengiGuncelle(enSiteHaritasi_Blok sBlok)
        {
            enBlokPozisyon pozisyon = bllBlokPozisyonlari.Getir(sBlok.PozisyonId);

            if (pozisyon.Master)
            {
                sBlok.SayfaId = 0;
                new dalSiteHaritasi_Blok().MetinRengiGuncelle(sBlok);
            }
            else
            {
                new dalSiteHaritasi_Blok().MetinRengiGuncelle(sBlok);
            }
        }

        public static void ArkaPlanRengiGuncelle(enSiteHaritasi_Blok sBlok)
        {
            enBlokPozisyon pozisyon = bllBlokPozisyonlari.Getir(sBlok.PozisyonId);

            if (pozisyon.Master)
            {
                sBlok.SayfaId = 0;
                new dalSiteHaritasi_Blok().ArkaPlanRengiGuncelle(sBlok);
            }
            else
            {
                new dalSiteHaritasi_Blok().ArkaPlanRengiGuncelle(sBlok);
            }
        }

        public static void YerSiraGuncelle(enSiteHaritasi_Blok sBlok)
        {
            enBlokPozisyon pozisyon = bllBlokPozisyonlari.Getir(sBlok.PozisyonId);

            if (pozisyon.Master)
            {
                if (PozisyondaVarMi(sBlok))
                {
                    PozisyonSiraGuncelle(sBlok);
                }
                else
                {
                    Ekle(sBlok);
                }
            }
            else
            {
                if (SayfadaVarMi(sBlok))
                {
                    new dalSiteHaritasi_Blok().YerSiraGuncelle(sBlok);
                }
                else
                {
                    Ekle(sBlok);
                }
            }

        }

        public static void PozisyonSiraGuncelle(enSiteHaritasi_Blok sBlok)
        {
            new dalSiteHaritasi_Blok().PozisyonSiraGuncelle(sBlok);
        }

        public static List<enBlok> Getir(int sayfaId, int? pozisyonId = null, bool? statu = null)
        {
            return new dalSiteHaritasi_Blok().Getir(sayfaId, pozisyonId, statu);
        }

        public static List<enBlok> MasterGetir(bool? statu)
        {
            return new dalSiteHaritasi_Blok().MasterGetir(statu);
        }

        public static bool SayfadaVarMi(enSiteHaritasi_Blok sBlok)
        {
            return new dalSiteHaritasi_Blok().SayfadaVarMi(sBlok);
        }

        public static bool PozisyondaVarMi(enSiteHaritasi_Blok sBlok)
        {
            return new dalSiteHaritasi_Blok().PozisyondaVarMi(sBlok);
        }

        public static void Ekle(enSiteHaritasi_Blok blok)
        {
            new dalSiteHaritasi_Blok().Ekle(blok);
        }

        public static List<enBlok> SayfayaEklenmemisleriGetir(int sayfaId, bool? statu = null)
        {
            return new dalSiteHaritasi_Blok().SayfaEklenmemisleriGetir(sayfaId, statu);
        }

        public static void Kaldir(int pozisyonId, int blokId, int? sayfaId = null)
        {
            enBlokPozisyon pozisyon = bllBlokPozisyonlari.Getir(pozisyonId);

            if (pozisyon.Master)
            {
                new dalSiteHaritasi_Blok().Kaldir(pozisyonId, blokId);
            }
            else
            {
                new dalSiteHaritasi_Blok().Kaldir(pozisyonId, blokId, sayfaId);
            }
        }

        public static enSiteHaritasi_Blok Getir(int pozisyonId, int blokId, int? sayfaId = null)
        {
            if (sayfaId == null || sayfaId == 0)
            {
                return new dalSiteHaritasi_Blok().Getir(pozisyonId, blokId);
            }

            return new dalSiteHaritasi_Blok().Getir(pozisyonId, blokId, sayfaId);

        }

        public static enBlok PozisyonaGoreBlokGetir(int pozisyonId, int blokId)
        {
            return new dalSiteHaritasi_Blok().PozisyonaGoreBlokGetir(pozisyonId, blokId);
        }

        public static void BaslikKullanimiGuncelle(enSiteHaritasi_Blok sBlok)
        {
            enBlokPozisyon pozisyon = bllBlokPozisyonlari.Getir(sBlok.PozisyonId);

            if (pozisyon.Master)
            {
                sBlok.SayfaId = 0;
                new dalSiteHaritasi_Blok().BaslikKullanimiGuncelle(sBlok);
            }
            else
            {
                new dalSiteHaritasi_Blok().BaslikKullanimiGuncelle(sBlok);
            }
        }

        public static void CerceveKullanimiGuncelle(enSiteHaritasi_Blok sBlok)
        {
            enBlokPozisyon pozisyon = bllBlokPozisyonlari.Getir(sBlok.PozisyonId);

            if (pozisyon.Master)
            {
                sBlok.SayfaId = 0;
                new dalSiteHaritasi_Blok().CerceveKullanimiGuncelle(sBlok);
            }
            else
            {
                new dalSiteHaritasi_Blok().CerceveKullanimiGuncelle(sBlok);
            }
        }

        public static void SiraGuncelle(enSiteHaritasi_Blok sBlok)
        {
            new dalSiteHaritasi_Blok().SiraGuncelle(sBlok);
        }

        public static void PozisyonGuncelle(enSiteHaritasi_Blok sBlok)
        {
            new dalSiteHaritasi_Blok().PozisyonGuncelle(sBlok);
        }

        public static enBlok BlokGetir(int blokId, int sayfaId)
        {
            return new dalSiteHaritasi_Blok().BlokGetir(blokId, sayfaId);
        }

        /// <summary>
        /// Blok içeriklerini kendi içinde oluşturup getirir.
        /// </summary>
        /// <param name="pozId"></param>
        /// <param name="statu"></param>
        /// <returns></returns>
        public static List<enBlok> PozisyonaGoreGetir(int pozId, bool? statu = null, int? sayfaId = null)
        {
            enBlokPozisyon pozisyon = bllBlokPozisyonlari.Getir(pozId);

            List<enBlok> bloks = new List<enBlok>();

            if (pozisyon.Master)
            {
                bloks = new dalSiteHaritasi_Blok().PozisyonaGoreGetir(pozId, statu);
            }
            else
            {
                bloks = new dalSiteHaritasi_Blok().PozisyonaGoreGetir(pozId, statu, sayfaId);
            }

            foreach (enBlok blok in bloks)
            {
                IcerikOlustur(blok);
            }

            return bloks;
        }

        public static enBlok IcerikOlustur(enBlok blok)
        {

            //if (blok.Id == enEnumaration.enmBloklar.Duyurular)
            //{
            //    DuyurulariGetir(blok);
            //}

            if (blok.SayfaId != null)
            {
                SayfalariListele(blok);
            }

            if (blok.CarouselId != null)
            {
                CarouselOlustur(blok);
            }

            if (blok.Id == enEnumaration.enmBloklar.YatayMenu)
            {
                YatayMenuOlustur(blok);
            }

            if (blok.Id == enEnumaration.enmBloklar.DikeyMenu)
            {
                DikeyMenuOlustur(blok);
            }

            if (blok.Id == enEnumaration.enmBloklar.Logo)
            {
                LogoOlustur(blok);
            }

            if (blok.Id == enEnumaration.enmBloklar.SosyalMedya)
            {
                SosyalMedyaOlustur(blok);
            }

            return blok;
        }

        private static void SosyalMedyaOlustur(enBlok blok)
        {
            StringBuilder sb = new StringBuilder();

            List<enSosyalMedya> hesaplar = bllSosyalMedya.AktifleriGetir();

            sb.Append("<ul class='button-group'>");

            foreach (enSosyalMedya medya in hesaplar)
            {
                sb.Append("<li><a href='" + medya.sos_url + "' targer='_blank'><img src='" + medya.sos_ikonu + "' /></a></li>");
            }

            sb.Append("</ul>");

            blok.Icerik += sb.ToString();
        }

        private static void SayfalariListele(enBlok blok)
        {
            StringBuilder sb = new StringBuilder();

            List<enSiteHaritasi> sayfalar = bllSiteHaritasi.AktifAltSayfalariGetirList_SinirliSayida(blok.SayfaId.Value, 10);

            int altSayfaSayisi = bllSiteHaritasi.AltSayfaSayisi(blok.SayfaId.Value);

            sb.Append("<ul class='side-nav'>");

            foreach (enSiteHaritasi sayfa in sayfalar)
            {
                sb.Append("<li><a href='" + sayfa.Url + "'>" + sayfa.Adi + "</a></li>");
            }

            sb.Append("</ul>");

            if (altSayfaSayisi > 10)
            {
                sb.Append("<div class='text-center'><a href='" + bllSiteHaritasi.UrlGetir(blok.SayfaId.Value) + "'><h3>...</h3></a></div>");
            }

            blok.Icerik += sb.ToString();
        }

        private static void CarouselOlustur(enBlok blok)
        {
            enCarousel carousel = bllCarousel.Getir(blok.CarouselId.Value);

            List<enCarouselResim> resimler = bllCarouselResimleri.ResimleriGetir(blok.CarouselId.Value, true);

            StringBuilder sb = new StringBuilder();

            string guid = Guid.NewGuid().ToString();

            sb.Append("<div id='" + guid + "' class='owl-carousel'>");

            string thumbStr = carousel.TekrarSayisi > 1 ? "th " : "";

            foreach (enCarouselResim resim in resimler)
            {
                string fvLink = "";

                if (resim.VideoLink)
                {
                    fvLink = "fancybox-media' data-fancybox-group='gallery'";
                }

                if (resim.FotoLink)
                {
                    fvLink = "fancybox' data-fancybox-group='gallery'";
                    resim.NavUrl = resim.Buyuk;
                }

                sb.Append(@"<div class='item'>
                    <a class='" + thumbStr + " " + fvLink + " href='" + resim.NavUrl + @"'" + (resim.NavUrl.Contains("#!") ? "" : " target='_blank'") + @">
                        <img src='" + resim.Orta + @"' alt='' />
                        <br/>" + resim.Baslik + @"
                    </a>
                </div>");
            }

            sb.Append("</div>");
            sb.Append(@"<script>$('#" + guid + @"').owlCarousel({
                    autoPlay: " + carousel.GosterimSuresi + @", 
                    items: " + carousel.TekrarSayisi + @",
                    itemsDesktop: [1199, 3],
                    itemsDesktopSmall: [979, 3]
                });</script>");

            blok.Icerik += sb.ToString();
        }

        private static void DikeyMenuOlustur(enBlok blok)
        {
            blok.Icerik += SosyalMedyalariOlustur(true) + YanMenuOlustur();
        }

        private static string YanMenuOlustur()
        {
            StringBuilder sb = new StringBuilder();

            int? currentPageId = null;
            int? parentPageId = null;

            try
            {
                currentPageId = RouteTable.Routes.GetRouteData(new HttpContextWrapper(HttpContext.Current)).Values["Id"].xToInt();
            }
            catch { }

            List<enSiteHaritasi> sayfalar = new List<enSiteHaritasi>();

            enSiteHaritasi currentPage = new enSiteHaritasi();

            if (currentPageId == null)
            {
                sayfalar = bllSiteHaritasi.YanUstSayfalariGetirList(true);
            }
            else
            {
                currentPage = bllSiteHaritasi.SayfaGetir(currentPageId.Value);
                parentPageId = currentPage.Parent;

                sayfalar = bllSiteHaritasi.YanAltSayfalariGetirList(currentPageId.Value, true);
            }

            if (sayfalar.Count == 0)
            {
                if (parentPageId != null)
                {
                    sayfalar = bllSiteHaritasi.YanAltSayfalariGetirList(parentPageId.Value, true);
                }
                else
                {
                    sayfalar = bllSiteHaritasi.YanUstSayfalariGetirList(true);
                }
            }

            sb.Append("<ul class='side-nav'>");

            if (parentPageId != null)
            {

                enSiteHaritasi parentPage = bllSiteHaritasi.SayfaGetir(parentPageId.Value);

                sb.Append("<li><a title='" + parentPage.Title + "' class='main-menu-a' href='" + parentPage.Url + "' " + (parentPage.Url.Contains("http://www.") ? "target='_blank'" : "") + "><< " + parentPage.Adi + "</a>");
                sb.Append("<li class='divider'></li>");
            }

            foreach (enSiteHaritasi sayfa in sayfalar)
            {
                sb.Append("<li><a title='" + sayfa.Title + "' class='main-menu-a' href='" + sayfa.Url + "' " + (sayfa.Url.Contains("http://www.") ? "target='_blank'" : "") + ">" + sayfa.Adi + "</a>");

            }

            sb.Append("</ul>");

            return sb.ToString();
        }

        private static void YatayMenuOlustur(enBlok blok)
        {
            string siteIciArama = @"<ul class='right'>
                                        <li class='has-form'>
                                          <div class='row collapse'>
                                            <div class='large-8 small-9 columns'>
                                              <input type='text' placeholder='" + bllDiziler.DiziGetir("NavigationBar.Input.PlaceHolder.Search") + @"'>
                                            </div>
                                            <div class='large-4 small-3 columns'>
                                              <a href='#' class='button expand'>" + bllDiziler.DiziGetir("NavigationBar.Input.Button.Search") + @"</a>
                                            </div>
                                          </div>
                                        </li>
                                    </ul>";

            if (!bllGenelAyarlar.GenelAyarGetir(enEnumaration.enmGenelAyarlar.SiteIciArama).Icerik.xToBooleanDefault())
            {
                siteIciArama = "";
            }

            blok.Icerik += @"<nav id='main-nav' class='top-bar' data-topbar>
                                <ul class='title-area'>
                                    <li class='name'><a id='aMenuLogo' href='/' class='show-for-small'>
                                        <img id='imgMenuLogo' src=" + bllTasarimAyarlari.TasarimAyariGetir(enEnumaration.enmTasarimAyarlari.Logo).Degeri + @" /></a></li>
                                    <li class='toggle-topbar menu-icon'><a href='#!'><span>" + bllDiziler.DiziGetir("Mobile.Main.TopBar.Text.Menu") +
                                                      @"</span></a></li>
                                </ul>
                                <section id='sctItems' class='top-bar-section'>
                                    " + GetNavigationHTML() + @"
                                    " + siteIciArama + @"
                                </section>
                            </nav>";

        }

        private static void LogoOlustur(enBlok blok)
        {
            blok.Icerik = @"<a href='/'>
                                    <img id='imgLogo' src='" + bllTasarimAyarlari.TasarimAyariGetir(enEnumaration.enmTasarimAyarlari.Logo).Degeri + @"'  /></a>";
        }

        private static string GetNavigationHTML()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<ul id='ulUst' class='left'>");

            List<enSiteHaritasi> sayfalar = bllSiteHaritasi.MenuUstSayfalariGetirList(true);

            int i = 0;
            foreach (enSiteHaritasi sayfa in sayfalar)
            {

                List<enSiteHaritasi> altSayfalar = bllSiteHaritasi.MenuAltSayfalariGetirList(sayfa.Id, true);

                string ddClass = altSayfalar.Count > 0 && sayfa.AcilirMenu ? " class='has-dropdown'" : "";

                if (i != 0)
                {
                    sb.Append("<li class='divider'></li>");
                }

                sb.Append("<li" + ddClass + "><a title='" + sayfa.Title + "' class='main-menu-a' href='" + sayfa.Url + "' " + (sayfa.Url.Contains("http://www.") ? "target='_blank'" : "") + ">" + sayfa.Adi + "</a>");


                if (altSayfalar.Count > 0 && sayfa.AcilirMenu)
                {
                    AltSayfalariYaz(sb, altSayfalar);
                }

                sb.Append("</li>");

                i++;
            }

            sb.Append("</ul>");

            return sb.ToString();
        }

        private static void AltSayfalariYaz(StringBuilder sb, List<enSiteHaritasi> altSayfalar)
        {
            sb.Append("<ul class='dropdown'>");

            int i = 0;
            foreach (enSiteHaritasi altSayfa in altSayfalar)
            {
                if (i != 0)
                {
                    sb.Append("<li class='divider'></li>");
                }
                List<enSiteHaritasi> altSayfalar2 = bllSiteHaritasi.AktifAltSayfalariGetirList(altSayfa.Id);

                string ddClass = altSayfalar2.Count > 0 && altSayfa.AcilirMenu ? " class='has-dropdown'" : "";

                if (altSayfa.DefaultSayfa)
                {
                    sb.Append("<li" + ddClass + "><a title='" + altSayfa.Title + "' href='" + altSayfa.Url + "' " + (altSayfa.Url.Contains("http://www.") ? "target='_blank'" : "") + ">" + altSayfa.Adi + "</a>");
                }
                else
                {
                    sb.Append("<li" + ddClass + "><a title='" + altSayfa.Title + "' href='" + altSayfa.Url + "' " + (altSayfa.Url.Contains("http://www.") ? "target='_blank'" : "") + ">" + altSayfa.Adi + "</a>");
                }

                if (altSayfalar2.Count > 0 && altSayfa.AcilirMenu)
                {
                    AltSayfalariYaz(sb, altSayfalar2);
                }

                sb.Append("</li>");

                i++;
            }

            sb.Append("</ul>");
        }

        private static string SosyalMedyalariOlustur(bool? side = null)
        {
            StringBuilder sb = new StringBuilder();

            List<enSosyalMedya> hesaplar = bllSosyalMedya.AktifleriGetir();

            string sideNot = side != null ? ("class='button-group'") : "class='right'";

            sb.Append("<ul " + sideNot + ">");

            int i = 0;
            foreach (enSosyalMedya medya in hesaplar)
            {
                if (side != null && !medya.sos_yanMenu)
                {
                    return "";
                }
                if (medya.sos_menu)
                {
                    sb.Append("<li><a href='" + medya.sos_url + "' targer='_blank'><img src='" + medya.sos_ikonu + "' /></a></li>");
                    i++;
                }
            }

            sb.Append("</ul>");

            string sonuc = i > 0 ? sb.ToString() : "";

            return sonuc;
        }
    }
}
