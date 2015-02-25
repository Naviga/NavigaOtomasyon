using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ws.usc
{
    public partial class uscUyari : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void UyariGoster(string mesaj, string baslik, bool basari)
        {
            lblUyari.Text = "<div data-alert class='alert-box " + (basari ? " success " : " alert ") + "'>" + mesaj + "</div>";
            lblUyariBaslik.Text = baslik;

            mpeUyari.Show();
            this.xUpdateAjax();
        }
    }
}