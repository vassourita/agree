namespace Agree.Allow.Domain.Specifications;

using Agree.SharedKernel.Data;

public class EmailEqualSpecification : Specification<UserAccount>
{
    public EmailEqualSpecification(string email)
        => Expression = x
        => x.EmailAddress == email;

    public EmailEqualSpecification(UserAccount account)
        => Expression = x
        => x.EmailAddress == account.EmailAddress;
}