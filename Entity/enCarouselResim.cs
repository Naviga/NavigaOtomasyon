using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    public class enCarouselResim : enTemelVeri
    {
        public int CarouselId { get; set; }
        public string Kucuk { get; set; }
        public string Orta { get; set; }
        public string Buyuk { get; set; }
        public int Sira { get; set; }
        public string Baslik { get; set; }
        public string NavUrl { get; set; }
        public bool FotoLink { get; set; }
        public bool VideoLink { get; set; }
    }
}
