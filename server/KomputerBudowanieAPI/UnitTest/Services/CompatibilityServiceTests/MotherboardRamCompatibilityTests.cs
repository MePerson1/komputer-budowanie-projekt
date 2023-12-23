using AutoMapper.Configuration.Annotations;
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
    public class MotherboardRamCompatibilityTests
    {
        private readonly ICompatibilityPcConfigurationService _compatibilityPcConfigurationService = new CompatibilityPcConfigurationService(new CompatibilityPartsService());

        private static readonly Ram ram1xUdimmDdr3 = new()
        {
            Name = "Pamięć GoodRam DDR3, 8 GB, 1333MHz, CL9 (GR1333D364L9/8G)",
            MemoryType = "DDR3",
            PinType = "UDIMM",
            ModuleCount = 1,
        };

        private static readonly Ram ram1xUdimmDdr4 = new()
        {
            Name = "Pamięć Lexar DDR4, 16 GB, 3200MHz, CL22 (LD4AU016G-B3200GSST)",
            MemoryType = "DDR4",
            PinType = "UDIMM",
            ModuleCount = 1,
        };

        private static readonly Ram ram1xUdimmDdr5 = new()
        {
            Name = "Pamięć Corsair Vengeance RGB, DDR5, 32 GB, 6000MHz, CL30 (CMH32GX5M2B6000Z30K)",
            MemoryType = "DDR5",
            PinType = "UDIMM",
            ModuleCount = 1,
        };

        private static readonly Ram ram2xUdimmDdr4 = new()
        {
            Name = "Pamięć G.Skill Trident Z RGB, DDR4, 16 GB, 4133MHz, CL17 (F4-4133C17D-16GTZR)",
            MemoryType = "DDR4",
            PinType = "UDIMM",
            ModuleCount = 2,
        };

        private static readonly Ram ram2xUdimmDdr5 = new()
        {
            Name = "Pamięć G.Skill Trident Z5 RGB, DDR5, 32 GB, 5600MHz, CL36 (F5-5600J3636C16GX2-TZ5RK)",
            MemoryType = "DDR5",
            PinType = "UDIMM",
            ModuleCount = 2,
        };

        private static readonly Ram ram2xDimmDdr4 = new()
        {
            Name = "Pamięć Gigabyte AORUS RGB, DDR4, 16 GB, 3333MHz, CL18 (GP-ARS16G33)",
            MemoryType = "DDR4",
            PinType = "DIMM",
            ModuleCount = 2,
        };

        private static readonly Ram ram2xDimmDdr3 = new()
        {
            Name = "Pamięć G.Skill RipjawsX, DDR3, 8 GB, 2133MHz, CL9 (F3-17000CL9D-8GBXM)",
            MemoryType = "DDR3",
            PinType = "DIMM",
            ModuleCount = 2,
        };

        private static readonly Motherboard motherboard4xSlotDdr4Udimm = new()
        {
            Name = "Płyta główna MSI B450M PRO-VDH MAX",
            MemoryConnectorType = "UDIMM",
            MemoryStandard = "DDR4",
            MemorySlotsCount = 4,
        };

        private static readonly Motherboard motherboard4xSlotDdr5Udimm = new()
        {
            Name = "Płyta główna ASRock B650M PRO RS WIFI",
            MemoryConnectorType = "UDIMM",
            MemoryStandard = "DDR5",
            MemorySlotsCount = 4,
        };

        public static IEnumerable<object[]> MotherboardAndRamIncompatibleData()
        {
            yield return new object[]
            {
                motherboard4xSlotDdr4Udimm,
                new List<Ram>
                {
                    ram1xUdimmDdr4,
                    ram1xUdimmDdr4,
                    ram1xUdimmDdr4,
                    ram1xUdimmDdr4,
                    ram1xUdimmDdr4
                },
                new List<string>
                {
                    "Płyta głowna nie pomieści więcej niż 4 kości ram!"
                }
            };

            yield return new object[]
            {
                motherboard4xSlotDdr4Udimm,
                new List<Ram>
                {
                    ram1xUdimmDdr4,
                    ram2xUdimmDdr4,
                    ram2xUdimmDdr4,
                },
                new List<string>
                {
                    "Płyta głowna nie pomieści więcej niż 4 kości ram!"
                }
            };

            yield return new object[]
            {
                motherboard4xSlotDdr5Udimm,
                new List<Ram>
                {
                    ram1xUdimmDdr4
                },
                new List<string>
                {
                    "Płyta główna nie wspiera tego standardu pamięci ram! " + ram1xUdimmDdr4.Name
                }
            };

            yield return new object[]
            {
                motherboard4xSlotDdr4Udimm,
                new List<Ram>
                {
                    ram1xUdimmDdr3
                },
                new List<string>
                {
                    "Płyta główna nie wspiera tego standardu pamięci ram! " + ram1xUdimmDdr3.Name
                }
            };
        }

        public static IEnumerable<object[]> MotherboardAndRamCompatibleData()
        {
            yield return new object[]
            {
                motherboard4xSlotDdr4Udimm,
                new List<Ram>
                {
                    ram1xUdimmDdr4,
                }
            };

            yield return new object[]
            {
                motherboard4xSlotDdr4Udimm,
                new List<Ram>
                {
                    ram1xUdimmDdr4,
                    ram1xUdimmDdr4,
                    ram1xUdimmDdr4,
                    ram1xUdimmDdr4,
                }
            };

            yield return new object[]
            {
                motherboard4xSlotDdr4Udimm,
                new List<Ram>
                {
                    ram2xUdimmDdr4,
                    ram2xUdimmDdr4,
                }
            };

            yield return new object[]
            {
                motherboard4xSlotDdr4Udimm,
                new List<Ram>
                {
                    ram2xUdimmDdr4,
                    ram1xUdimmDdr4,
                    ram1xUdimmDdr4,
                }
            };

            yield return new object[]
            {
                motherboard4xSlotDdr5Udimm,
                new List<Ram>
                {
                    ram1xUdimmDdr5
                }
            };

            yield return new object[]
            {
                motherboard4xSlotDdr5Udimm,
                new List<Ram>
                {
                    ram1xUdimmDdr5,
                    ram1xUdimmDdr5,
                    ram1xUdimmDdr5,
                    ram1xUdimmDdr5,
                }
            };

            yield return new object[]
            {
                motherboard4xSlotDdr5Udimm,
                new List<Ram>
                {
                    ram2xUdimmDdr5,
                    ram2xUdimmDdr5,
                }
            };
        }

        [Theory]
        [MemberData(nameof(MotherboardAndRamIncompatibleData))]
        public async void Given_MotherboardAndRams_When_CompatibilityCheck_Then_NotCompatible(Motherboard motherboard, List<Ram> ramList, List<string> problems)
        {
            // Arrange (Given)
            List<PcConfigurationRam> rams = makePcConfigurationRamList(ramList);

            PcConfiguration configuration = new PcConfiguration
            {
                Motherboard = motherboard,
                PcConfigurationRams = rams
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
        [MemberData(nameof(MotherboardAndRamCompatibleData))]
        public async void Given_MotherboardAndRams_When_CompatibilityCheck_Then_Compatible(Motherboard motherboard, List<Ram> ramList)
        {
            // Arrange (Given)
            List<PcConfigurationRam> rams = makePcConfigurationRamList(ramList);

            PcConfiguration configuration = new PcConfiguration
            {
                Motherboard = motherboard,
                PcConfigurationRams = rams
            };

            // Act (When)
            Toast? toast = await _compatibilityPcConfigurationService.CompatibilityCheck(configuration);

            // Assert (Then)
            Assert.NotNull(toast);
            Assert.Empty(toast.Problems);
        }

        public static List<PcConfigurationRam> makePcConfigurationRamList(List<Ram> ramList)
        {
            List<PcConfigurationRam> pcConfigurationRamList = new List<PcConfigurationRam>();
            foreach (Ram ram in ramList) 
            {
                PcConfigurationRam pcConfigurationRam = new PcConfigurationRam()
                {
                    RamsId = ram.Id,
                    Ram = ram,
                    Quantity = 1
                };
                pcConfigurationRamList.Add(pcConfigurationRam);
            }
            return pcConfigurationRamList;
        }
    }
}
