using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Model
{
    public class CurrentAccount : BankAccount
    {
        public CurrentAccount(string accountNumber, Customer owner, double initialBalance)
        : base(accountNumber, owner, initialBalance) { }

        public override string GetAccountType()
        {
            return "Current Account";
        }
    }
}
