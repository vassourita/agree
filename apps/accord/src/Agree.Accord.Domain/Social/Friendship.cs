using System;
using Agree.Accord.Domain.Identity;

namespace Agree.Accord.Domain.Social
{
    public class Friendship
    {
        // EF ctor
        protected Friendship() { }

        public Friendship(ApplicationUser from, ApplicationUser to)
        {
            this.From = from;
            this.FromId = from.Id;
            this.To = to;
            this.ToId = to.Id;
            this.Accepted = false;
        }

        public Guid FromId { get; private set; }
        public ApplicationUser From { get; private set; }
        public Guid ToId { get; private set; }
        public ApplicationUser To { get; private set; }
        public bool Accepted { get; private set; }

        public void Accept()
        {
            Accepted = true;
        }
    }
}