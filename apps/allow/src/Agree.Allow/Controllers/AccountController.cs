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
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
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

            return Ok(new { AccessToken = await GenerateJwt(user.Email) });
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login(LoginDto loginUser)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(x => x.Errors));

            var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, true);

            if (!result.Succeeded)
            {
                return BadRequest(new { Message = "Email or Password are incorrect" });
            }

            return Ok(new { AccessToken = await GenerateJwt(loginUser.Email) });
        }

        [HttpPut]
        [Route("")]
        [Authorize]
        public async Task<ActionResult> Update(UpdateAccountDto updateAccountDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(x => x.Errors));

            var user = await GetAuthenticatedUserAccount();

            if (user == null)
            {
                return NotFound(new { Message = "User account not found" });
            }

            if (!string.IsNullOrEmpty(updateAccountDto.Email) &&
                user.Email != updateAccountDto.Email)
            {
                var emailToken = await _userManager.GenerateChangeEmailTokenAsync(user, updateAccountDto.Email);
            }

            if (!string.IsNullOrEmpty(updateAccountDto.UserName) &&
                user.DisplayName != updateAccountDto.UserName)
            {
                user.DisplayName = updateAccountDto.UserName;
            }

            if (user.Tag != updateAccountDto.Tag)
            {
                var userWithSameTagAndName
                    = await _userManager.Users.FirstOrDefaultAsync(
                        u => u.DisplayName == user.DisplayName &&
                        u.Tag == updateAccountDto.Tag);

                if (userWithSameTagAndName == null)
                {
                    user.Tag = updateAccountDto.Tag;
                }
            }

            await _userManager.UpdateAsync(user);

            return Ok(new { AccessToken = await GenerateJwt(user.Email), User = user.ToViewModel() });
        }

        [HttpGet]
        [Route("ConfirmEmail")]
        [Authorize]
        public async Task<ActionResult> ConfirmEmail([FromQuery] string token, [FromQuery] string email, [FromQuery] string newEmail)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == email);

            var result = await _userManager.ChangeEmailAsync(user, newEmail, token);

            if (!result.Succeeded)
            {
                return BadRequest(new { Message = "We could not verify your email. Please try again by clicking in the link we sent on your email", Verified = false });
            }

            return Ok(new { Message = "Email verified successfully", Verified = true });
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
                     new Claim("verified", user.EmailConfirmed.ToString().ToLower()),
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