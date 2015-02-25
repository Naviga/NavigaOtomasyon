using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ws.admin
{
    public partial class Restart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpRuntime.UnloadAppDomain();
        }
    }
}