using System;
using System.Net;
using System.Threading.Tasks;
using Agree.Athens.Application.Dtos;
using Agree.Athens.Domain.Exceptions;
using Agree.Athens.Domain.Services;
using Microsoft.AspNetCore.Mvc;

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
                var confirmationUrl = Url.Link("ConfirmEmail", new { });
                await _accountService.Register(createAccountDto.UserName, createAccountDto.Email, createAccountDto.Password, confirmationUrl);
                return Ok();
            }
            catch (BaseDomainException ex)
            {
                if (ex is ValidationException validationException)
                {
                    return BadRequest(new { Message = validationException.Message, Errors = validationException.GetErrors() });
                }
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Message = ex.Message });
            }
        }
    }
}