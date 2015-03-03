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
using System.Web.Routing;
using Common;


namespace Ws.admin
{
    public partial class Pages : System.Web.UI.Page
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

        public int? VwAnaResimId
        {
            get { return ViewState["VwAnaResimId"].xToInt(); }
            set { ViewState["VwAnaResimId"] = value; }
        }

        public List<enSiteHaritasi> VwEklenecekler
        {
            get
            {
                if (ViewState["VwEklenecekler"] == null)
                    ViewState["VwEklenecekler"] = new List<enSiteHaritasi>();
                return ViewState["VwEklenecekler"] as List<enSiteHaritasi>;
            }
            set
            {
                ViewState["VwEklenecekler"] = value;
            }
        }

        public List<enSiteHaritasi> VwSilinecekler
        {
            get
            {
                if (ViewState["VwSilinecekler"] == null)
                    ViewState["VwSilinecekler"] = new List<enSiteHaritasi>();
                return ViewState["VwSilinecekler"] as List<enSiteHaritasi>;
            }
            set
            {
                ViewState["VwSilinecekler"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TumSayfalariGetir();
                OzelSayfalariGetir();
                PozisyonlariDoldur();

                enSablon sablon = bllSablonlar.SablonGetir(Settings.Tasarim.sSablon.xToIntDefault());

                if (sablon.Id == enEnumaration.enmSablonlar.Tek)
                {
                    chkSayfaMenu.Checked = true;
                }

                if (Request.QueryString["dzid"] != null)
                {
                    VwID = Request.QueryString["dzid"].xToInt();

                    SayfaGetir();

                    ////SayfaBloklariGetir();
                }
            }

            foreach (UploadedFile file in rauResmi.UploadedFiles)
            {
                string guid = System.Guid.NewGuid().ToString();

                file.SaveAs(Server.MapPath("~/yukleme/" + guid + file.GetExtension()), true);
                VwResimler.Add(guid + file.GetExtension());
            }

            if (Request.Url.AbsoluteUri.Contains("?iframe"))
            {
                if (Request.QueryString["i"].xBosMu() == false)
                {
                    int tabIndex = Request.QueryString["i"].xToIntDefault();

                    rtsGenel.Tabs[0].Visible = false;
                    rtsGenel.Tabs[1].Visible = false;
                    rtsGenel.Tabs[2].Visible = false;

                    rtsGenel.Tabs[tabIndex].Visible = true;

                    rmpGenel.SelectedIndex = tabIndex;
                }

                if (Request.QueryString["ai"].xBosMu() == false)
                {
                    int tabIndex = Request.QueryString["ai"].xToIntDefault();

                    rtsYeni.Tabs[0].Visible = false;
                    rtsYeni.Tabs[1].Visible = false;
                    rtsYeni.Tabs[2].Visible = false;

                    rtsYeni.Tabs[tabIndex].Visible = true;

                    rmpYeni.SelectedIndex = tabIndex;
                }
            }

        }

        protected void SayfaGetir()
        {
            if (VwID != null && VwID.Value != 0)
            {
                rtsYeni.Tabs[1].Enabled = true;
                rtsYeni.Tabs[2].Enabled = true;
                //rtsYeni.Tabs[3].Enabled = true;

                rtsGenel.SelectedIndex = 1;
                rmpGenel.SelectedIndex = 1;
                rtsGenel.Tabs[1].Text = "Düzenle";
                //btnNewPageClear.Visible = true;

                if (Request.Url.AbsoluteUri.Contains("?iframe"))
                {
                    rtsGenel.Tabs[0].Visible = false;
                    rtsGenel.Tabs[2].Visible = false;
                }

                enSiteHaritasi sayfa = bllSiteHaritasi.SayfaGetir(VwID.Value);

                txtYeniSayfaAdi.Text = sayfa.Adi;
                txtYeniSayfaBaslik.Text = sayfa.Title;
                txtYeniSayfaDesc.Text = sayfa.Description;
                txtYeniSayfaKeywords.Text = sayfa.Keywords;
                txtYeniSayfaUrl.Text = sayfa.Url.Contains("://www.") ? sayfa.Url : sayfa.Url.Replace("/", "");
                edtYeniSayfaIcerik.Content = sayfa.Icerik;
                txtFotoBaslik.Text = sayfa.FotoBaslik;
                txtVideoBaslik.Text = sayfa.VideoBaslik;
                //imgSayfaIkon.ImageUrl = sayfa.Resim;
                //chkFotoGaleri.Checked = sayfa.FotoGaleriMi;
                //chkFaceComments.Checked = sayfa.FaceComments;
                //chkBlogList.Checked = sayfa.List;
                //chkCustom.Checked = sayfa.Custom;
                chkBaslikAlani.Checked = sayfa.BaslikAlani;
                chkPaylasimAlani.Checked = sayfa.PaylasimAlani;
                chkSayfaYolu.Checked = sayfa.SayfaYolu;
                chkSayfaMenu.Checked = sayfa.SayfaMenu;
                chkUrunMu.Checked = sayfa.UrunMu;
                chkHaberMi.Checked = sayfa.HaberMi;
                chkSagAtlMenu.Checked = sayfa.SagAltMenu;
                chkSolAtlMenu.Checked = sayfa.SolAltMenu;

                hdnDefaultSayfa.Value = sayfa.DefaultSayfa.ToString();
                hdnFizikselUrl.Value = sayfa.FizikselUrl;
                hdnDynamic.Value = sayfa.Dinamik.ToString();

                if (sayfa.Parent != null)
                {
                    RadTreeNode node = trvSayfalar.FindNodeByValue(sayfa.Parent.Value.ToString());

                    node.Selected = true;
                    node.ExpandParentNodes();
                }

                //if (sayfa.DefaultSayfa)
                //{
                //    rtsYeni.Tabs[2].Visible = false;
                //    //rtsYeni.Tabs[3].Visible = false;
                //}
                //else
                //{
                //    rtsYeni.Tabs[2].Visible = true;
                //    //rtsYeni.Tabs[3].Visible = true;

                //    ResimleriGetir();
                //    rgrvResimler.DataBind();
                //    VideolariGetir();
                //}
            }


        }

        protected void btnYeniSayfa_Click(object sender, EventArgs e)
        {
            enSiteHaritasi sayfa = new enSiteHaritasi();

            sayfa.Adi = txtYeniSayfaAdi.Text;
            sayfa.Icerik = edtYeniSayfaIcerik.Content;
            sayfa.Parent = trvSayfalar.SelectedValue.xToInt();
            sayfa.Description = txtYeniSayfaDesc.Text;
            sayfa.Keywords = txtYeniSayfaKeywords.Text;
            sayfa.Statu = false;
            sayfa.Title = txtYeniSayfaBaslik.Text;
            sayfa.Url = txtYeniSayfaUrl.Text.Contains("://www.") ? txtYeniSayfaUrl.Text : "/" + txtYeniSayfaUrl.Text;
            sayfa.Sira = bllSiteHaritasi.SonSiraNoGetir(sayfa.Parent) + 1;
            sayfa.FotoBaslik = txtFotoBaslik.Text;
            sayfa.VideoBaslik = txtVideoBaslik.Text;
            //sayfa.FotoGaleriMi = chkFotoGaleri.Checked;
            //sayfa.FaceComments = chkFaceComments.Checked;
            //sayfa.List = chkBlogList.Checked;
            //sayfa.Custom = chkCustom.Checked;
            sayfa.BaslikAlani = chkBaslikAlani.Checked;
            sayfa.PaylasimAlani = chkPaylasimAlani.Checked;
            sayfa.SayfaYolu = chkSayfaYolu.Checked;
            sayfa.SayfaMenu = chkSayfaMenu.Checked;
            sayfa.UrunMu = chkUrunMu.Checked;
            sayfa.HaberMi = chkHaberMi.Checked;
            sayfa.SagAltMenu = chkSagAtlMenu.Checked;
            sayfa.SolAltMenu = chkSolAtlMenu.Checked;


            if (VwID == null) //yeni sayfa
            {
                if (bllSiteHaritasi.UrlVarMi(txtYeniSayfaUrl.Text) && !txtYeniSayfaUrl.Text.Contains("://www."))
                {
                    uscUyari1.UyariGoster("Sayfa url'si kullanımda lütfen başka bir url yazın.", "URL Kullanımda", false);
                    return;
                }

                try
                {
                    bllSiteHaritasi.YeniSayfaEkle(sayfa);

                    Settings.RouteManager.Refresh();

                    Response.Redirect("Pages.aspx?dzid=" + sayfa.Id); ;

                    //KontrolleriSifirla();
                    //SayfalariGetir();
                }
                catch
                {
                    uscUyari1.UyariGoster("Sayfa kaydedilirken hata oluştu !", "Yeni Sayfa", false);
                }
            }
            else
            {
                if (bllSiteHaritasi.UrlVarMi(txtYeniSayfaUrl.Text, VwID.Value))
                {
                    uscUyari1.UyariGoster("Sayfa url'si kullanımda lütfen başka bir url yazın.", "URL Kullanımda", false);
                    return;
                }

                sayfa.Id = VwID.Value;

                if (sayfa.Id == sayfa.Parent)
                {
                    uscUyari1.UyariGoster("Düzenlediğiniz sayfanın, <b>ÜST SAYFASI</b> olarak kendisini seçemezsiniz.<br/>Lütfen soldaki üst sayfa seçimini kontrol edin.", "Hatalı Üst Sayfa Seçimi", false);
                    return;
                }

                sayfa.DefaultSayfa = hdnDefaultSayfa.Value.xToBooleanDefault();
                sayfa.FizikselUrl = hdnFizikselUrl.Value;
                sayfa.Dinamik = hdnDynamic.Value.xToBooleanDefault();

                try
                {
                    bllSiteHaritasi.SayfaDuzenle(sayfa);

                    uscUyari1.UyariGoster("Değişiklikler kaydedildi.", "Sayfa Düzenleme", true);

                    trlSiteHaritasi.Rebind();

                    Settings.RouteManager.Refresh();

                    //KontrolleriSifirla();
                    //SayfalariGetir();
                }
                catch
                {
                    uscUyari1.UyariGoster("Değişiklikler kaydedilirken hata oluştu !", "Sayfa Düzenleme", false);
                }
            }

            //try
            //{
            //    RouteTable.Routes.MapPageRoute(sayfa.Adi.xToUrl() + sayfa.Id, "page/" + "{Id}/" + sayfa.Url.Replace("/", ""), "~/Sayfa.aspx");
            //}
            //catch
            //{}

        }

        protected void KontrolleriSifirla()
        {
            trvSayfalar.UnselectAllNodes();
            trvSayfalar.CollapseAllNodes();

            txtYeniSayfaAdi.Text = "";
            txtYeniSayfaBaslik.Text = "";
            txtYeniSayfaDesc.Text = "";
            txtYeniSayfaKeywords.Text = "";
            txtYeniSayfaUrl.Text = "";

            edtYeniSayfaIcerik.Content = "";

            VwID = null;

            rtsGenel.SelectedIndex = 0;
            rmpGenel.SelectedIndex = 0;

            btnNewPageClear.Visible = false;

            rtsYeni.Tabs[1].Enabled = false;
            rtsYeni.Tabs[2].Enabled = false;
            //rtsYeni.Tabs[3].Enabled = false;

            rtsYeni.SelectedIndex = 0;
            rmpYeni.SelectedIndex = 0;

            rgrvResimler.DataSource = null;
            rgrvResimler.DataBind();

            rptIcerikVideolar.DataSource = null;
            rptIcerikVideolar.DataBind();

            rtsGenel.Tabs[1].Text = "(+) Yeni";
        }

        protected void lnkStatuDegistir_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;

            int sayfaId = lnk.CommandArgument.xToIntDefault();

            enSiteHaritasi sayfa = bllSiteHaritasi.SayfaGetir(sayfaId);

            if (sayfa.Statu)
            {
                sayfa.Statu = false;
            }
            else
            {
                sayfa.Statu = true;
            }

            bllSiteHaritasi.StatuDegistir(sayfa);

            ListeYenile();
        }

