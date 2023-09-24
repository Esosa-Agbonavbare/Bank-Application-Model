using BankApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Common
{
    public class Registration
    {
        public Customer Register(List<Customer> existingCustomers, List<BankAccount> existingAccounts)
        {
            Console.WriteLine("Customer Registration:");

            string firstName = GetValidName("Enter your first name: ");
            string lastName = GetValidName("Enter your last name: ");
            string email = GetValidEmail(existingCustomers);
            string password = GetValidPassword();

            if (existingCustomers.Any(customer => customer.EmailAddress == email))
            {
                Console.WriteLine("Customer with the same email already exists.");
                return null;
            }

            Customer newCustomer = new Customer(Guid.NewGuid().ToString(), firstName, lastName, email, password);

            while (true)
            {
                Console.WriteLine("\nSelect an account type to register:");
                Console.WriteLine("1. Savings Account");
                Console.WriteLine("2. Current Account");
                Console.WriteLine("3. Finish Registration");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            if (!HasAccountOfType(newCustomer, existingAccounts, typeof(SavingsAccount)))
                            {
                                SavingsAccount savingsAccount = CreateSavingsAccount(newCustomer);
                                newCustomer.AddAccount(savingsAccount);
                                existingAccounts.Add(savingsAccount);
                                Console.WriteLine("Savings Account registered successfully.");

                                Console.Write("Please, deposit at least 1000 into the savings account: ");
                                if (double.TryParse(Console.ReadLine(), out double initialDeposit) && initialDeposit >= 1000)
                                {
                                    savingsAccount.Deposit(initialDeposit);
                                    Console.WriteLine($"Initial deposit of ${initialDeposit} successful.");
                                }
                                else
                                {
                                    Console.WriteLine("Invalid initial deposit amount. Must be at least 1000. Registration canceled.");
                                    existingAccounts.Remove(savingsAccount);
                                    return null;
                                }
                            }
                            else
                            {
                                Console.WriteLine("You already have a Savings Account.");
                            }
                            break;
                        case 2:
                            if (!HasAccountOfType(newCustomer, existingAccounts, typeof(CurrentAccount)))
                            {
                                CurrentAccount currentAccount = CreateCurrentAccount(newCustomer);
                                newCustomer.AddAccount(currentAccount);
                                existingAccounts.Add(currentAccount);
                                Console.WriteLine("Current Account registered successfully.");
                            }
                            else
                            {
                                Console.WriteLine("You already have a Current Account.");
                            }
                            break;
                        case 3:
                            return newCustomer;
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

        private string GetValidName(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string name = Console.ReadLine().Trim();
                if (!string.IsNullOrEmpty(name) && char.IsLetter(name[0]) && name.All(char.IsLetter))
                {
                    return name;
                }
                Console.WriteLine("Invalid name. Please enter a valid name.");
            }
        }

        private string GetValidEmail(List<Customer> existingCustomers)
        {
            while (true)
            {
                Console.Write("Enter your email address: ");
                string email = Console.ReadLine().Trim();
                if (IsValidEmail(email))
                {
                    if (existingCustomers.Any(customer => customer.EmailAddress == email))
                    {
                        Console.WriteLine("Email address is already in use. Please use a different email.");
                    }
                    else
                    {
                        return email;
                    }
                }
                Console.WriteLine("Invalid email format. Please enter a valid email address.");
            }
        }

        private static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private string GetValidPassword()
        {
            while (true)
            {
                Console.Write("Enter a password (min. 6 characters, including at least one special character): ");
                string password = Console.ReadLine();
                if (IsValidPassword(password))
                {
                    return password;
                }
                Console.WriteLine("Invalid password format. Please enter a valid password.");
            }
        }

        private bool IsValidPassword(string password)
        {
            string specialCharacters = "@#$%^&!";
            return password.Length >= 6 && password.Any(c => specialCharacters.Contains(c));
        }

        private SavingsAccount CreateSavingsAccount(Customer customer)
        {
            Random random = new Random();
            int accountNumberInt = random.Next(100000000, 999999999);

            string accountNumber = accountNumberInt.ToString();

            double initialBalance = 0;

            SavingsAccount savingsAccount = new SavingsAccount(accountNumber, customer, initialBalance);

            return savingsAccount;
        }

        private CurrentAccount CreateCurrentAccount(Customer customer)
        {
            Random random = new Random();
            int accountNumberInt = random.Next(100000000, 999999999);

            string accountNumber = accountNumberInt.ToString();

            double initialBalance = 0;

            CurrentAccount currentAccount = new CurrentAccount(accountNumber, customer, initialBalance);

            return currentAccount;
        }

        private bool HasAccountOfType(Customer customer, List<BankAccount> accounts, Type accountType)
        {
            return accounts.Any(account => account.Owner == customer && accountType.IsInstanceOfType(account));
        }



    }
}
