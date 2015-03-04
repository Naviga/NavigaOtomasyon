using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Entity;

namespace Ws.usc
{
    public partial class uscHaberSayfa : System.Web.UI.UserControl
    {
        public int SayfaId { get; set; }
        public string SayfaYolu { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string CarouselOlustur(int? carouselId)
        {
            if (carouselId == null)
            {
                return "";
            }
            enCarousel carousel = bllCarousel.Getir(carouselId.Value);

            List<enCarouselResim> resimler = bllCarouselResimleri.ResimleriGetir(carouselId.Value, true);

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
                    itemsDesktop: [1199, " + carousel.TekrarSayisi + @"],
                    itemsDesktopSmall: [979, " + carousel.TekrarSayisi + @"]
                });</script>");

            return sb.ToString();
        }

        protected void SiteMapOlustur(enSiteHaritasi gSayfa)
        {
            bool top = false;

            StringBuilder sb = new StringBuilder();



            if (gSayfa.Parent != null)
            {
                enSiteHaritasi sayfa = gSayfa;

                while (!top)
                {
                    enSiteHaritasi pSayfa = bllSiteHaritasi.SayfaGetir(sayfa.Parent.Value);

                    sb.Insert(0, "<li><a href='" + pSayfa.DisplayUrl + "'>" + pSayfa.Adi + "</a></li>");

                    if (pSayfa.Parent == null)
                    {
                        top = true;
                    }

                    sayfa = pSayfa;
                }
            }
            sb.Insert(0, "<ul class='breadcrumbs reset-margin left'>");
            sb.Append("<li class='current'><a href='" + gSayfa.DisplayUrl + "'>" + gSayfa.Adi + "</a></li>");
            sb.Append("</ul>");

            lblSiteMap.Text = sb.ToString();
        }
    }
}