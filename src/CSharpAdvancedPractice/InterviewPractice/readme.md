**CLR** - runtime for .net framework that manages the execution of .net program
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

**CLS** - Common Language Specification
it provides

- ensures interoperability among .net Languages
- ensures code written in one .net language can be used in another
- specifies subset of common features that all .net languages(C#, VB.NET, F#) must support to ensure compatibility


### STACK vs HEAP memory
- stack memory is faster in allocate and deallocate compared to HEAP
- overusing heap memory cause frequent garbage collection which affect performance

- value types(stack) are copied in assignment, safer in concurrency
- reference types(heap) share memeory location, changes affect all references
- stack is thread safe but heap is not

### STACK - Value Type:

```
int x = 5;
int y = x; // copy of x is assigned to y

y = 10; // x is still 5, because value is copied`


### HEAP - Reference Type:

`class Person { public string name; }

Person p1 = new Person { name = "Alice" };
Person p2 = p1;

p2.name = "Bob";
```
_now p1.name is also "Bob" beacause they point to same reference or heap object_

### Important notes on STACK and HEAP memory

- both are allocated in RAM
- **STACK** is allocated on the lower part of the RAM and HEAP is allocated on the upper portion of the RAM
- **STACK** is faster, smaller and limited but HEAP allows dynamic allocation but slower 
- **STACK** managed by Compiler/Runtime (LIFO), no garbase collection
- **HEAP** managed by GC(Random)
- In .NET we can specifies stack size while creating thread

```
var thread = new Thread(SomeMethod, stackSize: 2 * 1024 * 1024);
thread.start();
```

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


### STRUCT 
struct is a value type to encapsulate small group of related variables kind of similar to class but with important differences in memeory
allocation, performance and use cases.
- allocated on stack since value type
- inheritence not supported
- copied by value
- use when object is small and contains mostly data
- you want immutable or atomic data containers like Point, Vector, Color


### FACTORY PATTERN

- **Factory Pattern** is used to create objects without exposing the creation logic to the client. It provides a layer of abstraction
over the instantiation process
- code reference FactoryPatternExample.cs

when to use Factory pattern
- need to centralize object creation logic
- when the creation logic is complex
- want to decouple clients from specific class implementations


### REPOSITORY PATTERN

- is used to abstract the data access logic from the business logic
- it acts like an in-memory collection for the domain but actually interacts with database

**Factory vs Repository Pattern**
- purpose of factory pattern is to create object but repository pattern is data access abstraction 
- factory return instance but repository returns domain/entity objects 
- use factory when instantiation logic is complex, use repository when seperate business logics from data
- Example: Factory - Vehicle Factory, Repository - product repository manages product entities

### Circuit Breaker Pattern
- Circuit breaker pattern has a different purpose than the "Retry pattern"
- Retry pattern enables application to retry an operation in the exception that the operation will eventually succeed
- Circuit breaker prevents application performing an operation that likely to fail
- An application can combine both of these pattern but retry logic should be sensitive and it should abandon retry attempts if the circuit breaker indicates that a fault is not transient


### Scenario: Application Suddenly Crashes in Production
**Problem:** .NET core API crashes intermittently without clear logs

**How to Troubleshoot**
1. Check events viewer or system logs for unhandled exceptions or memory access violations
2. Enable app.useExceptionHandler middleware to log internal errors gracefully
3. Analyze **core dumps** using **WinDbg** or **dotnet-dump**
4. Use **Application Insights** or **ELK Stack** to capture structured logs
5. **Tools**
- dotnet-dump
- WinDbg
- SeriLog/ELK
- Azure Application Insights
**Best practice** use global exception handler and circuit breaker pattern

### CPU spikes High CPU usage

- **CPU** spikes 80-100% 
- app becomes unresponsive

**Troubleshoot**
- use task manager or top/htop in linux to identify the process
- collect run time performance metrics running dotnet-trace or PerfView
- look for 
    - infinite loops
    - blocking calls on async calls
    - excessive GC collections
**Tools**
- dotnet-trace
- perfcollect(Linux)
- VS Diagnostice tools

### Memory Leak Detected After Deployment
- gradual memory growth
- out of memory exceptions

**Troubleshoot**
- monitor using **dotnet-counters** or **windows performance monitor**
- take memory dumps via **dotnet dump** and analyze with VS or DotMemory by JetBrains
- Look for
    - static collections holding reference
    - event handlers not being registered
    - large objects on HEAP not being freed

### API takes too long to respond Latency Issues

**Troubleshoot**
- use application performance monitor(APM) tools like app insights
- identify is slowness is
    - in DB - use SQL Profiler
    - in external API - add timeout and retries
    - in thread starvation - use ThreadPool.GetAvailableThreads
- use structured logging - SeriLog with proper trace ids

### Deployment works in local but fails in CI/CD

**Troubleshoot**
- check appsettings based on environment - appsettings.Development.json
- check if docker image runs fine, if in K8
- check .net sdk / runtime mismatch between dev and CI
- use dotnet publish with --self-contained if runtime not installed on target

**Tools**
- Github or Gitlab CI logs
- Docker logs
- Azure Devops release logs


### Intermittent 500 internal server errors

- log HttpContext.TraceIdentifier for correlation
- use Serilog enricher to log user/session/req id
- check middleware order
- validate appsettings for prod

### Authentication Issues in PROD

- JWT not being validated
- 401 on every call

**Troubleshoot**
- check if **clock skew** between client and server causes token invalidation
- confirm that audience/issuer settings are aligned with identity provider
- enable verbose logging for authentication
- decode JWT via jwt.io and verify claims

### Extension Methods
```
public static class StringExtensions {
    public static bool IsEmail(this string input){
        return input.Contains("@") && input.Contains(".");
    }
}

// usage
string email = "muhammad@gmai.com";
bool isValid = email.IsEmail(); // true
```
_**Note:** this keyword in the parameter tells the compiler it's an extension method_

### Operator Overloading
- let overload operators for custom behavior in your class

```
public class Money {
    public static bool operator ==(Money m1, Money m2){
        return m1.Amount == m2.Amount;
    }
    public static bool operator !=(Money m1, Money m2){
        return !(m1 == m2);
    }
}
// usage
Money a = new Money { Amount = 100 };
Money b = new Money { Amount = 100 };
Console.WriteLine(a == b); // true`
```
### Thread vs Task
**Thread**
- is a low level concept. It provides control over individual threads and works directly with the system's threading infrastructure
**Task**
- is a higher order abstraction that represents an asynchronous operation.
- it is part of Task Parallel Library(TPL) and is preferred for handling concurrency, parallelism, pooling, scheduling, execution management etc.
- 3P's
- P -> use thread pools
- P -> parallel processing
- P -> Plus( support return result, cancel, chain, async, await)
```
// Using Thread
Thread thread = new Thread(() => 
{
    Console.WriteLine("Thread started");
});
thread.Start();

// Using Task
Task task = Task.Run(() =>
{
    Console.WriteLine("Task started");
});
task.Wait();
```
### What is the use of async and await in multithreading?
- async and await are used to simplify asynchronous programming
- async marks method as asynchronous
- await is used to wait for the completion of a task
- usefull in UI apps to keep the UI responsive
```
public async Task<int> GetDataAsync()
{
    var result = await Task.Run(() =>
    {
        // Simulate a long-running task
        Thread.Sleep(2000);
        return 42;
    });
    return result;
}

public async Task ExecuteAsync()
{
    int data = await GetDataAsync();
    Console.WriteLine($"Data received: {data}");
}
```
### Explain ThreadPool and Task Parallel Library(TPL) in .NET

**ThreadPool**
- manages a pool of worker threads that are used to execute tasks without the overhead of creating and destroying threads manually

**TPL**
- builds on the threadpool to provide a higher level abstraction for scheduling work, running tasks in parallel, handling continuations

**Code using TPL**

```
using System;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        // Using Task.Run to schedule a task on the ThreadPool
        Task<int> task = Task.Run(() => {
            // Simulate work
            int sum = 0;
            for (int i = 0; i < 10; i++)
            {
                sum += i;
                Task.Delay(100).Wait(); // Simulate delay
            }
            return sum;
        });

        // Continue with a continuation task when the first one completes
        task.ContinueWith(prevTask => {
            Console.WriteLine("Sum calculated: " + prevTask.Result);
        });

        Console.WriteLine("Main thread continues executing...");
        Console.ReadLine(); // Prevent the application from closing immediately
    }
}
```

### How do you manage Thread synchronizatin in .NET
- **lock**, ensure that only one thread executes the critical section at a time. it's a syntactic sugar for **Monitor.Enter/Monitor.Exit**
- **Monitor**, provide more controls with careful coding
- **Mutex**, kernel level synchronization primitive that can work across process bounnderies
- **Semaphore**, allows limiting the number of thread that can access a resource concurrently

**Lock:**
```
using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    private static readonly object _lockObj = new object();
    private static int _counter = 0;

    static void Main()
    {
        Parallel.For(0, 1000, i =>
        {
            // Synchronize the increment operation
            lock (_lockObj)
            {
                _counter++;
            }
        });

        Console.WriteLine($"Final counter value: {_counter}");
        // Expected output: Final counter value: 1000
    }
}
```
### What is Deadlock and how can you prevent them in a multi-threaded .net application
- deadlock occurs when two or more threads are waiting indefinitelty for resources locked by each other.

**Prevention Strategies**
- **Lock Ordering**, always acquire locks in a consistent order
- **Timeout** use synchronization primitive that support timeouts
- **Minimize lock scopes**, keep critical sections as short as possible
- **Using concurrent collections** when possible use thread safe collection from System.Collections.Concurrent

### What is thread safe lazy initialization in .net

- Thread-safe lazy initialization ensures that an object is created only once, even when multiple threads attempt to access it concurrently
**Code**
```
using System;

