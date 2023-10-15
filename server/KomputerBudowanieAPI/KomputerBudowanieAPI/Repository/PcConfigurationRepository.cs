using KomputerBudowanieAPI.Interfaces;
using KomputerBudowanieAPI.Models;

namespace KomputerBudowanieAPI.Repository
{
    public class PcConfigurationRepository : IPcConfigurationRepository
    {
        public Task<IEnumerable<PcConfiguration>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PcConfiguration>> GetAllAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task Create(PcConfiguration entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(PcConfiguration entity)
        {
            throw new NotImplementedException();
        }

        public Task<PcConfiguration?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task SaveChanges()
        {
            throw new NotImplementedException();
        }

        public Task Update(PcConfiguration entity)
        {
            throw new NotImplementedException();
        }
    }
}
