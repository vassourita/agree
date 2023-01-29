namespace Agree.Allow.Domain.Specifications;

using Agree.SharedKernel.Data;

public class NameTagLikeSpecification : PaginatedSpecification<UserAccount>
{
    public NameTagLikeSpecification(string query, IPagination pagination)
        : base(pagination)
        => Expression = u
        => (u.Username + "#" + u.Tag).ToLower().Contains(query.ToLower());
}