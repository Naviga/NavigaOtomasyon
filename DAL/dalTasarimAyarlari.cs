﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entity;
using System.Data;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class dalTasarimAyarlari
    {
        public List<enTasarimAyar> TasarimAyarlariniGetir()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(@"SELECT * FROM TasarimAyarlari");

            MySqlDataAdapter adp = new MySqlDataAdapter(sb.ToString(), FxMySqlHelper.Connection());

            DataTable dt = new DataTable();

            adp.Fill(dt);

            List<enTasarimAyar> ayarlar = new List<enTasarimAyar>();

            foreach (DataRow rw in dt.Rows)
            {
                enTasarimAyar ayar = new enTasarimAyar();

                ayar.Id = rw["tasAy_id"].xToIntDefault();
                ayar.Adi = rw["tasAy_adi"].ToString();
                ayar.Degeri = rw["tasAy_degeri"].ToString();

                ayarlar.Add(ayar);
            }

            return ayarlar;
        }

        public enTasarimAyar TasarimAyariGetir(int id)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(@"SELECT * FROM TasarimAyarlari WHERE tasAy_id = @id");

            MySqlDataAdapter adp = new MySqlDataAdapter(sb.ToString(), FxMySqlHelper.Connection());

            adp.SelectCommand.Parameters.AddWithValue("@id", id);

            DataTable dt = new DataTable();

            adp.Fill(dt);

            enTasarimAyar ayar = new enTasarimAyar();

            if (dt.Rows.Count > 0)
            {
                DataRow rw = dt.Rows[0];

                ayar.Id = rw["tasAy_id"].xToIntDefault();
                ayar.Adi = rw["tasAy_adi"].ToString();
                ayar.Degeri = rw["tasAy_degeri"].ToString();
            }

            return ayar;
        }

        public void TasarimAyariGuncelle(enTasarimAyar ayar)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();

            dict.Add("tasAy_degeri", ayar.Degeri);

            FxMySqlHelper.Update("TasarimAyarlari", dict, "tasAy_id", ayar.Id);
        }


    }
}
