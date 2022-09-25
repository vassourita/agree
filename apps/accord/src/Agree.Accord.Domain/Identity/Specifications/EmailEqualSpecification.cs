namespace Agree.Accord.Domain.Identity.Specifications;

using Agree.Accord.SharedKernel.Data;

/// <summary>
/// A specification that checks if the account email address of a given user is equal to a given value.
/// </summary>
public class EmailEqualSpecification : Specification<UserAccount>
{
    public EmailEqualSpecification(string email)
        => Expression = x
        => x.EmailAddress == email;

    public EmailEqualSpecification(UserAccount account)
        => Expression = x
        => x.EmailAddress == account.EmailAddress;
}