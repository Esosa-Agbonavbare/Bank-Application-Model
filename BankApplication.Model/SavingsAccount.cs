using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Model
{
    public class SavingsAccount : BankAccount
    {
        public SavingsAccount(string accountNumber, Customer owner, double initialBalance)
        : base(accountNumber, owner, initialBalance) { }

        public override string GetAccountType()
        {
            return "Savings Account";
        }

        public bool Deposit(double amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Invalid deposit amount. Amount must be greater than zero.");
                return false;
            }

            if (Balance + amount < 1000)
            {
                Console.WriteLine("Deposit failed. The deposit would cause the balance to go below the minimum balance requirement.");
                return false;
            }

            Balance += amount;
            Console.WriteLine($"Deposit of #{amount:F2} successful. New balance: #{Balance:F2}");
            return true;
        }
    }
}
