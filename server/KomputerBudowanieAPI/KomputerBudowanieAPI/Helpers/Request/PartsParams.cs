namespace KomputerBudowanieAPI.Helpers.Request
{
    public class PartsParams : PaginationParams
    {
        public string? SortBy { get; set; }
        public string? SearchTerm { get; set; }
    }
}
