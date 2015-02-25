using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entity;
using DAL;

namespace BLL
{
    public static class bllIcerikSablonlar
    {
        public static void YeniEkle(enIcerikSablon sablon)
        {
            new dalIcerikSablonlar().YeniEkle(sablon);
        }

        public static List<enIcerikSablon> SablonlariGetir()
        {
            return new dalIcerikSablonlar().SablonlariGetir();
        }

        public static enIcerikSablon SablonGetir(int sablonId)
        {
            return new dalIcerikSablonlar().SablonGetir(sablonId);
        }

        public static void Sil(int sablonId)
        {
            new dalIcerikSablonlar().Sil(sablonId);
        }
    }
}
