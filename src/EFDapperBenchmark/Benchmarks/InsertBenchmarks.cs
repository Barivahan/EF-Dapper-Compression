using BenchmarkDotNet.Attributes;
using Dapper;
using EFDapperBenchmark.Configurations;
using EfDapperComparison;
using Npgsql;


namespace EFDapperBenchmark.Benchmarks
{
    [MemoryDiagnoser]
    public class InsertBenchmarks
    {
        
        [Benchmark]
        public TestRecord GetRecordByIdEFCore()
        {
            using var context = new AppDbContext();
            return context.TestRecords.FirstOrDefault(r => r.Id == 1);
        }

        [Benchmark]
        public TestRecord GetRecordByIdDapper()
        {
            using var connection = new NpgsqlConnection(DBConfig.ConnectionString);
            return connection.QueryFirstOrDefault<TestRecord>("SELECT * FROM testrecords WHERE id = @Id", new { Id = 1 });
        }

        [Benchmark]
        public int InsertRecordEFCore()
        {
            using var context = new AppDbContext();
            var record = new TestRecord { CreatedDate = DateTime.UtcNow };
            context.TestRecords.Add(record);
            return context.SaveChanges();
        }

        [Benchmark]
        public int InsertRecordDapper()
        {
            using var connection = new NpgsqlConnection(DBConfig.ConnectionString);
            return connection.Execute("INSERT INTO testrecords (createddate) VALUES (@CreatedDate)",
                new { CreatedDate = DateTime.UtcNow });
        }
    }
}
