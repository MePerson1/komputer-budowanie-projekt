using KomputerBudowanieAPI.Services;

namespace UnitTest
{
    public class ExtractConnectorInfoServiceTests
    {
        [Fact]
        public void Test_ExtractSocketsFromCpuCooling_Intel_WaterCooling()
        {
            string processorSocketIntelString = "LGA 1366, LGA 2011/2011-3, LGA 2066, LGA 1150/1151/1155/1156/1200, LGA 1700";
            List<string> processorSockets = ExtractConnectorInfoService.ExtractSocketsFromCpuCooling(processorSocketIntelString);

            Assert.Contains("1366", processorSockets);
            Assert.Contains("2011", processorSockets);
            Assert.Contains("2011-3", processorSockets);
            Assert.Contains("1151", processorSockets);
            Assert.Contains("1156", processorSockets);
            Assert.Contains("1200", processorSockets);
            Assert.Contains("1700", processorSockets);
        }

        [Fact]
        public void Test_ExtractSocketsFromCpuCooling_Amd_WaterCooling()
        {
          
            string processorSocketString = "AM4/AM5, TR4, AM3(+)/AM2(+)/FM2(+)/FM1, AM4/AM5";
            List<string> processorSockets = ExtractConnectorInfoService.ExtractSocketsFromCpuCooling(processorSocketString);

            Assert.Contains("AM4", processorSockets);
            Assert.Contains("AM5", processorSockets);
            Assert.Contains("TR4", processorSockets);
            Assert.Contains("AM2(+)", processorSockets);
            Assert.Contains("AM4", processorSockets);
            Assert.Contains("AM5", processorSockets);
        }


        [Fact]
        public void Test_ExtractSocketsFromCpuCooling_CpuCooling()
        {
            string processorSocketIntelString = "1150/1151/1155/1156/1200, 1366, 1700, 2011/2011-3, 2066, 775, AM3(+)/AM2(+)/FM2(+)/FM1, AM4/AM5";
            List<string> processorSockets = ExtractConnectorInfoService.ExtractSocketsFromCpuCooling(processorSocketIntelString);

            Assert.Contains("1150", processorSockets);
            Assert.Contains("1155", processorSockets);
            Assert.Contains("1200", processorSockets);
            Assert.Contains("1700", processorSockets);
            Assert.Contains("2011", processorSockets);
            Assert.Contains("2011-3", processorSockets);
            Assert.Contains("775", processorSockets);
            Assert.Contains("AM3(+)", processorSockets);
            Assert.Contains("FM1", processorSockets);
            Assert.Contains("AM5", processorSockets);
        }


        [Fact]
        public void Test_FromGraphicCard_3x8pin()
        {
            string input = "3x 8-pin";

            List<string> connectors = ExtractConnectorInfoService.FromGraphicCard(input);

            Assert.Equal(3, connectors.Count);
            foreach (var item in connectors)
            {
                Assert.Equal("8-pin", item);
            }
        }

        [Fact]
        public void Test_FromGraphicCard_6pin()
        {
            string input = "6-pin";

            List<string> connectors = ExtractConnectorInfoService.FromGraphicCard(input);

            Assert.Single(connectors);
            Assert.Contains(input, connectors);
        }

        [Fact]
        public void Test_FromGraphicCard_6pin_8pin()
        {
            string input = "6-pin + 8-pin";

            List<string> connectors = ExtractConnectorInfoService.FromGraphicCard(input);

            Assert.Equal(2, connectors.Count);
            Assert.Contains("6-pin", connectors);
            Assert.Contains("8-pin", connectors);
        }

        [Fact]
        public void Test_FromGraphicCard_2x6pin_8pin()
        {
            string input = "2x 6-pin + 8-pin";

            List<string> connectors = ExtractConnectorInfoService.FromGraphicCard(input);

            Assert.Equal(3, connectors.Count);
            Assert.Equal("6-pin", connectors[0]);
            Assert.Equal("6-pin", connectors[1]);
            Assert.Equal("8-pin", connectors[2]);
        }

        [Fact]
        public void Test_FromGraphicCard_12pin()
        {
            string input = "12-pin";

            List<string> connectors = ExtractConnectorInfoService.FromGraphicCard(input);

            Assert.Single(connectors);
            Assert.Contains(input, connectors);
        }

        [Fact]
        public void Test_FromMotherboard_2xM2_4xSata3()
        {
            string input = "M.2 slot x2, SATA 3 x4";

            Dictionary<string, int> connectors = ExtractConnectorInfoService.FromMotherboard(input);

            Assert.Equal(2, connectors["M.2 slot"]);
            Assert.Equal(4, connectors["SATA 3"]);
        }

        [Fact]
        public void Test_FromMotherboard_1xM2_6xSata3_1xU2()
        {
            string input = "M.2 slot x1, SATA 3 x6, U.2 port x1";

            Dictionary<string, int> connectors = ExtractConnectorInfoService.FromMotherboard(input);

            Assert.Equal(1, connectors["M.2 slot"]);
            Assert.Equal(6, connectors["SATA 3"]);
            Assert.Equal(1, connectors["U.2 port"]);
        }
    }
}