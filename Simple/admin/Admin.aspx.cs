using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity;
using BLL;
using Telerik.Web.UI;

namespace Ws.admin
{
    public partial class Admin : System.Web.UI.Page
    {
        public int? VwID
        {
            get
            {
                return ViewState["VwID"].xToInt();
            }
            set
            {
                ViewState["VwID"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AdminleriGetir();
            }
        }

        protected void AdminleriGetir()
        {
            List<enAdmin> adminler = bllAdmin.AdminleriGetir();

            grvKullanicilar.DataSource = adminler;
            grvKullanicilar.DataBind();
        }

        protected void lnkStatuDegistir_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;

            int adminId = lnk.CommandArgument.xToIntDefault();

            enAdmin admin = bllAdmin.AdminGetir(adminId);

            if (admin.Statu)
            {
                admin.Statu = false;
            }
            else
            {
                admin.Statu = true;
            }

            bllAdmin.StatuGuncelle(admin);

            AdminleriGetir();
        }

        protected void btnKaydet_Click(object sender, EventArgs e)
        {
            enAdmin admin = new enAdmin();


            admin.KullaniciAdi = txtKullaniciAdi.Text;
            admin.Sifre = txtSifre.Text;

            if (VwID != null) //düzenleme
            {
                bllAdmin.Guncelle(admin);
            }
            else //yeni kayıt
            {
                enAdmin admKontrol = bllAdmin.AdminGetir(admin.KullaniciAdi);

                if (admKontrol.Id != 0)
                {
                    uscUyari1.UyariGoster("Kullanıcı adı daha önce alınmış !", "Kullanıcı adı kullanımda", false);
                    return;
                }
                try
                {
                    admin.Statu = false;
                    admin.Finex = false;

                    bllAdmin.YeniAdminEkle(admin);

                    uscUyari1.UyariGoster("Kullanıcı başarıyla eklendi.", "Yeni Kullanıcı", true);

                }
                catch
                {
                    uscUyari1.UyariGoster("Kullanıcı eklenirken hata oluştu !", "Yeni Kullanıcı", true);
                }
            }

            txtKullaniciAdi.Text = "";
            txtSifre.Text = "";
            VwID = null;
            
            AdminleriGetir();
        }

        protected void lnkSil_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;

            int adminId = lnk.CommandArgument.xToIntDefault();

            bllAdmin.AdminSil(adminId);

            AdminleriGetir();
        }

        protected void lnkDuzenle_OnClick(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;

            int adminId = lnk.CommandArgument.xToIntDefault();

            enAdmin admin = bllAdmin.AdminGetir(adminId);

            txtKullaniciAdi.Text = admin.KullaniciAdi;
            txtSifre.Text = admin.Sifre;

            VwID = adminId;
        }

        protected void grvKullanicilar_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            List<enAdmin> adminler = bllAdmin.AdminleriGetir();

            grvKullanicilar.DataSource = adminler;
        }
    }
}