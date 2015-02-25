using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using Entity;

namespace BLL
{
    public static class bllBloklar
    {
        public static List<enBlok> BloklariGetir(bool? statu)
        {
            return new dalBloklar().BloklariGetir(statu);
        }

        public static enBlok BlokGetir(int blokId)
        {
            enBlok blk = new dalBloklar().BlokGetir(blokId);

            return blk;
        }

        //public static enBlok BirUsttekiBloguGetir(enBlok blok)
        //{
        //    return new dalBloklar().BirUsttekiBloguGetir(blok);
        //}

        //public static enBlok BirAlttakiBloguGetir(enBlok blok)
        //{
        //    return new dalBloklar().BirAlttakiBloguGetir(blok);
        //}

        public static void YeniBlokEkle(enBlok blok)
        {
            new dalBloklar().YeniBlokEkle(blok);
        }

        public static void BlokSil(int blokId)
        {
            new dalBloklar().BlokSil(blokId);
        }

        public static void BlokGuncelle(enBlok blok)
        {
            new dalBloklar().BlokGuncelle(blok);
        }

        public static void BlokStatuGuncelle(enBlok blok)
        {
            new dalBloklar().BlokStatuGuncelle(blok);
        }
    }
}
