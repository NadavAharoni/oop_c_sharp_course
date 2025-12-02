public class ArrayProcessor
{
    public delegate void UnaryAction(double a);
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
    private int _sum;

    public void AddToSum(int number)
    {
        _sum += number;
    }

    public int GetSum()
    {
        return _sum;
    }
}
