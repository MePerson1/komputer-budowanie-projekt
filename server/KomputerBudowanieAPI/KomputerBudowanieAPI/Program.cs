using KomputerBudowanieAPI.Database;
using KomputerBudowanieAPI.Interfaces;
using KomputerBudowanieAPI.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//database
builder.Services.AddDbContext<KomBuildDbContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("KomBuildDBContext")));

builder.Services.AddControllers();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<ICaseRepository, CaseRepository>();
builder.Services.AddScoped<ICpuCoolingRepository, CpuCoolingRepository>();
builder.Services.AddScoped<ICpuRepository, CpuRepository>();
builder.Services.AddScoped<IFanRepository, FanRepository>();
builder.Services.AddScoped<IGraphicCardRepository, GraphicCardRepository>();
builder.Services.AddScoped<IMemoryRepository, MemoryRepository>();
builder.Services.AddScoped<IMotherboardRepository, MotherboardRepository>();
builder.Services.AddScoped<IPowerSupplyRepository, PowerSupplyRepository>();
builder.Services.AddScoped<IRamRepository, RamRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
