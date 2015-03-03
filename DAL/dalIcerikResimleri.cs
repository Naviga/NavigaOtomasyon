using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using Entity;

namespace DAL
{
    public class dalIcerikResimleri
    {
        public void YeniResimEkle(enIcerikResim resim)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            List<object> degerList = new List<object>();

            dict.Add("icrkRes_site_id", resim.SayfaId); 
            dict.Add("icrkRes_aciklama", resim.Aciklama); 
            dict.Add("icrkRes_kucuk", resim.Kucuk); 
            dict.Add("icrkRes_orta", resim.Orta); 
            dict.Add("icrkRes_buyuk", resim.Buyuk); 
            dict.Add("icrkRes_sira", resim.Sira); 
            dict.Add("icrkRes_statu", resim.Statu); 
            dict.Add("icrkRes_kayitTar", resim.KayitTarihi); 
            dict.Add("icrkRes_baslik", resim.Baslik); 
            dict.Add("icrkRes_anaResim", resim.AnaResim); ;

            FxMySqlHelper.Insert("IcerikResimleri", dict);

        }

        public void ResimSil(int resimId)
        {
            FxMySqlHelper.Delete("IcerikResimleri", "icrkRes_id", resimId);
        }

        public List<enIcerikResim> TumResimleriGetir(bool? statu)
        {
            StringBuilder sb = new StringBuilder();
            MySqlDataAdapter adp = new MySqlDataAdapter(sb.ToString(), FxMySqlHelper.Connection());

            if (statu != null)
            {
                sb.Append(@"SELECT r.* FROM IcerikResimleri r Inner Join SiteHaritasi s on r.icrkRes_site_id=s.site_id where s.site_urunMu=true AND r.icrkRes_statu = @statu ORDER BY r.icrkRes_sira ");

                adp.SelectCommand.Parameters.AddWithValue("@statu", statu);
            }
            else
            {
                sb.Append(@"SELECT r.* FROM IcerikResimleri r Inner Join SiteHaritasi s on r.icrkRes_site_id=s.site_id where s.site_urunMu=true ORDER BY r.icrkRes_sira ");
            }

            adp.SelectCommand.CommandText = sb.ToString();

            DataTable dt = new DataTable();

            adp.Fill(dt);

            List<enIcerikResim> resimler = new List<enIcerikResim>();

            foreach (DataRow rw in dt.Rows)
            {
                enIcerikResim resim = new enIcerikResim();

                resim.Aciklama = rw["icrkRes_aciklama"].ToString();
                resim.Buyuk = rw["icrkRes_buyuk"].ToString();
                resim.SayfaId = rw["icrkRes_site_id"].xToIntDefault();
                resim.Id = rw["icrkRes_id"].xToIntDefault();
                resim.KayitTarihi = rw["icrkRes_kayitTar"].xToDateTimeDefault();
                resim.Kucuk = rw["icrkRes_kucuk"].ToString();
                resim.Orta = rw["icrkRes_orta"].ToString();
                resim.Sira = rw["icrkRes_sira"].xToIntDefault();
                resim.Statu = rw["icrkRes_statu"].xToBooleanDefault();
                resim.Baslik = rw["icrkRes_baslik"].ToString();
                resim.AnaResim = rw["icrkRes_anaResim"].xToBooleanDefault();

                resimler.Add(resim);
            }

            return resimler;

        }

