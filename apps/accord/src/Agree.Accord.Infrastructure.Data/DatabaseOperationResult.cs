using Agree.Accord.SharedKernel;

namespace Agree.Accord.Infrastructure.Data
{
    public class DatabaseOperationResult : Result, IResult
    {
        private DatabaseOperationResult(bool succeeded) : base(succeeded)
        { }
        public static IResult Ok() => new DatabaseOperationResult(true);
        public static IResult Fail() => new DatabaseOperationResult(false);
    }
}