using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broccoli {
	class Controller {

		public Model Model { get; set; }

		public Controller (Model model) {
			Model = model;
		}

		public void Read () {
			Console.Write(">");
			processInput(Console.ReadLine());
			Read();
		}

		private void processInput (string input) {
            if (input == null || input.Equals(" ") || input.Equals("")) {
                Read();
            }
			if (input.Contains("save") && !input.Contains("show"))
				save(input);
			else if (input.Contains("read"))
				readStorageOrNew(input);
			else if (input.Contains("delete"))
				delete(input);
			else
				switch (input) {
					case "whats up":
					whatsUp();
					break;

					case "exit":
					exit();
					break;

					case "show saved":
					showSaved();
					break;

					case "help":
					help();
					break;

                    case "source":
                    changeSource();
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
            try {
                Model.ShowArticles(Source);
            } catch (Exception e) {
                //error(e.Message);
            }
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
		}

		private void readNew (string input) {
			try {
				string[] words = input.Split(' ');
				int number = Convert.ToInt16(words[2]);
				if (number > 10 || number <= 0)
					throw new FormatException();
				Model.ReadNew(number, Source);
			} catch (IndexOutOfRangeException) {
				error("The command is invalid. Please make sure you entered it in the correct format and a valid article number!");
			} catch (FormatException) {
				error("The command is invalid. Please make sure you entered a valid article number!");
			} catch (Exception) {
				error("An unknown error occured!");
			}
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
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Article " + number + " has been saved.");
                Console.ResetColor();
            }
            catch (IndexOutOfRangeException) {
                error("The command is invalid. Please make sure you entered it in the correct format and a valid article number!");
            } catch (FormatException) {
                error("The command is invalid. Please make sure you entered a valid article number!");
            } catch (NullReferenceException) {
                error("Make sure you typed \"whats up\" before saving!");
			} catch (Exception e) {
                error(e.Message);
			}
		}

		//delete an article (with the specific number)
		private void delete (string input) {
			try {
				string[] words = input.Split(' ');
				int number = Convert.ToInt16(words[1]);
				if (number > 10 || number <= 0)
					throw new FormatException();
				Model.Delete(number);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Article " + number + " has been deleted.");
                Console.ResetColor();
            }
            catch (IndexOutOfRangeException) {
				error("The command is invalid. Please make sure you entered it in the correct format and have a valid article number!");
			} catch (FormatException) {
				error("The command is invalid. Please make sure you entered a valid article number!");
			} catch (Exception e) {
                error(e.Message);
			}
		}
		#endregion

		#region errors
		private void unkown () {
			View.Error("This command is unknown!");
		}

		private void error (string message) {
			View.Error(message);
		}

        #endregion

        public int Source { get; set; } = 1;

        public static string[] sources = { "General", "Android", "Apple", "Apps & Software", "Blackberry", "Microsoft", "Mobile", "Photography", "Policy & Law", "Web & Social" };

        /**
            change the section in theverge to use
        */
        private void changeSource()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Which source would you like to use? [current: ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(sources[Source - 1]);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("]");
            Console.ResetColor();
            Model.ShowSources();

            int number;

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("source");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(">");
            Console.ResetColor();

            bool chosen = int.TryParse(Console.ReadLine(), out number);
            if (!chosen)
            {
                error("This source does not exist. Please choose another one.\n");
                changeSource();
            }
            else
            {
                Source = number;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("You selected " + sources[Source-1]);
                Console.ResetColor();
            }
        }
    }
}
