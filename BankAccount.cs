using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp
{
    // ==========================================
    // 1. THE MODELS (The Blueprints)
    // ==========================================
    public abstract class BankAccount
    {
        public string Owner { get; set; }
        public decimal Balance { get; protected set; }

        public BankAccount(string name, decimal initialBalance)
        {
            this.Owner = name;
            this.Balance = initialBalance;
        }

        public void Deposit(decimal amount)
        {
            if (amount < 0) return;
            this.Balance += amount;
            Console.WriteLine($"Deposited ${amount} to {Owner}.");
        }

        public virtual bool Withdraw(decimal amount)
        {
            if (amount > Balance)
            {
                Console.WriteLine("Insufficient Funds");
                return false;
            }
            this.Balance -= amount;
            return true;
        }

        public void Transfer(BankAccount receiver, decimal amount)
        {
            // The Model handles the math of moving money
            if (this.Withdraw(amount))
            {
                receiver.Deposit(amount);
                Console.WriteLine("Transfer Completed!");
            }
            else
            {
                Console.WriteLine("Transfer Failed: Insufficient funds.");
            }
        }
    }

    public class NormalAccount : BankAccount
    {
        public NormalAccount(string name, decimal initialBalance) : base(name, initialBalance) { }
    }

    public class VIPAccount : BankAccount
    {
        public VIPAccount(string name, decimal initialBalance) : base(name, initialBalance) { }

        public override bool Withdraw(decimal amount)
        {
            // VIP Rule: Can go into debt up to -$1000
            if (this.Balance - amount < -1000)
            {
                Console.WriteLine("VIP Limit Reached!");
                return false;
            }
            this.Balance -= amount;
            return true;
        }
    }

    // ==========================================
    // 2. THE CONTROLLER (The Brain)
    // ==========================================
    public class BankManager
    {
        // The Database is PRIVATE. Main cannot touch this Dictionary directly.
        private Dictionary<string, BankAccount> accounts = new Dictionary<string, BankAccount>();

        public void CreateAccount(string name, decimal initialDeposit, bool isVip)
        {
            if (accounts.ContainsKey(name))
            {
                Console.WriteLine("Error: Account already exists!");
                return;
            }

            // The Manager logic decides which Model to build
            if (isVip)
            {
                accounts.Add(name, new VIPAccount(name, initialDeposit));
            }
            else
            {
                accounts.Add(name, new NormalAccount(name, initialDeposit));
            }
            Console.WriteLine("Success! Account created.");
        }

        public IEnumerable<BankAccount> GetAllAccounts()
        {
            return accounts.Values;
        }

        public void TransferMoney(string fromName, string toName, decimal amount)
        {
            // The Manager checks if people exist before trying to move money
            if (accounts.ContainsKey(fromName) && accounts.ContainsKey(toName))
            {
                BankAccount sender = accounts[fromName];
                BankAccount receiver = accounts[toName];
                sender.Transfer(receiver, amount);
            }
            else
            {
                Console.WriteLine("Error: Account not found.");
            }
        }
    }
}