using ATMOperations.Constants;
using ATMOperations.Models;
using ATMOperations.Repository;
using System.Text;

namespace ATMOperations.Services
{
    public class AuthService
    {
        private UserJSONRepository _userJSONRepository = new();
        public User Login()
        {
            bool isAuthorised = false;
            while (!isAuthorised)
            {
                string identityNumber = GetIdentityNumber();
                string pin = GetPin();

                var user = _userJSONRepository.Login(identityNumber, pin);
                if (user != null)
                {
                    isAuthorised = true;
                    return user;
                }
            }
            return default;
        }
        public bool Register()
        {
            string firstName = GetFirstName();
            string lastName = GetLastName();
            string identityNumber = GetIdentityNumber();
            string pin = GenerateRandomPin();

            if (!_userJSONRepository.UserExists(identityNumber))
            {
                Console.WriteLine(Error.DuplicateUser);
                return false;
            }
            _userJSONRepository.Register(firstName, lastName, identityNumber, pin);
            return true;
        }
        private string GetFirstName()
        {
            var incorrect = true;
            string? firstName;
            do
            {
                Console.WriteLine("\nEnter your first name: ");
                firstName = Console.ReadLine();
                if (!firstName.All(char.IsLetter) || string.IsNullOrWhiteSpace(firstName))
                {
                    Console.WriteLine(Error.InvalidFirstName);
                }
                else
                {
                    incorrect = false;
                }
            } while (incorrect);
            return firstName;
        }
        private string GetLastName()
        {
            var incorrect = true;
            string? lastName;
            do
            {
                Console.WriteLine("\nEnter your last name: ");
                lastName = Console.ReadLine();
                if (!lastName.All(char.IsLetter) || string.IsNullOrWhiteSpace(lastName))
                {
                    Console.WriteLine(Error.InvalidLastName);
                }
                else
                {
                    incorrect = false;
                }
            } while (incorrect);
            return lastName;
        }
        private string GetIdentityNumber()
        {
            var incorrect = true;
            string? identityNumber;
            do
            {
                Console.WriteLine("\nEnter your Identity number: ");
                identityNumber = Console.ReadLine();
                if (!identityNumber.All(char.IsDigit))
                {
                    Console.WriteLine(Error.InvalidId);
                }
                else if (identityNumber.Length != 11)
                {
                    Console.WriteLine(Error.IdLength);
                }
                else
                {
                    incorrect = false;
                }
            } while (incorrect);
            return identityNumber;
        }
        private string GetPin()
        {
            var incorrect = true;
            string? pin;
            do
            {
                Console.WriteLine("\nEnter your pin code: ");
                pin = Console.ReadLine();
                if (!pin.All(char.IsDigit))
                {
                    Console.WriteLine(Error.InvalidPin);
                }
                else if (pin.Length != 4)
                {
                    Console.WriteLine(Error.PinLenght);
                }
                else
                {
                    incorrect = false;
                }
            } while (incorrect);
            return pin;
        }
        private string GenerateRandomPin()
        {
            Random random = new Random();
            const string digits = "0123456789";
            StringBuilder stringBuilder = new StringBuilder(4);
            for (int i = 0; i < 4; i++)
            {
                stringBuilder.Append(digits[random.Next(digits.Length)]);
            }
            return stringBuilder.ToString();
        }
    }
}