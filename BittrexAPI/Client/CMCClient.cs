using System;
using System.Net.Http;
using System.Threading.Tasks;
using BittrexAPI.Models;
using Newtonsoft.Json;

namespace BittrexAPI.MainClient
{
    public class CMCClient
    {
        // Initialize the client with base address and version
        private const string _baseAddress = "https://api.coinmarketcap.com";
        private const string _apiVersion = "v1";
        
        // Set up a HttpClient
        private readonly HttpClient _httpClient;

        public CMCClient()
        {
            _httpClient = new HttpClient();
        }

        public async Task<CMC[]> GetTicker()
        {
            var url = $"{_baseAddress}/{_apiVersion}/ticker/";

            var result = await GetPublicAsync(url).ConfigureAwait(false);

            return JsonConvert.DeserializeObject<CMC[]>(result);
        }

        private async Task<string> GetPublicAsync(string url)
        {
            // Create a get request
            var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url));

            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        }
    }
}