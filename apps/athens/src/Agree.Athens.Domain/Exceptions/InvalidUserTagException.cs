using System;
using Agree.Athens.Domain.Entities;

namespace Agree.Athens.Domain.Exceptions
{
    public class InvalidUserTagException : BaseDomainException
    {
        public InvalidUserTagException(Guid id) : base($"Tag for user with id {id} has invalid format")
        {
        }

        public InvalidUserTagException(User user) : base($"Tag for user with id {user.Id} has invalid format")
        {
        }

        public InvalidUserTagException() : base("Invalid value used to create a new tag")
        {
        }
    }
}