using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entity;
using System.Data.OleDb;
using System.Data;

namespace DAL
{
    public class dalAdmin
    {
        public List<enAdmin> AdminleriGetir()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(@"SELECT * FROM Admin");

            OleDbDataAdapter adp = new OleDbDataAdapter(sb.ToString(), dalManager.Connection());

            DataTable dt = new DataTable();

            adp.Fill(dt);

            List<enAdmin> adminler = new List<enAdmin>();

            foreach (DataRow rw in dt.Rows)
            {
                enAdmin admin = new enAdmin();

                admin.KullaniciAdi = rw["adm_kullAdi"].ToString();
                admin.Sifre = rw["adm_sifre"].ToString();
                admin.Id = rw["adm_id"].xToIntDefault();
                admin.Finex = rw["adm_finex"].xToBooleanDefault();
                admin.Statu = rw["adm_statu"].xToBooleanDefault();

                adminler.Add(admin);
            }

            return adminler;
        }

        public enAdmin AdminGetir(string kullaniciAdi, string sifre)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(@"SELECT * FROM Admin WHERE adm_kullAdi = @adi AND adm_sifre = @sifre");

            OleDbDataAdapter adp = new OleDbDataAdapter(sb.ToString(), dalManager.Connection());

            adp.SelectCommand.Parameters.AddWithValue("@adi", kullaniciAdi);
            adp.SelectCommand.Parameters.AddWithValue("@sifre", sifre);

            DataTable dt = new DataTable();

            adp.Fill(dt);

            enAdmin admin = new enAdmin();

            if (dt.Rows.Count > 0)
            {

                DataRow rw = dt.Rows[0];

                admin.KullaniciAdi = rw["adm_kullAdi"].ToString();
                admin.Sifre = rw["adm_sifre"].ToString();
                admin.Id = rw["adm_id"].xToIntDefault();
                admin.Finex = rw["adm_finex"].xToBooleanDefault();
                admin.Statu = rw["adm_statu"].xToBooleanDefault();
            }

            return admin;
        }

        public enAdmin AdminGetir(int adminId)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(@"SELECT * FROM Admin WHERE adm_id = @id ");

            OleDbDataAdapter adp = new OleDbDataAdapter(sb.ToString(), dalManager.Connection());

            adp.SelectCommand.Parameters.AddWithValue("@adi", adminId);

            DataTable dt = new DataTable();

            adp.Fill(dt);

            enAdmin admin = new enAdmin();

            if (dt.Rows.Count > 0)
            {

                DataRow rw = dt.Rows[0];

                admin.KullaniciAdi = rw["adm_kullAdi"].ToString();
                admin.Sifre = rw["adm_sifre"].ToString();
                admin.Id = rw["adm_id"].xToIntDefault();
                admin.Finex = rw["adm_finex"].xToBooleanDefault();
                admin.Statu = rw["adm_statu"].xToBooleanDefault();
            }

            return admin;
        }

        public void YeniAdminEkle(enAdmin admin)
        {
            List<string> prmList = new List<string>();
            List<object> degerList = new List<object>();

            prmList.Add("adm_kullAdi"); degerList.Add(admin.KullaniciAdi);
            prmList.Add("adm_sifre"); degerList.Add(admin.Sifre);
            prmList.Add("adm_finex"); degerList.Add(admin.Finex);
            prmList.Add("adm_statu"); degerList.Add(admin.Statu);


            dalManager.MakeAnDbInsert(prmList, "Admin", degerList, "");
        }

        public void Guncelle(enAdmin admin)
        {
            List<string> prmList = new List<string>();
            List<object> degerList = new List<object>();

            prmList.Add("adm_kullAdi"); degerList.Add(admin.KullaniciAdi);
            prmList.Add("adm_sifre"); degerList.Add(admin.Sifre);

            dalManager.MakeAnDbUpdate(prmList, "Admin", "adm_id", admin.Id, degerList);
        }

        public void StatuGuncelle(enAdmin admin)
        {
            List<string> prmList = new List<string>();
            List<object> degerList = new List<object>();

            prmList.Add("adm_statu"); degerList.Add(admin.Statu);

            dalManager.MakeAnDbUpdate(prmList, "Admin", "adm_id", admin.Id, degerList);
        }

        public void AdminSil(int adminId)
        {
            dalManager.MakeAnDbDelete("Admin", "adm_id", adminId);
        }

        public enAdmin AdminGetir(string kullaniciAdi)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(@"SELECT * FROM Admin WHERE adm_kullAdi = @adi");

            OleDbDataAdapter adp = new OleDbDataAdapter(sb.ToString(), dalManager.Connection());

            adp.SelectCommand.Parameters.AddWithValue("@adi", kullaniciAdi);

            DataTable dt = new DataTable();

            adp.Fill(dt);

            enAdmin admin = new enAdmin();

            if (dt.Rows.Count > 0)
            {

                DataRow rw = dt.Rows[0];

                admin.KullaniciAdi = rw["adm_kullAdi"].ToString();
                admin.Sifre = rw["adm_sifre"].ToString();
                admin.Id = rw["adm_id"].xToIntDefault();
                admin.Finex = rw["adm_finex"].xToBooleanDefault();
                admin.Statu = rw["adm_statu"].xToBooleanDefault();
            }

            return admin;
        }
    }
}
