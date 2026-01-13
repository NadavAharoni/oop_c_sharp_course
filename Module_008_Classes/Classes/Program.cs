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

            Console.WriteLine();
            string expression = "3 + 5 * (2 - 8)";
            BaseExpression e = ExpressionParser.Parse(expression);
            double result = e.Evaluate();
            Console.WriteLine($"The result of the expression '{expression}' is: {result}");

            Console.WriteLine();
            Console.WriteLine("Testing a Singleton Class");
            Page p1 = Page.getThePage();
            Page p2 = Page.getThePage();
        }
    }
}
