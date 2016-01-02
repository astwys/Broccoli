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
			foreach (var articles in Storage.Show()) {
				Console.WriteLine(articles.ID+": "+articles.Title);
			}

		}

		public void ShowHelp () {
			Console.WriteLine("The available commands are shown below:");
            Console.WriteLine("\thelp\n\t\tshow this information\n\n"+
                "\twhats up\n\t\tget the latest articles\n\n"+
                "\tshow saved\n\t\tshow the articles in the storage\n\n"+
                "\tread -n|-s $articlenumber\n\t\topen new|saved article with the number $article number\n\n" +
                "\tsave $articlenumber\n\t\tsave article with number $articlenumber to storage\n\n"+
                "\tdelete $articlenumber\n\t\tdelete article with the number $articlenumber from storage\n"+
                "\texit\n\t\texit Broccoli");
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
            Storage.Store(newArticles[articleNumber-1]);
		}

		public void Delete (int articleNumber) {
            Storage.Delete(articleNumber);
		}
		#endregion

	}
}
