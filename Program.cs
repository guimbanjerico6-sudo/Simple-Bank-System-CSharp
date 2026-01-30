using System;
using System.Collections.Generic;

namespace BankApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1. The Database
            List<BankAccount> accounts = new List<BankAccount>();

            // 2. The Infinite Loop
            while (true)
            {
                Console.WriteLine("\n--- BANKING SYSTEM ---");
                Console.WriteLine("1. Create Account");
                Console.WriteLine("2. View All Accounts");
                Console.WriteLine("3. Exit");
                Console.Write("Select an option: ");

                string option = Console.ReadLine();

                if (option == "1")
                {
                    Console.Write("Enter Name: ");
                    string name = Console.ReadLine();

                    Console.Write("Enter Initial Deposit: ");
                    // Simple check to prevent crashing on empty input
                    string moneyInput = Console.ReadLine();
                    if (double.TryParse(moneyInput, out double money))
                    {
                        Console.Write("Is this a VIP account? (yes/no): ");
                        string type = Console.ReadLine().ToLower();

                        if (type == "yes")
                        {
                            accounts.Add(new VIPAccount(name, money));
                        }
                        else
                        {
                            accounts.Add(new NormalAccount(name, money));
                        }
                        Console.WriteLine("Success! Account Created.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid amount entered.");
                    }
                }
                else if (option == "2")
                {
                    Console.WriteLine("\n--- CUSTOMER LIST ---");
                    foreach (BankAccount acc in accounts)
                    {
                        Console.WriteLine($"Owner: {acc.Owner} | Balance: ${acc.Balance} | Type: {acc.GetType().Name}");
                    }
                }
                else if (option == "3")
                {
                    Console.WriteLine("Goodbye!");
                    break; // BREAKS the loop and ends the program
                }
                else
                {
                    Console.WriteLine("Invalid Option. Try again.");
                }
            }
        }
    }

    // --- CLASSES START HERE ---

    // 1. ABSTRACTION
    public abstract class BankAccount
    {
        public string Owner;
        public double Balance { get; protected set; }

        public BankAccount(string name, double initialBalance)
        {
            this.Owner = name;
            this.Balance = initialBalance;
        }

        public void Deposit(double amount)
        {
            if (amount < 0) return;
            this.Balance += amount;
            Console.WriteLine("Deposited: " + amount);
        }

        public virtual bool Withdraw(double amount)
        {
            if (amount > Balance)
            {
                Console.WriteLine("Insufficient Funds");
                return false;
            }
            this.Balance -= amount;
            return true;
        }
    }

    // 2. INHERITANCE
    public class NormalAccount : BankAccount
    {
        public NormalAccount(string name, double initialBalance) : base(name, initialBalance) { }
    }

    // 3. POLYMORPHISM
    public class VIPAccount : BankAccount
    {
        public VIPAccount(string name, double initialBalance) : base(name, initialBalance) { }

        public override bool Withdraw(double amount)
        {
            if (this.Balance - amount < -1000)
            {
                Console.WriteLine("VIP Limit Reached!");
                return false;
            }
            this.Balance -= amount;
            return true;
        }
    }
}