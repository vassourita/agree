using Agree.SharedKernel;

namespace Agree.Allow.Infrastructure.Data
{
    /// <summary>
    /// A result of a no-return database operation.
    /// </summary>
    public class DatabaseOperationResult : Result
    {
        private DatabaseOperationResult(bool succeeded) : base(succeeded)
        { }
        public static IResult Ok()
        {
            return new DatabaseOperationResult(true);
        }

        public static IResult Fail()
        {
            return new DatabaseOperationResult(false);
        }
    }
}