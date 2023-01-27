namespace Agree.Allow.Domain.Specifications;

using Agree.SharedKernel.Data;

/// <summary>
/// A specification that checks if the account email address of a given user is equal to a given value.
/// </summary>
public class EmailEqualSpecification : Specification<UserAccount>
{
    public EmailEqualSpecification(string email)
        => Expression = u
        => u.EmailAddress == email;

    public EmailEqualSpecification(UserAccount account)
        => Expression = u
        => u.EmailAddress == account.EmailAddress;
}