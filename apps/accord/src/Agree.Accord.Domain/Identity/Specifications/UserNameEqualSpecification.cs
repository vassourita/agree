namespace Agree.Accord.Domain.Identity.Specifications;

using Agree.Accord.SharedKernel.Data;

/// <summary>
/// A specification that checks if the account name of a given user is equal to a given value.
/// </summary>
public class UserNameEqualSpecification : Specification<ApplicationUser>
{
    public UserNameEqualSpecification(string userName) => Expression = u => u.Username == userName;

    public UserNameEqualSpecification(ApplicationUser account) => Expression = u => u.Username == account.Username;
}