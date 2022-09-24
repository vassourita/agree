namespace Agree.Accord.Presentation.Identity.Controllers;

using System;
using System.Linq;
using System.Threading.Tasks;
using Agree.Accord.Domain.Identity;
using Agree.Accord.Domain.Identity.Dtos;
using Agree.Accord.Domain.Identity.Services;
using Agree.Accord.Domain.Providers;
using Agree.Accord.Presentation.Identity.ViewModels;
using Agree.Accord.Presentation.Responses;
using Agree.Accord.SharedKernel;
using Agree.Accord.SharedKernel.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// A controller for managing user accounts.
/// </summary>
[ApiController]
[Route("api/identity/accounts")]
[Authorize]
public class AccountController : CustomControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly TokenService _tokenService;
    private readonly IMailProvider _mailProvider;

    public AccountController(
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
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] CreateAccountDto createAccountDto)
    {
        try
        {
            var validationResult = AnnotationValidator.TryValidate(createAccountDto);

            if (validationResult.Failed)
            {
                return BadRequest(validationResult.Error.ToErrorList());
            }

            var user = new ApplicationUser
            {
                EmailAddress = createAccountDto.Email,
                Username = createAccountDto.Username,
                Tag = await _accountService.GenerateDiscriminatorTagAsync(createAccountDto.Username),
                EmailConfirmed = false,
                CreatedAt = DateTime.UtcNow
            };

            var result = await _userManager.CreateAsync(user, createAccountDto.Password);

            // if (!result.Succeeded)
            // return BadRequest(result.Errors.ToErrorList());

            var mailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationUrl = Url.Link("ConfirmEmail", new { token = mailToken, id = user.Id });
            await _mailProvider.SendMailAsync(
                user.EmailAddress,
                "Agree - Welcome",
                $"<html><body>Welcome to Agree! Please click <a href=\"{confirmationUrl}\">HERE</a> to confirm your email.</body></html>");
            await _signInManager.SignInAsync(user, false);

            var userViewModel = ApplicationUserViewModel.FromEntity(user);

            var token = await _tokenService.GenerateAccessTokenAsync(user);

            await _signInManager.SignInAsync(user, false);

            Response.Cookies.Append(AccessTokenCookieName, token.Token, new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Strict
            });

            return Created(
                Url.Link("GetAccountById", new { user.Id }),
                new RegisterResponse(userViewModel));
        }
        catch
        {
            return InternalServerError();
        }
    }

    [HttpGet]
    [Route("@me")]
    [Authorize]
    public async Task<IActionResult> Me()
    {
        var entity = await _accountService.GetAccountByIdAsync(CurrentlyLoggedUser.Id);
        return Ok(new UserResponse(ApplicationUserViewModel.FromEntity(entity)));
    }

    [HttpGet]
    [Route("{id:guid}", Name = "GetAccountById")]
    [Authorize]
    public async Task<IActionResult> Show([FromRoute] Guid id)
    {
        var entity = await _accountService.GetAccountByIdAsync(id);

        return entity == null ? NotFound() : Ok(new UserResponse(ApplicationUserViewModel.FromEntity(entity)));
    }

    [HttpGet]
    [Route("")]
    [Authorize]
    public async Task<IActionResult> Index([FromQuery] SearchAccountsDto search)
    {
        var entities = await _accountService.SearchUsers(search);

        return Ok(new { users = entities.Select(ApplicationUserViewModel.FromEntity) });
    }
}