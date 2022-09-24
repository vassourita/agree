namespace Agree.Accord.Domain.Social.Results;

using Agree.Accord.SharedKernel;

public class DirectMessageResult : Result<DirectMessage, ErrorList>
{
    private DirectMessageResult(DirectMessage message) : base(message)
    { }
    private DirectMessageResult(ErrorList error) : base(error)
    { }

    public static DirectMessageResult Ok(DirectMessage message) => new(message);
    public static DirectMessageResult Fail(ErrorList data) => new(data);
}