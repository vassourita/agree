using System;
namespace Agree.Athens.Application.ViewModels
{
    public class MessageViewModel
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public AccountViewModel Author { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}