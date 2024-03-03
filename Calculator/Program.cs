using System.Linq;
using System.Text.RegularExpressions;

namespace Calculator
{
    public class Program
    {
        static void Main()
        {
            CalculatorOperations operations = new CalculatorOperations();
            bool isRunning = true;
            Console.WriteLine("Console Calculator.");
            while (isRunning)
            {
                Console.WriteLine("\n1.Addition (+).");
                Console.WriteLine("2.Subtraction (-).");
                Console.WriteLine("3.Multiplication (*).");
                Console.WriteLine("4.Division (/).");
                Console.WriteLine("0.Cancel.");
                Console.WriteLine("------------");
                Console.Write("\nEnter: ");

                string? userInput = Console.ReadLine();

                if (Regex.IsMatch(userInput, @"^(1|2|3|4|0)$"))
                {
                    byte choice = byte.Parse(userInput);
                    switch (choice)
                    {
                        case 1:
                            var firstAddend = decimal.Parse(AskValue("\nEnter first addend: "));
                            var secondAddend = decimal.Parse(AskValue("\nEnter second addend: "));
                            Console.WriteLine("\nSum equals to: " + operations.Sum(firstAddend, secondAddend));
                            break;
                        case 2:
                            var minuend = decimal.Parse(AskValue("\nEnter minuend: "));
                            var subtrahend = decimal.Parse(AskValue("\nEnter subtrahend: "));
                            Console.WriteLine("\nDifference equals to: " + operations.Difference(minuend, subtrahend));
                            break;
                        case 3:
                            var firstFactor = decimal.Parse(AskValue("\nEnter first factor: "));
                            var secondFactor = decimal.Parse(AskValue("\nEnter second factor: "));
                            Console.WriteLine("\nProduct equals to: " + operations.Product(firstFactor, secondFactor));
                            break;
                        case 4:
                            Console.WriteLine();
                            var dividend = decimal.Parse(AskValue("\nEnter dividend: "));
                            var divisor = decimal.Parse(AskValue("\nEnter divisor: "));
                            var quotient = operations.Quotient(dividend, divisor);
                            if (quotient != 0) {
                                Console.WriteLine("\nQuotient equals to: " + quotient);
                            }
                            break;
                        case 0:
                            isRunning = false;
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("\nInvalid input. Please enter a valid option (1, 2, 3, 4 or 0).");
                }
            }
        }
        private static string AskValue(string message)
        {
            bool incorrect = true;
            string? value;
            do
            {
                Console.WriteLine(message);
                value = Console.ReadLine();
                if (!value.All(char.IsDigit) || string.IsNullOrWhiteSpace(value))
                {
                    Console.WriteLine("Invalid input. Please enter valid number.");
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
