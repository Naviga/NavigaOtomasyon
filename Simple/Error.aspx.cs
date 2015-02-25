using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Entity;

namespace Ws
{
    public partial class Error : CustomPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var error = Server.GetLastError();
                var code = (error is HttpException) ? (error as HttpException).GetHttpCode() : 500;

                if (code == 404)
                {
                    ltrSiteHaritasi.Text = "<hr/><p>Aşağıdaki sayfalardan birini deneyin !</p>" + GetNavigationHTML();
                }


            }
        }

        private string GetNavigationHTML()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<ul id='ulUst'>");

            List<enSiteHaritasi> sayfalar = bllSiteHaritasi.AktifUstSayfalariGetirList();

            int i = 0;
            foreach (enSiteHaritasi sayfa in sayfalar)
            {

                List<enSiteHaritasi> altSayfalar = bllSiteHaritasi.AktifAltSayfalariGetirList(sayfa.Id);

                string ddClass = altSayfalar.Count > 0 && sayfa.AcilirMenu ? " class='has-dropdown'" : "";

                sb.Append("<li" + ddClass + "><a title='" + sayfa.Title + "' class='main-menu-a' href='" + sayfa.Url + "'>" + sayfa.Adi + "</a>");


                if (altSayfalar.Count > 0 && sayfa.AcilirMenu)
                {
                    AltSayfalariYaz(sb, altSayfalar);
                }

                sb.Append("</li>");

                i++;
            }

            sb.Append("</ul>");

            return sb.ToString();
        }

        private void AltSayfalariYaz(StringBuilder sb, List<enSiteHaritasi> altSayfalar)
        {
            sb.Append("<ul>");

            int i = 0;
            foreach (enSiteHaritasi altSayfa in altSayfalar)
            {
                List<enSiteHaritasi> altSayfalar2 = bllSiteHaritasi.AktifAltSayfalariGetirList(altSayfa.Id);

                string ddClass = altSayfalar2.Count > 0 && altSayfa.AcilirMenu ? " class='has-dropdown'" : "";

                if (altSayfa.DefaultSayfa)
                {
                    sb.Append("<li" + ddClass + "><a title='" + altSayfa.Title + "' href='" + altSayfa.Url + "'>" + altSayfa.Adi + "</a>");
                }
                else
                {
                    sb.Append("<li" + ddClass + "><a title='" + altSayfa.Title + "' href='" + altSayfa.Url + "'>" + altSayfa.Adi + "</a>");
                }

                if (altSayfalar2.Count > 0 && altSayfa.AcilirMenu)
                {
                    AltSayfalariYaz(sb, altSayfalar2);
                }

                sb.Append("</li>");

                i++;
            }

            sb.Append("</ul>");
        }
    }
}