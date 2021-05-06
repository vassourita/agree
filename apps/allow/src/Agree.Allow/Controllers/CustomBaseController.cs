using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Agree.Allow.Models;
using Agree.Allow.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Agree.Allow.Controllers
{
    public abstract class CustomBaseController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public CustomBaseController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        protected ApplicationUserViewModel CurrentlyLoggedUser =>
            HttpContext.User.Identity.IsAuthenticated
            ? new ApplicationUserViewModel
            {
                Id = Guid.Parse(HttpContext.User.Claims.First(c => c.Type == "id").Value),
                UserName = HttpContext.User.Identity.Name.Split('#').First(),
                Tag = HttpContext.User.Identity.Name.Split('#').Last(),
                Email = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Email).Value,
                Verified = bool.Parse(HttpContext.User.Claims.First(c => c.Type == "verified").Value)
            }
            : null;

        protected async Task<ApplicationUser> GetAuthenticatedUserAccount()
            => await _userManager.Users.FirstOrDefaultAsync(u => u.Id == CurrentlyLoggedUser.Id);

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