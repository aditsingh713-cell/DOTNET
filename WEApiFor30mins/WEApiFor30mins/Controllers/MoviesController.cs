using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEApiFor30mins.Models;

namespace WEApiFor30mins.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MovieContext _movieContext;

        public MoviesController(MovieContext movieContext)
        {
            _movieContext = movieContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>>GetMovies()
        {
            if (_movieContext.Movies== null)
            {
                return NotFound();
            }
            return await _movieContext.Movies.ToListAsync();
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<Movie>> GetMovies(int Id)
        {
            if (_movieContext.Movies is null)
            {
                return NotFound();
            }
            var movie = await _movieContext.Movies.FindAsync(Id);
            if (movie is null)
            {
                return NotFound();
            }
            return movie;
        }
    }
}
