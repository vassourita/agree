using Agree.Accord.Domain.Social;
using Agree.Accord.Presentation.Identity.ViewModels;

namespace Agree.Accord.Presentation.ViewModels
{
    public class FriendshipRequestViewModel
    {
        public ApplicationUserViewModel From { get; private set; }
        public ApplicationUserViewModel To { get; private set; }
        public bool Accepted { get; private set; }

        public static FriendshipRequestViewModel FromEntity(Friendship entity)
        {
            return new FriendshipRequestViewModel
            {
                From = ApplicationUserViewModel.FromEntity(entity.From),
                To = ApplicationUserViewModel.FromEntity(entity.To),
                Accepted = entity.Accepted
            };
        }
    }
}