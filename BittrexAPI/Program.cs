using System.Threading.Tasks;
using BittrexAPI.Client;

namespace BittrexAPI
{
    public class Program
    {
        public static async Task Main()
        {           
            // Our apiKey and apiSecret variables
            const string apiKey = "";
            const string apiSecret = "";
            
            // Set up the three clients: one for the operations (such as buy/sell), one to calculate the margins and one for the CoinMarketCap data
            var bittrexClient = new Bittrex(apiKey, apiSecret);           
            var calculationClient = new CalculationClient(bittrexClient);

            // Load the user config file
            var userConfig = bittrexClient.LoadConfig();

            foreach (var item in userConfig.Result)
            {
                await calculationClient.CalculateToDollar(item.CoinName, item.Amount).ConfigureAwait(false);
            }
        }
    }
}
