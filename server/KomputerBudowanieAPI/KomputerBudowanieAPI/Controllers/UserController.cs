using KomputerBudowanieAPI.Dto;
using KomputerBudowanieAPI.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KomputerBudowanieAPI.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _user;
        private readonly IConfiguration _configuration;

        public UserController(UserManager<ApplicationUser> user, IConfiguration configuration)
        {
            _user = user;
            _configuration = configuration;
        }

        [HttpPost("token")]
        public async Task<IActionResult> Token([FromBody] LoginUserDto dto)
        {
            var user = await _user.FindByEmailAsync(dto.Email);
            var result = await _user.CheckPasswordAsync(user, dto.Password);
            if (result)
            {
                var claims = new List<Claim>()
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                };
                foreach (var role in await _user.GetRolesAsync(user))
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    issuer: _configuration["Tokens:Issuer"],
                    audience: _configuration["Tokens:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(60),
                    signingCredentials: creds
                );
                return Ok(
                    new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo
                    }
                );
            }
            else
            {
                return BadRequest("Niepoprawnie podane hasło lub email!");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {
            var existingUser = await _user.FindByEmailAsync(dto.Email);
            if (existingUser != null)
            {
                return BadRequest("User with this email already exists!");
            }
            existingUser = await _user.FindByNameAsync(dto.NickName);
            if (existingUser != null)
            {
                return BadRequest("User with this nickname already exists!");
            }

            var newUser = new ApplicationUser
            {
                UserName = dto.NickName,
                Email = dto.Email,
            };

            var result = await _user.CreateAsync(newUser, dto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result);
            }

            return Ok();
        }
    }
}
