namespace Agree.Accord.Domain.Identity.Specifications;

using Agree.Accord.SharedKernel.Data;

/// <summary>
/// A specification that checks if the account nametag (userName + tag) of a given user is equal to a given value.
/// </summary>
public class NameTagEqualSpecification : Specification<UserAccount>
{
    public NameTagEqualSpecification(DiscriminatorTag tag, string userName) => Expression = u => u.Username == userName && u.Tag == tag;

    public NameTagEqualSpecification(UserAccount account) => Expression = u => u.Username == account.Username && u.Tag == account.Tag;
}