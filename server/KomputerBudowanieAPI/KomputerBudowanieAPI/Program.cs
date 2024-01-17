using KomputerBudowanieAPI.Database;
using KomputerBudowanieAPI.Identity;
using KomputerBudowanieAPI.Interfaces;
using KomputerBudowanieAPI.Models;
using KomputerBudowanieAPI.Repository;
using KomputerBudowanieAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddTransient<Seed>();
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
            options.User.RequireUniqueEmail = true;
        })
        .AddErrorDescriber<CustomIdentityErrorDescriber>()
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<KomBuildDbContext>();

        builder.Services.AddControllers();

        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        builder.Services.AddScoped<IShopPriceRepository, ShopPriceRepository>();
        builder.Services.AddScoped(typeof(IPcPartsRepository<>), typeof(PcPartsRepository<>));
        builder.Services.AddScoped<IPcConfigurationRepository, PcConfigurationRepository>();

        builder.Services.AddScoped<ICompatibilityPartsService, CompatibilityPartsService>();
        builder.Services.AddScoped<ICompatibilityPcConfigurationService, CompatibilityPcConfigurationService>();
        builder.Services.AddScoped<ICompatibilityDataFilterService, CompatibilityDataFilterService>();

        var issuer = builder.Configuration["Tokens:Issuer"];
        var audience = builder.Configuration["Tokens:Audience"];
        var key = builder.Configuration["Tokens:Key"];

        var jwtTokenHandler = new JwtTokenHandler(issuer, audience, key);

        builder.Services.AddSingleton<IJwtTokenHandler>(jwtTokenHandler);

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
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = jwtTokenHandler.GetTokenValidationParameters();
            }
        );

        builder.Services.AddAuthorization(options =>
        {
            options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build();

            options.AddPolicy(IdentityData.AdminPolicyName, policy =>
                policy
                .RequireRole(IdentityData.AdminUserClaimName)
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
            );

            options.AddPolicy(IdentityData.ScraperOrAdminPolicyName, policy =>
                policy
                .RequireRole(IdentityData.ScraperUserClaimName, IdentityData.AdminUserClaimName)
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

        //adding the scrapper user
        try
        {
            Task.Run(() => SeedData(app)).Wait();
        }
        catch (Exception ex)
        {

            Console.WriteLine(ex.ToString());
        }


        async Task SeedData(IHost app)
        {
            var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

            using (var scope = scopedFactory.CreateScope())
            {
                var service = scope.ServiceProvider.GetService<Seed>();
                await service.SeedDataContext();
            }
        }

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseCors("CorsPolicy");
        app.UseAuthentication();
        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}