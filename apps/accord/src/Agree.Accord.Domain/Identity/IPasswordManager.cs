namespace Agree.Accord.Domain.Identity;

using System.Threading.Tasks;

/// <summary>
/// The interface for a password hasher and comparer.
/// </summary>
public interface IPasswordManager
{
    /// <summary>
    /// Hashes a password asynchronously.
    /// </summary>
    /// <param name="password">The password to be hashed.</param>
    Task<string> HashAsync(string password);

    /// <summary>
    /// Compares a password with a hash asynchronously.
    /// </summary>
    /// <param name="hashed">The hashed password.</param>
    /// <param name="password">The password to be compared.</param>
    Task<bool> CompareAsync(string hashed, string password);
}