using Agree.Accord.SharedKernel;

namespace Agree.Accord.Domain.Servers.Results
{
    /// <summary>
    /// The result of a server creation.
    /// </summary>
    public class CreateServerResult : Result<Server, ErrorList>
    {
        private CreateServerResult(Server server) : base(server)
        { }
        private CreateServerResult(ErrorList error) : base(error)
        { }

        public static CreateServerResult Ok(Server server) => new(server);
        public static CreateServerResult Fail(ErrorList data) => new(data);
    }
}