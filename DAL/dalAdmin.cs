﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entity;
using MySql.Data.MySqlClient;
using System.Data;

namespace DAL
{
    public class dalAdmin
    {
        public List<enAdmin> AdminleriGetir()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(@"SELECT * FROM Admin");

            MySqlDataAdapter adp = new MySqlDataAdapter(sb.ToString(), FxMySqlHelper.Connection());

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

            MySqlDataAdapter adp = new MySqlDataAdapter(sb.ToString(), FxMySqlHelper.Connection());

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

            MySqlDataAdapter adp = new MySqlDataAdapter(sb.ToString(), FxMySqlHelper.Connection());

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
            Dictionary<string, object> dict = new Dictionary<string, object>();

            dict.Add("adm_kullAdi", admin.KullaniciAdi);
            dict.Add("adm_sifre", admin.Sifre);
            dict.Add("adm_finex", admin.Finex);
            dict.Add("adm_statu", admin.Statu);

            FxMySqlHelper.Insert("Admin",dict);
        }

        public void Guncelle(enAdmin admin)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();

            dict.Add("adm_kullAdi", admin.KullaniciAdi);
            dict.Add("adm_sifre", admin.Sifre);

            FxMySqlHelper.Update("Admin", dict, "adm_id", admin.Id);
        }

        public void StatuGuncelle(enAdmin admin)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();

            dict.Add("adm_statu", admin.Statu);

            FxMySqlHelper.Update("Admin", dict, "adm_id", admin.Id);
        }

        public void AdminSil(int adminId)
        {
            FxMySqlHelper.Delete("Admin", "adm_id", adminId);
        }

        public enAdmin AdminGetir(string kullaniciAdi)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(@"SELECT * FROM Admin WHERE adm_kullAdi = @adi");

            MySqlDataAdapter adp = new MySqlDataAdapter(sb.ToString(), FxMySqlHelper.Connection());

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
