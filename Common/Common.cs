using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.IO;
using Entity;
using System.Data.OleDb;

namespace Common
{
    public delegate void SimpleDelegate(object sender, object data);

    public sealed class Common
    {
        public static string UserIPAdress()
        {
            string IP = "";

            if (HttpContext.Current.Request.ServerVariables["HTTP_C_IP"] != null)
            {
                IP = HttpContext.Current.Request.ServerVariables["HTTP_C_IP"].ToString();
            }
            else if (HttpContext.Current.Request.UserHostAddress.Length != 0)
            {
                IP = HttpContext.Current.Request.UserHostAddress;
            }

            return IP;
        }

        public static bool IsInteger(string value)
        {
            Int64 tmp;
            return (IsStringNotNullOrEmpty(value) && Int64.TryParse(value, out tmp));
        }

        public static bool IsStringNotNullOrEmpty(string value)
        {
            return !string.IsNullOrEmpty(value);
        }

        public static bool TcNoKontrol(string TcNo)
        {


            //Tc numarasının 11 hane olup olmadığını kontrol ediyoruz 

            if (TcNo.Length.Equals(11) == false)
                return false;



            //Tc numarasının pozitif bir tam sayı olup olmadığını kontrol ediyoruz 

            UInt64 tmpTcNo;
            if (UInt64.TryParse(TcNo, out tmpTcNo) == false)
                return false;

            if (tmpTcNo < 10000000000 || tmpTcNo > 99999999999)
                return false;


            //Tc numarasının çift olup olmadığını kontrol ediyoruz 

            if (tmpTcNo % 2 != 0)
                return false;


            //Tc numarasının soldan ilk 10 hanesinin toplamının birler basamağının 11 nci haneye eşit olup olmadığını kontrol ediyoruz 

            byte basamakToplami = 0;
            for (int i = 0; i < 10; i++)
            {
                basamakToplami += Convert.ToByte(TcNo.Substring(i, 1));
            }

            if ((basamakToplami % 10).Equals(Convert.ToByte(tmpTcNo % 10)) == false)
                return false;

            //Yukarıdaki tüm testler başarılı oldu ise Tc numarası geçerli bir Tc Numarası olabilir demektir. 

            return true;

        }

        public static string ToTitleCase(string str)
        {
            if (!string.IsNullOrEmpty(str))
                return System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
            else
                return str;
        }

        public static string ToUpperCase(string str)
        {
            if (!string.IsNullOrEmpty(str))
                return str.ToUpper();
            else
                return str;
        }

        public static string ToLowerCaseInvariant(string str)
        {
            if (!string.IsNullOrEmpty(str))
                return str.ToLowerInvariant();
            else
                return str;
        }

