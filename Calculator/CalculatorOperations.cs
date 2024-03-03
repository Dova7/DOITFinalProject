using System.Linq.Expressions;

namespace Calculator
{
    public class CalculatorOperations
    {
        public decimal Sum(decimal firstNumber, decimal secondNumber)
        {            
            return firstNumber + secondNumber;
        }
        public decimal Difference(decimal firstNumber, decimal secondNumber)
        {
            return (firstNumber - secondNumber);
        }
        public decimal Product(decimal firstNumber, decimal secondNumber)
        {
            return (firstNumber * secondNumber);
        }
        public decimal Quotient(decimal firstNumber, decimal secondNumber)
        {
            try
            {
                return (firstNumber / secondNumber);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return default;
            }
        }
    }
}