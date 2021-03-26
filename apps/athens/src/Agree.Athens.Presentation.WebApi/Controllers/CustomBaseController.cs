using System.Threading.Tasks;
using System.Net;
using System.Security.Claims;
using System.Linq;
using System;
using Agree.Athens.Domain.Aggregates.Account;
using Microsoft.AspNetCore.Mvc;
using Agree.Athens.Domain.Aggregates.Account.Factories;
using Agree.Athens.Application.ViewModels;
using Agree.Athens.Application.Services;
using Agree.Athens.Domain.Exceptions;
using Agree.Athens.Presentation.WebApi.Models;

namespace Agree.Athens.Presentation.WebApi.Controllers
{
    public abstract class CustomBaseController : ControllerBase
    {
        private readonly AccountService _accountService;
        public CustomBaseController(AccountService accountService)
        {
            _accountService = accountService;
        }

        protected AccountViewModel CurrentlyLoggedUser =>
            HttpContext.User.Identity.IsAuthenticated
            ? new AccountViewModel
            {
                Id = Guid.Parse(HttpContext.User.Claims.First(c => c.Type == "id").Value),
                UserName = HttpContext.User.Identity.Name.Split('#').First(),
                Tag = HttpContext.User.Identity.Name.Split('#').Last(),
                Email = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Email).Value
            }
            : null;

        protected async Task<UserAccount> GetAuthenticatedUserAccount()
            => await _accountService.GetUserById(CurrentlyLoggedUser.Id);

        protected IActionResult InternalServerError()
        {
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        protected IActionResult InternalServerError(object obj)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, obj);
        }

        protected IActionResult HandleDomainException(BaseDomainException ex)
        {
            if (ex is EntityNotFoundException notFoundException)
            {
                return NotFound(new Response(notFoundException.Message));
            }
            if (ex is DomainValidationException validationException)
            {
                return BadRequest(ErrorResponse.FromException(validationException));
            }
            if (ex is DomainUnauthorizedException unauthorizedException)
            {
                return Unauthorized(new Response(unauthorizedException.Message));
            }
            return InternalServerError(new Response(ex.Message));
        }
    }
}