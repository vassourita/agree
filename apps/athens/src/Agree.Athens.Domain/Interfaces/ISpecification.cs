namespace Agree.Athens.Domain.Interfaces
{
    public interface ISpecification<T>
    {
        bool IsValid(T item);
    }
}