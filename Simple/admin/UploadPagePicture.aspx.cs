using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Entity;

namespace Ws.admin
{
    public partial class UploadPagePicture : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lnkSayfaResmiKaldir_OnClick(object sender, EventArgs e)
        {
            bllSiteHaritasi.ResimGuncelle(Request.QueryString["id"].xToIntDefault(), "");
        }
    }
}