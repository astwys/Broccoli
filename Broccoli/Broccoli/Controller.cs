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
			Console.Write("\n>");
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

		//exit the program
		private void exit () {
			Environment.Exit(0);
		}

		//show the help section
		private void help () {
			Model.ShowHelp();
		}

		//get update from TheVerge
		private void whatsUp () {
			Model.ShowArticles();
		}

		//shows the saved articles
		private void showSaved () {
			Model.ShowSavedArticles();
		}

		#region read commad
		/**
		*	Handling of the read input
		*	-> looking for -s / -n (saved or new article)
		*	-> calling corr. method in model
		**/

		private void readStorageOrNew (string input) {
			if (input.Contains("-s"))
				readStorage(input);
			else if (input.Contains("-n"))
				readNew(input);
			else
				error("The command is incomplete!");
			//return to Read method to read the user's entry
			Read();
		}

		private void readStorage (string input) {
			try {
				string[] words = input.Split(' ');
				int number = Convert.ToInt16(words[2]);
				if (number > 10 || number <= 0)
					throw new FormatException();
				Model.ReadStorage(number);
			} catch (IndexOutOfRangeException) {
				error("The command is invalid. Please make sure you entered it in the correct format and a valid article number!");
			} catch (FormatException) {
				error("The command is invalid. Please make sure you entered a valid article number!");
			} catch (Exception) {
				error("An unknown error occured!");
			}
			//return to Read method to read the user's entry
			Read();
		}

		private void readNew (string input) {
			try {
				string[] words = input.Split(' ');
				int number = Convert.ToInt16(words[2]);
				if (number > 10 || number <= 0)
					throw new FormatException();
				Model.ReadNew(number);
			} catch (IndexOutOfRangeException) {
				error("The command is invalid. Please make sure you entered it in the correct format and a valid article number!");
			} catch (FormatException) {
				error("The command is invalid. Please make sure you entered a valid article number!");
			} catch (Exception) {
				error("An unknown error occured!");
			}
			//return to Read method to read the user's entry
			Read();
		}

		#endregion command

		#region storage management
		//save the entered articel from the list of new articles
		private void save (string input) {
			try {
				string[] words = input.Split(' ');
				int number = Convert.ToInt16(words[1]);
				if (number > 10 || number <= 0)
					throw new FormatException();
				Model.Save(number);
			} catch (IndexOutOfRangeException) {
				error("The command is invalid. Please make sure you entered it in the correct format and a valid article number!");
			} catch (FormatException) {
				error("The command is invalid. Please make sure you entered a valid article number!");
			} catch (Exception) {
				error("An unknown error occured!");
			}
			//return to Read method to read the user's entry
			Read();
		}
		
		//delete an article (with the specific number)
		private void delete (string input) {
			try {
				string[] words = input.Split(' ');
				int number = Convert.ToInt16(words[1]);
				if (number > 10 || number <= 0)
					throw new FormatException();
				Model.Delete(number);
			} catch (IndexOutOfRangeException) {
				error("The command is invalid. Please make sure you entered it in the correct format and a valid article number!");
			} catch (FormatException) {
				error("The command is invalid. Please make sure you entered a valid article number!");
			} catch (Exception) {
				error("An unknown error occured!");
			}
			//return to Read method to read the user's entry
			Read();
		}
		#endregion

		#region errors
		private void unkown () {

		}

		private void error (string message) {
			Model.View.Error(message);
		}
		#endregion
	}
}
