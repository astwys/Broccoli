using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broccoli
{
    class Controller
    {

		public Model Model { get; set; }

		public Controller (Model model) {
			Model = model;
		}

		public void Read () {
			processInput(Console.ReadLine());
		}

		private void processInput (string input) {
			switch (input) {
				case "whats up":
					whatsUp();
				break;

				default:
					unkown();
				break;
			}
		}

		private void whatsUp () {

		}

		private void unkown () {

		}

    }
}
