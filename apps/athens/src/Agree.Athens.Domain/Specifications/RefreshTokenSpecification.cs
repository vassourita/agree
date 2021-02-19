using System;
using System.Linq;
using System.Linq.Expressions;
using Agree.Athens.Domain.Entities;

namespace Agree.Athens.Domain.Specifications
{
    public class RefreshTokenSpecification : Specification<User>
    {
        private readonly string _token;

        public RefreshTokenSpecification(string token)
        {
            _token = token;
        }

        public override Expression<Func<User, bool>> ToExpression()
        {
            return (x) => x.RefreshTokens.Any(y => y.Token == _token && y.UserId == x.Id);
        }
    }
}