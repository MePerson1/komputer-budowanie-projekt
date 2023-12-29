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
    public class PowersupplyGraphiccardCompatibilityTests
    {
        private readonly ICompatibilityPcConfigurationService _compatibilityPcConfigurationService = new CompatibilityPcConfigurationService(new CompatibilityPartsService());

        private static readonly PowerSupply powersupply_6x0_8x0_6plus8x0_16x0_400w = new()
        {
            Name = "Zasilacz Bandit HX-380 400W",
            PCIE6Pin = 0,
            PCIE8Pin = 0,
            PCIE8Pin_6Plus2 = 0,
            PCIE16Pin = 0,
            PowerW = 400
        };

        private static readonly PowerSupply powersupply_6x0_8x0_6plus8x2_16x0_600w = new()
        {
            Name = "Zasilacz Thermaltake Smart SE2 600 (PS-SPS-0600MNSAWE-1)",
            PCIE6Pin = 0,
            PCIE8Pin = 0,
            PCIE8Pin_6Plus2 = 2,
            PCIE16Pin = 0,
            PowerW = 600
        };

        private static readonly PowerSupply powersupply_6x2_8x2_6plus8x0_16x0_800w = new()
        {
            Name = "Dummy 1",
            PCIE6Pin = 2,
            PCIE8Pin = 2,
            PCIE8Pin_6Plus2 = 0,
            PCIE16Pin = 0,
            PowerW = 800
        };

        private static readonly PowerSupply powersupply_6x0_8x0_6plus8x4_16x1_1000w = new()
        {
            Name = "Dummy 2",
            PCIE6Pin = 0,
            PCIE8Pin = 0,
            PCIE8Pin_6Plus2 = 4,
            PCIE16Pin = 1,
            PowerW = 1000
        };

        private static readonly PowerSupply powersupply_6x0_8x2_6plus8x0_16x0_500w = new()
        {
            Name = "Dummy 3",
            PCIE6Pin = 0,
            PCIE8Pin = 2,
            PCIE8Pin_6Plus2 = 0,
            PCIE16Pin = 0,
            PowerW = 500
        };

        private static readonly GraphicCard graphicCard1x16pin_1000w = new()
        {
            Name = "Karta graficzna Gigabyte GeForce RTX 4090 Gaming OC 24 GB GDDR6X (GV-N4090GAMING OC-24GD)",
            PowerConnectors = "16-pin",
            RecommendedPSUCapacityW = 1000,
        };

        private static readonly GraphicCard graphiccard1x8pin_550w = new()
        {
            Name = "Karta graficzna Gigabyte GeForce RTX 3060 Eagle OC 12GB GDDR6 (GV-N3060EAGLE OC-12GD 2.0)",
            PowerConnectors = "8-pin",
            RecommendedPSUCapacityW = 550,
        };

        private static readonly GraphicCard graphiccard1x6pin_350w = new()
        {
            Name = "Dummy 6-pin",
            PowerConnectors = "6-pin",
            RecommendedPSUCapacityW = 350,
        };

        private static readonly GraphicCard graphiccard2x8pin_500w = new()
        {
            Name = "Dummy 2x 8-pin",
            PowerConnectors = "2x 8-pin",
            RecommendedPSUCapacityW = 500,
        };

        private static readonly GraphicCard graphiccard2x8pin_700w = new()
        {
            Name = "Dummy 2x 8-pin",
            PowerConnectors = "2x 8-pin",
            RecommendedPSUCapacityW = 700,
        };

        private static readonly GraphicCard graphiccard2x6pin_400w = new()
        {
            Name = "Dummy 2x 6-pin",
            PowerConnectors = "2x 6-pin",
            RecommendedPSUCapacityW = 400,
        };

        private static readonly GraphicCard graphiccard4x8pin_850w = new()
        {
            Name = "Dummy 4x 8-pin",
            PowerConnectors = "4x 8-pin",
            RecommendedPSUCapacityW = 850,
        };

        private static readonly GraphicCard graphiccard1x6pin1x8pin_500w = new()
        {
            Name = "Dummy 6-pin + 8-pin",
            PowerConnectors = "6-pin + 8-pin",
            RecommendedPSUCapacityW = 500
        };

        private static readonly GraphicCard graphiccard2x6pin2x8pin_650w = new()
        {
            Name = "Dummy 2x 6-pin + 2x 8-pin",
            PowerConnectors = "2x 6-pin + 2x 8-pin",
            RecommendedPSUCapacityW = 650
        };

        public static IEnumerable<object[]> PowerSupplyAndGraphicCardIncompatibleData()
        {
            yield return new object[]
            {
                powersupply_6x0_8x0_6plus8x0_16x0_400w,
                graphicCard1x16pin_1000w,
                new List<string>
                {
                    "Niewystarczająca ilość 16 pinowych złączy do karty graficznej!",
                    $"Zalecany jest zasilacz do karty graficznej o mocy minimum: {graphicCard1x16pin_1000w.RecommendedPSUCapacityW} W!"
                }
            };

            yield return new object[]
            {
                powersupply_6x0_8x0_6plus8x0_16x0_400w,
                graphiccard1x6pin1x8pin_500w,
                new List<string>
                {
                    "Niewystarczająca ilość 8 pinowych złączy do karty graficznej!",
                    $"Zalecany jest zasilacz do karty graficznej o mocy minimum: {graphiccard1x6pin1x8pin_500w.RecommendedPSUCapacityW} W!"
                }
            };

            yield return new object[]
            {
                powersupply_6x0_8x0_6plus8x0_16x0_400w,
                graphiccard1x6pin_350w,
                new List<string>
                {
                    "Niewystarczająca ilość 6 pinowych złączy do karty graficznej!"
                }
            };

            yield return new object[]
            {
                powersupply_6x0_8x0_6plus8x0_16x0_400w,
                graphiccard4x8pin_850w,
                new List<string>
                {
                    "Niewystarczająca ilość 8 pinowych złączy do karty graficznej!",
                    $"Zalecany jest zasilacz do karty graficznej o mocy minimum: {graphiccard4x8pin_850w.RecommendedPSUCapacityW} W!"
                }
            };

            yield return new object[]
            {
                powersupply_6x0_8x0_6plus8x0_16x0_400w,
                graphiccard1x6pin_350w,
                new List<string>
                {
                    "Niewystarczająca ilość 6 pinowych złączy do karty graficznej!"
                }
            };

            yield return new object[]
            {
                powersupply_6x2_8x2_6plus8x0_16x0_800w,
                graphiccard4x8pin_850w,
                new List<string>
                {
                    "Niewystarczająca ilość 8 pinowych złączy do karty graficznej!",
                    $"Zalecany jest zasilacz do karty graficznej o mocy minimum: {graphiccard4x8pin_850w.RecommendedPSUCapacityW} W!"
                }
            };

            yield return new object[]
            {
                powersupply_6x0_8x2_6plus8x0_16x0_500w,
                graphiccard4x8pin_850w,
                new List<string>
                {
                    "Niewystarczająca ilość 8 pinowych złączy do karty graficznej!",
                    $"Zalecany jest zasilacz do karty graficznej o mocy minimum: {graphiccard4x8pin_850w.RecommendedPSUCapacityW} W!"
                }
            };

            yield return new object[]
            {
                powersupply_6x0_8x2_6plus8x0_16x0_500w,
                graphiccard2x6pin_400w,
                new List<string>
                {
                    "Niewystarczająca ilość 6 pinowych złączy do karty graficznej!"
                }
            };

            yield return new object[]
            {
                powersupply_6x0_8x0_6plus8x2_16x0_600w,
                graphiccard2x8pin_700w,
                new List<string>
                {
                    $"Zalecany jest zasilacz do karty graficznej o mocy minimum: {graphiccard2x8pin_700w.RecommendedPSUCapacityW} W!"
                }
            };

            yield return new object[]
            {
                powersupply_6x0_8x0_6plus8x0_16x0_400w,
                graphiccard1x6pin1x8pin_500w,
                new List<string>
                {
                    "Niewystarczająca ilość 8 pinowych złączy do karty graficznej!",
                    "Niewystarczająca ilość 6 pinowych złączy do karty graficznej!",
                    $"Zalecany jest zasilacz do karty graficznej o mocy minimum: {graphiccard1x6pin1x8pin_500w.RecommendedPSUCapacityW} W!"
                }
            };
        }

        public static IEnumerable<object[]> PowerSupplyAndGraphicCardCompatibleData()
        {
            yield return new object[]
            {
                powersupply_6x0_8x0_6plus8x2_16x0_600w,
                graphiccard1x6pin1x8pin_500w
            };

            yield return new object[]
            {
                powersupply_6x0_8x0_6plus8x4_16x1_1000w,
                graphicCard1x16pin_1000w
            };

            yield return new object[]
            {
                powersupply_6x0_8x0_6plus8x4_16x1_1000w,
                graphiccard4x8pin_850w
            };

            yield return new object[]
            {
                powersupply_6x0_8x0_6plus8x4_16x1_1000w,
                graphiccard2x6pin2x8pin_650w
            };

            yield return new object[]
            {
                powersupply_6x0_8x2_6plus8x0_16x0_500w,
                graphiccard2x8pin_500w
            };

            yield return new object[]
            {
                powersupply_6x2_8x2_6plus8x0_16x0_800w,
                graphiccard2x8pin_700w
            };
        }

        [Theory]
        [MemberData(nameof(PowerSupplyAndGraphicCardIncompatibleData))]
        public async void Given_PowerSupplyAndGraphicCard_When_CompatibilityCheck_Then_NotCompatible(PowerSupply powerSupply, GraphicCard graphicCard, List<string> problems)
        {
            // Arrange (Given)
            PcConfiguration configuration = new PcConfiguration
            {
                PowerSupply = powerSupply,
                GraphicCard = graphicCard,
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
        [MemberData(nameof(PowerSupplyAndGraphicCardCompatibleData))]
        public async void Given_PowerSupplyAndGraphicCard_When_CompatibilityCheck_Then_Compatible(PowerSupply powerSupply, GraphicCard graphicCard)
        {
            // Arrange (Given)
            PcConfiguration configuration = new PcConfiguration
            {
                PowerSupply = powerSupply,
                GraphicCard = graphicCard,
            };

            // Act (When)
            Toast? toast = await _compatibilityPcConfigurationService.CompatibilityCheck(configuration);

            // Assert (Then)
            Assert.NotNull(toast);
            Assert.Empty(toast.Problems);
        }
    }
}
