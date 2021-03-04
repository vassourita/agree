using System.Net;
using System.Security.Claims;
using System.Linq;
using System;
using Agree.Athens.Domain.Aggregates.Account;
using Microsoft.AspNetCore.Mvc;
using Agree.Athens.Domain.Aggregates.Account.Factories;
using Agree.Athens.Application.ViewModels;

namespace Agree.Athens.Presentation.WebApi.Controllers
{
    public abstract class CustomBaseController : ControllerBase
    {
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

        protected IActionResult InternalServerError()
        {
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        protected IActionResult InternalServerError(object obj)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, obj);
        }
    }
}