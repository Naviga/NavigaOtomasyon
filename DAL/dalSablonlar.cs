﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entity;
using System.Data.OleDb;
using System.Data;

namespace DAL
{
    public class dalSablonlar
    {
        public void YeniEkle(enSablon sablon)
        {
            List<string> prmList = new List<string>();
            List<object> degerList = new List<object>();

            prmList.Add("sbl_adi"); degerList.Add(sablon.Adi);
            prmList.Add("sbl_aciklama"); degerList.Add(sablon.Aciklama);
            prmList.Add("sbl_ikon"); degerList.Add(sablon.Ikon);

            dalManager.MakeAnDbInsert(prmList, "Sablonlar", degerList, "");
        }

        public List<enSablon> SablonlariGetir()
        {
            StringBuilder sb = new StringBuilder();
            OleDbDataAdapter adp = new OleDbDataAdapter("", dalManager.Connection());

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


            OleDbDataAdapter adp = new OleDbDataAdapter(sb.ToString(), dalManager.Connection());

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
            dalManager.MakeAnDbDelete("Sablonlar", "sbl_id", sablonId);
        }
    }
}
