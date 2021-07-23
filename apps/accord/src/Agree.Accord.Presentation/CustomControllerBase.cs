using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace Agree.Accord.Presentation
{
    public class CustomControllerBase : ControllerBase
    {
        public const string AccessTokenCookieName = "agreeaccord_accesstoken";

        public IActionResult InternalServerError(object data = null)
            => StatusCode((int)HttpStatusCode.InternalServerError, data);
    }
}