        public List<enIcerikResim> ResimleriGetir(int sayfaId, bool? statu)
        {
            StringBuilder sb = new StringBuilder();

            MySqlDataAdapter adp = new MySqlDataAdapter(sb.ToString(), FxMySqlHelper.Connection());

            if (statu != null)
            {
                sb.Append("SELECT * FROM IcerikResimleri WHERE icrkRes_site_id = @id AND icrkRes_statu = @statu ORDER BY icrkRes_sira ");

                adp.SelectCommand.Parameters.AddWithValue("@id", sayfaId);
                adp.SelectCommand.Parameters.AddWithValue("@statu", statu);
            }
            else
            {
                sb.Append("SELECT * FROM IcerikResimleri WHERE icrkRes_site_id = @id ORDER BY icrkRes_sira ");

                adp.SelectCommand.Parameters.AddWithValue("@id", sayfaId);
            }

            adp.SelectCommand.CommandText = sb.ToString();

            DataTable dt = new DataTable();

            adp.Fill(dt);

            List<enIcerikResim> resimler = new List<enIcerikResim>();

            foreach (DataRow rw in dt.Rows)
            {
                enIcerikResim resim = new enIcerikResim();

                resim.Aciklama = rw["icrkRes_aciklama"].ToString();
                resim.Buyuk = rw["icrkRes_buyuk"].ToString();
                resim.SayfaId = rw["icrkRes_site_id"].xToIntDefault();
                resim.Id = rw["icrkRes_id"].xToIntDefault();
                resim.KayitTarihi = rw["icrkRes_kayitTar"].xToDateTimeDefault();
                resim.Kucuk = rw["icrkRes_kucuk"].ToString();
                resim.Orta = rw["icrkRes_orta"].ToString();
                resim.Sira = rw["icrkRes_sira"].xToIntDefault();
                resim.Statu = rw["icrkRes_statu"].xToBooleanDefault();
                resim.Baslik = rw["icrkRes_baslik"].ToString();
                resim.AnaResim = rw["icrkRes_anaResim"].xToBooleanDefault();

                resimler.Add(resim);
            }

            return resimler;
        }

        public List<enIcerikResim> ResimleriGetir(int sayfaId, int? kayitSayisi, bool? statu)
        {
            string topStr = kayitSayisi == null ? "" : " LIMIT " + kayitSayisi + " ";

            StringBuilder sb = new StringBuilder();

            MySqlDataAdapter adp = new MySqlDataAdapter(sb.ToString(), FxMySqlHelper.Connection());

            if (statu != null)
            {
                sb.Append("SELECT * FROM IcerikResimleri WHERE icrkRes_site_id = @id AND icrkRes_statu = @statu ORDER BY icrkRes_sira " + topStr );

                adp.SelectCommand.Parameters.AddWithValue("@id", sayfaId);
                adp.SelectCommand.Parameters.AddWithValue("@statu", statu);
            }
            else
            {
                sb.Append("SELECT * FROM IcerikResimleri WHERE icrkRes_site_id = @id ORDER BY icrkRes_sira " + topStr);

                adp.SelectCommand.Parameters.AddWithValue("@id", sayfaId);
            }

            adp.SelectCommand.CommandText = sb.ToString();

            DataTable dt = new DataTable();

            adp.Fill(dt);

            List<enIcerikResim> resimler = new List<enIcerikResim>();

            foreach (DataRow rw in dt.Rows)
            {
                enIcerikResim resim = new enIcerikResim();

                resim.Aciklama = rw["icrkRes_aciklama"].ToString();
                resim.Buyuk = rw["icrkRes_buyuk"].ToString();
                resim.SayfaId = rw["icrkRes_site_id"].xToIntDefault();
                resim.Id = rw["icrkRes_id"].xToIntDefault();
                resim.KayitTarihi = rw["icrkRes_kayitTar"].xToDateTimeDefault();
                resim.Kucuk = rw["icrkRes_kucuk"].ToString();
                resim.Orta = rw["icrkRes_orta"].ToString();
                resim.Sira = rw["icrkRes_sira"].xToIntDefault();
                resim.Statu = rw["icrkRes_statu"].xToBooleanDefault();
                resim.Baslik = rw["icrkRes_baslik"].ToString();
                resim.AnaResim = rw["icrkRes_anaResim"].xToBooleanDefault();

                resimler.Add(resim);
            }

            return resimler;
        }

        public enIcerikResim ResimGetir(int resimId)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT * FROM IcerikResimleri WHERE icrkRes_id = @id ");

