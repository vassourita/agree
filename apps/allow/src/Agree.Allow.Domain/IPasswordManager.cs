namespace Agree.Allow.Domain;

using System.Threading.Tasks;

public interface IPasswordManager
{
    Task<string> HashAsync(string password);

    Task<bool> CompareAsync(string hashed, string password);
}