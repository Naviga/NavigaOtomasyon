using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using Common;
using Entity;

namespace DAL
{
    public class dalSiteHaritasi_Blok
    {
        public void YukseklikGuncelle(enSiteHaritasi_Blok sBlok)
        {
            string strW = sBlok.SayfaId == 0 ? "" : " AND site_id = @siteId ";

            StringBuilder sb = new StringBuilder();

            sb.Append("UPDATE SiteHaritasi_Blok ");
            sb.Append("SET height = @height ");
            sb.Append("WHERE bPoz_id = @pozId AND blok_id = @blokId " + strW);

            MySqlCommand cmd = new MySqlCommand(sb.ToString(), FxMySqlHelper.Connection());
  
            cmd.Parameters.AddWithValue("@height", sBlok.Height.xToIntDefault() == 0 ? "" : sBlok.Height);
            cmd.Parameters.AddWithValue("@bPoz_id", sBlok.PozisyonId);
            cmd.Parameters.AddWithValue("@blokId", sBlok.BlokId);

            if (sBlok.SayfaId != 0)
            {
                cmd.Parameters.AddWithValue("@siteId", sBlok.SayfaId);
            }

            cmd.Connection.Open();

            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
        }

        public void CerceveRengiGuncelle(enSiteHaritasi_Blok sBlok)
        {
            string strW = sBlok.SayfaId == 0 ? "" : " AND site_id = @siteId ";

            StringBuilder sb = new StringBuilder();

            sb.Append("UPDATE SiteHaritasi_Blok ");
            sb.Append("SET cerceve_rengi = @renk ");
            sb.Append("WHERE bPoz_id = @pozId AND blok_id = @blokId " + strW);

            MySqlCommand cmd = new MySqlCommand(sb.ToString(), FxMySqlHelper.Connection());

            cmd.Parameters.AddWithValue("@cerceve_rengi", sBlok.CerceveRengi);
            cmd.Parameters.AddWithValue("@bPoz_id", sBlok.PozisyonId);
            cmd.Parameters.AddWithValue("@blokId", sBlok.BlokId);

            if (sBlok.SayfaId != 0)
            {
                cmd.Parameters.AddWithValue("@siteId", sBlok.SayfaId);
            }

            cmd.Connection.Open();

            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
        }

        public void MetinRengiGuncelle(enSiteHaritasi_Blok sBlok)
        {
            string strW = sBlok.SayfaId == 0 ? "" : " AND site_id = @siteId ";

            StringBuilder sb = new StringBuilder();

            sb.Append("UPDATE SiteHaritasi_Blok ");
            sb.Append("SET metin_rengi = @renk ");
            sb.Append("WHERE bPoz_id = @pozId AND blok_id = @blokId " + strW);

            MySqlCommand cmd = new MySqlCommand(sb.ToString(), FxMySqlHelper.Connection());

            cmd.Parameters.AddWithValue("@metin_rengi", sBlok.MetinRengi);
            cmd.Parameters.AddWithValue("@bPoz_id", sBlok.PozisyonId);
            cmd.Parameters.AddWithValue("@blokId", sBlok.BlokId);

            if (sBlok.SayfaId != 0)
            {
                cmd.Parameters.AddWithValue("@siteId", sBlok.SayfaId);
            }

            cmd.Connection.Open();

            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
        }

        public void ArkaPlanRengiGuncelle(enSiteHaritasi_Blok sBlok)
        {
            string strW = sBlok.SayfaId == 0 ? "" : " AND site_id = @siteId ";

            StringBuilder sb = new StringBuilder();

            sb.Append("UPDATE SiteHaritasi_Blok ");
            sb.Append("SET arkaplan_rengi = @renk ");
            sb.Append("WHERE bPoz_id = @pozId AND blok_id = @blokId " + strW);

            MySqlCommand cmd = new MySqlCommand(sb.ToString(), FxMySqlHelper.Connection());

            cmd.Parameters.AddWithValue("@arkaplan_rengi", sBlok.ArkaplanRengi);
            cmd.Parameters.AddWithValue("@bPoz_id", sBlok.PozisyonId);
            cmd.Parameters.AddWithValue("@blokId", sBlok.BlokId);

            if (sBlok.SayfaId != 0)
            {
                cmd.Parameters.AddWithValue("@siteId", sBlok.SayfaId);
            }

            cmd.Connection.Open();

            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
        }

