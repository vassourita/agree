using System.Linq;
using System;
using System.Net;
using System.Threading.Tasks;
using Agree.Athens.Application.Dtos;
using Agree.Athens.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Agree.Athens.Application.Services;
using Agree.Athens.Presentation.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Agree.Athens.Application.Security;

namespace Agree.Athens.Presentation.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : CustomBaseController
    {
        private readonly AccountService _accountService;
        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] CreateAccountDto createAccountDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var confirmationUrl = Url.Link("ConfirmEmail", new { token = "" });
                await _accountService.Register(createAccountDto, confirmationUrl);
                return Ok(new Response("Account successfully created"));
            }
            catch (BaseDomainException ex)
            {
                if (ex is DomainValidationException validationException)
                {
                    return BadRequest(ErrorResponse.FromException(validationException));
                }
                return InternalServerError(new Response(ex.Message));
            }
            catch (Exception ex)
            {
                return InternalServerError(new Response(ex.Message));
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("ConfirmEmail", Name = "ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] Guid token)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _accountService.ConfirmEmail(token);
                return Ok(new Response("Account successfully verified"));
            }
            catch (BaseDomainException ex)
            {
                if (ex is EntityNotFoundException notFoundException)
                {
                    return NotFound(new Response(notFoundException.Message));
                }
                return InternalServerError(new Response(ex.Message));
            }
            catch (Exception ex)
            {
                return InternalServerError(new Response(ex.Message));
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (loginDto.GrantType == GrantTypes.Password)
                {
                    loginDto.IpAddress = HttpContext.Connection.RemoteIpAddress.ToString();
                    var (accessToken, refreshToken) = await _accountService.Login(loginDto);
                    return Ok(new LoginResponse(accessToken, refreshToken));
                }
                if (loginDto.GrantType == GrantTypes.RefreshToken)
                {
                    loginDto.IpAddress = HttpContext.Connection.RemoteIpAddress.ToString();
                    if (loginDto.RefreshToken is null)
                    {
                        loginDto.RefreshToken = Request.Headers["x-refresh-token"];
                    }
                    var (accessToken, refreshToken) = await _accountService.RefreshTokens(loginDto);
                    return Ok(new LoginResponse(accessToken, refreshToken));
                }
                return BadRequest(new Response("Invalid login grant type. Must be one of ('password'/'refresh_token')"));
            }
            catch (BaseDomainException ex)
            {
                if (ex is EntityNotFoundException notFoundException)
                {
                    return NotFound(new Response(notFoundException.Message));
                }
                if (ex is DomainUnauthorizedException unauthorizedException)
                {
                    return Unauthorized(new Response(unauthorizedException.Message));
                }
                if (ex is DomainValidationException validationException)
                {
                    return BadRequest(ErrorResponse.FromException(validationException));
                }
                return InternalServerError(new Response(ex.Message));
            }
            catch (Exception ex)
            {
                return InternalServerError(new Response(ex.Message));
            }
        }

        [HttpGet]
        [Authorize]
        [Route("Me")]
        public IActionResult Me()
        {
            try
            {
                var user = CurrentlyLoggedUser;
                return Ok(new UserResponse(CurrentlyLoggedUser));
            }
            catch (Exception ex)
            {
                return InternalServerError(new Response(ex.Message));
            }
        }

        [HttpPut]
        [Authorize]
        [Route("")]
        public async Task<IActionResult> Update([FromBody] UpdateAccountDto updateAccountDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                updateAccountDto.UserId = CurrentlyLoggedUser.Id;
                return Ok(new UserResponse(await _accountService.UpdateAccount(updateAccountDto), "Account updated successfully"));
            }
            catch (BaseDomainException ex)
            {
                if (ex is DomainValidationException validationException)
                {
                    return BadRequest(ErrorResponse.FromException(validationException));
                }
                if (ex is EntityNotFoundException notFoundException)
                {
                    return NotFound(new Response(notFoundException.Message));
                }
                if (ex is DomainUnauthorizedException unauthorizedException)
                {
                    return Unauthorized(new Response(unauthorizedException.Message));
                }
                return InternalServerError(new Response(ex.Message));
            }
            catch (Exception ex)
            {
                return InternalServerError(new Response(ex.Message));
            }
        }
    }
}