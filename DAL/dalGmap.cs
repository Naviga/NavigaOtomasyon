using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using Entity;


namespace DAL
{
    public class dalGmap
    {
        public enGmap GmapGetir()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT * FROM GMap");

            MySqlDataAdapter adp = new MySqlDataAdapter(sb.ToString(), FxMySqlHelper.Connection());

            DataTable dt = new DataTable();

            adp.Fill(dt);

            enGmap gmap = new enGmap();

            if (dt.Rows.Count > 0)
            {
                DataRow rw = dt.Rows[0];

                gmap.Id = rw["gmap_id"].xToIntDefault();
                gmap.APIKey = rw["gmap_apiKey"].ToString();
                gmap.Latitude = rw["gmap_lat"].ToString();
                gmap.Longitude = rw["gmap_long"].ToString();
                gmap.Statu = rw["gmap_statu"].xToBooleanDefault();
                gmap.Metin = rw["gmap_metin"].ToString();

            }

            return gmap;
        }

        public void GmapGuncelle(enGmap gmap)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            List<object> degerList = new List<object>();

            dict.Add("gmap_lat",gmap.Latitude); degerList.Add(gmap.Latitude);
            dict.Add("gmap_long", gmap.Longitude); degerList.Add(gmap.Longitude);
            dict.Add("gmap_apiKey", gmap.APIKey); degerList.Add(gmap.APIKey);
            dict.Add("gmap_statu", gmap.Statu); degerList.Add(gmap.Statu);
            dict.Add("gmap_metin", gmap.Metin); degerList.Add(gmap.Metin);

            FxMySqlHelper.Insert("GMap", dict);
        }
    }
}
