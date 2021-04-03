using Agree.Athens.SharedKernel;

namespace Agree.Athens.Domain.Dtos
{
    public class CreateAccountDto : Validatable
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}