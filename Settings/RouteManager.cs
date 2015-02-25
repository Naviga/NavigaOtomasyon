using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Routing;
using System.Xml;
using BLL;
using Entity;
using Microsoft.Win32.SafeHandles;

namespace Settings
{
    public static class RouteManager
    {
        public static void SaveRoutes(RouteCollection routes)
        {
            routes.Ignore("{resource}.axd/{*pathInfo}");

            List<enSiteHaritasi> sayfalar = bllSiteHaritasi.TumSayfalariGetir();

            foreach (enSiteHaritasi sayfa in sayfalar)
            {
                if (sayfa.Url == "/") continue;
                if (sayfa.Url.Contains("http://") || sayfa.Url.Contains("https://")) continue;

                string url = sayfa.Url.StartsWith("/") ? sayfa.Url.Remove(0, 1) : sayfa.Url;

                if (sayfa.FizikselUrl.xBosMu())
                {
                    routes.MapPageRoute(url + sayfa.Id, url, "~/Sayfa.aspx", false, new RouteValueDictionary { { "Id", sayfa.Id } });
                }
                else
                {
                    routes.MapPageRoute(url + sayfa.Id, url, sayfa.FizikselUrl, false, new RouteValueDictionary { { "Id", sayfa.Id } });
                }
            }

            CreateSiteMap(sayfalar);
        }

        private static void CreateSiteMap(List<enSiteHaritasi> sayfalar)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(@"<?xml version='1.0' encoding='UTF-8'?>
                                    <urlset xmlns='http://www.sitemaps.org/schemas/sitemap/0.9'>");

            foreach (enSiteHaritasi sayfa in sayfalar)
            {
                if (sayfa.Url == "/") continue;
                if (sayfa.Url.Contains("http://") || sayfa.Url.Contains("https://")) continue;

                string url = sayfa.Url.StartsWith("/") ? sayfa.Url.Remove(0, 1) : sayfa.Url;

                sb.Append("<url>");
                sb.Append("<loc>http://" + HttpContext.Current.Request.Url.Host + "/" + url + "</loc>");
                sb.Append("</url>");

                if (sayfa.FotoGaleriMi)
                {
                    List<enIcerikResim> resimler = bllIcerikResimleri.ResimleriGetir(sayfa.Id, true);

                    foreach (enIcerikResim resim in resimler)
                    {
                        sb.Append("<url>");
                        sb.Append("<loc>http://" + HttpContext.Current.Request.Url.Host + "/" + url + "?no=" + resim.Id + "</loc>");
                        sb.Append("</url>");
                    }
                }

            }



            sb.Append("</urlset>");

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(sb.ToString());
            doc.Save(HttpContext.Current.Server.MapPath("~/sitemap.xml"));
        }

        public static void Refresh()
        {
            RouteTable.Routes.Clear();

            SaveRoutes(RouteTable.Routes);
        }
    }
}
