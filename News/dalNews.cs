using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace News
{
    public class dalNews
    {
        public int New(enNews news)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();

            dict.Add("news_title", news.Title);
            dict.Add("news_content", news.Content);
            dict.Add("news_image", news.Image);

            return AccessHelper.Insert("News", dict, "news_id");
        }

        public List<enNews> GetAll()
        {
            DataTable dt = AccessHelper.Select_Dt("News");

            List<enNews> newsList = new List<enNews>();
            foreach (DataRow rw in dt.Rows)
            {
                enNews news = new enNews();

                news.Id = rw["news_id"].xToIntDefault();
                news.Content = rw["news_content"].ToString();
                news.Image = rw["news_image"].ToString();
                news.Title = rw["news_title"].ToString();

                newsList.Add(news);
            }

            return newsList;
        }
    }
}
