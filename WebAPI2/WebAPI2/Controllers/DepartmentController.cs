using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI2.DTO;
using WebAPI2.Filters;
using WebAPI2.Model;

namespace WebAPI2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly DepartmentDB _context;

        public DepartmentController(DepartmentDB context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartments()
        {
            var list = await _context.Department.ToListAsync();
            return Ok(list);
        }
        [HttpPost]
        [ServiceFilter(typeof(DepartmentActionLogFilter))]
        public async Task<IActionResult> CreateDepartment([FromBody] Department department)
        {
            _context.Department.Add(department);
            await _context.SaveChangesAsync();

            return Ok(department);
        }

        [HttpGet("logs/{departmentName}")]
        public async Task<IActionResult> GetLogs(string departmentName)
        {
            var logs = await _context.DepartmentLogs
                .Where(l => l.DepartmentName == departmentName)
                .OrderByDescending(l => l.Timestamp)
                .ToListAsync();

            return Ok(logs);
        }


    }
}
