using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Entity;

namespace Ws.admin
{
    public partial class Carousel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                carousellariGetir();
            }
        }

        protected void carousellariGetir()
        {
            List<enCarousel> carousellar = bllCarousel.CarouselleriGetir(null);

            rgrvCarousel.DataSource = carousellar;
            rgrvCarousel.DataBind();
        }

        protected void lnkStatuDegistir_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;

            int carouselId = lnk.CommandArgument.xToIntDefault();

            enCarousel carousel = bllCarousel.Getir(carouselId);

            if (carousel.Statu)
            {
                carousel.Statu = false;
            }
            else
            {
                carousel.Statu = true;
            }

            bllCarousel.StatuDegistir(carousel);

            carousellariGetir();
        }

        protected void btnYeniCarousel_OnClick(object sender, EventArgs e)
        {
            enCarousel carousel = new enCarousel();

            carousel.Adi = txtYeniCarouselAdi.Text;
            carousel.TekrarSayisi = txtTekrarSayisi.Text.xToIntDefault();
            carousel.GosterimSuresi = txtGosterimSuresi.Text.xToIntDefault() * 1000;

            int carouselId = bllCarousel.YeniEkle(carousel);

            Response.Redirect("CarouselDuzenle.aspx?id=" + carouselId);
        }

        protected void lnkSil_OnClick(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;

            int carouselId = lnk.CommandArgument.xToIntDefault();

            bllCarousel.Sil(carouselId);

            carousellariGetir();
        }

        protected void lnkOnizleme_OnClick(object sender, EventArgs e)
        {
            int carouseId = (sender as LinkButton).CommandArgument.xToIntDefault();

            enCarousel carousel = bllCarousel.Getir(carouseId);

            List<enCarouselResim> resimler = bllCarouselResimleri.ResimleriGetir(carouseId, true);

            StringBuilder sb = new StringBuilder();

            string guid = Guid.NewGuid().ToString();

            sb.Append("<div id='" + guid + "' class='owl-carousel'>");

            string thumbStr = carousel.TekrarSayisi > 1 ? "th " : "";

            foreach (enCarouselResim resim in resimler)
            {
                string fvLink = "";

                if (resim.VideoLink)
                {
                    fvLink = "fancybox-media' data-fancybox-group='gallery'";
                }

                if (resim.FotoLink)
                {
                    fvLink = "picture-gallery'";
                    resim.NavUrl = resim.Buyuk;
                }

                sb.Append(@"<div class='item'>
                    <a class='" + thumbStr + " " + fvLink + " href='" + resim.NavUrl + @"'" + (resim.NavUrl.Contains("#!") ? "" : " target='_blank'") + @">
                        <img src='" + resim.Orta + @"' alt='' />
                        <br/>" + resim.Baslik + @"
                    </a>
                </div>");
            }

            sb.Append("</div>");
            sb.Append(@"<script>$('#" + guid + @"').owlCarousel({
                    autoPlay: " + carousel.GosterimSuresi + @", 
                    items: " + carousel.TekrarSayisi + @",
                    itemsDesktop: [1199, 3],
                    itemsDesktopSmall: [979, 3]
                });</script>");

            ltrOnizleme.Text = sb.ToString();

        }

        protected void lnkCarouselSec_OnClick(object sender, EventArgs e)
        {
            int carouseId = (sender as LinkButton).CommandArgument.xToIntDefault();

            int sayfaId = Request.QueryString["id"].xToIntDefault();

            bool carouselSec = !CarouselSeciliMi(carouseId);

            bllSiteHaritasi.CarouselSec(sayfaId, carouseId, carouselSec);

            carousellariGetir();
        }

        protected bool CarouselSeciliMi(int carouselId)
        {
            int sayfaId = Request.QueryString["id"].xToIntDefault();
            enSiteHaritasi sayfa = bllSiteHaritasi.SayfaGetir(sayfaId);

            return sayfa.CarouselId == carouselId;
        }
    }
}