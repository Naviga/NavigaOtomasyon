using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using Entity;

namespace DAL
{
    public class dalIcerikVideolari
    {
        public void YeniVideoEkle(enIcerikVideo video)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            List<object> degerList = new List<object>();

            dict.Add("icrkVid_site_id"); degerList.Add(video.SayfaId);
            dict.Add("icrkVid_baslik"); degerList.Add(video.Baslik);
            dict.Add("icrkVid_aciklama"); degerList.Add(video.Aciklama);
            dict.Add("icrkVid_source"); degerList.Add(video.Kaynak);
            dict.Add("icrkVid_kapak"); degerList.Add(video.Kapak);
            dict.Add("icrkVid_sira"); degerList.Add(video.Sira);
            dict.Add("icrkVid_statu"); degerList.Add(video.Statu);
            dict.Add("icrkVid_kayitTar"); degerList.Add(video.KayitTarihi);
            dict.Add("icrkVid_kaydeden"); degerList.Add(video.Kaydeden);
            dict.Add("icrkVid_urlKodu"); degerList.Add(video.UrlKodu);

            dalManager.MakeAnDbInsert(prmList, "IcerikVideolari", degerList, "");

        }

        public void VideoSil(int videoId)
        {
            dalManager.MakeAnDbDelete("IcerikVideolari", "icrkVid_id", videoId);
        }

        public List<enIcerikVideo> VideoleriGetir(int sayfaId, bool? statu)
        {
            StringBuilder sb = new StringBuilder();

            MySqlDataAdapter adp = new MySqlDataAdapter(sb.ToString(), FxMySqlHelper.Connection());

            if (statu != null)
            {
                sb.Append("SELECT * FROM IcerikVideolari WHERE icrkVid_site_id = @id AND icrkVid_statu = @statu ORDER BY icrkVid_sira ");

                adp.SelectCommand.Parameters.AddWithValue("@id", sayfaId);
                adp.SelectCommand.Parameters.AddWithValue("@statu", statu);
            }
            else
            {
                sb.Append("SELECT * FROM IcerikVideolari WHERE icrkVid_site_id = @id ORDER BY icrkVid_sira ");

                adp.SelectCommand.Parameters.AddWithValue("@id", sayfaId);
            }

            adp.SelectCommand.CommandText = sb.ToString();

            DataTable dt = new DataTable();

            adp.Fill(dt);

            List<enIcerikVideo> videoler = new List<enIcerikVideo>();

            foreach (DataRow rw in dt.Rows)
            {
                enIcerikVideo video = new enIcerikVideo();

                video.Aciklama = rw["icrkVid_aciklama"].ToString();
                video.Baslik = rw["icrkVid_baslik"].ToString();
                video.SayfaId = rw["icrkVid_site_id"].xToIntDefault();
                video.Id = rw["icrkVid_id"].xToIntDefault();
                video.KayitTarihi = rw["icrkVid_kayitTar"].xToDateTimeDefault();
                video.Kaynak = rw["icrkVid_source"].ToString();
                video.Kapak = rw["icrkVid_kapak"].ToString();
                video.Sira = rw["icrkVid_sira"].xToIntDefault();
                video.Statu = rw["icrkVid_statu"].xToBooleanDefault();
                video.UrlKodu = rw["icrkVid_urlKodu"].ToString();

                videoler.Add(video);
            }

            return videoler;
        }

