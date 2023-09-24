using BankApplication.Common;
using BankApplication.Core;
using BankApplication.Model;

namespace BankApplication
{
    public class Program
    {
        static List<Customer> customers = new List<Customer>();
        static List<BankAccount> accounts = new List<BankAccount>();
        static TransactionService transaction = new TransactionService();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Welcome to Regal Online Banking");
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Register");
                Console.WriteLine("3. Exit");
                Console.Write("Please, select an option: ");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            LogIn logIn = new LogIn();
                            Customer loggedInCustomer = logIn.Login(customers);
                            if (loggedInCustomer != null)
                            {
                                Console.Clear();
                                Console.WriteLine($"Login successful. Welcome, {loggedInCustomer.FullName}!");
                                Menu.BankingTransactions(loggedInCustomer, customers, accounts, transaction);
                            }
                            else
                            {
                                Console.WriteLine("Login failed.\n");
                            }
                            break;
                        case 2:
                            Registration registration = new Registration();
                            Console.Clear();
                            Customer newCustomer = registration.Register(customers, accounts);
                            if (newCustomer != null)
                            {
                                customers.Add(newCustomer);
                                Console.Clear();
                                Console.WriteLine("Registration successful. You can now login.");
                            }
                            else
                            {
                                Console.WriteLine("Registration failed.");
                            }
                            break;
                        case 3:
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid option.");
                }
            }
        }
    }
}