using Microsoft.IdentityModel.Tokens;
using NuGet.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KomputerBudowanieAPI.Services
{
    public class JwtTokenHandler
    {
        private readonly string _issuer;
        private readonly string _audience;
        private readonly byte[] _secretKey;

        public JwtTokenHandler(string issuer, string audience, string secretKey)
        {
            _issuer = issuer;
            _audience = audience;
            _secretKey = System.Text.Encoding.UTF8.GetBytes(secretKey);
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

        public ClaimsPrincipal? GetPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var principal = tokenHandler.ValidateToken(token, GetTokenValidationParameters(), out var securityToken);

                return principal;
            }
            catch
            {
                return null;
            }
        }

        public TokenValidationParameters GetTokenValidationParameters()
        {
            return new TokenValidationParameters
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
    }
}
