using KomputerBudowanieAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace KomputerBudowanieAPI.Interfaces
{
    public interface IJwtTokenHandler
    {
        JwtSecurityToken GenerateToken(IEnumerable<Claim> claims, DateTime lifeTime);

        JwtSecurityToken GenerateTokenForUserWithClaims(ApplicationUser user, IList<string> userRoles);

        ClaimsPrincipal? GetPrincipalFromToken(string token);

        TokenValidationParameters GetTokenValidationParameters();
    }
}
