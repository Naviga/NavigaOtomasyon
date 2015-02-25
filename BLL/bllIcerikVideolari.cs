using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using Entity;

namespace BLL
{
    public static class bllIcerikVideolari
    {
        public static void YeniEkle(enIcerikVideo video)
        {
            new dalIcerikVideolari().YeniVideoEkle(video);
        }

        public static void Sil(int videoId)
        {
            new dalIcerikVideolari().VideoSil(videoId);
        }

        public static List<enIcerikVideo> Getir(int sayfaId, bool? statu)
        {
            return new dalIcerikVideolari().VideoleriGetir(sayfaId, statu);
        }

        public static List<enIcerikVideo> Getir(int sayfaId, int? kayitSayisi, bool? statu)
        {
            return new dalIcerikVideolari().VideoleriGetir(sayfaId, kayitSayisi, statu);
        }

        public static enIcerikVideo Getir(int videoId)
        {
            return new dalIcerikVideolari().VideoGetir(videoId);
        }

        public static void StatuGuncelle(enIcerikVideo video)
        {
            new dalIcerikVideolari().VideoStatuGuncelle(video);
        }

        public static void SiraGuncelle(enIcerikVideo video)
        {
            new dalIcerikVideolari().VideoSiraGuncelle(video);
        }

        public static void AciklamaGuncelle(enIcerikVideo video)
        {
            new dalIcerikVideolari().VideoAciklamaGuncelle(video);
        }

        public static int SonSiraNoGetir(int sayfaId)
        {
            return new dalIcerikVideolari().SonSiraNoGetir(sayfaId);
        }
    }
}
