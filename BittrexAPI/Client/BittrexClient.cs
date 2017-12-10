using System;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BittrexAPI.Models;
using Newtonsoft.Json;

namespace BittrexAPI.Client
{
    public class Bittrex
    {
        // Initialize the Client with the base address, api version, api key and api secret
        private const string _baseAddress = "https://bittrex.com/api";
        private const string _apiVersion = "v1.1";
        
        private readonly string _apiKey, _apiSecret;
        
        // Set up different default access screens, for now we'll only use our balances and market screens
        private readonly string _publicBaseUrl, _marketBaseUrl, _accountBaseUrl;
        
        // A httpclient lets us connect to the internet
        private readonly HttpClient _httpClient;
        
        /// <summary>
        /// Make a client that will connect to the designed Bittrex api page
        /// </summary>
        /// <param name="apiKey">The API key Bittrex has given you</param>
        /// <param name="apiSecret">The API secret Bittrex has given you</param>
        public Bittrex(string apiKey, string apiSecret)
        {
            _httpClient = new HttpClient();
            
            _apiKey = apiKey;
            _apiSecret = apiSecret;

            // Modify those default access screens!
            _publicBaseUrl = $"{_baseAddress}/{_apiVersion}/public";
            _marketBaseUrl = $"{_baseAddress}/{_apiVersion}/market";
            _accountBaseUrl = $"{_baseAddress}/{_apiVersion}/account";

            // Console.WriteLine("The client has been configured succesfully, buckle your seatbelts, you're about to go onto a mission to the moon!\n");            
        }                
        
        /// <summary>
        /// Get all available markets on bittrex, URL passed in: https://bittrex.com/api/v1.1/public/getmarkets
        /// </summary>
        public async Task<ApiResult<Market[]>> GetMarkets()
        {
            var url = $"{_publicBaseUrl}/getmarkets";
            
            var result = await GetPublicAsync(url).ConfigureAwait(false);          
            
            return JsonConvert.DeserializeObject<ApiResult<Market[]>>(result);
        }
        
        /// <summary>
        /// Get all available currencies on bittrex, URL passed in: https://bittrex.com/api/v1.1/public/getcurrencies
        /// </summary>
        public async Task<ApiResult<CurrencyModel[]>> GetCurrenies()
        {
            var url = $"{_publicBaseUrl}/getcurrencies";

            var result = await GetPublicAsync(url).ConfigureAwait(false);

            return JsonConvert.DeserializeObject<ApiResult<CurrencyModel[]>>(result);
        }
        
        /// <summary>
        /// Get the ticker for a specific coin, URL passed in: https://bittrex.com/api/v1.1/public/getticker
        /// </summary>
        /// <param name="market">The marketname, e.g. BTC-LTC</param>
        public async Task<ApiResult<Ticker>> GetTicker(string market)
        {
            var url = $"{_publicBaseUrl}/getticker?market={market}";

            var result = await GetPublicAsync(url).ConfigureAwait(false);

            return JsonConvert.DeserializeObject<ApiResult<Ticker>>(result);
        }
        
        /// <summary>
        /// Get the market summaries for all available coins, URL passed in: https://bittrex.com/api/v1.1/public/getmarketsummaries
        /// </summary>
        public async Task<ApiResult<MarketSummary[]>> GetMarketSummaries()
        {
            var url = $"{_publicBaseUrl}/getmarketsummaries";

            var result = await GetPublicAsync(url).ConfigureAwait(false);

            return JsonConvert.DeserializeObject<ApiResult<MarketSummary[]>>(result);
        }
        
        /// <summary>
        /// Create a buylimit for the selected market, URL passed in: https://bittrex.com/api/v1.1/market/buylimit?apikey=APIKEY&market=MARKET&quantity=QUANTITY&rate=RATE&nonce=NONCE
        /// </summary>
        /// <param name="market">The market you want to buy, e.g. BTC-LTC</param>
        /// <param name="quantity">The amount of a coin you want to buy</param>
        /// <param name="rate">The rate at which you want to place the order</param>
        public async Task<ApiResult<BuyLimit>> BuyLimit(string market, decimal quantity, decimal rate)
        {
            var nonce = GenerateNonce();
            
            var url = $"{_marketBaseUrl}/buylimit?apikey={_apiKey}&market={market}&quantity={quantity}&rate={rate}&nonce={nonce}";

            var result = await GetPrivateAsync(url, _apiSecret).ConfigureAwait(false);

            return JsonConvert.DeserializeObject<ApiResult<BuyLimit>>(result);
        }

