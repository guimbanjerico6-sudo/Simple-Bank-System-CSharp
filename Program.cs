// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
// --- MAIN PROGRAM ---
// BankAccount b = new BankAccount(); // ❌ ERROR! Cannot create abstract class.

NormalAccount bob = new NormalAccount("Bob", 100); // ✅ OK
VIPAccount elon = new VIPAccount("Elon", 1000000); // ✅ OK

bob.Transfer(elon, 50);


// --- 1. ABSTRACTION (The Blueprint) ---
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

    // "virtual" means children CAN change it.
    // If you wanted to force them to write their own, you'd use "abstract".
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
        if (this.Withdraw(amount))
        {
            receiver.Deposit(amount);
            Console.WriteLine("Transfer Completed!");
        }
    }
}

// --- 2. INHERITANCE (The Standard Child) ---
public class NormalAccount : BankAccount
{
    // Just passes data to parent. Uses default Withdraw logic.
    public NormalAccount(string name, double initialBalance) : base(name, initialBalance) { }
}

// --- 3. POLYMORPHISM (The Special Child) ---
public class VIPAccount : BankAccount
{
    public VIPAccount(string name, double initialBalance) : base(name, initialBalance) { }

    // Overrides the parent's rule
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