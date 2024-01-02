using KomputerBudowanieAPI.Models;
using System.Security.Cryptography;

namespace KomputerBudowanieAPI.Interfaces
{
    public interface ICompatibilityPartsService
    {
        void Cpu_AllCooling(ref Toast toast, PcConfiguration configuration);


        void Cpu_CpuCooling(ref Toast toast, Cpu? cpu, CpuCooling? cpuCooling, List<string>? socketsFromCpuCooling = null);

        void Motherboard_CpuCooling(ref Toast toast, Motherboard? motherboard, CpuCooling? cpuCooling, List<string>? socketsFromCpuCooling = null);

        void Cpu_WaterCooling(ref Toast toast, Cpu? cpu, WaterCooling? waterCooling, List<string>? socketsFromWaterCooling = null);

        void Motherboard_WaterCooling(ref Toast toast, Motherboard? motherboard, WaterCooling? waterCooling, List<string>? socketsFromWaterCooling = null);

        void Cpu_GraphicCard(ref Toast toast, Cpu? cpu, GraphicCard? graphicCard);

        void Case_WaterCooling(ref Toast toast, Case? pcCase, WaterCooling? waterCooling);

        void Case_CpuCooling(ref Toast toast, Case? pcCase, CpuCooling? cpuCooling);

        void Case_Motherboard(ref Toast toast, Case? pcCase, Motherboard? motherboard);

        void Case_PowerSupply(ref Toast toast, Case? pcCase, PowerSupply? powerSupply); 

        void Cpu_Motherboard(ref Toast toast, Cpu? cpu, Motherboard? motherboard);

        void Case_GraphicCard(ref Toast toast, Case? pcCase, GraphicCard? graphicCard);

        void GraphicCard_Motherboard(ref Toast toast, GraphicCard? graphicCard, Motherboard? motherboard);

        void GraphicCard_PowerSupply(ref Toast toast, GraphicCard? graphicCard, PowerSupply? powerSupply);


        void PowerSupply_Storages(ref Toast toast, PowerSupply? powerSupply, ICollection<PcConfigurationStorage>? storages);

        void Motherboard_Storages(ref Toast toast, Motherboard? motherboard, ICollection<PcConfigurationStorage>? storages);

        void Case_Storages(ref Toast toast, Case? pcCase, ICollection<PcConfigurationStorage>? storages);

        void Motherboard_Rams(ref Toast toast, Motherboard? motherboard, ICollection<PcConfigurationRam>? rams);

        void CpuCooling_Rams(ref Toast toast, CpuCooling? cpuCooling, ICollection<PcConfigurationRam>? rams);
    }
}
