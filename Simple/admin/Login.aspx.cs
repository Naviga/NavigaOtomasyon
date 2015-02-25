using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using Entity;
using BLL;

namespace Ws.admin
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string hostName = Request.Url.Host;

            //if (hostName.Contains("www."))
            //{
            //    hostName = hostName.Replace("www", "mail").Replace("http://", "");
            //}
            //else
            //{
            //    hostName = "mail." + hostName;
            //}

            //lblHostTest.Text = hostName;
        }

        protected void btnGiris_Click(object sender, EventArgs e)
        {
            string kullaniciAdi = txtKullaniciAdi.Text.Trim();
            string sifre = txtSifre.Text.Trim();

            enAdmin adm = bllAdmin.AdminGetir(kullaniciAdi, sifre);

            if (adm.Id != 0 && adm.Statu)
            {
                SessionManager.Admin = adm;

                Response.Redirect("/");
            }
            else
            {
                lbluyari.Text = "Kullanıcı adı veya şifre yanlış.";
            }
        }
    }
}