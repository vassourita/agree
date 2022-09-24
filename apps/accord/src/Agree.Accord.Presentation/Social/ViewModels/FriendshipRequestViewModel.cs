namespace Agree.Accord.Presentation.Social.ViewModels;

using Agree.Accord.Domain.Social;
using Agree.Accord.Presentation.Identity.ViewModels;

/// <summary>
/// The view model for a friendship request.
/// </summary>
public class FriendshipRequestViewModel
{
    public UserAccountViewModel From { get; private set; }
    public UserAccountViewModel To { get; private set; }
    public bool Accepted { get; private set; }

    /// <summary>
    /// Creates a new instance of the <see cref="FriendshipRequestViewModel"/> class from a friendship entity.
    /// </summary>
    /// <param name="entity">The friendship entity.</param>
    /// <returns>The view model.</returns>
    public static FriendshipRequestViewModel FromEntity(Friendship entity) => new()
    {
        From = UserAccountViewModel.FromEntity(entity.From),
        To = UserAccountViewModel.FromEntity(entity.To),
        Accepted = entity.Accepted
    };
}