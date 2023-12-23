using KomputerBudowanieAPI.Models;

namespace KomputerBudowanieAPI.Interfaces
{
    public interface ICompatibilityPartsService
    {
        void Cpu_GraphicCard(ref Toast toast, PcConfiguration configuration);

        void Cpu_AllCooling(ref Toast toast, PcConfiguration configuration);

        void Case_Storages(ref Toast toast, PcConfiguration configuration);

        void Case_PowerSupply(ref Toast toast, PcConfiguration configuration);

        void Ram_CpuCooling(ref Toast toast, PcConfiguration configuration);

        void Ram_Motherboard(ref Toast toast, PcConfiguration configuration);

        void Storage_Motherboard(ref Toast toast, PcConfiguration configuration);

        void Storages_PowerSupply(ref Toast toast, PcConfiguration configuration);

        void Cpu_CpuCooling(ref Toast toast, Cpu? cpu, CpuCooling? cpuCooling);

        void Cpu_WaterCooling(ref Toast toast, Cpu? cpu, WaterCooling? waterCooling);

        void Case_WaterCooling(ref Toast toast, Case? pcCase, WaterCooling? waterCooling);

        void Case_CpuCooling(ref Toast toast, Case? pcCase, CpuCooling? cpuCooling);

        void Case_Motherboard(ref Toast toast, Case? pcCase, Motherboard? motherboard);

        void Cpu_Motherboard(ref Toast toast, Cpu? cpu, Motherboard? motherboard);

        void Case_GraphicCard(ref Toast toast, Case? pcCase, GraphicCard? graphicCard);

        void GraphicCard_Motherboard(ref Toast toast, GraphicCard? graphicCard, Motherboard? motherboard);

        void GraphicCard_PowerSupply(ref Toast toast, GraphicCard? graphicCard, PowerSupply? powerSupply);
    }
}
