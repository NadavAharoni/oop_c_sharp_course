namespace GarbageCollection2025
{
    class ClassWithDtor
    {
        private static int _objectCount = 0; // Tracks total objects created
        public int _id;                      // Unique ID for each object
        private ClassWithDtor? _next = null;

        public ClassWithDtor()
        {
            _id = ++_objectCount;            // Increment static counter and assign ID
            Console.WriteLine($"Object {_id} created");
        }

        public void SetNext(ClassWithDtor? next)
        {
            _next = next;
        }

        ~ClassWithDtor()
        {
            Console.WriteLine($"Finalize called for object {_id}");
            _objectCount--;
        }
    }

    class Program
    {
        static ClassWithDtor f()
        {
            const int numObjects = 10;
            ClassWithDtor head = new ClassWithDtor();
            ClassWithDtor? next = head;
            for (int i = 0; i < numObjects; i++)
            {
                ClassWithDtor obj = new ClassWithDtor();
                obj.SetNext(next);
                next = obj;
            }
            head.SetNext(next);
            Console.WriteLine("Finished allocating objects.");
            return head;
        }
        static void Main(string[] args)
        {
            ClassWithDtor list = f();
            // Force garbage collection
            Console.WriteLine("Forcing garbage collection...");
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Console.WriteLine("Garbage collection complete.");
        }
    }
}