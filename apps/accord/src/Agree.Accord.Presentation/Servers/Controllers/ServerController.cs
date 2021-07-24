using System;
using System.Threading.Tasks;
using Agree.Accord.Domain.Identity;
using Agree.Accord.Domain.Identity.Dtos;
using Agree.Accord.Domain.Identity.Services;
using Agree.Accord.Domain.Providers;
using Agree.Accord.Domain.Servers.Dtos;
using Agree.Accord.Domain.Servers.Services;
using Agree.Accord.Presentation.Responses;
using Agree.Accord.Presentation.Servers.ViewModels;
using Agree.Accord.SharedKernel;
using Agree.Accord.SharedKernel.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Agree.Accord.Presentation.Servers.Controllers
{
    [ApiController]
    [Route("api/servers")]
    [Authorize]
    public class ServerController : CustomControllerBase
    {
        private readonly ServerService _serverService;

        public ServerController(
            ServerService serverService,
            AccountService accountService) : base(accountService)
        {
            _serverService = serverService;
        }

        [HttpPost]
        [Route("")]
        [Authorize]
        public async Task<ActionResult> Store([FromBody] CreateServerDto createServerDto)
        {
            var result = await _serverService.CreateServerAsync(createServerDto, await GetAuthenticatedUserAccount());

            if (result.Failed)
            {
                return BadRequest(result.Error);
            }

            var server = result.Data;
            var vm = ServerViewModel.FromEntity(server);

            var url = Url.Link("GetServerById", new { id = server.Id });
            return Created(url, new ServerResponse(vm));
        }

        [HttpGet]
        [Route("{id:guid}", Name = "GetServerById")]
        [Authorize]
        public async Task<ActionResult> Show([FromRoute] Guid id)
        {
            return Ok(id);
        }
    }
}