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
			View = new View();
			Controller = new Controller();
			Downloader = new Downloader(this);
			Storage = new Storage();
		}

		public void ShowArticles () {

		}

		public void ShowSavedArticles () {

		}

		public void ShowHelp () {

		}

		public void Save () {

		}

	}
}
