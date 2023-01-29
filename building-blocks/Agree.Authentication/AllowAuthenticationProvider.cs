namespace Agree.Authentication;

using System.Net.Http.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Agree.Authentication.Requests;
using Agree.Authentication.Results;

public class AllowAuthenticationProvider : IAuthenticationProvider
{
    private string _baseUrl;
    private string _appKey;
    private Token _refreshToken;
    private Token _accessToken
    {
        get => _accessToken;
        set
        {
            _accessToken = value;
            _httpClient.DefaultRequestHeaders.Remove("Authorization");
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {value.TokenValue}");
        }
    }
    public UserAccountResult UserAccount;

    private readonly HttpClient _httpClient = new HttpClient();

    public AllowAuthenticationProvider(string baseUrl, string appKey, Token? refreshToken, Token? accessToken)
    {
        _httpClient.BaseAddress = new Uri(baseUrl);
        _httpClient.DefaultRequestHeaders.Add("X-App-Key", appKey);
        _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        _httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json");

        _baseUrl = baseUrl;
        _appKey = appKey;
        _refreshToken = refreshToken ?? null;
        _accessToken = accessToken ?? null;
    }

    public async Task<AuthenticationResult> AuthenticateAsync(AuthenticationRequest request)
    {
        var url = $"/api/sessions";
        var body = new JsonObject();
        if (request.GrantType == "password")
        {
            url += "?grant_type=password";
            body.Add("username", request.Username);
            body.Add("password", request.Password);
        }
        else if (request.GrantType == "refresh_token")
            url += $"?grant_type=refresh_token&refresh_token={_refreshToken.TokenValue}";
        else
            throw new ArgumentException("Invalid grant type");

        var response = await _httpClient.PostAsJsonAsync(url, body);

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<AuthenticationResult>();
            _refreshToken = result.RefreshToken;
            _accessToken = result.AccessToken;
            UserAccount = await GetUserAccountAsync();
            return result;
        }
        else
            return null;
    }

    public async Task<UserAccountResult> GetUserAccountAsync()
    {
        var url = $"/api/accounts/me";
        var response = await _httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<UserAccountResult>();
        else
            return null;
    }

    public Task<AuthenticationResult> RefreshTokenAsync()
    {
        var request = new AuthenticationRequest(_refreshToken.TokenValue);
        return AuthenticateAsync(request);
    }
}
