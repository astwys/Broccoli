using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broccoli {

	class Model {

		public View View { get; set; }
		public Controller Controller { get; set; }
		public Downloader Downloader { get; set; }
		public Storage Storage { get; set; }

		private List<Article> newArticles;

		public Model () {
			View = new View(this);
			Controller = new Controller(this);
			Downloader = new Downloader(this);
			Storage = new Storage();
			Controller.Read();
		}

		#region show methods
		public void ShowArticles () {

			//assign the downloaded articles to the array
			newArticles = Downloader.Download();

			Console.WriteLine("Here are the latest articles:");
			foreach (var article in newArticles) {
				Console.WriteLine(article.ID+": "+article.Title);
			}

		}

		public void ShowSavedArticles () {

			Console.WriteLine("Here are your saved articles:");
			foreach (var articles in Storage.SavedArticles) {
				Console.WriteLine(articles.ID+": "+articles.Title);
			}

		}

		public void ShowHelp () {
			Console.WriteLine("Show help articles");
		}
		#endregion

		#region read methods
		public void ReadStorage (int articleNumber) {
			Process.Start(Storage.SavedArticles[articleNumber - 1].Link);
		}

		public void ReadNew (int articleNumber) {
			if (newArticles == null)
				newArticles = Downloader.Download();
			Process.Start(newArticles[articleNumber - 1].Link);
		}
		#endregion

		#region storage management
		public void Save (int articleNumber) {
			Console.WriteLine("Save article #"+ articleNumber);
		}

		public void Delete (int articleNumber) {
			Console.WriteLine("Delete article");
		}
		#endregion

	}
}
