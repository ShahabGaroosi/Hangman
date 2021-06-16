using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Hangman
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\wordSet.txt"; ;
            
            Console.WriteLine("Write set of words (to be saved to " + path + "):");
            File.WriteAllText(path, Console.ReadLine());
            
            string[] wordSet = File.ReadAllText(path).Split(",".ToCharArray());

            Random rng = new Random();
            string word = wordSet[rng.Next(wordSet.Length)];
            char[] word0 = Enumerable.Repeat('_', word.Length).ToArray();

            string guess;
            StringBuilder guesses = new StringBuilder();
            int numberOfGuesses = 10;

            while (numberOfGuesses > 0)
            {
                Console.Write("\n");
                foreach (char i in word0)
                {
                    Console.Write(i + " ");
                }
                Console.Write("\n");
                Console.WriteLine(guesses);
                Console.WriteLine(numberOfGuesses);
                
                Console.WriteLine("Guess word:");
                guess = Console.ReadLine();
                if (!guess.All(char.IsLetter))
                {
                    Console.WriteLine("Invalid guess.");
                }
                else if ((guesses.ToString().Contains(guess+", "))||(Array.IndexOf(word0, guess) > -1))
                {
                    Console.WriteLine("Guessed previously.");
                }
                else if (guess.Length == 1)
                {
                    
                    if (word.Contains(guess))
                    {
                        for (int i = 0; i < word.Length; i++)
                        {
                            if (word[i] == guess[0])
                            {
                                word0[i] = guess[0];
                            }
                        }
                        if (Array.IndexOf(word0, '_') < 0)
                        {
                            break;
                        }
                    }
                    else
                    {
                        guesses.Append(guess + ", ");
                        numberOfGuesses--;
                    }
                }
                else if (guess.Length > 1)
                {
                    if (word.Equals(guess, StringComparison.OrdinalIgnoreCase))
                    {
                        break;
                    }
                    else
                    {
                        guesses.Append(guess + ", ");
                        numberOfGuesses--;
                    }
                }
            }

            Console.WriteLine((numberOfGuesses > 0) ? "Correct guess!" : "You lose!");
        }
    }
}
