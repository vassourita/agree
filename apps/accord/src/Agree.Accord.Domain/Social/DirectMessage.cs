using System;
using Agree.Accord.Domain.Identity;

namespace Agree.Accord.Domain.Social
{
    public class DirectMessage
    {
        // EF ctor
        public DirectMessage() { }

        public DirectMessage(string text, ApplicationUser from, ApplicationUser to)
        {
            Id = Guid.NewGuid();
            Text = text;
            From = from;
            To = to;
            Read = false;
            CreatedAt = DateTime.UtcNow;
        }

        public Guid Id { get; private set; }

        public string Text { get; private set; }
        public ApplicationUser From { get; private set; }
        public ApplicationUser To { get; private set; }
        public bool Read { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public void MarkRead()
        {
            Read = true;
        }
    }
}