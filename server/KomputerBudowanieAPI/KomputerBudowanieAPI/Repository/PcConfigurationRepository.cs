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
            return await _context.PcConfigurations
                .Include(pc => pc.Motherboard)
                .Include(pc => pc.Case)
                .Include(pc => pc.Cpu)
                .Include(pc => pc.CpuCooling)
                .Include(pc => pc.GraphicCard)
                .Include(pc => pc.PowerSupply)
                .Include(pc => pc.WaterCooling)
                .Include(pc => pc.PcConfigurationRams)
                    .ThenInclude(ram => ram.Ram)
                .Include(pc => pc.PcConfigurationStorages)
                    .ThenInclude(storage => storage.Storage)
                .ToListAsync();
        }

        public async Task<IEnumerable<PcConfiguration>> GetAllAsync(int userId)
        {
            return await _context.Set<PcConfiguration>().Where(config => config.User.Id == userId)
                .Include(pc => pc.Motherboard)
                .Include(pc => pc.Case)
                .Include(pc => pc.Cpu)
                .Include(pc => pc.CpuCooling)
                .Include(pc => pc.GraphicCard)
                .Include(pc => pc.PowerSupply)
                .Include(pc => pc.WaterCooling)
                .Include(pc => pc.PcConfigurationRams)
                    .ThenInclude(ram => ram.Ram)
                .Include(pc => pc.PcConfigurationStorages)
                    .ThenInclude(storage => storage.Storage)
                .ToListAsync();
        }

        public async Task<PcConfiguration?> GetByIdAsync(Guid id)
        {
            return await _context.Set<PcConfiguration>().Include(pc => pc.Motherboard)
                .Include(pc => pc.Case)
                .Include(pc => pc.Cpu)
                .Include(pc => pc.CpuCooling)
                .Include(pc => pc.GraphicCard)
                .Include(pc => pc.PowerSupply)
                .Include(pc => pc.WaterCooling)
                .Include(pc => pc.PcConfigurationRams)
                    .ThenInclude(ram => ram.Ram)
                .Include(pc => pc.PcConfigurationStorages)
                    .ThenInclude(storage => storage.Storage)
                .FirstOrDefaultAsync(pc => pc.Id == id);
        }

        public async Task<bool> Create(PcConfigurationDto newConfigurationDto)
        {
            try
            {
                PcConfiguration pcConfiguration = new PcConfiguration();

                pcConfiguration = await GetDataFromIds(newConfigurationDto, pcConfiguration);
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
                configuration = await GetDataFromIds(dto, configuration);

                await SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }


        }

        public async Task<PcConfiguration?> GetDataFromIds(PcConfigurationDto dto, PcConfiguration pcConfiguration)
        {
            var pcCase = await _context.Set<Case>().FindAsync(dto.CaseId);
            var cpu = await _context.Cpus.FirstOrDefaultAsync(x => x.Id == dto.CpuId);
            var cpuCooling = await _context.CpuCoolings.FirstOrDefaultAsync(x => x.Id == dto.CpuCoolingId);
            var motherboard = await _context.Motherboards.FirstOrDefaultAsync(x => x.Id == dto.MotherboadId);
            var graphicCard = await _context.GraphicCards.FirstOrDefaultAsync(x => x.Id == dto.GraphicCardId);
            var powerSupply = await _context.PowerSupplies.FirstOrDefaultAsync(x => x.Id == dto.PowerSuplyId);
            var waterCooling = await _context.WaterCoolings.FirstOrDefaultAsync(x => x.Id == dto.WaterCoolingId);



            var storages = await _context.Storages
                .Where(x => dto.StorageIds != null && dto.StorageIds.Contains(x.Id))
                .ToListAsync();

            var rams = await _context.Rams
                .Where(x => dto.RamsIds != null && dto.RamsIds.Contains(x.Id))
                .ToListAsync();

            double totalPrice = 0;

            if (pcCase != null)
                totalPrice += pcCase.Price;

            if (cpu != null)
                totalPrice += cpu.Price;

            if (cpuCooling != null)
                totalPrice += cpuCooling.Price;

            if (motherboard != null)
                totalPrice += motherboard.Price;

            if (graphicCard != null)
                totalPrice += graphicCard.Price;

            if (powerSupply != null)
                totalPrice += powerSupply.Price;

            if (waterCooling != null)
                totalPrice += waterCooling.Price;

            // Add prices of storages
            if (storages != null && storages.Any())
            {
                foreach (var storage in storages)
                {
                    totalPrice += storage.Price;
                }
            }

            // Add prices of RAMs
            if (rams != null && rams.Any())
            {
                foreach (var ram in rams)
                {
                    totalPrice += ram.Price;
                }
            }


            pcConfiguration.Name = dto.Name;
            pcConfiguration.Description = dto.Description;
            pcConfiguration.Case = pcCase;
            pcConfiguration.Cpu = cpu;
            pcConfiguration.CpuCooling = cpuCooling;
            pcConfiguration.Motherboard = motherboard;
            pcConfiguration.GraphicCard = graphicCard;
            pcConfiguration.PowerSupply = powerSupply;
            //pcConfiguration.TotalPrice = totalPrice;
            pcConfiguration.PcConfigurationStorages = storages.Select(storage =>
            {
                var quantity = dto.StorageIds.Where(id => id == storage.Id).Count();
                return new PcConfigurationStorage
                {
                    PcConfiguration = pcConfiguration,
                    Storage = storage,
                    Quantity = quantity
                };
            }).ToList();


            try
            {
                pcConfiguration.PcConfigurationRams = rams.Select(ram =>
                {
                    var quantity = dto.RamsIds.Where(id => id == ram.Id).Count();
                    return new PcConfigurationRam
                    {
                        PcConfiguration = pcConfiguration,
                        Ram = ram,
                        Quantity = quantity
                    };
                }).ToList();
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
            }


            pcConfiguration.WaterCooling = waterCooling;

            return pcConfiguration;
        }
    }
}
