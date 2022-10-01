namespace Agree.Accord.Domain.Social;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Agree.Accord.Domain.Social.Requests;
using Agree.Accord.SharedKernel.Data;

/// <summary>
/// The interface for a repository of direct messages. Has all the methods from a generic IRepository, plus a paginated lookup method.
/// </summary>
public interface IDirectMessageRepository : IRepository<DirectMessage, Guid>
{
    /// <summary>
    /// Searches for direct messages between two users.
    /// </summary>
    Task<IEnumerable<DirectMessage>> SearchAsync(GetFriendChatRequest request);
}