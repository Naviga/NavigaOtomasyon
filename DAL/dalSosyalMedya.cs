using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Entity;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class dalSosyalMedya
    {


        public void Insert(enSosyalMedya sosyalmedya)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();

            dict.Add("sos_adi", sosyalmedya.sos_adi);
            dict.Add("sos_ikonu", sosyalmedya.sos_ikonu);
            dict.Add("sos_sira", sosyalmedya.sos_sira);
            dict.Add("sos_statu", sosyalmedya.sos_statu);
            dict.Add("sos_url", sosyalmedya.sos_url);

            AccessHelper.Insert("SosyalMedya", dict);
        }

        public DataTable SelectAll()
        {
            return AccessHelper.Select_Dt("SosyalMedya", "*", "", "sos_sira ");
        }

        public DataTable SelectActiveTers()
        {
            return AccessHelper.Select_Dt("SosyalMedya", "*", "sos_statu = " + true, "sos_sira DESC");
        }

        public DataTable SelectActive()
        {
            return AccessHelper.Select_Dt("SosyalMedya", "*", "sos_statu = " + true);
        }

        public void Delete(int sos_id)
        {
            AccessHelper.Delete("SosyalMedya", "sos_id", sos_id);
        }

        public int SonSiraNoGetir()
        {
            return dalManager.SonSiraNoGetir("SosyalMedya", "sos_sira");
        }

        public enSosyalMedya Getir(int sosId)
        {
            DataRow rw = AccessHelper.SelectRow("SosyalMedya", "*", "sos_id = " + sosId);

            enSosyalMedya sos = new enSosyalMedya();

            if (rw != null)
            {
                sos.sos_adi = rw["sos_adi"].ToString();
                sos.sos_id = rw["sos_id"].xToIntDefault();
                sos.sos_ikonu = rw["sos_ikonu"].ToString();
                sos.sos_sira = rw["sos_sira"].xToIntDefault();
                sos.sos_statu = rw["sos_statu"].xToBooleanDefault();
                sos.sos_url = rw["sos_url"].ToString();
                sos.sos_menu = rw["sos_menu"].xToBooleanDefault();
                sos.sos_yanMenu = rw["sos_yanMenu"].xToBooleanDefault();
                sos.sos_footer = rw["sos_footer"].xToBooleanDefault();
            }

            return sos;
        }

        public void StatuGuncelle(int sosId, bool statu)
        {
            Dictionary<string, object> pair = new Dictionary<string, object>();

            pair.Add("sos_statu", statu);

            AccessHelper.Update("SosyalMedya", pair, "sos_id", sosId);
        }

        public void SiraGuncelle(int sosId, int sira)
        {
            Dictionary<string, object> pair = new Dictionary<string, object>();

            pair.Add("sos_sira", sira);

            AccessHelper.Update("SosyalMedya", pair, "sos_id", sosId);
        }

        public enSosyalMedya BirAlttaki(int sira)
        {
            DataTable dt = AccessHelper.Select_Dt("SosyalMedya", "*", "sos_sira = " + (sira + 1));

            enSosyalMedya sos = new enSosyalMedya();
            if (dt.Rows.Count > 0)
            {
                DataRow rw = dt.Rows[0];

                sos.sos_adi = rw["sos_adi"].ToString();
                sos.sos_id = rw["sos_id"].xToIntDefault();
                sos.sos_ikonu = rw["sos_ikonu"].ToString();
                sos.sos_sira = rw["sos_sira"].xToIntDefault();
                sos.sos_statu = rw["sos_statu"].xToBooleanDefault();
                sos.sos_url = rw["sos_url"].ToString();
                sos.sos_menu = rw["sos_menu"].xToBooleanDefault();
                sos.sos_yanMenu = rw["sos_yanMenu"].xToBooleanDefault();
                sos.sos_footer = rw["sos_footer"].xToBooleanDefault();
            }

            return sos;
        }

        public enSosyalMedya BirUstteki(int sira)
        {
            DataTable dt = AccessHelper.Select_Dt("SosyalMedya", "*", "sos_sira = " + (sira - 1));

            enSosyalMedya sos = new enSosyalMedya();
            if (dt.Rows.Count > 0)
            {
                DataRow rw = dt.Rows[0];

                sos.sos_adi = rw["sos_adi"].ToString();
                sos.sos_id = rw["sos_id"].xToIntDefault();
                sos.sos_ikonu = rw["sos_ikonu"].ToString();
                sos.sos_sira = rw["sos_sira"].xToIntDefault();
                sos.sos_statu = rw["sos_statu"].xToBooleanDefault();
                sos.sos_url = rw["sos_url"].ToString();
                sos.sos_menu = rw["sos_menu"].xToBooleanDefault();
                sos.sos_yanMenu = rw["sos_yanMenu"].xToBooleanDefault();
                sos.sos_footer = rw["sos_footer"].xToBooleanDefault();
            }

            return sos;
        }

        public void Guncelle(enSosyalMedya media)
        {
            Dictionary<string, object> pair = new Dictionary<string, object>();

            pair.Add("sos_adi", media.sos_adi);
            pair.Add("sos_ikonu", media.sos_ikonu);
            pair.Add("sos_url", media.sos_url);

            AccessHelper.Update("SosyalMedya", pair, "sos_id", media.sos_id);
        }

        public List<enSosyalMedya> TumunuGetir()
        {
            DataTable dt = AccessHelper.Select_Dt("SosyalMedya", "*", "", "sos_sira ");

            List<enSosyalMedya> medyalar = new List<enSosyalMedya>();
            foreach (DataRow rw in dt.Rows)
            {
                enSosyalMedya sos = new enSosyalMedya();

                sos.sos_adi = rw["sos_adi"].ToString();
                sos.sos_id = rw["sos_id"].xToIntDefault();
                sos.sos_ikonu = rw["sos_ikonu"].ToString();
                sos.sos_sira = rw["sos_sira"].xToIntDefault();
                sos.sos_statu = rw["sos_statu"].xToBooleanDefault();
                sos.sos_url = rw["sos_url"].ToString();
                sos.sos_menu = rw["sos_menu"].xToBooleanDefault();
                sos.sos_yanMenu = rw["sos_yanMenu"].xToBooleanDefault();
                sos.sos_footer = rw["sos_footer"].xToBooleanDefault();

                medyalar.Add(sos);
            }

            return medyalar;
        }

        public List<enSosyalMedya> AktifleriGetir()
        {
            DataTable dt = AccessHelper.Select_Dt("SosyalMedya", "*", "sos_statu = " + true);

            List<enSosyalMedya> medyalar = new List<enSosyalMedya>();
            foreach (DataRow rw in dt.Rows)
            {
                enSosyalMedya sos = new enSosyalMedya();

                sos.sos_adi = rw["sos_adi"].ToString();
                sos.sos_id = rw["sos_id"].xToIntDefault();
                sos.sos_ikonu = rw["sos_ikonu"].ToString();
                sos.sos_sira = rw["sos_sira"].xToIntDefault();
                sos.sos_statu = rw["sos_statu"].xToBooleanDefault();
                sos.sos_url = rw["sos_url"].ToString();
                sos.sos_menu = rw["sos_menu"].xToBooleanDefault();
                sos.sos_yanMenu = rw["sos_yanMenu"].xToBooleanDefault();
                sos.sos_footer = rw["sos_footer"].xToBooleanDefault();

                medyalar.Add(sos);
            }

            return medyalar;
        }

        public void MenudeGoster(enSosyalMedya sos)
        {
            Dictionary<string, object> pair = new Dictionary<string, object>();

            pair.Add("sos_menu", sos.sos_menu);

            AccessHelper.Update("SosyalMedya", pair, "sos_id", sos.sos_id);
        }

        public void FooterdaGoster(enSosyalMedya sos)
        {
            Dictionary<string, object> pair = new Dictionary<string, object>();

            pair.Add("sos_footer", sos.sos_menu);

            AccessHelper.Update("SosyalMedya", pair, "sos_id", sos.sos_id);
        }

        public void YanMenudeGoster(enSosyalMedya sos)
        {
            Dictionary<string, object> pair = new Dictionary<string, object>();

            pair.Add("sos_yanMenu", sos.sos_yanMenu);

            AccessHelper.Update("SosyalMedya", pair, "sos_id", sos.sos_id);
        }
    }
}
