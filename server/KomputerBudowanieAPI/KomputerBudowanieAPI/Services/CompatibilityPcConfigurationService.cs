using KomputerBudowanieAPI.Interfaces;
using KomputerBudowanieAPI.Models;
using System.Text.RegularExpressions;

namespace KomputerBudowanieAPI.Services
{
    public class CompatibilityPcConfigurationService : ICompatibilityPcConfigurationService
    {
        private readonly ICompatibilityPartsService _compatibilityPartsService;

        public CompatibilityPcConfigurationService(ICompatibilityPartsService compatibilityPartsService)
        {
            _compatibilityPartsService = compatibilityPartsService;
        }

        public async Task<Toast> CompatibilityCheck(PcConfiguration configuration)
        {
            await Task.Run(() => { });
            Toast toast = new();
            
            if (configuration is null)
            {
                toast.Problems.Add("Coś poszło nie tak!");
                return toast;
            }

            _compatibilityPartsService.Cpu_Motherboard(ref toast, configuration.Cpu, configuration.Motherboard);
            _compatibilityPartsService.Cpu_GraphicCard(ref toast, configuration.Cpu, configuration.GraphicCard);
            _compatibilityPartsService.Cpu_AllCooling(ref toast, configuration);

            _compatibilityPartsService.GraphicCard_Motherboard(ref toast, configuration.GraphicCard, configuration.Motherboard);
            _compatibilityPartsService.GraphicCard_PowerSupply(ref toast, configuration.GraphicCard, configuration.PowerSupply);

            _compatibilityPartsService.Case_Motherboard(ref toast, configuration.Case, configuration.Motherboard);
            _compatibilityPartsService.Case_PowerSupply(ref toast, configuration.Case, configuration.PowerSupply);
            _compatibilityPartsService.Case_GraphicCard(ref toast, configuration.Case, configuration.GraphicCard);
            _compatibilityPartsService.Case_CpuCooling(ref toast, configuration.Case, configuration.CpuCooling);
            _compatibilityPartsService.Case_Storages(ref toast, configuration.Case, configuration.PcConfigurationStorages);

            _compatibilityPartsService.CpuCooling_Rams(ref toast, configuration.CpuCooling, configuration.PcConfigurationRams);
            _compatibilityPartsService.Motherboard_Rams(ref toast, configuration.Motherboard, configuration.PcConfigurationRams);

            _compatibilityPartsService.Motherboard_Storages(ref toast, configuration.Motherboard, configuration.PcConfigurationStorages);
            _compatibilityPartsService.PowerSupply_Storages(ref toast, configuration.PowerSupply, configuration.PcConfigurationStorages);

            return toast;
        }

        public async Task<Toast> CpuCompatibilityCheck(PcConfiguration configuration)
        {
            await Task.Run(() => { });
            Toast toast = new();

            if (configuration is null)
            {
                toast.Problems.Add("Coś poszło nie tak!");
                return toast;
            }

            _compatibilityPartsService.Cpu_Motherboard(ref toast, configuration.Cpu, configuration.Motherboard); //1 i 2
            _compatibilityPartsService.Cpu_AllCooling(ref toast, configuration);
            _compatibilityPartsService.Cpu_GraphicCard(ref toast, configuration.Cpu, configuration.GraphicCard); //3

            return toast;
        }

        public async Task<Toast> MotherboardCompatibilityCheck(PcConfiguration configuration)
        {
            await Task.Run(() => { });
            Toast toast = new();

            if (configuration is null)
            {
                toast.Problems.Add("Coś poszło nie tak!");
                return toast;
            }

            _compatibilityPartsService.Cpu_Motherboard(ref toast, configuration.Cpu, configuration.Motherboard); //1
            _compatibilityPartsService.GraphicCard_Motherboard(ref toast, configuration.GraphicCard, configuration.Motherboard); //2
            _compatibilityPartsService.Case_Motherboard(ref toast, configuration.Case, configuration.Motherboard); //4
            _compatibilityPartsService.Motherboard_Rams(ref toast, configuration.Motherboard, configuration.PcConfigurationRams); //5 i 6
            _compatibilityPartsService.Motherboard_Storages(ref toast, configuration.Motherboard, configuration.PcConfigurationStorages); //7

            return toast;
        }

        public async Task<Toast> CpuCoolingCompatibilityCheck(PcConfiguration configuration)
        {
            await Task.Run(() => { });
            Toast toast = new();

            _compatibilityPartsService.Case_CpuCooling(ref toast, configuration.Case, configuration.CpuCooling); //2
            _compatibilityPartsService.Cpu_CpuCooling(ref toast, configuration.Cpu, configuration.CpuCooling);
            _compatibilityPartsService.CpuCooling_Rams(ref toast, configuration.CpuCooling, configuration.PcConfigurationRams); //4

            return toast;
        }