class Program
{
    // The Lazy<T> initializer ensures that the object is created in a thread-safe manner.
    private static readonly Lazy<HeavyObject> _lazyObject =
        new Lazy<HeavyObject>(() => new HeavyObject(), isThreadSafe: true);

    private static readonly Lazy<HeavyObject> _obj = new Lazy<HeavyObject>(() => new HeavyObject(), isThreadSafe: true);

    static void Main()
    {
        // The object is initialized when accessed for the first time.
        HeavyObject obj = _lazyObject.Value;
        obj.DisplayMessage();
    }
}

class HeavyObject
{
    public HeavyObject()
    {
        Console.WriteLine("HeavyObject is being constructed...");
    }

    public void DisplayMessage()
    {
        Console.WriteLine("HeavyObject instance is now in use.");
    }
}
```


### Kubernetes
- open source container orchestration platform that automates
    - Deployment
    - Scalling
    - Load balancing
    - Management of containerized applications
    - developed by Google
**Need**
- Trend from Monolith to Microservices
- Increased usage of containers

**Offers**
- high availability or no downtime
- scalability or high performance
- disaster recovery, backup and restore

**BASIC ARCHITECTURE**
Cluster
    - At least one master node
    - Multiple worker nodes
    - Each node has **kubelet** process running on it
        **kubelet**, k8 process that enable nodes to talk to each other and also execute some tasks
    - On worker nodes that hold docker containers, where your application is running
    - Master node also run some processes that are absolutely necessary in order to make sure 
        - API server, that also a container
        - UI and API to manage the K8 cluster will talk to this API server
        - Controller manager
            - keeps track of whats happening in the cluster
        - Scheduler, schedule containers on different nodes based on the work load and available resources
        - etcd, key value store that holds state of k8 cluster
    - Virtual Network enable communications among all nodes in cluster

**POD**
- smallest unit k8 user can configure
- wraper of a container
- in general one pod hold one container but depending on the app needs it can hold multiple containers
- each pod has it's own internal IP address
- if pod dies, it get recreated and get new IP address
- **Service** in k8 what synchronize the new IP address so user don't get interrupted with new IP address
    - Service has permanent IP address that we can connect to
    - Load balancing

### Core K8 Concepts and Definitions
| Term            | Definition                                                                | Example                                            |
| --------------- | ------------------------------------------------------------------------- | -------------------------------------------------- |
| **Pod**         | The smallest deployable unit in K8s. Wraps one or more containers.        | A pod runs a single nginx container.               |
| **Deployment**  | Manages the desired state for Pods. Supports rolling updates & rollbacks. | Deploy an app with 3 replicas.                     |
| **ReplicaSet**  | Ensures a specific number of pod replicas are running at any time.        | Keeps 5 pods of the same app alive.                |
| **Service**     | Exposes pods internally (ClusterIP), externally (NodePort, LoadBalancer). | Load balancer routes traffic to your app.          |
| **Ingress**     | Manages external HTTP(S) access to services. Supports routing & TLS.      | Routes `/api` to one service, `/admin` to another. |
| **Namespace**   | Virtual cluster within a K8s cluster for isolation of resources.          | Dev and prod apps can run in separate namespaces.  |
| **ConfigMap**   | Stores config data in key-value format.                                   | Inject environment variables into a container.     |
| **Secret**      | Stores sensitive data (like passwords, API keys), base64-encoded.         | Inject DB credentials securely.                    |
| **Volume**      | Persistent storage attached to pods.                                      | Mount a PVC for database pod data.                 |
| **StatefulSet** | Like Deployment, but for stateful apps with stable identity and storage.  | Running MongoDB or Kafka.                          |
| **DaemonSet**   | Ensures a pod runs on **every node**.                                     | Log collectors like Fluentd.                       |
| **Job/CronJob** | Runs pods for **one-off** or **scheduled** tasks.                         | DB backup every midnight.                          |
| **Node**        | A physical/VM server in the cluster running containerized workloads.      | AWS EC2 node runs multiple pods.                   |
| **Kubelet**     | Agent running on each node to manage the state of that node's pods.       | Checks pod health, talks to control plane.         |
| **kubectl**     | CLI tool to interact with Kubernetes cluster.                             | `kubectl get pods`                                 |


    
    


### What is the difference between a Pod and a Deployment?
A Pod is a single instance of a running process in your cluster. A Deployment manages Pods and ensures the desired number of replicas are running and updated.

### How do you expose a service in Kubernetes?
Use a Service (ClusterIP for internal, NodePort or LoadBalancer for external), or Ingress for HTTP routing.

kubectl expose deployment myapp --type=LoadBalancer --port=80

### What is a ConfigMap vs Secret?
ConfigMap stores non-sensitive config.

Secret stores sensitive info like API keys (base64 encoded).

### What is a Namespace and when would you use it?
A namespace provides scope for resources. Use it for separating environments (dev, test, prod) or by teams.

### How does rolling update work in Kubernetes?
A Deployment updates pods incrementally using strategies like RollingUpdate. It replaces old pods gradually with new ones to avoid downtime.
strategy:
  type: RollingUpdate
  rollingUpdate:
    maxUnavailable: 1
    maxSurge: 1
### What is a PersistentVolume and PersistentVolumeClaim?
PV: actual storage resource in the cluster.

PVC: user's request for storage (claim).

###  What is a DaemonSet?
A DaemonSet ensures that a copy of a pod runs on every node. Great for logging agents, monitoring, etc.

### How do you troubleshoot a failed Pod?
`kubectl describe pod <pod-name> – events & errors

