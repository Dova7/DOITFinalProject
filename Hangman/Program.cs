using Hangman.Constants;
using Hangman.GameLogic;
using Hangman.Repository;
using System.Text.RegularExpressions;

namespace Hangman
{
    public class Program
    {
        static void Main(string[] args)
        {
            PlayerXMLRepository playerXMLRepository = new PlayerXMLRepository();
            ScoringSystem scoringSystem = new ScoringSystem();
            Gameplay gameplay = new Gameplay();
            Console.WriteLine("Hangman!");
            while (true)
            {
                Console.WriteLine("\n1. Start.");
                Console.WriteLine("2. High score.");
                Console.WriteLine("0. Exit.");
                Console.WriteLine("------------");
                string? userInput = Console.ReadLine();

                if (Regex.IsMatch(userInput, @"^(0|1|2)$"))
                {
                    byte choice = byte.Parse(userInput);
                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine("\nChoose type of words.");
                            Console.WriteLine("1. Animals.");
                            Console.WriteLine("2. Fruits.");
                            Console.WriteLine("3. Countries.");
                            var userChoice = AskForType();
                            var wordList = GetType(userChoice);
                            gameplay.PlayGame(wordList);
                            break;
                        case 2:
                            scoringSystem.DisplayScore();
                            break;
                        case 0:
                            return;
                    }
                }
                else
                {
                    Console.WriteLine("\nInvalid input. Please enter a valid option (0, 1, 2, 3 or 4).");
                }
            }
        }
        private static byte AskForType()
        {
            var incorrect = true;
            string? input;
            byte choice;
            var wordList = new List<string>();
            do
            {
                input = Console.ReadLine();
                if (!Regex.IsMatch(input, @"^(1|2|3)$"))
                {
                    Console.WriteLine("Invalid input. Please enter valid number. (1, 2, 3)");
                }
                else
                {
                    incorrect = false;
                }
            } while (incorrect);
            choice = byte.Parse(input);
            return choice;
        }
        private static List<string> GetType(byte choice)
        {
            var wordList = new List<string>();
            if (choice == 1)
            {
                wordList = WordData.AnimalList;
            }
            else if (choice == 2)
            {
                wordList = WordData.FruitList;
            }else
            {
                wordList = WordData.CountryList;
            }            
            return wordList;
        }
    }
}