namespace Agree.Allow.Domain.Specifications;

using System;
using System.Linq.Expressions;
using Agree.SharedKernel.Data;

public class NameTagLikeSpecification : Specification<UserAccount>, IPaginatedSpecification<UserAccount>
{
    public NameTagLikeSpecification(string query, IPagination pagination)
    {
        Expression = x
            => (x.Username + "#" + x.Tag).ToLower().Contains(query.ToLower());

        Pagination = pagination;
    }

    public IPagination Pagination { get; private set; }
}