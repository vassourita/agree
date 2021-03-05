using System;
using System.Threading.Tasks;
using Agree.Athens.Domain.Exceptions;
using Agree.Athens.Domain.Services;
using Agree.Athens.Presentation.WebApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Agree.Athens.Presentation.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AvatarController : CustomBaseController
    {
        private readonly AvatarService _avatarService;

        public AvatarController(AvatarService avatarService)
        {
            _avatarService = avatarService;
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] IFormFile avatar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var fileStream = avatar.OpenReadStream();
                var updatedAccount = await _avatarService.UpdateAvatar(CurrentlyLoggedUser.Id, fileStream, avatar.ContentType);
                return Ok(new UserResponse(updatedAccount, "Avatar succesfully updated"));
            }
            catch (BaseDomainException ex)
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
            catch (Exception ex)
            {
                return InternalServerError(new Response(ex.Message));
            }
        }
    }
}