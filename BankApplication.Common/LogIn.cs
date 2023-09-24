using BankApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Common
{
    public class LogIn
    {
        public Customer Login(List<Customer> customers)
        {
            Console.Clear();
            Console.WriteLine("User Login: \n");
            Console.Write("Enter Email: ");
            string email = Console.ReadLine();

            Customer customer = customers.FirstOrDefault(u => u.EmailAddress == email);

            if (customer == null)
            {
                Console.WriteLine("User with the provided email address not found. Please check your email or register as a new user.");
                return null;
            }

            Console.Write("Enter your password: ");
            string password = Console.ReadLine();

            if (customer.ValidatePassword(password))
            {
                Console.WriteLine($"Welcome, {customer.FirstName}!");
                return customer;
            }

            Console.WriteLine("Invalid password. Please try again.");
            return null;

        }
    }
}
