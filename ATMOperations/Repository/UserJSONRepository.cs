using ATMOperations.Logger;
using ATMOperations.Models;
using System.Text.Json;

namespace ATMOperations.Repository
{
    public class UserJSONRepository
    {
        private const string _fileLocation = "C:\\Users\\gujar\\source\\repos\\FinalProject\\ATMOperations\\Data\\Users.json";
        private List<User> _data = new();
        Log logger = new Log();
        public UserJSONRepository()
        {
            if (!File.Exists(_fileLocation))
            {
                File.WriteAllText(_fileLocation, "{}");
                Console.WriteLine("File created at location: " + _fileLocation);
            }
            _data = Parse(File.ReadAllText(_fileLocation));
        }
        private static List<User> Parse(string data)
        {
            try
            {
                return JsonSerializer.Deserialize<List<User>>(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError parsing Data File: {ex.Message}");
                return new List<User>();
            }
        }
        public void AddNewUser(User User)
        {
            _data.Add(User);
            Save(_data);
        }
        public void Save(List<User> userList)
        {
            try
            {
                File.WriteAllText(_fileLocation, JsonSerializer.Serialize(userList, new JsonSerializerOptions { WriteIndented = true }));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError saving Data file: {ex.Message}");
            }
        }
        public void UpdateUser(User user)
        {
            int index = _data.FindIndex(x => x.Id == user.Id);
            _data[index] = user;
            Save(_data);
        }
        public void Register(string firstName, string lastName, string identityNumber, string pin)
        {
            var newUser = new User();
            newUser.Id = Guid.NewGuid();
            newUser.FirstName = firstName.ToLower();
            newUser.LastName = lastName.ToLower();
            newUser.IdentityNumber = identityNumber;
            newUser.Pin = pin;
            newUser.Balance = 0;
            AddNewUser(newUser);
            Console.WriteLine("\nRegistered Successfully");
            Console.WriteLine($"\nYou pin code is {newUser.Pin}");
            logger.LogOperation($"{newUser.FirstName} {newUser.LastName} registered.");
        }
        public User Login(string identityNumber, string pin)
        {
            User? user = _data.Find(x => x.IdentityNumber == identityNumber && x.Pin == pin);
            if (user != null)
            {
                logger.LogOperation($"User {user.FirstName} {user.LastName} logged in.");
                Console.WriteLine($"\nWelcome back {user.FirstName}");
                return user;
            }
            else
            {
                Console.WriteLine("\nInvalid password or ID number");
                return default;
            }
        }
        public bool UserExists(string identityNumber)
        {
            var result = _data.Find(x => x.IdentityNumber == identityNumber);
            if (result != null)
            {
                return false;
            }
            return true;
        }
    }
}