        public static string CropString(string value, int length)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }
            else
            {
                if (value.Length > length)
                    return value.Substring(0, length);
                else
                    return value;
            }

        }

        public static void ResimSil(string path)
        {
            try
            {
                File.Delete(HttpContext.Current.Server.MapPath(path));
            }
            catch
            {

            }
        }

        public static bool DosyaSil(string path)
        {
            try
            {
                File.Delete(HttpContext.Current.Server.MapPath(path));

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void KillAllExcels()
        {
            foreach (System.Diagnostics.Process proc in System.Diagnostics.Process.GetProcessesByName("EXCEL"))
            {
                proc.Kill();
            }
        }

        public static string ReverseString(string s)
        {
            char[] arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }

        public static string GetAppSettingsValue(string key)
        {
            return System.Configuration.ConfigurationManager.AppSettings[key];
        }

        public static void DataRowToClass(DataRow rw, object o)
        {
            Type t = o.GetType();

            foreach (DataColumn col in rw.Table.Columns)
            {
                FieldInfo fi = t.GetField(col.ColumnName);
                if (fi != null)
                {
                    try
                    {
                        object deger = null;
                        if (rw[col.ColumnName] != DBNull.Value)
                        {
                            try
                            {
                                deger = Convert.ChangeType(rw[col.ColumnName], fi.FieldType);
                            }
                            catch { }
                            if (deger == null) deger = rw[col.ColumnName];
                        }
                        fi.SetValue(o, deger);
                    }
                    catch { }
                }
                else
                {
                    PropertyInfo pi = t.GetProperty(col.ColumnName);
                    if (pi != null)
                    {
                        try
                        {
                            object deger = null;
                            if (rw[col.ColumnName] != DBNull.Value)
                            {
                                try
                                {
                                    deger = Convert.ChangeType(rw[col.ColumnName], pi.PropertyType);
                                }
                                catch { }
                                if (deger == null) deger = rw[col.ColumnName];
                            }
                            pi.SetValue(o, deger, null);
                        }
                        catch { }
                    }
                }
            }

            //TemelVeriSinifi classını parse et
            foreach (PropertyInfo pi in t.GetProperties())
            {
                if (pi.PropertyType.Name.Contains("KayitBilgisi"))
                {
                    try
                    {
                        object o2 = pi.GetValue(o, null);
                        DataRowToClass(rw, o2);
                        pi.SetValue(o, o2, null);
                    }
                    catch { }
                }
            }
        }

        public static string TelNoFormatla(string telno)
        {
            if (string.IsNullOrEmpty(telno)) return telno;

            string sonuc = "";
            string telnoTemp = "";
            foreach (char c in telno)
            {
                double cDeger;
                bool isNumeric = double.TryParse(c.ToString(), out cDeger);
                if (isNumeric) telnoTemp += c.ToString();
            }

            switch (telnoTemp.Length)
            {
                case 7://xxx xx xx
                    sonuc = string.Format("{0} {1} {2}", telnoTemp.Substring(0, 3), telnoTemp.Substring(3, 2), telnoTemp.Substring(5, 2));
                    break;
                case 10://(xxx) xxx xx xx
                    sonuc = string.Format("({0}) {1} {2} {3}", telnoTemp.Substring(0, 3), telnoTemp.Substring(3, 3), telnoTemp.Substring(6, 2), telnoTemp.Substring(8, 2));
                    break;
                default:
                    sonuc = telno;
                    break;
            }

            return sonuc;
        }

        public static GridViewRow FindParentRow(System.Web.UI.Control control)
        {
            if (control.Parent == null)
            {
                return null;
            }
            else
            {
                if (control.Parent.GetType() == typeof(GridViewRow))
                {
                    return control.Parent as GridViewRow;
                }
                else
                {
                    return FindParentRow(control.Parent);
                }
            }
        }

        public static string TurkceKarakterDegistir(string giris)
        {
            string sonuc = giris.Replace("Ü", "U").Replace("Ğ", "G").Replace("İ", "I").Replace("Ş", "S").Replace("Ç", "C").Replace("Ö", "O");
            sonuc = sonuc.Replace("ü", "u").Replace("ğ", "g").Replace("ı", "i").Replace("ş", "s").Replace("ç", "c").Replace("ö", "o");
            return sonuc;
        }

        public static string GetCurrentPageUrl(HttpRequest request)
        {
            if (request == null) return null;

            string url = request.Url.PathAndQuery;
            string appPath = request.ApplicationPath;

            if (string.IsNullOrEmpty(appPath) == false && appPath.Length > 1)
            {   //urlden apppath çıkar
                url = url.Substring(appPath.Length, url.Length - appPath.Length);
            }

            if (url.StartsWith("/") == false)
                url = "/" + url;

            if (url.StartsWith("~") == false)
                url = "~" + url;

            return url;
        }

        public static void ButtonPreventDoubleClick(Button btn, ClientScriptManager csm)
        {
            try
            {
                btn.Attributes["onclick"] = "this.disabled = 'disabled';" + csm.GetPostBackEventReference(btn, "") + ";";
            }
            catch { }
        }


    }

}
