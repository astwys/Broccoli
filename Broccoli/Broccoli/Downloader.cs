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

      public Downloader(Model model){
          Model = model;
      }

      private XDocument update() {
          string xml = new WebClient().DownloadString("http://www.theverge.com/rss/index.xml");
          XDocument doc = XDocument.Parse(xml);
          return doc;
      }

      private List<Article> process() {
            List<Article> articles = new List<Article>();
            XDocument xd = update();
            var res1 = from el in xd.Root.Elements()
                       where el.Name.LocalName.Equals("entry")
                       select new { title = el.Element("{http://www.w3.org/2005/Atom}title").Value, link = el.Element("{http://www.w3.org/2005/Atom}id").Value };

            int i = 1;
            foreach (var el in res1)
            {
                articles.Add(new Article(i, el.title, el.link));
                i++;
            }
            return articles;
        }
    }
}
