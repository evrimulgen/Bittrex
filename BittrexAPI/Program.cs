using System;
using System.Threading.Tasks;
using BittrexAPI.MainClient;

namespace BittrexAPI
{
    public class Program
    {
        static async Task Main()
        {     
            string apiKey = "";
            string apiSecret = "";
            
            var bittrexClient = new Client(apiKey, apiSecret);          
        }
    }
}