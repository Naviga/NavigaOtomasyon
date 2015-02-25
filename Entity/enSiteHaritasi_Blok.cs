using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    public class enSiteHaritasi_Blok
    {
        public int SayfaId { get; set; }
        public int BlokId { get; set; }
        public int PozisyonId { get; set; }
        public bool MasterPozisyon { get; set; }
        public bool Statu { get; set; }
        public int Sira { get; set; }
        public string Height { get; set; }
        public bool CerceveKullanimi { get; set; }
        public bool BaslikKullanimi { get; set; }
        public string CerceveRengi { get; set; }
        public string ArkaplanRengi { get; set; }
        public string MetinRengi { get; set; }
    }
}
