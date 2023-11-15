﻿using KomputerBudowanieAPI.Models;

namespace KomputerBudowanieAPI.Interfaces
{
    public interface ICompatibilityService
    {
        Task<Toast?> CpuCompatibilityCheck(PcConfiguration configuration);
        Task<Toast?> MemoryCompatibilityCheck(PcConfiguration configuration);
        Task<Toast?> RamCompatibilityCheck(PcConfiguration configuration);

        Task<Toast?> CaseCompatibilityCheck(PcConfiguration configuration);
        Task<string> CompatibilityCheck(PcConfiguration configuration);



    }
}