namespace Agree.Allow.Test;

using System.Reflection;
using Agree.Allow.Infrastructure.IoC;
using Agree.SharedKernel.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public class DependencyInjectionContainer
{
    public readonly IServiceProvider Services;

    public DependencyInjectionContainer()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("testsettings.json")
            .Build();

        Services = new ServiceCollection()
            .AddAllowInfrastructure(configuration, Assembly.GetAssembly(typeof(DependencyInjectionContainer)))
            .AddSingleton(typeof(IRepository<,>), typeof(TestRepository<,>))
            .BuildServiceProvider();
    }
}