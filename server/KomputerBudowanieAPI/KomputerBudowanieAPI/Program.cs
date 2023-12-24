using KomputerBudowanieAPI.Database;
using KomputerBudowanieAPI.Interfaces;
using KomputerBudowanieAPI.Models;
using KomputerBudowanieAPI.Repository;
using KomputerBudowanieAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//database
builder.Services.AddDbContext<KomBuildDbContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("KomBuildDBContext")));

/*builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<KomBuildDbContext>()
        .AddDefaultTokenProviders();*/
builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireDigit = true;
    options.Password.RequireNonAlphanumeric = true;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<KomBuildDbContext>();

builder.Services.AddControllers();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//builder.Services.AddScoped<ICaseRepository, CaseRepository>();
//builder.Services.AddScoped<ICpuCoolingRepository, CpuCoolingRepository>();
//builder.Services.AddScoped<ICpuRepository, CpuRepository>();
//builder.Services.AddScoped<IFanRepository, FanRepository>();
//builder.Services.AddScoped<IGraphicCardRepository, GraphicCardRepository>();
//builder.Services.AddScoped<IMemoryRepository, MemoryRepository>();
//builder.Services.AddScoped<IMotherboardRepository, MotherboardRepository>();
//builder.Services.AddScoped<IPowerSupplyRepository, PowerSupplyRepository>();
//builder.Services.AddScoped<IRamRepository, RamRepository>();
//builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(PcPartsRepository<>));
builder.Services.AddScoped<IPcConfigurationRepository, PcConfigurationRepository>();
builder.Services.AddScoped<ICompatibilityService, CompatibilityService>();
builder.Services.AddScoped<ICompatibilityDataFilterService, CompatibilityDataFilterService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "KomputerBudowanieApi", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services
    .AddAuthentication()
    .AddCookie()
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Tokens:Issuer"],
            ValidAudience = builder.Configuration["Tokens:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Tokens:Key"]))
        };
    }
);

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("IsAdminJwt",policy =>
        policy
        .RequireRole("Admin")
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
    );

    options.AddPolicy("IsAdminOrScraperJwt", policy =>
        policy
        .RequireRole("Admin", "Scraper")
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
    );
});

builder.Services.AddCors(opt => opt.AddPolicy("CorsPolicy", builder =>
{
    builder.WithOrigins("http://localhost:3000")
        .AllowAnyMethod()
        .AllowAnyHeader();
}));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("CorsPolicy");
app.MapControllers();

app.Run();
