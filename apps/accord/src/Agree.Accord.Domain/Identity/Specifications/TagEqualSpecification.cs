using Agree.Accord.SharedKernel.Data;

namespace Agree.Accord.Domain.Identity.Specifications
{
    /// <summary>
    /// A specification that checks if the account tag of a given user is equal to a given value.
    /// </summary>
    public class TagEqualSpecification : Specification<UserAccount>
    {
        public TagEqualSpecification(DiscriminatorTag tag)
            : base(account => account.Tag == tag)
        { }

        public TagEqualSpecification(UserAccount acc)
            : base(account => account.Tag == acc.Tag)
        { }
    }
}