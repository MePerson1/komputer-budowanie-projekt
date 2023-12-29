using KomputerBudowanieAPI.Interfaces;
using KomputerBudowanieAPI.Models;
using KomputerBudowanieAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Services.CompatibilityServiceTests
{
    public class CpuAllCoolingCompatibilityTest
    {
        private readonly ICompatibilityPcConfigurationService _compatibilityPcConfigurationService = new CompatibilityPcConfigurationService(new CompatibilityPartsService());

        [Fact]
        public async void Given_Socket1700Cpu_CpuCooling_When_CompatibilityCheck_Then_Compatible()
        {
            // Arrange (Given)
            Cpu cpu = new()
            {
                Name = "Procesor Intel Core i7-10700F, 2.9 GHz, 16 MB, BOX (BX8070110700F)",
                Producer = "Intel",
                Line = "Core i7",
                SocketType = "Socket 1200"
            };
            CpuCooling cpuCooling = new()
            {
                Name = "Chłodzenie CPU be quiet! Dark Rock 4 (BK021)",
                ProcessorSocket = "1150/1151/1155/1156/1200, 1700, 2011/2011-3, 2066, AM3(+)/AM2(+)/FM2(+)/FM1, AM4/AM5"
            };

            PcConfiguration configuration = new()
            {
                Cpu = cpu,
                CpuCooling = cpuCooling
            };

            // Act (When)
            Toast? toast = await _compatibilityPcConfigurationService.CompatibilityCheck(configuration);

            // Assert (Then)
            Assert.NotNull(toast);
            Assert.Empty(toast.Problems);
        }

        [Fact]
        public async void Given_ThreadripperCpu_CpuCooling_When_CompatibilityCheck_Then_Compatible()
        {
            // Arrange (Given)
            Cpu cpu = new()
            {
                Name = "Procesor AMD Ryzen Threadripper Pro 5955WX, 4 GHz, 64 MB, OEM (100-000000447)",
                Producer = "AMD",
                Line = "Ryzen Threadripper",
                SocketType = "Socket sWRX8"
            };
            CpuCooling cpuCooling = new()
            {
                Name = "Chłodzenie CPU Arctic Freezer 4U-M (ACFRE00133A)",
                ProcessorSocket = "4189, 4677, SP6, sTR5, sTRX4, sWRX8, TR4/SP3"
            };

            PcConfiguration configuration = new()
            {
                Cpu = cpu,
                CpuCooling = cpuCooling
            };

            // Act (When)
            Toast? toast = await _compatibilityPcConfigurationService.CompatibilityCheck(configuration);

            // Assert (Then)
            Assert.NotNull(toast);
            Assert.Empty(toast.Problems);
        }

        [Fact]
        public async void Given_ThreadripperCpu_CpuCooling_When_CompatibilityCheck_Then_NotCompatible()
        {
            // Arrange (Given)
            Cpu cpu = new()
            {
                Name = "Procesor AMD Ryzen Threadripper Pro 5955WX, 4 GHz, 64 MB, OEM (100-000000447)",
                Producer = "AMD",
                Line = "Ryzen Threadripper",
                SocketType = "Socket sWRX8"
            };
            CpuCooling cpuCooling = new()
            {
                Name = "Chłodzenie CPU Arctic Freezer 34 eSports Duo 2x120mm (ACFRE00061A)",
                ProcessorSocket = "1150/1151/1155/1156/1200, 1700, 2011/2011-3, 2066, AM4/AM5"
            };

            PcConfiguration configuration = new()
            {
                Cpu = cpu,
                CpuCooling = cpuCooling
            };

            // Act (When)
            Toast? toast = await _compatibilityPcConfigurationService.CompatibilityCheck(configuration);

            // Assert (Then)
            Assert.NotNull(toast);
            Assert.Contains($"{configuration.CpuCooling.Name} nie wspiera gniazda procesora sWRX8!", toast.Problems);
        }

        [Fact]
        public async void Given_Intel1700Cpu_WaterCooling_When_CompatibilityCheck_Then_Compatible()
        {
            // Arrange (Given)
            Cpu cpu = new()
            {
                Name = "Procesor Intel Core i5-12400F, 2.5 GHz, 18 MB, BOX (BX8071512400F)",
                Producer = "Intel",
                Line = "Core i5",
                SocketType = "Socket 1700"
            };
            WaterCooling waterCooling = new()
            {
                Name = "Chłodzenie wodne Endorfy Navis F360 (EY3B003)",
                IntelCompatibility = "LGA 1150/1151/1155/1156/1200, LGA 1700, LGA 2011/2011-3, LGA 2066",
                AMDCompatibility = "AM3(+)/AM2(+)/FM2(+)/FM1, AM4/AM5"
            };

            PcConfiguration configuration = new()
            {
                Cpu = cpu,
                WaterCooling = waterCooling
            };

            // Act (When)
            Toast? toast = await _compatibilityPcConfigurationService.CompatibilityCheck(configuration);

            // Assert (Then)
            Assert.NotNull(toast);
            Assert.Empty(toast.Problems);
        }

        [Fact]
        public async void Given_ThreadripperCpu_WaterCooling_When_CompatibilityCheck_Then_NotCompatible()
        {
            // Arrange (Given)
            Cpu cpu = new()
            {
                Name = "Procesor AMD Ryzen Threadripper Pro 5955WX, 4 GHz, 64 MB, OEM (100-000000447)",
                Producer = "AMD",
                Line = "Ryzen Threadripper",
                SocketType = "Socket sWRX8"
            };
            WaterCooling waterCooling = new()
            {
                Name = "Chłodzenie wodne Endorfy Navis F360 (EY3B003)",
                IntelCompatibility = "LGA 1150/1151/1155/1156/1200, LGA 1700, LGA 2011/2011-3, LGA 2066",
                AMDCompatibility = "AM3(+)/AM2(+)/FM2(+)/FM1, AM4/AM5"
            };

            PcConfiguration configuration = new()
            {
                Cpu = cpu,
                WaterCooling = waterCooling
            };

            // Act (When)
            Toast? toast = await _compatibilityPcConfigurationService.CompatibilityCheck(configuration);

            // Assert (Then)
            Assert.NotNull(toast);
            Assert.Contains($"{waterCooling.Name} nie wspiera gniazda procesora sWRX8!", toast?.Problems);
        }
    }
}
