using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace News
{
    public class enNews
    {
        public enNews()
        {
            if (!AccessHelper.TableExists("News"))
            {
                Dictionary<string, string> columns = new Dictionary<string, string>();

                columns.Add("news_id", "AUTOINCREMENT PRIMARY KEY");
                columns.Add("news_title", "VARCHAR");
                columns.Add("news_content", "Memo");
                columns.Add("news_image", "VARCHAR");

                AccessHelper.CreateTable("News", columns);
            }
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
    }
}
