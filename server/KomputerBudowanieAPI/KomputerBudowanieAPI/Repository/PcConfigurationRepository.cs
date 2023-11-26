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
            .Include(pc => pc.CpuCooling)
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
            .Include(pc => pc.CpuCooling)
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
            .Include(pc => pc.CpuCooling)
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
                var pcCase = await _context.Set<Case>().FindAsync(newConfigurationDto.CaseId);
                var cpu = await _context.Cpus.FirstOrDefaultAsync(x => x.Id == newConfigurationDto.CpuId);
                var cpuCooling = await _context.CpuCoolings.FirstOrDefaultAsync(x => x.Id == newConfigurationDto.CpuCoolingId);
                var motherboard = await _context.Motherboards.FirstOrDefaultAsync(x => x.Id == newConfigurationDto.MotherboadId);
                var graphicCard = await _context.GraphicCards.FirstOrDefaultAsync(x => x.Id == newConfigurationDto.GraphicCardId);
                var powerSupply = await _context.PowerSupplies.FirstOrDefaultAsync(x => x.Id == newConfigurationDto.PowerSuplyId);

                var storages = await _context.Storages
                    .Where(x => newConfigurationDto.StorageIds != null && newConfigurationDto.StorageIds.Contains(x.Id))
                    .ToListAsync();

                var rams = await _context.Rams
                    .Where(x => newConfigurationDto.RamsIds != null && newConfigurationDto.RamsIds.Contains(x.Id))
                    .ToListAsync();

                var fans = await _context.Fans
                    .Where(x => newConfigurationDto.FanIds != null && newConfigurationDto.FanIds.Contains(x.Id))
                    .ToListAsync();

                PcConfiguration pcConfiguration = new PcConfiguration()
                {
                    Name = newConfigurationDto.Name,
                    Description = newConfigurationDto.Description,
                    Case = pcCase,
                    Cpu = cpu,
                    CpuCooling = cpuCooling,
                    Fans = fans,
                    Motherboard = motherboard,
                    GraphicCard = graphicCard,
                    PowerSupply = powerSupply,
                    Storages = storages,
                    Rams = rams
                };

                await _context.AddAsync(pcConfiguration);

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
                var powerSupply = await _context.PowerSupplies.FirstOrDefaultAsync(x => x.Id == dto.PowerSuplyId);

                var storages = await _context.Storages
                     .Where(x => dto.StorageIds != null && dto.StorageIds.Contains(x.Id))
                     .ToListAsync();

                var rams = await _context.Rams
                    .Where(x => dto.RamsIds != null && dto.RamsIds.Contains(x.Id))
                    .ToListAsync();

                var fans = await _context.Fans
                    .Where(x => dto.FanIds != null && dto.FanIds.Contains(x.Id))
                    .ToListAsync();

                configuration.Name = dto.Name;
                configuration.Description = dto.Description;
                configuration.Case = pcCase;
                configuration.Cpu = cpu;
                configuration.CpuCooling = cpuCooling;
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
