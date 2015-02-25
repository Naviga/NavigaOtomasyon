using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using Entity;

namespace DAL
{
    public class dalGenelAyarlar
    {
        public List<enGenelAyar> GenelAyarlariGetir()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(@"SELECT * FROM GenelAyarlar");

            OleDbDataAdapter adp = new OleDbDataAdapter(sb.ToString(), dalManager.Connection());

            DataTable dt = new DataTable();

            adp.Fill(dt);

            List<enGenelAyar> ayarlar = new List<enGenelAyar>();

            foreach (DataRow rw in dt.Rows)
            {
                enGenelAyar ayar = new enGenelAyar();

                ayar.Adi = rw["gayr_adi"].ToString();
                ayar.Icerik = rw["gayr_icerik"].ToString();
                ayar.Id = rw["gayr_id"].xToIntDefault();
                ayar.Statu = rw["gayr_statu"].xToBooleanDefault();

                ayarlar.Add(ayar);
            }

            return ayarlar;
        }

        public enGenelAyar GenelAyarGetir(int ayarId)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(@"SELECT * FROM GenelAyarlar WHERE gayr_id = @id");

            OleDbDataAdapter adp = new OleDbDataAdapter(sb.ToString(), dalManager.Connection());

            adp.SelectCommand.Parameters.AddWithValue("@id", ayarId);

            DataTable dt = new DataTable();

            adp.Fill(dt);

            enGenelAyar ayar = new enGenelAyar();

            if (dt.Rows.Count > 0)
            {
                DataRow rw = dt.Rows[0];

                ayar.Adi = rw["gayr_adi"].ToString();
                ayar.Icerik = rw["gayr_icerik"].ToString();
                ayar.Id = rw["gayr_id"].xToIntDefault();
                ayar.Statu = rw["gayr_statu"].xToBooleanDefault();
            }

            return ayar;
        }

        public void AyarGuncelle(enGenelAyar ayar)
        {
            List<string> prmList = new List<string>();
            List<object> degerList = new List<object>();

            prmList.Add("gayr_icerik"); degerList.Add(ayar.Icerik);
            prmList.Add("gayr_statu"); degerList.Add(ayar.Statu);

            dalManager.MakeAnDbUpdate(prmList, "GenelAyarlar", "gayr_id", ayar.Id, degerList);
        }
    }
}
