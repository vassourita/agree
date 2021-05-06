using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Agree.Allow.Configuration;
using Agree.Allow.Dtos;
using Agree.Allow.Models;
using Agree.Allow.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Agree.Allow.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : CustomBaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly TagService _tagService;
        private readonly TokenConfiguration _tokenConfiguration;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            TagService tagService,
            IOptions<TokenConfiguration> tokenConfiguration)
            : base(userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tagService = tagService;
            _tokenConfiguration = tokenConfiguration.Value;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(x => x.Errors));

            var user = new ApplicationUser
            {
                Email = registerDto.Email,
                UserName = registerDto.Email,
                DisplayName = registerDto.UserName,
                Tag = await _tagService.GenerateTag(registerDto.UserName),
                EmailConfirmed = false
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded) return BadRequest(result.Errors);

            await _signInManager.SignInAsync(user, false);

            return Ok();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login(LoginDto loginUser)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(x => x.Errors));

            var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, true);

            if (result.Succeeded)
            {
                return Ok(new { AccessToken = await GenerateJwt(loginUser.Email) });
            }

            return BadRequest(new { Message = "Email or Password are incorrect" });
        }

        [HttpGet]
        [Route("@Me")]
        [Authorize]
        public ActionResult Me()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(x => x.Errors));

            return BadRequest(new { User = CurrentlyLoggedUser });
        }

        private async Task<string> GenerateJwt(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_tokenConfiguration.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                     new Claim(ClaimTypes.Name, $"{user.DisplayName}#{user.Tag.ToString().PadLeft(4, '0')}"),
                     new Claim(ClaimTypes.Email, user.Email),
                     new Claim(ClaimTypes.Role, "user"),
                     new Claim("id", user.Id.ToString()),
                }),
                Issuer = _tokenConfiguration.Issuer,
                Audience = _tokenConfiguration.Audience,
                Expires = DateTime.UtcNow.AddHours(_tokenConfiguration.ExpiresInMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }
    }
}