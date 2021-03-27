using System;
using System.Collections.Generic;
using Agree.Athens.Domain.Aggregates.Servers;

namespace Agree.Athens.Application.ViewModels
{
    public class CategoryViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<TextChannelViewModel> TextChannels { get; set; }
    }
}