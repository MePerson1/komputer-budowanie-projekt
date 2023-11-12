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

        private readonly string[] tables = { "Cases", "CpuCoolings", "Fans", "Cpus", "GraphicCards", "Storages", "Motherboards", "Rams", "PowerSupplys" };

        public AdminController(KomBuildDbContext context)
        {
            this._context = context;
        }

        [HttpDelete("DeleteAll")]
        public IActionResult DeleteAll()
        {
            foreach (var table in tables)
            {
                var sql = $"DELETE FROM \"{table}\"";
                _context.Database.ExecuteSqlRaw(sql);
            }
            return Ok();
        }

        [HttpDelete("DeleteAllCases")]
        public IActionResult DeleteAllCases()
        {
            _context.Database.ExecuteSqlRaw($"DELETE FROM \"{tables[0]}\"");
            return Ok();
        }

        [HttpDelete("DeleteAllCpuCoolings")]
        public IActionResult DeleteAllCpuCoolings()
        {
            _context.Database.ExecuteSqlRaw($"DELETE FROM \"{tables[1]}\"");
            return Ok();
        }

        [HttpDelete("DeleteAllFans")]
        public IActionResult DeleteAllFans()
        {
            _context.Database.ExecuteSqlRaw($"DELETE FROM \"{tables[2]}\"");
            return Ok();
        }

        [HttpDelete("DeleteAllCpus")]
        public IActionResult DeleteAllCpus()
        {
            _context.Database.ExecuteSqlRaw($"DELETE FROM \"{tables[3]}\"");
            return Ok();
        }

        [HttpDelete("DeleteAllGraphicCard")]
        public IActionResult DeleteAllGraphicCards()
        {
            _context.Database.ExecuteSqlRaw($"DELETE FROM \"{tables[4]}\"");
            return Ok();
        }

        [HttpDelete("DeleteAllMemorie")]
        public IActionResult DeleteAllMemories()
        {
            _context.Database.ExecuteSqlRaw($"DELETE FROM \"{tables[5]}\"");
            return Ok();
        }

        [HttpDelete("DeleteAllMotherboard")]
        public IActionResult DeleteAllMotherboards()
        {
            _context.Database.ExecuteSqlRaw($"DELETE FROM \"{tables[6]}\"");
            return Ok();
        }

        [HttpDelete("DeleteAllRams")]
        public IActionResult DeleteAllRams()
        {
            _context.Database.ExecuteSqlRaw($"DELETE FROM \"{tables[7]}\"");
            return Ok();
        }

        [HttpDelete("DeleteAllPowerSupplys")]
        public IActionResult DeleteAllPowerSupplys()
        {
            _context.Database.ExecuteSqlRaw($"DELETE FROM \"{tables[8]}\"");
            return Ok();
        }


    }
}
