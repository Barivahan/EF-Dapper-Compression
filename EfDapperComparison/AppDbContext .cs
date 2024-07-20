using Microsoft.EntityFrameworkCore;

namespace EfDapperComparison
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<TestRecord> TestRecords { get; set; }
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
}
