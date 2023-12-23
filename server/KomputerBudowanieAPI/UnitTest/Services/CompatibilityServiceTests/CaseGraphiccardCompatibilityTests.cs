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
    public class CaseGraphiccardCompatibilityTests
    {
        private readonly ICompatibilityPcConfigurationService _compatibilityPcConfigurationService = new CompatibilityPcConfigurationService(new CompatibilityPartsService());

        [Fact]
        public async void Given_LongGraphiccard_ShortCase_When_CompatibilityCheck_Then_NotCompatible()
        {
            GraphicCard graphiccard = new()
            {
                Name = "Karta graficzna Gigabyte GeForce RTX 4090 Gaming OC 24 GB GDDR6X (GV-N4090GAMING OC-24GD)",
                CardLengthMM = 340,
                PowerConnectors = "16-pin",
            };

            Case pcCase = new()
            {
                Name = "Obudowa Krux Vako RGB (KRX0132)",
                MaxGPULengthCM = 31,
                LengthCM = (float)33.3,
                WidthCM = 20,
                HeightCM = 44
            };

            PcConfiguration configuration = new()
            {
                GraphicCard = graphiccard,
                Case = pcCase
            };

            // Act (When)
            Toast? toast = await _compatibilityPcConfigurationService.CompatibilityCheck(configuration);

            // Assert (Then)
            Assert.NotNull(toast);
            Assert.Contains("Karta graficzna jest za duża dla tej obudowy!", toast.Problems);
        }

        [Fact]
        public async void Given_LongGraphiccard_LongCase_When_CompatibilityCheck_Then_Compatible()
        {
            GraphicCard graphiccard = new()
            {
                Name = "Karta graficzna Gigabyte GeForce RTX 4090 Gaming OC 24 GB GDDR6X (GV-N4090GAMING OC-24GD)",
                CardLengthMM = 340,
                PowerConnectors = "16-pin",
            };

            Case pcCase = new()
            {
                Name = "Obudowa be quiet! Pure Base 600 (BG022)",
                MaxGPULengthCM = (float)42.5,
                LengthCM = (float)49.2,
                WidthCM = 22,
                HeightCM = 47
            };

            PcConfiguration configuration = new()
            {
                GraphicCard = graphiccard,
                Case = pcCase
            };

            // Act (When)
            Toast? toast = await _compatibilityPcConfigurationService.CompatibilityCheck(configuration);

            // Assert (Then)
            Assert.NotNull(toast);
            Assert.Empty(toast.Problems);
        }

        [Fact]
        public async void Given_MediumGraphiccard_MediumCase_When_CompatibilityCheck_Then_Compatible()
        {
            GraphicCard graphiccard = new()
            {
                Name = "Karta graficzna Gigabyte GeForce RTX 3060 Eagle OC 12GB GDDR6 (GV-N3060EAGLE OC-12GD 2.0)",
                CardLengthMM = 242,
                PowerConnectors = "8-pin",
            };

            Case pcCase = new()
            {
                Name = "Obudowa Endorfy Ventum 200 ARGB (EY2A014)",
                MaxGPULengthCM = (float)31.5,
                LengthCM = (float)37.3,
                WidthCM = (float)21.6,
                HeightCM = 44
            };

            PcConfiguration configuration = new()
            {
                GraphicCard = graphiccard,
                Case = pcCase
            };

            // Act (When)
            Toast? toast = await _compatibilityPcConfigurationService.CompatibilityCheck(configuration);

            // Assert (Then)
            Assert.NotNull(toast);
            Assert.Empty(toast.Problems);
        }
    }
}
