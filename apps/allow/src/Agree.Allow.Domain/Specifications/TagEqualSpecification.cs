namespace Agree.Allow.Domain.Specifications;

using Agree.SharedKernel.Data;

/// <summary>
/// A specification that checks if the account tag of a given user is equal to a given value.
/// </summary>
public class TagEqualSpecification : Specification<UserAccount>
{
    public TagEqualSpecification(DiscriminatorTag tag)
        => Expression = u
        => u.Tag == tag;

    public TagEqualSpecification(UserAccount account)
        => Expression = u
        => u.Tag == account.Tag;
}