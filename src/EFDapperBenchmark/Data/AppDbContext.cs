using EFDapperBenchmark.Configurations;
using EfDapperComparison;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<TestRecord> TestRecords { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(DBConfig.ConnectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TestRecord>(entity =>
        {
            entity.ToTable("testrecords");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedDate).HasColumnName("createddate");
        });
    }
}

