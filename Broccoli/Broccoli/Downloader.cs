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


  }
}
