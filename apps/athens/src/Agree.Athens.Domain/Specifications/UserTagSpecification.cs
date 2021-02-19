using System;
using System.Linq.Expressions;
using Agree.Athens.Domain.Entities;
using Agree.Athens.Domain.ValueObjects;

namespace Agree.Athens.Domain.Specifications
{
    public class UserTagSpecification : Specification<User>
    {
        private readonly UserTag _tag;
        private readonly string _username;

        public UserTagSpecification(UserTag tag, string username)
        {
            _tag = tag;
            _username = username;
        }
        public override Expression<Func<User, bool>> ToExpression()
        {
            return (user) => user.Tag == _tag && user.Username == _username;
        }
    }
}