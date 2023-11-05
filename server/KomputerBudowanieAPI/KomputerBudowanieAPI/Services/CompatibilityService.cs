using KomputerBudowanieAPI.Database;
using KomputerBudowanieAPI.Interfaces;
using KomputerBudowanieAPI.Models;

namespace KomputerBudowanieAPI.Services
{
    public class CompatibilityService : ICompatibilityService
    {
        protected KomBuildDbContext _context;
        public CompatibilityService(KomBuildDbContext context)
        {
            this._context = context;
        }
        public async Task<string> CompatibilityCheck(PcConfiguration configuration)
        {
            throw new NotImplementedException();
        }

        public async Task<Toast?> CpuCompatibilityCheck(PcConfiguration configuration)
        {

            Toast toast = new Toast();

            if (configuration is null)
            {
                toast.Problems.Add("Something went wrong");
            }

            if (configuration.Motherboard != null)
            {
                if (configuration.Motherboard.CPUSocket != configuration.Cpu.SocketType)
                {
                    toast.Problems.Add("Wrong motherboard socket!");
                }
            }

            return toast;
        }


    }
}

public class Toast
{
    public ICollection<string> Warnings { get; set; } = new List<string>();
    public ICollection<string> Problems { get; set; } = new List<string>();
}