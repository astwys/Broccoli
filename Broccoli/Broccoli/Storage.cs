using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
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

			XmlDocument xdoc = new XmlDocument();
			xdoc.Load(PATH);
			var root = xdoc.DocumentElement;

			XmlElement xarticle = xdoc.CreateElement("article");
			XmlElement xid = xdoc.CreateElement("id");
			XmlElement xtitle = xdoc.CreateElement("title");
			XmlElement xlink = xdoc.CreateElement("link");

			int id = article.ID;
			string title = article.Title;
			string link = article.Link;

			xid.InnerText = id.ToString();
			xtitle.InnerText = title;
			xlink.InnerText = link;

			xarticle.AppendChild(xid);
			xarticle.AppendChild(xtitle);
			xarticle.AppendChild(xlink);

			root.AppendChild(xarticle);

		}

		private void load () {

			//when the method is called the first time the List needs to be created
			//in addition it needs to be checked if there is any former version of the save file
			if (!File.Exists(PATH)) {
				File.Create(PATH);
				SavedArticles = new List<Article>();
				return;
			}

			//load the xml file into the program
			var xmlin = XElement.Load(PATH);

			//query all articles and their content
			var articles =
				from input in xmlin.Descendants("article")
				select new { Id = input.Element("id").Value, Title = input.Element("title").Value, Link = input.Element("link").Value };

			//transform the result of the LinQ statement into an Array of Object (Id, Title, Link)
			var articlesArray = articles.ToArray();

			//check again for safety reasons wether the List is initiated
			if (SavedArticles == null)
				SavedArticles = new List<Article>();

			//iterate over the created array and add objects to it
			foreach (var article in articlesArray) {
				try {
					SavedArticles.Add(new Article(Convert.ToInt16(article.Id), article.Title, article.Link));
				} catch (Exception) {
					View.Error("An error occured as an Article was added to the system! Please check the format of the XML-file!");
				}
			}

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
