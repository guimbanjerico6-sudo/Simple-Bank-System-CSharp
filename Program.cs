using System;
using System.Collections.Generic; // Required for Dictionary<Key, Value>

namespace BankApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // BUY THE TV! (Create the Manager)
            BankManager myBankManager = new BankManager();

            while (true)
            {
                Console.WriteLine("\n--- BANKING SYSTEM (MVC PRO) ---");
                Console.WriteLine("1. Create Account");
                Console.WriteLine("2. View Accounts");
                Console.WriteLine("3. Transfer");
                Console.Write("Select: ");
                string option = Console.ReadLine();

                if (option == "1")
                {
                    // The View just gathers ingredients...
                    Console.Write("Name: ");
                    string name = Console.ReadLine();

                    Console.Write("Deposit: ");
                    decimal.TryParse(Console.ReadLine(), out decimal money);

                    Console.Write("Is VIP? (yes/no): ");
                    bool isVip = Console.ReadLine().ToLower() == "yes";

                    // ...and lets the Chef (Manager) cook the meal!
                    myBankManager.CreateAccount(name, money, isVip);
                }
                else if (option == "2")
                {
                    // View asks Manager for data, then prints it
                    foreach (var acc in myBankManager.GetAllAccounts())
                    {
                        Console.WriteLine($"Owner: {acc.Owner} | Balance: {acc.Balance}");
                    }
                }
                else if (option == "3")
                {
                    Console.Write("From: ");
                    string from = Console.ReadLine();
                    Console.Write("To: ");
                    string to = Console.ReadLine();
                    Console.Write("Amount: ");
                    decimal.TryParse(Console.ReadLine(), out decimal amt);

                    // One line of code to trigger the complex logic!
                    myBankManager.TransferMoney(from, to, amt);
                }
            }
        }
    }

}

