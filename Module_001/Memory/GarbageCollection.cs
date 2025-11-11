using System;
using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;

namespace Memory
{
    class CyclicReference
    {
        string _name;
        public CyclicReference? cyclicRef = null;

        public CyclicReference(string name)
        {
            _name = name;
        }

        ~CyclicReference()
        {
            Console.WriteLine($"CyclicReference {_name} collected");
        }
    }
    class ClassWithDtor
    {
        private static int _objectCount = 0; // Tracks total objects created
        public int _id;                     // Unique ID for each object
        // private double[] arr;

        public ClassWithDtor()
        {
            _id = ++_objectCount;            // Increment static counter and assign ID
            // arr = new double[5000];
            Console.WriteLine($"Object {_id} created");
        }

        ~ClassWithDtor()
        {
            Console.WriteLine($"Finalize called for object {_id}");
            _objectCount--;
            // GC.ReRegisterForFinalize(this);
        }

        public static ClassWithDtor AllocateAndLoseRef()
        {
            // Create an array of ClassWithDtor objects
            ClassWithDtor[] objects = new ClassWithDtor[10];
            Console.WriteLine($"After allocating the array objectCount={_objectCount}");

            for (int i = 0; i < objects.Length; i++)
            {
                objects[i] = new ClassWithDtor();
                ClassWithDtor tmp = new ClassWithDtor();
            }

            ClassWithDtor theOneToKeep = objects[5];
            Console.WriteLine($"theOneToKeep: {theOneToKeep._id}");

            // Lose reference to the array
            objects = null;

            CyclicReference c1 = new CyclicReference("c1");
            CyclicReference c2 = new CyclicReference("c2");
            c1.cyclicRef = c2;
            c2.cyclicRef = c1;

            return theOneToKeep;
        }

        public static void F(out ClassWithDtor c)
        {
            c = new ClassWithDtor();
            c._id = 1000;
        }


        public static void G(ClassWithDtor? c)
        {
            c._id = 2000;
        }

        public static void TestGC()
        {
            GCGenerationsExperiment();

            // int[]? arr = { 1, 2, 3 };
            // ref int j = ref arr[2];
            // j++;
            // Console.WriteLine($"arr={String.Join(",", arr)}");

            //arr = null;
            //j++;

            // Allocate objects and lose reference, except for one object
            // ClassWithDtor c = AllocateAndLoseRef();
            // Force garbage collection
            // GC.Collect();
            // GC.WaitForPendingFinalizers();

            // System.Threading.Thread.Sleep(2000);
            // Console.WriteLine($"Object count after WaitForPendingFinalizers: {ClassWithDtor._objectCount}");

            // Console.WriteLine("Press any key to exit...");
            // Console.ReadKey();
        }

        static void GCGenerationsExperiment()
        {
            Console.WriteLine("Starting Generational GC Experiment");

            // Allocate a small object and check its generation
            var gen0Object = new object();
            Console.WriteLine($"Generation of gen0Object: {GC.GetGeneration(gen0Object)}");

            // Force a Gen 0 garbage collection
            GC.Collect(0); // Only collects Gen 0
            Console.WriteLine("Forced Gen 0 collection.");
            Console.WriteLine($"Generation of gen0Object after Gen 0 GC: {GC.GetGeneration(gen0Object)}");

            // re-allocate:
            gen0Object = new object();
            Console.WriteLine($"Generation of gen0Object after re-allocation: {GC.GetGeneration(gen0Object)}");

            // Allocate a large number of objects to trigger Gen 0 collection
            Console.WriteLine("Allocating objects to trigger Gen 0 collection...");
            for (int i = 0; i < 1000_000; i++)
            {
                var temp = new object();
            }

            // The gen0Object should now be promoted to Gen 1
            Console.WriteLine($"Generation of gen0Object after allocations: {GC.GetGeneration(gen0Object)}");

            // Force a Gen 1 garbage collection
            GC.Collect(1); // Collects Gen 0 and Gen 1
            Console.WriteLine("Forced Gen 1 collection.");
            Console.WriteLine($"Generation of gen0Object after Gen 1 GC: {GC.GetGeneration(gen0Object)}");

            // Create a long-lived object and test its promotion
            var gen2Object = new object();
            Console.WriteLine($"Generation of gen2Object: {GC.GetGeneration(gen2Object)}");

            // Simulate long-lived objects by holding references
            for (int i = 0; i < 10; i++)
            {
                GC.Collect(); // Force full GC (collects all generations)
                Console.WriteLine($"Generation of gen2Object after full GC #{i + 1}: {GC.GetGeneration(gen2Object)}");
            }

            // Allocate a large object to demonstrate Large Object Heap (LOH)
            var largeObject = new byte[85_001]; // Large object threshold is ~85 KB
            Console.WriteLine($"Large object is in Generation: {GC.GetGeneration(largeObject)}");

            // End of demonstration
            Console.WriteLine("Generational GC Demo Complete.");
        }
    }
}

