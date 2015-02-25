using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using Entity;
using BLL;
using System.IO;
using Telerik.Web.UI;
using Subgurim.Controles;
using System.Drawing;
using System.Web.UI.HtmlControls;


namespace Ws.admin
{
    public partial class Ayarlar : System.Web.UI.Page
    {
        public class Resim
        {
            public string Adi { get; set; }
        }

        public class Template
        {
            public string FileName { get; set; }
            public string DisplayName { get; set; }
            public string ImageThumb { get; set; }
            public string ImageBig { get; set; }
        }

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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CssDosyalariniGetir(bllTasarimAyarlari.TasarimAyariGetir(enEnumaration.enmTasarimAyarlari.GenelSablon).Degeri.xToIntDefault());
                GenelAyarlariGetir();
                TasarimAyarlariniGetir();
                GmapGetir();
                SablonlariGetir();
                LisansBilgileriniYaz();

                if (SessionManager.Admin.Finex)
                {
                    GetDatabaseTables_Access();
                    GetColumnNames_Access(ddlTables.SelectedValue);
                }

                RadTabStrip1.Tabs[4].Visible = SessionManager.Admin.Finex;

                if (!Request.QueryString["i"].xBosMu())
                {
                    int tabIndexToShow = Request.QueryString["i"].xToIntDefault();

                    TablariGoster(tabIndexToShow);
                }



                //DatabaseIsmiGetir();

            }

            foreach (UploadedFile file in uplSolUst.UploadedFiles)
            {
                VwResimler.Add(file.FileName);
            }