        protected void lnkSil_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;

            int sayfaId = lnk.CommandArgument.xToIntDefault();

            bllSiteHaritasi.SayfaSil(sayfaId);

            ListeYenile();
        }

        protected void lnkAsagi_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;

            int sayfaId = lnk.CommandArgument.xToIntDefault();

            enSiteHaritasi site = bllSiteHaritasi.SayfaGetir(sayfaId);
            enSiteHaritasi altSite = bllSiteHaritasi.BirAlttakiSayfayiGetir(site);

            if (altSite.Id == 0) return;

            site.Sira += 1;

            bllSiteHaritasi.SiraGuncelle(site);

            altSite.Sira -= 1;

            bllSiteHaritasi.SiraGuncelle(altSite);

            ListeYenile();
        }

        protected void lnkYukari_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;

            int sayfaId = lnk.CommandArgument.xToIntDefault();

            enSiteHaritasi site = bllSiteHaritasi.SayfaGetir(sayfaId);
            enSiteHaritasi ustSite = bllSiteHaritasi.BirUsttekiSayfayiGetir(site);

            if (site.Sira == 1) return;

            site.Sira -= 1;

            bllSiteHaritasi.SiraGuncelle(site);

            ustSite.Sira += 1;

            bllSiteHaritasi.SiraGuncelle(ustSite);

            ListeYenile();
        }

        protected void ListeYenile()
        {
            trlSiteHaritasi.Rebind();
        }

        void ResimKucult(string resimYol, string resim, int sabit, string kaydedilecekYol, bool silinsinMi)
        {
            //sabit  Küçültülecek Genişlik Değeri belirtiyoruz.

            Bitmap bmp = new Bitmap(Server.MapPath(resimYol + resim)); //Küçülmesini istedigimiz resmi seçiyoruz
            using (Bitmap OriginalBM = bmp)
            {
                double ResimYukseklik = OriginalBM.Height;
                double ResimGenislik = OriginalBM.Width;
                double oran = 0;

                if (ResimGenislik > ResimYukseklik && ResimGenislik > sabit) //Eğer resmin genişliği yüksekliginden büyükse veya 150 pxden büyükse 
                {
                    oran = ResimGenislik / ResimYukseklik;
                    ResimGenislik = sabit; //Resmin genişliğini 150 olarak atayacak ve aşağıda o genişliğe göre yüksekliği bulacak.
                    ResimYukseklik = sabit / oran;
                }
                else if (ResimYukseklik > ResimGenislik && ResimYukseklik > sabit) //Buradada yukarıdaki işlemin tersini yapıp genişliği otomatik bulacak.
                {

                    if (kaydedilecekYol.Contains("med"))
                    {
                        oran = ResimGenislik / ResimYukseklik;
                        ResimGenislik = sabit; //Resmin genişliğini 150 olarak atayacak ve aşağıda o genişliğe göre yüksekliği bulacak.
                        ResimYukseklik = sabit / oran;
                    }
                    else
                    {
                        oran = ResimYukseklik / ResimGenislik;
                        ResimYukseklik = sabit;
                        ResimGenislik = sabit / oran;
                    }

                }
                else if (ResimGenislik == ResimYukseklik && ResimGenislik > sabit)
                {
                    oran = ResimYukseklik / ResimGenislik;
                    ResimYukseklik = sabit;
                    ResimGenislik = sabit / oran;
                }

                Bitmap Resizebm = new Bitmap(OriginalBM);
                Resizebm = ResizeBitmap(Resizebm, ResimGenislik.xToIntDefault(), ResimYukseklik.xToIntDefault());

                ImageFormat imgFormat = ImageFormat.Png;

                if (resim.ToUpper().Contains(".JPG"))
                {
                    imgFormat = ImageFormat.Jpeg;
                }

                Resizebm.Save(Server.MapPath(kaydedilecekYol + resim), imgFormat);
                OriginalBM.Dispose();

                if (silinsinMi)
                {
                    try
                    {
                        File.Delete(Server.MapPath(resimYol + resim));
                    }
                    catch
                    {

                    }

                }

            }
        }

        public Bitmap ResizeBitmap(Bitmap b, int nWidth, int nHeight)
        {
            Bitmap result = new Bitmap(nWidth, nHeight);

            using (Graphics g = Graphics.FromImage(result))
            {
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(b, 0, 0, nWidth, nHeight);
            }
            return result;
        }

        protected void TumSayfalariGetir()
        {
            List<enSiteHaritasi> sayfalar = bllSiteHaritasi.TumSayfalariGetir();

            trvSayfalar.DataSource = sayfalar;
            trvSayfalar.DataFieldParentID = "Parent";
            trvSayfalar.DataFieldID = "Id";
            trvSayfalar.DataTextField = "Adi";
            trvSayfalar.DataValueField = "Id";
            trvSayfalar.DataBind();

        }

        protected void OzelSayfalariGetir()
        {
            List<enSiteHaritasi> sayfalar = bllSiteHaritasi.OzelSayfalariGetir();

            treeSayfalar.DataSource = sayfalar;
            treeSayfalar.DataTextField = "Adi";
            treeSayfalar.DataValueField = "Id";
            treeSayfalar.DataFieldID = "Id";
            treeSayfalar.DataFieldParentID = "Parent";
            treeSayfalar.DataBind();

            RadTreeNode node = new RadTreeNode("------------", "");
            node.Enabled = false;

            treeSayfalar.Nodes.Insert(0, node);
            treeSayfalar.Nodes.Insert(0, new RadTreeNode("Genel Şablon", "M"));

        }

        protected void btnNewPageClear_Click(object sender, EventArgs e)
        {
            KontrolleriSifirla();

            rtsGenel.SelectedIndex = 1;
            rmpGenel.SelectedIndex = 1;


        }

        protected void txtSiraNo_TextChanged(object sender, EventArgs e)
        {
            RadTextBox txt = sender as RadTextBox;

            int id = txt.Attributes["dzID"].xToIntDefault();

            enSiteHaritasi sayfa = bllSiteHaritasi.SayfaGetir(id);

            sayfa.Sira = txt.Text.xToIntDefault();

            bllSiteHaritasi.SiraGuncelle(sayfa);

            ListeYenile();
        }

        protected void txtResimSiraNo_TextChanged(object sender, EventArgs e)
        {
            RadTextBox txt = sender as RadTextBox;

            int id = txt.Attributes["dzID"].xToIntDefault();

            enIcerikResim resim = bllIcerikResimleri.ResimGetir(id);

            resim.Sira = txt.Text.xToIntDefault();

            bllIcerikResimleri.ResimSiraGuncelle(resim);

            ResimleriGetir();
            rgrvResimler.DataBind();
        }

        protected void txtVideoSiraNo_TextChanged(object sender, EventArgs e)
        {
            RadTextBox txt = sender as RadTextBox;

            int id = txt.Attributes["dzID"].xToIntDefault();

            enIcerikVideo video = bllIcerikVideolari.Getir(id);

            video.Sira = txt.Text.xToIntDefault();

            bllIcerikVideolari.SiraGuncelle(video);

            VideolariGetir();
        }

        protected void lnkBack_Click(object sender, EventArgs e)
        {
            int id = Request.QueryString["id"].xToIntDefault();

            enSiteHaritasi sayfa = bllSiteHaritasi.SayfaGetir(id);

            string url = sayfa.Parent == null ? "Pages.aspx" : "Pages.aspx?id=" + sayfa.Parent;

            Response.Redirect(url);
        }

        protected void trlSiteHaritasi_NeedDataSource(object sender, TreeListNeedDataSourceEventArgs e)
        {
            trlSiteHaritasi.DataSource = GetDataSource();
        }

        protected List<enSiteHaritasi> GetDataSource()
        {
            List<enSiteHaritasi> sayfalar = bllSiteHaritasi.TumSayfalariGetir();

            return sayfalar;
        }

        protected void ResimleriGetir()
        {
            if (VwID != null && VwID.Value != 0)
            {
                List<enIcerikResim> resimler = bllIcerikResimleri.ResimleriGetir(VwID.Value, null);

                rgrvResimler.DataSource = resimler;
                //rgrvResimler.DataBind();
                //AnaResimIsaretle();
            }
        }

        protected void VideolariGetir()
        {
            List<enIcerikVideo> videolar = bllIcerikVideolari.Getir(VwID.Value, null);

            rptIcerikVideolar.DataSource = videolar;
            rptIcerikVideolar.DataBind();
        }

        protected void lnkResimStatuDegistir_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;

            int resimId = lnk.CommandArgument.xToIntDefault();

            enIcerikResim resim = bllIcerikResimleri.ResimGetir(resimId);

            if (resim.Statu)
            {
                resim.Statu = false;
            }
            else
            {
                resim.Statu = true;
            }

            bllIcerikResimleri.ResimStatuGuncelle(resim);

            ResimleriGetir();
            rgrvResimler.DataBind();
        }

        protected void lnkResimSil_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;

            int resimId = lnk.CommandArgument.xToIntDefault();

            try
            {
                bllIcerikResimleri.ResimSil(resimId);

                ResimleriGetir();
                rgrvResimler.DataBind();
            }
            catch
            {
                //uscUyari1.UyariGoster("Resim silinirken hata oluştu ! Lütfen tekrar deneyiniz.", "Resim Silme", false);
            }
        }

        protected void lnkVideoStatuDegistir_Click(object sender, EventArgs e)
        {

        }

        protected void lnkVideoSil_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;

            int id = lnk.CommandArgument.xToIntDefault();

            try
            {
                bllIcerikVideolari.Sil(id);

                VideolariGetir();
            }
            catch
            { }
        }

        protected void btnYeniResimYukle_Click(object sender, EventArgs e)
        {
            foreach (string resimUrl in VwResimler)
            {
                ResimKucult("~/yukleme/", resimUrl, 100, "~/yukleme/icerik/kucuk/", false);
                ResimKucult("~/yukleme/", resimUrl, 250, "~/yukleme/icerik/orta/", false);
                ResimKucult("~/yukleme/", resimUrl, 1024, "~/yukleme/icerik/buyuk/", true);

                enIcerikResim resim = new enIcerikResim();

                resim.Buyuk = "/yukleme/icerik/buyuk/" + resimUrl;
                resim.SayfaId = VwID.Value;
                resim.KayitTarihi = DateTime.Now.Date;
                resim.Kucuk = "/yukleme/icerik/kucuk/" + resimUrl;
                resim.Orta = "/yukleme/icerik/orta/" + resimUrl;
                resim.Sira = bllIcerikResimleri.SonSiraNoGetir(resim.SayfaId) + 1;
                resim.Statu = chkAktifEkle.Checked;
                resim.Kaydeden = SessionManager.Admin.Id;
                resim.KayitTarihi = DateTime.Now.Date;

                bllIcerikResimleri.YeniResimEkle(resim);

            }

            VwResimler.Clear();

            ResimleriGetir();
            rgrvResimler.DataBind();
        }

        protected void btnVideoEkle_Click(object sender, EventArgs e)
        {
            enIcerikVideo video = new enIcerikVideo();

            video.SayfaId = VwID.Value;
            video.Aciklama = txtVidAciklama.Text;
            video.Baslik = txtVidBaslik.Text;
            video.Kaynak = txtVidKaynak.Text.xBosMu() ? "//player.vimeo.com/video/" + txtVidKaynakVimeo.Text : "//www.youtube.com/embed/" + txtVidKaynak.Text;
            video.Kapak = hdnKapakUrl.Value; //imgKapak.Attributes["src"];
            video.Kaydeden = SessionManager.Admin.Id;
            video.KayitTarihi = DateTime.Now.Date;
            video.Sira = bllIcerikVideolari.SonSiraNoGetir(video.SayfaId) + 1;
            video.Statu = chkVidAktifEkle.Checked;
            video.UrlKodu = txtVidKaynak.Text.xBosMu() ? txtVidKaynakVimeo.Text : txtVidKaynak.Text;

            bllIcerikVideolari.YeniEkle(video);

            VideolariGetir();
        }

        protected void VideoKontrolleriSifirla()
        {
            txtVidAciklama.Text = "";
            txtVidBaslik.Text = "";
            txtVidKaynak.Text = "";
            txtVidKaynakVimeo.Text = "";
            imgKapak.Visible = false;
        }

        protected void lnkAcilirMenu_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;

            int sayfaId = lnk.CommandArgument.xToIntDefault();

            enSiteHaritasi sayfa = bllSiteHaritasi.SayfaGetir(sayfaId);

            if (sayfa.AcilirMenu)
            {
                sayfa.AcilirMenu = false;
            }
            else
            {
                sayfa.AcilirMenu = true;
            }

            bllSiteHaritasi.AcilirMenuDurumDegistir(sayfa);

            ListeYenile();
        }

        protected void rgrvResimler_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            ResimleriGetir();
        }

        protected void lnkCustomDurumDegistir_OnClick(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;

            int sayfaId = lnk.CommandArgument.xToIntDefault();

            enSiteHaritasi sayfa = bllSiteHaritasi.SayfaGetir(sayfaId);

            if (sayfa.Custom)
            {
                sayfa.Custom = false;
            }
            else
            {
                sayfa.Custom = true;
            }

            bllSiteHaritasi.CustomDurumDegistir(sayfa);

            ListeYenile();

        }

        //protected void SayfaBloklariGetir()
        //{
        //    List<enBlok> bloks = new List<enBlok>();

        //    if (treeSayfalar.SelectedValue == "M")
        //    {
        //        bloks = bllSiteHaritasi_Blok.MasterGetir(null);
        //    }
        //    else
        //    {
        //        int? pozId = ddlSayfaBloklariPozisyonlar.SelectedValue.xToInt();
        //        int sayfaId = treeSayfalar.SelectedValue.xToIntDefault();

        //        bloks = bllSiteHaritasi_Blok.Getir(sayfaId, pozId);
        //    }

        //    rgrvSayfaBloklari.DataSource = bloks;
        //    rgrvSayfaBloklari.DataBind();
        //}

        //protected void MasterBloklariGetir()
        //{
        //    rgrvSayfaBloklari.DataSource = bllSiteHaritasi_Blok.MasterGetir(null);
        //    rgrvSayfaBloklari.DataBind();
        //}

        protected void lnkSayfaBlokKaldır_OnClick(object sender, EventArgs e)
        {
            //int sayfaId = treeSayfalar.SelectedValue.xToIntDefault();
            int pozisyonId = (sender as LinkButton).Attributes["PozisyonId"].xToIntDefault();
            int blokId = (sender as LinkButton).CommandArgument.xToIntDefault();

            bllSiteHaritasi_Blok.Kaldir(pozisyonId, blokId);

            //SayfaBloklariGetir();
        }

        protected void treeSayfalar_OnNodeClick(object sender, RadTreeNodeEventArgs e)
        {
            trArrow.Visible = true;
            trInfo.Visible = true;

            //SayfaBloklariGetir();
            BloklariGetir();

            List<enBlokPozisyon> pozs = bllBlokPozisyonlari.Getir();

            ddlSayfaBloklariPozisyonlar.DataBind(pozs, "Id", "Adi", "Tümü");

            //if (treeSayfalar.SelectedValue == "M")
            //{
            //    frm.Attributes.Add("src", "/BlokDuzenle.aspx");
            //}
            //else
            //{
            //    frm.Attributes.Add("src", "/BlokDuzenle.aspx?s=" + treeSayfalar.SelectedValue.xToIntDefault());
            //}
        }

        protected void chkBlokBaslik_OnCheckedChanged(object sender, EventArgs e)
        {
            int blokId = (sender as CheckBox).Attributes["BlokId"].xToIntDefault();
            int pozisyonId = (sender as CheckBox).Attributes["PozisyonId"].xToIntDefault();
            int sayfaId = treeSayfalar.SelectedValue.xToIntDefault();

            enSiteHaritasi_Blok sBlok = bllSiteHaritasi_Blok.Getir(pozisyonId, blokId, sayfaId);

            sBlok.BaslikKullanimi = !sBlok.BaslikKullanimi;
            sBlok.SayfaId = sayfaId;
            sBlok.BlokId = blokId;
            sBlok.PozisyonId = pozisyonId;

            bllSiteHaritasi_Blok.BaslikKullanimiGuncelle(sBlok);

            //SayfaBloklariGetir();
        }

        protected void chkBlokCerceve_OnCheckedChanged(object sender, EventArgs e)
        {
            int sayfaId = treeSayfalar.SelectedValue.xToIntDefault();
            int pozisyonId = (sender as CheckBox).Attributes["PozisyonId"].xToIntDefault();
            int blokId = (sender as CheckBox).Attributes["BlokId"].xToIntDefault();

            enSiteHaritasi_Blok sBlok = bllSiteHaritasi_Blok.Getir(pozisyonId, blokId, sayfaId);

            sBlok.CerceveKullanimi = !sBlok.CerceveKullanimi;
            sBlok.SayfaId = sayfaId;
            sBlok.BlokId = blokId;
            sBlok.PozisyonId = pozisyonId;

            bllSiteHaritasi_Blok.CerceveKullanimiGuncelle(sBlok);

            //SayfaBloklariGetir();
        }

        protected void txtBlokSiraNo_OnTextChanged(object sender, EventArgs e)
        {
            int blokId = (sender as RadTextBox).Attributes["BlokId"].xToIntDefault();
            int sayfaId = treeSayfalar.SelectedValue.xToIntDefault();

            enSiteHaritasi_Blok sBlok = bllSiteHaritasi_Blok.Getir(sayfaId, blokId);

            sBlok.Sira = (sender as RadTextBox).Text.xToIntDefault();
            sBlok.SayfaId = sayfaId;
            sBlok.BlokId = blokId;

            bllSiteHaritasi_Blok.SiraGuncelle(sBlok);

        }

        protected void btnPozisyonSec_OnClick(object sender, EventArgs e)
        {
            int sayfaId = treeSayfalar.SelectedValue.xToIntDefault();
            int blokId = hdnBlokId.Value.xToIntDefault();

            enSiteHaritasi_Blok sBlok = bllSiteHaritasi_Blok.Getir(sayfaId, blokId);

            sBlok.PozisyonId = ddlPozisyonSec.SelectedValue.xToIntDefault();
            sBlok.SayfaId = sayfaId;
            sBlok.BlokId = blokId;

            if (hdnBlokEkleme.Value.xBosMu())
            {
                bllSiteHaritasi_Blok.PozisyonGuncelle(sBlok);
            }
            else
            {
                if (bllSiteHaritasi_Blok.PozisyondaVarMi(sBlok))
                {
                    uscUyari1.UyariGoster("Bu blok seçitiğiniz pozisyona daha önce eklenmiş", "Pozisyon Müsait Değil !", false);
                    return;
                }
                else
                {
                    bllSiteHaritasi_Blok.Ekle(sBlok);
                }
            }

            //SayfaBloklariGetir();
        }

        protected void PozisyonlariDoldur()
        {
            List<enBlokPozisyon> pozs = bllBlokPozisyonlari.Getir();

            ddlPozisyonSec.DataBind(pozs, "Id", "Adi", "Seçiniz");
        }

        protected void txtYukseklik_OnTextChanged(object sender, EventArgs e)
        {
            int blokId = (sender as RadTextBox).Attributes["BlokId"].xToIntDefault();
            int sayfaId = treeSayfalar.SelectedValue.xToIntDefault();
            int pozisyonId = (sender as RadTextBox).Attributes["PozisyonId"].xToIntDefault();

            enSiteHaritasi_Blok sBlok = bllSiteHaritasi_Blok.Getir(pozisyonId, blokId, sayfaId);

            sBlok.Height = (sender as RadTextBox).Text.xToIntDefault().ToString();
            sBlok.SayfaId = sayfaId;
            sBlok.BlokId = blokId;

            bllSiteHaritasi_Blok.YukseklikGuncelle(sBlok);
        }

        protected void rcpCerceveRengi_OnColorChanged(object sender, EventArgs e)
        {
            int blokId = (sender as RadColorPicker).Attributes["BlokId"].xToIntDefault();
            int sayfaId = treeSayfalar.SelectedValue.xToIntDefault();
            int pozisyonId = (sender as RadColorPicker).Attributes["PozisyonId"].xToIntDefault();

            enSiteHaritasi_Blok sBlok = bllSiteHaritasi_Blok.Getir(pozisyonId, blokId, sayfaId);

            sBlok.CerceveRengi = ColorTranslator.ToHtml((sender as RadColorPicker).SelectedColor);
            sBlok.SayfaId = sayfaId;
            sBlok.BlokId = blokId;

            bllSiteHaritasi_Blok.CerceveRengiGuncelle(sBlok);
        }

        protected void rcpArkaplanRengi_OnColorChanged(object sender, EventArgs e)
        {
            int blokId = (sender as RadColorPicker).Attributes["BlokId"].xToIntDefault();
            int sayfaId = treeSayfalar.SelectedValue.xToIntDefault();
            int pozisyonId = (sender as RadColorPicker).Attributes["PozisyonId"].xToIntDefault();

            enSiteHaritasi_Blok sBlok = bllSiteHaritasi_Blok.Getir(pozisyonId, blokId, sayfaId);

            sBlok.ArkaplanRengi = ColorTranslator.ToHtml((sender as RadColorPicker).SelectedColor);
            sBlok.SayfaId = sayfaId;
            sBlok.BlokId = blokId;

            bllSiteHaritasi_Blok.ArkaPlanRengiGuncelle(sBlok);
        }

        protected void rcpMetinRengi_OnColorChanged(object sender, EventArgs e)
        {
            int blokId = (sender as RadColorPicker).Attributes["BlokId"].xToIntDefault();
            int sayfaId = treeSayfalar.SelectedValue.xToIntDefault();
            int pozisyonId = (sender as RadColorPicker).Attributes["PozisyonId"].xToIntDefault();

            enSiteHaritasi_Blok sBlok = bllSiteHaritasi_Blok.Getir(pozisyonId, blokId, sayfaId);

            sBlok.MetinRengi = ColorTranslator.ToHtml((sender as RadColorPicker).SelectedColor);
            sBlok.SayfaId = sayfaId;
            sBlok.BlokId = blokId;

            bllSiteHaritasi_Blok.MetinRengiGuncelle(sBlok);
        }

        protected void BloklariGetir()
        {
            rgrvBloklar.DataSource = bllBloklar.BloklariGetir(null);
            rgrvBloklar.DataBind();
        }

        protected void ddlSayfaBloklariPozisyonlar_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //SayfaBloklariGetir();
        }

        protected void lnkMenuGorunurluk_OnClick(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;

            int sayfaId = lnk.CommandArgument.xToIntDefault();

            enSiteHaritasi sayfa = bllSiteHaritasi.SayfaGetir(sayfaId);

            if (sayfa.Menu)
            {
                sayfa.Menu = false;
            }
            else
            {
                sayfa.Menu = true;
            }

            bllSiteHaritasi.MenuDegistir(sayfa);

            ListeYenile();
        }

        protected void lnkYanMenuGorunurluk_OnClick(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;

            int sayfaId = lnk.CommandArgument.xToIntDefault();

            enSiteHaritasi sayfa = bllSiteHaritasi.SayfaGetir(sayfaId);

            if (sayfa.YanMenu)
            {
                sayfa.YanMenu = false;
            }
            else
            {
                sayfa.YanMenu = true;
            }

            bllSiteHaritasi.YanMenuDegistir(sayfa);

            ListeYenile();

        }

        protected void lnkFooterGorunurluk_OnClick(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;

            int sayfaId = lnk.CommandArgument.xToIntDefault();

            enSiteHaritasi sayfa = bllSiteHaritasi.SayfaGetir(sayfaId);

            if (sayfa.Footer)
            {
                sayfa.Footer = false;
            }
            else
            {
                sayfa.Footer = true;
            }

            bllSiteHaritasi.FooterDegistir(sayfa);

            ListeYenile();

        }

        protected void txtResimBaslik_OnTextChanged(object sender, EventArgs e)
        {
            TextBox txt = (sender as TextBox);

            int resimId = txt.Attributes["resimId"].xToIntDefault();

            enIcerikResim resim = bllIcerikResimleri.ResimGetir(resimId);

            resim.Baslik = txt.Text;

            bllIcerikResimleri.ResimAciklamaGuncelle(resim);

            ResimleriGetir();

            txt.Focus();
        }

        protected void txtResimAciklama_OnTextChanged(object sender, EventArgs e)
        {
            TextBox txt = (sender as TextBox);

            int resimId = txt.Attributes["resimId"].xToIntDefault();

            enIcerikResim resim = bllIcerikResimleri.ResimGetir(resimId);

            resim.Aciklama = txt.Text;

            bllIcerikResimleri.ResimAciklamaGuncelle(resim);

            ResimleriGetir();

            txt.Focus();
        }

        protected void btnTumResimleriSil_OnClick(object sender, EventArgs e)
        {
            bllSiteHaritasi.TumResimleriSil(VwID.Value);

            ResimleriGetir();
            rgrvResimler.DataBind();
        }

        protected void rbAnaresim_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            GridItem item = (GridItem)rb.NamingContainer;

            HiddenField seciliResim = (HiddenField)item.Cells[5].FindControl("hfResimId");

            enIcerikResim anaResim = bllIcerikResimleri.AnaResimGetir(VwID);
            anaResim.AnaResim = false;

            enIcerikResim yeniAnaResim = bllIcerikResimleri.ResimGetir(seciliResim.Value.xToIntDefault());
            yeniAnaResim.AnaResim = true;

            bllIcerikResimleri.AnaResimYap(anaResim);
            bllIcerikResimleri.AnaResimYap(yeniAnaResim);
            rgrvResimler.Rebind();
        }

        private void AnaResimIsaretle()
        {
            for (int i = 0; i < rgrvResimler.MasterTableView.Items.Count; i++)
            {
                HiddenField hfAnaResim = (HiddenField)rgrvResimler.MasterTableView.Items[i].Cells[5].FindControl("hfAnaResim");

                if (hfAnaResim.Value.xToBooleanDefault())
                {
                    RadioButton rbAnaResim = (RadioButton)rgrvResimler.MasterTableView.Items[i].Cells[5].FindControl("rbAnaresim");
                    rbAnaResim.Checked = true;
                }
            }
        }

        protected void rgrvResimler_PreRender(object sender, EventArgs e)
        {
            foreach (GridDataItem item in rgrvResimler.MasterTableView.Items)
            {
                HiddenField hfAnaResim = (HiddenField)item.Cells[5].FindControl("hfAnaResim");

                if (hfAnaResim.Value.xToBooleanDefault())
                {
                    RadioButton rbAnaResim = (RadioButton)item.Cells[5].FindControl("rbAnaresim");
                    HiddenField hfResimId = (HiddenField)item.Cells[5].FindControl("hfResimId");
                    VwAnaResimId = hfResimId.xToInt();
                    rbAnaResim.Checked = true;
                }
            }
        }

        protected void lnkVitrin_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;

            int sayfaId = lnk.CommandArgument.xToIntDefault();
            enSiteHaritasi sayfa = bllSiteHaritasi.SayfaGetir(sayfaId);

            if (sayfa.Vitrin)
            {
                bllSiteHaritasi.VitrinGoster(sayfaId, false);
                ListeYenile();
            }

            else
            {
                bllSiteHaritasi.VitrinGoster(sayfaId, true);
                ListeYenile();
            }
        }

        protected void trlSiteHaritasi_ItemDataBound(object sender, TreeListItemDataBoundEventArgs e)
        {
            bool urunMu = false;

            if (e.Item is TreeListDataItem)
            {
                TreeListDataItem item = e.Item as TreeListDataItem;
                urunMu = (item["unVitrin"].FindControl("hdnUrunMu") as HiddenField).Value.xToBooleanDefault();

                if (urunMu == false)
                {
                    LinkButton lnkVitrin = (item["unVitrin"].FindControl("lnkVitrin") as LinkButton);
                    lnkVitrin.Visible = false;
                }
            }

        }
    }
}