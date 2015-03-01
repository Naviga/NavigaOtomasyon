using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Entity;
using System.Text;

namespace Ws.usc
{
    public partial class uscUrunSayfa : System.Web.UI.UserControl
    {
        public int SayfaId { get; set; }
        public string SayfaYolu { get; set; }
        public string SayfaAdi { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //UrunResimleriGetir();
            }
        }

        private void UrunResimleriGetir()
        {
            //    List<enIcerikResim> resimler = bllIcerikResimleri.ResimleriGetir(SayfaId, true);
            //    rptFotoBig.DataSource = resimler;
            //    //rptFotoThumb.DataSource = resimler;

            //    rptFotoBig.DataBind();
            //    //rptFotoThumb.DataBind();
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