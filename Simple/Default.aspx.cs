using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity;
using BLL;
using System.Text;

namespace Ws
{
    public partial class Default : System.Web.UI.Page
    {
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
    }
}