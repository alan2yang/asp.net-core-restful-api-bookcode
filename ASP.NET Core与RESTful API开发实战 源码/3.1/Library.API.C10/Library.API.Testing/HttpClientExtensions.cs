using Library.API.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Library.API.Testing
{
    public static class HttpClientExtensions
    {
        public static async Task<(bool result, string token)> TryGetBearerTokenAsync(
            this HttpClient httpClient,
            LoginUser loginUser)
        {
            var userCredentialInfo = new StringContent(
                content: JsonConvert.SerializeObject(loginUser),
                encoding: Encoding.UTF8,
                mediaType: "application/json");
            var response = await httpClient.PostAsync("auth/token", userCredentialInfo);
            var tokenResult = await response.Content.ReadAsAsync<TokenResult>();
            if (tokenResult == null)
            {
                return (false, null);
            }
            else
            {
                return (true, tokenResult.Token);
            }
        }
    }

    public class TokenResult
    {
        public DateTimeOffset Expiration { get; set; }
        public string Token { get; set; }
    }
}