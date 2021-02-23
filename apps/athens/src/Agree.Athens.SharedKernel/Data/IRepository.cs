using System;

namespace Agree.Athens.SharedKernel.Data
{
    public interface IRepository<T> : IDisposable
        where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}