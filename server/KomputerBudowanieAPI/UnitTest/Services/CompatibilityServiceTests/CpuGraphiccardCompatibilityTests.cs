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
    public class CpuGraphiccardCompatibilityTests
    {
        private readonly ICompatibilityPcConfigurationService _compatibilityPcConfigurationService = new CompatibilityPcConfigurationService(new CompatibilityPartsService());

        [Fact]
        public async void Given_CpuWithoutGraphic_When_CompatibilityCheck_Then_Warning()
        {
            // Arrange (Given)
            Cpu cpu = new()
            {
                Name = "Procesor Intel Core i5-12400F, 2.5 GHz, 18 MB, BOX (BX8071512400F)",
                SocketType = "Socket 1700",
                IntegratedGraphics = null
            };

            PcConfiguration configuration = new()
            {
                Cpu = cpu,
            };

            // Act (When)
            Toast? toast = await _compatibilityPcConfigurationService.CompatibilityCheck(configuration);

            // Assert (Then)
            Assert.NotNull(toast);
            Assert.Empty(toast?.Problems);
            Assert.Contains("Ten procesor nie ma zintegrowanej karty graficznej! Dodaj kartę graficzną.", toast.Warnings);
        }

        [Fact]
        public async void Given_CpuWithGraphic_When_CompatibilityCheck_Then_NoWarning()
        {
            // Arrange (Given)
            Cpu cpu = new()
            {
                Name = "Procesor Intel Core i5-13600K, 3.5 GHz, 24 MB, BOX (BX8071513600K)",
                SocketType = "Socket 1700",
                IntegratedGraphics = "Intel UHD Graphics 770"
            };

            PcConfiguration configuration = new()
            {
                Cpu = cpu,
            };

            // Act (When)
            Toast? toast = await _compatibilityPcConfigurationService.CompatibilityCheck(configuration);

            // Assert (Then)
            Assert.NotNull(toast);
            Assert.Empty(toast?.Problems);
            Assert.Empty(toast?.Warnings);
        }

        [Fact]
        public async void Given_CpuWithoutGraphic_Graphiccard_When_CompatibilityCheck_Then_NoWarning()
        {
            // Arrange (Given)
            Cpu cpu = new()
            {
                Name = "Procesor Intel Core i5-12400F, 2.5 GHz, 18 MB, BOX (BX8071512400F)",
                SocketType = "Socket 1700",
                IntegratedGraphics = null
            };
            GraphicCard graphicCard = new()
            {
                Name = "Karta graficzna Gigabyte GeForce RTX 3060 Eagle OC 12GB GDDR6 (GV-N3060EAGLE OC-12GD 2.0)",
                ChipsetType = "GeForce RTX 3060",
            };

            PcConfiguration configuration = new()
            {
                Cpu = cpu,
                GraphicCard = graphicCard
            };

            // Act (When)
            Toast? toast = await _compatibilityPcConfigurationService.CompatibilityCheck(configuration);

            // Assert (Then)
            Assert.NotNull(toast);
            Assert.Empty(toast?.Problems);
            Assert.Empty(toast?.Warnings);
        }
    }
}
