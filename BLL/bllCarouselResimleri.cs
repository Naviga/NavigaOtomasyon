using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Entity;
using DAL;

namespace BLL
{
    public static class bllCarouselResimleri
    {
        public static void YeniResimEkle(enCarouselResim resim)
        {
            new dalCarouselResimleri().YeniResimEkle(resim);
        }

        public static void ResimSil(int resimId)
        {
            try
            {
                enCarouselResim resim = ResimGetir(resimId);

                File.Delete(HttpContext.Current.Server.MapPath("~" + resim.Kucuk));
                File.Delete(HttpContext.Current.Server.MapPath("~" + resim.Orta));
                File.Delete(HttpContext.Current.Server.MapPath("~" + resim.Buyuk));

            }
            catch
            {
            }
            new dalCarouselResimleri().ResimSil(resimId);
        }

        public static List<enCarouselResim> ResimleriGetir(int carouselId, bool? statu)
        {
            return new dalCarouselResimleri().ResimleriGetir(carouselId, statu);
        }

        public static List<enCarouselResim> ResimleriGetir(int carouselId, int? kayitSayisi, bool? statu)
        {
            return new dalCarouselResimleri().ResimleriGetir(carouselId, kayitSayisi, statu);
        }

        public static enCarouselResim ResimGetir(int resimId)
        {
            return new dalCarouselResimleri().ResimGetir(resimId);
        }

        public static void ResimStatuGuncelle(enCarouselResim resim)
        {
            new dalCarouselResimleri().ResimStatuGuncelle(resim);
        }

        public static void ResimSiraGuncelle(enCarouselResim resim)
        {
            new dalCarouselResimleri().ResimSiraGuncelle(resim);
        }

        public static enCarouselResim BirUsttekiResmiGetir(enCarouselResim gResim)
        {
            return new dalCarouselResimleri().BirUsttekiResmiGetir(gResim);
        }

        public static enCarouselResim BirAlttakiResmiGetir(enCarouselResim gResim)
        {
            return new dalCarouselResimleri().BirAlttakiResmiGetir(gResim);
        }

        public static int SonSiraNoGetir(int carouselId)
        {
            return new dalCarouselResimleri().SonSiraNoGetir(carouselId);
        }

        public static void ResimBaslikGuncelle(enCarouselResim resim)
        {
            new dalCarouselResimleri().ResimBaslikGuncelle(resim);
        }

        public static void NavUrlGuncelle(enCarouselResim resim)
        {
            new dalCarouselResimleri().NavUrlGuncelle(resim);
        }

        public static void FotoVideoLinkGuncelle(enCarouselResim resim)
        {
            new dalCarouselResimleri().FotoVideoLinkGuncelle(resim);
        }

    }
}
