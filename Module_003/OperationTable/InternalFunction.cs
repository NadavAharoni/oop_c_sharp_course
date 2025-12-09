using System.Numerics;

class InternalFunction
{
    public delegate double UnaryOperator(double d);
    public static UnaryOperator RetOp(double c)
    {
        UnaryOperator f = (double x) => { return x + c; };
        return f;
    }

    public static UnaryOperator RetOp2(double c)
    {
        double LocalFunction(double x)
        {
            return x + c;
        }
        return LocalFunction;
    }


    public static void Test()
    {
        UnaryOperator op = RetOp(3.0);
        double result = op(4.0);
        Console.WriteLine($"Result: {result}");

        UnaryOperator op2 = RetOp(10.0);
        double result2 = op2(5.0);
        Console.WriteLine($"Result: {result2}");
    }
}