        public void YerSiraGuncelle(enSiteHaritasi_Blok sBlok)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("UPDATE SiteHaritasi_Blok ");
            sb.Append("SET bPoz_id = @pozId, ");
            sb.Append("sira = @sira ");
            sb.Append("WHERE site_id = @siteId AND blok_id = @blokId ");

            MySqlCommand cmd = new MySqlCommand(sb.ToString(), FxMySqlHelper.Connection());

            cmd.Parameters.AddWithValue("@pozId", sBlok.PozisyonId);
            cmd.Parameters.AddWithValue("@sira", sBlok.Sira);
            cmd.Parameters.AddWithValue("@siteId", sBlok.SayfaId);
            cmd.Parameters.AddWithValue("@blokId", sBlok.BlokId);

            cmd.Connection.Open();

            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
        }

        public void PozisyonSiraGuncelle(enSiteHaritasi_Blok sBlok)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("UPDATE SiteHaritasi_Blok ");
            sb.Append("sira = @sira ");
            sb.Append("WHERE bPoz_id = @pozId AND blok_id = @blokId ");

            MySqlCommand cmd = new MySqlCommand(sb.ToString(), FxMySqlHelper.Connection());

            cmd.Parameters.AddWithValue("@sira", sBlok.Sira);
            cmd.Parameters.AddWithValue("@pozId", sBlok.PozisyonId);
            cmd.Parameters.AddWithValue("@blokId", sBlok.BlokId);

