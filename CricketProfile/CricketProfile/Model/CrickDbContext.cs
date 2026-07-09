using Microsoft.EntityFrameworkCore;
namespace CricketProfile.Model
{
    public class CrickDbContext: DbContext
    {
        public CrickDbContext(DbContextOptions<CrickDbContext> options) : base(options)
        {
        }
        public DbSet<CricketerProfile> CricketerProfile { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CricketerProfile>().ToTable("CricketerProfile");
        }

    }
}
