using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL;
using Entity;

namespace Settings
{
    public class Tasarim
    {
        //var enmTasarimAyarlari = {
        //        "Sablon": null, "TamEkran": null, "APResim": null, "Logo": null, "LogoAlani": null, "LogoH": null, "LogoPH": null, "LogoPV": null,
        //        "LogoMenu": null, "Link": null, "MenuH": null, "MenuBg": null, "MenuDiv": null, "Header": null, "Middle": null,
        //        "Panel": null, "Text": null, "Baslik": null
        //    };

        private int? _GenelSablon = null;
        private string _Logo = "";
        private string _ArkaPlanResmi = "";
        private string _ArkaPlanRengi = "";
        private string _ArkaPlanDeseni = "";
        private string _BaglantiRengi = "";
        private string _BaglantiRengi2 = "";
        private string _BaslikRengi = "";
        private string _ButonRengi = "";
        private string _ButonRengi2 = "";
        private string _MenuArkaPlanRengi = "";
        private string _MenuYaziRengi = "";
        private bool? _TamEkranKullanim = null;
        private string _UstBolumRengi = "";
        private string _OrtaBolumRengi = "";
        private string _AltBolumRengi = "";
        private bool? _LogoAlani = null;
        private int? _LogoYuksekligi = null;
        private string _LogoYatayPozisyon = "";
        private bool? _LogoDikeyPozisyon = null;
        private bool? _MenuLogoGornumu = null;
        private int? _MenuYuksekligi = null;
        private string _MenuAyracRengi = "";
        private string _BlokArkaPlanRengi = "";
        private string _YaziRengi = "";
        private string _BlokCerceveRengi = "";
        private string _ContainerArkaPlanRengi = "";
        private string _Template = "";

