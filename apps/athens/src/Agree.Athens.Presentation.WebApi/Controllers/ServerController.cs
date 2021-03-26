using System;
using System.Threading.Tasks;
using Agree.Athens.Application.Dtos;
using Agree.Athens.Application.Services;
using Agree.Athens.Application.ViewModels;
using Agree.Athens.Domain.Exceptions;
using Agree.Athens.Domain.Services;
using Agree.Athens.Presentation.WebApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Agree.Athens.Presentation.WebApi.Controllers
{
    [ApiController]
    [Route("api/servers")]
    public class ServerController : CustomBaseController
    {
        private readonly ServerService _serverService;
        private readonly IMapper _mapper;

        public ServerController(ServerService serverService, AccountService accountService, IMapper mapper)
        {
            _serverService = serverService;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize]
        [Route("")]
        public async Task<IActionResult> Store([FromBody] CreateServerDto createServerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var newServer = await _serverService.CreateServer(CurrentlyLoggedUser.Id, createServerDto.Name, createServerDto.Description);
                return Ok(new ServerResponse(_mapper.Map<ServerViewModel>(newServer), "Server successfully created"));
            }
            catch (BaseDomainException ex)
            {
                return HandleDomainException(ex);
            }
            catch (Exception ex)
            {
                return InternalServerError(new Response(ex.Message));
            }
        }
    }
}