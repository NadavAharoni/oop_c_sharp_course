using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory
{
    internal class RecursionWithException
    {
        // accoding to Microsoft docs, StackOverflowException cannot be caught in .NET
        // see https://learn.microsoft.com/en-us/dotnet/api/system.stackoverflowexception?view=net-8.0
        // And indeed, the following code will crash the program.
        static int RecursiveMethod(int depth)
        {
            // 2. Prints current recursion depth
            Console.WriteLine($"Current depth: {depth}");

            // 3. Recursively calls itself, incrementing depth
            try
            {
                return RecursiveMethod(depth + 1);
            }
            catch (StackOverflowException)
            {
                return depth;
            }
        }
    }
}
