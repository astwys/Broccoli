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

		public Storage () {
			load();
		}

		public void Store (Article article) {
			save(article);
			load();
		}

		public List<Article> Show () {
			return SavedArticles;
		}

		private void save (Article article) {

			XmlDocument xdoc = new XmlDocument();
			xdoc.Load(PATH);

			// set root element --> <articles>
			var root = xdoc.DocumentElement;

			// create new elements
			XmlElement xarticle = xdoc.CreateElement("article");
			XmlElement xid = xdoc.CreateElement("id");
			XmlElement xtitle = xdoc.CreateElement("title");
			XmlElement xlink = xdoc.CreateElement("link");

            //get the highest id
            int max = 0;
            foreach (var a in SavedArticles) {
                if (a.ID > max)
                    max = a.ID;
            }

            // get information about the article
            int id = max+1;
			string title = article.Title;
			string link = article.Link;

			// set text inside of the elements
			xid.InnerText = id.ToString();
			xtitle.InnerText = title;
			xlink.InnerText = link;

			// add id, title & link elements to the article element
			xarticle.AppendChild(xid);
			xarticle.AppendChild(xtitle);
			xarticle.AppendChild(xlink);

			// add article to the root element
			root.AppendChild(xarticle);
            xdoc.AppendChild(root);
            xdoc.Save(PATH);

		}

		private void load () {

			//when the method is called the first time the List needs to be created
			//in addition it needs to be checked if there is any former version of the save file
			if (!File.Exists(PATH)) {
				File.Create(PATH);
				return;
			}

            //reinitialise the list so we have no duplicates
            SavedArticles = new List<Article>();

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
					throw new Exception("An error occured as an Article was added to the system! Please check the format of the XML-file!");
				}
			}

		}

        public void Delete (int articleNumber) {

            if (!File.Exists(PATH))
                throw new Exception("There are no saved articles yet!");

            var xmlin = XDocument.Load(PATH);

            try {
                xmlin.Descendants("article").Where(a => a.Element("id").Value.Equals("" + articleNumber)).Single().Remove();
            } catch (InvalidOperationException) {
                if (SavedArticles.Count<=0) {
                    throw new Exception("There are no saved articles yet!");
                }
            }

            //updated save and load the changed data
            update(ref xmlin, articleNumber);
            xmlin.Save(PATH);
            load();

        }

        //is called when the user deletes one of the saved articles
        //the IDs are uopdated
        private void update (ref XDocument doc, int deletedArticle) {

            var toupdate =
                from articles in doc.Descendants("article")
                where Convert.ToInt16(articles.Element("id").Value) > deletedArticle
                select articles;

            foreach (var article in toupdate) {
                article.Element("id").Value = (Convert.ToInt16(article.Element("id").Value)-1).ToString();
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
