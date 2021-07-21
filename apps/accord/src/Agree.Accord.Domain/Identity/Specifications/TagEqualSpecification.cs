using Agree.Accord.SharedKernel.Data;

namespace Agree.Accord.Domain.Identity.Specifications
{
    /// <summary>
    /// A specification that checks if the account tag of a given user is equal to a given value.
    /// </summary>
    public class TagEqualSpecification : Specification<UserAccount>
    {
        private readonly DiscriminatorTag _tag;

        public TagEqualSpecification(DiscriminatorTag tag)
        {
            Expression = u => u.Tag == tag;
        }

        public TagEqualSpecification(UserAccount account)
        {
            Expression = u => u.Tag == account.Tag;
        }
    }
}