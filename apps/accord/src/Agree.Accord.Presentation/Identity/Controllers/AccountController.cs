using System;
using System.Threading.Tasks;
using Agree.Accord.Domain.Identity.Dtos;
using Agree.Accord.Domain.Identity.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Agree.Accord.Presentation.Identity.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountController : CustomControllerBase
    {
        private readonly AccountService _accountService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(AccountService accountService, ILogger<AccountController> logger)
        {
            _accountService = accountService;
            _logger = logger;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Store([FromBody] CreateAccountDto createAccountDto)
        {
            try
            {
                var registerResult = await _accountService.CreateAccountAsync(createAccountDto);

                if (registerResult.Failed)
                {
                    return BadRequest(ModelState);
                }
                var account = registerResult.Data;
                var newAccountUri = Url.Link("GetAccountById", new
                {
                    id = account.Id
                });
                return Created(newAccountUri, new { Identity = account.Id });
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> Show([FromRoute] Guid id)
        {
            return Ok(await _accountService.GetAccountByIdAsync(id));
        }
    }
}