using Microsoft.EntityFrameworkCore;

namespace Grpc.Server.Data.Contexts
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {
        public DbSet<BorrowRequest> BorrowRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BorrowRequest>().HasKey(x => x.Name);
        }
    }
}
