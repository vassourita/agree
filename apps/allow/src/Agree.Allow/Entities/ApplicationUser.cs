using Microsoft.AspNetCore.Identity;

namespace Agree.Allow.Presentation.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int Tag { get; set; }
        public string TagStr { get => Tag.ToString().PadLeft(4, '0'); }
    }
}
