using Agree.Accord.SharedKernel;

namespace Agree.Accord.Domain.Identity.Results
{
    public class CreateAccountResult : Result<UserAccount, ErrorList>
    {
        private CreateAccountResult(UserAccount data) : base(data)
        { }
        private CreateAccountResult(ErrorList error) : base(error)
        { }

        public static CreateAccountResult Ok(UserAccount data) => new(data);
        public static CreateAccountResult Fail(ErrorList data) => new(data);
    }
}