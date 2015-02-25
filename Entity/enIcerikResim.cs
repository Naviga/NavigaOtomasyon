using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    [Serializable]
    public class enIcerikResim : enTemelVeri
    {
        public int SayfaId { get; set; }
        public string Aciklama { get; set; }
        public string Kucuk { get; set; }
        public string Orta { get; set; }
        public string Buyuk { get; set; }
        public int Sira { get; set; }
        public string Baslik { get; set; }
        public bool AnaResim { get; set; }
    }
}
