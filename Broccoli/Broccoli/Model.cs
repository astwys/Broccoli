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

		}

		public void ShowSavedArticles () {

		}

		public void ShowHelp () {

		}
		#endregion

		#region read methods
		public void ReadStorage (int articleNumber) {

		}

		public void ReadNew (int articleNumber) {

		}
		#endregion

		#region storage management
		public void Save (int articleNumber) {

		}

		public void Delete (int articleNumber) {

		}
		#endregion

	}
}
