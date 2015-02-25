using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity;
using BLL;
using Telerik.Web.UI;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace Ws.admin
{
    public partial class Bloklar : System.Web.UI.Page
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
                BloklariGetir();
                SayfalariDoldur();
                CarouselleriDoldur();

                if (Request.QueryString["id"] != null)
                {
                    VwID = Request.QueryString["id"].xToInt();

                    RadTabStrip1.SelectedIndex = 1;
                    RadMultiPage1.SelectedIndex = 1;
                    RadTabStrip1.Tabs[1].Text = "Düzenle";

                    btnNewBlokClear.Visible = true;

                    BlokGetir();
                }


            }

        }

        private void BlokGetir()
        {
            enBlok blok = bllBloklar.BlokGetir(VwID.Value);

            txtYeniBlokAdi.Text = blok.Adi;
            txtYeniBlokAciklama.Text = blok.Aciklama;
            edtIcerik.Content = blok.Icerik;

            if (blok.SayfaId != null)
            {
                enSiteHaritasi sayfa = bllSiteHaritasi.SayfaGetir(blok.SayfaId.Value);

                lnkSayfaSec.Text = sayfa.Adi + " sayfası seçildi.";
                try
                {
                    trvSayfalar.FindNodeByValue(blok.SayfaId.ToString()).Selected = true;
                }
                catch
                {
                }
                hdnSayfaID.Value = blok.SayfaId.ToString();
            }

            if (blok.CarouselId != null)
            {
                enCarousel carousel = bllCarousel.Getir(blok.CarouselId.Value);

                lnkCarouselSec.Text = carousel.Adi + " seçildi.";

                hdnCarouselID.Value = blok.CarouselId.ToString();
            }

            if (blok.Default)
            {
                trSayfaListeleSec.Visible = false;
                trSayfaListeleHr.Visible = false;
            }
            else
            {
                trSayfaListeleSec.Visible = true;
                trSayfaListeleHr.Visible = true;
            }
        }

        protected void BloklariGetir()
        {
            rgrvBloklar.DataSource = bllBloklar.BloklariGetir(null);
            rgrvBloklar.DataBind();
        }

        protected void lnkStatuDegistir_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;

            int blokId = lnk.CommandArgument.xToIntDefault();

            enBlok blok = bllBloklar.BlokGetir(blokId);

            if (blok.Statu)
            {
                blok.Statu = false;
            }
            else
            {
                blok.Statu = true;
            }

            bllBloklar.BlokStatuGuncelle(blok);

            BloklariGetir();
        }

        protected void btnYeniBlokKaydet_Click(object sender, EventArgs e)
        {

            if (txtYeniBlokAdi.Text.xBosMu())
            {
                return;
            }

            enBlok blok = new enBlok();

            blok.Adi = txtYeniBlokAdi.Text;
            blok.Icerik = edtIcerik.Content;
            blok.SayfaId = hdnSayfaID.Value.xToInt();
            blok.Aciklama = txtYeniBlokAciklama.Text;
            blok.CarouselId = hdnCarouselID.Value.xToInt();

            if (VwID == null)
            {
                blok.Statu = true;
                blok.CerceveKullanimi = true;
                blok.BaslikKullanimi = true;

                bllBloklar.YeniBlokEkle(blok);
            }
            else
            {
                blok.Id = VwID.Value;
                bllBloklar.BlokGuncelle(blok);
            }

            Response.Redirect("Bloklar.aspx");
        }

        protected void lnkDuzenle_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;

            int blokId = lnk.CommandArgument.xToIntDefault();

            Response.Redirect("~/admin/BlokDuzenle.aspx?id=" + blokId);
        }

        protected void lnkSil_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;

            int blokId = lnk.CommandArgument.xToIntDefault();

            enBlok blok = bllBloklar.BlokGetir(blokId);

            bllBloklar.BlokSil(blokId);

            BloklariGetir();
        }

        protected void btnNewBlokClear_Click(object sender, EventArgs e)
        {
            KontrolleriSifirla();

            RadTabStrip1.Tabs[1].Text = "+ Yeni Blok";
            RadTabStrip1.SelectedIndex = 1;
            RadMultiPage1.SelectedIndex = 1;
        }

        private void KontrolleriSifirla()
        {
            VwResimler.Clear();
            VwID = null;

            txtYeniBlokAdi.Text = "";

            edtIcerik.Content = "";

            btnNewBlokClear.Visible = false;
            RadTabStrip1.Tabs[1].Text = "(+) Yeni Blok";

            //hdnCarouselID.Value = "";
            //hdnID.Value = "";
            //hdnSayfaID.Value = "";
            //hdnSlaytID.Value = "";
        }

        protected void SayfalariDoldur()
        {
            List<enSiteHaritasi> sayfalar = bllSiteHaritasi.TumSayfalariGetir();

            trvSayfalar.DataTextField = "Adi";
            trvSayfalar.DataValueField = "Id";
            trvSayfalar.DataFieldID = "Id";
            trvSayfalar.DataFieldParentID = "Parent";
            trvSayfalar.DataSource = sayfalar;
            trvSayfalar.DataBind();

        }

        protected void CarouselleriDoldur()
        {
            List<enCarousel> carouseller = bllCarousel.CarouselleriGetir(true);

            rgrvCarousel.DataSource = carouseller;
            rgrvCarousel.DataBind();
        }

        protected void btnSayfaKaydet_Click(object sender, EventArgs e)
        {
            if (trvSayfalar.SelectedValue.xBosMu() == false)
            {
                hdnSayfaID.Value = trvSayfalar.SelectedValue;

                enSiteHaritasi sayfa = bllSiteHaritasi.SayfaGetir(trvSayfalar.SelectedValue.xToIntDefault());

                lnkSayfaSec.Text = sayfa.Adi + " sayfası seçildi.";

                hdnCarouselID.Value = "";
                lnkCarouselSec.Text = "Seç";
            }
        }

        protected void btnCarouselSec_OnClick(object sender, EventArgs e)
        {
            if (rgrvCarousel.SelectedValue != null)
            {
                int carouselId = rgrvCarousel.SelectedValue.xToIntDefault();

                hdnCarouselID.Value = carouselId.ToString();

                enCarousel carousel = bllCarousel.Getir(carouselId);

                lnkCarouselSec.Text = carousel.Adi + " carouseli seçildi.";

                hdnSayfaID.Value = "";
                lnkSayfaSec.Text = "";
            }
        }

        protected void rgrvBloklar_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgrvBloklar.DataSource = bllBloklar.BloklariGetir(null);
        }
    }
}