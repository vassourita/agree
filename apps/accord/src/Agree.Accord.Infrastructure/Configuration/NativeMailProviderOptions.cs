namespace Agree.Accord.Infrastructure.Configuration
{
    /// <summary>
    /// The configuration for the <see cref="Agree.Accord.Infrastructure.Providers.NativeMailProvider"/> class.
    /// </summary>
    public class NativeMailProviderOptions
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}