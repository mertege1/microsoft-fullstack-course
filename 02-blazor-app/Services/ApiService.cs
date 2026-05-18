using Microsoft.Extensions.Configuration;

namespace _02_blazor_app.Services
{
    public class ApiService
    {
        private readonly IConfiguration _configuration;

        public ApiService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetApiUrl()
        {
            return _configuration["ApiSettings:BaseUrl"] ?? "URL Bulunamadı";
        }
    }
}