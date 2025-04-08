namespace ObjectPooling
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ResourcePoolManager resourcePoolManager = ResourcePoolManager.GetInstance(5);
            Resource resource1 = resourcePoolManager.GetResource();
            Resource resource2 = resourcePoolManager.GetResource();
            Resource resource3 = resourcePoolManager.GetResource();
            Resource resource4 = resourcePoolManager.GetResource();
            Resource resource5 = resourcePoolManager.GetResource();
            Resource resource6 = resourcePoolManager.GetResource();
            resourcePoolManager.ReleaseResource(resource1);
            resourcePoolManager.ReleaseResource(resource2);
            resourcePoolManager.ReleaseResource(resource3);
            resourcePoolManager.ReleaseResource(resource4);
            resourcePoolManager.ReleaseResource(resource5);
            resourcePoolManager.ReleaseResource(resource6);

            // Simulate concurrent access
            Parallel.For(0, 6, i =>
            {
              
                    Console.WriteLine($"Thread {i} requesting resource...");
                    Resource resource = resourcePoolManager.GetResource();
                    if (resource == null)
                    {
                        Console.WriteLine($"Thread {i} could not get resource./Resource not available.");
                    }
                    else
                    {
                        Console.WriteLine($"Thread {i} got resource.");
                    }
                    if (resource != null)
                    {
                        // Simulate some work with the resource
                        Task.Delay(100).Wait();
                        resourcePoolManager.ReleaseResource(resource);
                        Console.WriteLine($"Thread {i} released resource.");
                    }
            });

            // Wait for user input to keep the console open
            Console.ReadLine();
        }
    }
}
