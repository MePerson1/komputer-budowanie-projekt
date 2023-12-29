using KomputerBudowanieAPI.Interfaces;
using KomputerBudowanieAPI.Models;
using KomputerBudowanieAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Services.CompatibilityServiceTests
{
    public class MotherboardCpuCoolingCompatybilityTests
    {
        private readonly ICompatibilityPcConfigurationService _compatibilityPcConfigurationService = new CompatibilityPcConfigurationService(new CompatibilityPartsService());

        private static readonly Motherboard motherboardAm4 = new()
        {
            Name = "Płyta główna Gigabyte B550 AORUS ELITE V2",
            CPUSocket = "Socket AM4"
        };

        private static readonly Motherboard motherboard1700 = new()
        {
            Name = "Płyta główna Asus PRIME B660-PLUS D4",
            CPUSocket = "Socket 1700"
        };

        private static readonly Motherboard motherboard1200 = new()
        {
            Name = "Płyta główna Asus PRIME H510M-K",
            CPUSocket = "Socket 1200"
        };

        private static readonly CpuCooling cpuCooling_intel_1200_1700_amd_am4am5 = new()
        {
            Name = "Chłodzenie CPU Endorfy Fera 5 Dual Fan (EY3A006)",
            ProcessorSocket = "1150/1151/1155/1156/1200, 1366, 1700, 2011/2011-3, 2066, 775, AM3(+)/AM2(+)/FM2(+)/FM1, AM4/AM5"
        };

        private static readonly CpuCooling cpuCooling_intel_1200_amd_am3 = new()
        {
            Name = "Chłodzenie CPU be quiet! Pure Rock Slim 2 (BK030)",
            ProcessorSocket = "1150/1151/1155/1156/1200, AM3(+)/AM2(+)/FM2(+)/FM1"
        };

        private static readonly CpuCooling cpuCooling_amd_threadripper = new()
        {
            Name = "Chłodzenie CPU Arctic Freezer 4U-M (ACFRE00133A)",
            ProcessorSocket = "4189, 4677, SP6, sTR5, sTRX4, sWRX8, TR4/SP3"
        };

        public static IEnumerable<object[]> MotherboardAndCpuCoolingIncompatibleData()
        {
            yield return new object[]
            {
                motherboardAm4,
                cpuCooling_amd_threadripper,
                new List<string>
                {
                    $"{cpuCooling_amd_threadripper.Name} nie wspiera gniazda procesora AM4 z płyty głownej!"
                }
            };

            yield return new object[]
            {
                motherboard1700,
                cpuCooling_intel_1200_amd_am3,
                new List<string>
                {
                    $"{cpuCooling_intel_1200_amd_am3.Name} nie wspiera gniazda procesora 1700 z płyty głownej!"
                }
            };
        }

        public static IEnumerable<object[]> MotherboardAndCpuCoolingCompatibleData()
        {
            yield return new object[]
            {
                motherboardAm4,
                cpuCooling_intel_1200_1700_amd_am4am5
            };

            yield return new object[]
            {
                motherboard1700,
                cpuCooling_intel_1200_1700_amd_am4am5
            };

            yield return new object[]
            {
                motherboard1200,
                cpuCooling_intel_1200_amd_am3
            };
        }

        [Theory]
        [MemberData(nameof(MotherboardAndCpuCoolingIncompatibleData))]
        public async void Given_MotherboardAndCpuCooling_When_CompatibilityCheck_Then_NotCompatible(Motherboard motherboard, CpuCooling cpuCooling, List<string> problems)
        {
            // Arrange (Given)
            PcConfiguration configuration = new PcConfiguration
            {
                Motherboard = motherboard,
                CpuCooling = cpuCooling,
            };

            // Act (When)
            Toast? toast = await _compatibilityPcConfigurationService.CompatibilityCheck(configuration);

            // Assert (Then)
            Assert.NotNull(toast);
            foreach (var problem in problems)
            {
                Assert.Contains(problem, toast.Problems);
            }
        }

        [Theory]
        [MemberData(nameof(MotherboardAndCpuCoolingCompatibleData))]
        public async void Given_MotherboardAndCpuCooling_When_CompatibilityCheck_Then_Compatible(Motherboard motherboard, CpuCooling cpuCooling)
        {
            // Arrange (Given)
            PcConfiguration configuration = new PcConfiguration
            {
                Motherboard = motherboard,
                CpuCooling = cpuCooling,
            };

            // Act (When)
            Toast? toast = await _compatibilityPcConfigurationService.CompatibilityCheck(configuration);

            // Assert (Then)
            Assert.NotNull(toast);
            Assert.Empty(toast.Problems);
        }
    }
}
