﻿using KomputerBudowanieAPI.Database;
using KomputerBudowanieAPI.Dto;
using KomputerBudowanieAPI.Helpers;
using KomputerBudowanieAPI.Helpers.Request;
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

        public async Task<PagedList<PcConfiguration>> GetAllAsyncPagination(PartsParams partsParams)
        {
            IQueryable<PcConfiguration> query = _context.PcConfigurations
                .Sort(partsParams.SortBy)
                .Search(partsParams.SearchTerm)
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
            .AsQueryable();

            return await PagedList<PcConfiguration>.ToPagedList(query, partsParams.PageNumber, partsParams.PageSize);
        }
        public async Task<IEnumerable<PcConfiguration>> GetAllAsyncPublic()
        {
            return await _context.PcConfigurations
               .Where(pc => pc.isPrivate == false)
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
        public async Task<IEnumerable<PcConfiguration>> GetAllAsync(string userId)
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
                .Include(pc => pc.User)
                .ToListAsync();
        }

        public async Task<PcConfiguration?> GetByIdAsync(Guid id)
        {
            return await _context.Set<PcConfiguration>()
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
                .FirstOrDefaultAsync(pc => pc.Id == id);
        }

        public async Task<PagedList<PcConfiguration>> GetAllAsyncPublicPagination(PartsParams partsParams)
        {
            IQueryable<PcConfiguration> query = _context.PcConfigurations
                .Where(pc => pc.isPrivate == false)
                .Sort(partsParams.SortBy)
                .Search(partsParams.SearchTerm)
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
            .AsQueryable();

            return await PagedList<PcConfiguration>.ToPagedList(query, partsParams.PageNumber, partsParams.PageSize);
        }

        public async Task<PagedList<PcConfiguration>> GetAllAsyncByUserIdPagination(string userId, PartsParams partsParams)
        {
            IQueryable<PcConfiguration> query = _context.PcConfigurations
                   .Where(config => config.User.Id == userId)
                   .Sort(partsParams.SortBy)
                   .Search(partsParams.SearchTerm)
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
               .AsQueryable();

            return await PagedList<PcConfiguration>.ToPagedList(query, partsParams.PageNumber, partsParams.PageSize);
        }

        public async Task<PcConfiguration> Create(PcConfigurationCreateModel newConfigurationDto)
        {
            try
            {
                PcConfiguration pcConfiguration = new PcConfiguration();

                pcConfiguration = await GetDataFromIds(newConfigurationDto, pcConfiguration);
                pcConfiguration.TotalPrice = await CountTotalPrice(pcConfiguration);
                await _context.AddAsync(pcConfiguration);

                await SaveChanges();
                return pcConfiguration;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Nie udało się utworzyć konfiguracji. Sprawdź swoje dane!");
            }
            catch (Exception ex)
            {
                throw new Exception($"Nie udało się utworzyć konfiguracji: {ex.Message}");
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

        public async Task<PcConfiguration> Update(Guid id, PcConfigurationCreateModel newConfigurationDto)
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
                pcConfiguration.TotalPrice = await CountTotalPrice(pcConfiguration);

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
            pcConfiguration.Name = dto.Name;
            pcConfiguration.Description = dto.Description;
            pcConfiguration.isPrivate = dto.isPrivate;

            pcConfiguration.Case = await _context.Cases.Include(c => c.Prices).FirstOrDefaultAsync(x => x.Id == dto.CaseId);
            pcConfiguration.Cpu = await _context.Cpus.Include(c => c.Prices).FirstOrDefaultAsync(x => x.Id == dto.CpuId);
            pcConfiguration.CpuCooling = await _context.CpuCoolings.Include(c => c.Prices).FirstOrDefaultAsync(x => x.Id == dto.CpuCoolingId);
            pcConfiguration.Motherboard = await _context.Motherboards.Include(c => c.Prices).FirstOrDefaultAsync(x => x.Id == dto.MotherboadId);
            pcConfiguration.GraphicCard = await _context.GraphicCards.Include(c => c.Prices).FirstOrDefaultAsync(x => x.Id == dto.GraphicCardId);
            pcConfiguration.PowerSupply = await _context.PowerSupplies.Include(c => c.Prices).FirstOrDefaultAsync(x => x.Id == dto.PowerSuplyId);
            pcConfiguration.WaterCooling = await _context.WaterCoolings.Include(c => c.Prices).FirstOrDefaultAsync(x => x.Id == dto.WaterCoolingId);
            pcConfiguration.User = await _context.Users.FirstOrDefaultAsync(x => x.Id == dto.UserId);

            var storages = await _context.Storages.Include(s => s.Prices)
                .Where(x => dto.StorageIds != null && dto.StorageIds.Contains(x.Id))
                .ToListAsync();

            var rams = await _context.Rams.Include(s => s.Prices)
                .Where(x => dto.RamsIds != null && dto.RamsIds.Contains(x.Id))
                .ToListAsync();

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

            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
            }


            return pcConfiguration;
        }
        public async Task<PcConfiguration?> GetDataFromIds(PcConfigurationCreateModel dto, PcConfiguration pcConfiguration)
        {
            pcConfiguration.Name = dto.Name;
            pcConfiguration.Description = dto.Description;
            pcConfiguration.isPrivate = dto.isPrivate;

            pcConfiguration.Case = await _context.Cases.Include(c => c.Prices).FirstOrDefaultAsync(x => x.Id == dto.CaseId);
            pcConfiguration.Cpu = await _context.Cpus.Include(c => c.Prices).FirstOrDefaultAsync(x => x.Id == dto.CpuId);
            pcConfiguration.CpuCooling = await _context.CpuCoolings.Include(c => c.Prices).FirstOrDefaultAsync(x => x.Id == dto.CpuCoolingId);
            pcConfiguration.Motherboard = await _context.Motherboards.Include(c => c.Prices).FirstOrDefaultAsync(x => x.Id == dto.MotherboadId);
            pcConfiguration.GraphicCard = await _context.GraphicCards.Include(c => c.Prices).FirstOrDefaultAsync(x => x.Id == dto.GraphicCardId);
            pcConfiguration.PowerSupply = await _context.PowerSupplies.Include(c => c.Prices).FirstOrDefaultAsync(x => x.Id == dto.PowerSuplyId);
            pcConfiguration.WaterCooling = await _context.WaterCoolings.Include(c => c.Prices).FirstOrDefaultAsync(x => x.Id == dto.WaterCoolingId);
            pcConfiguration.User = await _context.Users.FirstOrDefaultAsync(x => x.Id == dto.UserId);

            var storages = await _context.Storages.Include(s => s.Prices)
                .Where(x => dto.StorageIds != null && dto.StorageIds.Contains(x.Id))
                .ToListAsync();

            var rams = await _context.Rams.Include(s => s.Prices)
                .Where(x => dto.RamsIds != null && dto.RamsIds.Contains(x.Id))
                .ToListAsync();

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

            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
            }

            return pcConfiguration;
        }
        public async Task<double> CountTotalPrice(PcConfiguration pcConfiguration)
        {
            double totalPrice = 0;


            if (pcConfiguration.Case != null && pcConfiguration.Case.Prices.Any() && pcConfiguration.Case.Prices is not null)
                totalPrice += pcConfiguration.Case.Prices.Min(p => p.Price);

            if (pcConfiguration.Cpu != null && pcConfiguration.Cpu.Prices.Any() && pcConfiguration.Cpu.Prices is not null)
                totalPrice += pcConfiguration.Cpu.Prices.Min(p => p.Price);


            if (pcConfiguration.CpuCooling != null && pcConfiguration.CpuCooling.Prices.Any() && pcConfiguration.CpuCooling.Prices is not null)
                totalPrice += pcConfiguration.CpuCooling.Prices.Min(p => p.Price);

            if (pcConfiguration.Motherboard != null && pcConfiguration.Motherboard.Prices.Any() && pcConfiguration.Motherboard.Prices is not null)
                totalPrice += pcConfiguration.Motherboard.Prices.Min(p => p.Price);

            if (pcConfiguration.GraphicCard != null && pcConfiguration.GraphicCard.Prices.Any() && pcConfiguration.GraphicCard.Prices is not null)
                totalPrice += pcConfiguration.GraphicCard.Prices.Min(p => p.Price);

            if (pcConfiguration.PowerSupply != null && pcConfiguration.PowerSupply.Prices.Any() && pcConfiguration.PowerSupply.Prices is not null)
                totalPrice += pcConfiguration.PowerSupply.Prices.Min(p => p.Price);

            if (pcConfiguration.WaterCooling != null && pcConfiguration.WaterCooling.Prices.Any() && pcConfiguration.WaterCooling.Prices is not null)
                totalPrice += pcConfiguration.WaterCooling.Prices.Min(p => p.Price);

            if (pcConfiguration.PcConfigurationStorages != null && pcConfiguration.PcConfigurationStorages.Any())
            {
                foreach (var storage in pcConfiguration.PcConfigurationStorages)
                {
                    if (storage.Storage.Prices != null && storage.Storage.Prices.Any())
                        totalPrice += storage.Storage.Prices.Min(p => p.Price);
                }
            }

            if (pcConfiguration.PcConfigurationRams != null && pcConfiguration.PcConfigurationRams.Any())
            {
                foreach (var ram in pcConfiguration.PcConfigurationRams)
                {
                    if (ram.Ram.Prices != null && ram.Ram.Prices.Any())
                        totalPrice += ram.Ram.Prices.Min(p => p.Price);
                }
            }

            return totalPrice;
        }

    }


}
