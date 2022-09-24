using System;

namespace Agree.Allow.Presentation.ViewModels
{
    public class UserAccountViewModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public bool Verified { get; set; }
        public string UserName { get; set; }
        public string Tag { get; set; }
    }
}