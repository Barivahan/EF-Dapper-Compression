using BenchmarkDotNet.Attributes;
using Dapper;
using EfDapperComparison;
using Npgsql;


namespace ORMPerformanceComparison.Benchmarks
{
    [MemoryDiagnoser]
    
    public class UpdateBenchmarks
    {
        private const string ConnectionString = "Host=localhost;Database=performancetestdb;Username=postgres;Password=123";


        private List<int> _batchIds = new List<int>();

        [GlobalSetup]
        public void Setup()
        {
            using var connection = new NpgsqlConnection(ConnectionString);
            var count = connection.ExecuteScalar<int>("SELECT COUNT(*) FROM testrecords");
            if (count < 1000)
            {
                var recordsToAdd = Enumerable.Range(0, 1000 - count)
                    .Select(_ => new { createddate = DateTime.UtcNow });
                connection.Execute("INSERT INTO testrecords (createddate) VALUES (@createddate)", recordsToAdd);
            }
            _batchIds = connection.Query<int>("SELECT id FROM testrecords LIMIT 1000").ToList();
        }

        [Benchmark]
        public void UpdateSingleRecordEFCore()
        {
            using var context = new AppDbContext();
            var record = context.TestRecords.First();
            record.CreatedDate = DateTime.UtcNow;
            context.SaveChanges();
        }

        [Benchmark]
        public void UpdateSingleRecordDapper()
        {
            using var connection = new NpgsqlConnection(ConnectionString);
            var record = connection.QueryFirst<TestRecord>("SELECT * FROM testrecords LIMIT 1");
            connection.Execute("UPDATE testrecords SET createddate = @CreatedDate WHERE id = @Id",
                new { CreatedDate = DateTime.UtcNow, Id = record.Id });
        }

        [Benchmark]
        public void UpdateBatchEFCore()
        {
            using var context = new AppDbContext();
            var records = context.TestRecords.Where(r => _batchIds.Contains(r.Id)).ToList();
            foreach (var record in records)
            {
                record.CreatedDate = DateTime.UtcNow;
            }
            context.SaveChanges();
        }

        [Benchmark]
        public void UpdateBatchDapper()
        {
            using var connection = new NpgsqlConnection(ConnectionString);
            connection.Open();
            using var transaction = connection.BeginTransaction();

            var records = connection.Query<TestRecord>(
                "SELECT * FROM testrecords WHERE id = ANY(@Ids)",
                new { Ids = _batchIds },
                transaction);

            connection.Execute(
                "UPDATE testrecords SET createddate = @CreatedDate WHERE id = ANY(@Ids)",
                new { CreatedDate = DateTime.UtcNow, Ids = _batchIds },
                transaction);

            transaction.Commit();
        }

        
    }
}