using System.Web;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Agree.Allow.Infrastructure.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Agree.Allow.Domain.Security;
using Agree.Allow.Domain.Services;
using Agree.Allow.Domain.Dtos;
using AutoMapper;
using Agree.Allow.Presentation.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using Agree.Allow.Presentation.Responses;

namespace Agree.Allow.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : CustomBaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITagService _tagService;
        private readonly IMailService _mailService;
        private readonly IMapper _mapper;
        private readonly TokenConfiguration _tokenConfiguration;
        private readonly FrontendConfiguration _frontendConfiguration;

        public AccountsController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ITagService tagService,
            IMailService mailService,
            IMapper mapper,
            IOptions<TokenConfiguration> tokenConfiguration,
            IOptions<FrontendConfiguration> frontendConfiguration)
            : base(userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tagService = tagService;
            _mailService = mailService;
            _mapper = mapper;
            _tokenConfiguration = tokenConfiguration.Value;
            _frontendConfiguration = frontendConfiguration.Value;
        }

        [HttpPost]
        [Route("Register")]
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

            if (!result.Succeeded) return BadRequest(new { result.Errors });

            var mailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationUrl = Url.Link("ConfirmEmail", new { token = mailToken, email = user.Email });
            await _mailService.SendMailAsync(
                user.Email,
                "Agree - Welcome",
                $"<html><body>Welcome to Agree! Please click <a href=\"{confirmationUrl}\">HERE</a> to confirm your email.</body></html>");
            await _signInManager.SignInAsync(user, false);

            var userViewModel = _mapper.Map<ApplicationUserViewModel>(user);

            Response.Cookies.Append("agreeallow_accesstoken", await GenerateJwt(user.Email), new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Strict
            });

            return Created(
                Url.Link("GetById", new { Id = user.Id }),
                new UserResponse(userViewModel));
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

            Response.Cookies.Append("agreeallow_accesstoken", await GenerateJwt(loginUser.Email), new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Strict
            });

            return Ok();
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
                user.Email != updateAccountDto.Email &&
                await _userManager.Users.FirstOrDefaultAsync(u => u.Email == updateAccountDto.Email) == null)
            {
                user.EmailConfirmed = false;
                user.Email = updateAccountDto.Email;
                var mailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmationUrl = Url.Link("ConfirmEmail", new { token = mailToken, email = user.Email });
                await _mailService.SendMailAsync(
                    user.Email,
                    "Agree - Confirmation",
                    $"<html><body>Hello, {user.DisplayName}#{user.Tag.ToString().PadLeft(4, '0')}. Please click <a href=\"{confirmationUrl}\">HERE</a> to confirm your new email.</body></html>");
            }

            if (!string.IsNullOrEmpty(updateAccountDto.UserName) &&
                user.DisplayName != updateAccountDto.UserName)
            {
                var userWithSameTagAndName = await _userManager.Users.FirstOrDefaultAsync(
                    u => u.DisplayName == updateAccountDto.UserName &&
                    u.Tag == user.Tag);

                if (userWithSameTagAndName == null)
                {
                    user.DisplayName = updateAccountDto.UserName;
                }
            }

            if (user.Tag != updateAccountDto.Tag)
            {
                var userWithSameTagAndName = await _userManager.Users.FirstOrDefaultAsync(
                    u => u.DisplayName == user.DisplayName &&
                    u.Tag == updateAccountDto.Tag);

                if (userWithSameTagAndName == null)
                {
                    user.Tag = updateAccountDto.Tag;

                    if (!string.IsNullOrEmpty(updateAccountDto.UserName) &&
                        user.DisplayName != updateAccountDto.UserName)
                    {
                        userWithSameTagAndName = await _userManager.Users.FirstOrDefaultAsync(
                            u => u.DisplayName == updateAccountDto.UserName &&
                            u.Tag == user.Tag);

                        if (userWithSameTagAndName == null)
                        {
                            user.DisplayName = updateAccountDto.UserName;
                        }
                    }
                }
            }

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded) return BadRequest(new { result.Errors });

            var userViewModel = _mapper.Map<ApplicationUserViewModel>(user);

            Response.Cookies.Append("agreeallow_accesstoken", await GenerateJwt(user.Email), new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Strict
            });

            return Ok(new UserResponse(userViewModel));
        }

        [HttpPost]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("agreeallow_accesstoken");
            return NoContent();
        }

        [HttpGet]
        [Route("ConfirmEmail", Name = "ConfirmEmail")]
        public async Task<ActionResult> ConfirmEmail([FromQuery] string token, [FromQuery] string email)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == email);

            var result = await _userManager.ConfirmEmailAsync(user, token);

            string uiUrl = result.Succeeded
                ? $"{_frontendConfiguration.MailConfirmationBaseUrl}?mailVerifiedOk=true&mailVerified={HttpUtility.UrlEncode(user.Email)}"
                : $"{_frontendConfiguration.MailConfirmationBaseUrl}?mailVerifiedOk=false&mailVerified={HttpUtility.UrlEncode(user.Email)}";

            return Redirect(uiUrl);
        }

        [HttpPost]
        [Route("ResendConfirmationEmail")]
        [Authorize]
        public async Task<ActionResult> ResendConfirmationEmail()
        {
            var user = await GetAuthenticatedUserAccount();
            if (user.EmailConfirmed)
            {
                return BadRequest();
            }
            var mailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationUrl = Url.Link("ConfirmEmail", new { token = mailToken, email = user.Email });
            await _mailService.SendMailAsync(
                user.Email,
                "Agree - Confirmation",
                $"<html><body>Hello, {user.DisplayName}#{user.Tag.ToString().PadLeft(4, '0')}. Please click <a href=\"{confirmationUrl}\">HERE</a> to confirm your new email.</body></html>");

            return Ok();
        }

        [HttpGet]
        [Route("@Me")]
        [Authorize]
        public async Task<ActionResult> Me()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(x => x.Errors));

            var userViewModel = _mapper.Map<ApplicationUserViewModel>(await GetAuthenticatedUserAccount());

            return Ok(new UserResponse(userViewModel));
        }

        [HttpGet]
        [Route("{id}", Name = "GetById")]
        [Authorize]
        public async Task<ActionResult> Show([FromRoute] Guid id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(x => x.Errors));

            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null)
            {
                return NotFound();
            }

            var userViewModel = _mapper.Map<ApplicationUserViewModel>(await GetAuthenticatedUserAccount());

            return Ok(new UserResponse(userViewModel));
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