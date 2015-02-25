using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;


namespace Entity
{
    public class enSosyalMedya
    {
        public string sos_adi { get; set; }
        public int sos_id { get; set; }
        public string sos_ikonu { get; set; }
        public int sos_sira { get; set; }
        public bool sos_statu { get; set; }
        public string sos_url { get; set; }
        public bool sos_menu { get; set; }
        public bool sos_yanMenu { get; set; }
        public bool sos_footer { get; set; }
    }
}
