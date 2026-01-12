namespace ThreadsExample
{
    using System;
    using System.Threading;

    class Program
    {
        static void Main()
        {
            Console.WriteLine("Main thread started");

            // Create two threads
            Thread thread1 = new Thread(WriteNumbers);
            Thread thread2 = new Thread(WriteLetters);

            // Start both threads
            thread1.Start();
            thread2.Start();

            // Wait for both threads to complete
            thread1.Join();
            thread2.Join();

            Console.WriteLine("Main thread ended");
        }

        static void WriteNumbers()
        {
            for (int i = 1; i <= 26; i++)
            {
                Console.WriteLine($"Number Thread: {i}");
                Thread.Sleep(1); // Simulate some work
            }
        }

        static void WriteLetters()
        {
            for (char c = 'A'; c <= 'Z'; c++)
            {
                Console.WriteLine($"Letter Thread: {c}");
                Thread.Sleep(1); // Simulate some work
            }
        }
    }
}
