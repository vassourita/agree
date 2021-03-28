namespace Agree.Athens.Domain.Exceptions
{
    public class DomainInvalidSearchException : BaseDomainException
    {
        private DomainInvalidSearchException(string message) : base(message)
        {
        }

        public static DomainInvalidSearchException EmptyQuery()
            => new($"Query must not be empty");

        public static DomainInvalidSearchException InvalidQuery(string query)
            => new($"Query '{query}' is not a valid search");

        public static DomainInvalidSearchException InvalidSort(string sort)
            => new($"'{sort}' is not a valid sorting option");
    }
}