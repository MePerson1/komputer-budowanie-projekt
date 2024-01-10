using KomputerBudowanieAPI.Dto;
using KomputerBudowanieAPI.Models;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Controllers
{
    public class CpuCoolingControllerTests : IDisposable
    {
        private CustomWebApplicationMockFactory _factory;
        private HttpClient _client;

        public CpuCoolingControllerTests()
        {
            _factory = new CustomWebApplicationMockFactory();
            _client = _factory.CreateClient();
        }

        public void Dispose()
        {
            _client.Dispose();
            _factory.Dispose();
        }

        [Fact]
        public async Task Get_Always_ReturnAllCpuCooling()
        {
            // Arrange
            var mockCpuCooling = new CpuCooling[]
            {
                new() { Id = 1, Name = "Chłodzenie CPU Endorfy Fera 5 Dual Fan (EY3A006)" },
                new() { Id = 2, Name = "Chłodzenie CPU be quiet! Dark Rock 4 (BK021)" },
                new() { Id = 3, Name = "Chłodzenie CPU Deepcool AK620 (R-AK620-BKNNMT-G)" },
            }.AsQueryable();

            _factory.CpuCoolingRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(mockCpuCooling);

            // Act
            var response = await _client.GetAsync("/api/cpu-cooling");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var responseContent = await response.Content.ReadAsStringAsync();
            var cpuCoolings = JsonConvert.DeserializeObject<IEnumerable<CpuCoolingDto>>(responseContent);

            Assert.NotNull(cpuCoolings);
            Assert.Collection(cpuCoolings,
                cpuCooling => Assert.Equal("Chłodzenie CPU Endorfy Fera 5 Dual Fan (EY3A006)", cpuCooling.Name),
                cpuCooling => Assert.Equal("Chłodzenie CPU be quiet! Dark Rock 4 (BK021)", cpuCooling.Name),
                cpuCooling => Assert.Equal("Chłodzenie CPU Deepcool AK620 (R-AK620-BKNNMT-G)", cpuCooling.Name)
            );
        }

        [Fact]
        public async Task GetById_IfExists_ReturnsCases()
        {
            // Arrange
            var mockCpuCooling = new CpuCooling[]
            {
                new() { Id = 1, Name = "Chłodzenie CPU Endorfy Fera 5 Dual Fan (EY3A006)" },
                new() { Id = 2, Name = "Chłodzenie CPU be quiet! Dark Rock 4 (BK021)" },
                new() { Id = 3, Name = "Chłodzenie CPU Deepcool AK620 (R-AK620-BKNNMT-G)" },
            }.AsQueryable();

            _factory.CpuCoolingRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => mockCpuCooling.FirstOrDefault(cooling => cooling.Id == id));

            // Act
            var response = await _client.GetAsync("/api/cpu-cooling/2");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var responseContent = await response.Content.ReadAsStringAsync();
            var cpuCooling = JsonConvert.DeserializeObject<CpuCoolingDto>(responseContent);

            Assert.NotNull(cpuCooling);
            Assert.Equal(2, cpuCooling.Id);
            Assert.Equal("Chłodzenie CPU be quiet! Dark Rock 4 (BK021)", cpuCooling.Name);
        }

        [Fact]
        public async Task GetById_IfMissing_Returns404()
        {
            // Arrange
            var mockCpuCooling = new CpuCooling[]
            {
                new() { Id = 1, Name = "Chłodzenie CPU Endorfy Fera 5 Dual Fan (EY3A006)" },
                new() { Id = 3, Name = "Chłodzenie CPU Deepcool AK620 (R-AK620-BKNNMT-G)" },
            }.AsQueryable();

            _factory.CpuCoolingRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => mockCpuCooling.FirstOrDefault(cooling => cooling.Id == id));

            // Act
            var response = await _client.GetAsync("/api/cpu-cooling/2");

            // Assert
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Post_WithoutToken_Returns401()
        {
            // Arrange
            CpuCoolingDto cpuCoolingDto = new()
            {
                Name = "Chłodzenie CPU Endorfy Fera 5 Dual Fan (EY3A006)",
            };

            _factory.CpuCoolingRepositoryMock.Setup(r => r.Create(It.Is<CpuCooling>(c => c.Name == cpuCoolingDto.Name))).Verifiable();
            _factory.CpuCoolingRepositoryMock.Setup(r => r.SaveChanges()).Verifiable();

            // Act
            var response = await _client.PostAsync("/api/cpu-cooling", JsonContent.Create(cpuCoolingDto));

            // Assert
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}
