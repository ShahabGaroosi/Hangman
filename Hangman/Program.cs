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
            string word = GenerateWord(GetWordSet());
            char[] word0 = Enumerable.Repeat('_', word.Length).ToArray();

            StringBuilder guesses = new StringBuilder(",");
            string guess;
            int numberOfGuesses = 10;

            while (numberOfGuesses > 0)
            {
                PrintUpdate(word0, guesses, numberOfGuesses);
                guess = GetGuess();

                if (!guess.All(char.IsLetter))
                {
                    Console.WriteLine("Invalid guess.");
                }
                else if ((guesses.ToString().Contains($",{guess},")) || ((guess.Length==1) && (Array.IndexOf(word0, guess[0]) > -1)))
                {
                    Console.WriteLine("Guessed previously.");
                }
                else if ((guess.Length == 1) && (word.Contains(guess[0])))
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
                else if ((guess.Length > 1) && (word.Equals(guess, StringComparison.OrdinalIgnoreCase)))
                {
                    break;
                }
                else if (guess.Length > 0)
                {
                    guesses.Append(guess + ",");
                    numberOfGuesses--;
                }
            }
            PrintResult(word, numberOfGuesses);
        }

        static string[] GetWordSet()
        {
            string path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\wordSet.txt"; ;

            Console.WriteLine($"Write set of words (to be saved to {path}:");
            File.WriteAllText(path, Console.ReadLine());

            string[] wordSet = File.ReadAllText(path).Split(",".ToCharArray());
            File.Delete(path);

            return wordSet;
        }

        static string GenerateWord(string[] wordSet)
        {
            Random rng = new Random();
            return wordSet[rng.Next(wordSet.Length)].ToUpper();
        }

        static void PrintUpdate(char[] word0, StringBuilder guesses, int numberOfGuesses)
        {
            Console.WriteLine();
            foreach (char i in word0)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine($"\n{guesses}");
            Console.WriteLine($"{numberOfGuesses} guesses left.\n");
        }

        static string GetGuess()
        {
            Console.WriteLine("Guess word:");
            return Console.ReadLine().ToUpper();
        }

        static void PrintResult(string word, int numberOfGuesses)
        {
            Console.WriteLine($"\nSecret word: {word}");
            Console.WriteLine((numberOfGuesses > 0) ? "Correct guess!" : "You lose!");
        }

    }
}
