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
            ColorStringNL(message, ConsoleColor.Red);
        }


        /**
            change color of the complete line
        */
        public static void ColorStringNL(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        /**
            change color in line
        */
        public static void ColorStringIL(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(message);
        }
    }
}
