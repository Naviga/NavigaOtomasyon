using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using DAL;
using Entity;


namespace BLL
{
    public static class bllSosyalMedya
    {
        public static void Insert(enSosyalMedya sosyalmedya)
        {
            new dalSosyalMedya().Insert(sosyalmedya);
        }
        public static DataTable SelectAll()
        {
            return new dalSosyalMedya().SelectAll();
        }
        public static DataTable SelectActive()
        {
            return new dalSosyalMedya().SelectActive();
        }

        public static DataTable SelectActiveTers()
        {
            return new dalSosyalMedya().SelectActiveTers();
        }

        public static void Delete(int sos_id)
        {
            new dalSosyalMedya().Delete(sos_id);
        }
        public static int SonSiraNoGetir()
        {
            return 0; //new dalSosyalMedya().SonSiraNoGetir();
        }
        public static enSosyalMedya Getir(int sosId)
        {
            return new dalSosyalMedya().Getir(sosId);
        }

        public static void StatuGuncelle(int sosId, bool statu)
        {
            new dalSosyalMedya().StatuGuncelle(sosId, statu);
        }

        public static void SiraGuncelle(int sosId, int sira)
        {
            new dalSosyalMedya().SiraGuncelle(sosId, sira);
        }

        public static enSosyalMedya BirAlttaki(int sira)
        {
            return new dalSosyalMedya().BirAlttaki(sira);
        }

        public static enSosyalMedya BirUstteki(int sira)
        {
            return new dalSosyalMedya().BirUstteki(sira);
        }

        public static void Guncelle(enSosyalMedya media)
        {
            new dalSosyalMedya().Guncelle(media);
        }

        public static List<enSosyalMedya> TumunuGetir()
        {
            return new dalSosyalMedya().TumunuGetir();
        }

        public static List<enSosyalMedya> AktifleriGetir()
        {
            return new dalSosyalMedya().AktifleriGetir();
        }

        public static void MenudeGoster(enSosyalMedya sos)
        {
            new dalSosyalMedya().MenudeGoster(sos);
        }

        public static void FooterdaGoster(enSosyalMedya sos)
        {
            new dalSosyalMedya().FooterdaGoster(sos);
        }

        public static void YanMenudeGoster(enSosyalMedya sos)
        {
            new dalSosyalMedya().YanMenudeGoster(sos);
        }
    }
}
