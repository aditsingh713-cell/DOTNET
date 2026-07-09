using Microsoft.EntityFrameworkCore;
using BookStore.Models;
namespace BookStore.DTO
{
    public class BookDbContext:DbContext
    {
        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options)
        {
        }
          public DbSet<Book> Books { get; set; }
    }
}
