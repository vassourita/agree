namespace Agree.Accord.Domain.Identity.Services;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Agree.Accord.Domain.Identity.Dtos;
using Agree.Accord.Domain.Identity.Specifications;

/// <summary>
/// Provides methods for managing user accounts.
/// </summary>
public class AccountService
{
    private readonly IApplicationUserRepository _accountRepository;

    public AccountService(IApplicationUserRepository accountRepository) => _accountRepository = accountRepository;

    /// <summary>
    /// Generates a new DiscriminatorTag for the given display name.
    /// </summary>
    /// <param name="displayName">The user display name.</param>
    /// <returns>A new tag for this display name.</returns>
    public async Task<DiscriminatorTag> GenerateDiscriminatorTagAsync(string displayName)
    {
        var tag = DiscriminatorTag.NewTag();
        var tagIsInUse = await _accountRepository.GetFirstAsync(new NameTagEqualSpecification(tag, displayName)) != null;
        while (tagIsInUse)
        {
            tag = DiscriminatorTag.NewTag();
            tagIsInUse = await _accountRepository.GetFirstAsync(new NameTagEqualSpecification(tag, displayName)) != null;
        }
        return tag;
    }

    /// <summary>
    /// Gets a user account by its id and returns it.
    /// </summary>
    /// <param name="id">The user id.</param>
    /// <returns>The user account or null if it doesn't exists.</returns>
    public async Task<ApplicationUser> GetAccountByIdAsync(Guid id)
        => await _accountRepository.GetFirstAsync(new UserIdEqualSpecification(id));

    /// <summary>
    /// Gets a user account by its email and returns it.
    /// </summary>
    /// <param name="email">The user email.</param>
    /// <returns>The user account or null if it doesn't exists.</returns>
    public async Task<ApplicationUser> GetAccountByEmailAsync(string email)
        => await _accountRepository.GetFirstAsync(new EmailEqualSpecification(email));

    /// <summary>
    /// Searchs for user accounts by their nametag and returns them.
    /// </summary>
    /// <param name="query">The search input.</param>
    /// <returns>The user accounts that match the specified query.</returns>
    public async Task<IEnumerable<ApplicationUser>> SearchUsers(SearchAccountsDto searchAccountsDto)
        => await _accountRepository.SearchAsync(searchAccountsDto.Query, searchAccountsDto);
}