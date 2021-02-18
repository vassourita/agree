namespace Agree.Athens.Domain.Interfaces.Providers
{
    public interface IHashProvider
    {
        string Hash(string s);
        string Compare(string s, string hashed);
    }
}