        public async Task<Toast> WaterCoolingCompatibilityCheck(PcConfiguration configuration)
        {
            await Task.Run(() => { });
            Toast toast = new();

            _compatibilityPartsService.Case_WaterCooling(ref toast, configuration.Case, configuration.WaterCooling);
            _compatibilityPartsService.Cpu_WaterCooling(ref toast, configuration.Cpu, configuration.WaterCooling);

            return toast;
        }

        public async Task<Toast> CoolingCompatibilityCheck(PcConfiguration configuration)
        {
            await Task.Run(() => { });
            Toast toast = new();

            _compatibilityPartsService.Case_CpuCooling(ref toast, configuration.Case, configuration.CpuCooling);
            _compatibilityPartsService.Cpu_AllCooling(ref toast, configuration);
            _compatibilityPartsService.CpuCooling_Rams(ref toast, configuration.CpuCooling, configuration.PcConfigurationRams);

            return toast;
        }

        public async Task<Toast> RamCompatibilityCheck(PcConfiguration configuration)
        {
            await Task.Run(() => { });
            Toast toast = new();

            if (configuration is null)
            {
                toast.Problems.Add("Coś poszło nie tak!");
                return toast;
            }

            _compatibilityPartsService.Motherboard_Rams(ref toast, configuration.Motherboard, configuration.PcConfigurationRams); //1, 2 i 3
            _compatibilityPartsService.CpuCooling_Rams(ref toast, configuration.CpuCooling, configuration.PcConfigurationRams); //4

            return toast;
        }

        public async Task<Toast> StorageCompatibilityCheck(PcConfiguration configuration)
        {
            await Task.Run(() => { });
            Toast toast = new();

            if (configuration is null)
            {
                toast.Problems.Add("Coś poszło nie tak!");
                return toast;
            }

            _compatibilityPartsService.Motherboard_Storages(ref toast, configuration.Motherboard, configuration.PcConfigurationStorages); //1
            _compatibilityPartsService.PowerSupply_Storages(ref toast, configuration.PowerSupply, configuration.PcConfigurationStorages); //2

            return toast;
        }

        public async Task<Toast> GraphicCardCompatibilityCheck(PcConfiguration configuration)
        {
            await Task.Run(() => { });
            Toast toast = new();

            if (configuration is null)
            {
                toast.Problems.Add("Coś poszło nie tak!");
                return toast;
            }
            if (configuration.GraphicCard is null)
            {
                toast.Problems.Add("Brak karty graficznej!");
                return toast;
            }

            _compatibilityPartsService.Case_GraphicCard(ref toast, configuration.Case, configuration.GraphicCard); //1
            _compatibilityPartsService.GraphicCard_Motherboard(ref toast, configuration.GraphicCard, configuration.Motherboard); //3
            _compatibilityPartsService.GraphicCard_PowerSupply(ref toast, configuration.GraphicCard, configuration.PowerSupply); //4

            return toast;
        }

        public async Task<Toast> PowerSupplyCompatibliityCheck(PcConfiguration configuration)
        {
            await Task.Run(() => { });
            Toast toast = new();
            if (configuration is null)
            {
                toast.Problems.Add("Coś poszło nie tak!");
                return toast;
            }

            _compatibilityPartsService.GraphicCard_PowerSupply(ref toast, configuration.GraphicCard, configuration.PowerSupply);
            _compatibilityPartsService.Case_PowerSupply(ref toast, configuration.Case, configuration.PowerSupply);
            _compatibilityPartsService.PowerSupply_Storages(ref toast, configuration.PowerSupply, configuration.PcConfigurationStorages);

            return toast;
        }

        public async Task<Toast> CaseCompatibilityCheck(PcConfiguration configuration)
        {
            await Task.Run(() => { });
            Toast toast = new();
            if (configuration is null)
            {
                toast.Problems.Add("Coś poszło nie tak!");
                return toast;
            }

            _compatibilityPartsService.Case_Motherboard(ref toast, configuration.Case, configuration.Motherboard); //1
            _compatibilityPartsService.Case_PowerSupply(ref toast, configuration.Case, configuration.PowerSupply); //2 i 3
            _compatibilityPartsService.Case_GraphicCard(ref toast, configuration.Case, configuration.GraphicCard); //4
            _compatibilityPartsService.Case_CpuCooling(ref toast, configuration.Case, configuration.CpuCooling); //5
            _compatibilityPartsService.Case_WaterCooling(ref toast, configuration.Case, configuration.WaterCooling);
            _compatibilityPartsService.Case_Storages(ref toast, configuration.Case, configuration.PcConfigurationStorages); //7

            return toast;
        }
    }
}
