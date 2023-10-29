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
        private readonly string[] tables = { "Cases", "CpuCoolings", "Fans", "Cpus", "GraphicCards", "Memories", "Motherboards", "Rams", "PowerSupplys" };

        public AdminController(KomBuildDbContext context)
        {
            this._context = context;
        }

        [HttpDelete]
        public IActionResult DeleteAll()
        {
            foreach (var table in tables)
            {
                var sql = "TRUNCATE TABLE " + table;
                _context.Database.ExecuteSqlRaw(sql);
            }
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteAllCases()
        {
            _context.Database.ExecuteSqlRaw("TRUNCATE TABLE " + tables[0]);
            return Ok();
        }
        
        [HttpDelete]
        public IActionResult DeleteAllCpuCoolings()
        {
            _context.Database.ExecuteSqlRaw("TRUNCATE TABLE " + tables[1]);
            return Ok();
        }
        
        [HttpDelete]
        public IActionResult DeleteAllFans()
        {
            _context.Database.ExecuteSqlRaw("TRUNCATE TABLE " + tables[2]);
            return Ok();
        }
        
        [HttpDelete]
        public IActionResult DeleteAllCpus()
        {
            _context.Database.ExecuteSqlRaw("TRUNCATE TABLE " + tables[3]);
            return Ok();
        }
        
        [HttpDelete]
        public IActionResult DeleteAllGraphicCards()
        {
            _context.Database.ExecuteSqlRaw("TRUNCATE TABLE " + tables[4]);
            return Ok();
        }
        
        [HttpDelete]
        public IActionResult DeleteAllMemories()
        {
            _context.Database.ExecuteSqlRaw("TRUNCATE TABLE " + tables[5]);
            return Ok();
        }
        
        [HttpDelete]
        public IActionResult DeleteAllMotherboards()
        {
            _context.Database.ExecuteSqlRaw("TRUNCATE TABLE " + tables[6]);
            return Ok();
        }
        
        [HttpDelete]
        public IActionResult DeleteAllRams()
        {
            _context.Database.ExecuteSqlRaw("TRUNCATE TABLE " + tables[8]);
            return Ok();
        }
        
        [HttpDelete]
        public IActionResult DeleteAllPowerSupplys()
        {
            _context.Database.ExecuteSqlRaw("TRUNCATE TABLE " + tables[9]);
            return Ok();
        }
        
    }
}
