using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Model
{
    public abstract class BankAccount
    {
        public string AccountNumber { get; }
        public Customer Owner { get; }
        public double Balance { get; set; }

        public BankAccount(string accountNumber, Customer owner, double initialBalance)
        {
            AccountNumber = accountNumber;
            Owner = owner;
            Balance = initialBalance;
        }

        public abstract string GetAccountType();

        public override string ToString()
        {
            return $"Account Type: {GetAccountType()}\nAccount Number: {AccountNumber}\nBalance: #{Balance:F2}";
        }
    }
}
