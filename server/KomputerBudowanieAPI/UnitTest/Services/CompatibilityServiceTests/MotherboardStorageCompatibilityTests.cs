using KomputerBudowanieAPI.Interfaces;
using KomputerBudowanieAPI.Models;
using KomputerBudowanieAPI.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Services.CompatibilityServiceTests
{
    public class MotherboardStorageCompatibilityTests
    {
        private readonly ICompatibilityPcConfigurationService _compatibilityPcConfigurationService = new CompatibilityPcConfigurationService(new CompatibilityPartsService());

        private static readonly Storage discHddSata3Form3_5 = new()
        {
            Name = "Dysk Toshiba P300 3TB 3.5 cala SATA III (HDWD130UZSVA)",
            Interface = "SATA 3",
            Type = "HDD",
            FormFactor = "3.5 cala",
            Capacity = "3TB"
        };

        private static readonly Storage discHddStata3Form2_5 = new()
        {
            Name = "Dysk Seagate BarraCuda 2TB 2.5 cala SATA III (ST2000LM015)",
            Interface = "SATA 3",
            Type = "HDD",
            FormFactor = "2.5 cala",
            Capacity = "2TB"
        };

        private static readonly Storage discSsdStata3Form2_5 = new()
        {
            Name = "Dysk SSD Gigabyte 256GB 2.5 cala SATA III (GP-GSTFS31256GTND)",
            Interface = "SATA 3",
            Type = "SSD",
            FormFactor = "2.5 cala",
            Capacity = "256GB"
        };

        private static readonly Storage discSsdPciex4FormM2 = new()
        {
            Name = "Dysk SSD Lexar NM620 1TB M.2 2280 PCI-E x4 Gen3 NVMe (LNM620X001T-RNNNG)",
            Interface = "PC1-E x4 Gen3 NVMe",
            Type = "SSD",
            FormFactor = "M.2 2280",
            Capacity = "1TB"
        };

        private static readonly Motherboard motherboard4xSata3 = new()
        {
            Name = "Płyta główna MSI B450M PRO-VDH MAX",
            DriveConnectors = "SATA 3 x4",
        };

        private static readonly Motherboard motherboard2xM2_4xSata3 = new()
        {
            Name = "Płyta główna Gigabyte B550 AORUS ELITE V2",
            DriveConnectors = "M.2 slot x2, SATA 3 x4",
        };

        public static IEnumerable<object[]> MotherboardAndStorageIncompatibleData()
        {
            yield return new object[]
            {
                motherboard4xSata3,
                new List<PcConfigurationStorage>
                {
                    new PcConfigurationStorage()
                    {
                        Storage = discHddStata3Form2_5,
                        Quantity = 5
                    }
                },
                new List<string>
                {
                    $"Niewystarczająca ilość złączy {discHddStata3Form2_5.Interface}!"
                }
            };

            yield return new object[]
            {
                motherboard2xM2_4xSata3,
                new List<PcConfigurationStorage>
                {
                    new PcConfigurationStorage()
                    {
                        Storage = discSsdPciex4FormM2,
                        Quantity = 3
                    }
                },
                new List<string>
                {
                    "Niewystarczająca ilość złączy M2!"
                }
            };

            yield return new object[]
            {
                motherboard4xSata3,
                new List<PcConfigurationStorage>
                {
                    new PcConfigurationStorage()
                    {
                        Storage = discSsdPciex4FormM2,
                        Quantity = 1
                    }
                },
                new List<string>
                {
                    $"Płyta {motherboard4xSata3.Name} nie ma żadnego złącza {discSsdPciex4FormM2.Interface} dla dysku {discSsdPciex4FormM2.Name}!"
                }
            };
        }

        public static IEnumerable<object[]> MotherboardAndStorageCompatibleData()
        {
            yield return new object[]
            {
                motherboard4xSata3,
                new List<PcConfigurationStorage>
                {
                    new PcConfigurationStorage()
                    {
                        Storage = discHddSata3Form3_5,
                        Quantity = 1
                    }
                }
            };

            yield return new object[]
            {
                motherboard4xSata3,
                new List<PcConfigurationStorage>
                {
                    new PcConfigurationStorage()
                    {
                        Storage = discHddSata3Form3_5,
                        Quantity = 4
                    }
                }
            };

            yield return new object[]
            {
                motherboard2xM2_4xSata3,
                new List<PcConfigurationStorage>
                {
                    new PcConfigurationStorage()
                    {
                        Storage = discSsdPciex4FormM2,
                        Quantity = 1
                    }
                }
            };

            yield return new object[]
            {
                motherboard2xM2_4xSata3,
                new List<PcConfigurationStorage>
                {
                    new PcConfigurationStorage()
                    {
                        Storage = discSsdPciex4FormM2,
                        Quantity = 2
                    }
                }
            };

            yield return new object[]
            {
                motherboard2xM2_4xSata3,
                new List<PcConfigurationStorage>
                {
                    new PcConfigurationStorage()
                    {
                        Storage = discSsdPciex4FormM2,
                        Quantity = 2
                    },
                    new PcConfigurationStorage()
                    {
                        Storage = discHddSata3Form3_5,
                        Quantity = 4
                    }
                }
            };
        }

        [Theory]
        [MemberData(nameof(MotherboardAndStorageIncompatibleData))]
        public async void Given_MotherboardAndStorage_When_CompatibilityCheck_Then_NotCompatible(Motherboard motherboard, List<PcConfigurationStorage> storageList, List<string> problems)
        {
            // Arrange (Given)
            PcConfiguration configuration = new PcConfiguration
            {
                Motherboard = motherboard,
                PcConfigurationStorages = storageList,
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
        [MemberData(nameof(MotherboardAndStorageCompatibleData))]
        public async void Given_MotherboardAndStorage_When_CompatibilityCheck_Then_Compatible(Motherboard motherboard, List<PcConfigurationStorage> storageList)
        {
            // Arrange (Given)
            PcConfiguration configuration = new PcConfiguration
            {
                Motherboard = motherboard,
                PcConfigurationStorages = storageList,
            };

            // Act (When)
            Toast? toast = await _compatibilityPcConfigurationService.CompatibilityCheck(configuration);

            // Assert (Then)
            Assert.NotNull(toast);
            Assert.Empty(toast.Problems);
        }
    }
}
