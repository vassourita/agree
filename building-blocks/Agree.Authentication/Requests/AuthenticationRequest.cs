namespace Agree.Authentication.Requests;

public class AuthenticationRequest
{
    public AuthenticationRequest(string username, string password)
    {
        Username = username;
        Password = password;
        GrantType = "password";
    }

    public AuthenticationRequest(string refreshToken)
    {
        RefreshToken = refreshToken;
        GrantType = "refresh_token";
    }

    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? RefreshToken { get; set; }
    public string GrantType { get; private set; }

}