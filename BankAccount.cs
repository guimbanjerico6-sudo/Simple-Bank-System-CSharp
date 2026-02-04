using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp
{
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
            Console.WriteLine("Deposited: " + amount);
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
 
            if (this.Withdraw(amount))
            {
                receiver.Deposit(amount);
                Console.WriteLine("Transfer Completed!");
            }
            else
            {
                Console.WriteLine("Transfer Failed due to insufficient funds.");
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
