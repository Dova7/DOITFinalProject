using ATMOperations.Logger;
using ATMOperations.Models;
using ATMOperations.Repository;
using System;

namespace ATMOperations
{
    public class Operations
    {
        UserJSONRepository userJSONRepository = new UserJSONRepository();
        Log logger = new Log();

        public decimal CheckBalance(User user)
        {
            try
            {
                logger.LogOperation($"{user.FirstName} {user.LastName} checked funds.");
                return user.Balance;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking balance: {ex.Message}");
                return 0;
            }
        }

        public void DepositAmount(User user, decimal amount)
        {
            try
            {
                if (amount > 0)
                {
                    logger.LogOperation($"{user.FirstName} {user.LastName} deposited {amount} (GEL) to funds.");
                    user.Balance += amount;
                    userJSONRepository.UpdateUser(user);
                }
                else
                {
                    Console.WriteLine("\nCannot deposit negative amount.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error depositing amount: {ex.Message}");
            }
        }
        public void WithdrawAmount(User user, decimal amount)
        {
            try
            {
                if (amount > user.Balance)
                {
                    Console.WriteLine("\nNot enough funds.");
                }
                else if (amount < 0)
                {
                    Console.WriteLine("\nCannot withdraw negative amount.");
                }
                else
                {
                    user.Balance -= amount;
                    userJSONRepository.UpdateUser(user);
                    logger.LogOperation($"{user.FirstName} {user.LastName} withdrew {amount} (GEL) from funds.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error withdrawing amount: {ex.Message}");
            }
        }
    }
}
