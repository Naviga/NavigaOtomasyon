using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Entity;

namespace Ws.usc
{
    public partial class uscPozisyonBlok : System.Web.UI.UserControl
    {
        public enum enmPozisyon
        {
            Sol = enEnumaration.enmPozisyon.Sol,
            Orta = enEnumaration.enmPozisyon.Orta,
            Sag = enEnumaration.enmPozisyon.Sag,
            OrtaSol = enEnumaration.enmPozisyon.OrtaSol,
            OrtaSag = enEnumaration.enmPozisyon.OrtaSag,
            OrtaUstSol = enEnumaration.enmPozisyon.OrtaUstSol,
            OrtaUstSag = enEnumaration.enmPozisyon.OrtaUstSag,
            OrtaUst = enEnumaration.enmPozisyon.OrtaUst,
            OrtaAlt = enEnumaration.enmPozisyon.OrtaAlt,
            OrtaAltSol = enEnumaration.enmPozisyon.OrtaAltSol,
            OrtaAltSag = enEnumaration.enmPozisyon.OrtaAltSag,
            Ust = enEnumaration.enmPozisyon.Ust,
            UstSol = enEnumaration.enmPozisyon.UstSol,
            UstOrta = enEnumaration.enmPozisyon.UstOrta,
            UstSag = enEnumaration.enmPozisyon.UstSag,
            UstAlt = enEnumaration.enmPozisyon.UstAlt
        }

        private enmPozisyon _enmPozisyon;

        public enmPozisyon Pozisyon
        {
            get
            {
                return _enmPozisyon;
            }
            set { _enmPozisyon = value; }
        }

        public enum enmSayfaMi
        {
            True = 1,
            False = 0
        }

        private enmSayfaMi _enmSayfaMi;

        public enmSayfaMi SayfaMi
        {
            get
            {
                return _enmSayfaMi;
            }
            set { _enmSayfaMi = value; }
        }


        public enum enmDefaultSayfaMi
        {
            True = 1,
            False = 0
        }

        private enmDefaultSayfaMi _enmDefaultSayfaMi;

        public enmDefaultSayfaMi DefaultSayfaMi
        {
            get
            {
                return _enmDefaultSayfaMi;
            }
            set { _enmDefaultSayfaMi = value; }
        }


        private int? _sayfaId;

        public int? SayfaId
        {
            get
            {
                if (_sayfaId == null)
                {
                    _sayfaId = HttpContext.Current.Request.RequestContext.RouteData.Values["Id"].xToInt();
                }

                if (DefaultSayfaMi.xToBooleanDefault())
                {
                    _sayfaId = bllSiteHaritasi.SayfaGetirFiziksel("~/Default.aspx").Id;
                }

                if (_sayfaId == 0 || _sayfaId == null)
                {
                    _sayfaId = HttpContext.Current.Request.QueryString["s"].xToInt();
                }

                if (_sayfaId == 0 || _sayfaId == null)
                {
                    _sayfaId = 0;
                }

                return _sayfaId;
            }

            set { _sayfaId = value; }
        }

        //public static class enmPozisyon
        //{
        //    public const int Sol = 1;
        //    public const int Orta = 2;
        //    public const int Sag = 3;
        //    public const int OrtaSol = 4;
        //    public const int OrtaSag = 5;
        //    public const int OrtaUstSol = 9;
        //    public const int OrtaUstSag = 10;
        //    public const int OrtaUst = 11;
        //    public const int OrtaAlt = 12;
        //    public const int OrtaAltSol = 13;
        //    public const int OrtaAltSag = 14;
        //    public const int Ust = 15;
        //    public const int UstSol = 16;
        //    public const int UstOrta = 17;
        //    public const int UstSag = 18;
        //    public const int UstAlt = 19;
        //}

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}