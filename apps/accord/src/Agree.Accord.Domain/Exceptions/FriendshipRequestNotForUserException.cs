using Agree.SharedKernel.Exceptions;

public class FriendshipRequestNotForUserException : DomainException
{
    public FriendshipRequestNotForUserException() : base("To", "Friendship request not for the user.")
    {
    }
}