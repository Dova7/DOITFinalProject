using NumberGuessingGame.GameLogic;
using NumberGuessingGame.GamePlay;
using System.Text.RegularExpressions;

namespace NumberGuessingGame
{
    public class Program
    {
        static void Main()
        {
            ScoringSystem scoringSystem = new ScoringSystem();
            Gameplay gameplay = new Gameplay();
            byte maxNumber = 0;            

            Console.WriteLine("\nNumber guessing game!");

            while (true)
            {
                Console.WriteLine("\nSelect difficulty.");
                Console.WriteLine("1. Easy (1-15).");
                Console.WriteLine("2. Medium (1-25).");
                Console.WriteLine("3. Hard (1-50).");
                Console.WriteLine("4. High score.");
                Console.WriteLine("0. Exit.");
                Console.WriteLine("------------");

                string? userInput = Console.ReadLine();

                if (Regex.IsMatch(userInput, @"^(0|1|2|3|4)$"))
                {
                    byte choice = byte.Parse(userInput);
                    switch (choice)
                    {
                        case 1:
                            maxNumber = 15;
                            gameplay.PlayGame(maxNumber);
                            break;
                        case 2:
                            maxNumber = 25;
                            gameplay.PlayGame(maxNumber);
                            break;
                        case 3:
                            maxNumber = 50;
                            gameplay.PlayGame(maxNumber);
                            break;
                        case 4:
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
    }
}