using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace WebBlazor.Service
{
    public class UserService
    {
        public readonly string APIConnectionString = "http://localhost:8080/api/v1";
        public event Action OnNavbarRefresh;

        private string _token;

        public string Token
        {
            get => _token;
            set
            {
                _token = value;
                OnNavbarRefresh?.Invoke();
            }
        }

        private readonly HttpClient _httpClient;
        public HttpClient HttpClient => GetHttpClient();
        public bool IsAuthorized => Token != null;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private HttpClient GetHttpClient()
        {
            if (IsAuthorized)
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            }
            return _httpClient;
        }

        public bool IsTokenHasRole(Roles role)
        {
            if(Token == null) { return false; }
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(_token);
            var claimForCheck = new Claim(ClaimTypes.Role, role.ToString());
            var result = jwt.Claims.FirstOrDefault(c => c.Type == claimForCheck.Type && c.Value == claimForCheck.Value);
            if (result == null) { return false; }
            return true;
        }

        public string GetUsername()
        {
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(_token);
            var result = jwt.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
            return result.Value;
        }

    }

    public enum Roles
    {
        root
    }
}
