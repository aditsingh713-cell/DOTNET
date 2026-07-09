using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BookStore.Models;
using BookStore.DTO;
using Microsoft.EntityFrameworkCore;
namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookDbContext _context;

        public BookController(BookDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> GetResult()
        {
            return Ok(await _context.Books.ToListAsync());
        }
    }
}
