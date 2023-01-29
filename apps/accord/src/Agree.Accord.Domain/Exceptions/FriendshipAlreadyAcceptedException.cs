using Agree.SharedKernel.Exceptions;

public class FriendshipAlreadyAcceptedException : DomainException
{
    public FriendshipAlreadyAcceptedException() : base("Accepted", "Friendship already accepted.")
    {
    }
}