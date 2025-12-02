public class ArrayProcessor
{
    public delegate void UnaryAction(double a);

    public static void PrintDouble(double value)
    {
        Console.WriteLine($">>> {value} <<<");
    }

    public static void ProcessArray(int[] array, Action<int> processor)
    {
        foreach (int item in array)
        {
            processor(item);
        }
    }

    public static void ProcessArray(double[] array, UnaryAction processor)
    {
        foreach (double item in array)
        {
            processor(item);
        }
    }

    public static void TestDouble()
    {   
        double[] numbers = { 1.5, 2.5, 3.5 };
        ProcessArray(numbers, PrintDouble);

        var sumCalculator = new SumCalculatorDouble();
        ProcessArray(numbers, sumCalculator.AddToSum);
        double totalSum = sumCalculator.GetSum();
        Console.WriteLine($"Total sum (double): {totalSum}");

        double[] numbers2 = { 10.0, 20.0, 30.0 };
        ProcessArray(numbers2, sumCalculator.AddToSum);
        Console.WriteLine($"Total sum after adding more (double):" +
            $"{sumCalculator.GetSum()}");
    }

    public static void Test()
    {
        int[] numbers = { 1, 2, 3, 4, 5 };
        var sumCalculator = new SumCalculator();

        ArrayProcessor.ProcessArray(numbers, sumCalculator.AddToSum);

        int totalSum = sumCalculator.GetSum(); // Returns 15
        Console.WriteLine($"Total sum: {totalSum}");
    }
}

public class SumCalculator
{
    private int _sum = 0;

    public void AddToSum(int number)
    {
        _sum += number;
    }

    public int GetSum()
    {
        return _sum;
    }
}


public class SumCalculatorDouble
{
    private double _sum = 0.0;

    public void AddToSum(double number)
    {
        _sum += number;
    }

    public double GetSum()
    {
        return _sum;
    }
}