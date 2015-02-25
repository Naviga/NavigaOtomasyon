using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using Entity;

namespace BLL
{
    public static class bllBlokPozisyonlari
    {
        public static List<enBlokPozisyon> Getir()
        {
            return new dalBlokPozisyonlari().Getir();
        }
        public static enBlokPozisyon Getir(int id)
        {
            return new dalBlokPozisyonlari().Getir(id);
        }
    }
}
