using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Entity;

namespace Ws.usc
{
    public partial class uscTeknikDestekFormu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnGonder.Text = bllDiziler.DiziGetir("SupportForm.Button.Send");
            }
        }

        protected void btnGonder_Click(object sender, EventArgs e)
        {
            SmtpClient kaynak = new SmtpClient(bllGenelAyarlar.GenelAyarGetir(enEnumaration.enmGenelAyarlar.MailSunucu).Icerik);

            MailAddress gonderen = new MailAddress(txtEposta.Text, txtAdSoyad.Text);
            MailAddress giden = new MailAddress(bllGenelAyarlar.GenelAyarGetir(enEnumaration.enmGenelAyarlar.IletisimFormuEposta).Icerik, bllDiziler.DiziGetir("SupportForm.EMail.DisplayName"));

            MailMessage mesaj = new MailMessage(gonderen, giden);
            mesaj.Subject = bllDiziler.DiziGetir("SupportForm.EMail.Subject");

            StringBuilder sb = new StringBuilder();

            sb.Append("<h3>" + bllDiziler.DiziGetir("SupportForm.EMail.Title") + "</h3>");

            sb.Append("<p><b>" + bllDiziler.DiziGetir("SupportForm.Label.NameLastname") + " :" + txtAdSoyad.Text + "</b></p>");
            sb.Append("<p><b>" + bllDiziler.DiziGetir("SupportForm.Label.Company") + " :" + txtFirma.Text + "</b></p>");
            sb.Append("<p><b>" + bllDiziler.DiziGetir("SupportForm.Label.Address") + " :" + txtAdres.Text + "</b></p>");
            sb.Append("<p><b>" + bllDiziler.DiziGetir("SupportForm.Label.Phone") + " :" + txtTel.Text + "</b></p>");
            sb.Append("<p><b>" + bllDiziler.DiziGetir("SupportForm.Label.Fax") + " :" + txtFax.Text + "</b></p>");
            sb.Append("<p><b>" + bllDiziler.DiziGetir("SupportForm.Label.EMail") + " :" + txtEposta.Text + "</b></p>");
            sb.Append("<p><b>" + bllDiziler.DiziGetir("SupportForm.Label.Feedback") + " :" + txtMesaj.Text + "</b></p>");

            mesaj.Body = sb.ToString();
            mesaj.IsBodyHtml = true;

            try
            {
                kaynak.Send(mesaj);

                txtAdSoyad.Text = "";
                txtEposta.Text = "";
                txtAdres.Text = "";
                txtMesaj.Text = "";
                txtFax.Text = "";
                txtTel.Text = "";
                txtFirma.Text = "";

                lblUyari.Text = "<div data-alert class='alert-box success'>" + bllDiziler.DiziGetir("SupportForm.Message.Success") + "<a href='#' class='close'>&times;</a></div>";
            }
            catch (Exception ex)
            {
                lblUyari.Text = "<div data-alert class='alert-box alert'>" + bllDiziler.DiziGetir("SupportForm.Message.Error") + "<br/>" + ex.Message + "<a href='#' class='close'>&times;</a></div>";
            }
        }
    }
}