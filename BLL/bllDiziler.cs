using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using DAL;
using Entity;

namespace BLL
{
    public static class bllDiziler
    {
        public static void Yeni(enDizi dizi, int dilId)
        {
            new dalDiziler().Yeni(dizi, dilId);
        }

        public static void YeniKod(enDizi dizi)
        {
            new dalDiziler().YeniKod(dizi);
        }

        public static string DiziGetir(string kodu)
        {
            string dizi = "";

            //if (!Common.SessionManager.SeciliDil.VarsayilanMi)
            //{
            //    dizi = new dalDiziler().DilDiziGetir(kodu, SessionManager.SeciliDilId.xToIntDefault());
            //}
            //else
            //{
            dizi = new dalDiziler().DiziGetir(kodu);
            //}
            return dizi;
        }

        public static enDizi DiziGetir(int id)
        {
            return new dalDiziler().DiziGetir(id);
        }

        public static enDizi DiziGetirEnt(string kodu)
        {
            return new dalDiziler().DiziGetirEnt(kodu);
        }

        public static List<enDizi> TumunuGetir(string kodu = "")
        {
            //return new dalDiziler().TumunuGetir(SessionManager.SeciliDilId);
            return new dalDiziler().TumunuGetir(kodu);
        }

        public static void VarsayilanDiziDuncelle(enDizi dizi)
        {
            new dalDiziler().VarsayilanDiziDuncelle(dizi);
        }

        //public static void DiziDilDuncelle(enDizi dizi, int dilId)
        //{
        //    new dalDiziler().DiziDilDuncelle(dizi, dilId);
        //}
        public static void Sil(int diziId)
        {
            new dalDiziler().Sil(diziId);
        }
    }
}
