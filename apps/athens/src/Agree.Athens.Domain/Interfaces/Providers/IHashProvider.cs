namespace Agree.Athens.Domain.Interfaces.Providers
{
    public interface IHashProvider
    {
        string Hash(string s);
        bool Compare(string hashed, string s);
    }
}