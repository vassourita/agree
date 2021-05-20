using System.ComponentModel.DataAnnotations;

namespace Agree.Allow.Domain.Dtos
{
    public class UpdateAccountDto
    {
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(30, ErrorMessage = "{0} must have between {2} and {1} characters", MinimumLength = 1)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [EmailAddress(ErrorMessage = "{0} must be a valid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Range(1, 9999, ErrorMessage = "{0} must be between {1} and {2}")]
        public int Tag { get; set; }
    }
}