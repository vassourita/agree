namespace Agree.Accord.Domain.Identity.Specifications;

using Agree.Accord.SharedKernel.Data;

/// <summary>
/// A specification that checks if the account tag of a given user is equal to a given value.
/// </summary>
public class NameTagLikeSpecification : PaginatedSpecification<UserAccount>
{
    public NameTagLikeSpecification(string query, IPagination pagination)
        : base(pagination)
        => Expression = u
        => (u.Username + "#" + u.Tag).ToLower().Contains(query.ToLower());
}