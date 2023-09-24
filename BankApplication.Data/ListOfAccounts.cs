using BankApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Data
{
    public class ListOfAccounts
    {
        public List<BankAccount> Accounts { get; } = new List<BankAccount>();
    }
}
