using KomputerBudowanieAPI.Database;
using KomputerBudowanieAPI.Dto;
using KomputerBudowanieAPI.Interfaces;
using KomputerBudowanieAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KomputerBudowanieAPI.Repository
{
    public class PcConfigurationRepository : IPcConfigurationRepository
    {
        protected KomBuildDbContext _context;
        public PcConfigurationRepository(KomBuildDbContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<PcConfiguration>> GetAllAsync()
        {
            return await _context.PcConfigurations.Include(pc => pc.Motherboard)
            .Include(pc => pc.GraphicCard)
            .Include(pc => pc.Cpu)
            .Include(pc => pc.CPU_Cooling)
            .Include(pc => pc.Case)
            .Include(pc => pc.Fans)
            .Include(pc => pc.PowerSupply)
            .Include(pc => pc.User)
            .Include(pc => pc.Storages)
            .Include(pc => pc.Rams).ToListAsync();
        }

        public async Task<IEnumerable<PcConfiguration>> GetAllAsync(int userId)
        {
            return await _context.Set<PcConfiguration>().Where(config => config.User.Id == userId)
                .Include(pc => pc.GraphicCard)
            .Include(pc => pc.Cpu)
            .Include(pc => pc.CPU_Cooling)
            .Include(pc => pc.Case)
            .Include(pc => pc.Fans)
            .Include(pc => pc.PowerSupply)
            .Include(pc => pc.User)
            .Include(pc => pc.Storages)
            .Include(pc => pc.Rams).ToListAsync();
        }

        public async Task<PcConfiguration?> GetByIdAsync(Guid id)
        {
            return await _context.Set<PcConfiguration>().Include(pc => pc.GraphicCard)
            .Include(pc => pc.Cpu)
            .Include(pc => pc.CPU_Cooling)
            .Include(pc => pc.Case)
            .Include(pc => pc.Fans)
            .Include(pc => pc.PowerSupply)
            .Include(pc => pc.User)
            .Include(pc => pc.Storages)
            .Include(pc => pc.Rams).FirstOrDefaultAsync(pc => pc.Id == id);
        }

        public async Task<bool> Create(PcConfigurationDto newConfigurationDto)
        {
            try
            {
                //var pcCase = _context.Cases.FirstOrDefault(x => x.Id == newConfigurationDto.CaseId);
                var pcCase = await _context.Set<Case>().FindAsync(newConfigurationDto.CaseId);
                var cpu = _context.Cpus.FirstOrDefault(x => x.Id == newConfigurationDto.CpuId);
                var cpuCooling = _context.CpuCoolings.FirstOrDefault(x => x.Id == newConfigurationDto.CpuCoolingId);
                var motherboard = _context.Motherboards.FirstOrDefault(x => x.Id == newConfigurationDto.MotherboadId);
                var graphicCard = _context.GraphicCards.FirstOrDefault(x => x.Id == newConfigurationDto.GraphicCardId);
                var powerSupply = _context.PowerSupplys.FirstOrDefault(x => x.Id == newConfigurationDto.PowerSuplyId);

                //var memories = _context.Memories.Where(x => newConfigurationDto.MemoryIds.Contains(x.Id)).ToList();
                //var rams = _context.Rams.Where(x => newConfigurationDto.RamsIds.Contains(x.Id)).ToList();
                var memories = new List<Storage>();
                if (newConfigurationDto.StorageIds != null && newConfigurationDto.StorageIds.Any())
                {
                    memories = _context.Memories.Where(x => newConfigurationDto.StorageIds.Contains(x.Id)).ToList();
                }

                var rams = new List<Ram>();
                if (newConfigurationDto.RamsIds != null && newConfigurationDto.RamsIds.Any())
                {
                    rams = _context.Rams.Where(x => newConfigurationDto.RamsIds.Contains(x.Id)).ToList();
                }

                var fans = new List<Fan>();
                if (newConfigurationDto.FanIds != null && newConfigurationDto.FanIds.Any())
                {
                    fans = _context.Fans.Where(x => newConfigurationDto.FanIds.Contains(x.Id)).ToList();
                }

                PcConfiguration pcConfiguration = new PcConfiguration()
                {
                    //Id = newConfigurationDto.Id,
                    Name = newConfigurationDto.Name,
                    Description = newConfigurationDto.Description,
                    Case = pcCase,
                    Cpu = cpu,
                    CPU_Cooling = cpuCooling,
                    Fans = fans,
                    Motherboard = motherboard,
                    GraphicCard = graphicCard,
                    PowerSupply = powerSupply,
                    Storages = memories,
                    Rams = rams
                };

                await _context.AddAsync(pcConfiguration);
                //await _context.AddAsync();
                await SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task Delete(PcConfiguration entity)
        {
            _context.Remove(entity);
            await SaveChanges();
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Update(Guid id, PcConfigurationDto dto)
        {

            var configuration = await GetByIdAsync(id);
            if (configuration == null)
            {
                return false;
            }
            try
            {
                var pcCase = await _context.Cases.FirstOrDefaultAsync(x => x.Id == dto.CaseId);
                var cpu = await _context.Cpus.FirstOrDefaultAsync(x => x.Id == dto.CpuId);
                var cpuCooling = await _context.CpuCoolings.FirstOrDefaultAsync(x => x.Id == dto.CpuCoolingId);
                var motherboard = await _context.Motherboards.FirstOrDefaultAsync(x => x.Id == dto.MotherboadId);
                var graphicCard = await _context.GraphicCards.FirstOrDefaultAsync(x => x.Id == dto.GraphicCardId);
                var powerSupply = await _context.PowerSupplys.FirstOrDefaultAsync(x => x.Id == dto.PowerSuplyId);

                //var memories = _context.Memories.Where(x => newConfigurationDto.MemoryIds.Contains(x.Id)).ToList();
                //var rams = _context.Rams.Where(x => newConfigurationDto.RamsIds.Contains(x.Id)).ToList();
                var storages = new List<Storage>();
                if (dto.StorageIds != null && dto.StorageIds.Any())
                {
                    storages = _context.Memories.Where(x => dto.StorageIds.Contains(x.Id)).ToList();
                }

                var rams = new List<Ram>();
                if (dto.RamsIds != null && dto.RamsIds.Any())
                {
                    rams = _context.Rams.Where(x => dto.RamsIds.Contains(x.Id)).ToList();
                }

                var fans = new List<Fan>();
                if (dto.FanIds != null && dto.FanIds.Any())
                {
                    fans = _context.Fans.Where(x => dto.FanIds.Contains(x.Id)).ToList();
                }

                configuration.Name = dto.Name;
                configuration.Description = dto.Description;
                configuration.Case = pcCase;
                configuration.Cpu = cpu;
                configuration.CPU_Cooling = cpuCooling;
                configuration.Fans = fans;
                configuration.Motherboard = motherboard;
                configuration.GraphicCard = graphicCard;
                configuration.PowerSupply = powerSupply;
                configuration.Storages = storages;
                configuration.Rams = rams;

                await SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
