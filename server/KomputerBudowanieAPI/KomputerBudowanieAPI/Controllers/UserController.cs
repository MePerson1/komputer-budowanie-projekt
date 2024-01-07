using KomputerBudowanieAPI.Dto;
using KomputerBudowanieAPI.Interfaces;
using KomputerBudowanieAPI.Models;
using KomputerBudowanieAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace KomputerBudowanieAPI.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _user;
        private readonly IJwtTokenHandler _jwtTokenHandler;

        public UserController(UserManager<ApplicationUser> user, IJwtTokenHandler jwtTokenHandler)
        {
            _user = user;
            _jwtTokenHandler = jwtTokenHandler;
        }

        [HttpPost("token")]
        public async Task<IActionResult> Token([FromBody] LoginUserDto dto)
        {
            var user = await _user.FindByEmailAsync(dto.Email);
            var result = await _user.CheckPasswordAsync(user, dto.Password);
            if (!result)
            {
                return BadRequest("Niepoprawnie podane hasło lub email.");
            }

            var token = _jwtTokenHandler.GenerateTokenForUserWithClaims(user, await _user.GetRolesAsync(user));

            return Ok(
                new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                }
            );
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingUser = await _user.FindByEmailAsync(dto.Email);
            if (existingUser != null)
            {
                return BadRequest("Użytkownik z tym emailem już istnieje.");
            }
            existingUser = await _user.FindByNameAsync(dto.UserName);
            if (existingUser != null)
            {
                return BadRequest("Użytkownik z taką nazwą już istnieje.");
            }

            var newUser = new ApplicationUser
            {
                UserName = dto.UserName,
                Email = dto.Email,
            };

            var result = await _user.CreateAsync(newUser, dto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            var user = await _user.FindByEmailAsync(dto.Email);
            await _user.AddToRoleAsync(user, "Scraper");

            //var user = await _user.FindByEmailAsync(dto.Email);

            //var token = await GenerateTokenForUserWithClaims(newUser);
            var token = _jwtTokenHandler.GenerateTokenForUserWithClaims(newUser, await _user.GetRolesAsync(newUser));

            return Ok(
                new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                }
            );
        }

        [Authorize]
        [HttpGet("userinfo")]
        public async Task<IActionResult> GetUserInfo()
        {
            var user = HttpContext.User;

            var userInfo = new UserDto()
            {
                Id = GetClaimValue(user, ClaimTypes.NameIdentifier), //Tu zamiast JwtRegisteredClaimNames.Sub musi być ClaimTypes.NameIdentifier. Jakieś dziwactwo ASP.NET Core Identity.
                UserName = GetClaimValue(user, ClaimTypes.Name),
                Email = GetClaimValue(user, ClaimTypes.Email)
            };

            return Ok(userInfo);
        }

        private static string GetClaimValue(ClaimsPrincipal user, string claimType)
        {
            var claim = user.FindFirst(claimType);
            return claim?.Value ?? "";
        }

        [Authorize]
        [HttpPut("changepassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangeUserPasswordDto dto)
        {
            var user = await _user.FindByNameAsync(User.Identity?.Name);

            if (user == null)
            {
                return NotFound("Nie udało się znaleźć użytkownika.");
            }

            var changePasswordResult = await _user.ChangePasswordAsync(user, dto.CurrentPassword, dto.NewPassword);

            if (changePasswordResult.Succeeded)
            {
                return Ok("Hasło zostało zmienione.");
            }
            else
            {
                return BadRequest(changePasswordResult.Errors);
            }
        }

        [Authorize]
        [HttpPut("changeemail")]
        public async Task<IActionResult> ChangeEmail([FromBody] ChangeUserEmailDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _user.FindByNameAsync(User.Identity?.Name);

            if (user == null)
            {
                return NotFound("Nie udało się znaleźć użytkownika.");
            }

            var isPasswordValid = await _user.CheckPasswordAsync(user, dto.CurrentPassword);

            if (!isPasswordValid)
            {
                return BadRequest("Niepoprawne hasło.");
            }

            user.Email = dto.NewEmail;
            user.NormalizedEmail = dto.NewEmail.ToUpper();

            var updateResult = await _user.UpdateAsync(user);

            if (updateResult.Succeeded)
            {
                return Ok("Email został zmieniony.");
            }
            else
            {
                return BadRequest(updateResult.Errors);
            }
        }
    }
}
