namespace Agree.Athens.Domain.Exceptions
{
    public class InvalidChannelTypeException : BaseDomainException
    {
        public InvalidChannelTypeException(string message) : base(message)
        {
        }

        public static InvalidChannelTypeException ChannelCannotReceiveMessages()
            => new InvalidChannelTypeException($"Channel cannot receive messages because it is not a text channel");
    }
}