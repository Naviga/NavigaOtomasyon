using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace News
{
    public static class bllNews
    {
        public static int New(enNews news)
        {
            return new dalNews().New(news);
        }

        public static List<enNews> GetAll()
        {
            return new dalNews().GetAll();
        }
    }
}
