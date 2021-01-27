namespace Agree.Athens.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        void Commit();
        void Rollback();
    }
}