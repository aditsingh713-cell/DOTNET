using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi_MovieDb.DTo;
using WebApi_MovieDb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace WebApi_MovieDb.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    
    public class MoviesController : ControllerBase
    {
       
        private readonly MovieDbContext _context;
        public MoviesController(MovieDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movies>>> GetMovies()
        {
            var movies = await _context.Movies
                .Include(m => m.GenreNavigation)
                .Select(m => new 
                {
                    Id = m.Id,
                    Title = m.Title,
                    ReleaseYear = m.ReleaseYear,
                    Director = m.Director,
                    GenreName = m.GenreNavigation.Name
                })
                .ToListAsync();
            return Ok(movies);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Movies>> GetMovie(int id)
        {
            var movie = await _context.Movies
                .Include(m => m.GenreNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null) return Ok(null);
            return Ok(new
            {
                Id = movie.Id,
                Title = movie.Title,
                ReleaseYear = movie.ReleaseYear,
                Director = movie.Director,
                GenreName = movie.GenreNavigation.Name
            });
        }
        [HttpPost]
        public async Task<ActionResult<Movies>> CreateMovie([FromBody] MovieInputModel input)
        {
            if (!await _context.Genres.AnyAsync(g => g.Id == input.GenreId))
            {
                return BadRequest($"Genre with Id {input.GenreId} does not exist.");
            }
            var Moveies = new Movies
            {
                Title = input.Title,
                ReleaseYear = input.ReleaseYear,
                Director = input.Director,
                GenreId = input.GenreId
            };
            _context.Movies.Add(Moveies);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetMovie), new { id = Moveies.Id }, Moveies);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie(int id, [FromBody] MovieInputModel input)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null) return NotFound();
            if (!await _context.Genres.AnyAsync(g => g.Id == input.GenreId))
            {
                return BadRequest($"Genre with Id {input.GenreId} does not exist.");
            }
            movie.Title = input.Title;
            movie.ReleaseYear = input.ReleaseYear;
            movie.Director = input.Director;
            movie.GenreId = input.GenreId;
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
