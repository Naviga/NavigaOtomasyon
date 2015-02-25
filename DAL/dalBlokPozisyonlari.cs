using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using Entity;

namespace DAL
{
    public class dalBlokPozisyonlari
    {
        public List<enBlokPozisyon> Getir()
        {
            StringBuilder sb = new StringBuilder();
            OleDbDataAdapter adp = new OleDbDataAdapter("", dalManager.Connection());

            sb.Append("SELECT * FROM BlokPozisyonlari ORDER BY bPoz_adi");

            adp.SelectCommand.CommandText = sb.ToString();

            DataTable dt = new DataTable();

            adp.Fill(dt);

            List<enBlokPozisyon> pozisyonlar = new List<enBlokPozisyon>();
            foreach (DataRow rw in dt.Rows)
            {
                enBlokPozisyon pozisyon = new enBlokPozisyon();

                pozisyon.Id = rw["bPoz_id"].xToIntDefault();
                pozisyon.Adi = rw["bPoz_adi"].ToString();
                pozisyon.Master = rw["bPoz_master"].xToBooleanDefault();

                pozisyonlar.Add(pozisyon);
            }

            return pozisyonlar;
        }

        public enBlokPozisyon Getir(int id)
        {
            StringBuilder sb = new StringBuilder();
            OleDbDataAdapter adp = new OleDbDataAdapter("", dalManager.Connection());

            sb.Append("SELECT * FROM BlokPozisyonlari WHERE bPoz_id = @id");

            adp.SelectCommand.Parameters.AddWithValue("@id", id);

            adp.SelectCommand.CommandText = sb.ToString();

            DataTable dt = new DataTable();

            adp.Fill(dt);

            enBlokPozisyon pozisyon = new enBlokPozisyon();
            if (dt.Rows.Count > 0)
            {
                DataRow rw = dt.Rows[0];

                pozisyon.Id = rw["bPoz_id"].xToIntDefault();
                pozisyon.Adi = rw["bPoz_adi"].ToString();
                pozisyon.Master = rw["bPoz_master"].xToBooleanDefault();
            }

            return pozisyon;
        }
    }
}
