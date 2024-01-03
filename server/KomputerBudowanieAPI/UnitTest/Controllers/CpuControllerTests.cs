using KomputerBudowanieAPI.Controllers;
using KomputerBudowanieAPI.Dto;
using KomputerBudowanieAPI.Identity;
using KomputerBudowanieAPI.Interfaces;
using KomputerBudowanieAPI.Models;
using KomputerBudowanieAPI.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Controllers
{
    public class CpuControllerTests : IDisposable
    {
        private CustomWebApplicationMockFactory _factory;
        private HttpClient _client;

        public CpuControllerTests()
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
        public async Task Get_Always_ReturnAllCpus()
        {
            // Arrange
            var mockCpus = new Cpu[]
            {
                new() { Id = 1, Name = "AMD Ryzen 7 5700X" },
                new() { Id = 2, Name = "Intel Core i5-12400F" },
                new() { Id = 3, Name = "Intel Core i5-12600KF" },
            }.AsQueryable();

            _factory.CpuRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(mockCpus);

            // Act
            var response = await _client.GetAsync("/api/cpu");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var responseContent = await response.Content.ReadAsStringAsync();
            var cpus = JsonConvert.DeserializeObject<IEnumerable<CpuDto>>(responseContent);

            Assert.NotNull(cpus);
            Assert.Collection(cpus,
                cpu => Assert.Equal("AMD Ryzen 7 5700X", cpu.Name),
                cpu => Assert.Equal("Intel Core i5-12400F", cpu.Name),
                cpu => Assert.Equal("Intel Core i5-12600KF", cpu.Name)
            );
        }

        [Fact]
        public async Task GetById_IfExists_ReturnsCpu()
        {
            // Arrange
            var mockCpus = new Cpu[]
            {
                new() { Id = 1, Name = "AMD Ryzen 7 5700X" },
                new() { Id = 2, Name = "Intel Core i5-12400F" },
                new() { Id = 3, Name = "Intel Core i5-12600KF" },
            }.AsQueryable();

            _factory.CpuRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => mockCpus.FirstOrDefault(cpu => cpu.Id == id));

            // Act
            var response = await _client.GetAsync("/api/cpu/2");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var responseContent = await response.Content.ReadAsStringAsync();
            var cpu = JsonConvert.DeserializeObject<CpuDto>(responseContent);

            Assert.NotNull(cpu);
            Assert.Equal(2, cpu.Id);
            Assert.Equal("Intel Core i5-12400F", cpu.Name);
        }

        [Fact]
        public async Task GetById_IfMissing_Returns404()
        {
            // Arrange
            var mockCpus = new Cpu[]
            {
                new() { Id = 1, Name = "AMD Ryzen 7 5700X" },
                new() { Id = 3, Name = "Intel Core i5-12600KF" },
            }.AsQueryable();

            _factory.CpuRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => mockCpus.FirstOrDefault(cpu => cpu.Id == id));

            // Act
            var response = await _client.GetAsync("/api/cpu/2");

            // Assert
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Post_WithoutToken_Returns401()
        {
            // Arrange
            CpuDto cpuDto = new()
            {
                Name = "AMD Ryzen 7 5700X",
            };

            _factory.CpuRepositoryMock.Setup(r => r.Create(It.Is<Cpu>(c => c.Name == "AMD Ryzen 7 5700X"))).Verifiable();
            _factory.CpuRepositoryMock.Setup(r => r.SaveChanges()).Verifiable();

            // Act
            var response = await _client.PostAsync("/api/cpu", JsonContent.Create(cpuDto));

            // Assert
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}
