using KomputerBudowanieAPI.Interfaces;

namespace KomputerBudowanieAPI.Helpers
{
    public static class PartsExtensions
    {
        public static IQueryable<TEntity> Search<TEntity>(this IQueryable<TEntity> query, string searchTerm)
            where TEntity : class, IProduct
        {
            if (string.IsNullOrWhiteSpace(searchTerm)) return query;

            var searchTermToLower = searchTerm.Trim().ToLower();

            return query.Where(p => p.Name.ToLower().Contains(searchTermToLower));
        }

        public static IQueryable<TEntity> Sort<TEntity>(this IQueryable<TEntity> query, string sortBy)
            where TEntity : class, IProduct
        {
            if (string.IsNullOrWhiteSpace(sortBy)) return query.OrderBy(p => p.Name);

            query = sortBy switch
            {
                "nameDesc" => query.OrderByDescending(p => p.Name),
                "price" => query.OrderBy(p => p.Prices != null && p.Prices.Any() ? p.Prices.Min(price => price.Price) : double.MaxValue),
                "priceDesc" => query.OrderByDescending(p => p.Prices != null && p.Prices.Any() ? p.Prices.Min(price => price.Price) : double.MaxValue),
                _ => query.OrderBy(p => p.Name)
            };
            return query;
        }
    }
}
