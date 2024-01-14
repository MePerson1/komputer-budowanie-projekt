using KomputerBudowanieAPI.Database;
using KomputerBudowanieAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace KomputerBudowanieAPI
{
    public class Seed
    {
        private readonly KomBuildDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _user;
        public Seed(KomBuildDbContext dbContext, UserManager<ApplicationUser> user)
        {
            _dbContext = dbContext;
            _user = user;
        }

        public async Task SeedDataContext()
        {
            if (!_dbContext.Users.Any())
            {
                var scraper = new ApplicationUser
                {
                    UserName = "scraper",
                    Email = "scraper@gmail.com"
                };

                var result = await _user.CreateAsync(scraper, "Scraper123!");

                if (result.Succeeded)
                {
                    await _user.AddToRoleAsync(scraper, "Scraper");
                }
            }
        }

    }
}
