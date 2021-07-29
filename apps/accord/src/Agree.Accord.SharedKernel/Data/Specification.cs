using System;
using System.Linq.Expressions;

namespace Agree.Accord.SharedKernel.Data
{
    public abstract class Specification<T>
    {
        public Specification()
        { }

        public Expression<Func<T, bool>> Expression { get; protected set; }
    }

    public abstract class PaginatedSpecification<T> : Specification<T>
    {
        public IPagination Pagination { get; protected set; }

        public PaginatedSpecification(IPagination pagination)
        {
            Pagination = pagination;
        }
    }
}