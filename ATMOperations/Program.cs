using ATMOperations.Models;
using ATMOperations.Services;
using System.Text.RegularExpressions;

namespace ATMOperations
{
    public class Program
    {
        static void Main()
        {
            AuthService authService = new AuthService();
            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine("\nWelcome to ATM Service.");
                Console.WriteLine("1. Register.");
                Console.WriteLine("2. Login.");
                Console.WriteLine("0. Exit.");
                Console.WriteLine("------");

                string? userInput = Console.ReadLine();

                if (Regex.IsMatch(userInput, @"^(0|1|2)$"))
                {
                    byte choice = byte.Parse(userInput);

                    switch (choice)
                    {
                        case 1:
                            authService.Register();
                            break;
                        case 2:
                            User user = authService.Login();
                            if (user != null)
                                UserLoggedIn(user);
                            break;
                        case 0:
                            isRunning = false;
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("\nInvalid input. Please enter a valid option (0, 1, or 2).");
                }
            }
        }
        public static void UserLoggedIn(User user)
        {
            Operations operations = new Operations();

            while (true)
            {
                Console.WriteLine("\n1. Check balance.");
                Console.WriteLine("2. Deposit funds.");
                Console.WriteLine("3. Withdraw funds.");
                Console.WriteLine("0. Cancel.");
                Console.WriteLine("--------");

                string userInput = Console.ReadLine();

                if (Regex.IsMatch(userInput, @"^(0|1|2|3)$"))
                {
                    byte choice = byte.Parse(userInput);
                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine("\nAvailable funds " + operations.CheckBalance(user) + " (GEL):");
                            break;
                        case 2:
                            decimal amountToDeposit = decimal.Parse(AskAmount("\nEnter amount to deposit (GEL): "));
                            operations.DepositAmount(user, amountToDeposit);
                            Console.WriteLine("\nFunds " + operations.CheckBalance(user) + " (GEL):");
                            break;
                        case 3:
                            decimal amountToWithdraw = decimal.Parse(AskAmount("\nEnter amount to withdraw (GEL): "));
                            operations.WithdrawAmount(user, amountToWithdraw);
                            Console.WriteLine("\nFunds " + operations.CheckBalance(user) + " (GEL):");
                            break;
                        case 0:
                            return;
                    }
                }
                else
                {
                    Console.WriteLine("\nInvalid input. Please enter a valid option (0, 1, 2, or 3).");
                }
            }
        }
        private static string AskAmount(string message)
        {
            bool incorrect = true;
            string? value;
            do
            {
                Console.WriteLine(message);
                value = Console.ReadLine();
                if (!value.All(char.IsDigit) || string.IsNullOrWhiteSpace(value))
                {
                    Console.WriteLine("Invalid input. Please enter valid amount.");
                }
                else
                {
                    incorrect = false;
                }
            } while (incorrect);
            return value;
        }
    }
}