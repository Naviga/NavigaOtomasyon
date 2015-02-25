using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entity;
using DAL;

namespace BLL
{
    public static class bllAdmin
    {
        public static List<enAdmin> AdminleriGetir()
        {
            return new dalAdmin().AdminleriGetir();
        }

        public static enAdmin AdminGetir(string kullaniciAdi, string sifre)
        {
            return new dalAdmin().AdminGetir(kullaniciAdi, sifre);
        }

        public static enAdmin AdminGetir(int adminId)
        {
            return new dalAdmin().AdminGetir(adminId);
        }

        public static void YeniAdminEkle(enAdmin admin)
        {
            new dalAdmin().YeniAdminEkle(admin);
        }

        public static void Guncelle(enAdmin admin)
        {
            new dalAdmin().Guncelle(admin);
        }

        public static void StatuGuncelle(enAdmin admin)
        {
            new dalAdmin().StatuGuncelle(admin);
        }

        public static void AdminSil(int adminId)
        {
            new dalAdmin().AdminSil(adminId);
        }

        public static enAdmin AdminGetir(string kullaniciAdi)
        {
            return new dalAdmin().AdminGetir(kullaniciAdi);
        }
    }
}
