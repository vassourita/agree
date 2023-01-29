namespace Agree.Allow.Domain.Specifications;

using Agree.SharedKernel.Data;

public class TagEqualSpecification : Specification<UserAccount>
{
    public TagEqualSpecification(DiscriminatorTag tag)
        => Expression = u
        => u.Tag == tag;

    public TagEqualSpecification(UserAccount account)
        => Expression = u
        => u.Tag == account.Tag;
}