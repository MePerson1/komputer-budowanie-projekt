using KomputerBudowanieAPI.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KomputerBudowanieAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : Controller
    {
        readonly KomBuildDbContext _context;
        public AdminController(KomBuildDbContext context)
        {
            this._context = context;
        }
        [HttpGet]
        public IActionResult DeleteAll()
        {
            string[] tables = { "Cases", "CpuCoolings", "Fans", "Cpus", "GraphicCards", "Memories", "Motherboards", "Rams", "PowerSupplys" };
            foreach (var table in tables)
            {
                var sql = "TRUNCATE TABLE " + table;
                _context.Database.ExecuteSqlRaw(sql);
            }

            return Ok();
        }
    }
}
