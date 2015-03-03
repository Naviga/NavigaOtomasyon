using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web.Profile;
using DAL;
using Entity;
using System.Web.Routing;

namespace BLL
{
    public static class bllSiteHaritasi
    {
        public static DataTable AktifUstSayfalariGetir()
        {
            return new dalSiteHaritasi().AktifUstSayfalariGetir();
        }

        public static DataTable AktifAltSayfalariGetir(int parentId)
        {
            return new dalSiteHaritasi().AktifAltSayfalariGetir(parentId);
        }

        public static List<enSiteHaritasi> AktifUstSayfalariGetirList()
        {
            return new dalSiteHaritasi().AktifUstSayfalariGetirList();
        }

        public static List<enSiteHaritasi> UstSayfalariGetirList()
        {
            return new dalSiteHaritasi().UstSayfalariGetirList();
        }

        public static List<enSiteHaritasi> AktifAltSayfalariGetirList(int parentId)
        {
            return new dalSiteHaritasi().AktifAltSayfalariGetirList(parentId);
        }

        public static List<enSiteHaritasi> AktifAltSayfalariGetirList_SinirliSayida(int parentId, int kayitSayisi)
        {
            return new dalSiteHaritasi().AktifAltSayfalariGetirList_SinirliSayida(parentId, kayitSayisi);
        }

        public static List<enSiteHaritasi> AltSayfalariGetirList(int parentId)
        {
            return new dalSiteHaritasi().AltSayfalariGetirList(parentId);
        }

        public static enSiteHaritasi SayfaGetir(int sayfaId)
        {
            return new dalSiteHaritasi().SayfaGetir(sayfaId);
        }

        public static enSiteHaritasi SayfaGetir(string url)
        {
            return new dalSiteHaritasi().SayfaGetir(url);
        }

        public static enSiteHaritasi SayfaGetirFiziksel(string url)
        {
            return new dalSiteHaritasi().SayfaGetirFiziksel(url);
        }

        public static void SayfaDuzenle(enSiteHaritasi site)
        {
            string guid = Guid.NewGuid().ToString();

            new dalSiteHaritasi().SayfaDuzenle(site);

            List<enSiteHaritasi> altSayfalar = bllSiteHaritasi.AltSayfalariGetirList(site.Id);

            foreach (enSiteHaritasi altSayfa in altSayfalar)
            {
                altSayfa.SayfaMenu = site.SayfaMenu;
                SayfaMenuDegistir(altSayfa);
            }

            try
            {
                if (site.Url == "/") return;
                if (site.Url.Contains("http://") || site.Url.Contains("https://")) return;

                site.Url = UrlOlustur(site);

                string url = site.Url.StartsWith("/") ? site.Url.Remove(0, 1) : site.Url;

                if (site.FizikselUrl.xBosMu())
                {
                    RouteTable.Routes.MapPageRoute(url + site.Id, url, "~/Sayfa.aspx", false, new RouteValueDictionary { { "Id", site.Id } });
                }
                else
                {
                    RouteTable.Routes.MapPageRoute(url + site.Id, url, site.FizikselUrl, false, new RouteValueDictionary { { "Id", site.Id } });
                }
            }
            catch
            { }
        }

        public static void StatuDegistir(enSiteHaritasi site)
        {
            new dalSiteHaritasi().StatuDegistir(site);
        }

        public static void AcilirMenuDurumDegistir(enSiteHaritasi site)
        {
            new dalSiteHaritasi().AcilirMenuDurumDegistir(site);
        }

        public static void YeniSayfaEkle(enSiteHaritasi site)
        {
            new dalSiteHaritasi().YeniSayfaEkle(site);

            try
            {
                if (site.Url == "/") return;
                if (site.Url.Contains("http://") || site.Url.Contains("https://")) return;

                site.Url = UrlOlustur(site);

                string url = site.Url.StartsWith("/") ? site.Url.Remove(0, 1) : site.Url;

                if (site.FizikselUrl.xBosMu())
                {
                    RouteTable.Routes.MapPageRoute(url + site.Id, url, "~/Sayfa.aspx", false, new RouteValueDictionary { { "Id", site.Id } });
                }
                else
                {
                    RouteTable.Routes.MapPageRoute(url + site.Id, url, site.FizikselUrl, false, new RouteValueDictionary { { "Id", site.Id } });
                }
            }
            catch
            { }
        }

