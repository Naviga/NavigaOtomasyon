using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entity;
using MySql.Data.MySqlClient;
using System.Data;

namespace DAL
{
    public class dalSablonlar
    {
        public void YeniEkle(enSablon sablon)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            List<object> degerList = new List<object>();

            dict.Add("sbl_adi",sablon.Adi);
            dict.Add("sbl_aciklama",sablon.Aciklama);
            dict.Add("sbl_ikon",sablon.Ikon);

            FxMySqlHelper.Insert("Sablonlar",dict);
        }

        public List<enSablon> SablonlariGetir()
        {
            StringBuilder sb = new StringBuilder();
            MySqlDataAdapter adp = new MySqlDataAdapter("", FxMySqlHelper.Connection());

            sb.Append("SELECT * FROM Sablonlar");

            adp.SelectCommand.CommandText = sb.ToString();

            DataTable dt = new DataTable();

            adp.Fill(dt);

            List<enSablon> Sablonlar = new List<enSablon>();
            foreach (DataRow rw in dt.Rows)
            {
                enSablon sablon = new enSablon();

                sablon.Id = rw["sbl_id"].xToIntDefault();
                sablon.Adi = rw["sbl_adi"].ToString();
                sablon.Aciklama = rw["sbl_aciklama"].ToString();
                sablon.Ikon = rw["sbl_ikon"].ToString();
                sablon.IcerikGenislik = rw["sbl_icerikGenislik"].ToString();
                sablon.Margin = rw["sbl_margin"].ToString();
                sablon.MarginLeft = rw["sbl_marginLeft"].ToString();
                sablon.MarginRight = rw["sbl_marginRight"].ToString();
                sablon.BlokGenislik = rw["sbl_blokGenislik"].ToString();
                sablon.SlaytId = rw["sbl_slayt_id"].xToIntDefault();
                sablon.MasterPageFile = rw["sbl_masterPage"].ToString();

                Sablonlar.Add(sablon);
            }

            return Sablonlar;
        }

        public enSablon SablonGetir(int sablonId)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT * FROM Sablonlar WHERE sbl_id = @id");


            MySqlDataAdapter adp = new MySqlDataAdapter(sb.ToString(), FxMySqlHelper.Connection());

            adp.SelectCommand.Parameters.AddWithValue("@id", sablonId);

            DataTable dt = new DataTable();

            adp.Fill(dt);

            enSablon sablon = new enSablon();

            if (dt.Rows.Count > 0)
            {
                DataRow rw = dt.Rows[0];

                sablon.Id = rw["sbl_id"].xToIntDefault();
                sablon.Adi = rw["sbl_adi"].ToString();
                sablon.Aciklama = rw["sbl_aciklama"].ToString();
                sablon.Ikon = rw["sbl_ikon"].ToString();
                sablon.IcerikGenislik = rw["sbl_icerikGenislik"].ToString();
                sablon.Margin = rw["sbl_margin"].ToString();
                sablon.MarginLeft = rw["sbl_marginLeft"].ToString();
                sablon.MarginRight = rw["sbl_marginRight"].ToString();
                sablon.BlokGenislik = rw["sbl_blokGenislik"].ToString();
                sablon.SlaytId = rw["sbl_slayt_id"].xToIntDefault();
                sablon.MasterPageFile = rw["sbl_masterPage"].ToString();
            }

            return sablon;
        }

        public void Sil(int sablonId)
        {
            FxMySqlHelper.Delete("Sablonlar", "sbl_id", sablonId);
        }
    }
}
