using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entity;
using DAL;

namespace BLL
{
    public static class bllSablonlar
    {
        public static void YeniEkle(enSablon sablon)
        {
            new dalSablonlar().YeniEkle(sablon);
        }

        public static List<enSablon> SablonlariGetir()
        {
            return new dalSablonlar().SablonlariGetir();
        }

        public static enSablon SablonGetir(int sablonId)
        {
            return new dalSablonlar().SablonGetir(sablonId);
        }

        public static void Sil(int sablonId)
        {
            new dalSablonlar().Sil(sablonId);
        }
    }
}
