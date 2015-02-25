using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entity;
using System.Data;
using System.Data.OleDb;

namespace DAL
{
    public class dalTasarimAyarlari
    {
        public List<enTasarimAyar> TasarimAyarlariniGetir()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(@"SELECT * FROM TasarimAyarlari");

            OleDbDataAdapter adp = new OleDbDataAdapter(sb.ToString(), dalManager.Connection());

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

            OleDbDataAdapter adp = new OleDbDataAdapter(sb.ToString(), dalManager.Connection());

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
            List<string> prmList = new List<string>();
            List<object> degerList = new List<object>();
            
            prmList.Add("tasAy_degeri"); degerList.Add(ayar.Degeri);

            dalManager.MakeAnDbUpdate(prmList, "TasarimAyarlari", "tasAy_id", ayar.Id, degerList);
        }

        
    }
}
