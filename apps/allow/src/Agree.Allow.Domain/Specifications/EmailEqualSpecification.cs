namespace Agree.Allow.Domain.Specifications;

using Agree.SharedKernel.Data;

public class EmailEqualSpecification : Specification<UserAccount>
{
    public EmailEqualSpecification(string email)
        => Expression = u
        => u.EmailAddress == email;

    public EmailEqualSpecification(UserAccount account)
        => Expression = u
        => u.EmailAddress == account.EmailAddress;
}