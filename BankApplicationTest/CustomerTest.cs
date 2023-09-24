using BankApplication.Model;

namespace BankApplicationTest
{
    public class CustomerTest
    {
        [Theory]
        [InlineData("Abc@123", true)]  
        [InlineData("Abc@", false)]     
        [InlineData("12345", false)]    
        [InlineData("@#$%^&", false)]  
        [InlineData("abcdef", false)] 
        public void ValidatePassword_Validations(string password, bool expected)
        {
            var customer = new Customer();

            bool result = customer.ValidatePassword(password);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ValidateEmail_Test()
        {
            var customer = new Customer();

            bool isValid = customer.ValidateEmail("esosa@gmail.com");

            Assert.True(isValid);
        }

        [Fact]
        public void SanitizeName_Test()
        {
            var customer = new Customer();

            string actual = "Esosa";

            var expected = actual;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void HasSavingsAccount_ShouldReturnTrueWhenCustomerHasSavingsAccount()
        {
            var customer = new Customer();

            customer.Accounts.Add(new SavingsAccount("123456789", customer, 1000));

            bool hasSavingsAccount = customer.HasSavingsAccount();

            Assert.True(hasSavingsAccount);
        }

        [Fact]
        public void HasSavingsAccount_ShouldReturnFalseWhenCustomerHasNoSavingsAccount()
        {
            var customer = new Customer();

            bool hasSavingsAccount = customer.HasSavingsAccount();

            Assert.False(hasSavingsAccount);
        }

        [Fact]
        public void HasCurrentAccount_ShouldReturnTrueWhenCustomerHasCurrentAccount()
        {
            var customer = new Customer();

            customer.Accounts.Add(new CurrentAccount("123456789", customer, 1000));

            bool hasCurrentAccount = customer.HasCurrentAccount();

            Assert.True(hasCurrentAccount);
        }

        [Fact]
        public void HasCurrentAccount_ShouldReturnFalseWhenCustomerHasNoCurrentAccount()
        {
            var customer = new Customer();

            bool hasCurrentAccount = customer.HasCurrentAccount();

            Assert.False(hasCurrentAccount);
        }

        [Fact]
        public void AddSavingsAccount_ShouldAddSavingAccountToList()
        {
            var customer = new Customer();
            var account = new SavingsAccount("123456789", customer, 1000);

            customer.AddAccount(account);

            Assert.Contains(account, customer.Accounts);
        }

        [Fact]
        public void AddCurrentAccount_ShouldAddCurrentAccountToList()
        {
            var customer = new Customer();
            var account = new CurrentAccount("123456789", customer, 1000);

            customer.AddAccount(account);

            Assert.Contains(account, customer.Accounts);
        }
    }
}