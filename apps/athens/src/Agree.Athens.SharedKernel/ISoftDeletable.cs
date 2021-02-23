using System;
namespace Agree.Athens.SharedKernel
{
    public interface ISoftDeletable
    {
        DateTime? DeletedAt { get; set; }
    }
}