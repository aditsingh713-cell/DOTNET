using Microsoft.EntityFrameworkCore;
using WebAPI2.Model;

namespace WebAPI2.DTO
{
    public class DepartmentDB : DbContext
    {
        public DepartmentDB(DbContextOptions<DepartmentDB> options) : base(options)
        {
        }
        public DbSet<Department> Department
        {
            get; set;

        }
        public DbSet<DepartmentLog> DepartmentLogs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>()
                .ToTable("Department"); // EXACT SQL table name
        }
    }
}