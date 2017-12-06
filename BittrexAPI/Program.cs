using System;
using System.Threading.Tasks;
using BittrexAPI.MainClient;

namespace BittrexAPI
{
    public class Program
    {
        public static async Task Main()
        {           
            // Our apiKey and apiSecret variables
            string apiKey = "";
            string apiSecret = "";
            
            // Set up the three clients: one for the operations (such as buy/sell), one to calculate the margins and one for the CoinMarketCap data
            var bittrexClient = new BittrexClient(apiKey, apiSecret);           
            var calculationClient = new CalculationClient(bittrexClient);
            var cmcClient = new CMCClient();

            await calculationClient.CalculateTransactionFee("ETH");
        }
    }
}
