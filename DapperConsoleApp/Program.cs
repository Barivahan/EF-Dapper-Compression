using Dapper;
using Npgsql;
using System.Data;
using System.Diagnostics;



Console.WriteLine("Starting load test...");

// Number of threads
int numThreads = 100;
// Number of requests per thread
int requestsPerThread = 50000;

// List to hold the tasks
List<Task> tasks = new List<Task>();

Stopwatch stopwatch = Stopwatch.StartNew();

// Create and start the threads
for (int i = 0; i < numThreads; i++)
{
    int threadIndex = i;
    tasks.Add(Task.Run(() => PerformLoadTestAsync(requestsPerThread, numThreads)));
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


async Task PerformLoadTestAsync(int requestsPerThread, int numThreads)
{
    string connectionString = $"Host = localhost; Database = performancetestdb; Username = postgres; Password = 123";
    using (IDbConnection db = new NpgsqlConnection(connectionString))
    {
        for (int i = 0; i < requestsPerThread; i++)
        {
            try
            {
                var sql = "INSERT INTO testrecords (CreatedDate) VALUES (@CreatedDate) RETURNING Id;";
                var id = await db.ExecuteScalarAsync<int>(sql, new { CreatedDate = DateTime.Now });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}