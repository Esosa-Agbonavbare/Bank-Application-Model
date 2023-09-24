using BankApplication.Core;
using BankApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplicationTest.TransactionServiceTest.CurrentAccountTransactionTest
{
    public class TransferTransactionTest
    {
        [Fact]
        public void TransferFunds_ValidTransfer_ShouldDecreaseSourceBalanceAndIncreaseDestinationBalance()
        {
            var sourceCustomer = new Customer();
            var destinationCustomer = new Customer();
            var sourceAccount = new CurrentAccount("2344456", sourceCustomer, 10000.0);
            var destinationAccount = new CurrentAccount("7878778", destinationCustomer, 1000.0);
            double sourceInitialBalance = sourceAccount.Balance;
            double destinationInitialBalance = destinationAccount.Balance;
            double transferAmount = 1000;

            var transactionService = new TransactionService();

            bool result = transactionService.Transfer(sourceAccount, destinationAccount, transferAmount);

            Assert.True(result);
            Assert.Equal(sourceInitialBalance - transferAmount, sourceAccount.Balance);
            Assert.Equal(destinationInitialBalance + transferAmount, destinationAccount.Balance);
        }

        [Fact]
        public void TransferFunds_InvalidSourceAccount_ShouldReturnFalseAndNotChangeBalance()
        {
            var destinationCustomer = new Customer();
            var destinationAccount = new CurrentAccount("789012", destinationCustomer, 2000.0);
            double destinationInitialBalance = destinationAccount.Balance;
            double transferAmount = 100;

            var transactionService = new TransactionService();

            bool result = transactionService.Transfer(null, destinationAccount, transferAmount);

            Assert.False(result);
            Assert.Equal(destinationInitialBalance, destinationAccount.Balance);
        }
    }
}
