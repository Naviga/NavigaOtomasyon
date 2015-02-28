using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using Entity;

namespace DAL
{
    public class dalCarouselResimleri
    {
        public void YeniResimEkle(enCarouselResim resim)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();


            dict.Add("carRes_car_id", resim.CarouselId);
            dict.Add("carRes_kucuk", resim.Kucuk);
            dict.Add("carRes_orta", resim.Orta);
            dict.Add("carRes_buyuk", resim.Buyuk);
            dict.Add("carRes_sira", resim.Sira);
            dict.Add("carRes_statu", resim.Statu);
            dict.Add("carRes_kayitTar", resim.KayitTarihi);
            dict.Add("carRes_baslik", resim.Baslik);
            dict.Add("carRes_navUrl", resim.NavUrl);
            dict.Add("carRes_fotoLink", resim.FotoLink);
            dict.Add("carRes_videoLink", resim.VideoLink);

            FxMySqlHelper.Insert("CarouselResimleri", dict);

        }

        public void ResimSil(int resimId)
        {
            FxMySqlHelper.Delete("CarouselResimleri", "carRes_id", resimId);
        }

        public List<enCarouselResim> ResimleriGetir(int sayfaId, bool? statu)
        {
            StringBuilder sb = new StringBuilder();

            MySqlDataAdapter adp = new MySqlDataAdapter(sb.ToString(), FxMySqlHelper.Connection());

            if (statu != null)
            {
                sb.Append("SELECT * FROM CarouselResimleri WHERE carRes_car_id = @id AND carRes_statu = @statu ORDER BY carRes_sira ");

                adp.SelectCommand.Parameters.AddWithValue("@id", sayfaId);
                adp.SelectCommand.Parameters.AddWithValue("@statu", statu);
            }
            else
            {
                sb.Append("SELECT * FROM CarouselResimleri WHERE carRes_car_id = @id ORDER BY carRes_sira ");

                adp.SelectCommand.Parameters.AddWithValue("@id", sayfaId);
            }

            adp.SelectCommand.CommandText = sb.ToString();

            DataTable dt = new DataTable();

            adp.Fill(dt);

            List<enCarouselResim> resimler = new List<enCarouselResim>();

            foreach (DataRow rw in dt.Rows)
            {
                enCarouselResim resim = new enCarouselResim();

                resim.Buyuk = rw["carRes_buyuk"].ToString();
                resim.CarouselId = rw["carRes_car_id"].xToIntDefault();
                resim.Id = rw["carRes_id"].xToIntDefault();
                resim.KayitTarihi = rw["carRes_kayitTar"].xToDateTimeDefault();
                resim.Kucuk = rw["carRes_kucuk"].ToString();
                resim.Orta = rw["carRes_orta"].ToString();
                resim.Sira = rw["carRes_sira"].xToIntDefault();
                resim.Statu = rw["carRes_statu"].xToBooleanDefault();
                resim.Baslik = rw["carRes_baslik"].ToString();
                resim.NavUrl = rw["carRes_navUrl"].ToString();
                resim.FotoLink = rw["carRes_fotoLink"].xToBooleanDefault();
                resim.VideoLink = rw["carRes_videoLink"].xToBooleanDefault();

                resimler.Add(resim);
            }

            return resimler;
        }

