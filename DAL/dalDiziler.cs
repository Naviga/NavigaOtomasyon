﻿using System.Web;
using Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class dalDiziler
    {
        public void Yeni(enDizi dizi, int dilId)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();

            dict.Add("dizi_id", dizi.Id);
            dict.Add("dil_id", dilId);
            dict.Add("degeri", dizi.Degeri);

            AccessHelper.Insert("Diziler_Dil", dict);
        }

        public void YeniKod(enDizi dizi)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();

            dict.Add("dizi_kodu", dizi.Kodu);

            AccessHelper.Insert("Diziler", dict);
        }

        //        public string DilDiziGetir(string kodu, int? dilId)
        //        {
        //            string sonuc = "";

        //            StringBuilder sb = new StringBuilder();

        //            MySqlDataAdapter adp = new MySqlDataAdapter(sb.ToString(), AccessHelper.Connection());

        //            if (!Common.SessionManager.SeciliDil.VarsayilanMi)
        //            {
        //                sb.Append(@"select dizi_degeri, dd.degeri from Diziler dz
        //                            left outer join Diziler_Dil dd on dd.dizi_id = dz.dizi_id and dd.dil_id = @dilId
        //                            where dz.dizi_kodu = @kodu");

        //                adp.SelectCommand.Parameters.AddWithValue("@dilId", dilId);
        //            }
        //            else
        //            {
        //                sb.Append("select dizi_degeri from Diziler where dizi_kodu = @kodu");
        //            }

        //            adp.SelectCommand.Parameters.AddWithValue("@kodu", kodu);

        //            adp.SelectCommand.CommandText = sb.ToString();

        //            DataTable dt = new DataTable();

        //            adp.Fill(dt);

        //            if (dt.Rows.Count > 0)
        //            {
        //                string varsDeger = dt.Rows[0]["dizi_degeri"].ToString();
        //                string dilDeger = "";

        //                if (!Common.SessionManager.SeciliDil.VarsayilanMi)
        //                {
        //                    dilDeger = dt.Rows[0]["degeri"].ToString();

        //                    sonuc = dilDeger.xBosMu() ? varsDeger : dilDeger;
        //                }
        //                else
        //                {
        //                    sonuc = varsDeger;
        //                }
        //            }

        //            return sonuc;
        //        }

        public string DiziGetir(string kodu)
        {
            string sql = @"select dizi_degeri from Diziler where dizi_kodu = @kodu";

            OleDbCommand cmd = new OleDbCommand(sql, AccessHelper.Connection());

            cmd.Parameters.AddWithValue("@kodu", kodu);

            cmd.Connection.Open();

            object sonuc = cmd.ExecuteScalar();

            cmd.Connection.Close();

            return sonuc == null ? kodu : HttpContext.Current.Server.HtmlDecode(sonuc.ToString());

        }

        public enDizi DiziGetir(int id)
        {
            string sql = @"select * from Diziler where dizi_id = @id";

            MySqlDataAdapter adp = new MySqlDataAdapter(sql, AccessHelper.Connection());

            adp.SelectCommand.Parameters.AddWithValue("@id", id);

            DataTable dt = new DataTable();

            adp.Fill(dt);

            enDizi dizi = new enDizi();
            if (dt.Rows.Count > 0)
            {
                DataRow rw = dt.Rows[0];

                dizi.Id = rw["dizi_id"].xToIntDefault();
                dizi.Kodu = rw["dizi_kodu"].ToString();
                dizi.VarsayilanDegeri = rw["dizi_degeri"].ToString();
                dizi.Degeri = rw["dizi_degeri"].ToString();
            }

            return dizi;
        }

        public enDizi DiziGetirEnt(string kodu)
        {
            string sql = @"select * from Diziler where dizi_kodu = @kodu";

            MySqlDataAdapter adp = new MySqlDataAdapter(sql, AccessHelper.Connection());

            adp.SelectCommand.Parameters.AddWithValue("@kodu", kodu);

            DataTable dt = new DataTable();

            adp.Fill(dt);

            enDizi dizi = new enDizi();
            if (dt.Rows.Count > 0)
            {
                DataRow rw = dt.Rows[0];

                dizi.Id = rw["dizi_id"].xToIntDefault();
                dizi.Kodu = rw["dizi_kodu"].ToString();
                dizi.VarsayilanDegeri = rw["dizi_degeri"].ToString();

            }

            return dizi;
        }

        public List<enDizi> TumunuGetir(string kodu)
        {
            StringBuilder sb = new StringBuilder();

            //LEFT OUTER JOIN Diziler_Dil dd ON dd.dizi_id = d.dizi_id  AND dd.dil_id = @dilId
            if (kodu.xBosMu() == false)
            {
                sb.Append(@"SELECT * FROM Diziler d WHERE d.dizi_kodu LIKE @kodu
                        ORDER BY d.dizi_kodu");
            }
            else
            {
                sb.Append(@"SELECT * FROM Diziler d 
                        ORDER BY d.dizi_kodu");
            }

            MySqlDataAdapter adp = new MySqlDataAdapter(sb.ToString(), AccessHelper.Connection());

            if (kodu.xBosMu() == false)
            {
                adp.SelectCommand.Parameters.AddWithValue("@kodu", "%" + kodu + "%");
            }

            //adp.SelectCommand.Parameters.AddWithValue("@dilId", dilId);

            DataTable dt = new DataTable();

            adp.Fill(dt);

            List<enDizi> diziler = new List<enDizi>();
            foreach (DataRow rw in dt.Rows)
            {
                enDizi dizi = new enDizi();

                dizi.VarsayilanDegeri = rw["dizi_degeri"].ToString();
                dizi.Id = rw["dizi_id"].xToIntDefault();
                dizi.Kodu = rw["dizi_kodu"].ToString();
                dizi.Degeri = rw["dizi_degeri"].ToString();

                diziler.Add(dizi);
            }

            return diziler;
        }

        public void VarsayilanDiziDuncelle(enDizi dizi)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();

            dict.Add("dizi_degeri", dizi.VarsayilanDegeri);

            AccessHelper.Update("Diziler", dict, "dizi_id", dizi.Id);
        }

        //public void DiziDilDuncelle(enDizi dizi, int dilId)
        //{
        //    if (DiziDilVarMi(dizi.Id, dilId))
        //    {
        //        StringBuilder sb = new StringBuilder();

        //        sb.Append(@"UPDATE Diziler_Dil SET Diziler_Dil.degeri = @deger WHERE Diziler_Dil.dil_id = @dilId AND Diziler_Dil.dizi_id = @diziId");

        //        OleDbCommand cmd = new OleDbCommand(sb.ToString(), AccessHelper.Connection());

        //        cmd.Parameters.AddWithValue("@deger", dizi.Degeri);
        //        cmd.Parameters.AddWithValue("@dilId", dilId);
        //        cmd.Parameters.AddWithValue("@diziId", dizi.Id);

        //        cmd.Connection.Open();

        //        cmd.ExecuteNonQuery();

        //        cmd.Connection.Close();
        //    }
        //    else
        //    {
        //        Yeni(dizi, dilId);
        //    }
        //}

        //public bool DiziDilVarMi(int diziId, int dilId)
        //{
        //    string sql = @"select degeri from Diziler_Dil where dizi_id = @diziId and dil_id = @dil";

        //    OleDbCommand cmd = new OleDbCommand(sql, AccessHelper.Connection());

        //    cmd.Parameters.AddWithValue("@diziId", diziId);
        //    cmd.Parameters.AddWithValue("@dil", dilId);

        //    cmd.Connection.Open();

        //    object sonuc = cmd.ExecuteScalar();

        //    cmd.Connection.Close();

        //    return sonuc != null;
        //}
        public void Sil(int diziId)
        {
            AccessHelper.Delete("Diziler", "dizi_id", diziId);
        }
    }
}
