namespace Agree.Accord.Presentation.Identity.Controllers;

using System;
using System.Linq;
using System.Threading.Tasks;
using Agree.Accord.Domain.Identity;
using Agree.Accord.Domain.Identity.Commands;
using Agree.Accord.Domain.Identity.Results;
using Agree.Accord.Domain.Identity.Services;
using Agree.Accord.Domain.Providers;
using Agree.Accord.Presentation.Identity.ViewModels;
using Agree.Accord.Presentation.Responses;
using Agree.Accord.SharedKernel;
using Agree.Accord.SharedKernel.Data;
using MediatR;
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
    private readonly UserManager<UserAccount> _userManager;
    private readonly SignInManager<UserAccount> _signInManager;
    private readonly TokenService _tokenService;
    private readonly IMailProvider _mailProvider;
    private readonly IMediator _mediator;

    public AccountController(
        UserManager<UserAccount> userManager,
        SignInManager<UserAccount> signInManager,
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
    public async Task<IActionResult> Register([FromBody] CreateAccountCommand command)
    {
        var result = await _mediator.Send<CreateAccountResult>(command);

        if (result.Failed)
            return BadRequest(new ValidationErrorResponse(result.Error));

        var (account, token) = result.Data;

        var userViewModel = UserAccountViewModel.FromEntity(account);

        Response.Cookies.Append(AccessTokenCookieName, token.Token, new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.Strict
        });

        return Created(
            Url.Link("GetAccountById", new { account.Id }),
            new RegisterResponse(userViewModel));
    }

    [HttpGet]
    [Route("@me")]
    [Authorize]
    public async Task<IActionResult> Me()
    {
        var entity = await _accountService.GetAccountByIdAsync(CurrentlyLoggedUser.Id);
        return Ok(new UserResponse(UserAccountViewModel.FromEntity(entity)));
    }

    [HttpGet]
    [Route("{id:guid}", Name = "GetAccountById")]
    [Authorize]
    public async Task<IActionResult> Show([FromRoute] Guid id)
    {
        var entity = await _accountService.GetAccountByIdAsync(id);
        return entity == null ? NotFound() : Ok(new UserResponse(UserAccountViewModel.FromEntity(entity)));
    }

    [HttpGet]
    [Route("")]
    [Authorize]
    public async Task<IActionResult> Index([FromQuery] SearchAccountsCommand search)
    {
        var entities = await _accountService.SearchUsers(search);
        return Ok(new { users = entities.Select(UserAccountViewModel.FromEntity) });
    }
}