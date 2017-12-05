using System;
using System.Threading.Tasks;
using BittrexAPI.MainClient;

namespace BittrexAPI
{
    public class Program
    {
        static async Task Main()
        {
            // The commented lines is the old api key and secret, don't lose 'em!            
            /*string apiKey = "3b0ad3a4b08e49cc9e300c19d35ea726";
            string apiSecret = "05a59f7345d44d30a57b4e727d19ddfa";*/
            
            string apiKey = "6ab92b24ba904abfa3452da477a1a237 ";
            string apiSecret = "dd14659882a14862b6e638098b8fa9c3";
            
            var bittrexClient = new Client(apiKey, apiSecret);

            
        }
    }
}