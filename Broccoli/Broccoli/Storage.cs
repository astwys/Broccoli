using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Broccoli
{
    class Storage
    {

		public static string PATH = "\\..\\..\\xmlfile.xml";
		public List<Article> SavedArticles { get; set; }
		private FileStream fs;

		public Storage () { //TODO !! !! !! as soon as the program is closed, deinitialise the fs
			if (File.Exists(PATH)) {
				fs = File.Open(PATH, FileMode.Open);
			} else {
				fs = File.Create(PATH);
			}
			load();
		}

		public void Store () {

		}

		public List<Article> Show () {
			return null;
		}

		private void load () {
			var xmlin = XElement.Load(PATH);

			var articles =
				from input in xmlin.Descendants("articles")
				select new { Id = input.Element("id").Value, Title = input.Element("title").Value, Link = input.Element("link").Value };

		}

    }
}

/**

	XML structure for the saved files

	<articles>

		<article>
			<id>$id</id>
			<title>$title</title>
			<link>$link</link>
		</article>

		...

	</articles>














**/


