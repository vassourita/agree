using Agree.Accord.SharedKernel;

namespace Agree.Accord.Domain.Social.Results
{
    /// <summary>
    /// The result of an operation on a friendship request.
    /// </summary>
    public class RemoveFriendResult : Result<Friendship, ErrorList>
    {
        private RemoveFriendResult(Friendship friendship) : base(friendship)
        { }
        private RemoveFriendResult(ErrorList error) : base(error)
        { }

        public static RemoveFriendResult Ok(Friendship friendship) => new(friendship);
        public static RemoveFriendResult Fail(ErrorList data) => new(data);
    }
}