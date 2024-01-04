using System.Net.Http;
using System.Threading.Tasks;
namespace Examenp2FO.Services
{
    public class GEOAPI
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "[213158463910668233746x90759]"; 

        public GEOAPI(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetGeocodeDataAsync(string location)
        {
            var response = await _httpClient.GetAsync($"https://geocode.xyz/{location}?json=1&auth={_apiKey}");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            return result; 
        }
    }
}
    





        