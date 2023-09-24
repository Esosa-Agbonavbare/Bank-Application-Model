using BankApplication.Core;
using BankApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplicationTest.TransactionServiceTest.CurrentAccountTransactionTest
{
    public class DepositTransactionTest
    {
        [Fact]
        public void Deposit_ValidAmount_ShouldIncreaseBalance()
        {
            Customer customer = new Customer();
            var transactionService = new TransactionService();
            var currentAccount = new CurrentAccount("123449009", customer, 0.0);
            double initialBalance = currentAccount.Balance;
            double depositAmount = 1000;

            transactionService.Deposit(currentAccount, depositAmount);

            Assert.Equal(initialBalance + depositAmount, currentAccount.Balance);
        }

        [Fact]
        public void Deposit_NegativeAmount_ShouldThrowException()
        {
            var transactionService = new TransactionService();
            var customer = new Customer();
            var currentAccount = new CurrentAccount("123456", customer, 1000.0);
            double depositAmount = -50;

            Assert.Throws<ArgumentException>(() => transactionService.Deposit(currentAccount, depositAmount));
        }

        [Fact]
        public void Deposit_ZeroAmount_ShouldThrowException()
        {
            var transactionService = new TransactionService();
            var customer = new Customer();
            var currentAccount = new CurrentAccount("123456", customer, 1000.0);
            double depositAmount = 0;

            Assert.Throws<ArgumentException>(() => transactionService.Deposit(currentAccount, depositAmount));
        }
    }
}
