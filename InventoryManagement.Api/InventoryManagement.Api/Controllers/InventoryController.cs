using Microsoft.AspNetCore.Mvc;
using InventoryManagement.Api.Data;
using InventoryManagement.Api.DTOs;
using InventoryManagement.Api.Models;
using Microsoft.EntityFrameworkCore;
namespace InventoryManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InventoryController : Controller
{
    private readonly AppDbContext _context;

    public InventoryController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<InventoryItem>>> GetItems()
    {
        return await _context.InventoryItems.ToListAsync();
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<InventoryItem>> GetItem(int id)
    {
        var item = await _context.InventoryItems.FindAsync(id);
        if (item == null) return NotFound();
        return item;
    }
    public IActionResult Index()
    {
        return View();
    }
}
