using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broccoli {

	class Model {

		public View View { get; set; }
		public Controller Controller { get; set; }
		public Downloader Downloader { get; set; }
		public Storage Storage { get; set; }

		public Model () {
			View = new View(this);
			Controller = new Controller(this);
			Downloader = new Downloader(this);
			Storage = new Storage();
			Controller.Read();
		}

		#region show methods
		public void ShowArticles () {
			Console.WriteLine("Show articles");
		}

		public void ShowSavedArticles () {
			Console.WriteLine("Show saved articles");
		}

		public void ShowHelp () {
			Console.WriteLine("Show help articles");
		}
		#endregion

		#region read methods
		public void ReadStorage (int articleNumber) {
			Console.WriteLine("Read out of storage #"+ articleNumber +"");
		}

		public void ReadNew (int articleNumber) {
			Console.WriteLine("Read out of the new ones #"+ articleNumber +"");
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
