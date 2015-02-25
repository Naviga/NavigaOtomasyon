using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Entity;
using Telerik.Web.UI;

namespace Ws.admin
{
    public partial class Diziler : System.Web.UI.Page
    {
        List<enDizi> diziler = new List<enDizi>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DizileriGetir();
            }
        }

        protected void DizileriGetir()
        {
            diziler = bllDiziler.TumunuGetir();

            rgrvDiziler.DataSource = diziler;
            rgrvDiziler.DataBind();
        }

        protected void SetDataSource()
        {
            if (txtArama.Text.xBosMu() == false)
            {
                diziler = bllDiziler.TumunuGetir(txtArama.Text);
            }
            else
            {
                diziler = bllDiziler.TumunuGetir();
            }
            rgrvDiziler.DataSource = diziler;
        }

        protected void lnkDuzenle_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;

            int diziId = lnk.CommandArgument.xToIntDefault();

            Response.Redirect("~/admin/diziDuzenle.aspx?Id=" + diziId.ToString());
        }

        protected void rgrvDiziler_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            SetDataSource();
        }

        protected void txtArama_TextChanged(object sender, EventArgs e)
        {
            rgrvDiziler.Rebind();
        }

        protected void btnDiziKaydet_OnClick(object sender, EventArgs e)
        {
            enDizi dizi = bllDiziler.DiziGetir(hdnID.Value.xToIntDefault());

            dizi.VarsayilanDegeri = edtDiziDeger.Content;

            bllDiziler.VarsayilanDiziDuncelle(dizi);

            mpeDiziDuzenle.Hide();

            SetDataSource();

            rgrvDiziler.Rebind();
        }

        protected void btnDiziKoduEkle_OnClick(object sender, EventArgs e)
        {
            enDizi dizi = new enDizi();

            dizi.Kodu = Server.HtmlEncode(txtDiziKodu.Text);

            bllDiziler.YeniKod(dizi);

            DizileriGetir();
        }

        protected void lnkSil_OnClick(object sender, EventArgs e)
        {
            int diziId = (sender as LinkButton).CommandArgument.xToIntDefault();

            bllDiziler.Sil(diziId);

            DizileriGetir();
        }
    }
}