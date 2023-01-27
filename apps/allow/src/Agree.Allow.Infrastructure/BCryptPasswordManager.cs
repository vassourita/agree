namespace Agree.Allow.Infrastructure.Providers;

using System;
using System.Net.Mail;
using System.Threading.Tasks;
using Agree.Allow.Domain;

public class BCryptPasswordManager : IPasswordManager, IDisposable
{
    public Task<string> HashAsync(string password)
        => Task.Run(() => BCrypt.Net.BCrypt.HashPassword(password));

    public Task<bool> CompareAsync(string hashed, string password)
        => Task.Run(() => BCrypt.Net.BCrypt.Verify(password, hashed));

    public void Dispose() => GC.SuppressFinalize(this);
}