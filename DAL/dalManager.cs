using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Text;
using System.Data.OleDb;



namespace DAL
{
    public static class dalManager
    {
        public static OleDbConnection Connection()
        {
            OleDbConnection con = new OleDbConnection(ConfigurationManager.ConnectionStrings["AccConnectionString"].ToString());

            return con;
        }

        /// <summary>
        /// Kolon isimleri, tablo adi ve insert edilecek degerler gonderilerek veritabanina yeni bir kayit eklenir
        /// </summary>
        /// <param name="parametreler"></param>
        /// <param name="tabloAdi"></param>
        /// <param name="degerler"></param>
        public static int MakeAnDbInsert(List<string> parametreler, string tabloAdi, List<object> degerler, string identityColumn)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("INSERT INTO " + tabloAdi + " (");

            for (int i = 0; i < parametreler.Count; i++)
            {
                if (i == 0)
                {
                    sb.Append(parametreler[i]);
                }
                else
                {
                    sb.Append("," + parametreler[i]);
                }
            }

            sb.Append(")VALUES(");

            for (int i = 0; i < parametreler.Count; i++)
            {
                if (i == 0)
                {
                    sb.Append("@" + parametreler[i]);
                }
                else
                {
                    sb.Append("," + "@" + parametreler[i]);
                }
            }

            sb.Append(")");

            OleDbCommand cmd = new OleDbCommand(sb.ToString(), Connection());

            for (int i = 0; i < parametreler.Count; i++)
            {
                cmd.Parameters.AddWithValue("@" + parametreler[i], degerler[i] == null ? DBNull.Value : degerler[i]);
            }

            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();

            return GetIdentity(tabloAdi, identityColumn);
        }
        /// <summary>
        /// Tablo kolon isimleri, tablo adı, where kosulunda kullanılacak kolon ismi ve degeri, son olarak kolonlara kaydedilecek degerler gonderilerek 
        /// veri tabaninda kayit guncelleme islemi yapilir
        /// </summary>
        /// <param name="parametreler"></param>
        /// <param name="tabloAdi"></param>
        /// <param name="kosulAlan"></param>
        /// <param name="kosulAlanDegeri"></param>
        /// <param name="degerler"></param>
        public static void MakeAnDbUpdate(List<string> parametreler, string tabloAdi, string kosulAlan, object kosulAlanDegeri, List<object> degerler)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("UPDATE " + tabloAdi + " SET ");

            for (int i = 0; i < parametreler.Count; i++)
            {
                if (i == 0)
                {
                    sb.Append(parametreler[i] + " = " + "@" + parametreler[i]);
                }
                else
                {
                    sb.Append("," + parametreler[i] + " = " + "@" + parametreler[i]);
                }
            }

            sb.Append(" WHERE " + kosulAlan + " = @" + kosulAlan);

            OleDbCommand cmd = new OleDbCommand(sb.ToString(), Connection());

            for (int i = 0; i < parametreler.Count; i++)
            {
                cmd.Parameters.AddWithValue("@" + parametreler[i], degerler[i] == null ? DBNull.Value : degerler[i]);
            }

            cmd.Parameters.AddWithValue("@" + kosulAlan, kosulAlanDegeri);

            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        /// <summary>
        /// Belirtilen kosulda ve degerde tablodan kayit siler.
        /// Dikkat : Kosul belirtilmezse tum tabloyu siler !
        /// </summary>
        /// <param name="tabloAdi"></param>
        /// <param name="parametre"></param>
        /// <param name="id"></param>
        public static void MakeAnDbDelete(string tabloAdi, string parametre, object id)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(@"DELETE FROM " + tabloAdi);

            if (id != null)
            {
                sb.Append(" WHERE " + parametre + "= @" + parametre);
            }

            OleDbCommand cmd = new OleDbCommand(sb.ToString(), Connection());

            if (id != null)
            {
                cmd.Parameters.AddWithValue("@" + parametre, id);
            }

            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        public static int GetIdentity(string tabloadi, string identityColumn)
        {
            if (String.IsNullOrEmpty(identityColumn))
            {
                return 0;
            }

            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT TOP 1 " + identityColumn + " FROM " + tabloadi + " ORDER BY " + identityColumn + " DESC ");

            OleDbCommand cmd = new OleDbCommand(sb.ToString(), Connection());

            cmd.Connection.Open();
            int Id = cmd.ExecuteScalar().xToIntDefault();
            cmd.Connection.Close();

            return Id;
        }

        public static int SonSiraNoGetir(string tabloAdi, string kolonAdi)
        {
            OleDbCommand cmd = new OleDbCommand("SELECT " + kolonAdi + " FROM " + tabloAdi + " ORDER BY " + kolonAdi + " DESC ", Connection());

            cmd.Connection.Open();

            int siraNo = cmd.ExecuteScalar().xToIntDefault();

            cmd.Connection.Close();

            return siraNo;
        }
    }
}