﻿using KomputerBudowanieAPI.Database;
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

        public async Task<PcConfiguration> Create(PcConfigurationDto newConfigurationDto)
        {
            try
            {
                PcConfiguration pcConfiguration = new PcConfiguration();

                pcConfiguration = await GetDataFromIds(newConfigurationDto, pcConfiguration);
                await _context.AddAsync(pcConfiguration);

                await SaveChanges();
                return pcConfiguration;
            }
            catch (DbUpdateException ex) // Catch specific database update exceptions if using Entity Framework
            {
                throw new Exception("Failed to update the database. Please check your data.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create PC configuration: {ex.Message}");
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

        public async Task<PcConfiguration> Update(Guid id, PcConfigurationDto newConfigurationDto)
        {

            var configuration = await GetByIdAsync(id);

            if (configuration == null)
            {
                throw new Exception("Pc Configuration with that id doesn't exists!");
            }

            try
            {
                PcConfiguration pcConfiguration = new PcConfiguration();

                pcConfiguration = await GetDataFromIds(newConfigurationDto, configuration);

                await SaveChanges();
                return configuration;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Failed to update the database. Please check your data.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create PC configuration: {ex.Message}");
            }


        }

        public async Task<PcConfiguration?> GetDataFromIds(PcConfigurationDto dto, PcConfiguration pcConfiguration)
        {
            var pcCase = await _context.Cases.Include(c => c.Prices).FirstOrDefaultAsync(x => x.Id == dto.CpuId);
            var cpu = await _context.Cpus.Include(c => c.Prices).FirstOrDefaultAsync(x => x.Id == dto.CpuId);
            var cpuCooling = await _context.CpuCoolings.Include(c => c.Prices).FirstOrDefaultAsync(x => x.Id == dto.CpuCoolingId);
            var motherboard = await _context.Motherboards.Include(c => c.Prices).FirstOrDefaultAsync(x => x.Id == dto.MotherboadId);
            var graphicCard = await _context.GraphicCards.Include(c => c.Prices).FirstOrDefaultAsync(x => x.Id == dto.GraphicCardId);
            var powerSupply = await _context.PowerSupplies.Include(c => c.Prices).FirstOrDefaultAsync(x => x.Id == dto.PowerSuplyId);
            var waterCooling = await _context.WaterCoolings.Include(c => c.Prices).FirstOrDefaultAsync(x => x.Id == dto.WaterCoolingId);



            var storages = await _context.Storages.Include(s => s.Prices)
                .Where(x => dto.StorageIds != null && dto.StorageIds.Contains(x.Id))
                .ToListAsync();

            var rams = await _context.Rams.Include(s => s.Prices)
                .Where(x => dto.RamsIds != null && dto.RamsIds.Contains(x.Id))
                .ToListAsync();

            double totalPrice = 0;

            if (pcCase != null && pcCase.Prices.Any() && pcCase.Prices is not null)
                totalPrice += pcCase.Prices.Min(p => p.Price);

            if (cpu != null && cpu.Prices.Any() && cpu.Prices is not null)
                totalPrice += cpu.Prices.Min(p => p.Price);


            if (cpuCooling != null && cpuCooling.Prices.Any() && cpuCooling.Prices is not null)
                totalPrice += cpuCooling.Prices.Min(p => p.Price);

            if (motherboard != null && motherboard.Prices.Any() && motherboard.Prices is not null)
                totalPrice += motherboard.Prices.Min(p => p.Price);

            if (graphicCard != null && graphicCard.Prices.Any() && graphicCard.Prices is not null)
                totalPrice += graphicCard.Prices.Min(p => p.Price);

            if (powerSupply != null && powerSupply.Prices.Any() && powerSupply.Prices is not null)
                totalPrice += powerSupply.Prices.Min(p => p.Price);

            if (waterCooling != null && waterCooling.Prices.Any() && waterCooling.Prices is not null)
                totalPrice += waterCooling.Prices.Min(p => p.Price);


            // Add prices of storages
            if (storages != null && storages.Any())
            {
                foreach (var storage in storages)
                {
                    if (storage.Prices is not null)
                        totalPrice += storage.Prices.Min(p => p.Price);
                }
            }

            // Add prices of RAMs
            if (rams != null && rams.Any())
            {
                foreach (var ram in rams)
                {
                    if (ram.Prices is not null)
                        totalPrice += ram.Prices.Min(p => p.Price);
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
            pcConfiguration.TotalPrice = totalPrice;
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
