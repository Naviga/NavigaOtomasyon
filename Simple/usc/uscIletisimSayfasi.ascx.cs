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
    public partial class uscIletisimSayfasi : System.Web.UI.UserControl
    {
        public int SayfaId { get; set; }
        public string SayfaYolu { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }
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

                    sb.Insert(0, "<li><a href='" + pSayfa.Url + "'>" + pSayfa.Adi + "</a></li>");

                    if (pSayfa.Parent == null)
                    {
                        top = true;
                    }

                    sayfa = pSayfa;
                }
            }
            sb.Insert(0, "<ul class='breadcrumbs reset-margin left'>");
            sb.Append("<li class='current'><a href='" + gSayfa.Url + "'>" + gSayfa.Adi + "</a></li>");
            sb.Append("</ul>");

            lblSiteMap.Text = sb.ToString();
        }
    }
}