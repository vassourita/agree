using System;
using System.Collections.Generic;
using Agree.Athens.Domain.Aggregates.Messages;

namespace Agree.Athens.Application.ViewModels
{
    public class TextChannelViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Message> Messages { get; set; }
    }
}