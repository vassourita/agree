using System;
using System.Net;
using System.Threading.Tasks;
using Agree.Athens.Application.Dtos;
using Agree.Athens.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Agree.Athens.Application.Services;
using Agree.Athens.Presentation.WebApi.Models;

namespace Agree.Athens.Presentation.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;
        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult> Register([FromBody] CreateAccountDto createAccountDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var confirmationUrl = Url.Link("ConfirmEmail", new { token = "" });
                await _accountService.Register(createAccountDto, confirmationUrl);
                return Ok(new Response("Account succesfully created"));
            }
            catch (BaseDomainException ex)
            {
                if (ex is DomainValidationException validationException)
                {
                    return BadRequest(ErrorResponse.FromException(validationException));
                }
                return StatusCode((int)HttpStatusCode.InternalServerError, new Response(ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new Response(ex.Message));
            }
        }

        [HttpGet]
        [Route("ConfirmEmail", Name = "ConfirmEmail")]
        public async Task<ActionResult> ConfirmEmail([FromQuery] Guid token)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _accountService.ConfirmEmail(token);
                return Ok(new Response("Account succesfully verified"));
            }
            catch (BaseDomainException ex)
            {
                if (ex is EntityNotFoundException notFoundException)
                {
                    return NotFound(new Response(notFoundException.Message));
                }
                return StatusCode((int)HttpStatusCode.InternalServerError, new Response(ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new Response(ex.Message));
            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var (token, expiresIn) = await _accountService.Login(loginDto);
                return Ok(new TokenResponse(token, expiresIn));
            }
            catch (BaseDomainException ex)
            {
                if (ex is EntityNotFoundException notFoundException)
                {
                    return NotFound(new Response(notFoundException.Message));
                }
                return StatusCode((int)HttpStatusCode.InternalServerError, new Response(ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new Response(ex.Message));
            }
        }
    }
}