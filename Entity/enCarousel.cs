using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Entity
{
    public class enCarousel:enTemelVeri
    {
        public string Adi { get; set; }
        public bool Statu { get; set; }
        public int? SayfaId { get; set; }
        public int ResimSayisi { get; set; }
        public int TekrarSayisi { get; set; }
        public int GosterimSuresi { get; set; }
    }
}
