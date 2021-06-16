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
            Console.WriteLine("Hello World!");
            string path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\wordSet.txt"; ;
            
            Console.WriteLine("Write set of words (to be saved to " + path + "):");
            File.WriteAllText(path, Console.ReadLine());
            
            string[] wordSet = File.ReadAllText(path).Split(",".ToCharArray());
            
            foreach(string i in wordSet)
            {
                Console.WriteLine(i);
            }

            Random rng = new Random();
            string word = wordSet[rng.Next(wordSet.Length)];
            char[] word0 = Enumerable.Repeat(char.Parse("_"), word.Length).ToArray();

            Console.WriteLine(word0);
            foreach(char i in word0)
            {
                Console.Write(i + " ");
            }

            string guess;
            StringBuilder guesses = new StringBuilder();
            int numberOfGuesses = 0;
            do
            {
                Console.WriteLine("Guess word:");
                guess = Console.ReadLine();
                if (guess.Length == 1)
                {
                    if (word0.Contains(guess))
                    {
                        continue;
                    }
                    if (word.Contains(guess))
                    {
                        for (int i = 0; i < word.Length; i++)
                        {
                            if (word[i] == guess[0])
                            {
                                word0[i] = guess[0];
                            }
                        }
                    }
                    else
                    {
                        guesses.Append(guess + ", ");
                        numberOfGuesses++;
                    }
                }
                else if (guess.Length > 1)
                {
                    if(word.Equals(guess, StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("You guessed correctly!");
                        break;
                    }
                    else
                    {
                        guesses.Append(guess + ", ");
                        numberOfGuesses++;
                    }
                }
                foreach (char i in word0)
                {
                    Console.Write(i + " ");
                }
                Console.WriteLine(guesses);

            } while ((numberOfGuesses < 10)&(!word0.Contains("_")));

            //string guessed;
            //word.Equals(guessed, StringComparison.OrdinalIgnoreCase);
        }
    }
}
