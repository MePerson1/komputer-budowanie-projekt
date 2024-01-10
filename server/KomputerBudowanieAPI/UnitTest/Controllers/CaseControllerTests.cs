using KomputerBudowanieAPI.Dto;
using KomputerBudowanieAPI.Models;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Controllers
{
    public class CaseControllerTests : IDisposable
    {
        private CustomWebApplicationMockFactory _factory;
        private HttpClient _client;

        public CaseControllerTests()
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
        public async Task Get_Always_ReturnAllCases()
        {
            // Arrange
            var mockCases = new Case[]
            {
                new() { Id = 1, Name = "Obudowa Cooler Master MasterBox TD500 Mesh White (MCB-D500D-WGNN-S01)" },
                new() { Id = 2, Name = "Obudowa Krux Heko (KRXD001)" },
                new() { Id = 3, Name = "Obudowa Endorfy Ventum 200 ARGB (EY2A014)" },
            }.AsQueryable();

            _factory.CaseRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(mockCases);

            // Act
            var response = await _client.GetAsync("/api/case");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var responseContent = await response.Content.ReadAsStringAsync();
            var cases = JsonConvert.DeserializeObject<IEnumerable<CaseDto>>(responseContent);

            Assert.NotNull(cases);
            Assert.Collection(cases,
                pcCase => Assert.Equal("Obudowa Cooler Master MasterBox TD500 Mesh White (MCB-D500D-WGNN-S01)", pcCase.Name),
                pcCase => Assert.Equal("Obudowa Krux Heko (KRXD001)", pcCase.Name),
                pcCase => Assert.Equal("Obudowa Endorfy Ventum 200 ARGB (EY2A014)", pcCase.Name)
            );
        }

        [Fact]
        public async Task GetById_IfExists_ReturnsCases()
        {
            // Arrange
            var mockCases = new Case[]
            {
                new() { Id = 1, Name = "Obudowa Cooler Master MasterBox TD500 Mesh White (MCB-D500D-WGNN-S01)" },
                new() { Id = 2, Name = "Obudowa Krux Heko (KRXD001)" },
                new() { Id = 3, Name = "Obudowa Endorfy Ventum 200 ARGB (EY2A014)" },
            }.AsQueryable();

            _factory.CaseRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => mockCases.FirstOrDefault(pcCase => pcCase.Id == id));

            // Act
            var response = await _client.GetAsync("/api/case/2");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var responseContent = await response.Content.ReadAsStringAsync();
            var pcCase = JsonConvert.DeserializeObject<CaseDto>(responseContent);

            Assert.NotNull(pcCase);
            Assert.Equal(2, pcCase.Id);
            Assert.Equal("Obudowa Krux Heko (KRXD001)", pcCase.Name);
        }

        [Fact]
        public async Task GetById_IfMissing_Returns404()
        {
            // Arrange
            var mockCases = new Case[]
            {
                new() { Id = 1, Name = "Obudowa Cooler Master MasterBox TD500 Mesh White (MCB-D500D-WGNN-S01)" },
                new() { Id = 3, Name = "Obudowa Endorfy Ventum 200 ARGB (EY2A014)" },
            }.AsQueryable();

            _factory.CaseRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => mockCases.FirstOrDefault(pcCase => pcCase.Id == id));

            // Act
            var response = await _client.GetAsync("/api/case/2");

            // Assert
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Post_WithoutToken_Returns401()
        {
            // Arrange
            CaseDto caseDto = new()
            {
                Name = "Obudowa Endorfy Ventum 200 ARGB (EY2A014)",
            };

            _factory.CaseRepositoryMock.Setup(r => r.Create(It.Is<Case>(c => c.Name == caseDto.Name))).Verifiable();
            _factory.CaseRepositoryMock.Setup(r => r.SaveChanges()).Verifiable();

            // Act
            var response = await _client.PostAsync("/api/case", JsonContent.Create(caseDto));

            // Assert
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}
