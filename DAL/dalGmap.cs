using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
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

            OleDbDataAdapter adp = new OleDbDataAdapter(sb.ToString(), dalManager.Connection());

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
            List<string> prmList = new List<string>();
            List<object> degerList = new List<object>();

            prmList.Add("gmap_lat"); degerList.Add(gmap.Latitude);
            prmList.Add("gmap_long"); degerList.Add(gmap.Longitude);
            prmList.Add("gmap_apiKey"); degerList.Add(gmap.APIKey);
            prmList.Add("gmap_statu"); degerList.Add(gmap.Statu);
            prmList.Add("gmap_metin"); degerList.Add(gmap.Metin);

            dalManager.MakeAnDbUpdate(prmList, "GMap", "gmap_id", gmap.Id, degerList);
        }
    }
}
