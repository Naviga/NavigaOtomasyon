using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Entity;
using Telerik.Web.UI;

namespace Ws.admin
{
    public partial class CarouselDuzenle : System.Web.UI.Page
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarouselResimleriniGetir();
            }

            foreach (UploadedFile file in rauResim.UploadedFiles)
            {
                string guid = System.Guid.NewGuid().ToString();

                file.SaveAs(Server.MapPath("~/yukleme/resim/carouselResim/temp/" + guid + file.GetExtension()), true);
                VwResimler.Add(guid + file.GetExtension());
            }
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

        protected void CarouselResimleriniGetir()
        {
            int CarouselId = Request.QueryString["id"].xToIntDefault();

            enCarousel carousel = bllCarousel.Getir(CarouselId);

            txtTekrarSayisi.Text = carousel.TekrarSayisi.ToString();
            txtAdi.Text = carousel.Adi;
            txtGosterimSuresi.Text = (carousel.GosterimSuresi / 1000).ToString();
            

            List<enCarouselResim> resimler = bllCarouselResimleri.ResimleriGetir(CarouselId, null);

            rgrvCarousel.DataSource = resimler;
            rgrvCarousel.DataBind();
        }

        protected void btnResimYukle_click(object sender, EventArgs e)
        {
            enCarousel Carousel = bllCarousel.Getir(Request.QueryString["id"].xToIntDefault());

            foreach (string resimUrl in VwResimler)
            {
                ResimKucult("~/yukleme/resim/CarouselResim/temp/", resimUrl, 1920, "~/yukleme/resim/CarouselResim/", true);

                enCarouselResim resim = new enCarouselResim();

                resim.CarouselId = Request.QueryString["id"].xToIntDefault();
                resim.Sira = bllCarouselResimleri.SonSiraNoGetir(resim.CarouselId) + 1;
                resim.Statu = false;
                resim.Kucuk = "/yukleme/resim/CarouselResim/" + resimUrl;
                resim.Orta = "/yukleme/resim/CarouselResim/" + resimUrl;
                resim.Buyuk = "/yukleme/resim/CarouselResim/" + resimUrl;
                resim.NavUrl = "#!";

                bllCarouselResimleri.YeniResimEkle(resim);
            }

            VwResimler.Clear();
            CarouselResimleriniGetir();
        }

        protected void lnkStatuDegistir_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;

            int resimId = lnk.CommandArgument.xToIntDefault();

            enCarouselResim resim = bllCarouselResimleri.ResimGetir(resimId);

            resim.Statu = !resim.Statu;

            bllCarouselResimleri.ResimStatuGuncelle(resim);

            CarouselResimleriniGetir();
        }

        protected void lnkSil_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;

            int resimId = lnk.CommandArgument.xToIntDefault();

            enCarouselResim resim = bllCarouselResimleri.ResimGetir(resimId);

            try
            {
                bllCarouselResimleri.ResimSil(resimId);

                CarouselResimleriniGetir();
            }
            catch
            {
                uscUyari1.UyariGoster("Resim silinirken hata oluştu ! Lütfen tekrar deneyiniz.", "Resim Silme", false);
            }


        }

        protected void txtSiraNo_TextChanged(object sender, EventArgs e)
        {
            RadTextBox txt = sender as RadTextBox;

            int id = txt.Attributes["dzID"].xToIntDefault();

            enCarouselResim resim = bllCarouselResimleri.ResimGetir(id);

            resim.Sira = txt.Text.xToIntDefault();

            bllCarouselResimleri.ResimSiraGuncelle(resim);

            CarouselResimleriniGetir();
        }

        protected void lnkAciklama_Click(object sender, EventArgs e)
        {
            (sender as LinkButton).Parent.FindControl("tblDuzenle").Visible = true;
            (sender as LinkButton).Parent.FindControl("lnkAciklama").Visible = false;
        }

        protected void lnkUrl_Click(object sender, EventArgs e)
        {
            (sender as LinkButton).Parent.FindControl("tblUrlDuzenle").Visible = true;
            (sender as LinkButton).Parent.FindControl("lnkUrl").Visible = false;
        }

        protected void btnAciklamaKaydet_Click(object sender, EventArgs e)
        {
            RadButton btn = sender as RadButton;

            RadTextBox txt = btn.Parent.FindControl("txtAciklama") as RadTextBox;

            int id = btn.CommandArgument.xToIntDefault();

            enCarouselResim resim = bllCarouselResimleri.ResimGetir(id);

            resim.Baslik = txt.Text;

            bllCarouselResimleri.ResimBaslikGuncelle(resim);

            LinkButton lnk = btn.Parent.FindControl("lnkAciklama") as LinkButton;

            lnk.Text = txt.Text;

            (sender as RadButton).Parent.FindControl("tblDuzenle").Visible = false;
            (sender as RadButton).Parent.FindControl("lnkAciklama").Visible = true;

        }

        protected void btnAciklamaIptal_Click(object sender, EventArgs e)
        {
            (sender as RadButton).Parent.FindControl("tblDuzenle").Visible = false;
            (sender as RadButton).Parent.FindControl("lnkAciklama").Visible = true;
        }

        protected void btnURLKaydet_Click(object sender, EventArgs e)
        {
            RadButton btn = sender as RadButton;

            RadTextBox txt = btn.Parent.FindControl("txtURL") as RadTextBox;

            int id = btn.CommandArgument.xToIntDefault();

            enCarouselResim resim = bllCarouselResimleri.ResimGetir(id);

            resim.NavUrl = txt.Text;

            bllCarouselResimleri.NavUrlGuncelle(resim);

            LinkButton lnk = btn.Parent.FindControl("lnkUrl") as LinkButton;

            lnk.Text = txt.Text;

            (sender as RadButton).Parent.FindControl("tblUrlDuzenle").Visible = false;
            (sender as RadButton).Parent.FindControl("lnkUrl").Visible = true;
        }

        protected void btnURLIptal_Click(object sender, EventArgs e)
        {
            (sender as RadButton).Parent.FindControl("tblUrlDuzenle").Visible = false;
            (sender as RadButton).Parent.FindControl("lnkUrl").Visible = true;
        }

        protected void chkFotoLink_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = sender as CheckBox;

            int id = chk.Attributes["ResId"].xToIntDefault();

            enCarouselResim resim = bllCarouselResimleri.ResimGetir(id);

            if (!resim.FotoLink)
            {
                resim.FotoLink = true;
                resim.VideoLink = false;
            }
            else
            {
                resim.FotoLink = false;
            }
            bllCarouselResimleri.FotoVideoLinkGuncelle(resim);

            CarouselResimleriniGetir();
        }

        protected void chkVideoLink_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = sender as CheckBox;

            int id = chk.Attributes["ResId"].xToIntDefault();

            enCarouselResim resim = bllCarouselResimleri.ResimGetir(id);

            if (!resim.VideoLink)
            {
                resim.VideoLink = true;
                resim.FotoLink = false;
            }
            else
            {
                resim.VideoLink = false;
            }

            bllCarouselResimleri.FotoVideoLinkGuncelle(resim);

            CarouselResimleriniGetir();

        }

        protected void btnKaydet_OnClick(object sender, EventArgs e)
        {
            enCarousel Carousel = bllCarousel.Getir(Request.QueryString["id"].xToIntDefault());

            Carousel.TekrarSayisi = txtTekrarSayisi.Text.xToIntDefault();
            Carousel.Adi = txtAdi.Text;
            Carousel.GosterimSuresi = txtGosterimSuresi.Text.xToIntDefault()*1000;

            bllCarousel.Duzenle(Carousel);

            CarouselResimleriniGetir();
        }
    }
}