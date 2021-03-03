using System.Threading.Tasks;
using Agree.Athens.Domain.Interfaces.Providers;
using FluentEmail.Razor;

namespace Agree.Athens.Infrastructure.Providers
{
    public class FluentMailTemplateProvider : IMailTemplateProvider
    {
        public async Task<string> Compile<T>(string template, T model)
        {
            return await new RazorRenderer().ParseAsync(template, model);
        }
    }
}