using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entity;
using DAL;

namespace BLL
{
    public static class bllGmap
    {
        public static enGmap GmapGetir()
        {
            return new dalGmap().GmapGetir();
        }

        public static void GmapGuncelle(enGmap gmap)
        {
            new dalGmap().GmapGuncelle(gmap);
        }
    }
}