            MySqlDataAdapter adp = new MySqlDataAdapter(sb.ToString(), FxMySqlHelper.Connection());

            adp.SelectCommand.Parameters.AddWithValue("@id", resimId);

            DataTable dt = new DataTable();

            adp.Fill(dt);

            enIcerikResim resim = new enIcerikResim();

            if (dt.Rows.Count > 0)
            {
                DataRow rw = dt.Rows[0];

                resim.Aciklama = rw["icrkRes_aciklama"].ToString();
                resim.Buyuk = rw["icrkRes_buyuk"].ToString();
                resim.SayfaId = rw["icrkRes_site_id"].xToIntDefault();
                resim.Id = rw["icrkRes_id"].xToIntDefault();
                resim.KayitTarihi = rw["icrkRes_kayitTar"].xToDateTimeDefault();
                resim.Kucuk = rw["icrkRes_kucuk"].ToString();
                resim.Orta = rw["icrkRes_orta"].ToString();
                resim.Sira = rw["icrkRes_sira"].xToIntDefault();
                resim.Statu = rw["icrkRes_statu"].xToBooleanDefault();
                resim.Baslik = rw["icrkRes_baslik"].ToString();
                resim.AnaResim = rw["icrkRes_anaResim"].xToBooleanDefault();
            }

