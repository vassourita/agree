using System.Collections.Generic;
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
using Agree.Athens.Domain.Aggregates.Servers;

namespace Agree.Athens.Presentation.WebApi.Controllers
{
    [ApiController]
    [Route("api/servers")]
    public class ServerController : CustomBaseController
    {
        private readonly ServerService _serverService;
        private readonly IMapper _mapper;

        public ServerController(ServerService serverService, AccountService accountService, IMapper mapper) : base(accountService)
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
                var newServer = await _serverService.CreateServer(await GetAuthenticatedUserAccount(),
                                                                  createServerDto.Name,
                                                                  createServerDto.Description,
                                                                  (ServerPrivacy)Enum.Parse(typeof(ServerPrivacy), createServerDto.Privacy));
                var serverModel = _mapper.Map<ServerViewModel>(newServer);
                return Ok(new ServerResponse(serverModel, "Server successfully created"));
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


        [HttpGet]
        [Authorize]
        [Route("")]
        public async Task<IActionResult> Index([FromQuery] SearchServerDto searchServerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var servers = await _serverService.Search(await GetAuthenticatedUserAccount(),
                                                          searchServerDto.Query,
                                                          searchServerDto.OrderBy,
                                                          searchServerDto);
                var serverModels = _mapper.Map<IEnumerable<ServerViewModel>>(servers);
                return Ok(new ServerSearchResponse(serverModels));
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