// namespace Agree.Accord.Presentation.Social.Controllers;

// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Agree.Accord.Domain.Servers;
// using Agree.Accord.Domain.Servers.Requests;
// using Agree.Accord.Domain.Social.Requests;
// using Agree.Accord.Presentation.Identity.ViewModels;
// using Agree.Accord.Presentation.Responses;
// using Agree.Accord.Presentation.Servers.ViewModels;
// using Agree.Accord.Presentation.Shared;
// using Agree.Accord.SharedKernel;
// using MediatR;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;

// /// <summary>
// /// A controller for managing categories.
// /// </summary>
// [ApiController]
// [Route("api/servers")]
// [Authorize]
// public class CategoryController : CustomControllerBase
// {
//     public CategoryController(IMediator mediator) : base(mediator) { }

//     [HttpPost]
//     [Route("")]
//     [Authorize]
//     [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GenericResponse<Category>))]
//     [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationErrorResponse))]
//     public async Task<IActionResult> Store([FromBody] CreateCategoryRequest request)
//     {
//         request.Requester = await GetAuthenticatedUserAccount();
//         var result = await _mediator.Send(request);
//         if (result.Failed)
//             return BadRequest(result.Error);
//         return Created(
//             Url.Link("GetCategoryById", new { id = result.Data.Id }),
//             new GenericResponse(CategoryViewModel.FromEntity(result.Data)));
//     }

//     [HttpGet]
//     [Route("")]
//     [Authorize]
//     [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<IEnumerable<Category>>))]
//     public async Task<IActionResult> Index([FromQuery] CategoryCategorysRequest request)
//     {
//         request.UserId = (await GetAuthenticatedUserAccount()).Id;
//         var result = await _mediator.Send(request);
//         return Ok(new GenericResponse(result.Select(CategoryViewModel.FromEntity)));
//     }

//     [HttpGet]
//     [Route("{categoryId:guid}", Name = "GetCategoryById")]
//     [Authorize]
//     [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<Category>))]
//     [ProducesResponseType(StatusCodes.Status404NotFound)]
//     public async Task<IActionResult> Show([FromRoute] Guid categoryId)
//     {
//         var request = new GetCategoryByIdRequest
//         {
//             CategoryId = categoryId,
//             UserId = (await GetAuthenticatedUserAccount()).Id
//         };
//         var result = await _mediator.Send(request);
//         if (result == null)
//             return NotFound();
//         return Ok(new GenericResponse(CategoryViewModel.FromEntity(result)));
//     }
// }