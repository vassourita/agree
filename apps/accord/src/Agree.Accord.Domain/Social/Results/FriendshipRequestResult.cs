namespace Agree.Accord.Domain.Social.Results;

using Agree.Accord.SharedKernel;

/// <summary>
/// The result of an operation on a friendship request.
/// </summary>
public class FriendshipRequestResult : Result<Friendship, ErrorList>
{
    private FriendshipRequestResult(Friendship friendship) : base(friendship)
    { }
    private FriendshipRequestResult(ErrorList error) : base(error)
    { }

    public static FriendshipRequestResult Ok(Friendship friendship) => new(friendship);
    public static FriendshipRequestResult Fail(ErrorList data) => new(data);
}