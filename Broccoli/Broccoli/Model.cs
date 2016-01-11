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

			Console.WriteLine("Here are the latest articles:");
			foreach (var article in newArticles) {
				Console.WriteLine(article.ID+": "+article.Title);
			}

		}

		public void ShowSavedArticles () {

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Here are your saved articles:");
            Console.ResetColor();
            foreach (var articles in Storage.Show()) {
				Console.WriteLine(articles.ID+": "+articles.Title);
			}

		}

		public void ShowHelp () {
			Console.WriteLine("The available commands are shown below:");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\thelp");
            Console.ResetColor();
            Console.WriteLine("\t\tshow this information\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\twhats up");
            Console.ResetColor();
            Console.WriteLine("\t\tget the latest articles\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\tsource");
            Console.ResetColor();
            Console.WriteLine("\t\tchange the source for articles to show\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\tshow saved");
            Console.ResetColor();
            Console.WriteLine("\t\tshow the articles in the storage\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\tread -n|-s $articlenumber");
            Console.ResetColor();
            Console.WriteLine("\t\topen new|saved article with the number $article number\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\tsave $articlenumber");
            Console.ResetColor();
            Console.WriteLine("\t\tsave article with number $articlenumber to storage\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\tdelete $articlenumber");
            Console.ResetColor();
            Console.WriteLine("\t\tdelete article with the number $articlenumber from storage\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\texit");
            Console.ResetColor();
            Console.WriteLine("\t\texit Broccoli");
		}

        public void ShowSources()
        {
            Console.WriteLine("1: general\n" +
                "2: android\n" +
                "3: apple\n" +
                "4: apps & software\n" +
                "5: blackberry\n" +
                "6: microsoft\n" +
                "7: mobile\n" +
                "8: photography\n" +
                "9: policy & law\n" +
                "10: web & social");
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
