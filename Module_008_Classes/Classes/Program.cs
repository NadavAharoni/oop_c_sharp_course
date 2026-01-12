using System;

namespace Classes
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BankAccount account = new BankAccount(1000);
            account.Deposit(500);
            account.Withdraw(200);
            Console.WriteLine($"Current Balance: {account.GetBalance()}");
        }
    }
}
