using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using Entity;

namespace DAL
{
    public class dalIcerikResimleri
    {
        public void YeniResimEkle(enIcerikResim resim)
        {
            List<string> prmList = new List<string>();
            List<object> degerList = new List<object>();

            prmList.Add("icrkRes_site_id"); degerList.Add(resim.SayfaId);
            prmList.Add("icrkRes_aciklama"); degerList.Add(resim.Aciklama);
            prmList.Add("icrkRes_kucuk"); degerList.Add(resim.Kucuk);
            prmList.Add("icrkRes_orta"); degerList.Add(resim.Orta);
            prmList.Add("icrkRes_buyuk"); degerList.Add(resim.Buyuk);
            prmList.Add("icrkRes_sira"); degerList.Add(resim.Sira);
            prmList.Add("icrkRes_statu"); degerList.Add(resim.Statu);
            prmList.Add("icrkRes_kayitTar"); degerList.Add(resim.KayitTarihi);
            prmList.Add("icrkRes_baslik"); degerList.Add(resim.Baslik);
            prmList.Add("icrkRes_anaResim"); degerList.Add(resim.AnaResim);

            dalManager.MakeAnDbInsert(prmList, "IcerikResimleri", degerList, "");

        }

        public void ResimSil(int resimId)
        {
            dalManager.MakeAnDbDelete("IcerikResimleri", "icrkRes_id", resimId);
        }

        public List<enIcerikResim> TumResimleriGetir(bool? statu)
        {
            StringBuilder sb = new StringBuilder();
            OleDbDataAdapter adp = new OleDbDataAdapter(sb.ToString(), dalManager.Connection());

            if (statu != null)
            {
                sb.Append(@"SELECT r.* FROM IcerikResimleri r Inner Join SiteHaritasi s on r.icrkRes_site_id=s.site_id where s.site_urunMu=true AND icrkRes_statu = @statu ORDER BY r.icrkRes_sira ");

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

            OleDbDataAdapter adp = new OleDbDataAdapter(sb.ToString(), dalManager.Connection());

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
            string topStr = kayitSayisi == null ? "" : " TOP " + kayitSayisi + " ";

            StringBuilder sb = new StringBuilder();

            OleDbDataAdapter adp = new OleDbDataAdapter(sb.ToString(), dalManager.Connection());

            if (statu != null)
            {
                sb.Append("SELECT " + topStr + " * FROM IcerikResimleri WHERE icrkRes_site_id = @id AND icrkRes_statu = @statu ORDER BY icrkRes_sira ");

                adp.SelectCommand.Parameters.AddWithValue("@id", sayfaId);
                adp.SelectCommand.Parameters.AddWithValue("@statu", statu);
            }
            else
            {
                sb.Append("SELECT " + topStr + " * FROM IcerikResimleri WHERE icrkRes_site_id = @id ORDER BY icrkRes_sira ");

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

            OleDbDataAdapter adp = new OleDbDataAdapter(sb.ToString(), dalManager.Connection());

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
            List<string> prmList = new List<string>();
            List<object> degerList = new List<object>();

            prmList.Add("icrkRes_statu"); degerList.Add(resim.Statu);

            dalManager.MakeAnDbUpdate(prmList, "IcerikResimleri", "icrkRes_id", resim.Id, degerList);
        }

        public void ResimSiraGuncelle(enIcerikResim resim)
        {
            List<string> prmList = new List<string>();
            List<object> degerList = new List<object>();

            prmList.Add("icrkRes_sira"); degerList.Add(resim.Sira);

            dalManager.MakeAnDbUpdate(prmList, "IcerikResimleri", "icrkRes_id", resim.Id, degerList);
        }

        public void ResimAciklamaGuncelle(enIcerikResim resim)
        {
            List<string> prmList = new List<string>();
            List<object> degerList = new List<object>();

            prmList.Add("icrkRes_aciklama"); degerList.Add(resim.Aciklama);
            prmList.Add("icrkRes_baslik"); degerList.Add(resim.Baslik);

            dalManager.MakeAnDbUpdate(prmList, "IcerikResimleri", "icrkRes_id", resim.Id, degerList);
        }

        public enIcerikResim BirUsttekiResmiGetir(enIcerikResim gResim)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(@"SELECT TOP 1 * FROM IcerikResimleri 
                        WHERE icrkRes_site_id = @sayfaId AND icrkRes_sira < @sira ORDER BY icrkRes_sira DESC");

            OleDbDataAdapter adp = new OleDbDataAdapter(sb.ToString(), dalManager.Connection());

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

            sb.Append(@"SELECT TOP 1 * FROM IcerikResimleri 
                        WHERE icrkRes_site_id = @id AND icrkRes_sira > @sira ORDER BY icrkRes_sira ");

            OleDbDataAdapter adp = new OleDbDataAdapter(sb.ToString(), dalManager.Connection());

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

            sb.Append(@"SELECT TOP 1 icrkRes_sira FROM IcerikResimleri 
                        WHERE icrkRes_site_id = @sayfaId 
                        ORDER BY icrkRes_sira DESC");

            OleDbCommand cmd = new OleDbCommand(sb.ToString(), dalManager.Connection());

            cmd.Parameters.AddWithValue("@sayfaId", sayfaId);

            cmd.Connection.Open();

            int sayi = cmd.ExecuteScalar().xToIntDefault();

            cmd.Connection.Close();

            return sayi;
        }

        public void AnaResimYap(enIcerikResim resim)
        {
            List<string> prmList = new List<string>();
            List<object> degerList = new List<object>();

            prmList.Add("icrkRes_anaResim"); degerList.Add(resim.AnaResim);

            dalManager.MakeAnDbUpdate(prmList, "IcerikResimleri", "icrkRes_id", resim.Id, degerList);
        }

        public enIcerikResim AnaResimGetir(int? sayfaId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * From IcerikResimleri where icrkRes_site_id=@sayfaId and icrkRes_anaResim=true");

            OleDbDataAdapter adp = new OleDbDataAdapter(sb.ToString(), dalManager.Connection());
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
