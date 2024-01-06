using KomputerBudowanieAPI.Helpers.Request;
using System.Text.Json;

namespace KomputerBudowanieAPI.Helpers
{
    public static class HttpExtensions
    {
        public static void AddPaginationHeader(this HttpResponse response, MetaData metaData)
        {
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            response.Headers.Append("Pagination", JsonSerializer.Serialize(metaData));
            response.Headers.Append("Access-Control-Expose-Headers", "Pagination");
        }
    }
}
