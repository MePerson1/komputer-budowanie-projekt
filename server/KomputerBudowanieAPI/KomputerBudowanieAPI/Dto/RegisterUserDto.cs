using System.ComponentModel.DataAnnotations;

namespace KomputerBudowanieAPI.Dto
{
    public class RegisterUserDto
    {
        [Required(ErrorMessage = "Nazwa użytkownika jest wymagana!")]
        [MinLength(3, ErrorMessage = "Nazwa użytkownika musi wynosić minimum 3 znaki!")]
        [MaxLength(32, ErrorMessage = "Nazwa użytkownika nie może być dłuższa niż 32 znaki!")]
        [RegularExpression("^\\p{L}[\\p{L}0-9]*$", ErrorMessage = "Nazwa użytkownika musi zaczynać się od litery i zawierać tylko litery i cyfry!")]
        public string NickName { get; set; }

        [Required(ErrorMessage = "Adres email jest wymagany!")]
        [EmailAddress(ErrorMessage = "Adres email jest niepoprawny!!")]
        public string Email { get; set; }

        // Poprawność hasła jest weryfikowana poprzez ASP.NET Core Identity
        [Required(ErrorMessage = "Hasło jest wymagane!")]
        public string Password { get; set; }
    }
}
