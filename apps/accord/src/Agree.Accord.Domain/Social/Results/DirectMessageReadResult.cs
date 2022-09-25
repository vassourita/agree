namespace Agree.Accord.Domain.Social.Results;

using System.Collections.Generic;
using Agree.Accord.SharedKernel;

/// <summary>
/// The result of a request to read a direct message.
/// </summary>
public class DirectMessagesReadResult : Result<IEnumerable<DirectMessage>, ErrorList>
{
    private DirectMessagesReadResult(IEnumerable<DirectMessage> message) : base(message)
    { }
    private DirectMessagesReadResult(ErrorList error) : base(error)
    { }

    public static DirectMessagesReadResult Ok(IEnumerable<DirectMessage> message) => new(message);
    public static DirectMessagesReadResult Fail(ErrorList data) => new(data);
}