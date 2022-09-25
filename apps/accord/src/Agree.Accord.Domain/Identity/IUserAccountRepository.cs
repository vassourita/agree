namespace Agree.Accord.Domain.Identity;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Agree.Accord.SharedKernel.Data;

/// <summary>
/// The interface for a repository of users accounts. Has all the methods from a generic IRepository, plus a search method.
/// </summary>
public interface IUserAccountRepository : IRepository<UserAccount, Guid>
{
    /// <summary>
    /// Searches for users by their nameTag.
    /// </summary>
    Task<IEnumerable<UserAccount>> SearchAsync(string query, IPagination pagination);
}