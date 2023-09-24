using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BankApplication.Model
{
    public class Customer
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public string CustomerId { get; }

        public string EmailAddress { get; }

        public string Password { get; }

        public List<BankAccount> Accounts { get; } = new List<BankAccount>();

        public Customer(string customerId, string firstName, string lastName, string email, string password)
        {
            CustomerId = customerId;
            FirstName = SanitizeName(firstName);
            LastName = SanitizeName(lastName);
            EmailAddress = ValidateEmail(email) ? email : throw new ArgumentException("Invalid email format.");
            Password = password;
        }

        public Customer()
        {

        }

        public string SanitizeName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return "Invalid Name";

            char firstChar = name[0];

            if ((char.IsDigit(firstChar)) || char.IsLower(firstChar))
                return char.ToUpper(firstChar) + name.Substring(1);

            return name;
        }

        public bool ValidateEmail(string email)
        {
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, emailPattern);
        }

        public bool ValidatePassword(string password)
        {
            if (password.Length < 6)
            {
                return false;
            }

            string specialCharacters = @"@#$%^&!";
            return password.Any(char.IsDigit) && password.Any(char.IsLetter) && password.Any(c => specialCharacters.Contains(c));
        }

        public double GetTotalBalance()
        {
            return Accounts.Sum(account => account.Balance);
        }

        public string GetAccountStatements()
        {
            Console.Clear();
            string statement = "Account Statement:\n";
            statement += "---------------------------------------------------------------------------------------------------------\n";
            statement += $"| {"Full Name",-25} | {"Account Number",-20} | {"Account Type",-20} | {"Balance",-20} | {"Note"} |\n";
            statement += "---------------------------------------------------------------------------------------------------------\n";

            foreach (var account in Accounts)
            {
                statement += $"| {FullName,-25} | {account.AccountNumber,-20} | {account.GetAccountType(),-20} | #{account.Balance,-20:F2}| None |\n";

                statement += "---------------------------------------------------------------------------------------------------------\n";
            }
            return statement;
        }

        public void AddAccount(BankAccount account)
        {
            Accounts.Add(account);
        }

        public bool HasSavingsAccount()
        {
            return Accounts.Any(account => account is SavingsAccount);
        }

        public bool HasCurrentAccount()
        {
            return Accounts.Any(account => account is CurrentAccount);
        }
    }
}
