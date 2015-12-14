using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Broccoli
{
    class Downloader
    {
        public Model Model{ get; set; }

        /**
            Downloader constructor
        */
        public Downloader(Model model){
            Model = model;
        }

        /**
            method to be called to get a list with articles
        */
        public List<Article> Download() {
            return process();
        }

        /**
            download theverge.com rss feed and put it into an XDocument
        */
        private XDocument update() {
            // get rss feed from theverge.com
            string xml = new WebClient().DownloadString("http://www.theverge.com/rss/index.xml");

            // parse to xml
            XDocument doc = XDocument.Parse(xml);
            return doc;
        }

        /**
            process the theverge.com rss feed and put all articles into a List 
        */
        private List<Article> process() {
            List<Article> articles = new List<Article>();

            // get XDocument
            XDocument xd = update();

            // get title and link from the XDocument
            var res1 = from el in xd.Root.Elements()
                where el.Name.LocalName.Equals("entry")
                select new { title = el.Element("{http://www.w3.org/2005/Atom}title").Value,
                            link = el.Element("{http://www.w3.org/2005/Atom}id").Value };

            // add counter to set id of article
            int i = 1;

            // add articles to List<Article>
            foreach (var el in res1)
            {
                articles.Add(new Article(i, el.title, el.link));
                i++;
            }

            return articles;
        }
    }
}
