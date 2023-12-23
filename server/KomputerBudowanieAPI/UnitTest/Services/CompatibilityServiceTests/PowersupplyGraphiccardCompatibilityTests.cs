using KomputerBudowanieAPI.Interfaces;
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

        [Fact(Skip = "Nie sończone")]
        public async void Test_CompatibilityCheck_GraphicCard_PowerSupply_Compatible()
        {

        }

        [Fact(Skip = "Nie sończone")]
        public async void Test_CompatibilityCheck_GraphicCard_PowerSupply_NotCompatible()
        {

        }
    }
}
