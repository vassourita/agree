using Agree.Accord.Domain.Identity.Tokens;
using Agree.Accord.SharedKernel;

namespace Agree.Accord.Domain.Identity.Results
{
    public class LoginResult : Result<AccessToken>
    {
        private LoginResult(AccessToken data) : base(data)
        { }
        private LoginResult() : base()
        { }

        public static LoginResult Ok(AccessToken data) => new(data);
        public static LoginResult Fail() => new();
    }
}