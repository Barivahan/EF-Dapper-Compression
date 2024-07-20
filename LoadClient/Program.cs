using RestSharp;
using System.Diagnostics;

Console.WriteLine("Starting load test...");

// Number of threads
int numThreads = 5 ;
// Number of requests per thread
int requestsPerThread = 10000;

// List to hold the tasks
List<Task> tasks = new List<Task>();

// Start the stopwatch
Stopwatch stopwatch = Stopwatch.StartNew();

// Create and start the threads
for (int i = 0; i < numThreads; i++)
{
    int threadIndex = i;
    tasks.Add(Task.Run(() => PerformLoadTest(threadIndex, requestsPerThread)));
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



static async Task PerformLoadTest(int threadIndex, int requestsPerThread)
{
    var client = new RestClient("http://localhost:5000/api/Dapper/insert");
    
  //  var client = new RestClient("http://localhost:5000/api/Ef/insert");
    for (int i = 0; i < requestsPerThread; i++)
    {
        var request = new RestRequest
        {
            Method = Method.POST,            
        };


        IRestResponse response = await client.ExecuteAsync(request);

        if (response.IsSuccessful)
        {
           // Console.WriteLine($"Thread {threadIndex} - Request {i} succeeded.");
        }
        else
        {
            Console.WriteLine($"Thread {threadIndex} - Request {i} failed: {response.ErrorMessage}");
        }

        // Optional: Add a small delay between requests
        // await Task.Delay(100);
    }
}