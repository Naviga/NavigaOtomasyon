using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    public class enIcerikVideo : enTemelVeri
    {
        public int SayfaId { get; set; }
        public string Baslik { get; set; }
        public string Aciklama { get; set; }
        public string Kaynak { get; set; }
        public string Kapak { get; set; }
        public int Sira { get; set; }
        public string UrlKodu { get; set; }
    }
}
