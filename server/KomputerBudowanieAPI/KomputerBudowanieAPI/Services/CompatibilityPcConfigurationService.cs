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
            _compatibilityPartsService.Cpu_GraphicCard(ref toast, configuration);
            _compatibilityPartsService.Cpu_AllCooling(ref toast, configuration);

            _compatibilityPartsService.GraphicCard_Motherboard(ref toast, configuration.GraphicCard, configuration.Motherboard);
            _compatibilityPartsService.GraphicCard_PowerSupply(ref toast, configuration.GraphicCard, configuration.PowerSupply);

            _compatibilityPartsService.Case_Motherboard(ref toast, configuration.Case, configuration.Motherboard);
            _compatibilityPartsService.Case_PowerSupply(ref toast, configuration);
            _compatibilityPartsService.Case_GraphicCard(ref toast, configuration.Case, configuration.GraphicCard);
            _compatibilityPartsService.Case_CpuCooling(ref toast, configuration.Case, configuration.CpuCooling);
            _compatibilityPartsService.Case_Storages(ref toast, configuration);

            _compatibilityPartsService.Ram_CpuCooling(ref toast, configuration);
            _compatibilityPartsService.Ram_Motherboard(ref toast, configuration);

            _compatibilityPartsService.Storage_Motherboard(ref toast, configuration);
            _compatibilityPartsService.Storages_PowerSupply(ref toast, configuration);

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
            _compatibilityPartsService.Cpu_GraphicCard(ref toast, configuration); //3

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
            _compatibilityPartsService.Ram_Motherboard(ref toast, configuration); //5 i 6
            _compatibilityPartsService.Storage_Motherboard(ref toast, configuration); //7

            return toast;
        }

        public async Task<Toast> CpuCoolingCompatibilityCheck(PcConfiguration configuration)
        {
            await Task.Run(() => { });
            Toast toast = new();

            _compatibilityPartsService.Case_CpuCooling(ref toast, configuration.Case, configuration.CpuCooling); //2
            _compatibilityPartsService.Cpu_CpuCooling(ref toast, configuration.Cpu, configuration.CpuCooling);
            _compatibilityPartsService.Ram_CpuCooling(ref toast, configuration); //4

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
            _compatibilityPartsService.Ram_CpuCooling(ref toast, configuration);

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

            _compatibilityPartsService.Ram_Motherboard(ref toast, configuration); //1, 2 i 3
            _compatibilityPartsService.Ram_CpuCooling(ref toast, configuration); //4

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

            _compatibilityPartsService.Storage_Motherboard(ref toast, configuration); //1
            _compatibilityPartsService.Storages_PowerSupply(ref toast, configuration); //2

            return toast;
        }

        public async Task<Toast> GraphicCardCompatibilityCheck(PcConfiguration configuration)
        {
            await Task.Run(() => { });
            Toast toast = new();

            //"powerConnectors": "3x 8-pin",
            //"powerConnectors": "8-pin"
            //"powerConnectors": "6-pin",

            //"pciE8Pin_6Plus4": 4,
            //"pciE16Pin": 0,
            //"pciE8Pin": 0,
            //"pciE6Pin": 0,

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
            _compatibilityPartsService.Case_PowerSupply(ref toast, configuration);
            _compatibilityPartsService.Storages_PowerSupply(ref toast, configuration);

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
            _compatibilityPartsService.Case_PowerSupply(ref toast, configuration); //2 i 3
            _compatibilityPartsService.Case_GraphicCard(ref toast, configuration.Case, configuration.GraphicCard); //4
            _compatibilityPartsService.Case_CpuCooling(ref toast, configuration.Case, configuration.CpuCooling); //5
            _compatibilityPartsService.Case_WaterCooling(ref toast, configuration.Case, configuration.WaterCooling);
            _compatibilityPartsService.Case_Storages(ref toast, configuration); //7

            return toast;
        }

        //
        // Prywatne metody do sprawdzania kompatybilności części
        // PcPart1_PcPart2
        //

        
    }
}
