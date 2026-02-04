using System;
using System.Collections.Generic; // Required for Dictionary<Key, Value>

namespace BankApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // --- 1. THE DATABASE (DICTIONARY) ---
            // Syntax: Dictionary<KeyType, ValueType> name = new Dictionary<...>();
            // Logic: We switch from List to Dictionary for SPEED. 
            //        - Key (string) = The unique ID (Name). Acts like a locker label.
            //        - Value (BankAccount) = The actual object inside the locker.
            Dictionary<string, BankAccount> accountDict = new Dictionary<string, BankAccount>();

            // --- 2. THE MAIN LOOP ---
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

                    // --- LOGIC: DUPLICATE CHECK ---
                    // Why: Dictionaries CRASH if you add the same Key twice.
                    // Syntax: dict.ContainsKey(key) returns true/false.
                    if (accountDict.ContainsKey(name))
                    {
                        Console.WriteLine("Error: Account with that name already exists!");
                        continue; // Skip the rest of the loop and start over
                    }

                    Console.Write("Enter Initial Deposit: ");
                    string moneyInput = Console.ReadLine();

                    // --- SYNTAX: TRYPARSE ---
                    // Syntax: decimal.TryParse(string, out decimal result)
                    // Why: It tries to convert safely. If it fails (user types "abc"), it returns false instead of crashing.
                    if (decimal.TryParse(moneyInput, out decimal money))
                    {
                        Console.Write("Is this a VIP account? (yes/no): ");
                        string type = Console.ReadLine().ToLower();

                        if (type == "yes")
                        {
                            // --- SYNTAX: ADDING TO DICTIONARY ---
                            // Format: dict.Add(Key, Value);
                            // Logic: We create a 'VIPAccount' object and store it under the 'name' label.
                            accountDict.Add(name, new VIPAccount(name, money));
                        }
                        else
                        {
                            // Logic: We create a 'NormalAccount' object instead. Polymorphism allows both to fit in 'BankAccount'.
                            accountDict.Add(name, new NormalAccount(name, money));
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

                    // --- SYNTAX: LOOPING A DICTIONARY ---
                    // Logic: A Dictionary has Keys and Values. We only want to see the objects, so we loop 'accountDict.Values'.
                    foreach (var acc in accountDict.Values)
                    {
                        // Logic: acc.GetType().Name uses Reflection to print "VIPAccount" or "NormalAccount" automatically.
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
                    if (decimal.TryParse(Console.ReadLine(), out decimal amount))
                    {
                        // --- LOGIC: INSTANT LOOKUP ---
                        // Old Way (List): Loop through 1000 items to find "Bob". (Slow)
                        // New Way (Dictionary): Go straight to "Bob". (Instant)
                        if (accountDict.ContainsKey(fromName) && accountDict.ContainsKey(toName))
                        {
                            // Syntax: Type variable = dict[Key];
                            // Logic: Retrieve the objects from the dictionary so we can use their methods.
                            BankAccount sender = accountDict[fromName];
                            BankAccount receiver = accountDict[toName];

                            // Call the method inside the class (Encapsulation)
                            sender.Transfer(receiver, amount);
                        }
                        else
                        {
                            Console.WriteLine("Error: One or both accounts not found.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid amount.");
                    }
                }
            }
        }
    }
}

