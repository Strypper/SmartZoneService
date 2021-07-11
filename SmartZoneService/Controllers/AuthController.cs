using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SmartZone.Contracts;
using SmartZone.DataObjects;
using SmartZone.Entities;
using SmartZone.Repositories;
using SmartZoneService.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SmartZoneService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IOptionsMonitor<ApplicationSettings> _tokenConfigOptionsAccessor;

        public AuthController(UserManager userManager, SignInManager<User> signInManager, IOptionsMonitor<ApplicationSettings> tokenConfigOptionsAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenConfigOptionsAccessor = tokenConfigOptionsAccessor;
        }


        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user is null)
            {
                return BadRequest(new { message = "Username is incorrect" });
            }

            var passwordCheck = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!passwordCheck.Succeeded)
            {
                return BadRequest(new { message = "Password is incorrect" });
            }

            var tokenConfig = _tokenConfigOptionsAccessor.CurrentValue;
            var token = await GenerateToken(user, tokenConfig);
            var refresh_token = Guid.NewGuid().ToString().Replace("-", "");

            var requestAt = DateTime.UtcNow;
            var expiresIn = Math.Floor((requestAt.AddDays(1) - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds);

            return Ok(new
            {
                requestAt,
                expiresIn,
                accessToken = token,
                refresh_token,
            });
        }

        private async Task<string> GenerateToken(User user, ApplicationSettings tokenConfig)
        {
            var handler = new JwtSecurityTokenHandler();

            var roles = await _userManager.GetRolesAsync(user);

            var identity = new ClaimsIdentity(
                new GenericIdentity(user.UserName, "TokenAuth"),
                new[] { new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), new Claim("id", user.Id.ToString()) }
                    .Union(roles.Select(role => new Claim(ClaimTypes.Role, role)))
                );

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfig.JWT_Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = credentials
            };

            var securityToken = handler.CreateToken(tokenDescriptor);
            return handler.WriteToken(securityToken);
        }
    }
}
