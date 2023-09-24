using BankApplication.Core;
using BankApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication
{
    public class Menu
    {
        public static void BankingTransactions(Customer customer, List<Customer> allCustomers, List<BankAccount> allAccounts, TransactionService transactionService)
        {
            while (true)
            {
                Console.WriteLine("Banking Operations:");
                Console.WriteLine("1. Deposit");
                Console.WriteLine("2. Withdraw");
                Console.WriteLine("3. Transfer");
                Console.WriteLine("4. Account Statements");
                Console.WriteLine("5. Create Another Account");
                Console.WriteLine("6. Logout");
                Console.Write("Select an option: ");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            DepositFunds(customer);
                            break;
                        case 2:
                            WithdrawFunds(customer, transactionService);
                            break;
                        case 3:
                            TransferFunds(customer, allCustomers, allAccounts, transactionService);
                            break;
                        case 4:
                            Console.WriteLine(customer.GetAccountStatements());
                            break;
                        case 5:
                            RegisterAnotherAccount(customer, allAccounts);
                            break;
                        case 6:
                            Console.WriteLine("Logout successful.\n");
                            return;
                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid option.");
                }
            }
        }

        public static void DepositFunds(Customer customer)
        {
            Console.Clear();
            Console.WriteLine("Processing Deposit...");
            Console.Write("Enter the account number to deposit into: ");
            string accountNumber = Console.ReadLine();

            BankAccount accountToDeposit = customer.Accounts.FirstOrDefault(account => account.AccountNumber == accountNumber); ;

            if (accountToDeposit != null)
            {
                Console.Write("Enter the amount to deposit: ");
                if (double.TryParse(Console.ReadLine(), out double amount))
                {
                    TransactionService transaction = new TransactionService();
                    try
                    {
                        transaction.Deposit(accountToDeposit, amount);
                        Console.WriteLine($"Deposit of #{amount} into {accountToDeposit.GetAccountType()} successful.");
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid amount.");
                }
            }
            else
            {
                Console.WriteLine("Account not found. Please check the account number.\n");
            }
        }

        public static bool WithdrawFunds(Customer customer, TransactionService transactionService)
        {
            Console.Clear();
            Console.WriteLine("Banking System - Withdraw Funds");

            Console.Write("Enter the account number to withdraw from: ");
            string accountNumber = Console.ReadLine();

            BankAccount accountToWithdrawFrom = customer.Accounts.FirstOrDefault(account => account.AccountNumber == accountNumber);

            if (accountToWithdrawFrom != null)
            {
                Console.Write("Enter the amount to withdraw: ");
                if (double.TryParse(Console.ReadLine(), out double amount))
                {
                    bool success = TransactionService.Withdraw(accountToWithdrawFrom, amount);
                    if (success)
                    {
                        Console.WriteLine("Transaction completed successfully.");
                        return true;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid amount.");
                }
            }
            else
            {
                Console.WriteLine("Account not found. Please check the account number.\n");
            }

            return false;
        }

        public static bool TransferFunds(Customer customer, List<Customer> allCustomers, List<BankAccount> allAccounts, TransactionService transactionService)
        {
            Console.Clear();
            Console.WriteLine("Processing Transfer...");

            Console.Write("Enter the source account number: ");
            string sourceAccountNumber = Console.ReadLine();
            BankAccount sourceAccount = customer.Accounts.FirstOrDefault(account => account.AccountNumber == sourceAccountNumber);

            Console.Write("Enter the destination account number: ");
            string destinationAccountNumber = Console.ReadLine();
            BankAccount destinationAccount = customer.Accounts.FirstOrDefault(account => account.AccountNumber == destinationAccountNumber);

            Console.Write("Enter the amount to transfer: ");
            if (double.TryParse(Console.ReadLine(), out double amount))
            {
                return transactionService.Transfer(sourceAccount, destinationAccount, amount);
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid amount.");
                return false;
            }
        }

        private static void RegisterAnotherAccount(Customer customer, List<BankAccount> allAccounts)
        {
            Console.Clear();
            Console.WriteLine("Create Another Account: ");
            Console.WriteLine("1. Savings Account");
            Console.WriteLine("2. Current Account");
            Console.Write("Select an account type: ");

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                switch (choice)
                {
                    case 1:
                        if (!customer.HasSavingsAccount())
                        {
                            SavingsAccount savingsAccount = CreateSavingsAccount(customer);
                            customer.AddAccount(savingsAccount);
                            allAccounts.Add(savingsAccount);
                            Console.WriteLine("Savings Account registered successfully.");

                            Console.Write("Please, deposit at least 1000 into the savings account: ");
                            if (double.TryParse(Console.ReadLine(), out double initialDeposit) && initialDeposit >= 1000)
                            {
                                savingsAccount.Deposit(initialDeposit);
                                Console.WriteLine($"Initial deposit of #{initialDeposit} successful.\n");
                            }
                            else
                            {
                                Console.WriteLine("Invalid initial deposit amount. Must be at least 1000. Registration canceled.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("You already have a Savings Account.\n");
                        }
                        break;
                    case 2:
                        if (!customer.HasCurrentAccount())
                        {
                            CurrentAccount currentAccount = CreateCurrentAccount(customer);
                            customer.AddAccount(currentAccount);
                            allAccounts.Add(currentAccount);
                            Console.WriteLine("Current Account registered successfully.");
                        }
                        else
                        {
                            Console.WriteLine("You already have a Current Account.\n");
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid account type. Please try again.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid option.");
            }

            static SavingsAccount CreateSavingsAccount(Customer customer)
            {
                Random random = new Random();
                int accountNumberInt = random.Next(100000000, 999999999);

                string accountNumber = accountNumberInt.ToString();

                double initialBalance = 0;

                SavingsAccount savingsAccount = new SavingsAccount(accountNumber, customer, initialBalance);

                return savingsAccount;
            }

            static CurrentAccount CreateCurrentAccount(Customer customer)
            {
                Random random = new Random();
                int accountNumberInt = random.Next(100000000, 999999999);

                string accountNumber = accountNumberInt.ToString();

                double initialBalance = 0;

                CurrentAccount currentAccount = new CurrentAccount(accountNumber, customer, initialBalance);

                return currentAccount;
            }
        }
    }
}
