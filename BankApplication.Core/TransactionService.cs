using BankApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Core
{
    public class TransactionService
    {
        public TransactionService()
        {

        }

        public void Deposit(BankAccount accountToDeposit, double amount)
        {
            if (amount > 0)
            {
                accountToDeposit.Balance += amount;
            }
            else
            {
                throw new ArgumentException("Invalid deposit amount. Amount must be greater than zero.");
            }
        }

        public static bool Withdraw(BankAccount accountToWithdrawFrom, double amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Invalid withdrawal amount. Amount must be greater than zero.");
                return false;
            }

            if (accountToWithdrawFrom is SavingsAccount savingsAccount)
            {
                if (savingsAccount.Balance >= amount + 1000)
                {
                    savingsAccount.Balance -= amount;
                    Console.WriteLine($"Withdrawal of #{amount} from {savingsAccount.GetAccountType()} successful.");
                    return true;
                }
                else
                {
                    Console.WriteLine("Insufficient balance. Cannot withdraw below minimum balance for Savings Account.");
                }
            }
            else if (accountToWithdrawFrom is CurrentAccount currentAccount)
            {
                if (currentAccount.Balance >= amount)
                {
                    currentAccount.Balance -= amount;
                    Console.WriteLine($"Withdrawal of #{amount} from {currentAccount.GetAccountType()} successful.");
                    return true;
                }
                else
                {
                    Console.WriteLine("Insufficient balance. Cannot withdraw beyond overdraft limit for Current Account.");
                }
            }
            else
            {
                Console.WriteLine("Invalid account type. Cannot process withdrawal.");
            }

            return false;
        }

        public bool Transfer(BankAccount sourceAccount, BankAccount destinationAccount, double amount)
        {
            if (sourceAccount == null)
            {
                Console.WriteLine("Source account not found.");
                return false;
            }

            if (destinationAccount == null || sourceAccount.Equals(destinationAccount))
            {
                Console.WriteLine("Destination account not found or cannot transfer to the same account.");
                return false;
            }

            if (amount <= 0)
            {
                Console.WriteLine("Invalid transfer amount. Amount must be greater than zero.");
                return false;
            }

            if (sourceAccount.Balance >= amount)
            {
                sourceAccount.Balance -= amount;
                destinationAccount.Balance += amount;
                Console.WriteLine($"Transfer of #{amount} successful.");
                return true;
            }
            else
            {
                Console.WriteLine("Insufficient balance for the transfer.");
                return false;
            }
        }
    }
}
