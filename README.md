# ORM Performance Comparison

This project compares the performance of Entity Framework Core and Dapper for common database operations using BenchmarkDotNet.

## Table of Contents
- [Introduction](#introduction)
- [Prerequisites](#prerequisites)
- [Getting Started](#getting-started)
- [Running Benchmarks](#running-benchmarks)
- [Results Visualization](#results-visualization)
- [Project Structure](#project-structure)
- [Contributing](#contributing)
- [License](#license)

## Introduction
This project aims to benchmark the performance of two popular .NET ORMs: Entity Framework Core and Dapper. It uses BenchmarkDotNet to measure the performance of various database operations such as inserting and retrieving records.

## Prerequisites
Before running the benchmarks, ensure you have the following installed:
- [.NET SDK](https://dotnet.microsoft.com/download)
- [PostgreSQL](https://www.postgresql.org/download/)
- [BenchmarkDotNet](https://benchmarkdotnet.org/articles/guides/getting-started.html) NuGet package

## Getting Started
1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/ORMPerformanceComparison.git
   cd ORMPerformanceComparison
2. Configure the connection string in DbConfig.cs
3. Restore NuGet packages:
   ```bash
   dotnet restore
4. Ensure your PostgreSQL server is running and the performancetestdb database is created.
## Running Benchmarks
1. Run the benchmarks using the following command:
   ```bash
   dotnet run -c Release
## Contributing
  Contributions are welcome! Please fork the repository and open a pull request with your changes.
   
