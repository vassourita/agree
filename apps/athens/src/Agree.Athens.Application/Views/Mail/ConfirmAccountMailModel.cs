using System;
namespace Agree.Athens.Application.Views.Mail
{
    public class ConfirmAccountMailModel
    {
        private readonly string[] _titleOptions = new[]
        {
            "grande", "ilustre", "inesquecível", "valente", "benevolente", "proeminente",
            "notável", "colossal ( ͡° ͜ʖ ͡°)", "eminente", "memorável", "respeitável",
            "célebre", "cruel", "intolerável", "deselegante", "hábil", "saboros@"
        };

        public string UserName { get; set; }
        public string Tag { get; set; }
        public string ConfirmationUrl { get; set; }
        public string Title
        {
            get
            {
                var index = new Random().Next(_titleOptions.Length);
                return _titleOptions[index];
            }
        }
    }
}