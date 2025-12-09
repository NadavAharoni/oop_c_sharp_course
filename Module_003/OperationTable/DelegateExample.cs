using System;

public class DelegateExample
{
    public static double Multiply(double a, double b)
    {
        return a * b;
    }

    public static double Add(double a, double b)
    {
        return a + b;
    }

    public static double Square(double a)
    {
        return (a * a);
    }

    // public delegate double BinaryOperation(double a, double b);
    public delegate double UnaryOperation(double a);
    Func<double, double, double> fBinaryOp = Multiply;

    public static double CombineOperations(Func<double, double, double> bOp, UnaryOperation uOp, double a, double b)
    {
        return bOp(uOp(a), uOp(b));
    }

    public static double ApplyOperation(Func<double, double, double> bOp, double a, double b)
    {
        return bOp(a, b);
    }

    public static void TestApplyOperation()
    {
        double val1 = 5;
        double val2 = 10;

        BinaryOperation bOp1 = Add;
        double result1 = bOp1(val1, val2);

        bOp1 = Multiply;
        result1 = bOp1(val1, val2);

        bOp1 = (a, b) => a - b; // Subtraction using lambda
        result1 = bOp1(val1, val2);
        Console.WriteLine($"TestApplyOperation #1: result1={result1}");

        result1 = ApplyOperation( (a, b) => 2 * a - b, val1, val2);
        Console.WriteLine($"TestApplyOperation #2: result1={result1}");

        bOp1 = (a, b) =>
        {
            return a / b; 
        }; // Division using lambda with block

     }

    public static void TestDelegateExample()
    {
        double val1 = 3;
        double val2 = 4;

        double result = CombineOperations(Multiply, Square, val1, val2);

        Console.WriteLine($"TestDelegateExample: result={result}");
    }
}
