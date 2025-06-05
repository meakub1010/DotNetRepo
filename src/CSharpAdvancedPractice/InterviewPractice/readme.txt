CLR - runtime for .net framework that manages the execution of .net program
it provides 
- memory management
- security
- exception handling
- garbage collection
- thread management
- execute MSIL
- convert MSIL to native code using JIT(just in time) compiler
- manages application lyfe cycle
- enforces code access security and type safety

CLS - Common Language Specification
it provides

- ensures interoperability among .net Languages
- ensures code written in one .net language can be used in another
- specifies subset of common features that all .net languages(C#, VB.NET, F#) must support to ensure compatibility


STACK vs HEAP memory
- stack memory is faster in allocate and deallocate compared to HEAP
- overusing heap memory cause frequent garbage collection which affect performance

- value types(stack) are copied in assignment, safer in concurrency
- reference types(heap) share memeory location, changes affect all references
- stack is thread safe but heap is not

STACK - Value Type:

int x = 5;
int y = x; // copy of x is assigned to y

y = 10; // x is still 5, because value is copied


HEAP - Reference Type:

class Person { public string name; }

Person p1 = new Person { name = "Alice" };
Person p2 = p1;

p2.name = "Bob";

// now p1.name is also "Bob" beacause they point to same reference or heap object

Important notes on STACK and HEAP memory

- both are allocated in RAM
- STACK is allocated on the lower part of the RAM and HEAP is allocated on the upper portion of the RAM
- STACK is faster, smaller and limited but HEAP allows dynamic allocation but slower 
- STACK managed by Compiler/Runtime (LIFO), no garbase collection
- HEAP managed by GC(Random)
- In .NET we can specifies stack size while creating thread

var thread = new Thread(SomeMethod, stackSize: 2 * 1024 * 1024);
thread.start();

Memory Layout (simplified):

+------------------------------+
|          HEAP               |
|  "John" (string)            |
|  Person object              |
|------------------------------|
|          STACK              |
|  int x = 10                 |
|  ref to Person (pointer)    |
|  method call frames         |
+------------------------------+


STRUCT
struct is a value type to encapsulate small group of related variables kind of similar to class but with important differences in memeory
allocation, performance and use cases.
- allocated on stack since value type
- inheritence not supported
- copied by value
- use when object is small and contains mostly data
- you want immutable or atomic data containers like Point, Vector, Color


FACTORY PATTERN

- factory pattern is used to create objects without exposing the creation logic to the client. It provides a layer of abstraction
over the instantiation process
- code reference FactoryPatternExample.cs

when to use Factory pattern
- need to centralize object creation logic
- when the creation logic is complex
- want to decouple clients from specific class implementations


REPOSITORY PATTERN

- is used to abstract the data access logic from the business logic
- it acts like an in-memory collection for the domain but actually interacts with database

Factory vs Repository Pattern
- purpose of factory pattern is to create object but repository pattern is data access abstraction 
- factory return instance but repository returns domain/entity objects 
- use factory when instantiation logic is complex, use repository when seperate business logics from data
- Example: Factory - Vehicle Factory, Repository - product repository manages product entities