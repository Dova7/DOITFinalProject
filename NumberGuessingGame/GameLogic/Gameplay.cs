using NumberGuessingGame.GameLogic;
using NumberGuessingGame.Settings;

namespace NumberGuessingGame.GamePlay
{
    public class Gameplay
    {
        ScoringSystem scoringSystems = new ScoringSystem();
        Random random = new Random();
        public void PlayGame(byte maxNumber)
        {
            byte userGuess = 0;
            byte numberOfGuesses = 0;
            byte numberToGuess;
            char userDecision = ' ';
            float difficultyMultiplier = 1.0f;
            numberToGuess = GameSettings.GenerateRandomNumber(random, maxNumber);
            if (maxNumber > 0)
            {
                difficultyMultiplier = 1.0f + ((maxNumber - 15) / 10.0f) * 0.5f;
            }
            while (numberToGuess != userGuess)
            {
                Console.Write($"\nGuess a number between 1 - {maxNumber}. \nnumber of tries left {10 - numberOfGuesses}: ");
                string? input = Console.ReadLine();
                if (!byte.TryParse(input, out userGuess))
                {
                    Console.WriteLine($"\nInvalid input. Please enter a valid number.");
                    continue;
                }
                if (userGuess < 1 || userGuess > maxNumber)
                {
                    Console.WriteLine($"\nInvalid guess. Please enter a number between given range.");
                    continue;
                }

                Console.WriteLine("\nGuess: " + userGuess);

                if (userGuess > numberToGuess)
                {
                    Console.WriteLine(userGuess + " is too high!");
                }
                else if (userGuess < numberToGuess)
                {
                    Console.WriteLine(userGuess + " is too low!");
                }
                numberOfGuesses++;
                if (9 < numberOfGuesses)
                {
                    Console.WriteLine($"\nYou lose! \nCorrect number was {numberToGuess}");
                    break;
                }
                if (numberToGuess == userGuess)
                {
                    var score = scoringSystems.GetScore(numberOfGuesses, difficultyMultiplier);
                    Console.WriteLine($"\nCongratulations! You Guessed the number in {numberOfGuesses} tries.");
                    Console.WriteLine($"Your score for this round is: {score}");
                    Console.WriteLine("Correct number was " + numberToGuess);

                    Console.WriteLine("Enter your name: ");
                    string? playerName = Console.ReadLine().ToLower();
                    scoringSystems.AddRecord(playerName, score);
                }
            }
            Console.WriteLine("\nTry again? (Y/N)");
            while (userDecision != 'N' && userDecision != 'Y')
            {
                string? input = Console.ReadLine().ToUpper();
                if (!string.IsNullOrEmpty(input) && input.Length == 1)
                {
                    userDecision = Char.Parse(input);
                }
                if (userDecision != 'N' && userDecision != 'Y')
                {
                    Console.WriteLine("Invalid input. Please enter a valid option (Y/N).");
                }
            }
            if (userDecision == 'N')
            {
                return;
            }
            else
            {
                PlayGame(maxNumber);
            }
        }
    }
}