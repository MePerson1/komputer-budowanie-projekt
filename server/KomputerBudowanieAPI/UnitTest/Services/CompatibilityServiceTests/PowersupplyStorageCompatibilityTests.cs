using KomputerBudowanieAPI.Models;
using KomputerBudowanieAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Services.CompatibilityServiceTests
{
    public class PowersupplyStorageCompatibilityTests
    {
        private readonly CompatibilityService _compatibilityService = new CompatibilityService();

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

        private static readonly PowerSupply powersupply3xSata4xMolex500W = new()
        {
            Name = "Zasilacz iBOX CUBE II 500W (ZIC2500W12CMFA)",
            Sata = 3,
            Molex = 4,
            PowerW = 500
        };

        public static IEnumerable<object[]> PowersupplyAndStorageIncompatibleData()
        {
            yield return new object[]
            {
                powersupply3xSata4xMolex500W,
                new List<PcConfigurationStorage>
                {
                    new PcConfigurationStorage()
                    {
                        Storage = discHddStata3Form2_5,
                        Quantity = 4
                    }
                },
                new List<string>
                {
                    "Niewystarczająca ilość złączy zasilania dla dysków!"
                }
            };

            yield return new object[]
            {
                powersupply3xSata4xMolex500W,
                new List<PcConfigurationStorage>
                {
                    new PcConfigurationStorage()
                    {
                        Storage = discSsdStata3Form2_5,
                        Quantity = 2
                    },
                    new PcConfigurationStorage()
                    {
                        Storage = discHddSata3Form3_5,
                        Quantity = 2
                    },
                },
                new List<string>
                {
                    "Niewystarczająca ilość złączy zasilania dla dysków!"
                }
            };

            yield return new object[]
            {
                powersupply3xSata4xMolex500W,
                new List<PcConfigurationStorage>
                {
                    new PcConfigurationStorage()
                    {
                        Storage = discHddStata3Form2_5,
                        Quantity = 1
                    },
                    new PcConfigurationStorage()
                    {
                        Storage = discHddSata3Form3_5,
                        Quantity = 2
                    },
                    new PcConfigurationStorage()
                    {
                        Storage = discSsdStata3Form2_5,
                        Quantity = 1
                    }
                },
                new List<string>
                {
                    "Niewystarczająca ilość złączy zasilania dla dysków!"
                }
            };
        }

        public static IEnumerable<object[]> PowersupplyAndStorageCompatibleData()
        {
            yield return new object[]
            {
                powersupply3xSata4xMolex500W,
                new List<PcConfigurationStorage>
                {
                    new PcConfigurationStorage()
                    {
                        Storage = discHddStata3Form2_5,
                        Quantity = 1
                    }
                }
            };

            yield return new object[]
            {
                powersupply3xSata4xMolex500W,
                new List<PcConfigurationStorage>
                {
                    new PcConfigurationStorage()
                    {
                        Storage = discHddStata3Form2_5,
                        Quantity = 3
                    }
                }
            };

            yield return new object[]
            {
                powersupply3xSata4xMolex500W,
                new List<PcConfigurationStorage>
                {
                    new PcConfigurationStorage()
                    {
                        Storage = discHddStata3Form2_5,
                        Quantity = 2
                    },
                    new PcConfigurationStorage()
                    {
                        Storage = discHddSata3Form3_5,
                        Quantity = 1
                    }
                }
            };

            yield return new object[]
            {
                powersupply3xSata4xMolex500W,
                new List<PcConfigurationStorage>
                {
                    new PcConfigurationStorage()
                    {
                        Storage = discHddStata3Form2_5,
                        Quantity = 1
                    },
                    new PcConfigurationStorage()
                    {
                        Storage = discHddSata3Form3_5,
                        Quantity = 1
                    },
                    new PcConfigurationStorage()
                    {
                        Storage = discSsdStata3Form2_5,
                        Quantity = 1
                    }
                }
            };
        }

        [Theory]
        [MemberData(nameof(PowersupplyAndStorageIncompatibleData))]
        public async void Given_PowersupplyAndStorage_When_CompatibilityCheck_Then_NotCompatible(PowerSupply powersupply, List<PcConfigurationStorage> storageList, List<string> problems)
        {
            // Arrange (Given)
            PcConfiguration configuration = new PcConfiguration
            {
                PowerSupply = powersupply,
                PcConfigurationStorages = storageList,
            };

            // Act (When)
            Toast? toast = await _compatibilityService.CompatibilityCheck(configuration);

            // Assert (Then)
            Assert.NotNull(toast);
            foreach (var problem in problems)
            {
                Assert.Contains(problem, toast.Problems);
            }
        }

        [Theory]
        [MemberData(nameof(PowersupplyAndStorageCompatibleData))]
        public async void Given_PowersupplyAndStorage_When_CompatibilityCheck_Then_Compatible(PowerSupply powersupply, List<PcConfigurationStorage> storageList)
        {
            // Arrange (Given)
            PcConfiguration configuration = new PcConfiguration
            {
                PowerSupply = powersupply,
                PcConfigurationStorages = storageList,
            };

            // Act (When)
            Toast? toast = await _compatibilityService.CompatibilityCheck(configuration);

            // Assert (Then)
            Assert.NotNull(toast);
            Assert.Empty(toast.Problems);
        }
    }
}
