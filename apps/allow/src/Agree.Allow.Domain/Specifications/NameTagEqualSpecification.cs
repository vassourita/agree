namespace Agree.Allow.Domain.Specifications;

using Agree.SharedKernel.Data;

public class NameTagEqualSpecification : Specification<UserAccount>
{
    public NameTagEqualSpecification(DiscriminatorTag tag, string userName)
        => Expression = u
        => u.Username == userName && u.Tag == tag;

    public NameTagEqualSpecification(UserAccount account)
        => Expression = u
        => u.Username == account.Username && u.Tag == account.Tag;
}