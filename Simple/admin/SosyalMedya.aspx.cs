using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit.HTMLEditor.ToolbarButton;
using BLL;
using Entity;
using Telerik.Web.UI;

namespace Ws.admin
{
    public partial class SosyalMedya : System.Web.UI.Page
    {
        public List<string> VwResimler
        {
            get
            {
                if (ViewState["VwResimler"] == null)
                    ViewState["VwResimler"] = new List<string>();
                return ViewState["VwResimler"] as List<string>;
            }
            set
            {
                ViewState["VwResimler"] = value;
            }
        }

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
                SosyalMedyalariGetir();
            }

            foreach (UploadedFile file in rauSosyalMedya.UploadedFiles)
            {
                VwResimler.Add(rauSosyalMedya.TargetFolder + file.FileName);
            }
        }

        protected void SosyalMedyalariGetir()
        {
            List<enSosyalMedya> hesaplar = bllSosyalMedya.TumunuGetir();

            rgrvSosyalMedya.DataSource = hesaplar;
            rgrvSosyalMedya.DataBind();
        }

        protected void lnkStatuDegistir_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;

            int sosId = lnk.CommandArgument.xToIntDefault();

            enSosyalMedya sos = bllSosyalMedya.Getir(sosId);

            if (sos.sos_statu)
            {
                sos.sos_statu = false;
            }
            else
            {
                sos.sos_statu = true;
            }

            bllSosyalMedya.StatuGuncelle(sos.sos_id, sos.sos_statu);

            SosyalMedyalariGetir();
        }

        protected void lnkSil_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;

            int sosId = lnk.CommandArgument.xToIntDefault();

            bllSosyalMedya.Delete(sosId);

            SosyalMedyalariGetir();
        }

        protected void lnkDuzenle_OnClick(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;

            int sosId = lnk.CommandArgument.xToIntDefault();

            enSosyalMedya sos = bllSosyalMedya.Getir(sosId);

            txtSosyalAdi.Text = sos.sos_adi;
            txtSosyalAdres.Text = sos.sos_url;
            if (!sos.sos_ikonu.xBosMu())
            {
                ltrIkon.Text = "<img src='" + sos.sos_ikonu + "' />";
            }

            VwID = sosId;
        }

        protected void grvKullanicilar_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            List<enSosyalMedya> sosler = bllSosyalMedya.TumunuGetir();

            rgrvSosyalMedya.DataSource = sosler;
        }

        protected void btnSosyalMedya_OnClick(object sender, EventArgs e)
        {
            enSosyalMedya medya = new enSosyalMedya();

            medya.sos_adi = txtSosyalAdi.Text;

            if (VwResimler.Count > 0)
            {
                medya.sos_ikonu = VwResimler[0].Replace("~", "");
            }

            medya.sos_sira = bllSosyalMedya.SonSiraNoGetir() + 1;
            medya.sos_url = txtSosyalAdres.Text;

            if (VwID != null) //düzenleme
            {
                medya.sos_id = VwID.Value;

                bllSosyalMedya.Guncelle(medya);
            }
            else //yeni kayıt
            {
                try
                {
                    medya.sos_statu = false;

                    bllSosyalMedya.Insert(medya);

                    uscUyari1.UyariGoster("Hesap başarıyla eklendi.", "Yeni Hesap", true);

                }
                catch
                {
                    uscUyari1.UyariGoster("Hesap eklenirken hata oluştu !", "Yeni Hesap", true);
                }
            }

            txtSosyalAdi.Text = "";
            txtSosyalAdres.Text = "";
            VwID = null;
            ltrIkon.Text = "";
            VwResimler.Clear();

            SosyalMedyalariGetir();
        }

        protected void lnkMenudeGoster_OnClick(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;

            int sosId = lnk.CommandArgument.xToIntDefault();

            enSosyalMedya sos = bllSosyalMedya.Getir(sosId);

            if (sos.sos_menu)
            {
                sos.sos_menu = false;
            }
            else
            {
                sos.sos_menu = true;
            }

            bllSosyalMedya.MenudeGoster(sos);

            SosyalMedyalariGetir();
        }

        protected void lnkYanMenudeGoster_OnClick(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;

            int sosId = lnk.CommandArgument.xToIntDefault();

            enSosyalMedya sos = bllSosyalMedya.Getir(sosId);

            if (sos.sos_yanMenu)
            {
                sos.sos_yanMenu = false;
            }
            else
            {
                sos.sos_yanMenu = true;
            }

            bllSosyalMedya.YanMenudeGoster(sos);

            SosyalMedyalariGetir();
        }

        protected void lnkFooterdaGoster_OnClick(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;

            int sosId = lnk.CommandArgument.xToIntDefault();

            enSosyalMedya sos = bllSosyalMedya.Getir(sosId);

            if (sos.sos_footer)
            {
                sos.sos_footer = false;
            }
            else
            {
                sos.sos_footer = true;
            }

            bllSosyalMedya.FooterdaGoster(sos);

            SosyalMedyalariGetir();
        }
    }
}