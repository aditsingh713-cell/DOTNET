using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CricketProfile.Model;
using Microsoft.EntityFrameworkCore;

namespace CricketProfile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoreController : ControllerBase
    {
        private readonly CrickDbContext _context;

        public ScoreController(CrickDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetCricketerProfile()
        {
            var profile = await _context.CricketerProfile.ToListAsync();
            return Ok(profile);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCricketerProfileById(int id)
        {
            var profile = await _context.CricketerProfile.FindAsync(id);
            if (profile == null)
            {
                return NotFound();
            }
            return Ok(profile);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCricketerProfile([FromBody] CricketerProfile profile)
        {
            _context.CricketerProfile.Add(profile);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCricketerProfileById), new { id = profile.ID }, profile);
        }
    }
}
