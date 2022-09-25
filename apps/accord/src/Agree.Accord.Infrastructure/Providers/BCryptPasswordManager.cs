namespace Agree.Accord.Infrastructure.Providers;

using System;
using System.Net.Mail;
using System.Threading.Tasks;
using Agree.Accord.Domain.Identity;
using Agree.Accord.Domain.Providers;

/// <summary>
/// A implementation of <see cref="IMailProvider"/> using C#'s native <see cref="SmtpClient"/>.
/// </summary>
public class BCryptPasswordManager : IPasswordManager, IDisposable
{
    public Task<string> HashAsync(string password)
        => Task.Run(() => BCrypt.Net.BCrypt.HashPassword(password));

    public Task<bool> CompareAsync(string hashed, string password)
        => Task.Run(() => BCrypt.Net.BCrypt.Verify(password, hashed));


    public void Dispose() => GC.SuppressFinalize(this);
}