        /// <summary>
        /// Create a selllimit for the selected market, https://bittrex.com/api/v1.1/market/selllimit?apikey=APIKEY&market=MARKET&quantity=QUANTITY&rate=RATE&nonce=NONCE
        /// </summary>
        /// <param name="market">The market you want to sell, e.g. BTC-LTC</param>
        /// <param name="quantity">The amount of a coin you want to sell</param>
        /// <param name="rate">The rate at which you want to sell</param>
        public async Task<ApiResult<SellLimit>> SellLimit(string market, decimal quantity, decimal rate)
        {
            var nonce = GenerateNonce();
            
            var url = $"{_marketBaseUrl}/selllimit?apikey={_apiKey}&market={market}&quantity={quantity}&rate={rate}&nonce={nonce}";

            var result = await GetPrivateAsync(url, _apiSecret).ConfigureAwait(false);

            return JsonConvert.DeserializeObject<ApiResult<SellLimit>>(result);
        }
        
        /// <summary>
        /// Get the users available balance, URL passed in: https://bittrex.com/api/v1.1/getbalances?apikey=APIKEY&nonce=NONCE
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<AccountBalance[]>> GetBalance()
        {
            var nonce = GenerateNonce();

            var url = $"{_accountBaseUrl}/getbalances?apikey={_apiKey}&nonce={nonce}";

            var result = await GetPrivateAsync(url, _apiSecret).ConfigureAwait(false);

            return JsonConvert.DeserializeObject<ApiResult<AccountBalance[]>>(result);
        }

        public async Task<ApiResult<OrderHistory[]>> GetOrderHistory()
        {
            var nonce = GenerateNonce();

            var url = $"{_accountBaseUrl}/getorderhistory?apikey={_apiKey}&nonce={nonce}";

            var result = await GetPrivateAsync(url, _apiSecret).ConfigureAwait(false);

            return JsonConvert.DeserializeObject<ApiResult<OrderHistory[]>>(result);
        }
        
        /// <summary>
        /// Do a HTTP GET request to a public url
        /// </summary>
        private async Task<string> GetPublicAsync(string url)
        {                     
            // Create our GET request to the designated url
            var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url));
            
            // Send our GET request and receive a response!
            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);

            // Make sure we recieve a status code so we can further diagnose the problem if it occurs
            response.EnsureSuccessStatusCode();

            // Return our response
            return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        }
        
        /// <summary>
        /// Make a HTTP GET request to a private url, where the apikey is encrypted using HMAC-SHA512 and nonce is a timesetting
        /// </summary>
        private async Task<string> GetPrivateAsync(string url, string secret)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url));
            
            // Generate our signed apikey and attach them within the header of our HTTPGET request
            var apiSIGN = GenerateApiSign(url, secret);            
            request.Headers.Add("apisign", apiSIGN);

            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        }
        
        /// <summary>
        /// Cryptography magic is happening here, keep scrolling!
        /// </summary>
        /// <returns>This method returns a generated API signature using SHA512 hashing</returns>
        private string GenerateApiSign(string url, string secret)
        {
            var secretBytes = Encoding.ASCII.GetBytes(secret);

            using (var hmacsha512 = new HMACSHA512(secretBytes))
            {
                var hashBytes = hmacsha512.ComputeHash(Encoding.ASCII.GetBytes(url));

                return BitConverter.ToString(hashBytes).Replace("-", "");
            }
        }
        
        // Generate the DateTime difference, basic server authentication stuff
        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        private string GenerateNonce()
        {
            long nonce = (long) (DateTime.Now - Epoch).TotalSeconds;

            return nonce.ToString();
        }
        
        /// <summary>
        /// Read the users config file and process it
        /// </summary>
        /// <returns></returns>
        public ConfigReader<ConfigSerial[]> LoadConfig()
        {
            ConfigReader<ConfigSerial[]> userData;
            
            using (StreamReader reader = new StreamReader("config.json"))
            {
                string json = reader.ReadToEnd();

                userData = JsonConvert.DeserializeObject<ConfigReader<ConfigSerial[]>>(json);
            }

            return userData;
        }
    }
}
