namespace Agree.Accord.Domain.Servers.Dtos
{
    public class CreateServerDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ServerPrivacy PrivacyLevel { get; set; }
    }
}