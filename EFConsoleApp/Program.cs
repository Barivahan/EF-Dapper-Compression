using System.Diagnostics;

Console.WriteLine("Starting load test...");

// Number of threads
int numThreads = 1;
// Number of requests per thread
int requestsPerThread = 5000;

// List to hold the tasks
List<Task> tasks = new List<Task>();

Stopwatch stopwatch = Stopwatch.StartNew();


// Create and start the threads
for (int i = 0; i < numThreads; i++)
{
    int threadIndex = i;
    tasks.Add(Task.Run(() => PerformLoadTestAsync(requestsPerThread)));
}

// Wait for all tasks to complete
await Task.WhenAll(tasks);

// Stop the stopwatch
stopwatch.Stop();

// Calculate and display performance metrics
double totalRequests = numThreads * requestsPerThread;
double totalSeconds = stopwatch.Elapsed.TotalSeconds;
double requestsPerSecond = totalRequests / totalSeconds;

Console.WriteLine($"Load test completed in {stopwatch.Elapsed.TotalSeconds:F2} seconds.");
Console.WriteLine($"Total requests: {totalRequests}");
Console.WriteLine($"Requests per second: {requestsPerSecond:F2}");


async Task PerformLoadTestAsync(int requestsPerThread)
{
    using (var db = new MyDbContext())
    {
        for (int i = 0; i < requestsPerThread; i++)
        {
            try
            {
                var record = new TestRecord { CreatedDate = DateTime.UtcNow };
                db.Add(record);
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
