namespace Agree.Accord.Domain.Social.Results;

using System.Collections.Generic;
using Agree.Accord.SharedKernel;

public class DirectMessagesReadResult : Result<IEnumerable<DirectMessage>, ErrorList>
{
    private DirectMessagesReadResult(IEnumerable<DirectMessage> message) : base(message)
    { }
    private DirectMessagesReadResult(ErrorList error) : base(error)
    { }

    public static DirectMessagesReadResult Ok(IEnumerable<DirectMessage> message) => new(message);
    public static DirectMessagesReadResult Fail(ErrorList data) => new(data);
}