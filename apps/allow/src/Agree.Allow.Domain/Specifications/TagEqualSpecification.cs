namespace Agree.Allow.Domain.Specifications;

using Agree.SharedKernel.Data;

public class TagEqualSpecification : Specification<UserAccount>
{
    public TagEqualSpecification(DiscriminatorTag tag)
        => Expression = x
        => x.Tag == tag;

    public TagEqualSpecification(UserAccount account)
        => Expression = x
        => x.Tag == account.Tag;
}