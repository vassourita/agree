using Agree.Athens.Domain.Aggregates.Account;

namespace Agree.Athens.Application.ViewModels.Mail
{
    public class MailConfirmationModel
    {
        public MailConfirmationModel(UserAccount account, string confirmationUrl)
        {
            UserName = account.UserName;
            Tag = account.Tag.Value;
            ConfirmationUrl = confirmationUrl;
        }

        public string UserName { get; }
        public string Tag { get; }
        public string ConfirmationUrl { get; }
    }
}