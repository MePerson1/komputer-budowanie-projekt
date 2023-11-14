using KomputerBudowanieAPI.Models;

namespace KomputerBudowanieAPI.Services
{
    public class CompatibilityDataFilterService
    {

        public void CpuFilter(PcConfiguration configuration, ref ICollection<Cpu> cpus)
        {

            if (configuration.Motherboard is not null)
            {
                cpus = cpus
                    .Where(cpu => configuration.Motherboard.SupportedProcessors.Contains(cpu.Line) && configuration.Motherboard.CPUSocket == cpu.SocketType)
                    .ToList();
            }

        }

        public void MotherboardFilter(PcConfiguration configuration, ref ICollection<Motherboard> motherboards)
        {
            if (configuration.Cpu is not null)
            {
                motherboards = motherboards
                    .Where(motherboard => motherboard.SupportedProcessors.Contains(configuration.Cpu.Line) && motherboard.CPUSocket == configuration.Cpu.SocketType)
                    .ToList();
            }
            if (configuration.GraphicCard is not null)
            {

            }
            if (configuration.Case is not null)
            {

                string compatibility = configuration.Case.Compatibility + ",";
                motherboards = motherboards
                    .Where(motherboard =>
                        (motherboard.BoardStandard == "ATX" && compatibility.Contains(" " + motherboard.BoardStandard + ",")) ||
                        (motherboard.BoardStandard != "ATX" && compatibility.Contains(motherboard.BoardStandard)))
                    .ToList();
            }
            if (configuration.Rams is not null)
            {
                var ramCount = 0; //tutaj jak z tym ram count to trzeba przemyslec xd
                foreach (Ram ram in configuration.Rams)
                {

                    motherboards = motherboards
                        .Where(motherboard => motherboard.MemoryStandard == ram.MemoryType && motherboard.MemoryConnectorType == ram.PinType)
                        .ToList();
                }
            }
            if (configuration.Storages is not null)
            {
                //tutaj znowu regex i jakies dywagacje
            }
        }

        public void CpuCoolingFilter(PcConfiguration configuration, ref ICollection<CpuCooling> cpuCoolings)
        {
            if (configuration.Case is not null)
            {
                cpuCoolings = cpuCoolings
                    .Where(cpuCooling => configuration.Case.MaxCoolingSystemHeightCM < cpuCooling.HeightMM)
                    .ToList();
            }

            if (configuration.Rams is not null)
            {

            }
        }

        public void RamCompatibilityCheck(PcConfiguration configuration, ref ICollection<Ram> rams)
        {

        }
    }
}
