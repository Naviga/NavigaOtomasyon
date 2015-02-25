using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entity;
using System.Data.OleDb;
using System.Data;

namespace DAL
{
    public class dalCssDosyalari
    {
        public void YeniEkle(enCssDosya css)
        {
            List<string> prmList = new List<string>();
            List<object> degerList = new List<object>();

            prmList.Add("css_adi"); degerList.Add(css.Adi);
            prmList.Add("css_sbl_id"); degerList.Add(css.SablonId);

            dalManager.MakeAnDbInsert(prmList, "CssDosyalari", degerList, "");
        }

        public List<enCssDosya> CssDosyalariGetir(int? sablonId)
        {
            StringBuilder sb = new StringBuilder();
            OleDbDataAdapter adp = new OleDbDataAdapter("", dalManager.Connection());

            if (sablonId != null)
            {
                sb.Append("SELECT * FROM CssDosyalari WHERE css_sbl_id = @id");
                
                adp.SelectCommand.Parameters.AddWithValue("@id", sablonId);
            }
            else
            {
                sb.Append("SELECT * FROM CssDosyalari");
            }

            adp.SelectCommand.CommandText = sb.ToString();

            DataTable dt = new DataTable();

            adp.Fill(dt);

            List<enCssDosya> CssDosyalari = new List<enCssDosya>();
            foreach (DataRow rw in dt.Rows)
            {
                enCssDosya css = new enCssDosya();

                css.Id = rw["css_id"].xToIntDefault();
                css.Adi = rw["css_adi"].ToString();
                css.SablonId = rw["css_sbl_id"].xToIntDefault();

                CssDosyalari.Add(css);
            }

            return CssDosyalari;
        }

        public enCssDosya CssGetir(int cssId)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT * FROM CssDosyalari WHERE css_id = @id");


            OleDbDataAdapter adp = new OleDbDataAdapter(sb.ToString(), dalManager.Connection());

            adp.SelectCommand.Parameters.AddWithValue("@id", cssId);

            DataTable dt = new DataTable();

            adp.Fill(dt);

            enCssDosya css = new enCssDosya();

            if (dt.Rows.Count > 0)
            {
                DataRow rw = dt.Rows[0];

                css.Id = rw["css_id"].xToIntDefault();
                css.Adi = rw["css_adi"].ToString();
                css.SablonId = rw["css_sbl_id"].xToIntDefault();

            }

            return css;
        }

        public void Sil(int cssId)
        {
            dalManager.MakeAnDbDelete("CssDosyalari", "css_id", cssId);
        }
    }
}
