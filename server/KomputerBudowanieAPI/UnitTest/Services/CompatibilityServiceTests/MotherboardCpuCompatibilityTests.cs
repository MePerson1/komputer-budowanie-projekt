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
    public class MotherboardCpuCompatibilityTests
    {
        private readonly ICompatibilityPcConfigurationService _compatibilityPcConfigurationService = new CompatibilityPcConfigurationService(new CompatibilityPartsService());

        [Fact]
        public async void Given_AmdAm4Motherboard_Intel1700Cpu_When_CompatiblityCheck_Then_NotCompatible()
        {
            // Arrange (Given)
            Motherboard motherboard = new()
            {
                Name = "Płyta główna MSI B450M PRO-VDH MAX",
                SupportedProcessors = "AMD Ryzen",
                CPUSocket = "Socket AM4"
            };
            Cpu cpu = new()
            {
                Name = "Procesor Intel Core i5-12400F, 2.5 GHz, 18 MB, BOX (BX8071512400F)",
                Line = "Core i5",
                SocketType = "Socket 1700"
            };

            PcConfiguration configuration = new()
            {
                Motherboard = motherboard,
                Cpu = cpu
            };

            // Act (When)
            Toast? toast = await _compatibilityPcConfigurationService.CompatibilityCheck(configuration);

            // Assert (Then)
            Assert.NotNull(toast);
            Assert.Contains($"Płyta główna nie wspiera tej procesorów {configuration.Cpu.Line}!", toast.Problems);
            Assert.Contains($"Płyta główna ma inny typ gniazda {configuration.Motherboard.CPUSocket}, a procesor {configuration.Cpu.SocketType}!", toast.Problems);
        }

        [Fact]
        public async void Given_AmdAm4Motherboard_AmdAm4Cpu_When_CompatibilityCheck_Then_Compatible()
        {
            // Arrange (Given)
            Motherboard motherboard = new()
            {
                Name = "Płyta główna MSI B450M PRO-VDH MAX",
                SupportedProcessors = "AMD Ryzen",
                CPUSocket = "Socket AM4"
            };
            Cpu cpu = new()
            {
                Name = "Procesor AMD Ryzen 5 5600, 3.5 GHz, 32 MB, BOX (100-100000927BOX)",
                Producer = "AMD",
                Line = "Ryzen 5",
                SocketType = "Socket AM4"
            };

            PcConfiguration configuration = new()
            {
                Motherboard = motherboard,
                Cpu = cpu
            };

            // Act (When)
            Toast? toast = await _compatibilityPcConfigurationService.CompatibilityCheck(configuration);

            // Assert (Then)
            Assert.NotNull(toast);
            Assert.Empty(toast.Problems);
        }

        [Fact]
        public async void Given_Intel1200Motherboard_Intel1200Cpu_When_CompatiblityCheck_Then_Compatible()
        {
            // Arrange (Given)
            Motherboard motherboard = new()
            {
                Name = "Płyta główna MSI PRO H510M-B",
                SupportedProcessors = "Intel Celeron, Intel Core i3, Intel Core i5, Intel Core i7, Intel Core i9, Intel Pentium Gold",
                CPUSocket = "Socket 1200"
            };
            Cpu cpu = new()
            {
                Name = "Procesor Intel Core i7-10700F, 2.9 GHz, 16 MB, BOX (BX8070110700F)",
                Producer = "Intel",
                Line = "Core i7",
                SocketType = "Socket 1200"
            };

            PcConfiguration configuration = new()
            {
                Motherboard = motherboard,
                Cpu = cpu
            };

            // Act (When)
            Toast? toast = await _compatibilityPcConfigurationService.CompatibilityCheck(configuration);

            // Assert (Then)
            Assert.NotNull(toast);
            Assert.Empty(toast.Problems);
        }
    }
}
