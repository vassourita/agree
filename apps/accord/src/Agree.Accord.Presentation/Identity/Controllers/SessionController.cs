namespace Agree.Accord.Presentation.Identity.Controllers;

using System.Threading.Tasks;
using Agree.Accord.Domain.Identity;
using Agree.Accord.Domain.Identity.Dtos;
using Agree.Accord.Domain.Identity.Services;
using Agree.Accord.Domain.Providers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// A controller that handles user login and logout operations.
/// </summary>
[ApiController]
[AllowAnonymous]
[Route("api/identity/sessions")]
public class SessionController : CustomControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly TokenService _tokenService;
    private readonly IMailProvider _mailProvider;

    public SessionController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        TokenService tokenService,
        IMailProvider mailProvider,
        AccountService accountService) : base(accountService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _mailProvider = mailProvider;
    }

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        var result = await _signInManager.PasswordSignInAsync(loginDto.Email, loginDto.Password, false, true);

        if (!result.Succeeded)
        {
            return BadRequest();
        }

        var token = await _tokenService.GenerateAccessTokenAsync(loginDto.Email);

        Response.Cookies.Append(AccessTokenCookieName, token.Token, new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.Strict
        });

        return NoContent();
    }

    [HttpDelete]
    [Route("")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        Response.Cookies.Delete(AccessTokenCookieName);
        return NoContent();
    }
}