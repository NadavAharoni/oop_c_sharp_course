using Memory;
using System.Text;

internal class Program
{
    struct PointStruct
    {
        public int x;
        public int y;
        public int x1;
        public int y1;
        public int x2;
        public int y2;
    }

    class PointClass
    {
        public int x;
        public int y;
        public int x1;
        public int y1;
        public int x2;
        public int y2;
    }


    static void ModifyPoint(PointStruct p)
    {
        p.x += 10;
        p.y += 20;
        Console.WriteLine($"In ModifyPoint: p.x={p.x} p.y={p.y}");
    }

    static int counter = 0;
    static void Recursion()
    {
        int[] arr = new int[10];
        PointStruct p1, p2, p3, p4;
        PointStruct p5, p6, p7, p8;
        counter++;
        Console.WriteLine($"counter={counter}");
        Recursion();
    }

    static void MeasureStructMemory()
    {
        long before = GC.GetAllocatedBytesForCurrentThread();
        PointStruct pointStruct; // = new PointStruct(); // new has no effect!
        pointStruct.x = 17;
        long after = GC.GetAllocatedBytesForCurrentThread();
        Console.WriteLine($"MeasureStructMemory: Allocated Memory={after - before}");
    }

    static void MeasureClassMemory()
    {
        long before = GC.GetAllocatedBytesForCurrentThread();
        PointClass pointClass = new PointClass();
        pointClass.x = 17;
        long after = GC.GetAllocatedBytesForCurrentThread();
        Console.WriteLine($"MeasureStructMemory: Allocated Memory={after - before}");
    }

    static void MeasureStringMemory()
    {
        long before = GC.GetAllocatedBytesForCurrentThread();

        string s = "hello world";
        s = s + "!";
        s = s + " " + s;
        Console.WriteLine($"s={s}");

        long after1 = GC.GetAllocatedBytesForCurrentThread();
        Console.WriteLine($"MeasureStringMemory: Allocated Memory={after1 - before}");

        StringBuilder sb = new StringBuilder("hello world");
        sb.Append(s);
        Console.WriteLine($"sb={sb.ToString()}");

        long after2 = GC.GetAllocatedBytesForCurrentThread();
        Console.WriteLine($"MeasureStringMemory: Allocated Memory={after2 - after1}");
    }

    static void TryToModifyString(string s)
    {
        s = "hello";
    }    

    static void MeasureMemory()
    {
        long before = GC.GetAllocatedBytesForCurrentThread();
        PointClass[] array = new PointClass[100]; 
        long after = GC.GetAllocatedBytesForCurrentThread();
        Console.WriteLine($"Memory={after}-{before}={after - before}");
    }

    public static void ModifyArray(int[] arr)
    {
        arr[0] = 999; // Modifies original array
        arr = [10, 20, 30]; // Only modifies local reference
    }

    private static void Main(string[] args)
    {
        string s1 = "hello";
        string s2 = s1;
        s1 += " world";
        Console.WriteLine($"s1={s1}, s2={s2}");

        // Recursion();
        // ClassWithDtor.TestGC();

        // -- Demostrate string immutability
        // string s1 = "hi";
        // TryToModifyString(s1);
        // Console.WriteLine($"s1={s1}");

        // MeasureStringMemory();

        // MeasureStructMemory();

        // MeasureClassMemory();

        // int[] arr1 = { 1, 2, 3, 4 };
        // Console.WriteLine(string.Join(",", arr1));
        // ModifyArray(arr1);
        // Console.WriteLine(string.Join(",", arr1));

        // Recursion();

        // MeasureMemory();

        //PointStruct p = new PointStruct();
        //p.x = 4;
        //p.y = 5;
        //ModifyPoint(p);
        //Console.WriteLine($"p.x={p.x} p.y={p.y}");
    }
}

