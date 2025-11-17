namespace ValueAndRef
{
    class PointClass
    {
        public long X;
        public long Y;
        public long Z;
        public long W;
    }

    struct PointStruct
    {
        public long X;
        public long Y;
        public long Z;
    }

    internal class Program
    {
        static void ChangePoint(PointClass p)
        {
            p.X = 100;
            p.Y = 200;
        }

        static void ChangePointStruct(PointStruct p)
        {
            p.X = 300;
            p.Y = 400;
        }

        static void ChangePointStructRef(ref PointStruct p)
        {
            p.X = 300;
            p.Y = 400;
        }

        static void StringTest()
        {
            string s1 = "Hello";
            string s2 = s1;

            StringArgumentTest(s1, s2);

            if (Object.ReferenceEquals(s1, s2))
            {
                Console.WriteLine("s1 and s2 reference the same object.");
            }
            else
            {
                Console.WriteLine("s1 and s2 reference different objects.");
            }

            s2 += ", World!";

            if (Object.ReferenceEquals(s1, s2))
            {
                Console.WriteLine("s1 and s2 reference the same object.");
            }
            else
            {
                Console.WriteLine("s1 and s2 reference different objects.");
            }

            Console.WriteLine($"s1: {s1}");
            Console.WriteLine($"s2: {s2}");
        }

        static void StringArgumentTest(string sa, string sb)
        {
            Console.WriteLine("Inside StringArgumentTest method.");
            if (Object.ReferenceEquals(sa, sb))
            {
                Console.WriteLine("sa and sb reference the same object.");
            }
            else
            {
                Console.WriteLine("sa and sb reference different objects.");
            }
        }

        static void PointTest()
        {
            PointClass p1;
            p1 = new PointClass();
            p1.X = 10;
            p1.Y = 20;
            Console.WriteLine($"Before ChangePoint: X={p1.X}, Y={p1.Y}");
            PointClass p2 = p1;
            p2.X = 30;
            p2.Y = 40;
            Console.WriteLine($"After ChangePoint: X={p1.X}, Y={p1.Y}");

            ChangePoint(p1);
            Console.WriteLine($"After ChangePoint method call: p1.X={p1.X}, p1.Y={p1.Y}");

            Console.WriteLine("---");
            PointStruct ps1; // = new PointStruct();
            ps1.X = 10;
            ps1.Y = 20;
            ps1.Z = 30;
            Console.WriteLine($"Before ChangePointStruct: X={ps1.X}, Y={ps1.Y}");
            PointStruct ps2 = ps1;
            ps2.X = 50;
            ps2.Y = 60;
            Console.WriteLine($"After ChangePointStruct: X={ps1.X}, Y={ps1.Y}");
            Console.WriteLine($"After ChangePointStruct: X={ps2.X}, Y={ps2.Y}");

            ChangePointStruct(ps1);
            Console.WriteLine($"After ChangePointStruct method call: ps1.X={ps1.X}, ps1.Y={ps1.Y}");

            ChangePointStructRef(ref ps1);
            Console.WriteLine($"After ChangePointStructRef method call: ps1.X={ps1.X}, ps1.Y={ps1.Y}");
        }

        static void ArrayTest(int[] a)
        {
            a[0] = 200;

            a = new int[] { 4, 5, 6 };
            a[0] = 500;
        }

        static void AllocationSizeTest(int size)
        {
            long before = GC.GetAllocatedBytesForCurrentThread();
            PointClass[] a = new PointClass[size];
            a[0] = new PointClass();
            a[0].X = 10;
            long after = GC.GetAllocatedBytesForCurrentThread();
            Console.WriteLine($"Allocated bytes for int[{size}]: {after - before}");
        }

        static void Main(string[] args)
        {
            // StringTest();
            // PointTest();
            // int[] arr = new int[] { 1, 2, 3 };
            // Console.WriteLine($"Before ArrayTest: arr[0]={arr[0]}");
            // ArrayTest(arr);
            // Console.WriteLine($"After ArrayTest: arr[0]={arr[0]}");

            AllocationSizeTest(10);
            AllocationSizeTest(1_000);
            AllocationSizeTest(1_000_000);
        }
    }
}