using Microsoft.EntityFrameworkCore;
using WebApi_1.Models;
namespace WebApi_1.DTO
{
    public class EmployeeDB : DbContext
    {
        public EmployeeDB(DbContextOptions<EmployeeDB> options) : base(options)
        {
        }
        DbSet<Employee> Employees { get; set; }
    }
}