using KomputerBudowanieAPI.Identity;
using KomputerBudowanieAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace KomputerBudowanieAPI.Database
{
    public class SeedDatabase
    {
        private readonly KomBuildDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _user;
        public SeedDatabase(KomBuildDbContext dbContext, UserManager<ApplicationUser> user)
        {
            _dbContext = dbContext;
            _user = user;
        }

        public async Task SeedDataContext()
        {
            var isScraperExists = _dbContext.Users.FirstOrDefault(u => u.Email == "scraper@gmail.com");
            if (isScraperExists is null)
            {
                var scraper = new ApplicationUser
                {
                    UserName = "scraper",
                    Email = "scraper@gmail.com"
                };

                var result = await _user.CreateAsync(scraper, "(Scr4puScr4pu)");

                if (result.Succeeded)
                {
                    await _user.AddToRoleAsync(scraper, IdentityData.ScraperUserClaimName);
                }
            }
        }

    }
}
