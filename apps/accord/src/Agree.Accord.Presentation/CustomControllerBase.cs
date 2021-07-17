using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace Agree.Accord.Presentation
{
    public class CustomControllerBase : ControllerBase
    {
        public IActionResult InternalServerError(object data = null)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, data);
        }
    }
}