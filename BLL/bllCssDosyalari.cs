using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entity;
using DAL;

namespace BLL
{
    public static class bllCssDosyalari
    {
        public static void YeniEkle(enCssDosya css)
        {
            new dalCssDosyalari().YeniEkle(css);
        }

        public static List<enCssDosya> CssDosyalariGetir(int? sablonId)
        {
            return new dalCssDosyalari().CssDosyalariGetir(sablonId);
        }

        public static enCssDosya CssGetir(int cssId)
        {
            return new dalCssDosyalari().CssGetir(cssId);
        }

        public static void Sil(int cssId)
        {
            new dalCssDosyalari().Sil(cssId);
        }
    }
}
