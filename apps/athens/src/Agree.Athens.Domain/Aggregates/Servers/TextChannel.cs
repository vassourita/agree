using System.Collections.ObjectModel;
using System.Collections.Generic;
using Agree.Athens.Domain.Aggregates.Messages;
using Agree.Athens.Domain.Aggregates.Servers.Validators;
using Agree.Athens.SharedKernel;
using System;

namespace Agree.Athens.Domain.Aggregates.Servers
{
    public class TextChannel : Entity
    {
        public TextChannel(string name, Category category)
        {
            Messages = new Collection<Message>();
            Name = name;
            Category = category;

            Validate(this, new TextChannelValidator());
        }

        // Empty constructor for EF Core
        protected TextChannel()
        {
            Messages = new Collection<Message>();
        }

        public void UpdateName(string newName)
        {
            Name = newName;
            UpdatedAt = DateTime.UtcNow;

            Validate(this, new TextChannelValidator());
        }

        public string Name { get; protected set; }

        public Category Category { get; protected set; }

        public ICollection<Message> Messages { get; protected set; }
    }
}