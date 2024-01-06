using KomputerBudowanieAPI.Interfaces;
using KomputerBudowanieAPI.Models;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KomputerBudowanieAPI.Services
{
    public class JwtTokenHandler : IJwtTokenHandler
    {
        private readonly string _issuer;
        private readonly string _audience;
        private readonly byte[] _secretKey;
        private readonly TokenValidationParameters _validationParameters;

        public JwtTokenHandler(string issuer, string audience, string secretKey)
        {
            _issuer = issuer;
            _audience = audience;
            _secretKey = System.Text.Encoding.UTF8.GetBytes(secretKey);
            _validationParameters = new TokenValidationParameters
            {
                ValidIssuer = _issuer,
                ValidAudience = _audience,
                IssuerSigningKey = new SymmetricSecurityKey(_secretKey),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true
            };
        }

        public JwtSecurityToken GenerateToken(IEnumerable<Claim> claims, DateTime lifeTime)
        {
            var key = new SymmetricSecurityKey(_secretKey);
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: lifeTime,
                signingCredentials: creds
            );

            return token;
        }

        public JwtSecurityToken GenerateTokenForUserWithClaims(ApplicationUser user, IList<string> userRoles)
        {
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var tokenLiftime = DateTime.UtcNow.AddMinutes(60);
            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
                if (role == "Scraper")
                {
                    tokenLiftime = DateTime.UtcNow.AddHours(12);
                }
            }

            return GenerateToken(claims, tokenLiftime);
        }

        public ClaimsPrincipal? GetPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var principal = tokenHandler.ValidateToken(token, _validationParameters, out var securityToken);

                return principal;
            }
            catch
            {
                return null;
            }
        }

        public TokenValidationParameters GetTokenValidationParameters()
        {
            return _validationParameters;
        }
    }
}
