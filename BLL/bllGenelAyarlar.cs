using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entity;
using DAL;

namespace BLL
{
    public static class bllGenelAyarlar
    {
        public static List<enGenelAyar> GenelAyarlariGetir()
        {
            return new dalGenelAyarlar().GenelAyarlariGetir();
        }

        public static enGenelAyar GenelAyarGetir(int ayarId)
        {
            return new dalGenelAyarlar().GenelAyarGetir(ayarId);
        }

        public static void AyarGuncelle(enGenelAyar ayar)
        {
            new dalGenelAyarlar().AyarGuncelle(ayar);
        }
    }
}
