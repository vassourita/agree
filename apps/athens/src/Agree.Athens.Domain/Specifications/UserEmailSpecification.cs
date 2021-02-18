using System;
using System.Linq.Expressions;
using Agree.Athens.Domain.Entities;

namespace Agree.Athens.Domain.Specifications
{
    public class UserEmailSpecification : Specification<User>
    {
        private readonly string _email;

        public UserEmailSpecification(string email)
        {
            _email = email;
        }
        public override Expression<Func<User, bool>> ToExpression()
        {
            return (user) => user.Email == _email;
        }
    }
}