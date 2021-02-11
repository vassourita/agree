namespace Agree.Athens.Domain.Exceptions
{
    public class InvalidColorHexException : BaseDomainException
    {
        public InvalidColorHexException(string invalidColorHex) : base($"Color hex {invalidColorHex} has invalid format")
        {
        }
    }
}