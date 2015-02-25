using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Services;
using Entity;
using BLL;
using Newtonsoft.Json;
using System.Data;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing;
using Common;
using System.Reflection;

namespace Ws.services
{
    /// <summary>
    /// Summary description for general
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class general : System.Web.Services.WebService
    {

        [WebMethod]
        public string GetLogo()
        {
            enTasarimAyar ayar = bllTasarimAyarlari.TasarimAyariGetir(enEnumaration.enmTasarimAyarlari.Logo);

            return ayar.Degeri;
        }

        [WebMethod]
        public string GetNavigation()
        {
            DataTable dt = bllSiteHaritasi.AktifUstSayfalariGetir();

            List<enSiteHaritasi> sayfalar = new List<enSiteHaritasi>();

            foreach (DataRow rw in dt.Rows)
            {
                enSiteHaritasi sayfa = new enSiteHaritasi();

                sayfa.Adi = rw["site_adi"].ToString();
                sayfa.DefaultSayfa = rw["site_default"].xToBooleanDefault();
                sayfa.Description = rw["site_description"].ToString();
                sayfa.Icerik = rw["site_icerik"].ToString();
                sayfa.Id = rw["site_id"].xToIntDefault();
                sayfa.Keywords = rw["site_keywords"].ToString();
                sayfa.Parent = rw["site_parent"].xToInt();
                sayfa.Resim = rw["site_resim"].ToString();

                if (sayfa.Resim.xBosMu())
                {
                    sayfa.Resim = "~/css/img/siyahBlokIcon.png";
                }

                sayfa.Statu = rw["site_statu"].xToBooleanDefault();
                sayfa.Title = rw["site_title"].ToString();
                sayfa.Url = rw["site_url"].ToString();
                sayfa.Sira = rw["site_sira"].xToIntDefault();

                sayfalar.Add(sayfa);
            }

            return JsonConvert.SerializeObject(sayfalar);
        }

        [WebMethod]
        public string GetNavigationHTML()
        {

            StringBuilder sb = new StringBuilder();

            sb.Append("<ul id='ulUst' class='left'>");

            List<enSiteHaritasi> sayfalar = bllSiteHaritasi.AktifUstSayfalariGetirList();

            int i = 0;
            foreach (enSiteHaritasi sayfa in sayfalar)
            {

                List<enSiteHaritasi> altSayfalar = bllSiteHaritasi.AktifAltSayfalariGetirList(sayfa.Id);

                string ddClass = altSayfalar.Count > 0 && sayfa.AcilirMenu ? " class='has-dropdown'" : "";

                if (i != 0)
                {
                    sb.Append("<li class='divider'></li>");
                }

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

        protected void AltSayfalariYaz(StringBuilder sb, List<enSiteHaritasi> altSayfalar)
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

        [WebMethod]
        public string GetSections(int sayfaId = 0)
        {
            List<enBlok> bloklar = new List<enBlok>();

            if (sayfaId.xToIntDefault() == 0)
            {
                bloklar = bllBloklar.BloklariGetir(true);
            }
            else
            {
                bloklar = bllSiteHaritasi_Blok.Getir(sayfaId.xToIntDefault());
            }

            foreach (enBlok blok in bloklar)
            {
                bllSiteHaritasi_Blok.IcerikOlustur(blok);
            }

            return JsonConvert.SerializeObject(bloklar);
        }

        [WebMethod]
        public string MasterBloklariGetir()
        {
            List<enBlok> bloklar = bllSiteHaritasi_Blok.MasterGetir(true);

            foreach (enBlok blok in bloklar)
            {
                bllSiteHaritasi_Blok.IcerikOlustur(blok);
            }

            return JsonConvert.SerializeObject(bloklar);
        }

        [WebMethod]
        public string AktifTumBloklariGetir(int sayfaId = 0)
        {
            List<enBlok> bloklar = bllBloklar.BloklariGetir(true);

            foreach (enBlok blok in bloklar)
            {
                StringBuilder sb = new StringBuilder();

                sb.Append("<div class='row collapse'><div class='large-2 columns'><img src='/admin/css/img/DragAndDropIcon.png' /></div><div class='large-10 columns'><h3>" + blok.Adi + "</h3></div></div>");

                blok.Icerik = sb.ToString();
            }

            return JsonConvert.SerializeObject(bloklar);
        }

        [WebMethod]
        public string BlokIcerikGetir(int blokId, int pozId)
        {
            //enBlok blok = bllSiteHaritasi_Blok.PozisyonaGoreBlokGetir(pozId, blokId);
            enBlok blok = bllBloklar.BlokGetir(blokId);

            bllSiteHaritasi_Blok.IcerikOlustur(blok);

            return JsonConvert.SerializeObject(blok);
        }

        [WebMethod]
        public string GetBackgroundPicture()
        {
            enTasarimAyar ayar = bllTasarimAyarlari.TasarimAyariGetir(enEnumaration.enmTasarimAyarlari.ArkaPlanResmi);

            return ayar.Degeri;
        }

        [WebMethod]
        public string MapGetir()
        {
            enGmap gmap = bllGmap.GmapGetir();

            return JsonConvert.SerializeObject(gmap);
        }

        [WebMethod(EnableSession = true)]
        public void BlokYukseklikGuncelle(string height, int blokId, int sayfaId = 0)
        {

            if (SessionManager.Admin == null) throw new Exception("Yetki yok");

            enSiteHaritasi_Blok sBlok = new enSiteHaritasi_Blok();

            sBlok.BlokId = blokId;
            sBlok.Height = height;
            sBlok.SayfaId = sayfaId;

            bllSiteHaritasi_Blok.YukseklikGuncelle(sBlok);

        }

        [WebMethod(EnableSession = true)]
        public void BlokYerGuncelle(int sira, int blokId, int pozisyon, int sayfaId = 0)
        {
            if (SessionManager.Admin == null) throw new Exception("Yetki yok");

            enSiteHaritasi_Blok sBlok = new enSiteHaritasi_Blok();

            sBlok.BlokId = blokId;
            sBlok.PozisyonId = pozisyon;
            sBlok.SayfaId = sayfaId;
            sBlok.Sira = sira;

            bllSiteHaritasi_Blok.YerSiraGuncelle(sBlok);

        }

        [WebMethod(EnableSession = true)]
        public void uploadPhoto()
        {
            if (SessionManager.Admin == null) throw new Exception("Yetki yok");

            try
            {
                var httpRequest = HttpContext.Current.Request;

                // Check if files are available
                if (httpRequest.Files.Count > 0)
                {
                    var files = new List<string>();

                    string fileName = "";

                    // interate the files and save on the server
                    foreach (string file in httpRequest.Files)
                    {
                        HttpPostedFile postedFile = httpRequest.Files[file];

                        string ext = postedFile.FileName.Split('.')[1];

                        fileName = postedFile.FileName.Split('.')[0] + "_" +
                                   DateTime.Now.ToString().Replace("/", "").Replace(".", "").Replace(":", "").Replace(" ", "") + "." + ext;

                        var filePath = HttpContext.Current.Server.MapPath("~/yukleme/resim/Logo/" + fileName);

                        postedFile.SaveAs(filePath);

                        files.Add(fileName);
                    }

                    enTasarimAyar ayar = new enTasarimAyar();

                    ayar.Id = enEnumaration.enmTasarimAyarlari.Logo;
                    ayar.Degeri = "/yukleme/resim/Logo/" + fileName;

                    bllTasarimAyarlari.TasarimAyariGuncelle(ayar);

                    HttpContext.Current.Response.Write("success");
                }
                else
                {
                    HttpContext.Current.Response.End();
                }
            }
            catch
            {
                HttpContext.Current.Response.Write("hata");
            }


        }

        [WebMethod(EnableSession = true)]
        public void uploadPagePhoto()
        {
            if (SessionManager.Admin == null) throw new Exception("Yetki yok");

            int pageId = Session["PageId"].xToIntDefault();

            try
            {
                var httpRequest = HttpContext.Current.Request;

                // Check if files are available
                if (httpRequest.Files.Count > 0)
                {
                    var files = new List<string>();

                    string fileName = "";

                    // interate the files and save on the server
                    foreach (string file in httpRequest.Files)
                    {
                        HttpPostedFile postedFile = httpRequest.Files[file];

                        var filePath = HttpContext.Current.Server.MapPath("~/yukleme/icerik/" + postedFile.FileName);

                        postedFile.SaveAs(filePath);

                        fileName = postedFile.FileName;

                        files.Add(filePath);
                    }

                    string pictureUrl = "/yukleme/icerik/" + fileName;

                    bllSiteHaritasi.ResimGuncelle(pageId, pictureUrl);

                    HttpContext.Current.Response.Write("success");
                }
                else
                {
                    HttpContext.Current.Response.End();
                }
            }
            catch
            {
                HttpContext.Current.Response.Write("hata");
            }


        }

        [WebMethod(EnableSession = true)]
        public void uploadPagePictures()
        {
            if (SessionManager.Admin == null) throw new Exception("Yetki yok");

            int pageId = Session["PageId"].xToIntDefault();

            try
            {
                var httpRequest = HttpContext.Current.Request;

                // Check if files are available
                if (httpRequest.Files.Count > 0)
                {
                    var files = new List<string>();

                    string fileName = "";

                    int i = 0;
                    // interate the files and save on the server
                    foreach (string file in httpRequest.Files)
                    {
                        HttpPostedFile postedFile = httpRequest.Files[i];

                        string ext = postedFile.FileName.Split('.')[1];

                        fileName = HttpUtility.UrlEncode(postedFile.FileName.Split('.')[0]) + "_" +
                                   DateTime.Now.ToString().Replace("/", "").Replace(".", "").Replace(":", "").Replace(" ", "") + "." + ext;

                        var filePath = HttpContext.Current.Server.MapPath("~/yukleme/" + fileName);

                        postedFile.SaveAs(filePath);

                        files.Add(fileName);
                        i++;
                    }


                    foreach (string file in files)
                    {
                        ResimKucult("~/yukleme/", file, 100, "~/yukleme/icerik/kucuk/", false);
                        ResimKucult("~/yukleme/", file, 250, "~/yukleme/icerik/orta/", false);
                        ResimKucult("~/yukleme/", file, 1024, "~/yukleme/icerik/buyuk/", true);

                        enIcerikResim resim = new enIcerikResim();

                        resim.Buyuk = "/yukleme/icerik/buyuk/" + file;
                        resim.SayfaId = pageId;
                        resim.KayitTarihi = DateTime.Now.Date;
                        resim.Kucuk = "/yukleme/icerik/kucuk/" + file;
                        resim.Orta = "/yukleme/icerik/orta/" + file;
                        resim.Sira = bllIcerikResimleri.SonSiraNoGetir(resim.SayfaId) + 1;
                        resim.Statu = true;
                        resim.Kaydeden = SessionManager.Admin.Id;
                        resim.KayitTarihi = DateTime.Now.Date;

                        bllIcerikResimleri.YeniResimEkle(resim);
                    }


                    HttpContext.Current.Response.Write("success");
                }
                else
                {
                    HttpContext.Current.Response.End();
                }
            }
            catch
            {
                HttpContext.Current.Response.Write("hata");
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

        [WebMethod(EnableSession = true)]
        public string TasarimAyarlariKaydet(Settings.Tasarim ayar)
        {
            if (SessionManager.Admin == null) throw new Exception("Yetki yok");
            //if (ayar.LogoH == 0) ayar.LogoH = null;
            //if (ayar.MenuH == 0) ayar.MenuH = null;
            //if (ayar.Sablon == 0) ayar.Sablon = null;

            try
            {
                bllTasarimAyarlari.TasarimAyariGuncelle(new enTasarimAyar { Id = enEnumaration.enmTasarimAyarlari.ArkaPlanResmi, Degeri = ayar.APResim });
                bllTasarimAyarlari.TasarimAyariGuncelle(new enTasarimAyar { Id = enEnumaration.enmTasarimAyarlari.BaglantiRengi, Degeri = ayar.Link });
                bllTasarimAyarlari.TasarimAyariGuncelle(new enTasarimAyar { Id = enEnumaration.enmTasarimAyarlari.BaslikRengi, Degeri = ayar.Baslik });
                bllTasarimAyarlari.TasarimAyariGuncelle(new enTasarimAyar { Id = enEnumaration.enmTasarimAyarlari.BlokArkaPlanRengi, Degeri = ayar.Panel });
                bllTasarimAyarlari.TasarimAyariGuncelle(new enTasarimAyar { Id = enEnumaration.enmTasarimAyarlari.GenelSablon, Degeri = ayar.Sablon.ToString() });
                bllTasarimAyarlari.TasarimAyariGuncelle(new enTasarimAyar { Id = enEnumaration.enmTasarimAyarlari.Logo, Degeri = ayar.Logo });
                bllTasarimAyarlari.TasarimAyariGuncelle(new enTasarimAyar { Id = enEnumaration.enmTasarimAyarlari.LogoAlani, Degeri = ayar.LogoAlani.ToString() });
                bllTasarimAyarlari.TasarimAyariGuncelle(new enTasarimAyar { Id = enEnumaration.enmTasarimAyarlari.LogoDikeyPozisyon, Degeri = ayar.LogoPV.ToString() });
                bllTasarimAyarlari.TasarimAyariGuncelle(new enTasarimAyar { Id = enEnumaration.enmTasarimAyarlari.LogoYatayPozisyon, Degeri = ayar.LogoPH });
                bllTasarimAyarlari.TasarimAyariGuncelle(new enTasarimAyar { Id = enEnumaration.enmTasarimAyarlari.LogoYuksekligi, Degeri = ayar.LogoH.ToString() });
                bllTasarimAyarlari.TasarimAyariGuncelle(new enTasarimAyar { Id = enEnumaration.enmTasarimAyarlari.MenuArkaPlanRengi, Degeri = ayar.MenuBg });
                bllTasarimAyarlari.TasarimAyariGuncelle(new enTasarimAyar { Id = enEnumaration.enmTasarimAyarlari.MenuAyracRengi, Degeri = ayar.MenuDiv });
                bllTasarimAyarlari.TasarimAyariGuncelle(new enTasarimAyar { Id = enEnumaration.enmTasarimAyarlari.MenuLogoGornumu, Degeri = ayar.LogoMenu.ToString() });
                bllTasarimAyarlari.TasarimAyariGuncelle(new enTasarimAyar { Id = enEnumaration.enmTasarimAyarlari.MenuYuksekligi, Degeri = ayar.MenuH.ToString() });
                bllTasarimAyarlari.TasarimAyariGuncelle(new enTasarimAyar { Id = enEnumaration.enmTasarimAyarlari.OrtaBolumRengi, Degeri = ayar.Middle });
                bllTasarimAyarlari.TasarimAyariGuncelle(new enTasarimAyar { Id = enEnumaration.enmTasarimAyarlari.TamEkranKullanim, Degeri = ayar.TamEkran.ToString() });
                bllTasarimAyarlari.TasarimAyariGuncelle(new enTasarimAyar { Id = enEnumaration.enmTasarimAyarlari.UstBolumRengi, Degeri = ayar.Header });
                bllTasarimAyarlari.TasarimAyariGuncelle(new enTasarimAyar { Id = enEnumaration.enmTasarimAyarlari.YaziRengi, Degeri = ayar.Text });

                return "Değişiklikler başarıyla kaydedildi.";
            }
            catch (Exception ex)
            {
                return "Hata ! Değişiklikler kaydedilemedi. Hata : " + ex.Message;
            }
        }


        [WebMethod(EnableSession = true)]
        public string TasarimAyarGuncelle(int id, string deger)
        {
            if (SessionManager.Admin == null) throw new Exception("Yetki yok");

            try
            {
                bllTasarimAyarlari.TasarimAyariGuncelle(new enTasarimAyar { Id = id, Degeri = deger });

                return "Değişiklikler başarıyla kaydedildi.";
            }
            catch (Exception ex)
            {
                return "Hata ! Değişiklikler kaydedilemedi. Hata : " + ex.Message;
            }
        }

        [WebMethod]
        public string GetSubPages(int id)
        {
            List<enSiteHaritasi> sayfalar = bllSiteHaritasi.AktifAltSayfalariGetirList(id);

            return JsonConvert.SerializeObject(sayfalar.Count > 0 ? sayfalar : null);
        }

        //[WebMethod(EnableSession = true)]
        //public string DiziGuncelle(int id, string deger)
        //{
        //    enDizi dizi = bllDiziler.DiziGetir(id);

        //    dizi.Degeri = deger;

        //    bllDiziler.DiziDilDuncelle(dizi, SessionManager.SeciliDilId);

        //    return deger.xToRemoveHTMLTags().xLeft(50);
        //}

        [WebMethod(EnableSession = true)]
        public string VarsayilanDiziGuncelle(int id, string deger)
        {
            if (SessionManager.Admin == null) throw new Exception("Yetki yok");

            enDizi dizi = bllDiziler.DiziGetir(id);

            dizi.VarsayilanDegeri = deger;

            bllDiziler.VarsayilanDiziDuncelle(dizi);

            return deger.xToRemoveHTMLTags().xLeft(50);
        }

        [WebMethod]
        public bool UrlVarMi(string url, string dzID)
        {
            return bllSiteHaritasi.UrlVarMi(url, dzID.xToInt());
        }

        [WebMethod(EnableSession = true)]
        public void SayfaBloklariKaldir(string arrKaldirilacaklar, int sayfaId)
        {
            if (!arrKaldirilacaklar.xBosMu())
            {
                string[] strItems = arrKaldirilacaklar.Split('|');

                for (int i = 0; i < strItems.Length; i++)
                {
                    int blokId = strItems[i].Split('-')[0].xToIntDefault();
                    int pozisyonId = strItems[i].Split('-')[1].xToIntDefault();

                    bllSiteHaritasi_Blok.Kaldir(pozisyonId, blokId, sayfaId);
                }
            }
        }


        [WebMethod(EnableSession = true)]
        public string NewPage(string name, string parent)
        {
            if (SessionManager.Admin == null) throw new Exception("Yetki yok");

            enSiteHaritasi page = new enSiteHaritasi();

            page.Adi = name;
            page.Url = "/" + name.xToUrl();
            page.Statu = true;
            page.Sira = bllSiteHaritasi.SonSiraNoGetir(page.Parent) + 1;
            page.BaslikAlani = true;
            page.SayfaYolu = true;
            page.Parent = parent.xToInt();
            page.Menu = true;
            page.SayfaMenu = true;
            page.PaylasimAlani = true;

            bllSiteHaritasi.YeniSayfaEkle(page);

            return page.Url;
        }

        [WebMethod(EnableSession = true)]
        public string SavePageContent(string pageId, string content)
        {
            if (SessionManager.Admin == null) throw new Exception("Yetki yok");

            enSiteHaritasi page = bllSiteHaritasi.SayfaGetir(pageId.xToIntDefault());

            page.Icerik = HttpUtility.UrlDecode(content);

            bllSiteHaritasi.SayfaDuzenle(page);

            return page.Url;
        }

        [WebMethod(EnableSession = true)]
        public string SavePageTitle(string pageId, string title)
        {
            if (SessionManager.Admin == null) throw new Exception("Yetki yok");

            enSiteHaritasi page = bllSiteHaritasi.SayfaGetir(pageId.xToIntDefault());

            page.Adi = title;
            page.Url = "/" + title.xToUrl();

            bllSiteHaritasi.SayfaDuzenle(page);

            return page.Url;
        }
        [WebMethod(EnableSession = true)]
        public string SavePagePicturesTitle(string pageId, string title)
        {
            if (SessionManager.Admin == null) throw new Exception("Yetki yok");

            enSiteHaritasi page = bllSiteHaritasi.SayfaGetir(pageId.xToIntDefault());

            page.FotoBaslik = title;

            bllSiteHaritasi.SayfaDuzenle(page);

            return page.Url;
        }

        [WebMethod(EnableSession = true)]
        public string SaveStringResource(string code, string value)
        {
            if (SessionManager.Admin == null) throw new Exception("Yetki yok");

            enDizi dizi = bllDiziler.DiziGetirEnt(code);

            dizi.VarsayilanDegeri = HttpUtility.UrlDecode(value);

            bllDiziler.VarsayilanDiziDuncelle(dizi);

            return value;
        }

        [WebMethod(EnableSession = true)]
        public void DeletePageImage(string imageId)
        {
            if (SessionManager.Admin == null) throw new Exception("Yetki yok");

            bllIcerikResimleri.ResimSil(imageId.xToIntDefault());

        }
    }
}
