using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broccoli {

	class View {

		public Model Model { get; set; }

		public View (Model model) {
			Model = model;
			WelcomeMessage();
		}

		public void WelcomeMessage () {
			Console.Write("Hello and welcome to Broccoli. To find out about the latest news of TheVerge\njust type \"whats up\" and select your desired article.\nFor further information type \"help\".\n");
		}

		public static void Error (string message) {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

    }
}
