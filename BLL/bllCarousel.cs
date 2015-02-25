using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using Common;
using DAL;
using Entity;

namespace BLL
{
    public static class bllCarousel
    {
        public static int YeniEkle(enCarousel carousel)
        {
            return new dalCarousel().YeniEkle(carousel);
        }

        public static void Duzenle(enCarousel carousel)
        {
            new dalCarousel().Duzenle(carousel);
        }

        public static void Sil(int carouselId)
        {
            List<enCarouselResim> resimler = bllCarouselResimleri.ResimleriGetir(carouselId, null);

            foreach (enCarouselResim carouselResim in resimler)
            {
                bllCarouselResimleri.ResimSil(carouselResim.Id);
            }

            new dalCarousel().Sil(carouselId);
        }

        public static List<enCarousel> CarouselleriGetir()
        {
            return new dalCarousel().CarouselleriGetir();
        }

        public static List<enCarousel> CarouselleriGetir(bool? statu)
        {
            return new dalCarousel().CarouselleriGetir(statu);
        }

        public static enCarousel Getir(int carouselId)
        {
            return new dalCarousel().Getir(carouselId);
        }

        public static void StatuDegistir(enCarousel carousel)
        {
            new dalCarousel().StatuDegistir(carousel);
        }
    }
}
