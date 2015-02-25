using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    [Serializable]
    public class enSablon
    {
        public int Id { get; set; }
        public string Adi { get; set; }
        public string Aciklama { get; set; }
        public string Ikon { get; set; }
        public string IcerikGenislik { get; set; }
        public string Margin { get; set; }
        public string MarginLeft { get; set; }
        public string MarginRight { get; set; }
        public string BlokGenislik { get; set; }
        public int SlaytId { get; set; }
        public string MasterPageFile { get; set; }
    }
}
