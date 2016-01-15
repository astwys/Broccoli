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
        public List<Article> Download(int source) {
            return process(source);
        }

        /**
            download theverge.com rss feed and put it into an XDocument
        */
        private XDocument update(int source) {
            // get rss feed from theverge.com
            string xml="";
            try {
                // choose source according to the users intput
                switch (source)
                {
                    case 1:
                        xml = new WebClient().DownloadString("http://www.theverge.com/rss/index.xml");
                        break;
                    case 2:
                        xml = new WebClient().DownloadString("http://www.theverge.com/google/rss/index.xml");
                        break;
                    case 3:
                        xml = new WebClient().DownloadString("http://www.theverge.com/apple/rss/index.xml");
                        break;
                    case 4:
                        xml = new WebClient().DownloadString("http://www.theverge.com/apps/rss/index.xml");
                        break;
                    case 5:
                        xml = new WebClient().DownloadString("http://www.theverge.com/rss/group/blackberry/index.xml");
                        break;
                    case 6:
                        xml = new WebClient().DownloadString("http://www.theverge.com/microsoft/rss/index.xml");
                        break;
                    case 7:
                        xml = new WebClient().DownloadString("http://www.theverge.com/mobile/rss/index.xml");
                        break;
                    case 8:
                        xml = new WebClient().DownloadString("http://www.theverge.com/photography/rss/index.xml");
                        break;
                    case 9:
                        xml = new WebClient().DownloadString("http://www.theverge.com/policy/rss/index.xml");
                        break;
                    case 10:
                        xml = new WebClient().DownloadString("http://www.theverge.com/web/rss/index.xml");
                        break;
                    default:
                        xml = new WebClient().DownloadString("http://www.theverge.com/rss/index.xml");
                        break;
                }
            } catch (WebException) {
               View.Error("There is a problem with your internet connection!");
                return null;
            }

            // parse to xml
            XDocument doc = XDocument.Parse(xml);
            return doc;
        }

        /**
            process the theverge.com rss feed and put all articles into a List 
        */
        private List<Article> process(int source) {
            List<Article> articles = new List<Article>();

            // get XDocument
            XDocument xd = update(source);

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
