using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    public class enIcerik : enTemelVeri
    {
        public int SayfaId { get; set; }
        public string Baslik { get; set; }
        public string Icerik { get; set; }
        public int Sira { get; set; }
    }
}
