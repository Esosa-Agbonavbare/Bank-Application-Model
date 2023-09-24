using BankApplication.Core;
using BankApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplicationTest.TransactionServiceTest.SavingsAccountTransactionTest
{
    public class DepositTransactionTest
    {
        [Fact]
        public void Deposit_ValidAmount_ShouldIncreaseBalance()
        {
            Customer customer = new Customer();
            var transactionService = new TransactionService();
            var savingsAccount = new SavingsAccount("123449009", customer, 1000.0);
            double initialBalance = savingsAccount.Balance;
            double depositAmount = 100;

            transactionService.Deposit(savingsAccount, depositAmount);

            Assert.Equal(initialBalance + depositAmount, savingsAccount.Balance);
        }

        [Fact]
        public void Deposit_NegativeAmount_ShouldThrowException()
        {
            var transactionService = new TransactionService();
            var customer = new Customer();
            var savingsAccount = new SavingsAccount("123456", customer, 1000.0);
            double depositAmount = -50;

            Assert.Throws<ArgumentException>(() => transactionService.Deposit(savingsAccount, depositAmount));
        }

        [Fact]
        public void Deposit_ZeroAmount_ShouldThrowException()
        {
            var transactionService = new TransactionService();
            var customer = new Customer();
            var savingsAccount = new SavingsAccount("123456", customer, 1000.0);
            double depositAmount = 0;

            Assert.Throws<ArgumentException>(() => transactionService.Deposit(savingsAccount, depositAmount));
        }
    }
}
