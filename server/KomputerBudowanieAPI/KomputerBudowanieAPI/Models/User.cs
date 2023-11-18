using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KomputerBudowanieAPI.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } //to poźniej na hash zamienic

        /*
        *  RELACJE
        */
        [JsonIgnore]
        public ICollection<PcConfiguration>? UserConfigurations { get; set; }
        [JsonIgnore]
        public ICollection<PcConfiguration>? FavouriteConfigurations { get; set; }
        [JsonIgnore]
        public ICollection<Comment>? UserComments { get; set; }

    }
}
