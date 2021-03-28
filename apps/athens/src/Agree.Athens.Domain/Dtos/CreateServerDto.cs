using Agree.Athens.SharedKernel;

namespace Agree.Athens.Domain.Dtos
{
    public class CreateServerDto : Validatable
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Privacy { get; set; }
    }
}