            foreach (UploadedFile file in uplArkaPlan.UploadedFiles)
            {
                VwResimler.Add(file.FileName);
            }
        }

        protected void TablariGoster(int? index = null)
        {
            if (index != null)
            {
                RadTabStrip1.Tabs[0].Visible = false;
                RadTabStrip1.Tabs[1].Visible = false;
                RadTabStrip1.Tabs[2].Visible = false;
                RadTabStrip1.Tabs[3].Visible = false;

                RadTabStrip1.Tabs[index.Value].Visible = true;

                RadMultiPage1.SelectedIndex = index.Value;
                RadTabStrip1.SelectedIndex = 1;
            }
            else
            {
                RadTabStrip1.Tabs[0].Visible = true;
                RadTabStrip1.Tabs[1].Visible = true;
                RadTabStrip1.Tabs[2].Visible = true;
                RadTabStrip1.Tabs[3].Visible = true;
            }
        }

        protected void GenelAyarlariGetir()
        {
            List<enGenelAyar> ayarlar = bllGenelAyarlar.GenelAyarlariGetir();

            foreach (enGenelAyar ayar in ayarlar)
            {
                if (ayar.Id == enEnumaration.enmGenelAyarlar.AcilirMenuKullanimi)
                {
                    //chkAcilirMenu.Checked = ayar.Statu;
                }

                if (ayar.Id == enEnumaration.enmGenelAyarlar.UrunKategoriSistemi)
                {
                    //chkKategoriUrun.Checked = ayar.Statu;
                }

                if (ayar.Id == enEnumaration.enmGenelAyarlar.KatalogListeGorunumu)
                {
                    //chkKatalogGorunumu.Checked = ayar.Statu;
                }

                if (ayar.Id == enEnumaration.enmGenelAyarlar.IletisimFormuKullanimi)
                {
                    hdnIletisimFormu.Value = ayar.Statu.ToString();
                }

                //if (ayar.Id == enEnumaration.enmGenelAyarlar.SiteIciArama)
                //{
                //    hdnSiteIciArama.Value = ayar.Statu.ToString();
                //}

                if (ayar.Id == enEnumaration.enmGenelAyarlar.GmapKullanimi)
                {
                    hdnGoogleMap.Value = ayar.Icerik.xToBooleanDefault().ToString();
                }

                if (ayar.Id == enEnumaration.enmGenelAyarlar.ReferansKatalogListe)
                {
                    //chkReferansKatalog.Checked = ayar.Statu;
                }

                if (ayar.Id == enEnumaration.enmGenelAyarlar.IletisimFormuEposta)
                {
                    txtIletisimEposta.Text = ayar.Icerik;
                }

                if (ayar.Id == enEnumaration.enmGenelAyarlar.MailSunucu)
                {
                    txtMailSunucu.Text = ayar.Icerik;
                }

                //if (ayar.Id == enEnumaration.enmGenelAyarlar.SliderKullanimi)
                //{
                //    chkSlider.Checked = ayar.Icerik.xToBooleanDefault();
                //}
            }
        }

        protected void TasarimAyarlariniGetir()
        {
            List<enTasarimAyar> ayarlar = bllTasarimAyarlari.TasarimAyarlariniGetir();

            foreach (enTasarimAyar ayar in ayarlar)
            {

                if (ayar.Id == enEnumaration.enmTasarimAyarlari.ArkaPlanRengi)
                {
                    if (!string.IsNullOrEmpty(ayar.Degeri))
                    {
                        rcpArkaPlan.SelectedColor = ColorTranslator.FromHtml(ayar.Degeri);
                    }
                }

                if (ayar.Id == enEnumaration.enmTasarimAyarlari.YaziRengi)
                {
                    if (!string.IsNullOrEmpty(ayar.Degeri))
                    {
                        rcpYaziRengi.SelectedColor = ColorTranslator.FromHtml(ayar.Degeri);
                    }
                }

                if (ayar.Id == enEnumaration.enmTasarimAyarlari.BaslikRengi)
                {
                    if (!string.IsNullOrEmpty(ayar.Degeri))
                    {
                        rcpBaslikRengi.SelectedColor = ColorTranslator.FromHtml(ayar.Degeri);
                    }
                }

                if (ayar.Id == enEnumaration.enmTasarimAyarlari.BaglantiRengi)
                {
                    if (!string.IsNullOrEmpty(ayar.Degeri))
                    {
                        rcpBaglantiRengi1.SelectedColor = ColorTranslator.FromHtml(ayar.Degeri);
                    }
                }

                if (ayar.Id == enEnumaration.enmTasarimAyarlari.BaglantiRengi2)
                {
                    if (!string.IsNullOrEmpty(ayar.Degeri))
                    {
                        rcpBaglantiRengi2.SelectedColor = ColorTranslator.FromHtml(ayar.Degeri);
                    }
                }

                if (ayar.Id == enEnumaration.enmTasarimAyarlari.MenuArkaPlanRengi)
                {
                    if (!string.IsNullOrEmpty(ayar.Degeri))
                    {
                        rcpMenuArkaPlan.SelectedColor = ColorTranslator.FromHtml(ayar.Degeri);
                    }
                }

                if (ayar.Id == enEnumaration.enmTasarimAyarlari.MenuYuksekligi)
                {
                    if (!string.IsNullOrEmpty(ayar.Degeri))
                    {
                        rsMenuYuksekligi.Value = ayar.Degeri.Replace("px", "").xToDecimalDefault();
                        lblMenuYuksekligi.Text = ayar.Degeri.xBosMu() ? "varsayılan" : ayar.Degeri;
                    }
                }

                if (ayar.Id == enEnumaration.enmTasarimAyarlari.AltBolumRengi)
                {
                    if (!string.IsNullOrEmpty(ayar.Degeri))
                    {
                        rcpFooter.SelectedColor = ColorTranslator.FromHtml(ayar.Degeri);
                    }
                }

                //if (ayar.Id == enEnumaration.enmTasarimAyarlari.BlokArkaPlanRengi)
                //{
                //    if (!string.IsNullOrEmpty(ayar.Degeri))
                //    {
                //        rcpBlokArkaPlan.SelectedColor = ColorTranslator.FromHtml(ayar.Degeri);
                //    }
                //}

                //if (ayar.Id == enEnumaration.enmTasarimAyarlari.BlokCerceveRengi)
                //{
                //    if (!string.IsNullOrEmpty(ayar.Degeri))
                //    {
                //        rcpBlokCerceve.SelectedColor = ColorTranslator.FromHtml(ayar.Degeri);
                //    }
                //}

                if (ayar.Id == enEnumaration.enmTasarimAyarlari.GenelSablon)
                {
                    CssDosyalariniGetir(ayar.Degeri.xToIntDefault());
                }

                if (ayar.Id == enEnumaration.enmTasarimAyarlari.MetinFontu)
                {
                    if (!string.IsNullOrEmpty(ayar.Degeri))
                    {
                        lblMetinFontu.Font.Name = ayar.Degeri;
                    }
                }

                if (ayar.Id == enEnumaration.enmTasarimAyarlari.BaslikFontu)
                {
                    if (!string.IsNullOrEmpty(ayar.Degeri))
                    {
                        lblBaslikFontu.Font.Name = ayar.Degeri;
                    }
                }

            }
        }

        protected void CssDosyalariniGetir(int sablonId)
        {
            List<Template> templates = new List<Template>();

            string[] arrTemps = Directory.GetFiles(Server.MapPath("~/css/templates/"));

            foreach (string item in arrTemps)
            {
                string fileName = item.Split('\\')[item.Split('\\').Length - 1];

                string dispName = fileName.Replace(".css", "").Replace("-", " ");

                Template temp = new Template();

                temp.DisplayName = dispName;
                temp.FileName = fileName;
                temp.ImageBig = "/css/templates/preview/big/" + fileName.Replace(".css", ".jpg");
                temp.ImageThumb = "/css/templates/preview/thumb/" + fileName.Replace(".css", ".jpg");

                templates.Add(temp);
            }

            rptTemplates.DataSource = templates;
            rptTemplates.DataBind();


            //List<enCssDosya> dosyalar = bllCssDosyalari.CssDosyalariGetir(sablonId);

            //ddlSiteSablonlari.Items.Clear();

            //foreach (enCssDosya dosya in dosyalar)
            //{
            //    ddlSiteSablonlari.Items.Add(new ListItem(dosya.Adi.Replace(".css", ""), dosya.Id.ToString()));
            //}

            //ddlSiteSablonlari.Items.Insert(0, new ListItem("Seçiniz", ""));

            //enTasarimAyar ayr = bllTasarimAyarlari.TasarimAyariGetir(enEnumaration.enmTasarimAyarlari.Sablon);

            //try
            //{
            //    ddlSiteSablonlari.ClearSelection();
            //    ddlSiteSablonlari.Items.FindByValue(ayr.Degeri).Selected = true;
            //}
            //catch
            //{

            //}
        }

        protected void SablonlariGetir()
        {
            enTasarimAyar ayrSablon = bllTasarimAyarlari.TasarimAyariGetir(enEnumaration.enmTasarimAyarlari.GenelSablon);

            List<enSablon> sablonlar = bllSablonlar.SablonlariGetir();

            rptSablonlar.DataSource = sablonlar;
            rptSablonlar.DataBind();

            foreach (RepeaterItem item in rptSablonlar.Items)
            {
                enSablon sablon = item.DataItem as enSablon;

                if (sablon != null)
                {
                    HtmlControl dvSablon = item.FindControl("dvSablon") as HtmlControl;

                    if (sablon.Id == ayrSablon.Degeri.xToIntDefault())
                    {
                        dvSablon.Style.Add("background-color", "#f7fa9f");
                    }
                }
            }
        }

        protected void chkAcilirMenu_CheckedChanged(object sender, EventArgs e)
        {
            //enGenelAyar ayar = new enGenelAyar();

            //ayar.Id = enEnumaration.enmGenelAyarlar.AcilirMenuKullanimi;
            //ayar.Statu = chkAcilirMenu.Checked;

            //bllGenelAyarlar.AyarGuncelle(ayar);
        }

        protected void chkKategoriUrun_CheckedChanged(object sender, EventArgs e)
        {
            //enGenelAyar ayar = new enGenelAyar();

            //ayar.Id = enEnumaration.enmGenelAyarlar.UrunKategoriSistemi;
            //ayar.Statu = chkKategoriUrun.Checked;

            //bllGenelAyarlar.AyarGuncelle(ayar);
        }

        protected void chkKatalogGorunumu_CheckedChanged(object sender, EventArgs e)
        {
            //enGenelAyar ayar = new enGenelAyar();

            //ayar.Id = enEnumaration.enmGenelAyarlar.KatalogListeGorunumu;
            //ayar.Statu = chkKatalogGorunumu.Checked;

            //bllGenelAyarlar.AyarGuncelle(ayar);
        }

        protected void chkReferansKatalog_CheckedChanged(object sender, EventArgs e)
        {
            //enGenelAyar ayar = new enGenelAyar();

            //ayar.Id = enEnumaration.enmGenelAyarlar.ReferansKatalogListe;
            //ayar.Statu = chkReferansKatalog.Checked;

            //bllGenelAyarlar.AyarGuncelle(ayar);
        }

        protected void chkIletisimFormu_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void btnSablonKaydet_Click(object sender, EventArgs e)
        {
            //enTasarimAyar ayar = new enTasarimAyar();

            //ayar.Id = enEnumaration.enmTasarimAyarlari.Sablon;
            //ayar.Degeri = ddlSiteSablonlari.SelectedValue;

            //bllTasarimAyarlari.TasarimAyariGuncelle(ayar);
        }

        protected void btnSolResimYukle_Click(object sender, EventArgs e)
        {
            if (VwResimler.Count > 0)
            {
                enTasarimAyar ayar = new enTasarimAyar();

                ayar.Id = enEnumaration.enmTasarimAyarlari.Logo;
                ayar.Degeri = "/yukleme/resim/Logo/" + VwResimler[0];

                bllTasarimAyarlari.TasarimAyariGuncelle(ayar);

                VwResimler.Clear();

                TasarimAyarlariniGetir();
            }
        }

        protected void btnSolResimTemizle_Click(object sender, EventArgs e)
        {
            enTasarimAyar ayar = bllTasarimAyarlari.TasarimAyariGetir(enEnumaration.enmTasarimAyarlari.Logo);

            try
            {
                //File.Delete(Server.MapPath("~" + ayar.Degeri));
                ayar.Degeri = "";
                bllTasarimAyarlari.TasarimAyariGuncelle(ayar);

                TasarimAyarlariniGetir();

                uscUyari1.UyariGoster("Sol Üst resim silindi.", "Sol Üst Resim", true);
            }
            catch
            {
                uscUyari1.UyariGoster("Resim silinirken hata oluştu !", "Sol Üst Resim", true);
            }
        }

        protected void btnIkonYukle_Click(object sender, EventArgs e)
        {
            VwResimler.Clear();
        }

        protected void btnArkaPlanYukle_Click(object sender, EventArgs e)
        {
            if (VwResimler.Count > 0)
            {
                enTasarimAyar ayar = new enTasarimAyar();

                ayar.Id = enEnumaration.enmTasarimAyarlari.ArkaPlanResmi;
                ayar.Degeri = "/yukleme/resim/Arkaplanlar/" + VwResimler[0];

                bllTasarimAyarlari.TasarimAyariGuncelle(ayar);

                VwResimler.Clear();

                TasarimAyarlariniGetir();
            }
        }

        protected void btnArkaPlanTemizle_Click(object sender, EventArgs e)
        {
            enTasarimAyar ayar = bllTasarimAyarlari.TasarimAyariGetir(enEnumaration.enmTasarimAyarlari.ArkaPlanResmi);

            try
            {
                //File.Delete(Server.MapPath("~" + ayar.Degeri));

                ayar.Degeri = "";

                bllTasarimAyarlari.TasarimAyariGuncelle(ayar);

                TasarimAyarlariniGetir();

                uscUyari1.UyariGoster("Arka plan resmi silindi.", "Arka Plan Resmi", true);
            }
            catch
            {
                uscUyari1.UyariGoster("Resim silinirken hata oluştu !", "Arka Plan Resmi", true);
            }
        }

        protected void GmapGetir()
        {
            enGmap gmap = bllGmap.GmapGetir();

            txtLat.Text = gmap.Latitude;
            txtLong.Text = gmap.Longitude;
            edtGmapMetin.Content = gmap.Metin;

            //string latitude = gmap.Latitude.Replace('.', ',');
            //string longitude = gmap.Longitude.Replace('.', ',');

            //double gmapLat = latitude.xToDoubleDefault();
            //double gmapLong = longitude.xToDoubleDefault();

            //GMap1.addControl(new GControl(GControl.preBuilt.LargeMapControl));
            //GMap1.Key = gmap.APIKey;

            //GLatLng nokta = new GLatLng(gmapLat, gmapLong);
            //GMap1.setCenter(nokta, 15);
            //GMarker marker = new GMarker(nokta);
            //marker.options = new GMarkerOptions();
            //marker.options.clickable = true;
            //GInfoWindow window = new GInfoWindow(marker, gmap.Metin, true);
            //GMap1.addInfoWindow(window);
        }

        protected void btnGmapKaydet_Click(object sender, EventArgs e)
        {
            enGmap gmap = bllGmap.GmapGetir();

            gmap.Latitude = txtLat.Text;
            gmap.Longitude = txtLong.Text;
            gmap.Metin = edtGmapMetin.Content;

            bllGmap.GmapGuncelle(gmap);

            GmapGetir();
        }

        protected void lnkYukluResimlerdenSec_Click(object sender, EventArgs e)
        {
            ResimleriGetir("Logo");

            pnlYukluLogolar.Visible = true;
        }

        protected void lnkSolUstSec_Click(object sender, EventArgs e)
        {
            string resimAdi = (sender as LinkButton).CommandArgument;

            enTasarimAyar ayar = new enTasarimAyar();

            ayar.Id = enEnumaration.enmTasarimAyarlari.Logo;
            ayar.Degeri = "/yukleme/resim/Logo/" + resimAdi;

            bllTasarimAyarlari.TasarimAyariGuncelle(ayar);

            pnlYukluLogolar.Visible = false;
        }

        protected void lnkYukluArkaPlanlariGoster_Click(object sender, EventArgs e)
        {
            ResimleriGetir("Arkaplanlar");

            pnlYukleArkaPlanlar.Visible = true;
        }

        protected void lnkArkaPlanSec_Click(object sender, EventArgs e)
        {
            string resimAdi = (sender as LinkButton).CommandArgument;

            enTasarimAyar ayar = new enTasarimAyar();

            ayar.Id = enEnumaration.enmTasarimAyarlari.ArkaPlanResmi;
            ayar.Degeri = "/yukleme/resim/Arkaplanlar/" + resimAdi;

            bllTasarimAyarlari.TasarimAyariGuncelle(ayar);

            pnlYukleArkaPlanlar.Visible = false;
        }

        protected void lnkYukluSagUstGoster_Click(object sender, EventArgs e)
        {
            //ResimleriGetir("SagUst");

            //pnlYukluSagUst.Visible = true;
        }

        protected void lnkSagUstSec_Click(object sender, EventArgs e)
        {
            //string resimAdi = (sender as LinkButton).CommandArgument;

            //enTasarimAyar ayar = new enTasarimAyar();

            //ayar.Id = enEnumaration.enmTasarimAyarlari.UstSagResim;
            //ayar.Degeri = "/yukleme/resim/SagUst/" + resimAdi;

            //bllTasarimAyarlari.TasarimAyariGuncelle(ayar);

            //pnlYukluSagUst.Visible = false;
        }

        protected void lnkResimSil_Click(object sender, EventArgs e)
        {
            string resimAdi = (sender as LinkButton).CommandArgument;
            string resimYolu = "~/yukleme/resim/" + (sender as LinkButton).CommandName + "/" + resimAdi;

            try
            {
                File.Delete(Server.MapPath(resimYolu));

                ResimleriGetir((sender as LinkButton).CommandName);
            }
            catch
            {
                uscUyari1.UyariGoster("Resim silinirken hata oluştu !", "Resim Silme", false);
            }

        }

        public void ResimleriGetir(string klasor)
        {
            List<Resim> resimler = new List<Resim>();

            string[] strResimler = Directory.GetFiles(Server.MapPath("~/yukleme/resim/" + klasor));

            foreach (string item in strResimler)
            {
                Resim resim = new Resim();

                resim.Adi = item.Split('\\')[item.Split('\\').Length - 1];

                resimler.Add(resim);
            }

            if (klasor == "SagUst")
            {
                //rptSagUstResimler.DataSource = resimler;
                //rptSagUstResimler.DataBind();
            }
            else if (klasor == "Logo")
            {
                rptSolUstResimler.DataSource = resimler;
                rptSolUstResimler.DataBind();
            }
            else if (klasor == "Arkaplanlar")
            {
                rptArkaPlanlar.DataSource = resimler;
                rptArkaPlanlar.DataBind();
            }
        }

        protected void lnkWebOnizleme_Click(object sender, EventArgs e)
        {
            //frmOnizleme.Visible = !frmOnizleme.Visible;
        }

        protected void SablonSec_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;

            int sablonId = lnk.CommandArgument.xToIntDefault();

            enSablon sablon = bllSablonlar.SablonGetir(sablonId);
            enTasarimAyar ayarSablon = new enTasarimAyar();

            ayarSablon.Id = enEnumaration.enmTasarimAyarlari.GenelSablon;
            ayarSablon.Degeri = sablonId.ToString();

            bllTasarimAyarlari.TasarimAyariGuncelle(ayarSablon);

            SablonlariGetir();
        }

        protected void rptSablonlar_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            enSablon sablon = e.Item.DataItem as enSablon;

            enTasarimAyar ayrSablon = bllTasarimAyarlari.TasarimAyariGetir(enEnumaration.enmTasarimAyarlari.GenelSablon);

            HtmlControl dvSablon = e.Item.FindControl("dvSablon") as HtmlControl;

            if (sablon.Id == ayrSablon.Degeri.xToIntDefault())
            {
                dvSablon.Style.Add("background-color", "#f7fa9f");
            }
        }

        protected void LisansBilgileriniYaz()
        {
            enGenelAyar ayrYayinTar = bllGenelAyarlar.GenelAyarGetir(enEnumaration.enmGenelAyarlar.YayinTarihi);
            enGenelAyar ayrYayinSuresi = bllGenelAyarlar.GenelAyarGetir(enEnumaration.enmGenelAyarlar.YayinSuresi);
            enGenelAyar ayrYayinDurum = bllGenelAyarlar.GenelAyarGetir(enEnumaration.enmGenelAyarlar.YayinDurumu);

            DateTime yayinTarihi = ayrYayinTar.Icerik.xToDateTimeDefault();
            int yayinSuresi = ayrYayinSuresi.Icerik.xToIntDefault();
            bool yayinDurum = ayrYayinDurum.Icerik.xToBooleanDefault();

            DateTime bitisTarihi = yayinTarihi.AddYears(yayinSuresi);

            txtYayinTar.Text = yayinTarihi.ToShortDateString();
            txtYayinSure.Text = yayinSuresi.ToString();

            imgYayinDurum.ImageUrl = yayinDurum ? "/admin/css/img/dolu.png" : "/admin/css/img/bos.png";

            lnkYayinDurumuDegistir.Enabled = SessionManager.Admin.Finex;
            txtYayinSure.Enabled = SessionManager.Admin.Finex;
            txtYayinTar.Enabled = SessionManager.Admin.Finex;

            btnLisansBilgiGuncelle.Visible = SessionManager.Admin.Finex;
        }

        protected void btnLisansBilgiGuncelle_OnClick(object sender, EventArgs e)
        {
            enGenelAyar ayrYayinTar = bllGenelAyarlar.GenelAyarGetir(enEnumaration.enmGenelAyarlar.YayinTarihi);

            ayrYayinTar.Icerik = txtYayinTar.Text;

            bllGenelAyarlar.AyarGuncelle(ayrYayinTar);

            enGenelAyar ayrYayinSuresi = bllGenelAyarlar.GenelAyarGetir(enEnumaration.enmGenelAyarlar.YayinSuresi);

            ayrYayinSuresi.Icerik = txtYayinSure.Text;

            bllGenelAyarlar.AyarGuncelle(ayrYayinSuresi);

            LisansBilgileriniYaz();
        }

        protected void lnkYayinDurumuDegistir_OnClick(object sender, EventArgs e)
        {
            enGenelAyar ayrYayinDur = bllGenelAyarlar.GenelAyarGetir(enEnumaration.enmGenelAyarlar.YayinDurumu);

            ayrYayinDur.Icerik = ayrYayinDur.Icerik.xToBooleanDefault() ? "false" : "true";

            bllGenelAyarlar.AyarGuncelle(ayrYayinDur);

            LisansBilgileriniYaz();
        }

        protected void btnAddTable_OnClick(object sender, EventArgs e)
        {

            StringBuilder sb = new StringBuilder();

            string pkName = txtTablePrimaryKey.Text.xBosMu()
                ? txtTableName.Text.ToLowerInvariant().Replace(" ", "") + "_id"
                : txtTablePrimaryKey.Text;

            string tblName = txtTableName.Text.ToUpperInvariant().Replace(" ", "");

            sb.Append("CREATE TABLE " + tblName + " (" + pkName + " INTEGER CONSTRAINT " + txtTableName.Text +
                      "Constraint PRIMARY KEY);");

            ExecuteCommand(sb.ToString());

            GetDatabaseTables_Access();
            rgrvTables.Rebind();

            ddlTables.Items.FindByValue(tblName).Selected = true;
            ddlTables_OnSelectedIndexChanged(ddlTables, null);
        }

        protected void btnColumnEkle_OnClick(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("ALTER TABLE " + ddlTables.SelectedValue + " ADD COLUMN " + txtColumnName.Text + " " + ddlDataTypes.SelectedValue);

            ExecuteCommand(sb.ToString());

            //ExecuteCommand("ALTER TABLE " + ddlTables.SelectedValue + " ALTER Description {module}");

            GetColumnNames_Access(ddlTables.SelectedValue);

            rgrvColumns.Rebind();
        }

        private void ExecuteCommand(string cmdText)
        {
            try
            {
                OleDbCommand cmd = new OleDbCommand(cmdText, AccessHelper.Connection());

                cmd.Connection.Open();

                cmd.ExecuteNonQuery();

                cmd.Connection.Close();

                uscUyari1.UyariGoster("Command succesfull", "Success", false);
            }
            catch (Exception ex)
            {
                uscUyari1.UyariGoster("Can not execute command<br/>" + ex.Message, "Error", false);
            }
        }

        private void GetDatabaseTables_Access()
        {
            OleDbConnection conn = null;
            try
            {
                conn = AccessHelper.Connection();
                conn.Open();
                DataTable schemaTable = conn.GetOleDbSchemaTable(
                    OleDbSchemaGuid.Tables,
                    new object[] { null, null, null, "TABLE" });

                rgrvTables.DataSource = schemaTable;

                ddlTables.DataBind(schemaTable, "TABLE_NAME", "TABLE_NAME", "Select a table");

            }
            catch (OleDbException ex)
            {
                Trace.Write(ex.Message);
                Response.Write(ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        protected void GetColumnNames_Access(string tableName)
        {
            if (tableName.xBosMu()) return;

            OleDbConnection conn = null;
            try
            {
                conn = AccessHelper.Connection();
                conn.Open();
                string[] restrictions1 = new string[] { null, null, tableName, null };
                System.Data.DataTable schemaTable = conn.GetSchema("Columns", restrictions1).xSort("ORDINAL_POSITION");

                rgrvColumns.DataSource = schemaTable;


            }
            catch
            {
            }

        }

        protected void lnkSil_OnClick(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;

            StringBuilder sb = new StringBuilder();

            sb.Append("ALTER TABLE " + ddlTables.SelectedValue + " DROP COLUMN " + lnk.CommandArgument);

            ExecuteCommand(sb.ToString());

            GetColumnNames_Access(ddlTables.SelectedValue);
            rgrvColumns.Rebind();
        }

        protected void lnkSilTable_OnClick(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;

            StringBuilder sb = new StringBuilder();

            sb.Append("DROP TABLE " + lnk.CommandArgument + ";");

            ExecuteCommand(sb.ToString());

            GetDatabaseTables_Access();
            rgrvTables.Rebind();
        }

        protected void ddlTables_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            GetColumnNames_Access(ddlTables.SelectedValue);

            rgrvColumns.Rebind();
        }

        protected string GetDataType(string typeHashCode)
        {
            string veriTipiAdi = "UNKNOWN";

            if (OleDbType.Char.GetHashCode().ToString().ToLowerInvariant() == typeHashCode)
            {
                veriTipiAdi = "STRING";
            }
            else if (OleDbType.VarChar.GetHashCode().ToString().ToLowerInvariant() == typeHashCode)
            {
                veriTipiAdi = "STRING";
            }
            else if (OleDbType.LongVarChar.GetHashCode().ToString().ToLowerInvariant() == typeHashCode)
            {
                veriTipiAdi = "STRING";
            }
            else if (OleDbType.LongVarWChar.GetHashCode().ToString().ToLowerInvariant() == typeHashCode)
            {
                veriTipiAdi = "STRING";
            }
            else if (OleDbType.VarWChar.GetHashCode().ToString().ToLowerInvariant() == typeHashCode)
            {
                veriTipiAdi = "STRING";
            }
            else if (OleDbType.WChar.GetHashCode().ToString().ToLowerInvariant() == typeHashCode)
            {
                veriTipiAdi = "STRING";
            }
            else if (OleDbType.BigInt.GetHashCode().ToString().ToLowerInvariant() == typeHashCode)
            {
                veriTipiAdi = "INT";
            }
            //else if (OleDbType.Int.GetHashCode().ToString().ToLowerInvariant() == typeHashCode)
            //{
            //    veriTipiAdi = "int";
            //}
            else if (OleDbType.SmallInt.GetHashCode().ToString().ToLowerInvariant() == typeHashCode)
            {
                veriTipiAdi = "SMALL INT";
            }
            else if (OleDbType.TinyInt.GetHashCode().ToString().ToLowerInvariant() == typeHashCode)
            {
                veriTipiAdi = "TINY INT";
            }
            else if (OleDbType.Integer.GetHashCode().ToString().ToLowerInvariant() == typeHashCode)
            {
                veriTipiAdi = "INT";
            }
            else if (OleDbType.BigInt.GetHashCode().ToString().ToLowerInvariant() == typeHashCode)
            {
                veriTipiAdi = "BIG INT";
            }
            //else if (OleDbType.int.GetHashCode().ToString().ToLowerInvariant() == typeHashCode)
            //{
            //    veriTipiAdi = "int";
            //}
            else if (OleDbType.Boolean.GetHashCode().ToString().ToLowerInvariant() == typeHashCode)
            {
                veriTipiAdi = "YES/NO";
            }
            else if (OleDbType.DBDate.GetHashCode().ToString().ToLowerInvariant() == typeHashCode)
            {
                veriTipiAdi = "DATE/TIME";
            }
            else if (OleDbType.Date.GetHashCode().ToString().ToLowerInvariant() == typeHashCode)
            {
                veriTipiAdi = "DATE/TIME";
            }

            return veriTipiAdi;
        }

        //protected void btnFaceYoneticiKaydet_OnClick(object sender, EventArgs e)
        //{
        //    enGenelAyar ayar = new enGenelAyar();

        //    ayar.Id = enEnumaration.enmGenelAyarlar.FaceYoneticileri;
        //    ayar.Icerik = "";

        //    if (!txtFaceYonetici.Text.xBosMu())
        //    {
        //        ayar.Icerik = "{" + txtFaceYonetici.Text + "}";
        //    }

        //    bllGenelAyarlar.AyarGuncelle(ayar);
        //}

        protected void rcpArkaPlan_OnColorChanged(object sender, EventArgs e)
        {
            enTasarimAyar ayar = new enTasarimAyar();

            ayar.Id = enEnumaration.enmTasarimAyarlari.ArkaPlanRengi;
            ayar.Degeri = ColorTranslator.ToHtml((sender as RadColorPicker).SelectedColor);

            bllTasarimAyarlari.TasarimAyariGuncelle(ayar);

            TasarimAyarlariniGetir();
        }

        protected void rcpMenuArkaPlan_OnColorChanged(object sender, EventArgs e)
        {
            enTasarimAyar ayar = new enTasarimAyar();

            ayar.Id = enEnumaration.enmTasarimAyarlari.MenuArkaPlanRengi;
            ayar.Degeri = ColorTranslator.ToHtml((sender as RadColorPicker).SelectedColor);

            bllTasarimAyarlari.TasarimAyariGuncelle(ayar);

            TasarimAyarlariniGetir();
        }

        protected void btnMenuYuksekligiKaydet_OnClick(object sender, EventArgs e)
        {
            enTasarimAyar ayar = new enTasarimAyar();

            ayar.Id = enEnumaration.enmTasarimAyarlari.MenuYuksekligi;
            ayar.Degeri = rsMenuYuksekligi.Value + "px";

            bllTasarimAyarlari.TasarimAyariGuncelle(ayar);

            TasarimAyarlariniGetir();
        }

        protected void rcpYaziRengi_OnColorChanged(object sender, EventArgs e)
        {
            enTasarimAyar ayar = new enTasarimAyar();

            ayar.Id = enEnumaration.enmTasarimAyarlari.YaziRengi;
            ayar.Degeri = ColorTranslator.ToHtml((sender as RadColorPicker).SelectedColor);

            bllTasarimAyarlari.TasarimAyariGuncelle(ayar);

            TasarimAyarlariniGetir();

        }

        protected void rcpBaglantiRengi1_OnColorChanged(object sender, EventArgs e)
        {
            enTasarimAyar ayar = new enTasarimAyar();

            ayar.Id = enEnumaration.enmTasarimAyarlari.BaglantiRengi;
            ayar.Degeri = ColorTranslator.ToHtml((sender as RadColorPicker).SelectedColor);

            bllTasarimAyarlari.TasarimAyariGuncelle(ayar);

            TasarimAyarlariniGetir();
        }

        protected void rcpBaglantiRengi2_OnColorChanged(object sender, EventArgs e)
        {
            enTasarimAyar ayar = new enTasarimAyar();

            ayar.Id = enEnumaration.enmTasarimAyarlari.BaglantiRengi2;
            ayar.Degeri = ColorTranslator.ToHtml((sender as RadColorPicker).SelectedColor);

            bllTasarimAyarlari.TasarimAyariGuncelle(ayar);

            TasarimAyarlariniGetir();
        }

        protected void rcpBaslikRengi_OnColorChanged(object sender, EventArgs e)
        {
            enTasarimAyar ayar = new enTasarimAyar();

            ayar.Id = enEnumaration.enmTasarimAyarlari.BaslikRengi;
            ayar.Degeri = ColorTranslator.ToHtml((sender as RadColorPicker).SelectedColor);

            bllTasarimAyarlari.TasarimAyariGuncelle(ayar);

            TasarimAyarlariniGetir();
        }

        protected void rcpFooter_OnColorChanged(object sender, EventArgs e)
        {
            enTasarimAyar ayar = new enTasarimAyar();

            ayar.Id = enEnumaration.enmTasarimAyarlari.AltBolumRengi;
            ayar.Degeri = ColorTranslator.ToHtml((sender as RadColorPicker).SelectedColor);

            bllTasarimAyarlari.TasarimAyariGuncelle(ayar);

            TasarimAyarlariniGetir();
        }

        protected void rcpBlokArkaPlan_OnColorChanged(object sender, EventArgs e)
        {
            enTasarimAyar ayar = new enTasarimAyar();

            ayar.Id = enEnumaration.enmTasarimAyarlari.BlokArkaPlanRengi;
            ayar.Degeri = ColorTranslator.ToHtml((sender as RadColorPicker).SelectedColor);

            bllTasarimAyarlari.TasarimAyariGuncelle(ayar);

            TasarimAyarlariniGetir();
        }

        protected void rcpBlokCerceve_OnColorChanged(object sender, EventArgs e)
        {
            enTasarimAyar ayar = new enTasarimAyar();

            ayar.Id = enEnumaration.enmTasarimAyarlari.BlokCerceveRengi;
            ayar.Degeri = ColorTranslator.ToHtml((sender as RadColorPicker).SelectedColor);

            bllTasarimAyarlari.TasarimAyariGuncelle(ayar);

            TasarimAyarlariniGetir();
        }

        protected void btnGenelAyarlariKaydet_OnClick(object sender, EventArgs e)
        {
            //enGenelAyar ayarArama = new enGenelAyar();

            //ayarArama.Id = enEnumaration.enmGenelAyarlar.SiteIciArama;
            //ayarArama.Statu = hdnSiteIciArama.Value.xToBooleanDefault();

            //bllGenelAyarlar.AyarGuncelle(ayarArama);

            ///////////////////////////////////////////////////

            enGenelAyar ayarGmap = new enGenelAyar();

            ayarGmap.Id = enEnumaration.enmGenelAyarlar.GmapKullanimi;
            ayarGmap.Statu = hdnGoogleMap.Value.xToBooleanDefault();
            ayarGmap.Icerik = hdnGoogleMap.Value.xToBooleanDefault().ToString();

            bllGenelAyarlar.AyarGuncelle(ayarGmap);

            ///////////////////////////////////////////////////

            enGenelAyar ayarIletForm = new enGenelAyar();

            ayarIletForm.Id = enEnumaration.enmGenelAyarlar.IletisimFormuKullanimi;
            ayarIletForm.Statu = hdnIletisimFormu.Value.xToBooleanDefault();

            bllGenelAyarlar.AyarGuncelle(ayarIletForm);

            //////////////////////////////////////////////////

            enGenelAyar ayarAnalytics = new enGenelAyar();

            ayarAnalytics.Id = enEnumaration.enmGenelAyarlar.AnalyticsKodu;
            ayarAnalytics.Icerik = txtAnalytics.Text;

            bllGenelAyarlar.AyarGuncelle(ayarAnalytics);

            //////////////////////////////////////////////////

            enGenelAyar ayarIletEp = new enGenelAyar();

            ayarIletEp.Id = enEnumaration.enmGenelAyarlar.IletisimFormuEposta;
            ayarIletEp.Icerik = txtIletisimEposta.Text;

            bllGenelAyarlar.AyarGuncelle(ayarIletEp);

            //////////////////////////////////////////////////

            enGenelAyar ayarMailSun = new enGenelAyar();

            ayarMailSun.Id = enEnumaration.enmGenelAyarlar.MailSunucu;
            ayarMailSun.Icerik = txtMailSunucu.Text;

            bllGenelAyarlar.AyarGuncelle(ayarMailSun);
        }


        protected void lnkMenuYukseklikTemizle_OnClick(object sender, EventArgs e)
        {
            enTasarimAyar ayar = new enTasarimAyar();

            ayar.Id = enEnumaration.enmTasarimAyarlari.MenuYuksekligi;
            ayar.Degeri = "";

            bllTasarimAyarlari.TasarimAyariGuncelle(ayar);

            TasarimAyarlariniGetir();
        }

        protected void btnFontSec_OnClick(object sender, EventArgs e)
        {
            if (hdnMetinFontu.Value.xBosMu() == false)
            {
                enTasarimAyar ayar1 = new enTasarimAyar();

                ayar1.Id = enEnumaration.enmTasarimAyarlari.MetinFontu;
                ayar1.Degeri = hdnMetinFontu.Value;

                bllTasarimAyarlari.TasarimAyariGuncelle(ayar1);
            }

            /////////////////////////////////////////////////////////////

            if (hdnBaslikFontu.Value.xBosMu() == false)
            {
                enTasarimAyar ayar2 = new enTasarimAyar();

                ayar2.Id = enEnumaration.enmTasarimAyarlari.BaslikFontu;
                ayar2.Degeri = hdnBaslikFontu.Value;

                bllTasarimAyarlari.TasarimAyariGuncelle(ayar2);

                TasarimAyarlariniGetir();
            }
        }

        protected void btnBaslikFontuTemizle_OnClick(object sender, EventArgs e)
        {
            enTasarimAyar ayar2 = new enTasarimAyar();

            ayar2.Id = enEnumaration.enmTasarimAyarlari.BaslikFontu;
            ayar2.Degeri = "";

            bllTasarimAyarlari.TasarimAyariGuncelle(ayar2);

            TasarimAyarlariniGetir();

        }

        protected void btnMetinFontuTemizle_OnClick(object sender, EventArgs e)
        {
            enTasarimAyar ayar2 = new enTasarimAyar();

            ayar2.Id = enEnumaration.enmTasarimAyarlari.MetinFontu;
            ayar2.Degeri = "";

            bllTasarimAyarlari.TasarimAyariGuncelle(ayar2);

            TasarimAyarlariniGetir();

        }

        protected void DatabaseIsmiGetir()
        {
            var configuration = WebConfigurationManager.OpenWebConfiguration("~");
            var section = (ConnectionStringsSection)configuration.GetSection("connectionStrings");

            string conStr = section.ConnectionStrings["AccConnectionString"].ConnectionString;

            string[] arrConStr = conStr.Split('|');

            txtDatabaseName.Text = arrConStr[arrConStr.Length - 1].Replace(".mdb;", "");
        }

        protected void btnDatabaseKaydet_OnClick(object sender, EventArgs e)
        {
            var configuration = WebConfigurationManager.OpenWebConfiguration("~");
            var section = (ConnectionStringsSection)configuration.GetSection("connectionStrings");

            section.ConnectionStrings["AccConnectionString"].ConnectionString = "Provider=Microsoft.jet.oledb.4.0; Data Source=|DataDirectory|" + txtDatabaseName.Text + ".mdb;";
            configuration.Save();

            DatabaseIsmiGetir();
        }

        protected void lnkTemplateTemizle_OnClick(object sender, EventArgs e)
        {
            enTasarimAyar ayar = new enTasarimAyar();

            ayar.Id = enEnumaration.enmTasarimAyarlari.Template;
            ayar.Degeri = "";

            bllTasarimAyarlari.TasarimAyariGuncelle(ayar);

            TasarimAyarlariniGetir();
        }
    }
}