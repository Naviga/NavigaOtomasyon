using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Common;
using BLL;
using Entity;

namespace Ws.admin
{
    public partial class admin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (SessionManager.Admin == null)
            {
                Response.Redirect("~/admin/Login.aspx");
            }

            //string yoneticiler = bllGenelAyarlar.GenelAyarGetir(enEnumaration.enmGenelAyarlar.FaceYoneticileri).Icerik;

            ////<meta property="fb:admins" content="{YOUR_FACEBOOK_USER_ID}"/>

            //if (yoneticiler.xBosMu() == false)
            //{
            //    HtmlMeta meta = new HtmlMeta();

            //    meta.Attributes.Add("property", "fb:admins");
            //    meta.Content = yoneticiler;

            //    head.Controls.Add(meta);
            //}

        }



        protected void lnkCikis_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("~/admin/Login.aspx");
        }
    }
}