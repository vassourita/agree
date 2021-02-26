namespace Agree.Athens.Presentation.WebApi.Models
{
    public class Response
    {
        public string Message { get; set; }

        public Response(string message) => Message = message;
    }
}