using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broccoli
{
    class Article
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }

        /**
            Article Constructor
        */
        public Article (int id, string title, string link) {
            ID = id;
            Title = title;
            Link = link;
        }

        /**
            article ToString
            returns the id + title
        */
        public override string ToString()
        {
            return ID + ": " + Title;
        }
    }
}
