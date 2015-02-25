using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity;
using BLL;
using Common;

namespace Ws.admin
{
    public partial class LisansBilgileri : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (SessionManager.Admin != null)
            {
                if (!SessionManager.Admin.Finex)
                {
                    Response.Redirect("/admin/Default.aspx");
                    return;
                }

                if (!IsPostBack)
                {
                    enGenelAyar ayrYayinTar = bllGenelAyarlar.GenelAyarGetir(enEnumaration.enmGenelAyarlar.YayinTarihi);
                    enGenelAyar ayrYayinSuresi = bllGenelAyarlar.GenelAyarGetir(enEnumaration.enmGenelAyarlar.YayinSuresi);
                    enGenelAyar ayrYayinDurum = bllGenelAyarlar.GenelAyarGetir(enEnumaration.enmGenelAyarlar.YayinDurumu);

                    DateTime yayinTarihi = ayrYayinTar.Icerik.xToDateTimeDefault();
                    int yayinSuresi = ayrYayinSuresi.Icerik.xToIntDefault();
                    bool yayinDurum = ayrYayinDurum.Icerik.xToBooleanDefault();

                    DateTime bitisTarihi = yayinTarihi.AddYears(yayinSuresi);

                    txtYayinTar.Text = yayinTarihi.ToShortDateString();
                    txtYayinSure.Text = yayinSuresi.ToString();

                    imgYayinDurum.ImageUrl = yayinDurum ? "/admin/css/img/dolu.png" : "/admin/css/img/bos.png";
                }
            }
            else
            {
                Response.Redirect("/admin/Login.aspx");
            }


        }

        protected void btnGuncelle_Click(object sender, EventArgs e)
        {
            enGenelAyar ayrYayinTar = bllGenelAyarlar.GenelAyarGetir(enEnumaration.enmGenelAyarlar.YayinTarihi);

            ayrYayinTar.Icerik = txtYayinTar.Text;

            bllGenelAyarlar.AyarGuncelle(ayrYayinTar);

            enGenelAyar ayrYayinSuresi = bllGenelAyarlar.GenelAyarGetir(enEnumaration.enmGenelAyarlar.YayinSuresi);

            ayrYayinSuresi.Icerik = txtYayinSure.Text;

            bllGenelAyarlar.AyarGuncelle(ayrYayinSuresi);
        }

        protected void lnkYayinDurumuDegistir_OnClick(object sender, EventArgs e)
        {
            enGenelAyar ayrYayinDur = bllGenelAyarlar.GenelAyarGetir(enEnumaration.enmGenelAyarlar.YayinDurumu);

            ayrYayinDur.Icerik = ayrYayinDur.Icerik.xToBooleanDefault() ? "false" : "true";

            bllGenelAyarlar.AyarGuncelle(ayrYayinDur);
        }
    }
}