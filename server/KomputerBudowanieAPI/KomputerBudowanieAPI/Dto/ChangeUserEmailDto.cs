using System.ComponentModel.DataAnnotations;

namespace KomputerBudowanieAPI.Dto
{
    public class ChangeUserEmailDto
    {
        [Required(ErrorMessage = "Adres email jest wymagany!")]
        [EmailAddress(ErrorMessage = "Adres email jest niepoprawny!!")]
        public string NewEmail { get; set; }
        public string CurrentPassword { get; set; }
    }
}
