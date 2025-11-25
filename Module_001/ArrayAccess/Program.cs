using System.Diagnostics;

namespace ArrayAccess
{
    internal class Program
    {
        static void MaxArraySize()
        {
            long arraySize = 10_000;
            while (true)
            {
                try
                {
                    Console.WriteLine($"Allocating array of size {arraySize:N0}");
                    long[] largeArray = new long[arraySize];
                    arraySize *= 10;
                }
                catch (OutOfMemoryException)
                {
                    Console.WriteLine("Caught OutOfMemoryException");
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"arraySize={arraySize:N0}");
                    Console.WriteLine($"Caught Exception {e}");
                    break;
                }
            }
        }

        static void ArrayAccessTime()
        {
            // Create a very large array
            long array_size = 1_000_000_000;
            long[] a = new long[array_size];
            
            Random rand = new Random();

            long iterations = 10_000_000;

            // Sequential access and measure time
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            long index = 200_000_000;
            for (long i = 0; i < iterations; i++)
            {
                int value = rand.Next(1000);
                a[index] = value;
                index = (index + 1) % array_size;
            }
            stopwatch.Stop();
            Console.WriteLine($"Time taken for sequential access:" +
                $" {stopwatch.ElapsedMilliseconds} ms");

            // Access elements and measure time
            stopwatch.Start();
            index = 0;
            for (long i = 0; i < iterations; i++)
            {
                int value = rand.Next(1000);
                a[index] = value;
                if (i % 99_000 == 0) // || index > 900_000_000
                {
                    Console.WriteLine($"Iteration: {i}: index {index:N0}");
                }
                index = (index + 1300) % array_size;
            }
            stopwatch.Stop();
            Console.WriteLine($"Time taken for random access:" +
                $" {stopwatch.ElapsedMilliseconds} ms");
        }
        static void Main(string[] args)
        {
            // MaxArraySize();
            ArrayAccessTime();
        }
    }
}
