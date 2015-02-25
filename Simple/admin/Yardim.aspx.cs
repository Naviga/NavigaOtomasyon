using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;

namespace Ws.admin
{
    public partial class Yardim : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGonder_Click(object sender, EventArgs e)
        {
            string hostName = Request.Url.Host;
            string mailServer = "";

            if (hostName.Contains("www."))
            {
                mailServer = hostName.Replace("www", "mail").Replace("http://", "");
            }
            else
            {
                mailServer = "mail." + hostName;
            }

            //SmtpClient kaynak = new SmtpClient(mailServer);
            SmtpClient kaynak = new SmtpClient("mail.finexmedia.com");

            MailAddress gonderen = new MailAddress(txtEposta.Text, txtAd.Text + " " + txtSoyad.Text);
            MailAddress giden = new MailAddress("destek@finexmedia.com", "Finex Media Destek");

            MailMessage mesaj = new MailMessage(gonderen, giden);
            mesaj.Subject = "Müşteri Destek Talebi - " + txtKonu.Text;
            mesaj.Body = txtMesaj.Text;
            mesaj.IsBodyHtml = true;

            try
            {
                kaynak.Send(mesaj);
                uscUyari1.UyariGoster("Mesajınız gönderilmiştir. <br/> Teşekkür ederiz.", "İletişim Formu", true);
            }
            catch
            {
                uscUyari1.UyariGoster("Mesaj gönderilirken hata oluştu ! <br/> Lütfen tekrar deneyin.", "İletişim Formu", false);
            }
        }
    }
}