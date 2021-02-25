using System.Collections.Generic;

namespace Agree.Athens.Domain.Interfaces.Providers
{
    public interface IMailTemplateProvider
    {
        string Parse(string filePath, object model);
    }
}