using System;
namespace Agree.Athens.Presentation.WebApi.Models
{
    public class TokenResponse : Response
    {
        public TokenData Token { get; private set; }

        public TokenResponse(string token, int expiresIn) : base("Succesfully authenticated")
        {
            Token = new TokenData(token, expiresIn);
        }

        public record TokenData(string AccessToken, int ExpiresIn, string Type = "Bearer");
    }
}