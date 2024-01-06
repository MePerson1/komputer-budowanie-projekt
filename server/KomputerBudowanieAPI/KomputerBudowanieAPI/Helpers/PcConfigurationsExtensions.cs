using KomputerBudowanieAPI.Models;

namespace KomputerBudowanieAPI.Helpers
{
    public static class PcConfigurationsExtensions
    {
        public static IQueryable<PcConfiguration> Search(this IQueryable<PcConfiguration> query, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm)) return query;

            var searchTermToLower = searchTerm.Trim().ToLower();

            return query.Where(p => p.Name.ToLower().Contains(searchTermToLower));
        }

        public static IQueryable<PcConfiguration> Sort(this IQueryable<PcConfiguration> query, string sortBy)
        {
            if (string.IsNullOrWhiteSpace(sortBy)) return query.OrderBy(p => p.Name);

            query = sortBy switch
            {
                "nameDesc" => query.OrderByDescending(p => p.Name),
                "price" => query.OrderBy(p => p.TotalPrice),
                "priceDesc" => query.OrderByDescending(p => p.TotalPrice),
                _ => query.OrderBy(p => p.Name)
            };
            return query;
        }
    }
}
