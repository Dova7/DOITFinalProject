using Hangman.Settings;
using System.Text.RegularExpressions;
using static System.Formats.Asn1.AsnWriter;

namespace Hangman.GameLogic
{
    public class Gameplay
    {
        GameSettings gameSettings = new GameSettings();
        ScoringSystem scoringSystems = new ScoringSystem();
        Random random = new Random();
        public void PlayGame(List<string> wordList)
        {
            var lettersGuessed = new List<char>();
            char userGuess;
            string? finalGuess;
            string wordToGuess = gameSettings.GenerateRandomWord(random, wordList).ToLower();
            byte numberOfGuesses = 0;
            string mask = GetMask(wordToGuess);
            char userDecision = ' ';
            int score = 0;

            while (numberOfGuesses < 6)
            {                
                Console.Write($"\nGuess a letter (a-z). \nnumber of tries left {6 - numberOfGuesses}: " +
                    $"\n\nWord to guess: {mask}\n");
                string? input = Console.ReadLine();
                if (!Regex.IsMatch(input, @"^[a-zA-Z]$"))
                {
                    Console.WriteLine($"\nInvalid input. Please enter a valid letter (a-z).");
                    continue;
                }
                if (input.Length != 1)
                {
                    Console.WriteLine($"\nInvalid input. Input must be a letter (a-z).");
                    continue;
                }
                userGuess = Convert.ToChar(input.ToLower());
                if (lettersGuessed.Contains(userGuess))
                {
                    Console.WriteLine($"\nYou already guessed '{userGuess}'. Try another letter.");
                    continue;
                }
                if (wordToGuess.Contains(userGuess))
                {
                    mask = UpdateMask(mask, wordToGuess, userGuess);
                    Console.WriteLine("\nCorrect!");
                    lettersGuessed.Add(userGuess);
                    if (mask == wordToGuess)
                    {
                        score = scoringSystems.GetScore(numberOfGuesses);
                        Console.WriteLine($"\nCongratulations! You guessed the word '{wordToGuess}' correctly!");
                        Console.WriteLine($"Your score for this round was {score}");
                        SaveScore(score);
                        break;
                    }
                    continue;
                }
                else
                {
                    lettersGuessed.Add(userGuess);
                    PrintHangman(numberOfGuesses);
                    numberOfGuesses++;
                }
            }
            bool wordGuessed = true;
            if(mask == wordToGuess)
            {
                wordGuessed = false;
            }
            while (wordGuessed)
            {
                Console.WriteLine("\nYou are out of letter guesses. Final chance. Try to guess entire word: ");
                finalGuess = Console.ReadLine().ToLower();
                if (!finalGuess.All(char.IsLetter))
                {
                    Console.WriteLine($"\nInvalid input. Please enter a valid word (a-z).");
                    continue;
                }
                else if (finalGuess == wordToGuess)
                {
                    score = scoringSystems.GetScore(numberOfGuesses);
                    Console.WriteLine($"\nCongratulations! You guessed the word '{wordToGuess}' correctly!");
                    Console.WriteLine($"Your score for this round was {score}");
                    SaveScore(score);
                    break;
                }
                else
                {
                    PrintHangman(numberOfGuesses);
                    Console.WriteLine($"\nYou lose! Correct word was {wordToGuess}");
                    break;
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
                PlayGame(wordList);
            }
        }
        private void PrintHangman(byte numberOfGuesses)
        {
            Console.WriteLine("\nWrong!");
            if (numberOfGuesses == 0)
            {
                Console.WriteLine("\n------------");
                Console.WriteLine("\n +---+");
                Console.WriteLine("     |");
                Console.WriteLine("     |");
                Console.WriteLine("     |");
                Console.WriteLine("    ===");
            }
            else if (numberOfGuesses == 1)
            {
                Console.WriteLine("\n------------");
                Console.WriteLine("\n +---+");
                Console.WriteLine(" O   |");
                Console.WriteLine("     |");
                Console.WriteLine("     |");
                Console.WriteLine("    ===");

            }
            else if (numberOfGuesses == 2)
            {
                Console.WriteLine("\n------------");
                Console.WriteLine("\n +---+");
                Console.WriteLine(" O   |");
                Console.WriteLine(" |   |");
                Console.WriteLine("     |");
                Console.WriteLine("    ===");

            }
            else if (numberOfGuesses == 3)
            {
                Console.WriteLine("\n------------");
                Console.WriteLine("\n +---+");
                Console.WriteLine(" O   |");
                Console.WriteLine("/|   |");
                Console.WriteLine("     |");
                Console.WriteLine("    ===");
            }
            else if (numberOfGuesses == 4)
            {
                Console.WriteLine("\n------------");
                Console.WriteLine("\n +---+");
                Console.WriteLine(" O   |");
                Console.WriteLine("/|\\  |");
                Console.WriteLine("     |");
                Console.WriteLine("    ===");
            }
            else if (numberOfGuesses == 5)
            {
                Console.WriteLine("\n------------");
                Console.WriteLine("\n +---+");
                Console.WriteLine(" O   |");
                Console.WriteLine("/|\\  |");
                Console.WriteLine("/    |");
                Console.WriteLine("    ===");

            }
            else if (numberOfGuesses == 6)
            {
                Console.WriteLine("\n------------");
                Console.WriteLine("\n +---+");
                Console.WriteLine(" O   |");
                Console.WriteLine("/|\\  |");
                Console.WriteLine("/ \\  |");
                Console.WriteLine("    ===");
            }
        }
        private string GetMask(string wordToGuess)
        {
            int size = wordToGuess.Length;
            return new string('-', size);
        }
        private string UpdateMask(string mask, string wordToGuess, char letter)
        {
            for (int i = 0; i < wordToGuess.Length; i++)
            {
                if (letter == wordToGuess[i])
                {
                    mask = mask.Remove(i, 1).Insert(i, letter.ToString());
                }
            }
            return mask;
        }
        private void SaveScore(int score)
        {
            Console.WriteLine("\nEnter your name: ");
            string? playerName = Console.ReadLine().ToLower();
            if (!playerName.All(char.IsLetter) || string.IsNullOrWhiteSpace(playerName))
            {
                Console.WriteLine($"\nInvalid input. Please enter a valid name (a-z).");
                SaveScore(score);
            }
            else
            {
                scoringSystems.AddRecord(playerName, score);
            }
        }
    }
}