using System;
using System.Net;
using System.Threading.Tasks;
using Agree.Athens.Application.Dtos.Account;
using Agree.Athens.Application.Services;
using Agree.Athens.Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Agree.Athens.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] CreateAccountDto createAccountDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _authService.Register(createAccountDto);
                return Ok();
            }
            catch (BaseDomainException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();
                var token = await _authService.Login(loginDto, ipAddress);
                return Ok(new { Token = token });
            }
            catch (BaseDomainException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("RefreshToken")]
        public IActionResult RefreshToken()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var token = HttpContext.Request.Cookies["agree-athens-refresh-token"];
                var ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();
                var newToken = _authService.RefreshToken(token, ipAddress);
                return Ok(new { Token = newToken });
            }
            catch (BaseDomainException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }
        }

        [HttpPost]
        [Route("RevokeToken")]
        public async Task<IActionResult> RevokeToken(string token)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();
                await _authService.RevokeToken(token, ipAddress);
                return Ok();
            }
            catch (BaseDomainException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }
        }

        [HttpPost]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var token = HttpContext.Request.Cookies["agree-athens-refresh-token"];
                var ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();
                await _authService.Logout(token, ipAddress);
                return Ok();
            }
            catch (BaseDomainException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }
        }
    }
}
