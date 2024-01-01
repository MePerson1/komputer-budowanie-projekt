using KomputerBudowanieAPI.Database;
using KomputerBudowanieAPI.Interfaces;
using KomputerBudowanieAPI.Models;
using KomputerBudowanieAPI.Repository;
using KomputerBudowanieAPI.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.Web.CodeGeneration.Design;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Controllers
{
    public class CustomWebApplicationMockFactory : WebApplicationFactory<Program>
    {
        public Mock<IGenericRepository<Cpu>> CpuRepositoryMock { get; }
        public Mock<IPcConfigurationRepository> PcConfigurationRepositoryMock { get; }

        public CustomWebApplicationMockFactory()
        {
            CpuRepositoryMock = new Mock<IGenericRepository<Cpu>>();
            PcConfigurationRepositoryMock = new Mock<IPcConfigurationRepository>();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);

            builder.ConfigureServices(services =>
            {
                services.AddSingleton(CpuRepositoryMock.Object);
            });
        }
    }
}
