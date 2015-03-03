using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    [Serializable]
    public class enSiteHaritasi
    {
        public int Id { get; set; }
        public string Adi { get; set; }
        public string Resim { get; set; }
        public bool Statu { get; set; }
        public bool DefaultSayfa { get; set; }
        public string Icerik { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Keywords { get; set; }
        public int? Parent { get; set; }
        public string Url { get; set; }
        public int Sira { get; set; }
        public bool Dinamik { get; set; }
        public string FizikselUrl { get; set; }
        public bool AcilirMenu { get; set; }
        public string FotoBaslik { get; set; }
        public string VideoBaslik { get; set; }
        public bool FotoGaleriMi { get; set; }
        public string DisplayUrl { get; set; }
        public bool FaceComments { get; set; }
        public bool Custom { get; set; }
        public bool PaylasimAlani { get; set; }
        public bool BaslikAlani { get; set; }
        public bool AnaSayfa { get; set; }
        public bool SayfaYolu { get; set; }
        public string MasterPageFile { get; set; }
        public bool Menu { get; set; }
        public bool YanMenu { get; set; }
        public bool Footer { get; set; }
        public bool SayfaMenu { get; set; }
        public DateTime KayitTarihi { get; set; }
        public bool List { get; set; }
        public int? CarouselId { get; set; }
        public bool SolAltMenu { get; set; }
        public bool SagAltMenu { get; set; }
        public bool UrunMu { get; set; }
        public bool HaberMi { get; set; }
        public string FotoBuyuk { get; set; }
        public string FotoOrta { get; set; }
        public string FotoKucuk { get; set; }
        public bool Vitrin { get; set; }
    }
}
