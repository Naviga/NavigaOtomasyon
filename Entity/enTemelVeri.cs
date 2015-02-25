using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    [Serializable]
    public class enTemelVeri
    {
        public int Id { get; set; }
        public bool Statu { get; set; }
        public DateTime KayitTarihi { get; set; }
        public int Kaydeden { get; set; }
        public DateTime DegisiklikTarihi { get; set; }
        public int Degistiren { get; set; }
        public string KaydedenAdi { get; set; }
        public string DegistirenAdi { get; set; }
    }
}
