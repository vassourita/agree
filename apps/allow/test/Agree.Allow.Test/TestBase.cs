namespace Agree.Allow.Test;

using Agree.Allow.Domain;
using Agree.SharedKernel.Data;
using Microsoft.Extensions.DependencyInjection;

public class TestBase
{
    private readonly DependencyInjectionContainer _container = new();

    protected T Resolve<T>()
    {
        return _container.Services.GetRequiredService<T>();
    }

    protected async Task<UserAccount> CreateTestUserAccount(DiscriminatorTag tag = null)
    {
        var account = new UserAccount("testuser", "test@somemail", "somehash", tag ?? DiscriminatorTag.Parse(0));
        var repository = Resolve<IRepository<UserAccount, Guid>>();
        await repository.InsertAsync(account);
        await repository.CommitAsync();
        return account;
    }
}