using Agree.Accord.SharedKernel;

namespace Agree.Accord.Infrastructure.Data
{
    /// <summary>
    /// A result of a no-return database operation.
    /// </summary>
    public class DatabaseOperationResult : Result
    {
        private DatabaseOperationResult(bool succeeded) : base(succeeded)
        { }
        public static IResult Ok() => new DatabaseOperationResult(true);
        public static IResult Fail() => new DatabaseOperationResult(false);
    }
}