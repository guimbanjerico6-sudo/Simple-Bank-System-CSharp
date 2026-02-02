using System;
using System.Collections.Generic;

namespace BankApp
{
    //practice updating code in github
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
                Console.WriteLine("3. Transfer");
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
                    Console.Write("Enter Sender Name: ");
                    string fromName = Console.ReadLine();

                    Console.Write("Enter Receiver Name: ");
                    string toName = Console.ReadLine();

                    Console.Write("Enter Amount to Transfer: ");
                    double amount = double.Parse(Console.ReadLine()); // (Use TryParse if you want to be safe!)

                    // 1. FIND THE OBJECTS IN THE LIST
                    // "x => x.Owner == name" is a Lambda Expression. It means "Find the match".
                    BankAccount sender = accounts.Find(x => x.Owner == fromName);
                    BankAccount receiver = accounts.Find(x => x.Owner == toName);

                    // 2. CHECK IF THEY EXIST
                    if (sender == null)
                    {
                        Console.WriteLine("Error: Sender not found.");
                    }
                    else if (receiver == null)
                    {
                        Console.WriteLine("Error: Receiver not found.");
                    }
                    else
                    {
                        // 3. DO THE TRANSFER (Using the logic you wrote earlier!)
                        sender.Transfer(receiver, amount);
                    }
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
        public void Transfer(BankAccount receiver, double amount)
        {
            // Try to withdraw from THIS account first
            if (this.Withdraw(amount))
            {
                // If successful, give money to the OTHER account
                receiver.Deposit(amount);
                Console.WriteLine("Transfer Completed!");
            }
            else
            {
                Console.WriteLine("Transfer Failed due to insufficient funds.");
            }
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