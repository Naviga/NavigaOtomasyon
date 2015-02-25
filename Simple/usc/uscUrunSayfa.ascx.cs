using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Entity;

namespace Ws.usc
{
    public partial class uscUrunSayfa : System.Web.UI.UserControl
    {
        public int SayfaId { get; set; }
        public string SayfaYolu { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UrunResimleriGetir();
            }
        }

        private void UrunResimleriGetir()
        {
            List<enIcerikResim> resimler = bllIcerikResimleri.ResimleriGetir(Session["ürünler_sayfaId"].xToIntDefault(), true);
            rptFotoBig.DataSource = resimler;
            //rptFotoThumb.DataSource = resimler;

            rptFotoBig.DataBind();
            //rptFotoThumb.DataBind();
        }
    }
}