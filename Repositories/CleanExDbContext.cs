using Microsoft.EntityFrameworkCore;

namespace CleanEx.Repositories
{
    public class CleanExDbContext(DbContextOptions<CleanExDbContext> options) : DbContext(options)
    {
        public DbSet<Product> Products { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CleanExDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
