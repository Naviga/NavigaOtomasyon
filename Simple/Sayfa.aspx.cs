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
    public partial class Sayfa : CustomPage
    {
        public List<enIcerikResim> VwResimler
        {
            get
            {
                if (ViewState["VwResimler"] == null)
                    ViewState["VwResimler"] = new List<enIcerikResim>();
                return ViewState["VwResimler"] as List<enIcerikResim>;
            }
            set
            {
                ViewState["VwResimler"] = value;
            }
        }

        public int VwPhotoIndex
        {
            get
            {
                if (ViewState["VwPhotoIndex"] == null)
                    ViewState["VwPhotoIndex"] = 0;
                return ViewState["VwPhotoIndex"].xToIntDefault();
            }
            set
            {
                ViewState["VwPhotoIndex"] = value;
            }
        }

        public int VwPhotoId
        {
            get
            {
                if (ViewState["VwPhotoId"] == null)
                    ViewState["VwPhotoId"] = 0;
                return ViewState["VwPhotoId"].xToIntDefault();
            }
            set
            {
                ViewState["VwPhotoId"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (Request.QueryString["no"].xBosMu() == false)
                //{
                //    VwPhotoId = Request.QueryString["no"].xToIntDefault();
                //}

                //SayfaIcerigiGetir();


            }
        }

        protected void SayfaIcerigiGetir()
        {
            int sayfaId = 0;

            sayfaId = RouteData.Values["Id"].xToIntDefault();

            enSiteHaritasi sayfa = bllSiteHaritasi.SayfaGetir(sayfaId);

            bool altSayfaVarMi = !sayfa.FotoGaleriMi;

            altSayfaVarMi = sayfa.SayfaMenu;

            if (altSayfaVarMi)
            {
                AltSayfalariGetir(sayfa);
            }

            //lblIcerik.Text = altSayfaVarMi ? "<div class='large-9 columns icerikText'>" : "<div class='large-12 columns icerikText'>";

            //lblIcerik.Text += sayfa.Icerik.xBosMu() == false ? "<p>" + sayfa.Icerik + "</p><hr/>" : "";

            if (!sayfa.FotoGaleriMi && sayfa.FaceComments)
            {
                StringBuilder sb = new StringBuilder();

                sb.Append("<div class='row'>" +
                          "<div class='large-12'>");
                sb.Append("<div class='fb-comments' data-href='" + Request.Url.AbsoluteUri + "' data-numposts='5' data-colorscheme='light' width='100%'></div>");
                sb.Append("</div>");
                sb.Append("</div>");

                //lblIcerik.Text += sb.ToString();
            }

            //if (sayfa.Parent != null)
            //{

            //SiteMapOlustur(sayfa);
            ResimleriGetir(sayfa);
            VideolariGetir(sayfa);

            //lblIcerik.Text += "</div>";
        }

        protected bool AltSayfalariGetir(enSiteHaritasi gSayfa)
        {
            List<enSiteHaritasi> sayfalar = bllSiteHaritasi.AktifAltSayfalariGetirList(gSayfa.Id);

            if (sayfalar.Count == 0 && gSayfa.Parent == null)
            {
                return false;
            }

            if (sayfalar.Count == 0 && gSayfa.Parent != null)
                sayfalar = bllSiteHaritasi.AktifAltSayfalariGetirList(gSayfa.Parent.Value);

            StringBuilder sb = new StringBuilder();

            sb.Append("<div class='large-3 columns'><div class='panel reset-margin subPagesContainer'>");

            sb.Append("<ul class='side-nav'>");

            foreach (enSiteHaritasi sayfa in sayfalar)
            {
                var url = sayfa.Url;

                if (sayfa.DefaultSayfa)
                {
                    url = sayfa.Url;
                }

                sb.Append("<li><a href='" + url + "'>" + sayfa.Adi + "</a></li>");
            }

            sb.Append("</ul>");
            sb.Append("</div></div>");

            //lblAltSayfalar.Text = sb.ToString();

            return true;
        }

        //protected void SiteMapOlustur(enSiteHaritasi gSayfa)
        //{
        //    bool top = false;

        //    StringBuilder sb = new StringBuilder();



        //    if (gSayfa.Parent != null)
        //    {
        //        enSiteHaritasi sayfa = gSayfa;

        //        while (!top)
        //        {
        //            enSiteHaritasi pSayfa = bllSiteHaritasi.SayfaGetir(sayfa.Parent.Value);

        //            sb.Insert(0, "<li><a href='" + pSayfa.Url + "'>" + pSayfa.Adi + "</a></li>");

        //            if (pSayfa.Parent == null)
        //            {
        //                top = true;
        //            }

        //            sayfa = pSayfa;
        //        }
        //    }
        //    sb.Insert(0, "<ul class='breadcrumbs reset-margin left'>");
        //    sb.Append("<li class='current'><a href='" + gSayfa.Url + "'>" + gSayfa.Adi + "</a></li>");
        //    sb.Append("</ul>");

        //    lblSiteMap.Text = sb.ToString();
        //}

        protected void ResimleriGetir(enSiteHaritasi gSayfa)
        {
            StringBuilder sb = new StringBuilder();

            List<enIcerikResim> resimler = bllIcerikResimleri.ResimleriGetir(gSayfa.Id, true);

            if (gSayfa.FotoGaleriMi)
            {
                string nextUrl = "";
                string prevUrl = "";

                if (VwPhotoId != 0)
                {

                    for (int i = 0; i < resimler.Count; i++)
                    {
                        if (resimler[i].Id == VwPhotoId)
                        {
                            VwPhotoIndex = i;
                        }
                    }

                }

                if (resimler.Count > 1 && VwPhotoIndex + 1 < resimler.Count)
                {
                    nextUrl = gSayfa.DisplayUrl + "?no=" + resimler[VwPhotoIndex + 1].Id + "";
                }

                if (VwPhotoIndex > 0)
                {
                    prevUrl = gSayfa.DisplayUrl + "?no=" + resimler[VwPhotoIndex - 1].Id + "";
                }

                VwResimler = resimler;

                if (VwResimler.Count > 0)
                {
                    sb.Append("<form><div id='dvPhotosSearch' class='row collapse'>");
                    sb.Append("<div class='large-3 large-centered columns'>");
                    sb.Append("<div class='row collapse'>");
                    sb.Append(
                                  @"<div class='small-10 columns'>
                                        <input id='txtSearchByID' name='no' type='text' placeholder='#Photo No'>
                                    </div>
                                    <div class='small-2 columns'>
                                        <a id='btnSearchByID' href='#!' class='button prefix' onclick='FindPhoto();'>" + bllDiziler.DiziGetir("Page.Button.Find") + @"</a>
                                    </div>");
                    sb.Append("</div>");
                    sb.Append("</div>");
                    sb.Append("</div></form>");


                    sb.Append("<div id='dvPhotos' class='row galleryPhotoContainer'>");

                    if (!nextUrl.xBosMu())
                    {
                        sb.Append("<div class='photo-arrow right-arrow'><a href='" + nextUrl + "'><img src='/css/img/p_right.png' /></a></div>");
                    }
                    if (!prevUrl.xBosMu())
                    {
                        sb.Append("<div class='photo-arrow left-arrow'><a href='" + prevUrl + "'><img src='/css/img/p_left.png' /></a></div>");
                    }


                    enIcerikResim resim = VwResimler[VwPhotoIndex];

                    if (gSayfa.FotoBaslik.xBosMu() == false)
                    {
                        sb.Append("<h5>" + resim.Baslik + "</h5>");
                    }


                    sb.Append("<div class='small-12 medium-12 large-12 columns text-center'>");
                    sb.Append("<div id='dvPhotoGalleryPhotoID'><h2>#" + resim.Id + "<br/>" + resim.Baslik + "</h2></div>");
                    sb.Append("<div id='dvPhotoGalleryZoom'><a class='fancybox' href='" + resim.Buyuk + "' title='#" + resim.Id + "'><img src='/css/img/zoom.png' /></a></div>");
                    if (!nextUrl.xBosMu())
                    {
                        sb.Append("<a href='" + nextUrl + "'>");
                    }
                    sb.Append("<img class='galleryPhoto' src='" + resim.Buyuk + "' alt='" + (resim.Baslik.xBosMu() ? gSayfa.Title + " " + resim.Id : resim.Baslik) + "'>");
                    if (!nextUrl.xBosMu())
                    {
                        sb.Append("</a>");
                    }
                    sb.Append("<div class='row'><div class='large-12 text-center'>");

                    if (resim.Aciklama.xBosMu() == false)
                    {
                        sb.Append("<p>" + resim.Aciklama + "</p><hr/>");
                    }

                    if (gSayfa.FaceComments)
                    {
                        sb.Append("<div class='row'>" +
                                  "<div class='large-12'>");
                        sb.Append("<div class='fb-comments' data-href='" + Request.Url.AbsoluteUri + "' data-numposts='5' data-colorscheme='light' width='100%'></div>");
                        sb.Append("</div>");
                        sb.Append("</div>");
                    }

                    sb.Append("</div>");
                    sb.Append("<div class='row'><div class='large-8 large-centered columns text-center'>");
                    //sb.Append("<div class='large-6 columns'><a href='#!' onclick='FotoBegen(this)' fotoId='" + resim.Id + "'><img src='' alt='' /></a></div>");

                    sb.Append("</div></div>");
                    sb.Append("</div>");


                    sb.Append("</div>");

                    sb.Append("<hr/>");

                    if (gSayfa.FotoBaslik.xBosMu() == false)
                    {
                        sb.Append("<h5>" + gSayfa.FotoBaslik + "</h5>");
                    }

                    sb.Append("<div class='row'>");

                    int i = 1;
                    foreach (enIcerikResim thumbResim in resimler)
                    {
                        sb.Append("<div class='small-4 medium-2 large-2 columns'>");
                        sb.Append("<div class='imgThumb' style='background-image:url(" + thumbResim.Orta.Replace("~", "") + ")'><a class='th radius' href='" + gSayfa.DisplayUrl + "?no=" + thumbResim.Id + "'></a>");

                        if (thumbResim.Baslik.xBosMu() == false)
                        {
                            sb.Append("<span>#" + thumbResim.Id + " > " + thumbResim.Baslik.xLeft(100) + "</span>");
                        }
                        else
                        {
                            sb.Append("<span>#" + thumbResim.Id + "</span>");
                        }

                        sb.Append("</div>");

                        sb.Append("</div>");
                        i++;
                    }
                    sb.Append("</div>");
                    sb.Append("</div>");

                    //lblIcerik.Text += sb.ToString();
                }
            }
            else
            {
                if (resimler.Count > 0)
                {

                    sb.Append("<div id='dvPhotos' class='row'>");
                    if (gSayfa.FotoBaslik.xBosMu() == false)
                    {
                        sb.Append("<h5>" + gSayfa.FotoBaslik + "</h5>");
                    }

                    foreach (enIcerikResim resim in resimler)
                    {
                        sb.Append("<div class='small-6 medium-2 large-2 columns'>");
                        sb.Append("<div class='imgThumb' style='background-image:url(" + resim.Orta.Replace("~", "") + ")'><a class='th radius fancybox' data-fancybox-group='gallery' href='" + resim.Buyuk.Replace("~", "") + "' title='#" + resim.Id + " > " + resim.Aciklama + "'></a>");
                        if (resim.Baslik.xBosMu() == false)
                        {
                            sb.Append("<span>#" + resim.Id + " > " + resim.Baslik.xLeft(100) + "</span>");
                        }
                        else
                        {
                            sb.Append("<span>#" + resim.Id + "</span>");
                        }
                        sb.Append("</div>");

                        sb.Append("</div>");
                    }

                    sb.Append("</div>");

                    //lblIcerik.Text += sb.ToString();
                }
            }

        }

        protected void VideolariGetir(enSiteHaritasi gSayfa)
        {
            List<enIcerikVideo> videolar = bllIcerikVideolari.Getir(gSayfa.Id, true);

            if (videolar.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("<div id='dvVideos' class='row'><hr/>");

                if (gSayfa.VideoBaslik.xBosMu() == false)
                {
                    sb.Append("<h5>" + gSayfa.VideoBaslik + "</h5>");
                }

                foreach (enIcerikVideo video in videolar)
                {
                    sb.Append("<div class='small-6 medium-6 large-6 columns'>");
                    sb.Append("<div class='imgThumb' style='background-image:url(http://i.ytimg.com/vi/" + video.UrlKodu + "/0.jpg)'><a class='th radius fancybox-media' data-fancybox-group='gallery' href='" + video.Kaynak + "'>");
                    sb.Append("</a></div>");
                    sb.Append("</div>");
                }

                sb.Append("</div>");

                //lblIcerik.Text += sb.ToString();
            }
        }
    }
}