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
    public class MotherboardWaterCoolingCompatybilityTests
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

        private static readonly Motherboard motherboardswrx8 = new()
        {
            Name = "Płyta główna Asus PRO WS WRX80E-SAGE SE WIFI II",
            CPUSocket = "Socket sWRX8"
        };

        private static readonly WaterCooling waterCooling_intel_1200_1700_amd_am4am5 = new()
        {
            Name = "Chłodzenie wodne MSI MAG CoreLiquid M360 (306-7ZW4R24-813)",
            IntelCompatibility = "LGA 1150/1151/1155/1156/1200, LGA 1700, LGA 2011/2011-3, LGA 2066",
            AMDCompatibility = "AM3(+)/AM2(+)/FM2(+)/FM1, AM4/AM5"
        };

        private static readonly WaterCooling waterCooling_intel_1200_1700_amd_am4am5tr4 = new()
        {
            Name = "Chłodzenie wodne Endorfy Navis F360 (EY3B003)",
            IntelCompatibility = "LGA 2011/2011-3, LGA 2066, LGA 1150/1151/1155/1156/1200, LGA 1700",
            AMDCompatibility = "AM4/AM5, TR4"
        };

        private static readonly WaterCooling waterCooling_intel_1200_amd_am4am5 = new()
        {
            Name = "Chłodzenie wodne Cooler Master MasterLiquid Lite 240 (MLW-D24M-A20PW-R1)",
            IntelCompatibility = "LGA 775, LGA 1366, LGA 2011/2011-3, LGA 2066, LGA 1150/1151/1155/1156/1200",
            AMDCompatibility = "AM3(+)/AM2(+)/FM2(+)/FM1, AM4/AM5"
        };

        private static readonly WaterCooling waterCooling_intel_1200_amd_am4am5swrx8 = new()
        {
            Name = "Chłodzenie wodne Cooler Master MasterLiquid Lite 240 (MLW-D24M-A20PW-R1)",
            IntelCompatibility = "LGA 775, LGA 1366, LGA 2011/2011-3, LGA 2066, LGA 1150/1151/1155/1156/1200",
            AMDCompatibility = "AM4/AM5, TR4, sTRX4, SP3, sWRX8"
        };

        private static readonly WaterCooling waterCooling_dummy1 = new()
        {
            Name = "dummy1",
            IntelCompatibility = "LGA 775, LGA 1366, LGA 2011/2011-3, LGA 2066, LGA 1150/1151/1155/1156",
            AMDCompatibility = "TR4, sTRX4, SP3, sWRX8"
        };

        public static IEnumerable<object[]> MotherboardAndWaterCoolingIncompatibleData()
        {
            yield return new object[]
            {
                motherboardswrx8,
                waterCooling_intel_1200_amd_am4am5,
                new List<string>
                {
                    $"{waterCooling_intel_1200_amd_am4am5.Name} nie wspiera gniazda procesora sWRX8 z płyty głownej!"
                }
            };

            yield return new object[]
            {
                motherboardswrx8,
                waterCooling_intel_1200_1700_amd_am4am5tr4,
                new List<string>
                {
                    $"{waterCooling_intel_1200_1700_amd_am4am5tr4.Name} nie wspiera gniazda procesora sWRX8 z płyty głownej!"
                }
            };

            yield return new object[]
            {
                motherboard1700,
                waterCooling_intel_1200_amd_am4am5,
                new List<string>
                {
                    $"{waterCooling_intel_1200_amd_am4am5.Name} nie wspiera gniazda procesora 1700 z płyty głownej!"
                }
            };

            yield return new object[]
            {
                motherboard1700,
                waterCooling_dummy1,
                new List<string>
                {
                    $"{waterCooling_dummy1.Name} nie wspiera gniazda procesora 1700 z płyty głownej!"
                }
            };

            yield return new object[]
            {
                motherboard1200,
                waterCooling_dummy1,
                new List<string>
                {
                    $"{waterCooling_dummy1.Name} nie wspiera gniazda procesora 1200 z płyty głownej!"
                }
            };

            yield return new object[]
            {
                motherboardAm4,
                waterCooling_dummy1,
                new List<string>
                {
                    $"{waterCooling_dummy1.Name} nie wspiera gniazda procesora AM4 z płyty głownej!"
                }
            };
        }

        public static IEnumerable<object[]> MotherboardAndWaterCoolingCompatibleData()
        {
            yield return new object[]
            {
                motherboardswrx8,
                waterCooling_intel_1200_amd_am4am5swrx8
            };

            yield return new object[]
            {
                motherboardAm4,
                waterCooling_intel_1200_1700_amd_am4am5
            };

            yield return new object[]
            {
                motherboardAm4,
                waterCooling_intel_1200_amd_am4am5
            };

            yield return new object[]
            {
                motherboard1700,
                waterCooling_intel_1200_1700_amd_am4am5
            };

            yield return new object[]
            {
                motherboard1200,
                waterCooling_intel_1200_1700_amd_am4am5
            };

            yield return new object[]
            {
                motherboard1200,
                waterCooling_intel_1200_amd_am4am5
            };
        }

        [Theory]
        [MemberData(nameof(MotherboardAndWaterCoolingIncompatibleData))]
        public async void Given_MotherboardAndWaterCooling_When_CompatibilityCheck_Then_NotCompatible(Motherboard motherboard, WaterCooling waterCooling, List<string> problems)
        {
            // Arrange (Given)
            PcConfiguration configuration = new PcConfiguration
            {
                Motherboard = motherboard,
                WaterCooling = waterCooling,
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
        [MemberData(nameof(MotherboardAndWaterCoolingCompatibleData))]
        public async void Given_MotherboardAndWaterCooling_When_CompatibilityCheck_Then_Compatible(Motherboard motherboard, WaterCooling waterCooling)
        {
            // Arrange (Given)
            PcConfiguration configuration = new PcConfiguration
            {
                Motherboard = motherboard,
                WaterCooling = waterCooling,
            };

            // Act (When)
            Toast? toast = await _compatibilityPcConfigurationService.CompatibilityCheck(configuration);

            // Assert (Then)
            Assert.NotNull(toast);
            Assert.Empty(toast.Problems);
        }
    }
}
