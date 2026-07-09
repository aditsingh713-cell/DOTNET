using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AdventureWK1.Model;
using Microsoft.EntityFrameworkCore;
namespace AdventureWK1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditController : ControllerBase
    {
        private readonly AdventureWorks2019Context _context;

        public CreditController(AdventureWorks2019Context context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CreditCard>>> GetCreditCards()
        {
            return await _context.CreditCards.ToListAsync();
        }
        [HttpGet("transactions")]
        public async Task<IActionResult> GetTransactions(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 50)
                {
                    var query = _context.CreditCards.AsQueryable();

                    var totalCount = await query.CountAsync();
                    var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

                    var data = await query
                        .OrderByDescending(t => t.ModifiedDate)   // FIXED
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();

                    return Ok(new
                    {
                        page,
                        pageSize,
                        totalPages,
                        totalCount,
                        data
                    });
        }


    }
}