            return resim;
        }

        public void ResimStatuGuncelle(enIcerikResim resim)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            List<object> degerList = new List<object>();

            dict.Add("icrkRes_statu", resim.Statu);
            FxMySqlHelper.Update("IcerikResimleri", dict, "icrkRes_id",resim.Id);

        }

        public void ResimSiraGuncelle(enIcerikResim resim)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            List<object> degerList = new List<object>();

            dict.Add("icrkRes_sira",resim.Sira);
            FxMySqlHelper.Update("IcerikResimleri", dict, "icrkRes_id", resim.Id);
        }

        public void ResimAciklamaGuncelle(enIcerikResim resim)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            List<object> degerList = new List<object>();

            dict.Add("icrkRes_aciklama",resim.Aciklama);
            dict.Add("icrkRes_baslik",resim.Baslik);

            FxMySqlHelper.Update("IcerikResimleri", dict, "icrkRes_id", resim.Id);
        }

        public enIcerikResim BirUsttekiResmiGetir(enIcerikResim gResim)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(@"SELECT * FROM IcerikResimleri 
                        WHERE icrkRes_site_id = @sayfaId AND icrkRes_sira < @sira ORDER BY icrkRes_sira DESC LIMIT 1");

            MySqlDataAdapter adp = new MySqlDataAdapter(sb.ToString(), FxMySqlHelper.Connection());

            adp.SelectCommand.Parameters.AddWithValue("@sayfaId", gResim.SayfaId);
            adp.SelectCommand.Parameters.AddWithValue("@sira", gResim.Sira);

            DataTable dt = new DataTable();

            adp.Fill(dt);

            enIcerikResim resim = new enIcerikResim();

            if (dt.Rows.Count > 0)
            {
                DataRow rw = dt.Rows[0];

                resim.Aciklama = rw["icrkRes_aciklama"].ToString();
                resim.Buyuk = rw["icrkRes_buyuk"].ToString();
                resim.SayfaId = rw["icrkRes_site_id"].xToIntDefault();
                resim.Id = rw["icrkRes_id"].xToIntDefault();
                resim.KayitTarihi = rw["icrkRes_kayitTar"].xToDateTimeDefault();
                resim.Kucuk = rw["icrkRes_kucuk"].ToString();
                resim.Orta = rw["icrkRes_orta"].ToString();
                resim.Sira = rw["icrkRes_sira"].xToIntDefault();
                resim.Statu = rw["icrkRes_statu"].xToBooleanDefault();
                resim.AnaResim = rw["icrkRes_anaResim"].xToBooleanDefault();
            }

            return resim;
        }

        public enIcerikResim BirAlttakiResmiGetir(enIcerikResim gResim)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(@"SELECT * FROM IcerikResimleri 
                        WHERE icrkRes_site_id = @id AND icrkRes_sira > @sira ORDER BY icrkRes_sira  LIMIT 1");

            MySqlDataAdapter adp = new MySqlDataAdapter(sb.ToString(), FxMySqlHelper.Connection());

            adp.SelectCommand.Parameters.AddWithValue("@id", gResim.SayfaId);

            adp.SelectCommand.Parameters.AddWithValue("@sira", gResim.Sira);

            DataTable dt = new DataTable();

            adp.Fill(dt);

            enIcerikResim resim = new enIcerikResim();

            if (dt.Rows.Count > 0)
            {
                DataRow rw = dt.Rows[0];

                resim.Aciklama = rw["icrkRes_aciklama"].ToString();
                resim.Buyuk = rw["icrkRes_buyuk"].ToString();
                resim.SayfaId = rw["icrkRes_site_id"].xToIntDefault();
                resim.Id = rw["icrkRes_id"].xToIntDefault();
                resim.KayitTarihi = rw["icrkRes_kayitTar"].xToDateTimeDefault();
                resim.Kucuk = rw["icrkRes_kucuk"].ToString();
                resim.Orta = rw["icrkRes_orta"].ToString();
                resim.Sira = rw["icrkRes_sira"].xToIntDefault();
                resim.Statu = rw["icrkRes_statu"].xToBooleanDefault();
                resim.AnaResim = rw["icrkRes_anaResim"].xToBooleanDefault();
            }

            return resim;
        }

        public int SonSiraNoGetir(int sayfaId)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(@"SELECT icrkRes_sira FROM IcerikResimleri 
                        WHERE icrkRes_site_id = @sayfaId 
                        ORDER BY icrkRes_sira DESC LIMIT 1");

            MySqlCommand cmd = new MySqlCommand(sb.ToString(), FxMySqlHelper.Connection());
 
            cmd.Parameters.AddWithValue("@sayfaId", sayfaId);

            cmd.Connection.Open();

            int sayi = cmd.ExecuteScalar().xToIntDefault();

            cmd.Connection.Close();

            return sayi;
        }

        public void AnaResimYap(enIcerikResim resim)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            List<object> degerList = new List<object>();

            dict.Add("icrkRes_anaResim",resim.AnaResim);

            FxMySqlHelper.Update("IcerikResimleri", dict, "icrkRes_id", resim.Id);

        }

        public enIcerikResim AnaResimGetir(int? sayfaId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * From IcerikResimleri where icrkRes_site_id=@sayfaId and icrkRes_anaResim=true");

            MySqlDataAdapter adp = new MySqlDataAdapter(sb.ToString(), FxMySqlHelper.Connection());
            adp.SelectCommand.Parameters.AddWithValue("@sayfaId", sayfaId);

            DataTable dt = new DataTable();
            adp.Fill(dt);

            enIcerikResim resim = new enIcerikResim();

            if (dt.Rows.Count > 0)
            {
                DataRow rw = dt.Rows[0];

                resim.Aciklama = rw["icrkRes_aciklama"].ToString();
                resim.Buyuk = rw["icrkRes_buyuk"].ToString();
                resim.SayfaId = rw["icrkRes_site_id"].xToIntDefault();
                resim.Id = rw["icrkRes_id"].xToIntDefault();
                resim.KayitTarihi = rw["icrkRes_kayitTar"].xToDateTimeDefault();
                resim.Kucuk = rw["icrkRes_kucuk"].ToString();
                resim.Orta = rw["icrkRes_orta"].ToString();
                resim.Sira = rw["icrkRes_sira"].xToIntDefault();
                resim.Statu = rw["icrkRes_statu"].xToBooleanDefault();
                resim.AnaResim = rw["icrkRes_anaResim"].xToBooleanDefault();
            }

            return resim;
        }

    }
}
