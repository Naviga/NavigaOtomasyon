using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity;
using BLL;

namespace Ws.admin
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                YillikSureKontrol();
            }

            //uscIstatistik1.VwTarih1 = DateTime.Now.Date;
            //uscIstatistik1.VwBaslik = "Bugün";

            //uscIstatistik2.VwTarih1 = DateTime.Now.Date.AddDays(-DateTime.Now.Day);
            //uscIstatistik2.VwTarih2 = DateTime.Now.Date.AddDays(DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month) - DateTime.Now.Day);
            //uscIstatistik2.VwBaslik = "Bu Ay";

            //uscIstatistik3.VwBaslik = "Toplam";
        }

        protected void YillikSureKontrol()
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
                    lblLisansUyari.Text = "<div data-alert class='alert-box alert'><p>Dikkat ! Web sitenizin yıllık süresi dolmuş ve duraklatılmıştır. <br /> Sitenizin tekrar yayına girmesi için ödeme yapmanız gerekmektedir ! </p></div>";
                }
                else
                {
                    lblLisansUyari.Text = "<div data-alert class='alert-box alert'><p>Dikkat ! Web sitenizin yıllık süresini yenilemek için son " + bitisTarihi.Subtract(DateTime.Now).Days + " gün !</p></div>";
                }
            }
        }

    }
}