        public static void SayfaSil(int sayfaId)
        {
            List<enSiteHaritasi> altSayfalar = AltSayfalariGetirList(sayfaId);

            AltSayfalariSil(altSayfalar);

            List<enIcerikResim> resimler = bllIcerikResimleri.ResimleriGetir(sayfaId, null);

            foreach (enIcerikResim resim in resimler)
            {
                bllIcerikResimleri.ResimSil(resim.Id);
            }

            List<enIcerikVideo> videolar = bllIcerikVideolari.Getir(sayfaId, null);

            foreach (enIcerikVideo video in videolar)
            {
                bllIcerikVideolari.Sil(video.Id);
            }

            new dalSiteHaritasi().SayfaSil(sayfaId);
        }

        public static void AltSayfalariSil(List<enSiteHaritasi> altSayfalar)
        {
            foreach (enSiteHaritasi altSayfa in altSayfalar)
            {
                List<enIcerikResim> resimler = bllIcerikResimleri.ResimleriGetir(altSayfa.Id, null);

                foreach (enIcerikResim resim in resimler)
                {
                    bllIcerikResimleri.ResimSil(resim.Id);
                }

                List<enIcerikVideo> videolar = bllIcerikVideolari.Getir(altSayfa.Id, null);

                foreach (enIcerikVideo video in videolar)
                {
                    bllIcerikVideolari.Sil(video.Id);
                }

                new dalSiteHaritasi().SayfaSil(altSayfa.Id);

                List<enSiteHaritasi> altSayfalar2 = bllSiteHaritasi.AktifAltSayfalariGetirList(altSayfa.Id);

                if (altSayfalar2.Count > 0 && altSayfa.AcilirMenu)
                {
                    AltSayfalariSil(altSayfalar2);
                }
            }
        }

        public static enSiteHaritasi BirAlttakiSayfayiGetir(enSiteHaritasi gSayfa)
        {
            return new dalSiteHaritasi().BirAlttakiSayfayiGetir(gSayfa);
        }

        public static enSiteHaritasi BirUsttekiSayfayiGetir(enSiteHaritasi gSayfa)
        {
            return new dalSiteHaritasi().BirUsttekiSayfayiGetir(gSayfa);
        }

        public static void SiraGuncelle(enSiteHaritasi sayfa)
        {
            new dalSiteHaritasi().SiraGuncelle(sayfa);
        }

        public static int SonSiraNoGetir(int? parent)
        {
            return new dalSiteHaritasi().SonSiraNoGetir(parent);
        }

        public static int AltSayfaSayisi(int sayfaId)
        {
            return new dalSiteHaritasi().AltSayfaSayisi(sayfaId);
        }

        public static List<enSiteHaritasi> AktifTumSayfalariGetir(bool? custom = null)
        {
            return new dalSiteHaritasi().AktifTumSayfalariGetir(custom);
        }

        public static List<enSiteHaritasi> TumSayfalariGetir()
        {
            return new dalSiteHaritasi().TumSayfalariGetir();
        }

        public static string UrlGetir(int sayfaId)
        {
            return new dalSiteHaritasi().UrlGetir(sayfaId);
        }

        public static bool UrlVarMi(string url, int? dzID = null)
        {
            return new dalSiteHaritasi().UrlVarMi(url, dzID);
        }

        public static void FotoGaleriDurumDegistir(enSiteHaritasi site)
        {
            new dalSiteHaritasi().FotoGaleriDurumDegistir(site);
        }

        public static void FaceCommentsDurumDegistir(enSiteHaritasi site)
        {
            new dalSiteHaritasi().FaceCommentsDurumDegistir(site);
        }

        public static void CustomDurumDegistir(enSiteHaritasi sayfa)
        {
            new dalSiteHaritasi().CustomDurumDegistir(sayfa);
        }