        public List<enIcerikVideo> VideoleriGetir(int sayfaId, int? kayitSayisi, bool? statu)
        {
            string topStr = kayitSayisi == null ? "" : " TOP " + kayitSayisi + " ";

            StringBuilder sb = new StringBuilder();

            MySqlDataAdapter adp = new MySqlDataAdapter(sb.ToString(), FxMySqlHelper.Connection());

            if (statu != null)
            {
                sb.Append("SELECT " + topStr + " * FROM IcerikVideolari WHERE icrkVid_site_id = @id AND icrkVid_statu = @statu ORDER BY icrkVid_sira ");

                adp.SelectCommand.Parameters.AddWithValue("@id", sayfaId);
                adp.SelectCommand.Parameters.AddWithValue("@statu", statu);
            }
            else
            {
                sb.Append("SELECT " + topStr + " * FROM IcerikVideolari WHERE icrkVid_site_id = @id ORDER BY icrkVid_sira ");

                adp.SelectCommand.Parameters.AddWithValue("@id", sayfaId);
            }

            adp.SelectCommand.CommandText = sb.ToString();

            DataTable dt = new DataTable();

            adp.Fill(dt);

            List<enIcerikVideo> videoler = new List<enIcerikVideo>();

            foreach (DataRow rw in dt.Rows)
            {
                enIcerikVideo video = new enIcerikVideo();

                video.Aciklama = rw["icrkVid_aciklama"].ToString();
                video.Baslik = rw["icrkVid_baslik"].ToString();
                video.SayfaId = rw["icrkVid_site_id"].xToIntDefault();
                video.Id = rw["icrkVid_id"].xToIntDefault();
                video.KayitTarihi = rw["icrkVid_kayitTar"].xToDateTimeDefault();
                video.Kaynak = rw["icrkVid_source"].ToString();
                video.Kapak = rw["icrkVid_kapak"].ToString();
                video.Sira = rw["icrkVid_sira"].xToIntDefault();
                video.Statu = rw["icrkVid_statu"].xToBooleanDefault();
                video.UrlKodu = rw["icrkVid_urlKodu"].ToString();

                videoler.Add(video);
            }

            return videoler;
        }

        public enIcerikVideo VideoGetir(int videoId)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT * FROM IcerikVideolari WHERE icrkVid_id = @id ");

            MySqlDataAdapter adp = new MySqlDataAdapter(sb.ToString(), FxMySqlHelper.Connection());

            adp.SelectCommand.Parameters.AddWithValue("@id", videoId);

            DataTable dt = new DataTable();

            adp.Fill(dt);

            enIcerikVideo video = new enIcerikVideo();

            if (dt.Rows.Count > 0)
            {
                DataRow rw = dt.Rows[0];

                video.Aciklama = rw["icrkVid_aciklama"].ToString();
                video.Baslik = rw["icrkVid_baslik"].ToString();
                video.SayfaId = rw["icrkVid_site_id"].xToIntDefault();
                video.Id = rw["icrkVid_id"].xToIntDefault();
                video.KayitTarihi = rw["icrkVid_kayitTar"].xToDateTimeDefault();
                video.Kaynak = rw["icrkVid_source"].ToString();
                video.Kapak = rw["icrkVid_kapak"].ToString();
                video.Sira = rw["icrkVid_sira"].xToIntDefault();
                video.Statu = rw["icrkVid_statu"].xToBooleanDefault();
                video.UrlKodu = rw["icrkVid_urlKodu"].ToString();

            }

