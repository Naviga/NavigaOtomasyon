using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    public class enBlok
    {
        public int Id { get; set; }
        public string Adi { get; set; }
        public bool Sol { get; set; }
        public bool Sag { get; set; }
        public bool Ust { get; set; }
        public bool Alt { get; set; }
        public int Sira { get; set; }
        public bool Statu { get; set; }
        public string Icerik { get; set; }
        public string UscYolu { get; set; }
        public string Icon { get; set; }
        public bool BaslikKullanimi { get; set; }
        public bool CerceveKullanimi { get; set; }
        public bool Ic { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public bool Default { get; set; }
        public int PozisyonId { get; set; }
        public int? SayfaId { get; set; }
        public string Aciklama { get; set; }
        public int GenislikBirim { get; set; }
        public int? CarouselId { get; set; }
        public string CerceveRengi { get; set; }
        public string ArkaplanRengi { get; set; }
        public string MetinRengi { get; set; }
    }
}
