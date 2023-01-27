namespace Agree.Allow.Domain;

using System;
using System.Threading.Tasks;
using Agree.Allow.Domain.Specifications;
using Agree.SharedKernel.Data;

/// <summary>
/// Creates a <see cref="DiscriminatorTag"/> from a given <see cref="UserAccount"/>.
/// </summary>
public class DiscriminatorTagFactory
{
    private readonly IRepository<UserAccount, Guid> _accountRepository;

    public DiscriminatorTagFactory(IRepository<UserAccount, Guid> accountRepository) => _accountRepository = accountRepository;

    public async Task<DiscriminatorTag> GenerateDiscriminatorTagAsync(string displayName)
    {
        DiscriminatorTag tag;

        do
            tag = DiscriminatorTag.NewTag();
        while (await _accountRepository.GetFirstAsync(new NameTagEqualSpecification(tag, displayName)) != null);

        return tag;
    }
}