using System;

namespace Agree.Athens.Domain.Interfaces
{
    public interface ISoftDeletable
    {
        DateTime? DeletedAt { get; set; }
    }
}