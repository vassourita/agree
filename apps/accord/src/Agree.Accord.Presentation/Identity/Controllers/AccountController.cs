using System.Threading.Tasks;
using Agree.Accord.Domain.Identity.Dtos;
using Agree.Accord.Domain.Identity.Services;
using Microsoft.AspNetCore.Mvc;

namespace Agree.Accord.Presentation.Identity.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountController : CustomControllerBase
    {
        private readonly AccountService _accountService;

        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Register([FromBody] CreateAccountDto createAccountDto)
        {
            try
            {
                var registerResult = await _accountService.CreateAccountAsync(createAccountDto);

                if (registerResult.Failed)
                {
                    return BadRequest();
                }
                var account = registerResult.Data;
                var newAccountUri = Url.Link("GetAccountById", new
                {
                    id = account.Id
                });
                return Created(newAccountUri, new { Identity = account.Id });
            }
            catch
            {
                return InternalServerError();
            }
        }
    }
}