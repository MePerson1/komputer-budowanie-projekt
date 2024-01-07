using Microsoft.EntityFrameworkCore;

namespace KomputerBudowanieAPI.Helpers.Request
{
    public class PagedList<T> : List<T>
    {
        private PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            MetaData = new MetaData
            {
                TotalCount = count,
                PageSize = pageSize,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize)

            };
            AddRange(items);
        }

        public MetaData MetaData { get; set; }

        public static async Task<PagedList<T>> ToPagedList(IQueryable<T> query, int pageNumber, int pageSize)
        {
            var count = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }

        public static async Task<PagedList<T>> ToPagedList(IEnumerable<T> enumerable, int pageNumber, int pageSize)
        {
            var items = enumerable.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PagedList<T>(items, enumerable.Count(), pageNumber, pageSize);
        }

    }
}
