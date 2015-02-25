using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using Common;
using Entity;

namespace DAL
{
    public class dalCarousel
    {
        public int YeniEkle(enCarousel carousel)
        {
            List<string> prmList = new List<string>();
            List<object> degerList = new List<object>();

            prmList.Add("car_site_id"); degerList.Add(carousel.SayfaId);
            prmList.Add("car_adi"); degerList.Add(carousel.Adi);
            prmList.Add("car_statu"); degerList.Add(carousel.Statu);
            prmList.Add("car_kayitTar"); degerList.Add(DateTime.Now.Date);
            prmList.Add("car_kaydeden"); degerList.Add(SessionManager.Admin.Id);
            prmList.Add("car_tekrarSayisi"); degerList.Add(carousel.TekrarSayisi);
            prmList.Add("car_gosterimSuresi"); degerList.Add(carousel.GosterimSuresi);

            return dalManager.MakeAnDbInsert(prmList, "Carousel", degerList, "car_id");
        }

        public void Duzenle(enCarousel carousel)
        {
            List<string> prmList = new List<string>();
            List<object> degerList = new List<object>();

            prmList.Add("car_site_id"); degerList.Add(carousel.SayfaId);
            prmList.Add("car_adi"); degerList.Add(carousel.Adi);
            prmList.Add("car_statu"); degerList.Add(carousel.Statu);
            prmList.Add("car_degisiklikTar"); degerList.Add(DateTime.Now.Date);
            prmList.Add("car_degistiren"); degerList.Add(SessionManager.Admin.Id);
            prmList.Add("car_tekrarSayisi"); degerList.Add(carousel.TekrarSayisi);
            prmList.Add("car_gosterimSuresi"); degerList.Add(carousel.GosterimSuresi);

            dalManager.MakeAnDbUpdate(prmList, "Carousel", "car_id", carousel.Id, degerList);
        }

        public void Sil(int carouselId)
        {
            dalManager.MakeAnDbDelete("Carousel", "car_id", carouselId);
        }

        public List<enCarousel> CarouselleriGetir()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(@"SELECT rg.*,(SELECT COUNT(*) FROM CarouselResimleri r WHERE r.carcarRes_car_id = rg.car_id) AS ResimSayisi
                        FROM Carousel rg ");

            OleDbDataAdapter adp = new OleDbDataAdapter(sb.ToString(), dalManager.Connection());

            DataTable dt = new DataTable();

            adp.Fill(dt);

            List<enCarousel> carouseller = new List<enCarousel>();

            foreach (DataRow rw in dt.Rows)
            {
                enCarousel carousel = new enCarousel();

                carousel.Adi = rw["car_adi"].ToString();
                carousel.Id = rw["car_id"].xToIntDefault();
                carousel.KayitTarihi = rw["car_kayitTar"].xToDateTimeDefault();
                carousel.Statu = rw["car_statu"].xToBooleanDefault();
                carousel.ResimSayisi = rw["ResimSayisi"].xToIntDefault();
                carousel.TekrarSayisi = rw["car_tekrarSayisi"].xToIntDefault();
                carousel.GosterimSuresi = rw["car_gosterimSuresi"].xToIntDefault();

                carouseller.Add(carousel);
            }

            return carouseller;
        }

        public List<enCarousel> CarouselleriGetir(bool? statu)
        {
            StringBuilder sb = new StringBuilder();

            OleDbDataAdapter adp = new OleDbDataAdapter(sb.ToString(), dalManager.Connection());

            if (statu != null)
            {
                sb.Append(@"SELECT rg.*,(SELECT COUNT(*) FROM CarouselResimleri r WHERE r.carRes_car_id = rg.car_id AND r.carRes_statu = @statu) AS ResimSayisi
                        FROM Carousel rg 
                        WHERE rg.car_statu = @statu");

                adp.SelectCommand.Parameters.AddWithValue("@statu", statu);
            }
            else
            {
                sb.Append(@"SELECT rg.*,(SELECT COUNT(*) FROM CarouselResimleri r WHERE r.carRes_car_id = rg.car_id) AS ResimSayisi
                        FROM Carousel rg ");
            }

            adp.SelectCommand.CommandText = sb.ToString();

            DataTable dt = new DataTable();

            adp.Fill(dt);

            List<enCarousel> carouseller = new List<enCarousel>();

            foreach (DataRow rw in dt.Rows)
            {
                enCarousel carousel = new enCarousel();

                carousel.Adi = rw["car_adi"].ToString();
                carousel.Id = rw["car_id"].xToIntDefault();
                carousel.KayitTarihi = rw["car_kayitTar"].xToDateTimeDefault();
                carousel.Statu = rw["car_statu"].xToBooleanDefault();
                carousel.ResimSayisi = rw["ResimSayisi"].xToIntDefault();
                carousel.TekrarSayisi = rw["car_tekrarSayisi"].xToIntDefault();
                carousel.GosterimSuresi = rw["car_gosterimSuresi"].xToIntDefault();

                carouseller.Add(carousel);
            }

            return carouseller;
        }

        public enCarousel Getir(int carouselId)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT * FROM Carousel WHERE car_id = @id");

            OleDbDataAdapter adp = new OleDbDataAdapter(sb.ToString(), dalManager.Connection());

            adp.SelectCommand.Parameters.AddWithValue("@id", carouselId);

            DataTable dt = new DataTable();

            adp.Fill(dt);

            enCarousel carousel = new enCarousel();

            if (dt.Rows.Count > 0)
            {
                DataRow rw = dt.Rows[0];

                carousel.Adi = rw["car_adi"].ToString();
                carousel.Id = rw["car_id"].xToIntDefault();
                carousel.KayitTarihi = rw["car_kayitTar"].xToDateTimeDefault();
                carousel.Statu = rw["car_statu"].xToBooleanDefault();
                carousel.TekrarSayisi = rw["car_tekrarSayisi"].xToIntDefault();
                carousel.GosterimSuresi = rw["car_gosterimSuresi"].xToIntDefault();
            }

            return carousel;
        }

        public void StatuDegistir(enCarousel carousel)
        {
            List<string> prmList = new List<string>();
            List<object> degerList = new List<object>();

            prmList.Add("car_statu"); degerList.Add(carousel.Statu);

            dalManager.MakeAnDbUpdate(prmList, "Carousel", "car_id", carousel.Id, degerList);
        }
    }
}
