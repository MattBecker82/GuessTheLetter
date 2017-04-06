using System.IO;
using System.Linq;

namespace GuessTheLetter
{
    class Program
    {
        static string wordListFile = "wordlist.txt";
        const int maxWordLength = 6;

        static void Main(string[] args)
        {
            // Initialize UI
            var ui = new ConsoleUi();
            ui.Initialize();

            // Load the wordlist
            var wordList = new WordList(File.ReadAllLines(wordListFile).Select(w => w.Trim().ToLowerInvariant()));

            var cont = true;
            while (cont)
            {
                ui.WaitMessage("Think of a letter (a to z).");

                // Initialize possible letters
                var possibles = "abcdefghijklmnopqrstuvwxyz";
                // Try random words until at most maxWordLength remain
                while(possibles.Length > 1)
                {
                    var word = wordList.GetRandomWord(possibles, maxWordLength);
                    if (word == null)
                    {
                        possibles = string.Empty;
                        break;
                    }

                    var responseYes = ui.AskYesOrNo("Is your letter in the word '{0}'?", word);
                    
                    if (responseYes)
                    {
                        possibles = new string(possibles.Intersect(word).ToArray());
                    }
                    else
                    {
                        possibles = new string(possibles.Except(word).ToArray());
                    }
                }

                if (possibles.Length == 1)
                {
                    ui.Message("Your letter was: {0}", possibles);
                }
                else
                {
                    ui.Message("I give up. You win!");
                }

                cont = ui.AskYesOrNo("Do you want to play again?");
            }
        }
    }
}
