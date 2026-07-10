using Microsoft.EntityFrameworkCore;
using WebApi_MovieDb.Models;
namespace WebApi_MovieDb.DTo
{
    public class MovieDbContext : DbContext
    {
        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options)
        {
        }
        public DbSet<Movies> Movies
        {
            get; set;
        }
        public DbSet<Genres> Genres
        {
            get; set;
        }
    }
}
