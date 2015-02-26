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
    public static class bllIcerikResimleri
    {
        public static void YeniResimEkle(enIcerikResim resim)
        {
            new dalIcerikResimleri().YeniResimEkle(resim);
        }

        public static void ResimSil(int resimId)
        {
            enIcerikResim resim = ResimGetir(resimId);

            try
            {
                File.Delete(HttpContext.Current.Server.MapPath("~") + resim.Buyuk);
                File.Delete(HttpContext.Current.Server.MapPath("~") + resim.Orta);
                File.Delete(HttpContext.Current.Server.MapPath("~") + resim.Kucuk);
            }
            catch
            {

            }

            new dalIcerikResimleri().ResimSil(resimId);
        }

        public static void ResimSil(enIcerikResim resim)
        {
            try
            {
                File.Delete(HttpContext.Current.Server.MapPath("~") + resim.Buyuk);
                File.Delete(HttpContext.Current.Server.MapPath("~") + resim.Orta);
                File.Delete(HttpContext.Current.Server.MapPath("~") + resim.Kucuk);
            }
            catch
            {

            }

            new dalIcerikResimleri().ResimSil(resim.Id);
        }

        public static List<enIcerikResim> ResimleriGetir(int sayfaId, bool? statu)
        {
            return new dalIcerikResimleri().ResimleriGetir(sayfaId, statu);
        }

        public static List<enIcerikResim> ResimleriGetir(int sayfaId, int? kayitSayisi, bool? statu)
        {
            return new dalIcerikResimleri().ResimleriGetir(sayfaId, kayitSayisi, statu);
        }

        public static enIcerikResim ResimGetir(int resimId)
        {
            return new dalIcerikResimleri().ResimGetir(resimId);
        }

        public static void ResimStatuGuncelle(enIcerikResim resim)
        {
            new dalIcerikResimleri().ResimStatuGuncelle(resim);
        }

        public static void ResimAciklamaGuncelle(enIcerikResim resim)
        {
            new dalIcerikResimleri().ResimAciklamaGuncelle(resim);
        }

        public static void ResimSiraGuncelle(enIcerikResim resim)
        {
            new dalIcerikResimleri().ResimSiraGuncelle(resim);
        }

        public static enIcerikResim BirUsttekiResmiGetir(enIcerikResim gResim)
        {
            return new dalIcerikResimleri().BirUsttekiResmiGetir(gResim);
        }

        public static enIcerikResim BirAlttakiResmiGetir(enIcerikResim gResim)
        {
            return new dalIcerikResimleri().BirAlttakiResmiGetir(gResim);
        }

        public static int SonSiraNoGetir(int sayfaId)
        {
            return new dalIcerikResimleri().SonSiraNoGetir(sayfaId);
        }

        public static void AnaResimYap(enIcerikResim resim)
        {
            new dalIcerikResimleri().AnaResimYap(resim);
        }

        public static enIcerikResim AnaResimGetir(int? sayfaId)
        {
            return new dalIcerikResimleri().AnaResimGetir(sayfaId);
        }

        public static List<enIcerikResim> TumResimleriGetir(bool? statu)
        {
            return new dalIcerikResimleri().TumResimleriGetir(statu);
        }

        public static List<enIcerikResim> Top4ResimGetir(bool? statu)
        {
            return new dalIcerikResimleri().Top4ResimGetir(statu);
        }
    }
}
