using System.Collections.Generic;

namespace Agree.Athens.Domain.Interfaces.Providers
{
    public interface IMailTemplateProvider
    {
        string FromHtml(string filePath, IDictionary<string, string> parameters);
    }
}