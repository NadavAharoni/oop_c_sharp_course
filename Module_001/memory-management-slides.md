---
marp: true
---

# C# Memory Management
## Course Module Slides

---

# Memory Basics: Static vs. Stack vs. Heap

**Static / global:**

* Pre allocated
* Not necesarily global

---

# Memory Basics: Static vs. Stack vs. Heap

**Stack:**
* Used for function local variables
* When a function is called the operating system "makes room" for the local variables
* Function calls are LIFO (Last In, First Out) therefore stack is the natural data structure
* Makes it easier to implement recursion
* Fast, automatic memory management

---

# Memory Basics: Static vs. Stack vs. Heap

**Heap:**
* Dynamic memory allocation
* Flexible size
* Managed by garbage collector
* Stores reference type objects

---

# Value Types vs Reference Types

**Value Types:**
* Stored directly on stack
* Copy on assignment
* Examples:
  - `struct`
  - Primitive types (`int`, `double`, `bool`)
  - `enum`

---

# Value Types vs Reference Types

**Reference Types:**
* Stored on heap
* Reference stored on stack
* Examples:
  - `class`
  - Arrays
  - Interfaces
  - Delegates

---

# Understanding Value Semantics

```csharp
public struct Point
{
    public int X;
    public int Y;
}

Point p1 = new Point { X = 1, Y = 2 };
Point p2 = p1;  // Creates a copy
p2.X = 10;      // Only modifies p2

// p1 remains unchanged: {X:1, Y:2}
// p2 is modified: {X:10, Y:2}
```

---

# Understanding Reference Semantics

```csharp
public class PointClass
{
    public int X;
    public int Y;
}

PointClass p1 = new PointClass { X = 1, Y = 2 };
PointClass p2 = p1;  // Both reference same object
p2.X = 10;          // Modifies shared object

// Both p1 and p2 now see: {X:10, Y:2}
```

---

# ref Keyword

* Purpose: Pass variables by reference.
* Can also be used locally

Local Usage:
```csharp
int x = 5;
ref int y = ref x;
y = 10;
Console.WriteLine(x); // Outputs: 10
```

With Arrays:
```csharp
int[] arr = { 1, 2, 3 };
ref int elem = ref arr[1];
elem = 42;
Console.WriteLine(arr[1]); // Outputs: 42
```


---

# Array Memory Behavior

Arrays in C# are:
* Reference types
* Contiguously allocated on heap
* Fixed in size once created
* Passed by reference to methods

```csharp
int[] numbers = new int[] { 1, 2, 3 };
ModifyArray(numbers);

void ModifyArray(int[] arr)
{
    arr[0] = 99;        // Modifies original array
    arr = new int[3];   // Only changes local reference
}
```

---

# Stack Memory Investigation

**Stack Overflow Experiment:**
```csharp
static int counter = 0;

static void RecursiveCall()
{
    byte[] localArray = new byte[1024];
    counter++;
    Console.WriteLine($"Call {counter}");
    RecursiveCall();  // Will eventually overflow
}
```

* Stack size is limited
* Large local variables reduce available stack space
* Recursive calls consume stack space quickly

---

# Heap Memory Investigation

**Memory Allocation Tracking:**
```csharp
long before = GC.GetAllocatedBytesForCurrentThread();

// Allocate different types
int[] intArray = new int[1000];
string[] stringArray = new string[1000];

long after = GC.GetAllocatedBytesForCurrentThread();
Console.WriteLine($"Bytes allocated: {after - before}");
```

---

# Garbage Collection

* Automatic memory management
* Collects unreachable objects
* Compacts heap memory
* Multiple generations (0, 1, 2)

```csharp
class TrackableObject
{
    private readonly int _id;
    
    ~TrackableObject()
    {
        Console.WriteLine($"Object {_id} finalized");
    }
}
```

---

# Memory Management Best Practices (1)

1. **Use Value Types When:**
   * Small, simple types
   * Frequently created/destroyed
   * Stack allocation preferred

2. **Use Reference Types When:**
   * Complex objects
   * Shared state needed
   * Polymorphism required

---

# Memory Management Best Practices (2)

3. **Array Considerations:**
   * Pre-size when possible
   * Be careful with large arrays
   * Consider using collections

---

# Common Pitfalls (1)

1. **Memory Leaks:**
   * Forgotten event handlers
   * Undisposed resources
   * Circular references

2. **Performance Issues:**
   * Excessive boxing/unboxing
   * Large value type copying
   * Fragmented heap memory

---

# Common Pitfalls (2)

3. **Stack Overflow:**
   * Deep recursion
   * Large stack allocations
   * Infinite recursion

---

# Lab Exercises (1)

1. **Value vs Reference Comparison:**
   * Implement both struct and class versions
   * Compare behavior in method calls
   * Measure performance differences

2. **Array Manipulation:**
   * Create array modification methods
   * Track reference behavior
   * Measure memory usage

---

# Lab Exercises (2)

3. **Garbage Collection:**
   * Create trackable objects
   * Force collection
   * Observe finalization

---

