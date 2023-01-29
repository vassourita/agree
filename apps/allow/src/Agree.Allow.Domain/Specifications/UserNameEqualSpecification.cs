namespace Agree.Allow.Domain.Specifications;

using Agree.SharedKernel.Data;

public class UserNameEqualSpecification : Specification<UserAccount>
{
    public UserNameEqualSpecification(string userName)
        => Expression = u
        => u.Username == userName;

    public UserNameEqualSpecification(UserAccount account)
        => Expression = u
        => u.Username == account.Username;
}