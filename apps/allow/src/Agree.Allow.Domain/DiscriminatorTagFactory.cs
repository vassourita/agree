namespace Agree.Allow.Domain;

using System;
using System.Threading.Tasks;
using Agree.Allow.Domain.Specifications;
using Agree.SharedKernel.Data;

public class DiscriminatorTagFactory
{
    private readonly IRepository<UserAccount, Guid> _accountRepository;

    public DiscriminatorTagFactory(IRepository<UserAccount, Guid> accountRepository) => _accountRepository = accountRepository;

    public async Task<DiscriminatorTag> GenerateDiscriminatorTagAsync(string username)
    {
        DiscriminatorTag tag = DiscriminatorTag.Parse(0);

        var sameNameTagCheck = await _accountRepository.GetFirstAsync(new UserNameEqualSpecification(username, true));

        if (sameNameTagCheck == null)
            return tag;

        tag = sameNameTagCheck.Tag.Increment();

        if (tag.Value > 9999)
            return null;

        return tag;
    }
}