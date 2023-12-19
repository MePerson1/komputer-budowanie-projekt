using KomputerBudowanieAPI.Services;

namespace Tests.Services
{
    public class ExtractConnectorInfoServiceTests
    {
        [Fact]
        public void Given_IntelCpuSocketString_When_ExtractSocketsFromCpuCooling_Then_ReturnCpuSocketList()
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
        public void Given_AmdCpuCoolingSocketString_When_ExtractSocketsFromCpuCooling_Then_ReturnCpuSocketList()
        {
            // Arrange (Given)
            string processorSocketString = "AM4/AM5, TR4, AM3(+)/AM2(+)/FM2(+)/FM1, AM4/AM5";

            // Act (When)
            List<string> processorSockets = ExtractConnectorInfoService.ExtractSocketsFromCpuCooling(processorSocketString);

            // Assert (Then)
            Assert.Contains("AM4", processorSockets);
            Assert.Contains("AM5", processorSockets);
            Assert.Contains("TR4", processorSockets);
            Assert.Contains("AM2(+)", processorSockets);
            Assert.Contains("AM4", processorSockets);
            Assert.Contains("AM5", processorSockets);
        }


        [Fact]
        public void Given_AmdAndIntelCpuCoolingSocketString_When_ExtractSocketsFromCpuCooling_Then_ReturnCpuSocketList()
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

        [Theory]
        [InlineData("6-pin", 1, "6-pin")]
        [InlineData("8-pin", 1, "8-pin")]
        [InlineData("4x 8-pin", 4, "8-pin")]
        [InlineData("2x 6-pin", 2, "6-pin")]
        [InlineData("6-pin + 8-pin", 2, "6-pin", "8-pin")]
        [InlineData("8-pin + 6-pin", 2, "6-pin", "8-pin")]
        [InlineData("2x 8-pin + 6-pin", 3, "8-pin", "6-pin")]
        [InlineData("2x 6-pin + 2x 8-pin", 4, "8-pin", "6-pin")]
        [InlineData("12-pin", 1, "12-pin")]
        public void Given_PowerConnectorsString_ExtractPowerConnectorsFromGraphicCard_Then_ReturnPowerConnectorsList(
            string input, int expectedCount, params string[] expectedConnectors)
        {
            List<string> connectors = ExtractConnectorInfoService.ExtractPowerConnectorsFromGraphicCard(input);

            Assert.Equal(expectedCount, connectors.Count);

            foreach (var connector in expectedConnectors)
            {
                Assert.Contains(connector, connectors);
            }
        }

        [Theory]
        [InlineData("M.2 slot x1", 1, "M.2 slot")]
        [InlineData("M.2 slot x2, SATA 3 x4", 2, "M.2 slot", 4, "SATA 3")]
        [InlineData("SATA 3 x4, M.2 slot x2", 4, "SATA 3", 2, "M.2 slot")]
        [InlineData("M.2 slot x1, SATA 3 x6, U.2 port x1", 1, "M.2 slot", 6, "SATA 3", 1, "U.2 port")]
        public void Given_StorageSlotString_When_ExtractsStorageSlotsFromMotherboard_Then_ReturnStorageSlotDictionary(
            string input, params object[] expectedResults)
        {
            Dictionary<string, int> connectors = ExtractConnectorInfoService.ExtractsStorageSlotsFromMotherboard(input);

            Assert.Equal(expectedResults.Length / 2, connectors.Count);

            for (int i = 0; i < expectedResults.Length; i += 2)
            {
                int expectedCount = (int)expectedResults[i];
                string slotType = (string)expectedResults[i + 1];

                Assert.True(connectors.ContainsKey(slotType));
                Assert.Equal(expectedCount, connectors[slotType]);
            }
        }
    }
}