            return video;
        }

        public void VideoStatuGuncelle(enIcerikVideo video)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            List<object> degerList = new List<object>();

            dict.Add("icrkVid_statu"); degerList.Add(video.Statu);

            dalManager.MakeAnDbUpdate(prmList, "IcerikVideolari", "icrkVid_id", video.Id, degerList);
        }

        public void VideoSiraGuncelle(enIcerikVideo video)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            List<object> degerList = new List<object>();

            dict.Add("icrkVid_sira"); degerList.Add(video.Sira);

            dalManager.MakeAnDbUpdate(prmList, "IcerikVideolari", "icrkVid_id", video.Id, degerList);
        }

        public void VideoAciklamaGuncelle(enIcerikVideo video)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            List<object> degerList = new List<object>();

            dict.Add("icrkVid_aciklama"); degerList.Add(video.Aciklama);

            dalManager.MakeAnDbUpdate(prmList, "IcerikVideolari", "icrkVid_id", video.Id, degerList);
        }

        public enIcerikVideo BirUsttekiVideoyuGetir(enIcerikVideo gvideo)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(@"SELECT TOP 1 * FROM IcerikVideolari 
                        WHERE icrkVid_site_id = @sayfaId AND icrkVid_sira < @sira ORDER BY icrkVid_sira DESC");

            MySqlDataAdapter adp = new MySqlDataAdapter(sb.ToString(), FxMySqlHelper.Connection());

            adp.SelectCommand.Parameters.AddWithValue("@sayfaId", gvideo.SayfaId);
            adp.SelectCommand.Parameters.AddWithValue("@sira", gvideo.Sira);

            DataTable dt = new DataTable();

            adp.Fill(dt);

            enIcerikVideo video = new enIcerikVideo();

            if (dt.Rows.Count > 0)
            {
                DataRow rw = dt.Rows[0];

                video.Aciklama = rw["icrkVid_aciklama"].ToString();
                video.Baslik = rw["icrkVid_baslik"].ToString();
                video.SayfaId = rw["icrkVid_site_id"].xToIntDefault();
                video.Id = rw["icrkVid_id"].xToIntDefault();
                video.KayitTarihi = rw["icrkVid_kayitTar"].xToDateTimeDefault();
                video.Kaynak = rw["icrkVid_source"].ToString();
                video.Kapak = rw["icrkVid_kapak"].ToString();
                video.Sira = rw["icrkVid_sira"].xToIntDefault();
                video.Statu = rw["icrkVid_statu"].xToBooleanDefault();
                video.UrlKodu = rw["icrkVid_urlKodu"].ToString();
            }

            return video;
        }

        public enIcerikVideo BirAlttakiVideoyuGetir(enIcerikVideo gvideo)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(@"SELECT TOP 1 * FROM IcerikVideolari 
                        WHERE icrkVid_site_id = @id AND icrkVid_sira > @sira ORDER BY icrkVid_sira ");

            MySqlDataAdapter adp = new MySqlDataAdapter(sb.ToString(), FxMySqlHelper.Connection());

            adp.SelectCommand.Parameters.AddWithValue("@id", gvideo.SayfaId);

            adp.SelectCommand.Parameters.AddWithValue("@sira", gvideo.Sira);

            DataTable dt = new DataTable();

            adp.Fill(dt);

            enIcerikVideo video = new enIcerikVideo();

            if (dt.Rows.Count > 0)
            {
                DataRow rw = dt.Rows[0];

                video.Aciklama = rw["icrkVid_aciklama"].ToString();
                video.Baslik = rw["icrkVid_baslik"].ToString();
                video.SayfaId = rw["icrkVid_site_id"].xToIntDefault();
                video.Id = rw["icrkVid_id"].xToIntDefault();
                video.KayitTarihi = rw["icrkVid_kayitTar"].xToDateTimeDefault();
                video.Kaynak = rw["icrkVid_source"].ToString();
                video.Kapak = rw["icrkVid_kapak"].ToString();
                video.Sira = rw["icrkVid_sira"].xToIntDefault();
                video.Statu = rw["icrkVid_statu"].xToBooleanDefault();
                video.UrlKodu = rw["icrkVid_urlKodu"].ToString();
            }

            return video;
        }

        public int SonSiraNoGetir(int sayfaId)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(@"SELECT TOP 1 icrkVid_sira FROM IcerikVideolari 
                        WHERE icrkVid_site_id = @sayfaId 
                        ORDER BY icrkVid_sira DESC");

            OleDbCommand cmd = new OleDbCommand(sb.ToString(), FxMySqlHelper.Connection());

            cmd.Parameters.AddWithValue("@sayfaId", sayfaId);

            cmd.Connection.Open();

            int sayi = cmd.ExecuteScalar().xToIntDefault();

            cmd.Connection.Close();

            return sayi;
        }
    }
}
