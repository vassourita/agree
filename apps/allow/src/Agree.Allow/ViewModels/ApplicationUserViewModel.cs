using System;

namespace Agree.Allow.ViewModels
{
    public class ApplicationUserViewModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Tag { get; set; }
    }
}