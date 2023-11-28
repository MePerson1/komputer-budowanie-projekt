using KomputerBudowanieAPI.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KomputerBudowanieAPI.Controllers
{
    [ApiController]
    [Route("api/admin")]
    public class AdminController : Controller
    {
        private readonly KomBuildDbContext _context;

        private readonly string[] tables = { "Cases", "CpuCoolings", "Fans", "Cpus", "GraphicCards", "Storages", "Motherboards", "Rams", "PowerSupplys" };

        public AdminController(KomBuildDbContext context)
        {
            this._context = context;
        }

        [HttpDelete("all")]
        public IActionResult DeleteAll()
        {
            foreach (var table in tables)
            {
                var sql = $"DELETE FROM \"{table}\"";
                _context.Database.ExecuteSqlRaw(sql);
            }
            return Ok();
        }

        [HttpDelete("all-cases")]
        public IActionResult DeleteAllCases()
        {
            _context.Database.ExecuteSqlRaw($"DELETE FROM \"{tables[0]}\"");
            return Ok();
        }

        [HttpDelete("all-cpu-coolings")]
        public IActionResult DeleteAllCpuCoolings()
        {
            _context.Database.ExecuteSqlRaw($"DELETE FROM \"{tables[1]}\"");
            return Ok();
        }

        [HttpDelete("all-fan")]
        public IActionResult DeleteAllFans()
        {
            _context.Database.ExecuteSqlRaw($"DELETE FROM \"{tables[2]}\"");
            return Ok();
        }

        [HttpDelete("all-cpu")]
        public IActionResult DeleteAllCpus()
        {
            _context.Database.ExecuteSqlRaw($"DELETE FROM \"{tables[3]}\"");
            return Ok();
        }

        [HttpDelete("all-graphic-card")]
        public IActionResult DeleteAllGraphicCards()
        {
            _context.Database.ExecuteSqlRaw($"DELETE FROM \"{tables[4]}\"");
            return Ok();
        }

        [HttpDelete("all-memory")]
        public IActionResult DeleteAllMemories()
        {
            _context.Database.ExecuteSqlRaw($"DELETE FROM \"{tables[5]}\"");
            return Ok();
        }

        [HttpDelete("all-motherboard")]
        public IActionResult DeleteAllMotherboards()
        {
            _context.Database.ExecuteSqlRaw($"DELETE FROM \"{tables[6]}\"");
            return Ok();
        }

        [HttpDelete("all-ram")]
        public IActionResult DeleteAllRams()
        {
            _context.Database.ExecuteSqlRaw($"DELETE FROM \"{tables[7]}\"");
            return Ok();
        }

        [HttpDelete("all-power-supply")]
        public IActionResult DeleteAllPowerSupplys()
        {
            _context.Database.ExecuteSqlRaw($"DELETE FROM \"{tables[8]}\"");
            return Ok();
        }


    }
}
