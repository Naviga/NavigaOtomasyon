using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Routing;
using BLL;
using Entity;

namespace Ws
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            Settings.RouteManager.SaveRoutes(RouteTable.Routes);

            YayinDurumuKontrol();

            KlasorKontrol();
        }

        //protected void SaveRoutes(System.Web.Routing.RouteCollection routes)
        //{
        //    routes.Ignore("{resource}.axd/{*pathInfo}");

        //    List<enSiteHaritasi> sayfalar = bllSiteHaritasi.AktifTumSayfalariGetir();

        //    foreach (enSiteHaritasi sayfa in sayfalar)
        //    {
        //        if (sayfa.Url == "/") continue;
        //        if (sayfa.Url.Contains("http://") || sayfa.Url.Contains("https://")) continue;

        //        string url = sayfa.Url.StartsWith("/") ? sayfa.Url.Remove(0, 1) : sayfa.Url;

        //        if (sayfa.FizikselUrl.xBosMu())
        //        {
        //            if (!sayfa.Custom)
        //            {
        //                routes.MapPageRoute(url + sayfa.Id, url, "~/Sayfa.aspx", false, new RouteValueDictionary { { "Id", sayfa.Id } });
        //            }
        //            else
        //            {
        //                routes.MapPageRoute(url + sayfa.Id, url, "~/Page.aspx", false, new RouteValueDictionary { { "Id", sayfa.Id } });
        //            }
        //        }
        //        else
        //        {
        //            routes.MapPageRoute(url + sayfa.Id, url, sayfa.FizikselUrl, false, new RouteValueDictionary { { "Id", sayfa.Id } });
        //        }
        //    }

        //}

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            //Exception exc = Server.GetLastError();

            //// Handle HTTP errors
            //if (exc.GetType() == typeof(HttpException))
            //{
            //    // The Complete Error Handling Example generates
            //    // some errors using URLs with "NoCatch" in them;
            //    // ignore these here to simulate what would happen
            //    // if a global.asax handler were not implemented.
            //    if (exc.Message.Contains("NoCatch") || exc.Message.Contains("maxUrlLength"))
            //        return;

            //    //Redirect HTTP errors to HttpError page
            //    Server.Transfer("Error.aspx");
            //}

            //// For other kinds of errors give the user some information
            //// but stay on the default page
            //Response.Write("<h2>Global Page Error</h2>\n");
            //Response.Write(
            //    "<p>" + exc.Message + "</p>\n");
            //Response.Write("Return to the <a href='Default.aspx'>" +
            //    "Default Page</a>\n");

            //// Log the exception and notify system operators
            ////ExceptionUtility.LogException(exc, "DefaultPage");
            ////ExceptionUtility.NotifySystemOps(exc);

            //// Clear the error from the server
            //Server.ClearError();
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }

        protected void YayinDurumuKontrol()
        {
            enGenelAyar ayrYayinTar = bllGenelAyarlar.GenelAyarGetir(enEnumaration.enmGenelAyarlar.YayinTarihi);
            enGenelAyar ayrYayinSuresi = bllGenelAyarlar.GenelAyarGetir(enEnumaration.enmGenelAyarlar.YayinSuresi);


            DateTime yayinTarihi = ayrYayinTar.Icerik.xToDateTimeDefault();
            int yayinSuresi = ayrYayinSuresi.Icerik.xToIntDefault();

            DateTime bitisTarihi = yayinTarihi.AddYears(yayinSuresi);

            if (bitisTarihi.Subtract(DateTime.Now).Days < 15)
            {
                if (bitisTarihi.Subtract(DateTime.Now).Days <= 0)
                {
                    enGenelAyar ayrYayinDur = bllGenelAyarlar.GenelAyarGetir(enEnumaration.enmGenelAyarlar.YayinDurumu);

                    ayrYayinDur.Icerik = "false";

                    bllGenelAyarlar.AyarGuncelle(ayrYayinDur);
                }
                else
                {
                    //lblLisansUyari.Text = "<p>Dikkat ! Web sitenizin yıllık süresini yenilemek için son " + bitisTarihi.Subtract(DateTime.Now).Days + " gün !</p>";
                }
            }
        }

        protected void KlasorKontrol()
        {
            if (!Directory.Exists(Server.MapPath("~/yukleme/")))
            {
                Directory.CreateDirectory(Server.MapPath("~/yukleme/"));
            }

            if (!Directory.Exists(Server.MapPath("~/yukleme/icerik/")))
            {
                Directory.CreateDirectory(Server.MapPath("~/yukleme/icerik/"));
            }
            if (!Directory.Exists(Server.MapPath("~/yukleme/icerik/kucuk/")))
            {
                Directory.CreateDirectory(Server.MapPath("~/yukleme/icerik/kucuk/"));
            }
            if (!Directory.Exists(Server.MapPath("~/yukleme/icerik/orta/")))
            {
                Directory.CreateDirectory(Server.MapPath("~/yukleme/icerik/orta/"));
            }
            if (!Directory.Exists(Server.MapPath("~/yukleme/icerik/buyuk/")))
            {
                Directory.CreateDirectory(Server.MapPath("~/yukleme/icerik/buyuk/"));
            }

            if (!Directory.Exists(Server.MapPath("~/yukleme/resim/")))
            {
                Directory.CreateDirectory(Server.MapPath("~/yukleme/resim/"));
            }

            if (!Directory.Exists(Server.MapPath("~/yukleme/resim/Ikon")))
            {
                Directory.CreateDirectory(Server.MapPath("~/yukleme/resim/Ikon"));
            }

            if (!Directory.Exists(Server.MapPath("~/yukleme/resim/Arkaplanlar")))
            {
                Directory.CreateDirectory(Server.MapPath("~/yukleme/resim/Arkaplanlar"));
            }

            if (!Directory.Exists(Server.MapPath("~/yukleme/resim/Logo")))
            {
                Directory.CreateDirectory(Server.MapPath("~/yukleme/resim/Logo"));
            }

            if (!Directory.Exists(Server.MapPath("~/yukleme/resim/CarouselResim")))
            {
                Directory.CreateDirectory(Server.MapPath("~/yukleme/resim/CarouselResim"));

                if (Directory.Exists(Server.MapPath("~/CarouselResim")))
                {
                    DirectoryCopy(Server.MapPath("~/CarouselResim"), Server.MapPath("~/yukleme/resim/CarouselResim"), true);
                }
            }
            if (!Directory.Exists(Server.MapPath("~/yukleme/resim/CarouselResim/temp")))
            {
                Directory.CreateDirectory(Server.MapPath("~/yukleme/resim/CarouselResim/temp"));
            }
            if (!Directory.Exists(Server.MapPath("~/yukleme/resim/CarouselResim/kucuk")))
            {
                Directory.CreateDirectory(Server.MapPath("~/yukleme/resim/CarouselResim/kucuk"));
            }
            if (!Directory.Exists(Server.MapPath("~/yukleme/resim/CarouselResim/orta")))
            {
                Directory.CreateDirectory(Server.MapPath("~/yukleme/resim/CarouselResim/orta"));
            }
            if (!Directory.Exists(Server.MapPath("~/yukleme/resim/CarouselResim/buyuk")))
            {
                Directory.CreateDirectory(Server.MapPath("~/yukleme/resim/CarouselResim/buyuk"));
            }
            if (!Directory.Exists(Server.MapPath("~/yukleme/resim/CarouselResim/buyuk")))
            {
                Directory.CreateDirectory(Server.MapPath("~/yukleme/resim/CarouselResim/buyuk"));
            }
        }

        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] dirs = dir.GetDirectories();

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            // If the destination directory doesn't exist, create it. 
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location. 
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }
    }
}