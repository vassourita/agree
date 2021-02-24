using System.Xml;
using System.Collections.Generic;
using System.IO;
using Agree.Athens.Domain.Interfaces.Providers;

namespace Agree.Athens.Infrastructure.Providers
{
    public class NativeMailTemplateProvider : IMailTemplateProvider
    {
        public string FromHtml(string filePath, IDictionary<string, string> parameters)
        {
            var body = string.Empty;
            using (var reader = new StreamReader(filePath))
            {
                body = reader.ReadToEnd();
            }
            foreach (var param in parameters)
            {
                body.Replace($"{{Model.{param.Key}}}", param.Value);
            }
            return body;
        }
    }
}