using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entity;
using DAL;

namespace BLL
{
    public static class bllTasarimAyarlari
    {
        public static List<enTasarimAyar> TasarimAyarlariniGetir()
        {
            return new dalTasarimAyarlari().TasarimAyarlariniGetir();
        }

        public static void TasarimAyariGuncelle(enTasarimAyar ayar)
        {
            new dalTasarimAyarlari().TasarimAyariGuncelle(ayar);
        }

        public static enTasarimAyar TasarimAyariGetir(int id)
        {
            return new dalTasarimAyarlari().TasarimAyariGetir(id);
        }
    }
}
