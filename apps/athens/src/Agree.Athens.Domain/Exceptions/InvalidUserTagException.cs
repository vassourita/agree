namespace Agree.Athens.Domain.Exceptions
{
    public class InvalidUserTagException : BaseDomainException
    {
        private InvalidUserTagException(string message) : base(message)
        {
        }

        public static InvalidUserTagException AllZeros()
            => new InvalidUserTagException("Value used to create new tag is all zeros");
        public static InvalidUserTagException MaxLengthExceeded()
            => new InvalidUserTagException("Value used to create new tag exceeded max length of 4 characters");
        public static InvalidUserTagException NotNumeric()
            => new InvalidUserTagException("Value used to create new tag is not numeric");
        public static InvalidUserTagException NullOrEmpty()
            => new InvalidUserTagException("Value used to create new tag is null or empty");
    }
}