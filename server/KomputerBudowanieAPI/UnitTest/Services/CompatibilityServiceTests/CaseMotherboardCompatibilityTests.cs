using KomputerBudowanieAPI.Models;
using KomputerBudowanieAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Services.CompatibilityServiceTests
{
    public class CaseMotherboardCompatibilityTests
    {
        private readonly CompatibilityService _compatibilityService = new CompatibilityService();

        [Fact]
        public async void Given_AtxCase_MicroAtxMotherboard_When_CompatibilityCheck_Then_Compatible()
        {
            // Arrange (Given)
            Case pcCase = new()
            {
                Name = "Obudowa Modecom Oberon Pro Silent (AT-OBERON-PS-10-000000-0002)",
                Compatibility = "ATX, Micro ATX (uATX), Mini ITX",
                HeightCM = (float)47.5,
                LengthCM = (float)45.5,
                WidthCM = (float)20.5,
            };
            Motherboard motherboard = new()
            {
                Name = "Płyta główna MSI B450M PRO-VDH MAX",
                BoardStandard = "Micro ATX",
                WidthMM = 244,
                DepthMM = 244
            };

            PcConfiguration configuration = new()
            {
                Case = pcCase,
                Motherboard = motherboard
            };

            // Act (When)
            Toast? toast = await _compatibilityService.CompatibilityCheck(configuration);

            // Assert (Then)
            Assert.NotNull(toast);
            Assert.Empty(toast?.Problems);
        }

        [Fact]
        public async void Given_AtxCase_EAtxMotherboard_When_CompatibilityCheck_Then_NotCompatible()
        {
            // Arrange (Given)
            Case pcCase = new()
            {
                Name = "Obudowa Modecom Oberon Pro Silent (AT-OBERON-PS-10-000000-0002)",
                Compatibility = "ATX, Micro ATX (uATX), Mini ITX",
                HeightCM = (float)47.5,
                LengthCM = (float)45.5,
                WidthCM = (float)20.5,
            };
            Motherboard motherboard = new()
            {
                Name = "Płyta główna Gigabyte Z790 AORUS MASTER",
                BoardStandard = "Extended ATX",
                WidthMM = 260,
                DepthMM = 305
            };

            PcConfiguration configuration = new()
            {
                Case = pcCase,
                Motherboard = motherboard
            };

            // Act (When)
            Toast? toast = await _compatibilityService.CompatibilityCheck(configuration);

            // Assert (Then)
            Assert.NotNull(toast);
            Assert.Contains($"Obudowa nie jest kompatybilna z standardem płyty głównej {motherboard.BoardStandard}!", toast.Problems);
        }
    }
}