        public int? Sablon
        {
            get
            {
                if (_GenelSablon == null)
                {
                    return bllTasarimAyarlari.TasarimAyariGetir(enEnumaration.enmTasarimAyarlari.GenelSablon).Degeri.xToInt();
                }

                return _GenelSablon;
            }
            set
            {
                _GenelSablon = value;
            }
        }
        public string APResim
        {
            get
            {
                if (_ArkaPlanResmi.xBosMu())
                    return bllTasarimAyarlari.TasarimAyariGetir(enEnumaration.enmTasarimAyarlari.ArkaPlanResmi).Degeri;
                return _ArkaPlanResmi;
            }
            set
            {
                _ArkaPlanResmi = value;
            }
        }
        public string Logo
        {
            get
            {
                if (_Logo.xBosMu())
                {
                    return bllTasarimAyarlari.TasarimAyariGetir(enEnumaration.enmTasarimAyarlari.Logo).Degeri;
                }
                return _Logo;
            }
            set
            {
                _Logo = value;
            }
        }
        public bool? TamEkran
        {
            get
            {
                if (_TamEkranKullanim == null)
                    return bllTasarimAyarlari.TasarimAyariGetir(enEnumaration.enmTasarimAyarlari.TamEkranKullanim).Degeri.xToBoolean();

                return _TamEkranKullanim;
            }
            set
            {
                _TamEkranKullanim = value;
            }
        }
        public bool? LogoAlani
        {
            get
            {
                if (_LogoAlani == null)
                    return bllTasarimAyarlari.TasarimAyariGetir(enEnumaration.enmTasarimAyarlari.LogoAlani).Degeri.xToBoolean();
                return _LogoAlani;
            }
            set
            {
                _LogoAlani = value;
            }
        }
        public int? LogoH
        {
            get
            {
                if (_LogoYuksekligi == null)
                    return bllTasarimAyarlari.TasarimAyariGetir(enEnumaration.enmTasarimAyarlari.LogoYuksekligi).Degeri.xToInt();
                return _LogoYuksekligi;
            }
            set
            {
                _LogoYuksekligi = value;
            }
        }
        public string LogoPH
        {
            get
            {
                if (_LogoYatayPozisyon.xBosMu())
                    return bllTasarimAyarlari.TasarimAyariGetir(enEnumaration.enmTasarimAyarlari.LogoYatayPozisyon).Degeri;
                return _LogoYatayPozisyon;
            }
            set
            {
                _LogoYatayPozisyon = value;
            }
        }
        public bool? LogoPV
        {
            get
            {
                if (_LogoDikeyPozisyon == null)
                    return bllTasarimAyarlari.TasarimAyariGetir(enEnumaration.enmTasarimAyarlari.LogoDikeyPozisyon).Degeri.xToBoolean();
                return _LogoDikeyPozisyon;
            }
            set
            {
                _LogoDikeyPozisyon = value;
            }
        }
        public bool? LogoMenu
        {
            get
            {
                if (_MenuLogoGornumu == null)
                    return bllTasarimAyarlari.TasarimAyariGetir(enEnumaration.enmTasarimAyarlari.MenuLogoGornumu).Degeri.xToBoolean();
                return _MenuLogoGornumu;
            }
            set
            {
                _MenuLogoGornumu = value;
            }
        }
        public string Link
        {
            get
            {
                if (_BaglantiRengi.xBosMu())
                    return bllTasarimAyarlari.TasarimAyariGetir(enEnumaration.enmTasarimAyarlari.BaglantiRengi).Degeri;
                return _BaglantiRengi;
            }
            set
            {
                _BaglantiRengi = value;
            }
        }
        public int? MenuH
        {
            get
            {
                if (_MenuYuksekligi == null)
                    return bllTasarimAyarlari.TasarimAyariGetir(enEnumaration.enmTasarimAyarlari.MenuYuksekligi).Degeri.xToInt();
                return _MenuYuksekligi;
            }
            set
            {
                _MenuYuksekligi = value;
            }
        }
        public string MenuBg
        {
            get
            {
                if (_MenuArkaPlanRengi.xBosMu())
                    return bllTasarimAyarlari.TasarimAyariGetir(enEnumaration.enmTasarimAyarlari.MenuArkaPlanRengi).Degeri;
                return _MenuArkaPlanRengi;
            }
            set
            {
                _MenuArkaPlanRengi = value;
            }
        }
        public string MenuDiv
        {
            get
            {
                if (_MenuAyracRengi.xBosMu())
                    return bllTasarimAyarlari.TasarimAyariGetir(enEnumaration.enmTasarimAyarlari.MenuAyracRengi).Degeri;
                return _MenuAyracRengi;
            }
            set
            {
                _MenuAyracRengi = value;
            }
        }
        public string Header
        {
            get
            {
                if (_UstBolumRengi.xBosMu())
                    return bllTasarimAyarlari.TasarimAyariGetir(enEnumaration.enmTasarimAyarlari.UstBolumRengi).Degeri;
                return _UstBolumRengi;
            }
            set
            {
                _UstBolumRengi = value;
            }
        }
        public string Middle
        {
            get
            {
                if (_OrtaBolumRengi.xBosMu())
                    return bllTasarimAyarlari.TasarimAyariGetir(enEnumaration.enmTasarimAyarlari.OrtaBolumRengi).Degeri;
                return _OrtaBolumRengi;
            }
            set
            {
                _OrtaBolumRengi = value;
            }
        }
        public string Panel
        {
            get
            {
                if (_BlokArkaPlanRengi.xBosMu())
                    return bllTasarimAyarlari.TasarimAyariGetir(enEnumaration.enmTasarimAyarlari.BlokArkaPlanRengi).Degeri;
                return _BlokArkaPlanRengi;
            }
            set
            {
                _BlokArkaPlanRengi = value;
            }
        }
        public string Text
        {
            get
            {
                if (_YaziRengi.xBosMu())
                    return bllTasarimAyarlari.TasarimAyariGetir(enEnumaration.enmTasarimAyarlari.YaziRengi).Degeri;
                return _YaziRengi;
            }
            set
            {
                _YaziRengi = value;
            }
        }
        public string Baslik
        {
            get
            {
                if (_BaslikRengi.xBosMu())
                    return bllTasarimAyarlari.TasarimAyariGetir(enEnumaration.enmTasarimAyarlari.BaslikRengi).Degeri;
                return _BaslikRengi;
            }
            set { _BaslikRengi = value; }
        }
        public string BlokCerceveRengi
        {
            get
            {
                if (_BlokCerceveRengi.xBosMu())
                    return bllTasarimAyarlari.TasarimAyariGetir(enEnumaration.enmTasarimAyarlari.BlokCerceveRengi).Degeri;
                return _BlokCerceveRengi;
            }
            set { _BlokCerceveRengi = value; }
        }
        public string ContainerBg
        {
            get
            {
                if (_ContainerArkaPlanRengi.xBosMu())
                    return bllTasarimAyarlari.TasarimAyariGetir(enEnumaration.enmTasarimAyarlari.ContainerArkaPlanRengi).Degeri;
                return _ContainerArkaPlanRengi;
            }
            set { _ContainerArkaPlanRengi = value; }
        }
        public string Template
        {
            get
            {
                if (_MenuYuksekligi == null)
                    return bllTasarimAyarlari.TasarimAyariGetir(enEnumaration.enmTasarimAyarlari.Template).Degeri;
                return _Template;
            }
            set
            {
                _Template = value;
            }
        }