        public static List<enSiteHaritasi> OzelSayfalariGetir()
        {
            return new dalSiteHaritasi().OzelSayfalariGetir();
        }

        public static void MenuDegistir(enSiteHaritasi sayfa)
        {
            new dalSiteHaritasi().MenuDegistir(sayfa);
        }

        public static void YanMenuDegistir(enSiteHaritasi sayfa)
        {
            new dalSiteHaritasi().YanMenuDegistir(sayfa);
        }

        public static void FooterDegistir(enSiteHaritasi sayfa)
        {
            new dalSiteHaritasi().FooterDegistir(sayfa);
        }

        public static void SayfaMenuDegistir(enSiteHaritasi sayfa)
        {
            new dalSiteHaritasi().SayfaMenuDegistir(sayfa);
        }

        public static void ListDegistir(enSiteHaritasi sayfa)
        {
            new dalSiteHaritasi().ListDegistir(sayfa);
        }

        public static List<enSiteHaritasi> YanUstSayfalariGetirList(bool? statu = null)
        {
            return new dalSiteHaritasi().YanUstSayfalariGetirList(statu);
        }

        public static List<enSiteHaritasi> YanAltSayfalariGetirList(int parentId, bool? statu = null)
        {
            return new dalSiteHaritasi().YanAltSayfalariGetirList(parentId, statu);
        }

        public static List<enSiteHaritasi> FooterUstSayfalariGetirList(bool? statu = null)
        {
            return new dalSiteHaritasi().FooterUstSayfalariGetirList(statu);
        }

        public static List<enSiteHaritasi> FooterAltSayfalariGetirList(int parentId, bool? statu = null)
        {
            return new dalSiteHaritasi().FooterAltSayfalariGetirList(parentId, statu);
        }

        public static List<enSiteHaritasi> MenuUstSayfalariGetirList(bool? statu = null)
        {
            return new dalSiteHaritasi().MenuUstSayfalariGetirList(statu);
        }

        public static List<enSiteHaritasi> MenuAltSayfalariGetirList(int parentId, bool? statu = null)
        {
            return new dalSiteHaritasi().MenuAltSayfalariGetirList(parentId, statu);
        }

        public static void ResimGuncelle(int pageId, string pictureUrl)
        {
            new dalSiteHaritasi().ResimGuncelle(pageId, pictureUrl);
        }

        public static void CarouselSec(int sayfaId, int carouseId, bool carouselSec)
        {
            new dalSiteHaritasi().CarouselSec(sayfaId, carouseId, carouselSec);
        }

        public static void TumResimleriSil(int sayfaId)
        {
            List<enIcerikResim> resimler = bllIcerikResimleri.ResimleriGetir(sayfaId, null);

            foreach (enIcerikResim resim in resimler)
            {
                bllIcerikResimleri.ResimSil(resim);
            }
        }

        private static string UrlOlustur(enSiteHaritasi sayfa)
        {
            string url = sayfa.Url;

            while (sayfa.Parent != null)
            {
                sayfa = SayfaGetir(sayfa.Parent.Value);

                url = sayfa.Url + url;
            }

            return url.ToLowerInvariant();
        }

        public static List<enSiteHaritasi> SolAltMenuGetir()
        {
            return new dalSiteHaritasi().SolAltMenuGetir();
        }

        public static List<enSiteHaritasi> SagAltMenuGetir()
        {
            return new dalSiteHaritasi().SagAltMenuGetir();
        }

        public static List<enSiteHaritasi> UrunleriGetir(int parentId)
        {
            return new dalSiteHaritasi().UrunleriGetir(parentId);
        }

        public static List<enSiteHaritasi> HaberleriGetir()
        {
            return new dalSiteHaritasi().HaberleriGetir();
        }

        public static enSiteHaritasi HaberGetir(int sayfaId)
        {
            return new dalSiteHaritasi().HaberGetir(sayfaId);
        }

        public static void VitrinGoster(int sayfaId,bool vitrin)
        {
            new dalSiteHaritasi().VitrindeGoster(sayfaId, vitrin);
        }
    }
}
