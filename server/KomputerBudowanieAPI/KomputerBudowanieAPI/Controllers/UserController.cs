using KomputerBudowanieAPI.Dto;
using KomputerBudowanieAPI.Models;
using KomputerBudowanieAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;
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
                //var token = await GenerateTokenForUserWithClaims(user);
                var token = _jwtTokenHandler.GenerateTokenForUserWithClaims(user, await _user.GetRolesAsync(user));

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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
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
            //var user = await _user.FindByEmailAsync(dto.Email);
            //await _user.AddToRoleAsync(user, "Admin");

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
                NickName = GetClaimValue(user, ClaimTypes.Name),
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
                return NotFound("Użytkownik nieznaleziony!");
            }

            var changePasswordResult = await _user.ChangePasswordAsync(user, dto.CurrentPassword, dto.NewPassword);

            if (changePasswordResult.Succeeded)
            {
                return Ok("Hasło zostało zmienione!");
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
                return NotFound("Użytkownik nieznaleziony!");
            }

            var isPasswordValid = await _user.CheckPasswordAsync(user, dto.CurrentPassword);

            if (!isPasswordValid)
            {
                return BadRequest("Niepoprawne hasło!");
            }

            user.Email = dto.NewEmail;
            user.NormalizedEmail = dto.NewEmail.ToUpper();

            var updateResult = await _user.UpdateAsync(user);

            if (updateResult.Succeeded)
            {
                return Ok("Email został zmieniony!");
            }
            else
            {
                return BadRequest(updateResult.Errors);
            }
        }
    }
}
