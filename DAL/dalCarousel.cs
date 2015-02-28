using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
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
            Dictionary<string, object> dict = new Dictionary<string, object>();

            dict.Add("car_site_id", carousel.SayfaId);
            dict.Add("car_adi", carousel.Adi);
            dict.Add("car_statu", carousel.Statu);
            dict.Add("car_kayitTar", DateTime.Now.Date);
            dict.Add("car_kaydeden", SessionManager.Admin.Id);
            dict.Add("car_tekrarSayisi", carousel.TekrarSayisi);
            dict.Add("car_gosterimSuresi", carousel.GosterimSuresi);

            return FxMySqlHelper.Insert("Carousel", dict, true);
        }

        public void Duzenle(enCarousel carousel)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            List<object> degerList = new List<object>();

            dict.Add("car_site_id", carousel.SayfaId);
            dict.Add("car_adi", carousel.Adi);
            dict.Add("car_statu", carousel.Statu);
            dict.Add("car_degisiklikTar", DateTime.Now.Date);
            dict.Add("car_degistiren", SessionManager.Admin.Id);
            dict.Add("car_tekrarSayisi", carousel.TekrarSayisi);
            dict.Add("car_gosterimSuresi", carousel.GosterimSuresi);

            FxMySqlHelper.Update("Carousel", dict, "car_id", carousel.Id);
        }

        public void Sil(int carouselId)
        {
            FxMySqlHelper.Delete("Carousel", "car_id", carouselId);
        }

        public List<enCarousel> CarouselleriGetir()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(@"SELECT rg.*,(SELECT COUNT(*) FROM CarouselResimleri r WHERE r.carcarRes_car_id = rg.car_id) AS ResimSayisi
                        FROM Carousel rg ");

            MySqlDataAdapter adp = new MySqlDataAdapter(sb.ToString(), FxMySqlHelper.Connection());

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

            MySqlDataAdapter adp = new MySqlDataAdapter(sb.ToString(), FxMySqlHelper.Connection());

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

            MySqlDataAdapter adp = new MySqlDataAdapter(sb.ToString(), FxMySqlHelper.Connection());

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
            Dictionary<string, object> dict = new Dictionary<string, object>();
            List<object> degerList = new List<object>();

            dict.Add("car_statu", carousel.Statu);

            FxMySqlHelper.Update("Carousel", dict, "car_id", carousel.Id);
        }
    }
}
