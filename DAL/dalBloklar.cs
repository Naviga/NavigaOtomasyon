using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using Entity;
using MySql.Data.MySqlClient;
using System.Data;

namespace DAL
{
    public class dalBloklar
    {
        public List<enBlok> BloklariGetir(bool? statu)
        {
            string whereStr = statu == null ? "" : " WHERE blok_statu = @statu";

            StringBuilder sb = new StringBuilder();

            sb.Append(@"SELECT * FROM Bloklar " + whereStr + " ORDER BY blok_adi ");

            MySqlDataAdapter adp = new MySqlDataAdapter(sb.ToString(), FxMySqlHelper.Connection());

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
                blok.Id = rw["blok_id"].xToIntDefault();
                blok.Sira = rw["blok_sira"].xToIntDefault();
                blok.UscYolu = rw["blok_uscYol"].ToString();
                blok.BaslikKullanimi = rw["blok_baslik"].xToBooleanDefault();
                blok.Width = rw["blok_width"].ToString();
                blok.Height = rw["blok_height"].ToString();
                blok.Statu = rw["blok_statu"].xToBooleanDefault();
                blok.CerceveKullanimi = rw["blok_cerceve"].xToBooleanDefault();
                blok.Default = rw["blok_default"].xToBooleanDefault();
                blok.PozisyonId = rw["blok_bPoz_id"].xToIntDefault();
                blok.SayfaId = rw["blok_sayfa_id"].xToInt();
                blok.Aciklama = rw["blok_aciklama"].ToString();

                blok.CarouselId = rw["blok_car_id"].xToInt();

                if (SessionManager.Admin != null && SessionManager.Admin.Finex)
                {
                    blok.Default = false;
                }

                bloklar.Add(blok);
            }

            return bloklar;
        }

        public enBlok BlokGetir(int blokId)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(@"SELECT * FROM Bloklar WHERE blok_id = @id");

            MySqlDataAdapter adp = new MySqlDataAdapter(sb.ToString(), FxMySqlHelper.Connection());

            adp.SelectCommand.Parameters.AddWithValue("@id", blokId);

            DataTable dt = new DataTable();

            adp.Fill(dt);

            enBlok blok = new enBlok();

            if (dt.Rows.Count > 0)
            {
                DataRow rw = dt.Rows[0];

                blok.Adi = rw["blok_adi"].ToString();
                blok.Icerik = rw["blok_icerik"].ToString();
                blok.Id = rw["blok_id"].xToIntDefault();
                blok.Statu = rw["blok_statu"].xToBooleanDefault();
                blok.Sira = rw["blok_sira"].xToIntDefault();
                blok.UscYolu = rw["blok_uscYol"].ToString();
                blok.BaslikKullanimi = rw["blok_baslik"].xToBooleanDefault();
                blok.Width = rw["blok_width"].ToString();
                blok.Height = rw["blok_height"].ToString();
                blok.CerceveKullanimi = rw["blok_cerceve"].xToBooleanDefault();
                blok.Default = rw["blok_default"].xToBooleanDefault();
                blok.PozisyonId = rw["blok_bPoz_id"].xToIntDefault();
                blok.SayfaId = rw["blok_sayfa_id"].xToInt();
                blok.Aciklama = rw["blok_aciklama"].ToString();

                blok.CarouselId = rw["blok_car_id"].xToInt();

            }

            return blok;
        }

        public void YeniBlokEkle(enBlok blok)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();

            dict.Add("blok_adi", blok.Adi);
            dict.Add("blok_statu", blok.Statu);
            dict.Add("blok_icerik", blok.Icerik);
            dict.Add("blok_baslik", blok.BaslikKullanimi);
            dict.Add("blok_sayfa_id", blok.SayfaId);
            dict.Add("blok_aciklama", blok.Aciklama);
            dict.Add("blok_car_id", blok.CarouselId);

            FxMySqlHelper.Insert("Bloklar", dict);
        }

        public void BlokSil(int blokId)
        {
            FxMySqlHelper.Delete("Bloklar", "blok_id", blokId);
        }

        public void BlokGuncelle(enBlok blok)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();

            dict.Add("blok_adi", blok.Adi);
            dict.Add("blok_icerik", blok.Icerik);
            dict.Add("blok_baslik", blok.BaslikKullanimi);
            dict.Add("blok_sayfa_id", blok.SayfaId);
            dict.Add("blok_aciklama", blok.Aciklama);
            dict.Add("blok_car_id", blok.CarouselId);

            FxMySqlHelper.Update("Bloklar", dict, "blok_id", blok.Id);
        }

        public void BlokStatuGuncelle(enBlok blok)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();

            dict.Add("blok_statu", blok.Statu);

            FxMySqlHelper.Update("Bloklar", dict, "blok_id", blok.Id);
        }
    }
}
