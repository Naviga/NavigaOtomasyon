using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Common;
using Entity;

public class CustomPage : Page
{
    void Page_PreInit(Object sender, EventArgs e)
    {
        //if (SessionManager.MasterPageFile == null)
        //{
        //    enSablon sablon = bllSablonlar.SablonGetir(Settings.Tasarim.sSablon.xToIntDefault());

        //    this.MasterPageFile = "~/" + sablon.MasterPageFile;
        //}
        //else
        //{
        //    this.MasterPageFile = SessionManager.MasterPageFile;
        //}

        //int? masterPageId = Request["mf"].xToInt();

        //if (masterPageId != null)
        //{
        //    enSablon sablonGetir = bllSablonlar.SablonGetir(masterPageId.Value);

        //    this.MasterPageFile = "~/" + sablonGetir.MasterPageFile;

        //    SessionManager.MasterPageFile = "~/" + sablonGetir.MasterPageFile;
        //}

        //enSiteHaritasi sayfa = new enSiteHaritasi();

        //if (sayfa.MasterPageFile.xBosMu() == false)
        //{
        //    this.MasterPageFile = "~/" + sayfa.MasterPageFile;
        //}

        //if (RouteData.Values["Id"] == null)
        //{

        //    sayfa = bllSiteHaritasi.SayfaGetirFiziksel("~" + Request.Url.AbsolutePath);
        //}
        //else
        //{
        //    sayfa = bllSiteHaritasi.SayfaGetir(RouteData.Values["Id"].xToIntDefault());
        //}

        //Page.Title = sayfa.Title;
        //try { Page.MetaDescription = sayfa.Description.xBosMu() ? sayfa.Icerik.xToRemoveHTMLTags().xLeft(250) : sayfa.Description; }
        //catch { }
        //Page.MetaKeywords = sayfa.Keywords;

        //if (sayfa.Keywords.xBosMu())
        //{
        //    Response.Write("<span style='width:2px;height:2px;background-color:red; position:absolute;left:0;top:0;z-index:9999999'>&nbsp;</span>");
        //}
    }
}

