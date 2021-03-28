using System;
using System.Threading.Tasks;
using Agree.Athens.Domain.Dtos;
using Agree.Athens.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Agree.Athens.Application.Services;
using Agree.Athens.Presentation.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Agree.Athens.Application.Security;
using AutoMapper;
using Agree.Athens.Application.ViewModels;

namespace Agree.Athens.Presentation.WebApi.Controllers
{
    [ApiController]
    [Route("api/Accounts")]
    public class AccountController : CustomBaseController
    {
        private readonly AccountService _accountService;
        private readonly IMapper _mapper;

        public AccountController(AccountService accountService, IMapper mapper) : base(accountService)
        {
            _accountService = accountService;
            _mapper = mapper;
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
                return HandleDomainException(ex);
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
                return HandleDomainException(ex);
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
                loginDto.IpAddress = HttpContext.Connection.RemoteIpAddress.ToString();
                if (loginDto.GrantType == "refresh_token" && loginDto.RefreshToken is null)
                {
                    loginDto.RefreshToken = Request.Headers["x-refresh-token"];
                }
                var (accessToken, refreshToken) = await _accountService.Login(loginDto);
                return Ok(new LoginResponse(accessToken, refreshToken));
            }
            catch (BaseDomainException ex)
            {
                return HandleDomainException(ex);
            }
            catch (Exception ex)
            {
                return InternalServerError(new Response(ex.Message));
            }
        }

        [HttpGet]
        [Authorize]
        [Route("@Me")]
        public async Task<IActionResult> Me()
        {
            try
            {
                var user = _mapper.Map<AccountViewModel>(await GetAuthenticatedUserAccount());
                return Ok(new UserResponse(user));
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
                return HandleDomainException(ex);
            }
            catch (Exception ex)
            {
                return InternalServerError(new Response(ex.Message));
            }
        }
    }
}