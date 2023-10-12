using KomputerBudowanieAPI.Interfaces;
using KomputerBudowanieAPI.Models;

namespace KomputerBudowanieAPI.Repository
{
    public class PcConfigurationRepository : IPcConfigurationRepository
    {
        Task<ICollection<PcConfiguration>> IPcConfigurationRepository.GetAll()
        {
            throw new NotImplementedException();
        }

        Task<PcConfiguration?> IPcConfigurationRepository.GetById(Guid id)
        {
            throw new NotImplementedException();
        }
        public int Create(PcConfiguration cpu)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<PcConfiguration>> GetAll(int userId)
        {
            throw new NotImplementedException();
        }


        public bool Update(Guid id, PcConfiguration cpu)
        {
            throw new NotImplementedException();
        }

        
    }
}