            cmd.Connection.Open();

            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
        }

        public List<enBlok> Getir(int sayfaId, int? pozisyonId, bool? statu)
        {
            string whereStr = "";

            whereStr = (statu == null ? "" : " AND Bloklar.blok_statu = @statu ") + (pozisyonId == null ? "" : " AND SiteHaritasi_Blok.bPoz_id = @pozId ");

            StringBuilder sb = new StringBuilder();

            sb.Append(@"SELECT Bloklar.*,SiteHaritasi_Blok.*
                        FROM Bloklar 
                        INNER JOIN (BlokPozisyonlari INNER JOIN SiteHaritasi_Blok ON BlokPozisyonlari.bPoz_id = SiteHaritasi_Blok.bPoz_id) ON Bloklar.blok_id = SiteHaritasi_Blok.blok_id
                        WHERE SiteHaritasi_Blok.site_id = @siteId " + whereStr + " ORDER BY BlokPozisyonlari.bPoz_adi, SiteHaritasi_Blok.sira ");

            MySqlDataAdapter adp = new MySqlDataAdapter(sb.ToString(), FxMySqlHelper.Connection());

            adp.SelectCommand.Parameters.AddWithValue("@siteId", sayfaId);

            if (statu != null)
            {
                adp.SelectCommand.Parameters.AddWithValue("@statu", statu);
            }

            if (pozisyonId != null)
            {
                adp.SelectCommand.Parameters.AddWithValue("@pozId", pozisyonId);
            }

            DataTable dt = new DataTable();

            adp.Fill(dt);

            List<enBlok> bloklar = new List<enBlok>();

            foreach (DataRow rw in dt.Rows)
            {
                enBlok blok = new enBlok();

                blok.Adi = rw["blok_adi"].ToString();
                blok.Icerik = rw["blok_icerik"].ToString();
                blok.Id = rw["Bloklar.blok_id"].xToIntDefault();
                blok.Sira = rw["sira"].xToIntDefault();
                blok.UscYolu = rw["blok_uscYol"].ToString();
                blok.BaslikKullanimi = rw["baslik"].xToBooleanDefault();
                blok.Height = rw["height"].ToString();
                blok.Statu = rw["blok_statu"].xToBooleanDefault();
                blok.CerceveKullanimi = rw["cerceve"].xToBooleanDefault();
                blok.Default = rw["blok_default"].xToBooleanDefault();
                blok.PozisyonId = rw["bPoz_id"].xToIntDefault();
                blok.SayfaId = rw["blok_sayfa_id"].xToInt();
                blok.Aciklama = rw["blok_aciklama"].ToString();

                blok.CarouselId = rw["blok_car_id"].xToInt();
                blok.CerceveRengi = rw["cerceve_rengi"].ToString();
                blok.ArkaplanRengi = rw["arkaplan_rengi"].ToString();
                blok.MetinRengi = rw["metin_rengi"].ToString();

                if (SessionManager.Admin != null && SessionManager.Admin.Finex)
                {
                    blok.Default = false;
                }

                bloklar.Add(blok);
            }

            return bloklar;
        }

        public List<enBlok> MasterGetir(bool? statu)
        {
            string whereStr = statu == null ? "" : " AND Bloklar.blok_statu = @statu ";

            StringBuilder sb = new StringBuilder();

            sb.Append(@"SELECT Bloklar.*,SiteHaritasi_Blok.*
                        FROM Bloklar 
                        INNER JOIN (BlokPozisyonlari INNER JOIN SiteHaritasi_Blok ON BlokPozisyonlari.bPoz_id = SiteHaritasi_Blok.bPoz_id) ON Bloklar.blok_id = SiteHaritasi_Blok.blok_id
                        WHERE BlokPozisyonlari.bPoz_master = @master " + whereStr + " ORDER BY SiteHaritasi_Blok.sira ");

            MySqlDataAdapter adp = new MySqlDataAdapter(sb.ToString(), FxMySqlHelper.Connection());

            adp.SelectCommand.Parameters.AddWithValue("@master", true);

            if (statu != null)
            {
                adp.SelectCommand.Parameters.AddWithValue("@statu", statu);
            }

            DataTable dt = new DataTable();

            adp.Fill(dt);

            List<enBlok> bloklar = new List<enBlok>();

            foreach (DataRow rw in dt.Rows)
            {
                enBlok blok = new enBlok();

                blok.Adi = rw["blok_adi"].ToString();
                blok.Icerik = rw["blok_icerik"].ToString();
                blok.Id = rw["Bloklar.blok_id"].xToIntDefault();
                blok.Sira = rw["sira"].xToIntDefault();
                blok.UscYolu = rw["blok_uscYol"].ToString();
                blok.BaslikKullanimi = rw["baslik"].xToBooleanDefault();
                blok.Height = rw["height"].ToString();
                blok.Statu = rw["blok_statu"].xToBooleanDefault();
                blok.CerceveKullanimi = rw["cerceve"].xToBooleanDefault();
                blok.Default = rw["blok_default"].xToBooleanDefault();
                blok.PozisyonId = rw["bPoz_id"].xToIntDefault();
                blok.SayfaId = rw["blok_sayfa_id"].xToInt();
                blok.Aciklama = rw["blok_aciklama"].ToString();

                blok.CarouselId = rw["blok_car_id"].xToInt();
                blok.CerceveRengi = rw["cerceve_rengi"].ToString();
                blok.ArkaplanRengi = rw["arkaplan_rengi"].ToString();
                blok.MetinRengi = rw["metin_rengi"].ToString();

                if (SessionManager.Admin != null && SessionManager.Admin.Finex)
                {
                    blok.Default = false;
                }

                bloklar.Add(blok);
            }

            return bloklar;
        }

        public bool SayfadaVarMi(enSiteHaritasi_Blok sBlok)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("select count(*) from SiteHaritasi_Blok where site_id = @sayfaId and blok_id = @blokId");

            MySqlCommand cmd = new MySqlCommand(sb.ToString(), FxMySqlHelper.Connection());

            cmd.Parameters.AddWithValue("@siteId", sBlok.SayfaId);
            cmd.Parameters.AddWithValue("@blokId", sBlok.BlokId);

            cmd.Connection.Open();

            int count = cmd.ExecuteScalar().xToIntDefault();

            cmd.Connection.Close();

            return count > 0;
        }

        public void Ekle(enSiteHaritasi_Blok blok)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            List<object> degerList = new List<object>();


            dict.Add("site_id",blok.SayfaId);
            dict.Add("blok_id",blok.BlokId);
            dict.Add("bPoz_id",blok.PozisyonId);
            dict.Add("statu",blok.Statu);
            dict.Add("sira",blok.Sira);
            dict.Add("height",blok.Height);
            dict.Add("baslik",blok.BaslikKullanimi);
            dict.Add("cerceve",blok.CerceveKullanimi);

            FxMySqlHelper.Insert("SiteHaritasi_Blok", dict);
        }

        public List<enBlok> SayfaEklenmemisleriGetir(int sayfaId, bool? statu)
        {
            string whereStr = statu == null ? "" : " AND Bloklar.blok_statu = @statu ";

            StringBuilder sb = new StringBuilder();

            sb.Append(@"SELECT Bloklar.*, SiteHaritasi_Blok.*
                              FROM Bloklar LEFT JOIN 
                                [SELECT SiteHaritasi_Blok.* FROM SiteHaritasi_Blok WHERE SiteHaritasi_Blok.site_id = @siteId ]. AS SiteHaritasi_Blok
                                  ON Bloklar.blok_id = SiteHaritasi_Blok.blok_id
                            WHERE SiteHaritasi_Blok.blok_id IS NULL" + whereStr);

            MySqlDataAdapter adp = new MySqlDataAdapter(sb.ToString(), FxMySqlHelper.Connection());

            adp.SelectCommand.Parameters.AddWithValue("@siteId", sayfaId);

            if (statu != null)
            {
                adp.SelectCommand.Parameters.AddWithValue("@statu", statu);
            }

            DataTable dt = new DataTable();

            adp.Fill(dt);

            List<enBlok> bloklar = new List<enBlok>();

            foreach (DataRow rw in dt.Rows)
            {
                enBlok blok = new enBlok();

                blok.Adi = rw["blok_adi"].ToString();
                blok.Icerik = rw["blok_icerik"].ToString();
                blok.Id = rw["Bloklar.blok_id"].xToIntDefault();
                blok.Sira = rw["sira"].xToIntDefault();
                blok.UscYolu = rw["blok_uscYol"].ToString();
                blok.BaslikKullanimi = rw["baslik"].xToBooleanDefault();
                blok.Height = rw["height"].ToString();
                blok.Statu = rw["blok_statu"].xToBooleanDefault();
                blok.CerceveKullanimi = rw["cerceve"].xToBooleanDefault();
                blok.Default = rw["blok_default"].xToBooleanDefault();
                blok.PozisyonId = rw["bPoz_id"].xToIntDefault();
                blok.SayfaId = rw["blok_sayfa_id"].xToInt();
                blok.Aciklama = rw["blok_aciklama"].ToString();

                blok.CarouselId = rw["blok_car_id"].xToInt();
                blok.CerceveRengi = rw["cerceve_rengi"].ToString();
                blok.ArkaplanRengi = rw["arkaplan_rengi"].ToString();
                blok.MetinRengi = rw["metin_rengi"].ToString();

                if (SessionManager.Admin != null && SessionManager.Admin.Finex)
                {
                    blok.Default = false;
                }

                bloklar.Add(blok);
            }

            return bloklar;
        }

        public void Kaldir(int pozisyonId, int blokId, int? sayfaId = null)
        {
            string wStr = sayfaId == null ? "" : " AND site_id = @siteId ";

            StringBuilder sb = new StringBuilder();

            sb.Append("DELETE FROM SiteHaritasi_Blok ");
            sb.Append("WHERE bPoz_id = @pozId AND blok_id = @blokId " + wStr);

            MySqlCommand cmd = new MySqlCommand(sb.ToString(), FxMySqlHelper.Connection());

            cmd.Parameters.AddWithValue("@pozId", pozisyonId);
            cmd.Parameters.AddWithValue("@blokId", blokId);

            if (sayfaId != null)
            {
                cmd.Parameters.AddWithValue("@siteId", sayfaId);
            }

            cmd.Connection.Open();

            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
        }

        public enSiteHaritasi_Blok Getir(int pozisyonId, int blokId, int? sayfaId = null)
        {
            string wStr = sayfaId == null ? "" : " AND site_id = @siteId ";

            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT * FROM SiteHaritasi_Blok WHERE bPoz_id = @pozId AND blok_id = @blokId" + wStr);

            MySqlDataAdapter adp = new MySqlDataAdapter(sb.ToString(), FxMySqlHelper.Connection());

            adp.SelectCommand.Parameters.AddWithValue("@pozId", pozisyonId);
            adp.SelectCommand.Parameters.AddWithValue("@blokId", blokId);

            if (sayfaId != null)
            {
                adp.SelectCommand.Parameters.AddWithValue("@sayfaId", sayfaId);
            }

            DataTable dt = new DataTable();

            adp.Fill(dt);

            enSiteHaritasi_Blok sbBlok = new enSiteHaritasi_Blok();

            if (dt.Rows.Count > 0)
            {
                DataRow rw = dt.Rows[0];

                sbBlok.BaslikKullanimi = rw["baslik"].xToBooleanDefault();
                sbBlok.CerceveKullanimi = rw["cerceve"].xToBooleanDefault();
                sbBlok.Height = rw["height"].ToString();
                sbBlok.PozisyonId = rw["bPoz_id"].xToIntDefault();
                sbBlok.SayfaId = rw["site_id"].xToIntDefault();
                sbBlok.Sira = rw["sira"].xToIntDefault();
                sbBlok.CerceveRengi = rw["cerceve_rengi"].ToString();
                sbBlok.ArkaplanRengi = rw["arkaplan_rengi"].ToString();
                sbBlok.MetinRengi = rw["metin_rengi"].ToString();
            }

            return sbBlok;
        }

        public enBlok PozisyonaGoreBlokGetir(int pozisyonId, int blokId)
        {
            //string wStr = sayfaId == null ? "" : " AND SiteHaritasi_Blok.site_id = @siteId ";

            StringBuilder sb = new StringBuilder();

            sb.Append(@"SELECT Bloklar.*,SiteHaritasi_Blok.*
                        FROM Bloklar 
                        INNER JOIN (BlokPozisyonlari INNER JOIN SiteHaritasi_Blok ON BlokPozisyonlari.bPoz_id = SiteHaritasi_Blok.bPoz_id) ON Bloklar.blok_id = SiteHaritasi_Blok.blok_id
                        WHERE SiteHaritasi_Blok.bPoz_id = @pozId AND SiteHaritasi_Blok.blok_id = @blokId ");

            MySqlDataAdapter adp = new MySqlDataAdapter(sb.ToString(), FxMySqlHelper.Connection());

            adp.SelectCommand.Parameters.AddWithValue("@pozId", pozisyonId);
            adp.SelectCommand.Parameters.AddWithValue("@blokId", blokId);

            //if (sayfaId != null)
            //{
            //    adp.SelectCommand.Parameters.AddWithValue("@sayfaId", sayfaId);
            //}

            DataTable dt = new DataTable();

            adp.Fill(dt);

            enBlok blok = new enBlok();

            if (dt.Rows.Count > 0)
            {
                DataRow rw = dt.Rows[0];

                blok.Adi = rw["blok_adi"].ToString();
                blok.Icerik = rw["blok_icerik"].ToString();
                blok.Id = rw["Bloklar.blok_id"].xToIntDefault();
                blok.Sira = rw["sira"].xToIntDefault();
                blok.UscYolu = rw["blok_uscYol"].ToString();
                blok.BaslikKullanimi = rw["baslik"].xToBooleanDefault();
                blok.Height = rw["height"].ToString();
                blok.Statu = rw["blok_statu"].xToBooleanDefault();
                blok.CerceveKullanimi = rw["cerceve"].xToBooleanDefault();
                blok.Default = rw["blok_default"].xToBooleanDefault();
                blok.PozisyonId = rw["bPoz_id"].xToIntDefault();
                blok.SayfaId = rw["blok_sayfa_id"].xToInt();
                blok.Aciklama = rw["blok_aciklama"].ToString();

                blok.CarouselId = rw["blok_car_id"].xToInt();
                blok.CerceveRengi = rw["cerceve_rengi"].ToString();
                blok.ArkaplanRengi = rw["arkaplan_rengi"].ToString();
                blok.MetinRengi = rw["metin_rengi"].ToString();

                if (SessionManager.Admin != null && SessionManager.Admin.Finex)
                {
                    blok.Default = false;
                }
            }

            return blok;
        }

        public void BaslikKullanimiGuncelle(enSiteHaritasi_Blok sBlok)
        {
            string strW = sBlok.SayfaId == 0 ? "" : " AND site_id = @siteId ";

            StringBuilder sb = new StringBuilder();

            sb.Append("UPDATE SiteHaritasi_Blok ");
            sb.Append("SET baslik = @baslik ");
            sb.Append("WHERE bPoz_id = @pozId AND blok_id = @blokId " + strW);

            MySqlCommand cmd = new MySqlCommand(sb.ToString(), FxMySqlHelper.Connection());

            cmd.Parameters.AddWithValue("@baslik", sBlok.BaslikKullanimi);
            cmd.Parameters.AddWithValue("@pozId", sBlok.PozisyonId);
            cmd.Parameters.AddWithValue("@blokId", sBlok.BlokId);

            if (sBlok.SayfaId != 0)
            {
                cmd.Parameters.AddWithValue("@siteId", sBlok.SayfaId);
            }

            cmd.Connection.Open();

            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
        }

        public void CerceveKullanimiGuncelle(enSiteHaritasi_Blok sBlok)
        {
            string strW = sBlok.SayfaId == 0 ? "" : " AND site_id = @siteId ";

            StringBuilder sb = new StringBuilder();

            sb.Append("UPDATE SiteHaritasi_Blok ");
            sb.Append("SET cerceve = @cerceve ");
            sb.Append("WHERE bPoz_id = @pozId AND blok_id = @blokId " + strW);

            MySqlCommand cmd = new MySqlCommand(sb.ToString(), FxMySqlHelper.Connection());

            cmd.Parameters.AddWithValue("@cerceve", sBlok.CerceveKullanimi);
            cmd.Parameters.AddWithValue("@pozId", sBlok.PozisyonId);
            cmd.Parameters.AddWithValue("@blokId", sBlok.BlokId);

            if (sBlok.SayfaId != 0)
            {
                cmd.Parameters.AddWithValue("@siteId", sBlok.SayfaId);
            }

            cmd.Connection.Open();

            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
        }

        public void SiraGuncelle(enSiteHaritasi_Blok sBlok)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("UPDATE SiteHaritasi_Blok ");
            sb.Append("SET sira = @sira ");
            sb.Append("WHERE site_id = @siteId AND blok_id = @blokId ");

            MySqlCommand cmd = new MySqlCommand(sb.ToString(), FxMySqlHelper.Connection());

            cmd.Parameters.AddWithValue("@sira", sBlok.Sira);
            cmd.Parameters.AddWithValue("@siteId", sBlok.SayfaId);
            cmd.Parameters.AddWithValue("@blokId", sBlok.BlokId);

            cmd.Connection.Open();

            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
        }

        public void PozisyonGuncelle(enSiteHaritasi_Blok sBlok)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("UPDATE SiteHaritasi_Blok ");
            sb.Append("SET bPoz_id = @pozId ");
            sb.Append("WHERE site_id = @siteId AND blok_id = @blokId ");

            MySqlCommand cmd = new MySqlCommand(sb.ToString(), FxMySqlHelper.Connection());

            cmd.Parameters.AddWithValue("@pozId", sBlok.PozisyonId);
            cmd.Parameters.AddWithValue("@siteId", sBlok.SayfaId);
            cmd.Parameters.AddWithValue("@blokId", sBlok.BlokId);

            cmd.Connection.Open();

            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
        }

        public bool PozisyondaVarMi(enSiteHaritasi_Blok sBlok)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("select count(*) from SiteHaritasi_Blok where site_id = @sayfaId and bPoz_id = @pozId and blok_id = @blokId");

            MySqlCommand cmd = new MySqlCommand(sb.ToString(), FxMySqlHelper.Connection());

            cmd.Parameters.AddWithValue("@sayfaId", sBlok.SayfaId);
            cmd.Parameters.AddWithValue("@pozId", sBlok.PozisyonId);
            cmd.Parameters.AddWithValue("@blokId", sBlok.BlokId);

            cmd.Connection.Open();

            int count = cmd.ExecuteScalar().xToIntDefault();

            cmd.Connection.Close();

            return count > 0;
        }

        public enBlok BlokGetir(int blokId, int sayfaId)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(@"SELECT Bloklar.*,SiteHaritasi_Blok.*
                        FROM Bloklar 
                        INNER JOIN (BlokPozisyonlari INNER JOIN SiteHaritasi_Blok ON BlokPozisyonlari.bPoz_id = SiteHaritasi_Blok.bPoz_id) ON Bloklar.blok_id = SiteHaritasi_Blok.blok_id
                        WHERE SiteHaritasi_Blok.site_id = @siteId AND SiteHaritasi_Blok.blok_id = @blokId ");

            MySqlDataAdapter adp = new MySqlDataAdapter(sb.ToString(), FxMySqlHelper.Connection());

            adp.SelectCommand.Parameters.AddWithValue("@siteId", sayfaId);
            adp.SelectCommand.Parameters.AddWithValue("@blokId", blokId);

            DataTable dt = new DataTable();

            adp.Fill(dt);

            enBlok blok = new enBlok();

            if (dt.Rows.Count > 0)
            {
                DataRow rw = dt.Rows[0];

                blok.Adi = rw["blok_adi"].ToString();
                blok.Icerik = rw["blok_icerik"].ToString();
                blok.Id = rw["Bloklar.blok_id"].xToIntDefault();
                blok.Sira = rw["sira"].xToIntDefault();
                blok.UscYolu = rw["blok_uscYol"].ToString();
                blok.BaslikKullanimi = rw["baslik"].xToBooleanDefault();
                blok.Height = rw["height"].ToString();
                blok.Statu = rw["blok_statu"].xToBooleanDefault();
                blok.CerceveKullanimi = rw["cerceve"].xToBooleanDefault();
                blok.Default = rw["blok_default"].xToBooleanDefault();
                blok.PozisyonId = rw["bPoz_id"].xToIntDefault();
                blok.SayfaId = rw["blok_sayfa_id"].xToInt();
                blok.Aciklama = rw["blok_aciklama"].ToString();

                blok.CarouselId = rw["blok_car_id"].xToInt();
                blok.CerceveRengi = rw["cerceve_rengi"].ToString();
                blok.ArkaplanRengi = rw["arkaplan_rengi"].ToString();
                blok.MetinRengi = rw["metin_rengi"].ToString();

                if (SessionManager.Admin != null && SessionManager.Admin.Finex)
                {
                    blok.Default = false;
                }
            }

            return blok;
        }

        public List<enBlok> PozisyonaGoreGetir(int pozId, bool? statu, int? sayfaId = null)
        {
            string whereStr = statu == null ? "" : " AND Bloklar.blok_statu = @statu ";

            whereStr += sayfaId == null ? "" : " AND SiteHaritasi_Blok.site_id = @sayfaId";


            StringBuilder sb = new StringBuilder();

            sb.Append(@"SELECT Bloklar.*,SiteHaritasi_Blok.*
                        FROM Bloklar 
                        INNER JOIN (BlokPozisyonlari INNER JOIN SiteHaritasi_Blok ON BlokPozisyonlari.bPoz_id = SiteHaritasi_Blok.bPoz_id) ON Bloklar.blok_id = SiteHaritasi_Blok.blok_id
                        WHERE SiteHaritasi_Blok.bPoz_id = @pozId " + whereStr + " ORDER BY SiteHaritasi_Blok.sira ");

            MySqlDataAdapter adp = new MySqlDataAdapter(sb.ToString(), FxMySqlHelper.Connection());

            adp.SelectCommand.Parameters.AddWithValue("@pozId", pozId);

            if (statu != null)
            {
                adp.SelectCommand.Parameters.AddWithValue("@statu", statu);
            }

            if (sayfaId != null)
            {
                adp.SelectCommand.Parameters.AddWithValue("@sayfaId", sayfaId);
            }

            DataTable dt = new DataTable();

            adp.Fill(dt);

            List<enBlok> bloklar = new List<enBlok>();

            foreach (DataRow rw in dt.Rows)
            {
                enBlok blok = new enBlok();

                blok.Adi = rw["blok_adi"].ToString();
                blok.Icerik = rw["blok_icerik"].ToString();
                blok.Id = rw["Bloklar.blok_id"].xToIntDefault();
                blok.Sira = rw["sira"].xToIntDefault();
                blok.UscYolu = rw["blok_uscYol"].ToString();
                blok.BaslikKullanimi = rw["baslik"].xToBooleanDefault();
                blok.Height = rw["height"].ToString();
                blok.Statu = rw["blok_statu"].xToBooleanDefault();
                blok.CerceveKullanimi = rw["cerceve"].xToBooleanDefault();
                blok.Default = rw["blok_default"].xToBooleanDefault();
                blok.PozisyonId = rw["bPoz_id"].xToIntDefault();
                blok.SayfaId = rw["blok_sayfa_id"].xToInt();
                blok.Aciklama = rw["blok_aciklama"].ToString();

                blok.CarouselId = rw["blok_car_id"].xToInt();
                blok.CerceveRengi = rw["cerceve_rengi"].ToString();
                blok.ArkaplanRengi = rw["arkaplan_rengi"].ToString();
                blok.MetinRengi = rw["metin_rengi"].ToString();

                if (SessionManager.Admin != null && SessionManager.Admin.Finex)
                {
                    blok.Default = false;
                }

                bloklar.Add(blok);
            }

            return bloklar;
        }
    }
}
