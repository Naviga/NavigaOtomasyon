using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entity;
using System.Data;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class dalSiteHaritasi
    {
        public DataTable AktifUstSayfalariGetir()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT * FROM SiteHaritasi WHERE site_parent is NULL AND site_statu = @statu ORDER BY site_sira");

            MySqlDataAdapter adp = new MySqlDataAdapter(sb.ToString(), FxMySqlHelper.Connection());

            adp.SelectCommand.Parameters.AddWithValue("@statu", true);

            DataTable dt = new DataTable();

            adp.Fill(dt);

            return dt;
        }

        public DataTable AktifAltSayfalariGetir(int parentId)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT * FROM SiteHaritasi WHERE site_parent = @parentId AND site_statu = @statu ORDER BY site_sira");

            MySqlDataAdapter adp = new MySqlDataAdapter(sb.ToString(), FxMySqlHelper.Connection());

            adp.SelectCommand.Parameters.AddWithValue("@parentId", parentId);
            adp.SelectCommand.Parameters.AddWithValue("@statu", true);

            DataTable dt = new DataTable();

            adp.Fill(dt);

            return dt;
        }

        public List<enSiteHaritasi> AktifUstSayfalariGetirList()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT * FROM SiteHaritasi WHERE site_parent is NULL AND site_statu = @statu ORDER BY site_sira");

            MySqlDataAdapter adp = new MySqlDataAdapter(sb.ToString(), FxMySqlHelper.Connection());

            adp.SelectCommand.Parameters.AddWithValue("@statu", true);

            DataTable dt = new DataTable();

            adp.Fill(dt);

            List<enSiteHaritasi> sayfalar = new List<enSiteHaritasi>();

            foreach (DataRow rw in dt.Rows)
            {
                enSiteHaritasi sayfa = new enSiteHaritasi();

                sayfa.Adi = rw["site_adi"].ToString();
                sayfa.DefaultSayfa = rw["site_default"].xToBooleanDefault();
                sayfa.Description = rw["site_description"].ToString();
                sayfa.Icerik = rw["site_icerik"].ToString();
                sayfa.Id = rw["site_id"].xToIntDefault();
                sayfa.Keywords = rw["site_keywords"].ToString();
                sayfa.Parent = rw["site_parent"].xToInt();
                sayfa.Resim = rw["site_resim"].ToString();
                sayfa.Statu = rw["site_statu"].xToBooleanDefault();
                sayfa.Title = rw["site_title"].ToString();
                sayfa.Url = rw["site_url"].ToString();
                sayfa.Url = UrlOlustur(sayfa).ToLowerInvariant();
                sayfa.Sira = rw["site_sira"].xToIntDefault();
                sayfa.AcilirMenu = rw["site_acilirMenu"].xToBooleanDefault();
                sayfa.FizikselUrl = rw["site_fizikselUrl"].ToString();
                sayfa.Dinamik = rw["site_dinamik"].xToBooleanDefault();
                sayfa.FotoBaslik = rw["site_fotoBaslik"].ToString();
                sayfa.VideoBaslik = rw["site_videoBaslik"].ToString();
                sayfa.FotoGaleriMi = rw["site_fotoGaleri"].xToBooleanDefault();
                sayfa.FaceComments = rw["site_faceComments"].xToBooleanDefault();
                sayfa.Custom = rw["site_custom"].xToBooleanDefault();
                sayfa.PaylasimAlani = rw["site_paylasimAlani"].xToBooleanDefault();
                sayfa.BaslikAlani = rw["site_baslikAlani"].xToBooleanDefault();
                sayfa.AnaSayfa = rw["site_baslangic"].xToBooleanDefault();
                sayfa.SayfaYolu = rw["site_sayfaYolu"].xToBooleanDefault();
                sayfa.Menu = rw["site_menu"].xToBooleanDefault();
                sayfa.YanMenu = rw["site_yanMenu"].xToBooleanDefault();
                sayfa.Footer = rw["site_footer"].xToBooleanDefault();
                sayfa.SayfaMenu = rw["site_sayfaMenu"].xToBooleanDefault();
                sayfa.KayitTarihi = rw["site_kayitTar"].xToDateTimeDefault();
                sayfa.List = rw["site_list"].xToBooleanDefault();
                sayfa.UrunMu = rw["site_urunMu"].xToBooleanDefault();

                sayfalar.Add(sayfa);
            }

            return sayfalar;
        }

        public List<enSiteHaritasi> TumSayfalariGetir()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT * FROM SiteHaritasi ORDER BY site_sira");

            MySqlDataAdapter adp = new MySqlDataAdapter(sb.ToString(), FxMySqlHelper.Connection());

            DataTable dt = new DataTable();

            adp.Fill(dt);

            List<enSiteHaritasi> sayfalar = new List<enSiteHaritasi>();

            foreach (DataRow rw in dt.Rows)
            {
                enSiteHaritasi sayfa = new enSiteHaritasi();

                sayfa.Adi = rw["site_adi"].ToString();
                sayfa.DefaultSayfa = rw["site_default"].xToBooleanDefault();
                sayfa.Description = rw["site_description"].ToString();
                sayfa.Icerik = rw["site_icerik"].ToString();
                sayfa.Id = rw["site_id"].xToIntDefault();
                sayfa.Keywords = rw["site_keywords"].ToString();
                sayfa.Parent = rw["site_parent"].xToInt();
                sayfa.Resim = rw["site_resim"].ToString();
                sayfa.Statu = rw["site_statu"].xToBooleanDefault();
                sayfa.Title = rw["site_title"].ToString();
                sayfa.Url = rw["site_url"].ToString();
                sayfa.Url = UrlOlustur(sayfa).ToLowerInvariant();
                sayfa.Sira = rw["site_sira"].xToIntDefault();
                sayfa.Dinamik = rw["site_dinamik"].xToBooleanDefault();
                sayfa.FizikselUrl = rw["site_fizikselUrl"].ToString();
                sayfa.AcilirMenu = rw["site_acilirMenu"].xToBooleanDefault();
                sayfa.FotoBaslik = rw["site_fotoBaslik"].ToString();
                sayfa.VideoBaslik = rw["site_videoBaslik"].ToString();
                sayfa.FotoGaleriMi = rw["site_fotoGaleri"].xToBooleanDefault();
                sayfa.FaceComments = rw["site_faceComments"].xToBooleanDefault();
                sayfa.Custom = rw["site_custom"].xToBooleanDefault();
                sayfa.PaylasimAlani = rw["site_paylasimAlani"].xToBooleanDefault();
                sayfa.BaslikAlani = rw["site_baslikAlani"].xToBooleanDefault();
                sayfa.AnaSayfa = rw["site_baslangic"].xToBooleanDefault();
                sayfa.SayfaYolu = rw["site_sayfaYolu"].xToBooleanDefault();
                sayfa.Menu = rw["site_menu"].xToBooleanDefault();
                sayfa.YanMenu = rw["site_yanMenu"].xToBooleanDefault();
                sayfa.Footer = rw["site_footer"].xToBooleanDefault();
                sayfa.SayfaMenu = rw["site_sayfaMenu"].xToBooleanDefault();
                sayfa.KayitTarihi = rw["site_kayitTar"].xToDateTimeDefault();
                sayfa.List = rw["site_list"].xToBooleanDefault();
                sayfa.UrunMu = rw["site_urunMu"].xToBooleanDefault();

                sayfalar.Add(sayfa);
            }

            return sayfalar;
        }

        public List<enSiteHaritasi> AktifTumSayfalariGetir(bool? custom)
        {
            string whereStr = custom != null ? " AND site_cutom = @custom " : "";

            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT * FROM SiteHaritasi WHERE site_statu = @statu " + whereStr + " ORDER BY site_sira");

            MySqlDataAdapter adp = new MySqlDataAdapter(sb.ToString(), FxMySqlHelper.Connection());

            adp.SelectCommand.Parameters.AddWithValue("@statu", true);

            DataTable dt = new DataTable();

            adp.Fill(dt);

            List<enSiteHaritasi> sayfalar = new List<enSiteHaritasi>();

            foreach (DataRow rw in dt.Rows)
            {
                enSiteHaritasi sayfa = new enSiteHaritasi();

                sayfa.Adi = rw["site_adi"].ToString();
                sayfa.DefaultSayfa = rw["site_default"].xToBooleanDefault();
                sayfa.Description = rw["site_description"].ToString();
                sayfa.Icerik = rw["site_icerik"].ToString();
                sayfa.Id = rw["site_id"].xToIntDefault();
                sayfa.Keywords = rw["site_keywords"].ToString();
                sayfa.Parent = rw["site_parent"].xToInt();
                sayfa.Resim = rw["site_resim"].ToString();
                sayfa.Statu = rw["site_statu"].xToBooleanDefault();
                sayfa.Title = rw["site_title"].ToString();
                sayfa.Url = rw["site_url"].ToString();
                sayfa.Url = UrlOlustur(sayfa).ToLowerInvariant();
                sayfa.Sira = rw["site_sira"].xToIntDefault();
                sayfa.Dinamik = rw["site_dinamik"].xToBooleanDefault();
                sayfa.FizikselUrl = rw["site_fizikselUrl"].ToString();
                sayfa.AcilirMenu = rw["site_acilirMenu"].xToBooleanDefault();
                sayfa.FotoBaslik = rw["site_fotoBaslik"].ToString();
                sayfa.VideoBaslik = rw["site_videoBaslik"].ToString();
                sayfa.FotoGaleriMi = rw["site_fotoGaleri"].xToBooleanDefault();
                sayfa.FaceComments = rw["site_faceComments"].xToBooleanDefault();
                sayfa.Custom = rw["site_custom"].xToBooleanDefault();
                sayfa.PaylasimAlani = rw["site_paylasimAlani"].xToBooleanDefault();
                sayfa.BaslikAlani = rw["site_baslikAlani"].xToBooleanDefault();
                sayfa.AnaSayfa = rw["site_baslangic"].xToBooleanDefault();
                sayfa.SayfaYolu = rw["site_sayfaYolu"].xToBooleanDefault();
                sayfa.Menu = rw["site_menu"].xToBooleanDefault();
                sayfa.YanMenu = rw["site_yanMenu"].xToBooleanDefault();
                sayfa.Footer = rw["site_footer"].xToBooleanDefault();
                sayfa.SayfaMenu = rw["site_sayfaMenu"].xToBooleanDefault();
                sayfa.KayitTarihi = rw["site_kayitTar"].xToDateTimeDefault();
                sayfa.List = rw["site_list"].xToBooleanDefault();
                sayfa.UrunMu = rw["site_urunMu"].xToBooleanDefault();

                sayfalar.Add(sayfa);
            }

            return sayfalar;
        }

        public List<enSiteHaritasi> UstSayfalariGetirList()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT * FROM SiteHaritasi WHERE site_parent is NULL ORDER BY site_sira");

            MySqlDataAdapter adp = new MySqlDataAdapter(sb.ToString(), FxMySqlHelper.Connection());

            DataTable dt = new DataTable();

            adp.Fill(dt);

            List<enSiteHaritasi> sayfalar = new List<enSiteHaritasi>();

            foreach (DataRow rw in dt.Rows)
            {
                enSiteHaritasi sayfa = new enSiteHaritasi();

                sayfa.Adi = rw["site_adi"].ToString();
                sayfa.DefaultSayfa = rw["site_default"].xToBooleanDefault();
                sayfa.Description = rw["site_description"].ToString();
                sayfa.Icerik = rw["site_icerik"].ToString();
                sayfa.Id = rw["site_id"].xToIntDefault();
                sayfa.Keywords = rw["site_keywords"].ToString();
                sayfa.Parent = rw["site_parent"].xToInt();
                sayfa.Resim = rw["site_resim"].ToString();
                sayfa.Statu = rw["site_statu"].xToBooleanDefault();
                sayfa.Title = rw["site_title"].ToString();
                sayfa.Url = rw["site_url"].ToString();
                sayfa.Url = UrlOlustur(sayfa).ToLowerInvariant();
                sayfa.Sira = rw["site_sira"].xToIntDefault();
                sayfa.AcilirMenu = rw["site_acilirMenu"].xToBooleanDefault();
                sayfa.FizikselUrl = rw["site_fizikselUrl"].ToString();
                sayfa.Dinamik = rw["site_dinamik"].xToBooleanDefault();
                sayfa.FotoBaslik = rw["site_fotoBaslik"].ToString();
                sayfa.VideoBaslik = rw["site_videoBaslik"].ToString();
                sayfa.FotoGaleriMi = rw["site_fotoGaleri"].xToBooleanDefault();
                sayfa.FaceComments = rw["site_faceComments"].xToBooleanDefault();
                sayfa.Custom = rw["site_custom"].xToBooleanDefault();
                sayfa.PaylasimAlani = rw["site_paylasimAlani"].xToBooleanDefault();
                sayfa.BaslikAlani = rw["site_baslikAlani"].xToBooleanDefault();
                sayfa.AnaSayfa = rw["site_baslangic"].xToBooleanDefault();
                sayfa.SayfaYolu = rw["site_sayfaYolu"].xToBooleanDefault();
                sayfa.Menu = rw["site_menu"].xToBooleanDefault();
                sayfa.YanMenu = rw["site_yanMenu"].xToBooleanDefault();
                sayfa.Footer = rw["site_footer"].xToBooleanDefault();
                sayfa.SayfaMenu = rw["site_sayfaMenu"].xToBooleanDefault();
                sayfa.KayitTarihi = rw["site_kayitTar"].xToDateTimeDefault();
                sayfa.List = rw["site_list"].xToBooleanDefault();
                sayfa.UrunMu = rw["site_urunMu"].xToBooleanDefault();

                sayfalar.Add(sayfa);
            }

            return sayfalar;
        }

        public List<enSiteHaritasi> AktifAltSayfalariGetirList(int parentId)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT * FROM SiteHaritasi WHERE site_parent = @parentId AND site_statu = @statu ORDER BY site_sira");

            MySqlDataAdapter adp = new MySqlDataAdapter(sb.ToString(), FxMySqlHelper.Connection());

            adp.SelectCommand.Parameters.AddWithValue("@parentId", parentId);
            adp.SelectCommand.Parameters.AddWithValue("@statu", true);

            DataTable dt = new DataTable();

            adp.Fill(dt);

            List<enSiteHaritasi> sayfalar = new List<enSiteHaritasi>();

            foreach (DataRow rw in dt.Rows)
            {
                enSiteHaritasi sayfa = new enSiteHaritasi();

                sayfa.Adi = rw["site_adi"].ToString();
                sayfa.DefaultSayfa = rw["site_default"].xToBooleanDefault();
                sayfa.Description = rw["site_description"].ToString();
                sayfa.Icerik = rw["site_icerik"].ToString();
                sayfa.Id = rw["site_id"].xToIntDefault();
                sayfa.Keywords = rw["site_keywords"].ToString();
                sayfa.Parent = rw["site_parent"].xToIntDefault();
                sayfa.Statu = rw["site_statu"].xToBooleanDefault();
                sayfa.Title = rw["site_title"].ToString();
                sayfa.Url = rw["site_url"].ToString();
                sayfa.Url = UrlOlustur(sayfa).ToLowerInvariant();
                sayfa.Sira = rw["site_sira"].xToIntDefault();
                sayfa.AcilirMenu = rw["site_acilirMenu"].xToBooleanDefault();
                sayfa.FizikselUrl = rw["site_fizikselUrl"].ToString();
                sayfa.Dinamik = rw["site_dinamik"].xToBooleanDefault();
                sayfa.FotoBaslik = rw["site_fotoBaslik"].ToString();
                sayfa.VideoBaslik = rw["site_videoBaslik"].ToString();
                sayfa.FotoGaleriMi = rw["site_fotoGaleri"].xToBooleanDefault();
                sayfa.FaceComments = rw["site_faceComments"].xToBooleanDefault();
                sayfa.Custom = rw["site_custom"].xToBooleanDefault();
                sayfa.PaylasimAlani = rw["site_paylasimAlani"].xToBooleanDefault();
                sayfa.BaslikAlani = rw["site_baslikAlani"].xToBooleanDefault();
                sayfa.AnaSayfa = rw["site_baslangic"].xToBooleanDefault();
                sayfa.SayfaYolu = rw["site_sayfaYolu"].xToBooleanDefault();
                sayfa.Menu = rw["site_menu"].xToBooleanDefault();
                sayfa.YanMenu = rw["site_yanMenu"].xToBooleanDefault();
                sayfa.Footer = rw["site_footer"].xToBooleanDefault();
                sayfa.SayfaMenu = rw["site_sayfaMenu"].xToBooleanDefault();
                sayfa.KayitTarihi = rw["site_kayitTar"].xToDateTimeDefault();
                sayfa.List = rw["site_list"].xToBooleanDefault();
                sayfa.Resim = rw["site_resim"].ToString();
                sayfa.UrunMu = rw["site_urunMu"].xToBooleanDefault();

                sayfalar.Add(sayfa);
            }

            return sayfalar;
        }

        public List<enSiteHaritasi> AktifAltSayfalariGetirList_SinirliSayida(int parentId, int kayitSayisi)
        {
            StringBuilder sb = new StringBuilder();

            string topStr = " LIMIT " + kayitSayisi + " ";

            sb.Append("SELECT * FROM SiteHaritasi WHERE site_parent = @parentId AND site_statu = @statu ORDER BY site_sira " + topStr + "");

            MySqlDataAdapter adp = new MySqlDataAdapter(sb.ToString(), FxMySqlHelper.Connection());

            adp.SelectCommand.Parameters.AddWithValue("@parentId", parentId);
            adp.SelectCommand.Parameters.AddWithValue("@statu", true);

            DataTable dt = new DataTable();

            adp.Fill(dt);

            List<enSiteHaritasi> sayfalar = new List<enSiteHaritasi>();

            foreach (DataRow rw in dt.Rows)
            {
                enSiteHaritasi sayfa = new enSiteHaritasi();

                sayfa.Adi = rw["site_adi"].ToString();
                sayfa.DefaultSayfa = rw["site_default"].xToBooleanDefault();
                sayfa.Description = rw["site_description"].ToString();
                sayfa.Icerik = rw["site_icerik"].ToString();
                sayfa.Id = rw["site_id"].xToIntDefault();
                sayfa.Keywords = rw["site_keywords"].ToString();
                sayfa.Parent = rw["site_parent"].xToIntDefault();
                sayfa.Statu = rw["site_statu"].xToBooleanDefault();
                sayfa.Title = rw["site_title"].ToString();
                sayfa.Url = rw["site_url"].ToString();
                sayfa.Url = UrlOlustur(sayfa).ToLowerInvariant();
                sayfa.Sira = rw["site_sira"].xToIntDefault();
                sayfa.AcilirMenu = rw["site_acilirMenu"].xToBooleanDefault();
                sayfa.FizikselUrl = rw["site_fizikselUrl"].ToString();
                sayfa.Dinamik = rw["site_dinamik"].xToBooleanDefault();
                sayfa.FotoBaslik = rw["site_fotoBaslik"].ToString();
                sayfa.VideoBaslik = rw["site_videoBaslik"].ToString();
                sayfa.FotoGaleriMi = rw["site_fotoGaleri"].xToBooleanDefault();
                sayfa.FaceComments = rw["site_faceComments"].xToBooleanDefault();
                sayfa.Custom = rw["site_custom"].xToBooleanDefault();
                sayfa.PaylasimAlani = rw["site_paylasimAlani"].xToBooleanDefault();
                sayfa.BaslikAlani = rw["site_baslikAlani"].xToBooleanDefault();
                sayfa.AnaSayfa = rw["site_baslangic"].xToBooleanDefault();
                sayfa.SayfaYolu = rw["site_sayfaYolu"].xToBooleanDefault();
                sayfa.Menu = rw["site_menu"].xToBooleanDefault();
                sayfa.YanMenu = rw["site_yanMenu"].xToBooleanDefault();
                sayfa.Footer = rw["site_footer"].xToBooleanDefault();
                sayfa.SayfaMenu = rw["site_sayfaMenu"].xToBooleanDefault();
                sayfa.KayitTarihi = rw["site_kayitTar"].xToDateTimeDefault();
                sayfa.List = rw["site_list"].xToBooleanDefault();
                sayfa.Resim = rw["site_resim"].ToString();
                sayfa.UrunMu = rw["site_urunMu"].xToBooleanDefault();

                sayfalar.Add(sayfa);
            }

            return sayfalar;
        }

        public List<enSiteHaritasi> AltSayfalariGetirList(int parentId)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT * FROM SiteHaritasi WHERE site_parent = @parentId ORDER BY site_sira");

            MySqlDataAdapter adp = new MySqlDataAdapter(sb.ToString(), FxMySqlHelper.Connection());

            adp.SelectCommand.Parameters.AddWithValue("@parentId", parentId);

            DataTable dt = new DataTable();

            adp.Fill(dt);

            List<enSiteHaritasi> sayfalar = new List<enSiteHaritasi>();

            foreach (DataRow rw in dt.Rows)
            {
                enSiteHaritasi sayfa = new enSiteHaritasi();

                sayfa.Adi = rw["site_adi"].ToString();
                sayfa.DefaultSayfa = rw["site_default"].xToBooleanDefault();
                sayfa.Description = rw["site_description"].ToString();
                sayfa.Icerik = rw["site_icerik"].ToString();
                sayfa.Id = rw["site_id"].xToIntDefault();
                sayfa.Keywords = rw["site_keywords"].ToString();
                sayfa.Parent = rw["site_parent"].xToInt();
                sayfa.Resim = rw["site_resim"].ToString();
                sayfa.Statu = rw["site_statu"].xToBooleanDefault();
                sayfa.Title = rw["site_title"].ToString();
                sayfa.Url = rw["site_url"].ToString();
                sayfa.Url = UrlOlustur(sayfa).ToLowerInvariant();
                sayfa.Sira = rw["site_sira"].xToIntDefault();
                sayfa.AcilirMenu = rw["site_acilirMenu"].xToBooleanDefault();
                sayfa.FizikselUrl = rw["site_fizikselUrl"].ToString();
                sayfa.Dinamik = rw["site_dinamik"].xToBooleanDefault();
                sayfa.FotoBaslik = rw["site_fotoBaslik"].ToString();
                sayfa.VideoBaslik = rw["site_videoBaslik"].ToString();
                sayfa.FotoGaleriMi = rw["site_fotoGaleri"].xToBooleanDefault();
                sayfa.FaceComments = rw["site_faceComments"].xToBooleanDefault();
                sayfa.Custom = rw["site_custom"].xToBooleanDefault();
                sayfa.PaylasimAlani = rw["site_paylasimAlani"].xToBooleanDefault();
                sayfa.BaslikAlani = rw["site_baslikAlani"].xToBooleanDefault();
                sayfa.AnaSayfa = rw["site_baslangic"].xToBooleanDefault();
                sayfa.SayfaYolu = rw["site_sayfaYolu"].xToBooleanDefault();
                sayfa.Menu = rw["site_menu"].xToBooleanDefault();
                sayfa.YanMenu = rw["site_yanMenu"].xToBooleanDefault();
                sayfa.Footer = rw["site_footer"].xToBooleanDefault();
                sayfa.SayfaMenu = rw["site_sayfaMenu"].xToBooleanDefault();
                sayfa.KayitTarihi = rw["site_kayitTar"].xToDateTimeDefault();
                sayfa.List = rw["site_list"].xToBooleanDefault();
                sayfa.UrunMu = rw["site_urunMu"].xToBooleanDefault();

                sayfalar.Add(sayfa);
            }

            return sayfalar;
        }

        public enSiteHaritasi SayfaGetir(int sayfaId)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT * FROM SiteHaritasi WHERE site_id = @id");

            MySqlDataAdapter adp = new MySqlDataAdapter(sb.ToString(), FxMySqlHelper.Connection());

            adp.SelectCommand.Parameters.AddWithValue("@id", sayfaId);

            DataTable dt = new DataTable();

            adp.Fill(dt);

            enSiteHaritasi sayfa = new enSiteHaritasi();

            if (dt.Rows.Count > 0)
            {
                DataRow rw = dt.Rows[0];

                sayfa.Adi = rw["site_adi"].ToString();
                sayfa.DefaultSayfa = rw["site_default"].xToBooleanDefault();
                sayfa.Description = rw["site_description"].ToString();
                sayfa.Icerik = rw["site_icerik"].ToString();
                sayfa.Id = rw["site_id"].xToIntDefault();
                sayfa.Keywords = rw["site_keywords"].ToString();
                sayfa.Parent = rw["site_parent"].xToInt();
                sayfa.Resim = rw["site_resim"].ToString();
                sayfa.Statu = rw["site_statu"].xToBooleanDefault();
                sayfa.Title = rw["site_title"].ToString();
                sayfa.Url = rw["site_url"].ToString();
                sayfa.DisplayUrl = UrlOlustur(sayfa);
                sayfa.Sira = rw["site_sira"].xToIntDefault();
                sayfa.AcilirMenu = rw["site_acilirMenu"].xToBooleanDefault();
                sayfa.FizikselUrl = rw["site_fizikselUrl"].ToString();
                sayfa.Dinamik = rw["site_dinamik"].xToBooleanDefault();
                sayfa.FotoBaslik = rw["site_fotoBaslik"].ToString();
                sayfa.VideoBaslik = rw["site_videoBaslik"].ToString();
                sayfa.FotoGaleriMi = rw["site_fotoGaleri"].xToBooleanDefault();
                sayfa.FaceComments = rw["site_faceComments"].xToBooleanDefault();
                sayfa.Custom = rw["site_custom"].xToBooleanDefault();
                sayfa.PaylasimAlani = rw["site_paylasimAlani"].xToBooleanDefault();
                sayfa.BaslikAlani = rw["site_baslikAlani"].xToBooleanDefault();
                sayfa.AnaSayfa = rw["site_baslangic"].xToBooleanDefault();
                sayfa.SayfaYolu = rw["site_sayfaYolu"].xToBooleanDefault();
                sayfa.Menu = rw["site_menu"].xToBooleanDefault();
                sayfa.YanMenu = rw["site_yanMenu"].xToBooleanDefault();
                sayfa.Footer = rw["site_footer"].xToBooleanDefault();
                sayfa.SayfaMenu = rw["site_sayfaMenu"].xToBooleanDefault();
                sayfa.KayitTarihi = rw["site_kayitTar"].xToDateTimeDefault();
                sayfa.List = rw["site_list"].xToBooleanDefault();
                sayfa.CarouselId = rw["site_car_id"].xToInt();
                sayfa.UrunMu = rw["site_urunMu"].xToBooleanDefault();
            }

            return sayfa;
        }

        public void YeniSayfaEkle(enSiteHaritasi site)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            List<object> degerList = new List<object>();

            dict.Add("site_adi", site.Adi);
            dict.Add("site_resim", site.Resim);
            dict.Add("site_statu", site.Statu);
            dict.Add("site_default", site.DefaultSayfa);
            dict.Add("site_icerik", site.Icerik);
            dict.Add("site_title", site.Title);
            dict.Add("site_description", site.Description);
            dict.Add("site_keywords", site.Keywords);
            dict.Add("site_parent", site.Parent);
            dict.Add("site_url", site.Url.Trim().ToLowerInvariant());
            dict.Add("site_sira", site.Sira);
            dict.Add("site_fotoBaslik", site.FotoBaslik);
            dict.Add("site_videoBaslik", site.VideoBaslik);
            dict.Add("site_fotoGaleri", site.FotoGaleriMi);
            dict.Add("site_faceComments", site.FaceComments);
            dict.Add("site_custom", site.Custom);
            dict.Add("site_paylasimAlani", site.PaylasimAlani);
            dict.Add("site_baslikAlani", site.BaslikAlani);
            dict.Add("site_sayfaYolu", site.SayfaYolu);
            dict.Add("site_menu", site.Menu);
            dict.Add("site_sayfaMenu", site.SayfaMenu);
            dict.Add("site_kayitTar", DateTime.Now.Date);
            dict.Add("site_list", site.List);
            dict.Add("site_urunMu", site.UrunMu);

            site.Id = FxMySqlHelper.Insert("SiteHaritasi", dict, true);
        }

        public void SayfaDuzenle(enSiteHaritasi site)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            List<object> degerList = new List<object>();


            dict.Add("site_adi", site.Adi);

            if (site.Resim.xBosMu() == false)
            {
                dict.Add("site_resim", site.Resim);
            }
            dict.Add("site_icerik", site.Icerik);
            dict.Add("site_title", site.Title);
            dict.Add("site_description", site.Description);
            dict.Add("site_keywords", site.Keywords);
            dict.Add("site_parent", site.Parent);
            dict.Add("site_url", site.Url.Trim().ToLowerInvariant());
            dict.Add("site_fotoBaslik", site.FotoBaslik);
            dict.Add("site_videoBaslik", site.VideoBaslik);
            dict.Add("site_fotoGaleri", site.FotoGaleriMi);
            dict.Add("site_faceComments", site.FaceComments);
            dict.Add("site_custom", site.Custom);
            dict.Add("site_paylasimAlani", site.PaylasimAlani);
            dict.Add("site_baslikAlani", site.BaslikAlani);
            dict.Add("site_sayfaYolu", site.SayfaYolu);
            dict.Add("site_sayfaMenu", site.SayfaMenu);
            dict.Add("site_list", site.List);

            FxMySqlHelper.Update("SiteHaritasi", dict, "site_id", site.Id);
        }

        public void StatuDegistir(enSiteHaritasi site)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            List<object> degerList = new List<object>();

            dict.Add("site_statu", site.Statu);

            FxMySqlHelper.Update("SiteHaritasi", dict, "site_id", site.Id);
        }

        public void AcilirMenuDurumDegistir(enSiteHaritasi site)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            List<object> degerList = new List<object>();

            dict.Add("site_acilirMenu", site.AcilirMenu);

            FxMySqlHelper.Update("SiteHaritasi", dict, "site_id", site.Id);
        }

        public void FotoGaleriDurumDegistir(enSiteHaritasi site)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            List<object> degerList = new List<object>();

            dict.Add("site_fotoGaleri", site.FotoGaleriMi);

            FxMySqlHelper.Update("SiteHaritasi", dict, "site_id", site.Id);
        }

        public void FaceCommentsDurumDegistir(enSiteHaritasi site)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            List<object> degerList = new List<object>();

            dict.Add("site_faceComments", site.FaceComments);

            FxMySqlHelper.Update("SiteHaritasi", dict, "site_id", site.Id);
        }

        public void SayfaSil(int sayfaId)
        {
            FxMySqlHelper.Delete("SiteHaritasi", "site_id", sayfaId);
        }

        public enSiteHaritasi BirUsttekiSayfayiGetir(enSiteHaritasi gSayfa)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(@"SELECT * FROM SiteHaritasi 
                        WHERE site_sira < @sira ORDER BY site_sira DESC LIMIT 1");

            MySqlDataAdapter adp = new MySqlDataAdapter(sb.ToString(), FxMySqlHelper.Connection());

            adp.SelectCommand.Parameters.AddWithValue("@sira", gSayfa.Sira);

            DataTable dt = new DataTable();

            adp.Fill(dt);

            enSiteHaritasi sayfa = new enSiteHaritasi();

            if (dt.Rows.Count > 0)
            {
                DataRow rw = dt.Rows[0];

                sayfa.Adi = rw["site_adi"].ToString();
                sayfa.DefaultSayfa = rw["site_default"].xToBooleanDefault();
                sayfa.Description = rw["site_description"].ToString();
                sayfa.Icerik = rw["site_icerik"].ToString();
                sayfa.Id = rw["site_id"].xToIntDefault();
                sayfa.Keywords = rw["site_keywords"].ToString();
                sayfa.Parent = rw["site_parent"].xToInt();
                sayfa.Resim = rw["site_resim"].ToString();

                if (sayfa.Resim.xBosMu())
                {
                    sayfa.Resim = "~/css/img/siyahBlokIcon.png";
                }

                sayfa.Statu = rw["site_statu"].xToBooleanDefault();
                sayfa.Title = rw["site_title"].ToString();
                sayfa.Url = rw["site_url"].ToString();
                sayfa.Url = UrlOlustur(sayfa).ToLowerInvariant();
                sayfa.Sira = rw["site_sira"].xToIntDefault();
                sayfa.UrunMu = rw["site_urunMu"].xToBooleanDefault();
            }

            return sayfa;
        }

        public enSiteHaritasi BirAlttakiSayfayiGetir(enSiteHaritasi gSayfa)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(@"SELECT * FROM SiteHaritasi 
                        WHERE site_sira > @sira ORDER BY site_sira LIMIT 1");

            MySqlDataAdapter adp = new MySqlDataAdapter(sb.ToString(), FxMySqlHelper.Connection());

            adp.SelectCommand.Parameters.AddWithValue("@sira", gSayfa.Sira);

            DataTable dt = new DataTable();

            adp.Fill(dt);

            enSiteHaritasi sayfa = new enSiteHaritasi();

            if (dt.Rows.Count > 0)
            {
                DataRow rw = dt.Rows[0];

                sayfa.Adi = rw["site_adi"].ToString();
                sayfa.DefaultSayfa = rw["site_default"].xToBooleanDefault();
                sayfa.Description = rw["site_description"].ToString();
                sayfa.Icerik = rw["site_icerik"].ToString();
                sayfa.Id = rw["site_id"].xToIntDefault();
                sayfa.Keywords = rw["site_keywords"].ToString();
                sayfa.Parent = rw["site_parent"].xToInt();
                sayfa.Resim = rw["site_resim"].ToString();

                if (sayfa.Resim.xBosMu())
                {
                    sayfa.Resim = "~/css/img/siyahBlokIcon.png";
                }

                sayfa.Statu = rw["site_statu"].xToBooleanDefault();
                sayfa.Title = rw["site_title"].ToString();
                sayfa.Url = rw["site_url"].ToString();
                sayfa.Url = UrlOlustur(sayfa).ToLowerInvariant();
                sayfa.Sira = rw["site_sira"].xToIntDefault();
                sayfa.UrunMu = rw["site_urunMu"].xToBooleanDefault();
            }

            return sayfa;
        }

        public void SiraGuncelle(enSiteHaritasi sayfa)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            List<object> degerList = new List<object>();

            dict.Add("site_sira", sayfa.Sira);

            FxMySqlHelper.Update("SiteHaritasi", dict, "site_id", sayfa.Id);
        }

        public int SonSiraNoGetir(int? parent)
        {
            StringBuilder sb = new StringBuilder();

            string whereStr = parent == null ? "" : " WHERE site_parent = @parent ";

            sb.Append(@"SELECT site_sira FROM SiteHaritasi " + whereStr + " ORDER BY site_sira DESC LIMIT 1");

            MySqlCommand cmd = new MySqlCommand(sb.ToString(), FxMySqlHelper.Connection());

            if (parent != null) cmd.Parameters.AddWithValue("@parent", parent);

            cmd.Connection.Open();

            int sayi = cmd.ExecuteScalar().xToIntDefault();

            cmd.Connection.Close();

            return sayi;
        }

        public int AltSayfaSayisi(int sayfaId)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(@"SELECT COUNT(site_id) FROM SiteHaritasi  
                        WHERE site_parent = @id");

            MySqlCommand cmd = new MySqlCommand(sb.ToString(), FxMySqlHelper.Connection());

            cmd.Parameters.AddWithValue("@id", sayfaId);

            cmd.Connection.Open();

            int sayi = cmd.ExecuteScalar().xToIntDefault();

            cmd.Connection.Close();

            return sayi;
        }

        public string UrlGetir(int sayfaId)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(@"SELECT site_url FROM SiteHaritasi  
                        WHERE site_id = @id");

            MySqlCommand cmd = new MySqlCommand(sb.ToString(), FxMySqlHelper.Connection());

            cmd.Parameters.AddWithValue("@id", sayfaId);

            cmd.Connection.Open();

            string url = cmd.ExecuteScalar().ToString();

            cmd.Connection.Close();

            return url;
        }

        public bool UrlVarMi(string url, int? dzID)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(@"SELECT COUNT(site_url) FROM SiteHaritasi  
                        WHERE site_url = @url ");

            if (dzID != null) sb.Append(" AND site_id <> @id ");

            MySqlCommand cmd = new MySqlCommand(sb.ToString(), FxMySqlHelper.Connection());

            cmd.Parameters.AddWithValue("@url", "/" + url);

            if (dzID != null) cmd.Parameters.AddWithValue("@id", dzID);

            cmd.Connection.Open();

            int count = cmd.ExecuteScalar().xToIntDefault();

            cmd.Connection.Close();

            return count > 0;
        }

        public enSiteHaritasi SayfaGetir(string url)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT * FROM SiteHaritasi WHERE site_url = @url");

            MySqlDataAdapter adp = new MySqlDataAdapter(sb.ToString(), FxMySqlHelper.Connection());

            adp.SelectCommand.Parameters.AddWithValue("@url", url.Trim());

            DataTable dt = new DataTable();

            adp.Fill(dt);

            enSiteHaritasi sayfa = new enSiteHaritasi();

            if (dt.Rows.Count > 0)
            {
                DataRow rw = dt.Rows[0];

                sayfa.Adi = rw["site_adi"].ToString();
                sayfa.DefaultSayfa = rw["site_default"].xToBooleanDefault();
                sayfa.Description = rw["site_description"].ToString();
                sayfa.Icerik = rw["site_icerik"].ToString();
                sayfa.Id = rw["site_id"].xToIntDefault();
                sayfa.Keywords = rw["site_keywords"].ToString();
                sayfa.Parent = rw["site_parent"].xToInt();
                sayfa.Resim = rw["site_resim"].ToString();
                sayfa.Statu = rw["site_statu"].xToBooleanDefault();
                sayfa.Title = rw["site_title"].ToString();
                sayfa.Url = rw["site_url"].ToString();
                sayfa.DisplayUrl = UrlOlustur(sayfa);
                sayfa.Sira = rw["site_sira"].xToIntDefault();
                sayfa.AcilirMenu = rw["site_acilirMenu"].xToBooleanDefault();
                sayfa.FizikselUrl = rw["site_fizikselUrl"].ToString();
                sayfa.Dinamik = rw["site_dinamik"].xToBooleanDefault();
                sayfa.FotoBaslik = rw["site_fotoBaslik"].ToString();
                sayfa.VideoBaslik = rw["site_videoBaslik"].ToString();
                sayfa.FotoGaleriMi = rw["site_fotoGaleri"].xToBooleanDefault();
                sayfa.FaceComments = rw["site_faceComments"].xToBooleanDefault();
                sayfa.Custom = rw["site_custom"].xToBooleanDefault();
                sayfa.PaylasimAlani = rw["site_paylasimAlani"].xToBooleanDefault();
                sayfa.BaslikAlani = rw["site_baslikAlani"].xToBooleanDefault();
                sayfa.AnaSayfa = rw["site_baslangic"].xToBooleanDefault();
                sayfa.SayfaYolu = rw["site_sayfaYolu"].xToBooleanDefault();
                sayfa.Menu = rw["site_menu"].xToBooleanDefault();
                sayfa.YanMenu = rw["site_yanMenu"].xToBooleanDefault();
                sayfa.Footer = rw["site_footer"].xToBooleanDefault();
                sayfa.SayfaMenu = rw["site_sayfaMenu"].xToBooleanDefault();
                sayfa.KayitTarihi = rw["site_kayitTar"].xToDateTimeDefault();
                sayfa.List = rw["site_list"].xToBooleanDefault();
                sayfa.CarouselId = rw["site_car_id"].xToInt();
                sayfa.UrunMu = rw["site_urunMu"].xToBooleanDefault();
            }

            return sayfa;

        }

        public enSiteHaritasi SayfaGetirFiziksel(string url)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT * FROM SiteHaritasi WHERE site_fizikselUrl = @url");

            MySqlDataAdapter adp = new MySqlDataAdapter(sb.ToString(), FxMySqlHelper.Connection());

            adp.SelectCommand.Parameters.AddWithValue("@url", url.Trim());

            DataTable dt = new DataTable();

            adp.Fill(dt);

            enSiteHaritasi sayfa = new enSiteHaritasi();

            if (dt.Rows.Count > 0)
            {
                DataRow rw = dt.Rows[0];

                sayfa.Adi = rw["site_adi"].ToString();
                sayfa.DefaultSayfa = rw["site_default"].xToBooleanDefault();
                sayfa.Description = rw["site_description"].ToString();
                sayfa.Icerik = rw["site_icerik"].ToString();
                sayfa.Id = rw["site_id"].xToIntDefault();
                sayfa.Keywords = rw["site_keywords"].ToString();
                sayfa.Parent = rw["site_parent"].xToInt();
                sayfa.Resim = rw["site_resim"].ToString();
                sayfa.Statu = rw["site_statu"].xToBooleanDefault();
                sayfa.Title = rw["site_title"].ToString();
                sayfa.Url = rw["site_url"].ToString();
                sayfa.DisplayUrl = UrlOlustur(sayfa);
                sayfa.Sira = rw["site_sira"].xToIntDefault();
                sayfa.AcilirMenu = rw["site_acilirMenu"].xToBooleanDefault();
                sayfa.FizikselUrl = rw["site_fizikselUrl"].ToString();
                sayfa.Dinamik = rw["site_dinamik"].xToBooleanDefault();
                sayfa.FotoBaslik = rw["site_fotoBaslik"].ToString();
                sayfa.VideoBaslik = rw["site_videoBaslik"].ToString();
                sayfa.FotoGaleriMi = rw["site_fotoGaleri"].xToBooleanDefault();
                sayfa.FaceComments = rw["site_faceComments"].xToBooleanDefault();
                sayfa.Custom = rw["site_custom"].xToBooleanDefault();
                sayfa.PaylasimAlani = rw["site_paylasimAlani"].xToBooleanDefault();
                sayfa.BaslikAlani = rw["site_baslikAlani"].xToBooleanDefault();
                sayfa.AnaSayfa = rw["site_baslangic"].xToBooleanDefault();
                sayfa.SayfaYolu = rw["site_sayfaYolu"].xToBooleanDefault();
                sayfa.Menu = rw["site_menu"].xToBooleanDefault();
                sayfa.YanMenu = rw["site_yanMenu"].xToBooleanDefault();
                sayfa.Footer = rw["site_footer"].xToBooleanDefault();
                sayfa.SayfaMenu = rw["site_sayfaMenu"].xToBooleanDefault();
                sayfa.KayitTarihi = rw["site_kayitTar"].xToDateTimeDefault();
                sayfa.List = rw["site_list"].xToBooleanDefault();
                sayfa.CarouselId = rw["site_car_id"].xToInt();
                sayfa.UrunMu = rw["site_urunMu"].xToBooleanDefault();
            }

            return sayfa;

        }

        private string UrlOlustur(enSiteHaritasi sayfa)
        {
            string url = sayfa.Url;

            while (sayfa.Parent != null)
            {
                sayfa = SayfaGetir(sayfa.Parent.Value);

                url = sayfa.Url + url;
            }

            return url.ToLowerInvariant();
        }

        public void CustomDurumDegistir(enSiteHaritasi sayfa)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            List<object> degerList = new List<object>();

            dict.Add("site_custom", sayfa.Custom);

            FxMySqlHelper.Update("SiteHaritasi", dict, "site_id", sayfa.Id);
        }

        public List<enSiteHaritasi> OzelSayfalariGetir()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT * FROM SiteHaritasi WHERE site_custom = @custom ORDER BY site_sira");

            MySqlDataAdapter adp = new MySqlDataAdapter(sb.ToString(), FxMySqlHelper.Connection());

            adp.SelectCommand.Parameters.AddWithValue("@custom", true);

            DataTable dt = new DataTable();

            adp.Fill(dt);

            List<enSiteHaritasi> sayfalar = new List<enSiteHaritasi>();

            foreach (DataRow rw in dt.Rows)
            {
                enSiteHaritasi sayfa = new enSiteHaritasi();

                sayfa.Adi = rw["site_adi"].ToString();
                sayfa.DefaultSayfa = rw["site_default"].xToBooleanDefault();
                sayfa.Description = rw["site_description"].ToString();
                sayfa.Icerik = rw["site_icerik"].ToString();
                sayfa.Id = rw["site_id"].xToIntDefault();
                sayfa.Keywords = rw["site_keywords"].ToString();
                sayfa.Parent = rw["site_parent"].xToInt();
                sayfa.Resim = rw["site_resim"].ToString();
                sayfa.Statu = rw["site_statu"].xToBooleanDefault();
                sayfa.Title = rw["site_title"].ToString();
                sayfa.Url = rw["site_url"].ToString();
                sayfa.Url = UrlOlustur(sayfa).ToLowerInvariant();
                sayfa.Sira = rw["site_sira"].xToIntDefault();
                sayfa.Dinamik = rw["site_dinamik"].xToBooleanDefault();
                sayfa.FizikselUrl = rw["site_fizikselUrl"].ToString();
                sayfa.AcilirMenu = rw["site_acilirMenu"].xToBooleanDefault();
                sayfa.FotoBaslik = rw["site_fotoBaslik"].ToString();
                sayfa.VideoBaslik = rw["site_videoBaslik"].ToString();
                sayfa.FotoGaleriMi = rw["site_fotoGaleri"].xToBooleanDefault();
                sayfa.FaceComments = rw["site_faceComments"].xToBooleanDefault();
                sayfa.Custom = rw["site_custom"].xToBooleanDefault();
                sayfa.PaylasimAlani = rw["site_paylasimAlani"].xToBooleanDefault();
                sayfa.BaslikAlani = rw["site_baslikAlani"].xToBooleanDefault();
                sayfa.AnaSayfa = rw["site_baslangic"].xToBooleanDefault();
                sayfa.SayfaYolu = rw["site_sayfaYolu"].xToBooleanDefault();
                sayfa.Menu = rw["site_menu"].xToBooleanDefault();
                sayfa.YanMenu = rw["site_yanMenu"].xToBooleanDefault();
                sayfa.Footer = rw["site_footer"].xToBooleanDefault();
                sayfa.SayfaMenu = rw["site_sayfaMenu"].xToBooleanDefault();
                sayfa.KayitTarihi = rw["site_kayitTar"].xToDateTimeDefault();
                sayfa.List = rw["site_list"].xToBooleanDefault();

                sayfalar.Add(sayfa);
            }

            return sayfalar;
        }

        public void MenuDegistir(enSiteHaritasi sayfa)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            List<object> degerList = new List<object>();

            dict.Add("site_menu", sayfa.Menu);

            FxMySqlHelper.Update("SiteHaritasi", dict, "site_id", sayfa.Id);
        }

        public void YanMenuDegistir(enSiteHaritasi sayfa)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            List<object> degerList = new List<object>();

            dict.Add("site_yanMenu", sayfa.YanMenu);

            FxMySqlHelper.Update("SiteHaritasi", dict, "site_id", sayfa.Id);
        }

        public void FooterDegistir(enSiteHaritasi sayfa)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            List<object> degerList = new List<object>();

            dict.Add("site_footer", sayfa.Footer);

            FxMySqlHelper.Update("SiteHaritasi", dict, "site_id", sayfa.Id);
        }

        public void SayfaMenuDegistir(enSiteHaritasi sayfa)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            List<object> degerList = new List<object>();

            dict.Add("site_sayfaMenu", sayfa.SayfaMenu);

            FxMySqlHelper.Update("SiteHaritasi", dict, "site_id", sayfa.Id);
        }

        public void ListDegistir(enSiteHaritasi sayfa)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            List<object> degerList = new List<object>();

            dict.Add("site_list", sayfa.List);

            FxMySqlHelper.Update("SiteHaritasi", dict, "site_id", sayfa.Id);
        }

        public List<enSiteHaritasi> YanUstSayfalariGetirList(bool? statu = null)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT * FROM SiteHaritasi WHERE site_parent is NULL AND site_yanMenu = @yanmenu " + (statu != null ? " AND site_statu = @statu " : "") + " ORDER BY site_sira");

            MySqlDataAdapter adp = new MySqlDataAdapter(sb.ToString(), FxMySqlHelper.Connection());

            adp.SelectCommand.Parameters.AddWithValue("@yanmenu", true);

            if (statu != null)
            {
                adp.SelectCommand.Parameters.AddWithValue("@statu", statu);
            }

            DataTable dt = new DataTable();

            adp.Fill(dt);

            List<enSiteHaritasi> sayfalar = new List<enSiteHaritasi>();

            foreach (DataRow rw in dt.Rows)
            {
                enSiteHaritasi sayfa = new enSiteHaritasi();

                sayfa.Adi = rw["site_adi"].ToString();
                sayfa.DefaultSayfa = rw["site_default"].xToBooleanDefault();
                sayfa.Description = rw["site_description"].ToString();
                sayfa.Icerik = rw["site_icerik"].ToString();
                sayfa.Id = rw["site_id"].xToIntDefault();
                sayfa.Keywords = rw["site_keywords"].ToString();
                sayfa.Parent = rw["site_parent"].xToInt();
                sayfa.Resim = rw["site_resim"].ToString();
                sayfa.Statu = rw["site_statu"].xToBooleanDefault();
                sayfa.Title = rw["site_title"].ToString();
                sayfa.Url = rw["site_url"].ToString();
                sayfa.Url = UrlOlustur(sayfa).ToLowerInvariant();
                sayfa.Sira = rw["site_sira"].xToIntDefault();
                sayfa.AcilirMenu = rw["site_acilirMenu"].xToBooleanDefault();
                sayfa.FizikselUrl = rw["site_fizikselUrl"].ToString();
                sayfa.Dinamik = rw["site_dinamik"].xToBooleanDefault();
                sayfa.FotoBaslik = rw["site_fotoBaslik"].ToString();
                sayfa.VideoBaslik = rw["site_videoBaslik"].ToString();
                sayfa.FotoGaleriMi = rw["site_fotoGaleri"].xToBooleanDefault();
                sayfa.FaceComments = rw["site_faceComments"].xToBooleanDefault();
                sayfa.Custom = rw["site_custom"].xToBooleanDefault();
                sayfa.PaylasimAlani = rw["site_paylasimAlani"].xToBooleanDefault();
                sayfa.BaslikAlani = rw["site_baslikAlani"].xToBooleanDefault();
                sayfa.AnaSayfa = rw["site_baslangic"].xToBooleanDefault();
                sayfa.SayfaYolu = rw["site_sayfaYolu"].xToBooleanDefault();
                sayfa.Menu = rw["site_menu"].xToBooleanDefault();
                sayfa.YanMenu = rw["site_yanMenu"].xToBooleanDefault();
                sayfa.Footer = rw["site_footer"].xToBooleanDefault();
                sayfa.SayfaMenu = rw["site_sayfaMenu"].xToBooleanDefault();
                sayfa.KayitTarihi = rw["site_kayitTar"].xToDateTimeDefault();
                sayfa.List = rw["site_list"].xToBooleanDefault();

                sayfalar.Add(sayfa);
            }

            return sayfalar;
        }

        public List<enSiteHaritasi> YanAltSayfalariGetirList(int parentId, bool? statu = null)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT * FROM SiteHaritasi WHERE site_parent = @parent AND site_yanMenu = @yanmenu " + (statu != null ? " AND site_statu = @statu " : "") + " ORDER BY site_sira");

            MySqlDataAdapter adp = new MySqlDataAdapter(sb.ToString(), FxMySqlHelper.Connection());

            adp.SelectCommand.Parameters.AddWithValue("@parent", parentId);
            adp.SelectCommand.Parameters.AddWithValue("@yanmenu", true);

            if (statu != null)
            {
                adp.SelectCommand.Parameters.AddWithValue("@statu", statu);
            }

            DataTable dt = new DataTable();

            adp.Fill(dt);

            List<enSiteHaritasi> sayfalar = new List<enSiteHaritasi>();

            foreach (DataRow rw in dt.Rows)
            {
                enSiteHaritasi sayfa = new enSiteHaritasi();

                sayfa.Adi = rw["site_adi"].ToString();
                sayfa.DefaultSayfa = rw["site_default"].xToBooleanDefault();
                sayfa.Description = rw["site_description"].ToString();
                sayfa.Icerik = rw["site_icerik"].ToString();
                sayfa.Id = rw["site_id"].xToIntDefault();
                sayfa.Keywords = rw["site_keywords"].ToString();
                sayfa.Parent = rw["site_parent"].xToInt();
                sayfa.Resim = rw["site_resim"].ToString();
                sayfa.Statu = rw["site_statu"].xToBooleanDefault();
                sayfa.Title = rw["site_title"].ToString();
                sayfa.Url = rw["site_url"].ToString();
                sayfa.Url = UrlOlustur(sayfa).ToLowerInvariant();
                sayfa.Sira = rw["site_sira"].xToIntDefault();
                sayfa.AcilirMenu = rw["site_acilirMenu"].xToBooleanDefault();
                sayfa.FizikselUrl = rw["site_fizikselUrl"].ToString();
                sayfa.Dinamik = rw["site_dinamik"].xToBooleanDefault();
                sayfa.FotoBaslik = rw["site_fotoBaslik"].ToString();
                sayfa.VideoBaslik = rw["site_videoBaslik"].ToString();
                sayfa.FotoGaleriMi = rw["site_fotoGaleri"].xToBooleanDefault();
                sayfa.FaceComments = rw["site_faceComments"].xToBooleanDefault();
                sayfa.Custom = rw["site_custom"].xToBooleanDefault();
                sayfa.PaylasimAlani = rw["site_paylasimAlani"].xToBooleanDefault();
                sayfa.BaslikAlani = rw["site_baslikAlani"].xToBooleanDefault();
                sayfa.AnaSayfa = rw["site_baslangic"].xToBooleanDefault();
                sayfa.SayfaYolu = rw["site_sayfaYolu"].xToBooleanDefault();
                sayfa.Menu = rw["site_menu"].xToBooleanDefault();
                sayfa.YanMenu = rw["site_yanMenu"].xToBooleanDefault();
                sayfa.Footer = rw["site_footer"].xToBooleanDefault();
                sayfa.SayfaMenu = rw["site_sayfaMenu"].xToBooleanDefault();
                sayfa.KayitTarihi = rw["site_kayitTar"].xToDateTimeDefault();
                sayfa.List = rw["site_list"].xToBooleanDefault();

                sayfalar.Add(sayfa);
            }

            return sayfalar;
        }

        public List<enSiteHaritasi> FooterUstSayfalariGetirList(bool? statu = null)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT * FROM SiteHaritasi WHERE site_parent is NULL AND site_footer = @footer " + (statu != null ? " AND site_statu = @statu " : "") + " ORDER BY site_sira");

            MySqlDataAdapter adp = new MySqlDataAdapter(sb.ToString(), FxMySqlHelper.Connection());

            adp.SelectCommand.Parameters.AddWithValue("@footer", true);

            if (statu != null)
            {
                adp.SelectCommand.Parameters.AddWithValue("@statu", statu);
            }

            DataTable dt = new DataTable();

            adp.Fill(dt);

            List<enSiteHaritasi> sayfalar = new List<enSiteHaritasi>();

            foreach (DataRow rw in dt.Rows)
            {
                enSiteHaritasi sayfa = new enSiteHaritasi();

                sayfa.Adi = rw["site_adi"].ToString();
                sayfa.DefaultSayfa = rw["site_default"].xToBooleanDefault();
                sayfa.Description = rw["site_description"].ToString();
                sayfa.Icerik = rw["site_icerik"].ToString();
                sayfa.Id = rw["site_id"].xToIntDefault();
                sayfa.Keywords = rw["site_keywords"].ToString();
                sayfa.Parent = rw["site_parent"].xToInt();
                sayfa.Resim = rw["site_resim"].ToString();
                sayfa.Statu = rw["site_statu"].xToBooleanDefault();
                sayfa.Title = rw["site_title"].ToString();
                sayfa.Url = rw["site_url"].ToString();
                sayfa.Url = UrlOlustur(sayfa).ToLowerInvariant();
                sayfa.Sira = rw["site_sira"].xToIntDefault();
                sayfa.AcilirMenu = rw["site_acilirMenu"].xToBooleanDefault();
                sayfa.FizikselUrl = rw["site_fizikselUrl"].ToString();
                sayfa.Dinamik = rw["site_dinamik"].xToBooleanDefault();
                sayfa.FotoBaslik = rw["site_fotoBaslik"].ToString();
                sayfa.VideoBaslik = rw["site_videoBaslik"].ToString();
                sayfa.FotoGaleriMi = rw["site_fotoGaleri"].xToBooleanDefault();
                sayfa.FaceComments = rw["site_faceComments"].xToBooleanDefault();
                sayfa.Custom = rw["site_custom"].xToBooleanDefault();
                sayfa.PaylasimAlani = rw["site_paylasimAlani"].xToBooleanDefault();
                sayfa.BaslikAlani = rw["site_baslikAlani"].xToBooleanDefault();
                sayfa.AnaSayfa = rw["site_baslangic"].xToBooleanDefault();
                sayfa.SayfaYolu = rw["site_sayfaYolu"].xToBooleanDefault();
                sayfa.Menu = rw["site_menu"].xToBooleanDefault();
                sayfa.YanMenu = rw["site_yanMenu"].xToBooleanDefault();
                sayfa.Footer = rw["site_footer"].xToBooleanDefault();
                sayfa.SayfaMenu = rw["site_sayfaMenu"].xToBooleanDefault();
                sayfa.KayitTarihi = rw["site_kayitTar"].xToDateTimeDefault();
                sayfa.List = rw["site_list"].xToBooleanDefault();

                sayfalar.Add(sayfa);
            }

            return sayfalar;
        }

        public List<enSiteHaritasi> FooterAltSayfalariGetirList(int parentId, bool? statu = null)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT * FROM SiteHaritasi WHERE site_parent = @parent AND site_footer = @footer " + (statu != null ? " AND site_statu = @statu " : "") + " ORDER BY site_sira");

            MySqlDataAdapter adp = new MySqlDataAdapter(sb.ToString(), FxMySqlHelper.Connection());

            adp.SelectCommand.Parameters.AddWithValue("@parent", parentId);
            adp.SelectCommand.Parameters.AddWithValue("@footer", true);

            if (statu != null)
            {
                adp.SelectCommand.Parameters.AddWithValue("@statu", statu);
            }

            DataTable dt = new DataTable();

            adp.Fill(dt);

            List<enSiteHaritasi> sayfalar = new List<enSiteHaritasi>();

            foreach (DataRow rw in dt.Rows)
            {
                enSiteHaritasi sayfa = new enSiteHaritasi();

                sayfa.Adi = rw["site_adi"].ToString();
                sayfa.DefaultSayfa = rw["site_default"].xToBooleanDefault();
                sayfa.Description = rw["site_description"].ToString();
                sayfa.Icerik = rw["site_icerik"].ToString();
                sayfa.Id = rw["site_id"].xToIntDefault();
                sayfa.Keywords = rw["site_keywords"].ToString();
                sayfa.Parent = rw["site_parent"].xToInt();
                sayfa.Resim = rw["site_resim"].ToString();
                sayfa.Statu = rw["site_statu"].xToBooleanDefault();
                sayfa.Title = rw["site_title"].ToString();
                sayfa.Url = rw["site_url"].ToString();
                sayfa.Url = UrlOlustur(sayfa).ToLowerInvariant();
                sayfa.Sira = rw["site_sira"].xToIntDefault();
                sayfa.AcilirMenu = rw["site_acilirMenu"].xToBooleanDefault();
                sayfa.FizikselUrl = rw["site_fizikselUrl"].ToString();
                sayfa.Dinamik = rw["site_dinamik"].xToBooleanDefault();
                sayfa.FotoBaslik = rw["site_fotoBaslik"].ToString();
                sayfa.VideoBaslik = rw["site_videoBaslik"].ToString();
                sayfa.FotoGaleriMi = rw["site_fotoGaleri"].xToBooleanDefault();
                sayfa.FaceComments = rw["site_faceComments"].xToBooleanDefault();
                sayfa.Custom = rw["site_custom"].xToBooleanDefault();
                sayfa.PaylasimAlani = rw["site_paylasimAlani"].xToBooleanDefault();
                sayfa.BaslikAlani = rw["site_baslikAlani"].xToBooleanDefault();
                sayfa.AnaSayfa = rw["site_baslangic"].xToBooleanDefault();
                sayfa.SayfaYolu = rw["site_sayfaYolu"].xToBooleanDefault();
                sayfa.Menu = rw["site_menu"].xToBooleanDefault();
                sayfa.YanMenu = rw["site_yanMenu"].xToBooleanDefault();
                sayfa.Footer = rw["site_footer"].xToBooleanDefault();
                sayfa.SayfaMenu = rw["site_sayfaMenu"].xToBooleanDefault();
                sayfa.KayitTarihi = rw["site_kayitTar"].xToDateTimeDefault();
                sayfa.List = rw["site_list"].xToBooleanDefault();

                sayfalar.Add(sayfa);
            }

            return sayfalar;
        }

        public List<enSiteHaritasi> MenuUstSayfalariGetirList(bool? statu = null)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT * FROM SiteHaritasi WHERE site_parent is NULL AND site_menu = @menu " + (statu != null ? " AND site_statu = @statu " : "") + " ORDER BY site_sira");

            MySqlDataAdapter adp = new MySqlDataAdapter(sb.ToString(), FxMySqlHelper.Connection());

            adp.SelectCommand.Parameters.AddWithValue("@menu", true);

            if (statu != null)
            {
                adp.SelectCommand.Parameters.AddWithValue("@statu", statu);
            }

            DataTable dt = new DataTable();

            adp.Fill(dt);

            List<enSiteHaritasi> sayfalar = new List<enSiteHaritasi>();

            foreach (DataRow rw in dt.Rows)
            {
                enSiteHaritasi sayfa = new enSiteHaritasi();

                sayfa.Adi = rw["site_adi"].ToString();
                sayfa.DefaultSayfa = rw["site_default"].xToBooleanDefault();
                sayfa.Description = rw["site_description"].ToString();
                sayfa.Icerik = rw["site_icerik"].ToString();
                sayfa.Id = rw["site_id"].xToIntDefault();
                sayfa.Keywords = rw["site_keywords"].ToString();
                sayfa.Parent = rw["site_parent"].xToInt();
                sayfa.Resim = rw["site_resim"].ToString();
                sayfa.Statu = rw["site_statu"].xToBooleanDefault();
                sayfa.Title = rw["site_title"].ToString();
                sayfa.Url = rw["site_url"].ToString();
                sayfa.Url = UrlOlustur(sayfa).ToLowerInvariant();
                sayfa.Sira = rw["site_sira"].xToIntDefault();
                sayfa.AcilirMenu = rw["site_acilirMenu"].xToBooleanDefault();
                sayfa.FizikselUrl = rw["site_fizikselUrl"].ToString();
                sayfa.Dinamik = rw["site_dinamik"].xToBooleanDefault();
                sayfa.FotoBaslik = rw["site_fotoBaslik"].ToString();
                sayfa.VideoBaslik = rw["site_videoBaslik"].ToString();
                sayfa.FotoGaleriMi = rw["site_fotoGaleri"].xToBooleanDefault();
                sayfa.FaceComments = rw["site_faceComments"].xToBooleanDefault();
                sayfa.Custom = rw["site_custom"].xToBooleanDefault();
                sayfa.PaylasimAlani = rw["site_paylasimAlani"].xToBooleanDefault();
                sayfa.BaslikAlani = rw["site_baslikAlani"].xToBooleanDefault();
                sayfa.AnaSayfa = rw["site_baslangic"].xToBooleanDefault();
                sayfa.SayfaYolu = rw["site_sayfaYolu"].xToBooleanDefault();
                sayfa.Menu = rw["site_menu"].xToBooleanDefault();
                sayfa.YanMenu = rw["site_yanMenu"].xToBooleanDefault();
                sayfa.Footer = rw["site_footer"].xToBooleanDefault();
                sayfa.SayfaMenu = rw["site_sayfaMenu"].xToBooleanDefault();
                sayfa.KayitTarihi = rw["site_kayitTar"].xToDateTimeDefault();
                sayfa.List = rw["site_list"].xToBooleanDefault();

                sayfalar.Add(sayfa);
            }

            return sayfalar;
        }

        public List<enSiteHaritasi> MenuAltSayfalariGetirList(int parentId, bool? statu = null)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT * FROM SiteHaritasi WHERE site_parent = @parent AND site_menu = @menu " + (statu != null ? " AND site_statu = @statu " : "") + " ORDER BY site_sira");

            MySqlDataAdapter adp = new MySqlDataAdapter(sb.ToString(), FxMySqlHelper.Connection());

            adp.SelectCommand.Parameters.AddWithValue("@parent", parentId);
            adp.SelectCommand.Parameters.AddWithValue("@menu", true);

            if (statu != null)
            {
                adp.SelectCommand.Parameters.AddWithValue("@statu", statu);
            }

            DataTable dt = new DataTable();

            adp.Fill(dt);

            List<enSiteHaritasi> sayfalar = new List<enSiteHaritasi>();

            foreach (DataRow rw in dt.Rows)
            {
                enSiteHaritasi sayfa = new enSiteHaritasi();

                sayfa.Adi = rw["site_adi"].ToString();
                sayfa.DefaultSayfa = rw["site_default"].xToBooleanDefault();
                sayfa.Description = rw["site_description"].ToString();
                sayfa.Icerik = rw["site_icerik"].ToString();
                sayfa.Id = rw["site_id"].xToIntDefault();
                sayfa.Keywords = rw["site_keywords"].ToString();
                sayfa.Parent = rw["site_parent"].xToInt();
                sayfa.Resim = rw["site_resim"].ToString();
                sayfa.Statu = rw["site_statu"].xToBooleanDefault();
                sayfa.Title = rw["site_title"].ToString();
                sayfa.Url = rw["site_url"].ToString();
                sayfa.Url = UrlOlustur(sayfa).ToLowerInvariant();
                sayfa.Sira = rw["site_sira"].xToIntDefault();
                sayfa.AcilirMenu = rw["site_acilirMenu"].xToBooleanDefault();
                sayfa.FizikselUrl = rw["site_fizikselUrl"].ToString();
                sayfa.Dinamik = rw["site_dinamik"].xToBooleanDefault();
                sayfa.FotoBaslik = rw["site_fotoBaslik"].ToString();
                sayfa.VideoBaslik = rw["site_videoBaslik"].ToString();
                sayfa.FotoGaleriMi = rw["site_fotoGaleri"].xToBooleanDefault();
                sayfa.FaceComments = rw["site_faceComments"].xToBooleanDefault();
                sayfa.Custom = rw["site_custom"].xToBooleanDefault();
                sayfa.PaylasimAlani = rw["site_paylasimAlani"].xToBooleanDefault();
                sayfa.BaslikAlani = rw["site_baslikAlani"].xToBooleanDefault();
                sayfa.AnaSayfa = rw["site_baslangic"].xToBooleanDefault();
                sayfa.SayfaYolu = rw["site_sayfaYolu"].xToBooleanDefault();
                sayfa.Menu = rw["site_menu"].xToBooleanDefault();
                sayfa.YanMenu = rw["site_yanMenu"].xToBooleanDefault();
                sayfa.Footer = rw["site_footer"].xToBooleanDefault();
                sayfa.SayfaMenu = rw["site_sayfaMenu"].xToBooleanDefault();
                sayfa.KayitTarihi = rw["site_kayitTar"].xToDateTimeDefault();
                sayfa.List = rw["site_list"].xToBooleanDefault();

                sayfalar.Add(sayfa);
            }

            return sayfalar;
        }

        public void ResimGuncelle(int pageId, string pictureUrl)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            List<object> degerList = new List<object>();

            dict.Add("site_resim", pictureUrl);

            FxMySqlHelper.Update("SiteHaritasi", dict, "site_id", pageId);
        }

        public void CarouselSec(int sayfaId, int carouseId, bool carouselSec)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            List<object> degerList = new List<object>();

            dict.Add("site_car_id", carouselSec ? carouseId : 0);

            FxMySqlHelper.Update("SiteHaritasi", dict, "site_id", sayfaId);
        }

        public List<enSiteHaritasi> SolAltMenuGetir()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT * FROM SiteHaritasi WHERE site_parent is NULL AND site_statu = @statu AND site_sol_altMenu = @solAlt ORDER BY site_sira");

            MySqlDataAdapter adp = new MySqlDataAdapter(sb.ToString(), FxMySqlHelper.Connection());

            adp.SelectCommand.Parameters.AddWithValue("@statu", true);
            adp.SelectCommand.Parameters.AddWithValue("@solAlt", true);

            DataTable dt = new DataTable();

            adp.Fill(dt);

            List<enSiteHaritasi> sayfalar = new List<enSiteHaritasi>();

            foreach (DataRow rw in dt.Rows)
            {
                enSiteHaritasi sayfa = new enSiteHaritasi();

                sayfa.Adi = rw["site_adi"].ToString();
                sayfa.DefaultSayfa = rw["site_default"].xToBooleanDefault();
                sayfa.Description = rw["site_description"].ToString();
                sayfa.Icerik = rw["site_icerik"].ToString();
                sayfa.Id = rw["site_id"].xToIntDefault();
                sayfa.Keywords = rw["site_keywords"].ToString();
                sayfa.Parent = rw["site_parent"].xToInt();
                sayfa.Resim = rw["site_resim"].ToString();
                sayfa.Statu = rw["site_statu"].xToBooleanDefault();
                sayfa.Title = rw["site_title"].ToString();
                sayfa.Url = rw["site_url"].ToString();
                sayfa.Url = UrlOlustur(sayfa).ToLowerInvariant();
                sayfa.Sira = rw["site_sira"].xToIntDefault();
                sayfa.AcilirMenu = rw["site_acilirMenu"].xToBooleanDefault();
                sayfa.FizikselUrl = rw["site_fizikselUrl"].ToString();
                sayfa.Dinamik = rw["site_dinamik"].xToBooleanDefault();
                sayfa.FotoBaslik = rw["site_fotoBaslik"].ToString();
                sayfa.VideoBaslik = rw["site_videoBaslik"].ToString();
                sayfa.FotoGaleriMi = rw["site_fotoGaleri"].xToBooleanDefault();
                sayfa.FaceComments = rw["site_faceComments"].xToBooleanDefault();
                sayfa.Custom = rw["site_custom"].xToBooleanDefault();
                sayfa.PaylasimAlani = rw["site_paylasimAlani"].xToBooleanDefault();
                sayfa.BaslikAlani = rw["site_baslikAlani"].xToBooleanDefault();
                sayfa.AnaSayfa = rw["site_baslangic"].xToBooleanDefault();
                sayfa.SayfaYolu = rw["site_sayfaYolu"].xToBooleanDefault();
                sayfa.Menu = rw["site_menu"].xToBooleanDefault();
                sayfa.YanMenu = rw["site_yanMenu"].xToBooleanDefault();
                sayfa.Footer = rw["site_footer"].xToBooleanDefault();
                sayfa.SayfaMenu = rw["site_sayfaMenu"].xToBooleanDefault();
                sayfa.KayitTarihi = rw["site_kayitTar"].xToDateTimeDefault();
                sayfa.List = rw["site_list"].xToBooleanDefault();
                sayfa.UrunMu = rw["site_urunMu"].xToBooleanDefault();

                sayfalar.Add(sayfa);
            }

            return sayfalar;
        }

        public List<enSiteHaritasi> SagAltMenuGetir()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT * FROM SiteHaritasi WHERE site_parent is NULL AND site_statu = @statu AND site_sag_altMenu = @sagAlt ORDER BY site_sira");

            MySqlDataAdapter adp = new MySqlDataAdapter(sb.ToString(), FxMySqlHelper.Connection());

            adp.SelectCommand.Parameters.AddWithValue("@statu", true);
            adp.SelectCommand.Parameters.AddWithValue("@sagAlt", true);

            DataTable dt = new DataTable();

            adp.Fill(dt);

            List<enSiteHaritasi> sayfalar = new List<enSiteHaritasi>();

            foreach (DataRow rw in dt.Rows)
            {
                enSiteHaritasi sayfa = new enSiteHaritasi();

                sayfa.Adi = rw["site_adi"].ToString();
                sayfa.DefaultSayfa = rw["site_default"].xToBooleanDefault();
                sayfa.Description = rw["site_description"].ToString();
                sayfa.Icerik = rw["site_icerik"].ToString();
                sayfa.Id = rw["site_id"].xToIntDefault();
                sayfa.Keywords = rw["site_keywords"].ToString();
                sayfa.Parent = rw["site_parent"].xToInt();
                sayfa.Resim = rw["site_resim"].ToString();
                sayfa.Statu = rw["site_statu"].xToBooleanDefault();
                sayfa.Title = rw["site_title"].ToString();
                sayfa.Url = rw["site_url"].ToString();
                sayfa.Url = UrlOlustur(sayfa).ToLowerInvariant();
                sayfa.Sira = rw["site_sira"].xToIntDefault();
                sayfa.AcilirMenu = rw["site_acilirMenu"].xToBooleanDefault();
                sayfa.FizikselUrl = rw["site_fizikselUrl"].ToString();
                sayfa.Dinamik = rw["site_dinamik"].xToBooleanDefault();
                sayfa.FotoBaslik = rw["site_fotoBaslik"].ToString();
                sayfa.VideoBaslik = rw["site_videoBaslik"].ToString();
                sayfa.FotoGaleriMi = rw["site_fotoGaleri"].xToBooleanDefault();
                sayfa.FaceComments = rw["site_faceComments"].xToBooleanDefault();
                sayfa.Custom = rw["site_custom"].xToBooleanDefault();
                sayfa.PaylasimAlani = rw["site_paylasimAlani"].xToBooleanDefault();
                sayfa.BaslikAlani = rw["site_baslikAlani"].xToBooleanDefault();
                sayfa.AnaSayfa = rw["site_baslangic"].xToBooleanDefault();
                sayfa.SayfaYolu = rw["site_sayfaYolu"].xToBooleanDefault();
                sayfa.Menu = rw["site_menu"].xToBooleanDefault();
                sayfa.YanMenu = rw["site_yanMenu"].xToBooleanDefault();
                sayfa.Footer = rw["site_footer"].xToBooleanDefault();
                sayfa.SayfaMenu = rw["site_sayfaMenu"].xToBooleanDefault();
                sayfa.KayitTarihi = rw["site_kayitTar"].xToDateTimeDefault();
                sayfa.List = rw["site_list"].xToBooleanDefault();
                sayfa.UrunMu = rw["site_urunMu"].xToBooleanDefault();

                sayfalar.Add(sayfa);
            }

            return sayfalar;
        }
    }
}