        private static int? _sGenelSablon = null;
        private static string _sLogo = "";
        private static string _sArkaPlanResmi = "";
        private static string _sArkaPlanRengi = "";
        private static string _sArkaPlanDeseni = "";
        private static string _sBaglantiRengi = "";
        private static string _sBaglantiRengi2 = "";
        private static string _sBaslikRengi = "";
        private static string _sButonRengi = "";
        private static string _sButonRengi2 = "";
        private static string _sMenuArkaPlanRengi = "";
        private static string _sMenuYaziRengi = "";
        private static bool? _sTamEkranKullanim = null;
        private static string _sUstBolumRengi = "";
        private static string _sOrtaBolumRengi = "";
        private static string _sAltBolumRengi = "";
        private static bool? _sLogoAlani = null;
        private static int? _sLogoYuksekligi = null;
        private static string _sLogoYatayPozisyon = "";
        private static bool? _sLogoDikeyPozisyon = null;
        private static bool? _sMenuLogoGornumu = null;
        private static int? _sMenuYuksekligi = null;
        private static string _sMenuAyracRengi = "";
        private static string _sBlokArkaPlanRengi = "";
        private static string _sYaziRengi = "";
        private static string _sTemplate = "";

        public static int? sSablon
        {
            get
            {
                if (_sGenelSablon == null)
                {
                    return bllTasarimAyarlari.TasarimAyariGetir(enEnumaration.enmTasarimAyarlari.GenelSablon).Degeri.xToInt();
                }

                return _sGenelSablon;
            }
            set
            {
                _sGenelSablon = value;
            }
        }
        public static string sAPResim
        {
            get
            {
                if (_sArkaPlanResmi.xBosMu())
                    return bllTasarimAyarlari.TasarimAyariGetir(enEnumaration.enmTasarimAyarlari.ArkaPlanResmi).Degeri;
                return _sArkaPlanResmi;
            }
            set
            {
                _sArkaPlanResmi = value;
            }
        }
        public static string sLogo
        {
            get
            {
                if (_sLogo.xBosMu())
                {
                    return bllTasarimAyarlari.TasarimAyariGetir(enEnumaration.enmTasarimAyarlari.Logo).Degeri;
                }
                return _sLogo;
            }
            set
            {
                _sLogo = value;
            }
        }
        public static bool? sTamEkran
        {
            get
            {
                if (_sTamEkranKullanim == null)
                    return bllTasarimAyarlari.TasarimAyariGetir(enEnumaration.enmTasarimAyarlari.TamEkranKullanim).Degeri.xToBoolean();

                return _sTamEkranKullanim;
            }
            set
            {
                _sTamEkranKullanim = value;
            }
        }
        public static bool? sLogoAlani
        {
            get
            {
                if (_sLogoAlani == null)
                    return bllTasarimAyarlari.TasarimAyariGetir(enEnumaration.enmTasarimAyarlari.LogoAlani).Degeri.xToBoolean();
                return _sLogoAlani;
            }
            set
            {
                _sLogoAlani = value;
            }
        }
        public static int? sLogoH
        {
            get
            {
                if (_sLogoYuksekligi == null)
                    return bllTasarimAyarlari.TasarimAyariGetir(enEnumaration.enmTasarimAyarlari.LogoYuksekligi).Degeri.xToInt();
                return _sLogoYuksekligi;
            }
            set
            {
                _sLogoYuksekligi = value;
            }
        }
        public static string sLogoPH
        {
            get
            {
                if (_sLogoYatayPozisyon.xBosMu())
                    return bllTasarimAyarlari.TasarimAyariGetir(enEnumaration.enmTasarimAyarlari.LogoYatayPozisyon).Degeri;
                return _sLogoYatayPozisyon;
            }
            set
            {
                _sLogoYatayPozisyon = value;
            }
        }
        public static bool? sLogoPV
        {
            get
            {
                if (_sLogoDikeyPozisyon == null)
                    return bllTasarimAyarlari.TasarimAyariGetir(enEnumaration.enmTasarimAyarlari.LogoDikeyPozisyon).Degeri.xToBoolean();
                return _sLogoDikeyPozisyon;
            }
            set
            {
                _sLogoDikeyPozisyon = value;
            }
        }
        public static bool? sLogoMenu
        {
            get
            {
                if (_sMenuLogoGornumu == null)
                    return bllTasarimAyarlari.TasarimAyariGetir(enEnumaration.enmTasarimAyarlari.MenuLogoGornumu).Degeri.xToBoolean();
                return _sMenuLogoGornumu;
            }
            set
            {
                _sMenuLogoGornumu = value;
            }
        }
        public static string sLink
        {
            get
            {
                if (_sBaglantiRengi.xBosMu())
                    return bllTasarimAyarlari.TasarimAyariGetir(enEnumaration.enmTasarimAyarlari.BaglantiRengi).Degeri;
                return _sBaglantiRengi;
            }
            set
            {
                _sBaglantiRengi = value;
            }
        }
        public static int? sMenuH
        {
            get
            {
                if (_sMenuYuksekligi == null)
                    return bllTasarimAyarlari.TasarimAyariGetir(enEnumaration.enmTasarimAyarlari.MenuYuksekligi).Degeri.xToInt();
                return _sMenuYuksekligi;
            }
            set
            {
                _sMenuYuksekligi = value;
            }
        }
        public static string sMenuBg
        {
            get
            {
                if (_sMenuArkaPlanRengi.xBosMu())
                    return bllTasarimAyarlari.TasarimAyariGetir(enEnumaration.enmTasarimAyarlari.MenuArkaPlanRengi).Degeri;
                return _sMenuArkaPlanRengi;
            }
            set
            {
                _sMenuArkaPlanRengi = value;
            }
        }
        public static string sMenuDiv
        {
            get
            {
                if (_sMenuAyracRengi.xBosMu())
                    return bllTasarimAyarlari.TasarimAyariGetir(enEnumaration.enmTasarimAyarlari.MenuAyracRengi).Degeri;
                return _sMenuAyracRengi;
            }
            set
            {
                _sMenuAyracRengi = value;
            }
        }
        public static string sHeader
        {
            get
            {
                if (_sUstBolumRengi.xBosMu())
                    return bllTasarimAyarlari.TasarimAyariGetir(enEnumaration.enmTasarimAyarlari.UstBolumRengi).Degeri;
                return _sUstBolumRengi;
            }
            set
            {
                _sUstBolumRengi = value;
            }
        }
        public static string sMiddle
        {
            get
            {
                if (_sOrtaBolumRengi.xBosMu())
                    return bllTasarimAyarlari.TasarimAyariGetir(enEnumaration.enmTasarimAyarlari.OrtaBolumRengi).Degeri;
                return _sOrtaBolumRengi;
            }
            set
            {
                _sOrtaBolumRengi = value;
            }
        }
        public static string sPanel
        {
            get
            {
                if (_sBlokArkaPlanRengi.xBosMu())
                    return bllTasarimAyarlari.TasarimAyariGetir(enEnumaration.enmTasarimAyarlari.BlokArkaPlanRengi).Degeri;
                return _sBlokArkaPlanRengi;
            }
            set
            {
                _sBlokArkaPlanRengi = value;
            }
        }
        public static string sText
        {
            get
            {
                if (_sYaziRengi.xBosMu())
                    return bllTasarimAyarlari.TasarimAyariGetir(enEnumaration.enmTasarimAyarlari.YaziRengi).Degeri;
                return _sYaziRengi;
            }
            set
            {
                _sYaziRengi = value;
            }
        }
        public static string sBaslik
        {
            get
            {
                if (_sBaslikRengi.xBosMu())
                    return bllTasarimAyarlari.TasarimAyariGetir(enEnumaration.enmTasarimAyarlari.BaslikRengi).Degeri;
                return _sBaslikRengi;
            }
            set { _sBaslikRengi = value; }
        }
        public static string sTemplate
        {
            get
            {
                if (_sTemplate.xBosMu())
                    return bllTasarimAyarlari.TasarimAyariGetir(enEnumaration.enmTasarimAyarlari.Template).Degeri;
                return _sTemplate;
            }
            set
            {
                _sTemplate = value;
            }
        }


        //public static void Kaydet()
        //{
        //    enTasarimAyar ayar = new enTasarimAyar();

        //    ayar.Id = _GenelSablon;
        //    ayar.Degeri = Sablon.ToString();

        //    bllTasarimAyarlari.TasarimAyariGuncelle(
        //}
    }
}
