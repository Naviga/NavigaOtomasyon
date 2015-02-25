using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BLL;
using Common;
using Entity;

namespace Ws.masterPages
{
    public partial class Main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SessionManager.Admin = new enAdmin();

                if (Request.Url.AbsoluteUri.Contains("?logout"))
                {
                    SessionManager.Admin = null;
                }

                AnalyticsYaz();
                YayinDurumuKontrol();

                ltrFinex.Text = "<a href='http://www.finexmedia.com' title='Finex Media tarafından tasarlanmış ve geliştirilmiştir.' target='_blank'>Developed by<br/><img src='http://finexmedia.com/yukleme/resim/SolUst/logo_h_75.png' alt='Finex Media' style='height:25px !important' />";
            }
        }

        protected void YayinDurumuKontrol()
        {
            enGenelAyar ayrYayinDur = bllGenelAyarlar.GenelAyarGetir(enEnumaration.enmGenelAyarlar.YayinDurumu);

            if (!ayrYayinDur.Icerik.xToBooleanDefault())
            {
                Response.Redirect("~/uyari.html");
            }
        }

        protected void AnalyticsYaz()
        {
            enGenelAyar ayar = bllGenelAyarlar.GenelAyarGetir(enEnumaration.enmGenelAyarlar.AnalyticsKodu);

            if (!ayar.Icerik.xBosMu())
            {
                ltrAnalytics.Text = @"<script>
                                      (function(i,s,o,g,r,a,m){i['GoogleAnalyticsObject']=r;i[r]=i[r]||function(){
                                      (i[r].q=i[r].q||[]).push(arguments)},i[r].l=1*new Date();a=s.createElement(o),
                                      m=s.getElementsByTagName(o)[0];a.async=1;a.src=g;m.parentNode.insertBefore(a,m)
                                      })(window,document,'script','//www.google-analytics.com/analytics.js','ga');

                                      ga('create', '" + ayar.Icerik + @"', 'auto');
                                      ga('send', 'pageview');

                                    </script>";
            }
        }

        protected string GetNavigation()
        {
            List<enSiteHaritasi> pages = bllSiteHaritasi.AktifUstSayfalariGetirList();

            StringBuilder sb = new StringBuilder();

            sb.Append("<ul id='ulMainNavigation' class='left'>");

            int i = 0;
            foreach (enSiteHaritasi page in pages)
            {
                if (page.Menu)
                {
                    List<enSiteHaritasi> altSayfalar = bllSiteHaritasi.MenuAltSayfalariGetirList(page.Id, true);

                    string ddClass = altSayfalar.Count > 0 && page.AcilirMenu ? " class='has-dropdown'" : "";

                    if (SessionManager.Admin != null)
                    {
                        ddClass = " class='has-dropdown' ";
                    }

                    if (i != 0)
                    {
                        sb.Append("<li class='divider'></li>");
                    }

                    sb.Append("<li" + ddClass + "><a href='" + page.Url + "' class='main-menu-a' pageId='" + page.Id + "'>" + page.Adi + "</a>");

                    if (SessionManager.Admin != null)
                    {
                        //sb.Append("<span pageid='" + page.Id + "' class='close' onclick='DeletePage(this)'>&times;</span>");
                    }

                    if (altSayfalar.Count > 0 && page.AcilirMenu)
                    {
                        AltSayfalariYaz(sb, altSayfalar);
                    }
                    else if (SessionManager.Admin != null)
                    {

                        sb.Append("<ul class='dropdown'>");
                        sb.Append("<li><a href='#!' class='button tiny' onclick='OpenNewNavItem(this)' parentId='" + page.Id + "' title='" + bllDiziler.DiziGetir("Main.AdminMenu.NewPage.Tooltip") + "'><span class='fa fa-plus-circle'></span>&nbsp; " + bllDiziler.DiziGetir("Main.AdminMenu.NewPage") + "</a></li>");
                        sb.Append("</ul>");
                    }

                    sb.Append("</li>");
                }
            }

            if (SessionManager.Admin != null)
            {
                sb.Append("<li><a href='#!' class='button small' onclick='OpenNewNavItem(this)' title='" + bllDiziler.DiziGetir("Main.AdminMenu.NewPage.Tooltip") + "'><span class='fa fa-plus-circle'></span>&nbsp; " + bllDiziler.DiziGetir("Main.AdminMenu.NewPage") + "</a></li>");
            }

            sb.Append("</ul>");

            return sb.ToString();
        }

        private static void AltSayfalariYaz(StringBuilder sb, List<enSiteHaritasi> altSayfalar)
        {
            sb.Append("<ul class='dropdown'>");

            int i = 0;
            foreach (enSiteHaritasi altSayfa in altSayfalar)
            {
                if (i != 0)
                {
                    sb.Append("<li class='divider'></li>");
                }
                List<enSiteHaritasi> altSayfalar2 = bllSiteHaritasi.AktifAltSayfalariGetirList(altSayfa.Id);

                string ddClass = altSayfalar2.Count > 0 && altSayfa.AcilirMenu ? " class='has-dropdown'" : "";

                if (SessionManager.Admin != null)
                {
                    ddClass = " class='has-dropdown' ";
                }

                sb.Append("<li" + ddClass + "><a title='" + altSayfa.Title + "' href='" + altSayfa.Url + "' " + (altSayfa.Url.Contains("http://www.") ? "target='_blank'" : "") + ">" + altSayfa.Adi + "</a>");

                if (SessionManager.Admin != null)
                {
                    //sb.Append("<span pageid='" + altSayfa.Id + "' class='close' onclick='DeletePage(this)'>&times;</span>");
                }

                if (altSayfalar2.Count > 0 && altSayfa.AcilirMenu)
                {
                    AltSayfalariYaz(sb, altSayfalar2);
                }
                else if (SessionManager.Admin != null)
                {
                    sb.Append("<ul class='dropdown'>");
                    sb.Append("<li><a href='#!' class='button tiny' onclick='OpenNewNavItem(this)' parentId='" + altSayfa.Id + "' title='Yeni başlık ekle'><span class='fa fa-plus-circle'></span>&nbsp; Başlık Ekle</a></li>");
                    sb.Append("</ul>");
                }

                sb.Append("</li>");

                i++;
            }

            if (SessionManager.Admin != null)
            {
                sb.Append("<li><a href='#!' class='button tiny' onclick='OpenNewNavItem(this)' parentId='" + altSayfalar[0].Parent + "' title='Yeni başlık ekle'><span class='fa fa-plus-circle'></span>&nbsp; Başlık Ekle</a></li>");
            }

            sb.Append("</ul>");
        }

    }
}