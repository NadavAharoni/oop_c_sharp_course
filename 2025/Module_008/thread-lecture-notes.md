# Introduction to C# Threads

## Understanding Threads - Basics with a Simple Example

Let's start with a basic example that demonstrates the behavior of unsynchronized threads.

```csharp
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
        for (int i = 1; i <= 5; i++)
        {
            Console.WriteLine($"Number Thread: {i}");
            Thread.Sleep(100); // Simulate some work
        }
    }

    static void WriteLetters()
    {
        for (char c = 'A'; c <= 'E'; c++)
        {
            Console.WriteLine($"Letter Thread: {c}");
            Thread.Sleep(100); // Simulate some work
        }
    }
}
```

### What's Happening in This Code?

1. We create two separate threads:
   - One thread writes numbers (1 to 5)
   - Another thread writes letters (A to E)

2. Key Points to Notice:
   - The output order is not guaranteed
   - Both threads run concurrently
   - Each thread operates independently
   - We use `Thread.Sleep()` to simulate work and make the threading behavior more visible

### Sample Output
The actual output might look something like this (but will vary between runs):
```
Main thread started
Number Thread: 1
Letter Thread: A
Letter Thread: B
Number Thread: 2
Number Thread: 3
Letter Thread: C
Letter Thread: D
Number Thread: 4
Number Thread: 5
Letter Thread: E
Main thread ended
```

### Important Concepts

1. Thread Creation:
   - Threads are created using the `Thread` class
   - Each thread needs a method to execute (thread entry point)
   - Threads must be explicitly started using the `Start()` method

2. Thread Joining:
   - `Join()` method waits for a thread to complete
   - Used to ensure all work is done before program ends

3. Concurrency Issues:
   - Without synchronization, threads run independently
   - Output can be interleaved unpredictably
   - This can lead to race conditions when sharing resources
