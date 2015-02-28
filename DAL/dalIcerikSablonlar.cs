using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entity;
using MySql.Data.MySqlClient;
using System.Data;

namespace DAL
{
    public class dalIcerikSablonlar
    {
        public void YeniEkle(enIcerikSablon sablon)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            List<object> degerList = new List<object>();

            dict.Add("icerikSablon_adi"); degerList.Add(sablon.Adi);
            dict.Add("icerikSablon_aciklama"); degerList.Add(sablon.Aciklama);

            dalManager.MakeAnDbInsert(prmList, "IcerikSablonlari", degerList, "");
        }

        public List<enIcerikSablon> SablonlariGetir()
        {
            StringBuilder sb = new StringBuilder();
            MySqlDataAdapter adp = new MySqlDataAdapter("", FxMySqlHelper.Connection());

            sb.Append("SELECT * FROM IcerikSablonlari");

            adp.SelectCommand.CommandText = sb.ToString();

            DataTable dt = new DataTable();

            adp.Fill(dt);

            List<enIcerikSablon> IcerikSablonlari = new List<enIcerikSablon>();
            foreach (DataRow rw in dt.Rows)
            {
                enIcerikSablon sablon = new enIcerikSablon();

                sablon.Id = rw["icerikSablon_id"].xToIntDefault();
                sablon.Adi = rw["icerikSablon_adi"].ToString();
                sablon.Aciklama = rw["icerikSablon_aciklama"].ToString();
                sablon.MasterPageFile = rw["icerikSablon_masterPage"].ToString();

                IcerikSablonlari.Add(sablon);
            }

            return IcerikSablonlari;
        }

        public enIcerikSablon SablonGetir(int sablonId)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT * FROM IcerikSablonlari WHERE icerikSablon_id = @id");


            MySqlDataAdapter adp = new MySqlDataAdapter(sb.ToString(), FxMySqlHelper.Connection());

            adp.SelectCommand.Parameters.AddWithValue("@id", sablonId);

            DataTable dt = new DataTable();

            adp.Fill(dt);

            enIcerikSablon sablon = new enIcerikSablon();

            if (dt.Rows.Count > 0)
            {
                DataRow rw = dt.Rows[0];

                sablon.Id = rw["icerikSablon_id"].xToIntDefault();
                sablon.Adi = rw["icerikSablon_adi"].ToString();
                sablon.Aciklama = rw["icerikSablon_aciklama"].ToString();
                sablon.MasterPageFile = rw["icerikSablon_masterPage"].ToString();
            }

            return sablon;
        }

        public void Sil(int sablonId)
        {
            dalManager.MakeAnDbDelete("IcerikSablonlari", "icerikSablon_id", sablonId);
        }
    }
}
