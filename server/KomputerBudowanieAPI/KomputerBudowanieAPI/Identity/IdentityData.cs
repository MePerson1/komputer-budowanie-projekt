namespace KomputerBudowanieAPI.Identity
{
    public class IdentityData
    {
        public const string AdminUserClaimName = "Admin";
        public const string ScraperUserClaimName = "Scraper";

        public const string AdminPolicyName = "IsAdminJwt";
        public const string ScraperOrAdminPolicyName = "IsAdminOrScraperJwt";
    }
}
