using System;
using System.Web;
using System.Web.UI;
using System.Web.SessionState;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Entity;

namespace Common
{
    public class SessionManager
    {
        public static string pcClientIp
        {
            get
            {
                return HttpContext.Current.Request.UserHostAddress;
            }
        }

        public static enAdmin Admin
        {
            get
            {
                if (HttpContext.Current.Session == null)
                {
                    return null;
                }

                return HttpContext.Current.Session["admin"] as enAdmin;
            }
            set
            {
                HttpContext.Current.Session["admin"] = value;
            }
        }

        public static string MasterPageFile
        {
            get
            {
                if (HttpContext.Current.Session["mf"] == null) return null;
                return HttpContext.Current.Session["mf"].ToString();
            }
            set
            {
                HttpContext.Current.Session["mf"] = value;
            }
        }

    }
}
