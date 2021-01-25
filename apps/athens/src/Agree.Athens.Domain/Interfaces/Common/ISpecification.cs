namespace Agree.Athens.Domain.Interfaces.Common
{
    public interface ISpecification<T>
    {
        bool IsSatisfied(T item);
    }
}