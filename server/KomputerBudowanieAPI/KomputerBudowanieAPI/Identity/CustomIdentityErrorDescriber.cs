using Microsoft.AspNetCore.Identity;

namespace KomputerBudowanieAPI.Identity
{
    public class CustomIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError DefaultError()
        {
            return new IdentityError
            {
                Code = nameof(DefaultError),
                Description = "Wystąpił błąd."
            };
        }

        public override IdentityError ConcurrencyFailure()
        {
            return new IdentityError
            {
                Code = nameof(ConcurrencyFailure),
                Description = "Wystąpił błąd jednoczesnej modyfikacji."
            };
        }

        public override IdentityError PasswordRequiresDigit()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresDigit),
                Description = "Hasło musi zawierać co najmniej jedną cyfrę (0-9)."
            };
        }

        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresNonAlphanumeric),
                Description = "Hasło musi zawierać co najmniej jeden znak specjalny."
            };
        }

        public override IdentityError PasswordTooShort(int length)
        {
            return new IdentityError
            {
                Code = nameof(PasswordTooShort),
                Description = $"Hasło musi mieć co najmniej {length} znaków."
            };
        }

        public override IdentityError PasswordRequiresLower()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresLower),
                Description = "Hasło musi zawierać co najmniej jedną małą literę."
            };
        }

        public override IdentityError PasswordRequiresUpper()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresUpper),
                Description = "Hasło musi zawierać co najmniej jedną dużą literę."
            };
        }

        public override IdentityError PasswordMismatch()
        {
            return new IdentityError
            {
                Code = nameof(PasswordMismatch),
                Description = "Hasło jest niepoprawne."
            };
        }

        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError
            {
                Code = nameof(DuplicateUserName),
                Description = $"Nazwa użytkownika '{userName}' jest już zajęta."
            };
        }

        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError
            {
                Code = nameof(DuplicateEmail),
                Description = $"Adres e-mail '{email}' jest już zajęty."
            };
        }

        public override IdentityError InvalidEmail(string email)
        {
            return new IdentityError
            {
                Code = nameof(InvalidEmail),
                Description = $"Adres e-mail '{email}' jest nieprawidłowy."
            };
        }
    }
}