        public List<enCarouselResim> ResimleriGetir(int sayfaId, int? kayitSayisi, bool? statu)
        {
            string topStr = kayitSayisi == null ? "" : " TOP " + kayitSayisi + " ";

            StringBuilder sb = new StringBuilder();

            MySqlDataAdapter adp = new MySqlDataAdapter(sb.ToString(), FxMySqlHelper.Connection());

            if (statu != null)
            {
                sb.Append("SELECT " + topStr + " * FROM CarouselResimleri WHERE carRes_car_id = @id AND carRes_statu = @statu ORDER BY carRes_sira ");

                adp.SelectCommand.Parameters.AddWithValue("@id", sayfaId);
                adp.SelectCommand.Parameters.AddWithValue("@statu", statu);
            }
            else
            {
                sb.Append("SELECT " + topStr + " * FROM CarouselResimleri WHERE carRes_car_id = @id ORDER BY carRes_sira ");

                adp.SelectCommand.Parameters.AddWithValue("@id", sayfaId);
            }

            adp.SelectCommand.CommandText = sb.ToString();

            DataTable dt = new DataTable();

            adp.Fill(dt);

            List<enCarouselResim> resimler = new List<enCarouselResim>();

            foreach (DataRow rw in dt.Rows)
            {
                enCarouselResim resim = new enCarouselResim();

                resim.Buyuk = rw["carRes_buyuk"].ToString();
                resim.CarouselId = rw["carRes_car_id"].xToIntDefault();
                resim.Id = rw["carRes_id"].xToIntDefault();
                resim.KayitTarihi = rw["carRes_kayitTar"].xToDateTimeDefault();
                resim.Kucuk = rw["carRes_kucuk"].ToString();
                resim.Orta = rw["carRes_orta"].ToString();
                resim.Sira = rw["carRes_sira"].xToIntDefault();
                resim.Statu = rw["carRes_statu"].xToBooleanDefault();
                resim.Baslik = rw["carRes_baslik"].ToString();
                resim.NavUrl = rw["carRes_navUrl"].ToString();
                resim.FotoLink = rw["carRes_fotoLink"].xToBooleanDefault();
                resim.VideoLink = rw["carRes_videoLink"].xToBooleanDefault();

                resimler.Add(resim);
            }

            return resimler;
        }

        public enCarouselResim ResimGetir(int resimId)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT * FROM CarouselResimleri WHERE carRes_id = @id ");

            MySqlDataAdapter adp = new MySqlDataAdapter(sb.ToString(), FxMySqlHelper.Connection());

            adp.SelectCommand.Parameters.AddWithValue("@id", resimId);

            DataTable dt = new DataTable();

            adp.Fill(dt);

            enCarouselResim resim = new enCarouselResim();

            if (dt.Rows.Count > 0)
            {
                DataRow rw = dt.Rows[0];

                resim.Buyuk = rw["carRes_buyuk"].ToString();
                resim.CarouselId = rw["carRes_car_id"].xToIntDefault();
                resim.Id = rw["carRes_id"].xToIntDefault();
                resim.KayitTarihi = rw["carRes_kayitTar"].xToDateTimeDefault();
                resim.Kucuk = rw["carRes_kucuk"].ToString();
                resim.Orta = rw["carRes_orta"].ToString();
                resim.Sira = rw["carRes_sira"].xToIntDefault();
                resim.Statu = rw["carRes_statu"].xToBooleanDefault();
                resim.Baslik = rw["carRes_baslik"].ToString();
                resim.NavUrl = rw["carRes_navUrl"].ToString();
                resim.FotoLink = rw["carRes_fotoLink"].xToBooleanDefault();
                resim.VideoLink = rw["carRes_videoLink"].xToBooleanDefault();
            }

