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

            return await _context.PcConfigurations.ToListAsync();



        }

        public async Task<IEnumerable<PcConfiguration>> GetAllAsync(int userId)
        {
            return await _context.Set<PcConfiguration>().Where(config => config.User.Id == userId).ToListAsync();
        }

        public async Task<PcConfiguration?> GetByIdAsync(Guid id)
        {
            return await _context.Set<PcConfiguration>().FindAsync(id);
        }

        public async Task<bool> Create(PcConfigurationDto newConfigurationDto)
        {
            try
            {
                var pcCase = _context.Cases.FirstOrDefault(x => x.Id == newConfigurationDto.CaseId);
                var cpu = _context.Cpus.FirstOrDefault(x => x.Id == newConfigurationDto.CpuId);
                var cpuCooling = _context.CpuCoolings.FirstOrDefault(x => x.Id == newConfigurationDto.CpuCoolingId);
                var fan = _context.Fans.FirstOrDefault(x => x.Id == newConfigurationDto.FanId);
                var motherboard = _context.Motherboards.FirstOrDefault(x => x.Id == newConfigurationDto.MotherboadId);
                var graphicCard = _context.GraphicCards.FirstOrDefault(x => x.Id == newConfigurationDto.GraphicCardId);
                var powerSupply = _context.PowerSupplys.FirstOrDefault(x => x.Id == newConfigurationDto.PowerSuplyId);

                var memories = _context.Memories.Where(x => newConfigurationDto.MemoryIds.Contains(x.Id)).ToList();
                var rams = _context.Rams.Where(x => newConfigurationDto.RamsIds.Contains(x.Id)).ToList();
                PcConfiguration pcConfiguration = new PcConfiguration()
                {
                    Name = newConfigurationDto.Name,
                    Description = newConfigurationDto.Description,
                    Case = pcCase,
                    Cpu = cpu,
                    CPU_Cooling = cpuCooling,
                    Fan = fan,
                    Motherboard = motherboard,
                    GraphicCard = graphicCard,
                    PowerSupply = powerSupply,
                    Memories = memories,
                    Rams = rams
                };


                await _context.AddAsync(pcConfiguration);
                //await _context.AddAsync();
                await SaveChanges();
                return true;

            }
            catch (Exception ex) { return false; }



        }

        public async Task Delete(PcConfiguration entity)
        {
            throw new NotImplementedException();
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Update(PcConfiguration entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await SaveChanges();
        }
    }
}
