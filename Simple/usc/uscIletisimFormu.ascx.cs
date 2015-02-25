using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using BLL;
using Entity;

namespace Ws.usc
{
    public partial class uscIletisimFormu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnGonder.Text = bllDiziler.DiziGetir("ContactForm.Button.Send");
            }
        }

        protected void btnGonder_Click(object sender, EventArgs e)
        {
            SmtpClient kaynak = new SmtpClient(bllGenelAyarlar.GenelAyarGetir(enEnumaration.enmGenelAyarlar.MailSunucu).Icerik);

            MailAddress gonderen = new MailAddress(txtEposta.Text, txtAd.Text);
            MailAddress giden = new MailAddress(bllGenelAyarlar.GenelAyarGetir(enEnumaration.enmGenelAyarlar.IletisimFormuEposta).Icerik, "Web Sitesi İletişim Formu");

            MailMessage mesaj = new MailMessage(gonderen, giden);
            mesaj.Subject = txtKonu.Text;
            mesaj.Body = txtMesaj.Text;
            mesaj.IsBodyHtml = true;

            try
            {
                kaynak.Send(mesaj);

                txtAd.Text = "";
                txtEposta.Text = "";
                txtKonu.Text = "";
                txtMesaj.Text = "";
                txtSoyad.Text = "";

                lblUyari.Text = "<div data-alert class='alert-box success'>" + bllDiziler.DiziGetir("ContactForm.Message.Success") + "<a href='#' class='close'>&times;</a></div>";
            }
            catch (Exception ex)
            {
                lblUyari.Text = "<div data-alert class='alert-box alert'>" + bllDiziler.DiziGetir("ContactForm.Message.Error") + "<br/>" + ex.Message + "<a href='#' class='close'>&times;</a></div>";
            }
        }
    }
}