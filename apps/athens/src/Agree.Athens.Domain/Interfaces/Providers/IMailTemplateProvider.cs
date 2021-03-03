using System.Threading.Tasks;

namespace Agree.Athens.Domain.Interfaces.Providers
{
    public interface IMailTemplateProvider
    {
        Task<string> CompileAsync<T>(string template, T model);
    }
}