            return resim;
        }

        public void ResimStatuGuncelle(enCarouselResim resim)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();

            dict.Add("carRes_statu", resim.Statu);

            FxMySqlHelper.Update("CarouselResimleri", dict, "carRes_id", resim.Id);
        }

        public void FotoVideoLinkGuncelle(enCarouselResim resim)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();


            dict.Add("carRes_fotoLink", resim.FotoLink);
            dict.Add("carRes_videoLink", resim.VideoLink);

            FxMySqlHelper.Update("CarouselResimleri", dict, "carRes_id", resim.Id);
        }

        public void ResimSiraGuncelle(enCarouselResim resim)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();


            dict.Add("carRes_sira", resim.Sira);

            FxMySqlHelper.Update("CarouselResimleri", dict, "carRes_id", resim.Id);
        }

        public void ResimBaslikGuncelle(enCarouselResim resim)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();


            dict.Add("carRes_baslik", resim.Baslik);

            FxMySqlHelper.Update("CarouselResimleri", dict, "carRes_id", resim.Id);
        }

        public void NavUrlGuncelle(enCarouselResim resim)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();


            dict.Add("carRes_navUrl", resim.NavUrl);

            FxMySqlHelper.Update("CarouselResimleri", dict, "carRes_id", resim.Id);
        }

        public enCarouselResim BirUsttekiResmiGetir(enCarouselResim gResim)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(@"SELECT TOP 1 * FROM CarouselResimleri 
                        WHERE carRes_car_id = @sayfaId AND carRes_sira < @sira ORDER BY carRes_sira DESC");

            MySqlDataAdapter adp = new MySqlDataAdapter(sb.ToString(), FxMySqlHelper.Connection());

            adp.SelectCommand.Parameters.AddWithValue("@sayfaId", gResim.CarouselId);
            adp.SelectCommand.Parameters.AddWithValue("@sira", gResim.Sira);

            DataTable dt = new DataTable();

            adp.Fill(dt);

            enCarouselResim resim = new enCarouselResim();

            if (dt.Rows.Count > 0)
            {
                DataRow rw = dt.Rows[0];

                resim.Buyuk = rw["carRes_buyuk"].ToString();
                resim.CarouselId = rw["carRes_car_id"].xToIntDefault();
                resim.Id = rw["carRes_id"].xToIntDefault();
                resim.KayitTarihi = rw["carRes_kayitTar"].xToDateTimeDefault();
                resim.Kucuk = rw["carRes_kucuk"].ToString();
                resim.Orta = rw["carRes_orta"].ToString();
                resim.Sira = rw["carRes_sira"].xToIntDefault();
                resim.Statu = rw["carRes_statu"].xToBooleanDefault();
            }

            return resim;
        }

        public enCarouselResim BirAlttakiResmiGetir(enCarouselResim gResim)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(@"SELECT TOP 1 * FROM CarouselResimleri 
                        WHERE carRes_car_id = @id AND carRes_sira > @sira ORDER BY carRes_sira ");

            MySqlDataAdapter adp = new MySqlDataAdapter(sb.ToString(), FxMySqlHelper.Connection());

            adp.SelectCommand.Parameters.AddWithValue("@id", gResim.CarouselId);

            adp.SelectCommand.Parameters.AddWithValue("@sira", gResim.Sira);

            DataTable dt = new DataTable();

            adp.Fill(dt);

            enCarouselResim resim = new enCarouselResim();

            if (dt.Rows.Count > 0)
            {
                DataRow rw = dt.Rows[0];

                resim.Buyuk = rw["carRes_buyuk"].ToString();
                resim.CarouselId = rw["carRes_car_id"].xToIntDefault();
                resim.Id = rw["carRes_id"].xToIntDefault();
                resim.KayitTarihi = rw["carRes_kayitTar"].xToDateTimeDefault();
                resim.Kucuk = rw["carRes_kucuk"].ToString();
                resim.Orta = rw["carRes_orta"].ToString();
                resim.Sira = rw["carRes_sira"].xToIntDefault();
                resim.Statu = rw["carRes_statu"].xToBooleanDefault();
            }

            return resim;
        }

        public int SonSiraNoGetir(int sayfaId)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(@"SELECT TOP 1 carRes_sira FROM CarouselResimleri 
                        WHERE carRes_car_id = @sayfaId 
                        ORDER BY carRes_sira DESC");

            MySqlCommand cmd = new MySqlCommand(sb.ToString(), FxMySqlHelper.Connection());

            cmd.Parameters.AddWithValue("@sayfaId", sayfaId);

            cmd.Connection.Open();

            int sayi = cmd.ExecuteScalar().xToIntDefault();

            cmd.Connection.Close();

            return sayi;
        }
    }
}