kubectl logs <pod-name> – stdout logs

kubectl exec -it <pod-name> -- bash – shell access`

### What is the role of etcd in Kubernetes?
Answer:
etcd is a distributed key-value store used by Kubernetes to store all cluster data and state.

### What is Helm?
Helm is a package manager for Kubernetes that allows you to define, install, and upgrade complex K8s applications using charts.

### YAML Example: Simple Deployment + Service
`apiVersion: apps/v1
kind: Deployment
metadata:
  name: myapp
spec:
  replicas: 3
  selector:
    matchLabels:
      app: myapp
  template:
    metadata:
      labels:
        app: myapp
    spec:
      containers:
        - name: myapp
          image: myapp:latest
          ports:
            - containerPort: 80
---
apiVersion: v1
kind: Service
metadata:
  name: myapp-service
spec:
  selector:
    app: myapp
  ports:
    - port: 80
  type: LoadBalancer`

  ### HTTP status code usage
  | HTTP Verb         | Status Code | When?                              |
| ----------------- | ----------- | ---------------------------------- |
| `GET /users`      | 200         | Returns a list of users            |
| `POST /users`     | 201         | A user was created                 |
| `DELETE /users/5` | 204         | User deleted, no content to return |
| `PUT /users/5`    | 200 or 204  | User updated                       |
| `GET /unknown`    | 404         | User doesn't exist                 |

### How to write custom middleware in .net

- Create class with constructor that accept (RequestDelegate next) as param
- Implement async Task InvokeAsync(HttpContext context)
- after the work is done for custom middleware call next middleware await next(context)

**Create custom middleware:**
```
public class MyCustomMiddleware
{
    private readonly RequestDelegate _next;

    public MyCustomMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Before the next middleware
        Console.WriteLine($"[Middleware] Request: {context.Request.Method} {context.Request.Path}");

        await _next(context); // Call the next middleware

        // After the next middleware
        Console.WriteLine($"[Middleware] Response: {context.Response.StatusCode}");
    }
}
```
**Create extension method for clean usage**

```
public static class MyCustomMiddlewareExtensions
{
    public static IApplicationBuilder UseMyCustomMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<MyCustomMiddleware>();
    }
}

```
**Register the middleware in the Pipeline(Program.cs)**

```
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseMyCustomMiddleware(); // 👈 Custom middleware goes here

app.MapControllers(); // Or app.Run(...) if using minimal API
app.Run();

```