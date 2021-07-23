using Agree.Accord.Domain.Identity.Tokens;
using Agree.Accord.SharedKernel;

namespace Agree.Accord.Domain.Identity.Results
{
    public class CreateAccountResult : Result<(ApplicationUser, AccessToken), ErrorList>
    {
        private CreateAccountResult(ApplicationUser user, AccessToken accessToken) : base((user, accessToken))
        { }
        private CreateAccountResult(ErrorList error) : base(error)
        { }

        public static CreateAccountResult Ok(ApplicationUser user, AccessToken accessToken) => new(user, accessToken);
        public static CreateAccountResult Fail(ErrorList data) => new(data);
    }
}