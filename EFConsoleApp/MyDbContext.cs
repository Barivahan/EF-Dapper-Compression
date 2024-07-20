using Microsoft.EntityFrameworkCore;

public class MyDbContext : DbContext
{
    public DbSet<TestRecord> TestRecords { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Database=performancetestdb;Username=postgres;Password=123");
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

public class TestRecord
{
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }
}
