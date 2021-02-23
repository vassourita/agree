using System;
namespace Agree.Athens.SharedKernel
{
    public interface ISoftDeletable
    {
        void SoftDelete();

        DateTime? DeletedAt { get; }
    }
}