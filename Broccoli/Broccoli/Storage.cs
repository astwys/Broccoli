using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Broccoli {
	class Storage {

		public static string PATH = "..\\..\\xmlfile.xml";
		public List<Article> SavedArticles { get; set; }
		private FileStream fs;

		public Storage () {
			load();
		}

		public void Store (Article article) {
			save(article); //TODO pass Article
			load();
		}

		public List<Article> Show () {
			return SavedArticles;
		}

		private void save (Article article) {

		}

		private void load () {

			if (!File.Exists(PATH)) {
				File.Create(PATH);
				SavedArticles = new List<Article>();
				return;
			}

			var xmlin = XElement.Load(PATH);

			var articles =
				from input in xmlin.Descendants("article")
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
