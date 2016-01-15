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
		public void ShowArticles (int source) {

			//assign the downloaded articles to the array
			newArticles = Downloader.Download(source);
            View.ColorStringNL("Here are the latest articles:", ConsoleColor.Yellow);

			foreach (var article in newArticles) {
				Console.WriteLine(article.ID+": "+article.Title);
			}

		}

		public void ShowSavedArticles () {
            View.ColorStringNL("Here are your saved articles:", ConsoleColor.Yellow);

            foreach (var articles in Storage.Show()) {
				Console.WriteLine(articles.ID+": "+articles.Title);
			}

		}

		public void ShowHelp () {
			Console.WriteLine("The available commands are shown below:");

            View.ColorStringNL("\thelp", ConsoleColor.Yellow);
            Console.WriteLine("\t\tshow this information\n");

            View.ColorStringNL("\twhats up", ConsoleColor.Yellow);
            Console.WriteLine("\t\tget the latest articles\n");

            View.ColorStringNL("\tsource", ConsoleColor.Yellow);
            Console.WriteLine("\t\tchange the source for articles to show\n");

            View.ColorStringNL("\tshow saved", ConsoleColor.Yellow);
            Console.WriteLine("\t\tshow the articles in the storage\n");

            View.ColorStringNL("\tread -n|-s $articlenumber", ConsoleColor.Yellow);
            Console.WriteLine("\t\topen new|saved article with the number $article number\n");

            View.ColorStringNL("\tsave $articlenumber", ConsoleColor.Yellow);
            Console.WriteLine("\t\tsave article with number $articlenumber to storage\n");

            View.ColorStringNL("\tdelete $articlenumber", ConsoleColor.Yellow);
            Console.WriteLine("\t\tdelete article with the number $articlenumber from storage\n");

            View.ColorStringNL("\texit", ConsoleColor.Yellow);
            Console.WriteLine("\t\texit Broccoli");
		}

        public void ShowSources()
        {
            int i = 1;
            foreach (var source in Controller.sources)
            {
                Console.WriteLine(i + ": " + source);
                i++;
            }
        }
        #endregion

        #region read methods
        public void ReadStorage (int articleNumber) {
			Process.Start(Storage.SavedArticles[articleNumber - 1].Link);
		}

		public void ReadNew (int articleNumber, int source) {
			if (newArticles == null)
				newArticles = Downloader.Download(source);
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
