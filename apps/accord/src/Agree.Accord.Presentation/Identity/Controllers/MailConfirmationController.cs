using System;
using System.Threading.Tasks;
using Agree.Accord.Domain.Identity;
using Agree.Accord.Domain.Identity.Dtos;
using Agree.Accord.Domain.Identity.Services;
using Agree.Accord.Domain.Providers;
using Agree.Accord.Presentation.Responses;
using Agree.Accord.Presentation.ViewModels;
using Agree.Accord.SharedKernel;
using Agree.Accord.SharedKernel.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Agree.Accord.Presentation.Identity.Controllers
{
    [ApiController]
    [Route("api/identity/email-confirmation")]
    [Authorize]
    public class MailConfirmationController : CustomControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMailProvider _mailProvider;
        private readonly AccountService _accountService;

        public MailConfirmationController(
            UserManager<ApplicationUser> userManager,
            IMailProvider mailProvider,
            AccountService accountService) : base(accountService)
        {
            _userManager = userManager;
            _mailProvider = mailProvider;
            _accountService = accountService;
        }

        [HttpGet]
        [Route("", Name = "ConfirmEmail")]
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail([FromQuery] string token, [FromQuery] Guid id)
        {
            var user = await _accountService.GetAccountByIdAsync(id);

            if (user == null)
            {
                return BadRequest();
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);

            return Ok();
        }

        [HttpPost]
        [Route("")]
        [Authorize]
        public async Task<ActionResult> ResendConfirmationEmail()
        {
            var user = await GetAuthenticatedUserAccount();
            if (user.EmailConfirmed)
            {
                return BadRequest();
            }
            var mailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationUrl = Url.Link("ConfirmEmail", new { token = mailToken, id = user.Id });
            await _mailProvider.SendMailAsync(
                user.Email,
                "Agree - Confirmation",
                $"<html><body>Hello, {user.DisplayName}#{user.Tag.ToString().PadLeft(4, '0')}. Please click <a href=\"{confirmationUrl}\">HERE</a> to confirm your new email.</body></html>");

            return Ok();
        }
    }
}