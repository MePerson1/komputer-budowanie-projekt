using KomputerBudowanieAPI.Dto;
using KomputerBudowanieAPI.Models;
using KomputerBudowanieAPI.Services;
using Microsoft.AspNetCore.Authorization;
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
        private readonly JwtTokenHandler _jwtTokenHandler;

        public UserController(UserManager<ApplicationUser> user, IConfiguration configuration, JwtTokenHandler jwtTokenHandler)
        {
            _user = user;
            _jwtTokenHandler = jwtTokenHandler;
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
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.UserName)
                };

                var tokenLiftime = DateTime.UtcNow.AddMinutes(60);
                foreach (var role in await _user.GetRolesAsync(user))
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                    if(role == "Scraper")
                    {
                        tokenLiftime = DateTime.UtcNow.AddHours(12);
                    }
                }

                var token = _jwtTokenHandler.GenerateToken(claims, tokenLiftime);

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
            //var user = await _user.FindByEmailAsync(dto.Email);
            //await _user.AddToRoleAsync(user, "Admin");

            if (!result.Succeeded)
            {
                return BadRequest(result);
            }

            return Ok();
        }

        [Authorize]
        [HttpGet("userinfo")]
        public async Task<IActionResult> GetUserInfo()
        {
            var user = HttpContext.User;
            
            var userInfo = new UserDto()
            {
                Id = GetClaimValue(user, ClaimTypes.NameIdentifier), //Tu zamiast JwtRegisteredClaimNames.Sub musi być ClaimTypes.NameIdentifier. Jakieś dziwactwo ASP.NET Core Identity.
                NickName = GetClaimValue(user, ClaimTypes.Name),
                Email = GetClaimValue(user, ClaimTypes.Email)
            };
             
            return Ok(userInfo);
        }

        private string GetClaimValue(ClaimsPrincipal user, string claimType)
        {
            var claim = user.FindFirst(claimType);
            return claim?.Value ?? "";
        }
    }
}
