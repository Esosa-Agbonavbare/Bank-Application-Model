using BankApplication.Core;
using BankApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplicationTest.TransactionServiceTest.SavingsAccountTransactionTest
{
    public class WithdrawTransactionTest
    {
        [Fact]
        public void Withdraw_ValidAmount_ShouldDecreaseBalance()
        {
            var customer = new Customer();
            var savingsAccount = new SavingsAccount("123456", customer, 2000.0);
            double initialBalance = savingsAccount.Balance;
            double withdrawalAmount = 100;

            bool result = TransactionService.Withdraw(savingsAccount, withdrawalAmount);

            Assert.True(result);
            Assert.Equal(initialBalance - withdrawalAmount, savingsAccount.Balance);
        }

        [Fact]
        public void Withdraw_NegativeAmount_ShouldNotChangeBalance()
        {
            var customer = new Customer();
            var savingsAccount = new SavingsAccount("789012", customer, 2000.0);
            double initialBalance = savingsAccount.Balance;
            double withdrawalAmount = -50;

            bool result = TransactionService.Withdraw(savingsAccount, withdrawalAmount);

            Assert.False(result);
            Assert.Equal(initialBalance, savingsAccount.Balance);
        }

        [Fact]
        public void Withdraw_InsufficientBalance_ShouldNotChangeBalance()
        {
            var customer = new Customer();
            var savingsAccount = new SavingsAccount("789012", customer, 2000.0);
            double initialBalance = savingsAccount.Balance;
            double withdrawalAmount = 3000;

            bool result = TransactionService.Withdraw(savingsAccount, withdrawalAmount);

            Assert.False(result);
            Assert.Equal(initialBalance, savingsAccount.Balance);
        }

        [Fact]
        public void Withdraw_ZeroAmount_ShouldNotChangeBalance()
        {
            var customer = new Customer();
            var savingsAccount = new SavingsAccount("123456", customer, 1000.0);
            double initialBalance = savingsAccount.Balance;
            double withdrawalAmount = 0;

            bool result = TransactionService.Withdraw(savingsAccount, withdrawalAmount);

            Assert.False(result);
            Assert.Equal(initialBalance, savingsAccount.Balance);
        }
    }
}
