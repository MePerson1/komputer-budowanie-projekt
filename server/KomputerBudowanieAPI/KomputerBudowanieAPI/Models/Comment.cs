using System.Text.Json.Serialization;

namespace KomputerBudowanieAPI.Models
{
    public class Comment
    {
        public string Id { get; set; }
        public string? Content { get; set; }
        public int? Rate { get; set; }
        public User User { get; set; }
        public DateTime TimeStamp { get; set; }
        [JsonIgnore]
        public PcConfiguration? PcConfiguration { get; set; }
    }
}
