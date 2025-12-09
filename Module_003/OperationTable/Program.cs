using OperationTable;

internal class Program
{
    class OperationTable
    {
        public delegate int Operation(int a, int b);

        Operation op;
        public int[,] results = new int[12, 10];

        public OperationTable(Operation _op)
        {
            op = _op;
        }

        public void Calculate()
        {
            for (int row = 0; row < results.GetLength(0); row++)
            {
                for (int col = 0; col < results.GetLength(1); col++)
                {
                    results[row, col] = op(row,col);
                }
            }
        }

        public override string ToString()
        {
            int maxWidth = 0;
            for (int row = 0; row < results.GetLength(0); row++)
            {
                for (int col = 0; col < results.GetLength(1); col++)
                {
                    string s = results[row, col].ToString();
                    maxWidth = Math.Max(maxWidth, s.Length);
                }
            }

            string res = "";
            for (int row = 0; row < results.GetLength(0); row++)
            {
                for (int col = 0; col < results.GetLength(1); col++)
                {
                    res += results[row, col].ToString().PadLeft(maxWidth, ' ');
                    if (col < results.GetLength(1) - 1)
                    {
                        res += ",";
                    }
                }
                res += "\n";
            }
            return res;
        }
    }

    public static int Multiply(int a, int b)
    {
        return a * b;
    }

    public static int Add(int a, int b)
    {
        return a + b;
    }

    class Counter
    {
        public int count = 0;

        public void Increment(int val)
        {
            count += val;
        }
    }

    delegate void F(int val);

    private static void Main(string[] args)
    {
        InternalFunction.Test();

        ArrayProcessor.TestDouble();
        return;

        Fraction f1 = new Fraction(1, 3);
        Fraction f2 = new Fraction(1, 4);

        Console.WriteLine($"{f1} + {f2} = {f1 + f2}");

        // DelegateExample.TestDelegateExample();

        //ComparerExample arr1 = new ComparerExample();
        //arr1.SortByLength();
        //Console.WriteLine($"Array sorted by length:\n{arr1.ToString()}");
        //Console.WriteLine("==================");
        //arr1.Sort();
        //Console.WriteLine($"Array sorted with default comparison:\n{arr1}");
        //Console.WriteLine("==================");

        //Counter c1 = new Counter();
        //Counter c2 = new Counter();
        //F f1 = c1.Increment;
        //F f2 = c2.Increment;

        //f1(3);
        //f2(5);

        //Console.WriteLine($"c1={c1.count}, c2={c2.count}");

        //F f3 = f1 + f2;
        //f3(10);
        //Console.WriteLine($"c1={c1.count}, c2={c2.count}");

        //f2 = f1;
        //f3(11);
        //Console.WriteLine($"c1={c1.count}, c2={c2.count}");

        return;

        int d = 2;

        OperationTable.Operation op = (int x, int y) =>
        {
            int result = 1;
            for (int i = 0; i<y; i++)
            {
                result *= x;
            }
            return result / d;
        };

        OperationTable table = new OperationTable(op);
        table.Calculate();
        Console.WriteLine(table);

        Console.WriteLine($"table.results[1,1]={table.results[1,1]}");
    }
}