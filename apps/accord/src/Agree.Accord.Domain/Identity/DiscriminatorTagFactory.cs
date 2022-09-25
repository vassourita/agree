namespace Agree.Accord.Domain.Identity;

using System.Threading.Tasks;
using Agree.Accord.Domain.Identity.Specifications;

public class DiscriminatorTagFactory
{
    private readonly IUserAccountRepository _accountRepository;

    public DiscriminatorTagFactory(IUserAccountRepository accountRepository) => _accountRepository = accountRepository;

    public async Task<DiscriminatorTag> GenerateDiscriminatorTagAsync(string displayName)
    {
        DiscriminatorTag tag;

        do
            tag = DiscriminatorTag.NewTag();
        while (await _accountRepository.GetFirstAsync(new NameTagEqualSpecification(tag, displayName)) != null);

        return tag;
    }
}