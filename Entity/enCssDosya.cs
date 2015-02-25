using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    [Serializable]
    public class enCssDosya
    {
        public int Id { get; set; }
        public string Adi { get; set; }
        public int SablonId { get; set; }
        public string SablonAdi { get; set; }

    }
}
