using BenchmarkDotNet.Running;
using EFDapperBenchmark.Benchmarks;
using ORMPerformanceComparison.Benchmarks;

Console.WriteLine("Starting ORM Performance Comparison...");
BenchmarkRunner.Run<InsertBenchmarks>();
BenchmarkRunner.Run<UpdateBenchmarks>();
Console.WriteLine("Benchmark complete. Press any key to exit.");
Console.ReadKey();