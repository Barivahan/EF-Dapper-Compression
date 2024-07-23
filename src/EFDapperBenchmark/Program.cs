using BenchmarkDotNet.Running;
using ORMPerformanceComparison.Benchmarks;

Console.WriteLine("Starting ORM Performance Comparison...");
var summary = BenchmarkRunner.Run<ORMBenchmark>();
Console.WriteLine("Benchmark complete. Press any key to exit.");
Console.ReadKey();